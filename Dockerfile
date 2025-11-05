# Build
FROM mcr.microsoft.com/dotnet/sdk:9.0@sha256:3fcf6f1e809c0553f9feb222369f58749af314af6f063f389cbd2f913b4ad556 AS build
WORKDIR /App

# Copia solo los archivos de la solución y proyectos necesarios
COPY *.sln .
COPY Web.API/*.csproj ./Web.API/
COPY Application/*.csproj ./Application/
COPY Domain/*.csproj ./Domain/
COPY Infrastructure/*.csproj ./Infrastructure/

# Restore solo Web.API
RUN dotnet restore Web.API/Web.API.csproj

# Copia todo el código
COPY . .

# Publica Web.API
RUN dotnet publish Web.API/Web.API.csproj -c Release -o /app/publish

# Runtime
FROM mcr.microsoft.com/dotnet/aspnet:9.0
WORKDIR /app
COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "Web.API.dll"]
