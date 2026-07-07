# Stage 1 — Build
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /app

COPY Boutique.slnx ./
COPY src/BuildingBlocks/Common/Common.csproj src/BuildingBlocks/Common/
COPY src/Services/Ordering/Ordering.Domain/Ordering.Domain.csproj src/Services/Ordering/Ordering.Domain/
COPY src/Services/Ordering/Ordering.Infrastructure/Ordering.Infrastructure.csproj src/Services/Ordering/Ordering.Infrastructure/
COPY src/Services/Ordering/Ordering.Application/Ordering.Application.csproj src/Services/Ordering/Ordering.Application/
COPY src/Services/Ordering/Ordering.API/Ordering.API.csproj src/Services/Ordering/Ordering.API/

RUN dotnet restore src/Services/Ordering/Ordering.API/Ordering.API.csproj

COPY src/ src/

RUN dotnet publish src/Services/Ordering/Ordering.API/Ordering.API.csproj \
    -c Release -o /app/publish --no-restore

# Stage 2 — Runtime
FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS runtime
WORKDIR /app
COPY --from=build /app/publish .

EXPOSE 8080
ENTRYPOINT ["dotnet", "Ordering.API.dll"]
