FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /src
# Вместо конкретного имени используем маску:
COPY *.csproj ./
RUN dotnet restore
COPY . .
RUN dotnet publish "GroupService.csproj" -c Release -o /app/publish

FROM mcr.microsoft.com/dotnet/aspnet:10.0
WORKDIR /app
COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "GroupService.dll"]