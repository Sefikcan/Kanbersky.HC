#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0-buster-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0-buster-slim AS build
WORKDIR /src
COPY ["src/Services/Catalog/Kanbersky.HC.Catalog.Api/Kanbersky.HC.Catalog.Api.csproj", "src/Services/Catalog/Kanbersky.HC.Catalog.Api/"]
RUN dotnet restore "src/Services/Catalog/Kanbersky.HC.Catalog.Api/Kanbersky.HC.Catalog.Api.csproj"
COPY . .
WORKDIR "/src/src/Services/Catalog/Kanbersky.HC.Catalog.Api"
RUN dotnet build "Kanbersky.HC.Catalog.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Kanbersky.HC.Catalog.Api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Kanbersky.HC.Catalog.Api.dll"]