FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
USER app
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src

COPY [".", "."]

RUN dotnet restore "./src/Services/Discount.API/Discount.API.csproj"

COPY . .

RUN dotnet build "./src/Services/Discount.API/Discount.API.csproj" \
    -c $BUILD_CONFIGURATION \
    -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release

RUN dotnet publish "./src/Services/Discount.API/Discount.API.csproj" \
    -c $BUILD_CONFIGURATION \
    -o /app/publish \
    /p:UseAppHost=false

RUN dotnet dev-certs https -ep %APPDATA%\.aspnet\https\aspnetapp.pfx -p password

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Discount.API.dll"]
