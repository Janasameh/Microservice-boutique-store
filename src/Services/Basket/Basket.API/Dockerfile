# Stage 1 — Build
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /app

# Copy project files first (layer caching)
COPY Boutique.slnx ./
COPY src/BuildingBlocks/Common/Common.csproj src/BuildingBlocks/Common/
COPY src/Services/Basket/Basket.Domain/Basket.Domain.csproj src/Services/Basket/Basket.Domain/
COPY src/Services/Basket/Basket.Infrastructure/Basket.Infrastructure.csproj src/Services/Basket/Basket.Infrastructure/
COPY src/Services/Basket/Basket.Application/Basket.Application.csproj src/Services/Basket/Basket.Application/
COPY src/Services/Basket/Basket.API/Basket.API.csproj src/Services/Basket/Basket.API/

# Restore dependencies
RUN dotnet restore src/Services/Basket/Basket.API/Basket.API.csproj

# Copy all source code
COPY src/ src/

# Build and publish
RUN dotnet publish src/Services/Basket/Basket.API/Basket.API.csproj \
    -c Release -o /app/publish --no-restore

# Stage 2 — Runtime
FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS runtime
WORKDIR /app
COPY --from=build /app/publish .

EXPOSE 8080
ENTRYPOINT ["dotnet", "Basket.API.dll"]
