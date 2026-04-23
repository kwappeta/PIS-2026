FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /src
# Замените ApiGateway.csproj на реальное имя, если оно другое
# Вместо конкретного имени используем маску:
COPY *.csproj ./
RUN dotnet restore
COPY . .
RUN dotnet publish "ApiGateway.csproj" -c Release -o /app/publish

FROM mcr.microsoft.com/dotnet/aspnet:10.0
WORKDIR /app
COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "ApiGateway.dll"]