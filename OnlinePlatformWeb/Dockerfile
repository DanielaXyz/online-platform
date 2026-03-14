FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /app

COPY OnlinePlatformWeb.csproj .
RUN dotnet restore

COPY . .
RUN dotnet publish -c Release -o /app/publish

FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS runtime
WORKDIR /app
EXPOSE 80
EXPOSE 443

COPY --from=build /app/publish .

RUN mkdir -p /https

ENTRYPOINT ["dotnet", "OnlinePlatformWeb.dll"]