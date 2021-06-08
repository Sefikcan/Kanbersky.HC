#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0-buster-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0-buster-slim AS build
WORKDIR /src
COPY ["src/Services/Basket/Kanbersky.HC.Basket.Api/Kanbersky.HC.Basket.Api.csproj", "src/Services/Basket/Kanbersky.HC.Basket.Api/"]
RUN dotnet restore "src/Services/Basket/Kanbersky.HC.Basket.Api/Kanbersky.HC.Basket.Api.csproj"
COPY . .
WORKDIR "/src/src/Services/Basket/Kanbersky.HC.Basket.Api"
RUN dotnet build "Kanbersky.HC.Basket.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Kanbersky.HC.Basket.Api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Kanbersky.HC.Basket.Api.dll"]