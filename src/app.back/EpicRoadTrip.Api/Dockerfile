FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
ENV ASPNETCORE_ENVIRONMENT=Development

USER app
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Debug
WORKDIR /src
COPY ["EpicRoadTrip.Api/EpicRoadTrip.Api.csproj", "EpicRoadTrip.Api/"]
RUN dotnet restore "./EpicRoadTrip.Api/EpicRoadTrip.Api.csproj"
COPY . .
WORKDIR "/src/EpicRoadTrip.Api"
RUN dotnet build "./EpicRoadTrip.Api.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Debug
RUN dotnet publish "./EpicRoadTrip.Api.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "EpicRoadTrip.Api.dll"]