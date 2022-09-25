FROM mcr.microsoft.com/dotnet/sdk:6.0 as build-env
WORKDIR /app


COPY *.sln .
COPY CarsServiceAPI/*.csproj ./CarsServiceAPI/

RUN dotnet restore

COPY CarsServiceAPI/. ./CarsServiceAPI/

WORKDIR /app/CarsServiceAPI
RUN dotnet publish -c Release -o out

FROM mcr.microsoft.com/dotnet/aspnet:6.0
WORKDIR /app
COPY --from=build-env /app/CarsServiceAPI/out ./

EXPOSE 80
ENTRYPOINT ["dotnet", "CarsServiceAPI.dll"]