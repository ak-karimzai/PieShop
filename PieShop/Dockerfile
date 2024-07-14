FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["PieShop/PieShop.csproj", "PieShop/"]
RUN dotnet restore "PieShop/PieShop.csproj"
COPY . .
WORKDIR "/src/PieShop"
RUN dotnet build "PieShop.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "PieShop.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .

USER root
RUN apt-get -q update && apt-get -qy install netcat-traditional
COPY ./ShellScripts/wait-for.sh .
COPY ./ShellScripts/start.sh .

ENTRYPOINT ["dotnet", "PieShop.dll"]
