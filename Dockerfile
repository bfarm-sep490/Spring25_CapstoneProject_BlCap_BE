#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

ENV ASPNETCORE_ENVIRONMENT=Development

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["Spring25.BlCapstone.BE.APIs/Spring25.BlCapstone.BE.APIs.csproj", "Spring25.BlCapstone.BE.APIs/"]
COPY ["Spring25.BlCapstone.BE.Repositories/Spring25.BlCapstone.BE.Repositories.csproj", "Spring25.BlCapstone.BE.Repositories/"]
COPY ["Spring25.BlCapstone.BE.Services/Spring25.BlCapstone.BE.Services.csproj", "Spring25.BlCapstone.BE.Services/"]
RUN dotnet restore "./Spring25.BlCapstone.BE.APIs/Spring25.BlCapstone.BE.APIs.csproj"
COPY . .
WORKDIR "/src/Spring25.BlCapstone.BE.APIs"
RUN dotnet build "./Spring25.BlCapstone.BE.APIs.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./Spring25.BlCapstone.BE.APIs.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Spring25.BlCapstone.BE.APIs.dll"]