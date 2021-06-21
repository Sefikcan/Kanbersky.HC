#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0-buster-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0-buster-slim AS build
WORKDIR /src
COPY ["src/Gateway/Ocelot/Kanbersky.HC.Ocelot/Kanbersky.HC.Ocelot.csproj", "src/Gateway/Ocelot/Kanbersky.HC.Ocelot/"]
COPY ["src/Kanbersky.HC.Core/Kanbersky.HC.Core.csproj", "src/Kanbersky.HC.Core/"]
RUN dotnet restore "src/Gateway/Ocelot/Kanbersky.HC.Ocelot/Kanbersky.HC.Ocelot.csproj"
COPY . .
WORKDIR "/src/src/Gateway/Ocelot/Kanbersky.HC.Ocelot"
RUN dotnet build "Kanbersky.HC.Ocelot.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Kanbersky.HC.Ocelot.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Kanbersky.HC.Ocelot.dll"]