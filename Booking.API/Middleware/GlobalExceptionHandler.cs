using System;
using System.Diagnostics;
using System.Net.Mime;
using System.Runtime.ExceptionServices;
using System.Text.Json;
using Booking.Application.Abstractions;
using Booking.Infrastructure.Configuration;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace Booking.API.Middleware
{
    public class GlobalExceptionHandler
    {
        private static readonly JsonSerializerOptions JsonOptions = new()
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            WriteIndented = false
        };

        private readonly RequestDelegate _next;
        private readonly ILogger<GlobalExceptionHandler> _logger;
        private readonly IHostEnvironment _env;
        private readonly string _logTopic;

        public GlobalExceptionHandler(
            RequestDelegate next,
            ILogger<GlobalExceptionHandler> logger,
            IHostEnvironment env,
            IOptions<KafkaProducerOptions> kafkaOptions)
        {
            _next = next;
            _logger = logger;
            _env = env;
            _logTopic = kafkaOptions.Value.ErrorTopic;
        }

        public async Task InvokeAsync(HttpContext context, IEventPublisher eventPublisher)
        {
            Exception? caughtEx = null;

            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                caughtEx = ex;
                await HandleExceptionAsync(context, ex);
            }
            finally
            {
                await PublishRequestLog(context, context.Response.StatusCode, caughtEx, eventPublisher);
            }
        }

        private async Task HandleExceptionAsync(HttpContext context, Exception ex)
        {
            var (status, title, detail) = MapException(ex);

            var traceId = Activity.Current?.Id ?? context.TraceIdentifier;

            _logger.LogError(ex,
                "Unhandled exception. {Method} {Path}. TraceId: {TraceId}",
                context.Request.Method,
                context.Request.Path.Value,
                traceId);

            if (context.Response.HasStarted)
            {
                _logger.LogWarning(
                    "Response already started; cannot write exception response. TraceId: {TraceId}",
                    traceId);
                ExceptionDispatchInfo.Capture(ex).Throw();
                return;
            }

            context.Response.Clear();
            context.Response.StatusCode = status;
            context.Response.ContentType = MediaTypeNames.Application.Json;

            var payload = new ExceptionDetails
            {
                Status = status,
                Title = title,
                Detail = detail,
                TraceId = traceId,
                ExceptionType = _env.IsDevelopment() ? ex.GetType().FullName : null,
                StackTrace = _env.IsDevelopment() ? ex.StackTrace : null
            };

            await context.Response.WriteAsync(JsonSerializer.Serialize(payload, JsonOptions));
        }

        private async Task PublishRequestLog(HttpContext context, int status, Exception? ex, IEventPublisher eventPublisher)
        {
            try
            {
                var statusType = status switch
                {
                    >= 200 and < 300 => "Success",
                    >= 400 and < 500 => "ClientError",
                    _ => "ServerError"
                };

                await eventPublisher.PublishAsync(_logTopic, new
                {
                    method = context.Request.Method,
                    path = context.Request.Path.Value,
                    statusCode = status,
                    statusType,
                    exceptionMessage = ex?.Message,
                    exceptionType = ex?.GetType().FullName,
                    stackTrace = status == StatusCodes.Status500InternalServerError ? ex?.StackTrace : null,
                    occurredAt = DateTime.UtcNow
                });
            }
            catch (Exception publishEx)
            {
                _logger.LogError(publishEx, "Failed to publish request log to Kafka");
            }
        }

        private static (int status, string title, string? detail) MapException(Exception ex)
        {
            return ex switch
            {
                ArgumentException aex => (StatusCodes.Status400BadRequest, "Bad request", aex.Message),
                UnauthorizedAccessException uex => (StatusCodes.Status401Unauthorized, "Unauthorized", uex.Message),
                KeyNotFoundException knf => (StatusCodes.Status404NotFound, "Not found", knf.Message),
                InvalidOperationException ioex => (StatusCodes.Status409Conflict, "Conflict", ioex.Message),
                DbUpdateException dbex => (StatusCodes.Status409Conflict, "Database conflict", dbex.InnerException?.Message ?? dbex.Message),
                _ => (StatusCodes.Status500InternalServerError, "Internal server error", "An unexpected error occurred.")
            };
        }
    }

    public static class GlobalExceptionHandlerExtensions
    {
        public static IApplicationBuilder UseGlobalExceptionHandler(this IApplicationBuilder app)
            => app.UseMiddleware<GlobalExceptionHandler>();
    }
}
