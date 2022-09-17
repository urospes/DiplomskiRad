FROM mcr.microsoft.com/dotnet/sdk:6.0 as build-env
WORKDIR /app


COPY *.sln .
COPY KafkaBridgeService/*.csproj ./KafkaBridgeService/
COPY Utils/*.csproj ./Utils/

RUN dotnet restore

COPY KafkaBridgeService/. ./KafkaBridgeService/
COPY Utils/. ./Utils/

WORKDIR /app/KafkaBridgeService
RUN dotnet publish -c Release -o out

FROM mcr.microsoft.com/dotnet/aspnet:6.0
WORKDIR /app
COPY --from=build-env /app/KafkaBridgeService/out ./

EXPOSE 80
ENTRYPOINT ["dotnet", "KafkaBridgeService.dll"]