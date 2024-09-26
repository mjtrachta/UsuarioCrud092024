# Etapa base para la ejecución de la aplicación
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 8080

# Etapa de construcción
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["UsuarioAPI/UsuarioAPI.csproj", "UsuarioAPI/"]
RUN dotnet restore "UsuarioAPI/UsuarioAPI.csproj"
COPY . .
WORKDIR "/src/UsuarioAPI"
RUN dotnet build "UsuarioAPI.csproj" -c Release -o /app/build

# Etapa de publicación
FROM build AS publish
RUN dotnet publish "UsuarioAPI.csproj" -c Release -o /app/publish /p:UseAppHost=false

# Imagen final para ejecución
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "UsuarioAPI.dll"]
