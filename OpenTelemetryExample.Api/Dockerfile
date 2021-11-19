FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["OpenTelemetryExample.Api/OpenTelemetryExample.Api.csproj", "OpenTelemetryExample.Api/"]
RUN dotnet restore "OpenTelemetryExample.Api/OpenTelemetryExample.Api.csproj"
COPY . .
WORKDIR "/src/OpenTelemetryExample.Api"
RUN dotnet build "OpenTelemetryExample.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "OpenTelemetryExample.Api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "OpenTelemetryExample.Api.dll"]
