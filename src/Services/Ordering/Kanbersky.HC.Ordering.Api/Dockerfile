#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0-buster-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0-buster-slim AS build
WORKDIR /src
COPY ["src/Services/Ordering/Kanbersky.HC.Ordering.Api/Kanbersky.HC.Ordering.Api.csproj", "src/Services/Ordering/Kanbersky.HC.Ordering.Api/"]
COPY ["src/Kanbersky.HC.Core/Kanbersky.HC.Core.csproj", "src/Kanbersky.HC.Core/"]
COPY ["src/Services/Ordering/Kanbersky.HC.Ordering.Services/Kanbersky.HC.Ordering.Services.csproj", "src/Services/Ordering/Kanbersky.HC.Ordering.Services/"]
COPY ["src/Services/Ordering/Kanbersky.HC.Ordering.Infrastructure/Kanbersky.HC.Ordering.Infrastructure.csproj", "src/Services/Ordering/Kanbersky.HC.Ordering.Infrastructure/"]
RUN dotnet restore "src/Services/Ordering/Kanbersky.HC.Ordering.Api/Kanbersky.HC.Ordering.Api.csproj"
COPY . .
WORKDIR "/src/src/Services/Ordering/Kanbersky.HC.Ordering.Api"
RUN dotnet build "Kanbersky.HC.Ordering.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Kanbersky.HC.Ordering.Api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Kanbersky.HC.Ordering.Api.dll"]