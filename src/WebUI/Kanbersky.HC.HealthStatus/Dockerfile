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

# install the agent
RUN  mkdir /usr/local/newrelic-netcore20-agent \
&& cd /usr/local \
&& export NEW_RELIC_DOWNLOAD_URI=https://download.newrelic.com/$(wget -qO - "https://nr-downloads-main.s3.amazonaws.com/?delimiter=/&prefix=dot_net_agent/latest_release/newrelic-netcore20-agent" | grep -E -o 'dot_net_agent/latest_release/newrelic-netcore20-agent_[[:digit:]]{1,3}(\.[[:digit:]]{1,3}){3}_amd64\.tar\.gz') \
&& echo "Downloading: $NEW_RELIC_DOWNLOAD_URI into $(pwd)" \
&& wget -O - "$NEW_RELIC_DOWNLOAD_URI" | gzip -dc | tar xf -

# Enable the agent
ENV CORECLR_ENABLE_PROFILING=1 \
CORECLR_PROFILER={36032161-FFC0-4B61-B559-F6C5D41BAE5A} \
CORECLR_NEWRELIC_HOME=/usr/local/newrelic-netcore20-agent \
CORECLR_PROFILER_PATH=/usr/local/newrelic-netcore20-agent/libNewRelicProfiler.so \
NEW_RELIC_LICENSE_KEY=<YOUR_LICENSE_KEY> \
NEW_RELIC_APP_NAME="<APP-NAME>"

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Kanbersky.HC.HealthStatus.dll"]