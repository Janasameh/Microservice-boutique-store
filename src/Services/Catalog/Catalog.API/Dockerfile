# Stage 1 — Build
FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /app

# Copy solution and project files first (layer caching optimization)
COPY Boutique.slnx ./
COPY src/BuildingBlocks/Common/Common.csproj src/BuildingBlocks/Common/
COPY src/Services/Catalog/Catalog.Domain/Catalog.Domain.csproj src/Services/Catalog/Catalog.Domain/
COPY src/Services/Catalog/Catalog.Infrastructure/Catalog.Infrastructure.csproj src/Services/Catalog/Catalog.Infrastructure/
COPY src/Services/Catalog/Catalog.API/Catalog.API.csproj src/Services/Catalog/Catalog.API/

# Restore dependencies
RUN dotnet restore src/Services/Catalog/Catalog.API/Catalog.API.csproj

# Copy all source code
COPY src/ src/

# Build and publish
RUN dotnet publish src/Services/Catalog/Catalog.API/Catalog.API.csproj \
    -c Release -o /app/publish --no-restore

# Stage 2 — Runtime (smaller image, no SDK)
FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS runtime
WORKDIR /app
COPY --from=build /app/publish .

EXPOSE 8080
ENTRYPOINT ["dotnet", "Catalog.API.dll"]
