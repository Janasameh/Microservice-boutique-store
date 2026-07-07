# Stage 1 — Build
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /app

COPY Boutique.slnx ./
COPY src/BuildingBlocks/Common/Common.csproj src/BuildingBlocks/Common/
COPY src/Services/Notification/Notification.Worker/Notification.Worker.csproj src/Services/Notification/Notification.Worker/

RUN dotnet restore src/Services/Notification/Notification.Worker/Notification.Worker.csproj

COPY src/ src/

RUN dotnet publish src/Services/Notification/Notification.Worker/Notification.Worker.csproj \
    -c Release -o /app/publish --no-restore

# Stage 2 — Runtime (worker image — no ASP.NET runtime needed)
FROM mcr.microsoft.com/dotnet/runtime:9.0 AS runtime
WORKDIR /app
COPY --from=build /app/publish .

ENTRYPOINT ["dotnet", "Notification.Worker.dll"]
