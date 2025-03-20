# See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

# This stage is used when running from VS in fast mode (Default for Debug configuration)
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

ENV ASPNETCORE_ENVIRONMENT=Development

# This stage is used to build the service project
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["Spring25.BlCapstone.BE.APIs/Spring25.BlCapstone.BE.APIs.csproj", "Spring25.BlCapstone.BE.APIs/"]
COPY ["Spring25.BlCapstone.BackgroundServices/Spring25.BlCapstone.BackgroundServices.csproj", "Spring25.BlCapstone.BackgroundServices/"]
COPY ["Spring25.BlCapstone.BE.Repositories/Spring25.BlCapstone.BE.Repositories.csproj", "Spring25.BlCapstone.BE.Repositories/"]
COPY ["Spring25.BlCapstone.BE.Services/Spring25.BlCapstone.BE.Services.csproj", "Spring25.BlCapstone.BE.Services/"]
RUN dotnet restore "./Spring25.BlCapstone.BE.APIs/Spring25.BlCapstone.BE.APIs.csproj"
COPY . .
WORKDIR "/src/Spring25.BlCapstone.BE.APIs"
RUN dotnet build "./Spring25.BlCapstone.BE.APIs.csproj" -c $BUILD_CONFIGURATION -o /app/build

# This stage is used to publish the service project to be copied to the final stage
FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./Spring25.BlCapstone.BE.APIs.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

# This stage is used in production or when running from VS in regular mode (Default when not using the Debug configuration)
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Spring25.BlCapstone.BE.APIs.dll"]