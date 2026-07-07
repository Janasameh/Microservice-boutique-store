# Stage 1 — Build
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /app

COPY src/Services/Identity/Identity.API/Identity.API.csproj src/Services/Identity/Identity.API/

RUN dotnet restore src/Services/Identity/Identity.API/Identity.API.csproj

COPY src/Services/Identity/ src/Services/Identity/

RUN dotnet publish src/Services/Identity/Identity.API/Identity.API.csproj \
    -c Release -o /app/publish --no-restore

# Stage 2 — Runtime
FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS runtime
WORKDIR /app
COPY --from=build /app/publish .

EXPOSE 8080
ENTRYPOINT ["dotnet", "Identity.API.dll"]
