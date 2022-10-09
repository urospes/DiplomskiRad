FROM mcr.microsoft.com/dotnet/sdk:6.0 as build-env
WORKDIR /app


COPY *.sln .
COPY APIGateway/*.csproj ./APIGateway/

RUN dotnet restore

COPY APIGateway/. ./APIGateway/

WORKDIR /app/APIGateway
RUN dotnet publish -c Release -o out

FROM mcr.microsoft.com/dotnet/aspnet:6.0
WORKDIR /app
COPY --from=build-env /app/APIGateway/out ./

EXPOSE 80
ENTRYPOINT ["dotnet", "APIGateway.dll"]