# Build stage
FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /src

# Copy project files
COPY Booking.API/Booking.API.csproj Booking.API/
COPY Booking.Application/Booking.Application.csproj Booking.Application/
COPY Booking.Domain/Booking.Domain.csproj Booking.Domain/
COPY Booking.Infrastructure/Booking.Infrastructure.csproj Booking.Infrastructure/

# Restore dependencies
RUN dotnet restore Booking.API/Booking.API.csproj

# Copy everything else
COPY . .

# Build and publish
WORKDIR /src/Booking.API
RUN dotnet publish -c Release -o /app/publish

# Runtime stage
FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS runtime
WORKDIR /app
COPY --from=build /app/publish .

EXPOSE 8080
ENTRYPOINT ["dotnet", "Booking.API.dll"]