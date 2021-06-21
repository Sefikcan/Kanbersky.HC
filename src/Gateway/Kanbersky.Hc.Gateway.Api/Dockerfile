#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0-buster-slim AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:5.0-buster-slim AS build
WORKDIR /src
COPY ["src/Gateway/Kanbersky.Hc.Gateway.Api/Kanbersky.Hc.Gateway.Api.csproj", "src/Gateway/Kanbersky.Hc.Gateway.Api/"]
COPY ["src/Kanbersky.HC.Core/Kanbersky.HC.Core.csproj", "src/Kanbersky.HC.Core/"]
RUN dotnet restore "src/Gateway/Kanbersky.Hc.Gateway.Api/Kanbersky.Hc.Gateway.Api.csproj"
COPY . .
WORKDIR "/src/src/Gateway/Kanbersky.Hc.Gateway.Api"
RUN dotnet build "Kanbersky.Hc.Gateway.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Kanbersky.Hc.Gateway.Api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Kanbersky.Hc.Gateway.Api.dll"]