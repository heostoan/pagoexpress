# Consulte https://aka.ms/customizecontainer para aprender a personalizar su contenedor de depuración y cómo Visual Studio usa este Dockerfile para compilar sus imágenes para una depuración más rápida.

# Esta fase se usa cuando se ejecuta desde VS en modo rápido (valor predeterminado para la configuración de depuración)
FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081


# Esta fase se usa para compilar el proyecto de servicio
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["src/Presentation/UW.AdminPagoExpress/UW.AdminPagoExpress/UW.AdminPagoExpress.csproj", "src/Presentation/UW.AdminPagoExpress/UW.AdminPagoExpress/"]
COPY ["src/Core/UW.AdminPagoExpress.Application/UW.AdminPagoExpress.Application.csproj", "src/Core/UW.AdminPagoExpress.Application/"]
COPY ["src/Shared/UW.AdminPagoExpress.Shared/UW.AdminPagoExpress.Shared.csproj", "src/Shared/UW.AdminPagoExpress.Shared/"]
COPY ["src/Core/UW.AdminPagoExpress.Domain/UW.AdminPagoExpress.Domain.csproj", "src/Core/UW.AdminPagoExpress.Domain/"]
COPY ["src/Infraestructure/UW.AdminPagoExpress.Infraestructure/UW.AdminPagoExpress.Infraestructure.csproj", "src/Infraestructure/UW.AdminPagoExpress.Infraestructure/"]
COPY ["src/Presentation/UW.AdminPagoExpress/UW.AdminPagoExpress.Client/UW.AdminPagoExpress.Client.csproj", "src/Presentation/UW.AdminPagoExpress/UW.AdminPagoExpress.Client/"]
RUN dotnet restore "./src/Presentation/UW.AdminPagoExpress/UW.AdminPagoExpress/UW.AdminPagoExpress.csproj"
COPY . .
WORKDIR "/src/src/Presentation/UW.AdminPagoExpress/UW.AdminPagoExpress"
RUN dotnet build "./UW.AdminPagoExpress.csproj" -c $BUILD_CONFIGURATION -o /app/build

# Esta fase se usa para publicar el proyecto de servicio que se copiará en la fase final.
FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./UW.AdminPagoExpress.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

# Esta fase se usa en producción o cuando se ejecuta desde VS en modo normal (valor predeterminado cuando no se usa la configuración de depuración)
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "UW.AdminPagoExpress.dll"]