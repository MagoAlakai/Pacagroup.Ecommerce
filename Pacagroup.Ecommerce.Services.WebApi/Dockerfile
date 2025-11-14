# --- BUILD STAGE ---
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src

# Copiamos solo los .csproj para cachear la restauración
COPY Pacagroup.Ecommerce.Services.WebApi/Pacagroup.Ecommerce.Services.WebApi.csproj Pacagroup.Ecommerce.Services.WebApi/
COPY Pacagroup.Ecommerce.Aplicaion.DTO/Pacagroup.Ecommerce.Aplicacion.DTO.csproj Pacagroup.Ecommerce.Aplicaion.DTO/
COPY Pacagroup.Ecommerce.Aplicaion.Interface/Pacagroup.Ecommerce.Aplicacion.Interface.csproj Pacagroup.Ecommerce.Aplicaion.Interface/
COPY Pacagroup.Ecommerce.Aplicaion.Main/Pacagroup.Ecommerce.Aplicacion.Main.csproj Pacagroup.Ecommerce.Aplicaion.Main/
COPY Pacagroup.Ecommerce.Domain.Entity/Pacagroup.Ecommerce.Domain.Entity.csproj Pacagroup.Ecommerce.Domain.Entity/
COPY Pacagroup.Ecommerce.Domain.Interface/Pacagroup.Ecommerce.Domain.Interface.csproj Pacagroup.Ecommerce.Domain.Interface/
COPY Pacagroup.Ecommerce.Infraestructura.Data/Pacagroup.Ecommerce.Infraestructura.Data.csproj Pacagroup.Ecommerce.Infraestructura.Data/
COPY Pacagroup.Ecommerce.Infraestructura.Interface/Pacagroup.Ecommerce.Infraestructura.Interface.csproj Pacagroup.Ecommerce.Infraestructura.Interface/
COPY Pacagroup.Ecommerce.Infraestructura.Repository/Pacagroup.Ecommerce.Infraestructura.Repository.csproj Pacagroup.Ecommerce.Infraestructura.Repository/
COPY Pacagroup.Ecommerce.Transversal.Common/Pacagroup.Ecommerce.Transversal.Common.csproj Pacagroup.Ecommerce.Transversal.Common/
COPY Pacagroup.Ecommerce.Transversal.Logging/Pacagroup.Ecommerce.Transversal.Logging.csproj Pacagroup.Ecommerce.Transversal.Logging/

RUN dotnet restore Pacagroup.Ecommerce.Services.WebApi/Pacagroup.Ecommerce.Services.WebApi.csproj

# Copiamos el resto del código
COPY . .

# Publicamos
RUN dotnet publish Pacagroup.Ecommerce.Services.WebApi/Pacagroup.Ecommerce.Services.WebApi.csproj \
    -c Debug -o /app/publish /p:UseAppHost=false


# --- RUNTIME STAGE ---
FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS runtime
WORKDIR /app

ENV ASPNETCORE_URLS=http://+:8080
EXPOSE 8080

COPY --from=build /app/publish .

ENTRYPOINT ["dotnet", "Pacagroup.Ecommerce.Services.WebApi.dll"]
