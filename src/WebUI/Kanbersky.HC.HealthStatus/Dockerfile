#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0-buster-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0-buster-slim AS build
WORKDIR /src
COPY ["src/WebUI/Kanbersky.HC.HealthStatus/Kanbersky.HC.HealthStatus.csproj", "src/WebUI/Kanbersky.HC.HealthStatus/"]
RUN dotnet restore "src/WebUI/Kanbersky.HC.HealthStatus/Kanbersky.HC.HealthStatus.csproj"
COPY . .
WORKDIR "/src/src/WebUI/Kanbersky.HC.HealthStatus"
RUN dotnet build "Kanbersky.HC.HealthStatus.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Kanbersky.HC.HealthStatus.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Kanbersky.HC.HealthStatus.dll"]