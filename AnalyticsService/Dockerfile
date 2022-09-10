FROM mcr.microsoft.com/dotnet/sdk:6.0 as build-env
WORKDIR /app


COPY *.sln .
COPY AnalyticsService/*.csproj ./AnalyticsService/
COPY Utils/*.csproj ./Utils/

RUN dotnet restore

COPY AnalyticsService/. ./AnalyticsService/
COPY Utils/. ./Utils/

WORKDIR /app/AnalyticsService
RUN dotnet publish -c Release -o out

FROM mcr.microsoft.com/dotnet/aspnet:6.0
WORKDIR /app
COPY --from=build-env /app/AnalyticsService/out ./

EXPOSE 80
ENTRYPOINT ["dotnet", "AnalyticsService.dll"]