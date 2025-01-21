# Imagem base para .NET SDK
FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /app

# Copia os arquivos e restaura dependências
COPY *.csproj ./
RUN dotnet restore

# Copia o restante do código e compila
COPY . ./
RUN dotnet publish -c Release -o out

# Imagem runtime para rodar a aplicação
FROM mcr.microsoft.com/dotnet/aspnet:7.0
WORKDIR /app
COPY --from=build /app/out .

# Define a porta e inicia a aplicação
ENV ASPNETCORE_URLS=http://+:8080
EXPOSE 8080
ENTRYPOINT ["dotnet", "GitHubBlipAPI.dll"]
