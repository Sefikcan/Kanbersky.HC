#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0-buster-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0-buster-slim AS build
WORKDIR /src
COPY ["src/BFF/Kanbersky.HC.BFF.Api/Kanbersky.HC.BFF.Api.csproj", "src/BFF/Kanbersky.HC.BFF.Api/"]
COPY ["src/BFF/Kanbersky.HC.BFF.Services/Kanbersky.HC.BFF.Services.csproj", "src/BFF/Kanbersky.HC.BFF.Services/"]
COPY ["src/Kanbersky.HC.Core/Kanbersky.HC.Core.csproj", "src/Kanbersky.HC.Core/"]
RUN dotnet restore "src/BFF/Kanbersky.HC.BFF.Api/Kanbersky.HC.BFF.Api.csproj"
COPY . .
WORKDIR "/src/src/BFF/Kanbersky.HC.BFF.Api"
RUN dotnet build "Kanbersky.HC.BFF.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Kanbersky.HC.BFF.Api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Kanbersky.HC.BFF.Api.dll"]