# Estágio de build
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# 1. Copiar arquivos de solução e projetos (caminhos relativos à raiz da solução)
COPY ["../AndreSalgados.sln", "."]
COPY ["../Application.Core/Application.Core.csproj", "Application.Core/"]
COPY ["../Infrastructure/Infrastructure.csproj", "Infrastructure/"]
COPY ["./AndreSalgados.csproj", "AndreSalgados/"]

# 2. Restaurar pacotes
RUN dotnet restore "AndreSalgados.sln"

# 3. Copiar todo o código fonte
COPY ../ .

# 4. Build da aplicação
WORKDIR "/src/AndreSalgados"
RUN dotnet publish -c Release -o /app/publish

# Estágio de runtime
FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app
COPY --from=build /app/publish .
COPY ./entrypoint.sh .
RUN chmod +x ./entrypoint.sh

ENTRYPOINT ["./entrypoint.sh"]