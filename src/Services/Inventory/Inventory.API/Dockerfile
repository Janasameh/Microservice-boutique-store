# Stage 1 — Build
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /app

# Copy project files first (layer caching)
COPY Boutique.slnx ./
COPY src/BuildingBlocks/Common/Common.csproj src/BuildingBlocks/Common/
COPY src/Services/Inventory/Inventory.API/Inventory.API.csproj src/Services/Inventory/Inventory.API/

# Restore dependencies
RUN dotnet restore src/Services/Inventory/Inventory.API/Inventory.API.csproj

# Copy source code
COPY src/BuildingBlocks/Common/ src/BuildingBlocks/Common/
COPY src/Services/Inventory/ src/Services/Inventory/

# Build and publish
RUN dotnet publish src/Services/Inventory/Inventory.API/Inventory.API.csproj \
    -c Release -o /app/publish --no-restore

# Stage 2 — Runtime
FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS runtime
WORKDIR /app
COPY --from=build /app/publish .

EXPOSE 8080
ENTRYPOINT ["dotnet", "Inventory.API.dll"]
