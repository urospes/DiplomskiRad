FROM mcr.microsoft.com/dotnet/sdk:6.0 as build-env
WORKDIR /app


COPY *.sln .
COPY DatastoreService/*.csproj ./DatastoreService/
COPY Utils/*.csproj ./Utils/
COPY MeasurementClasses/*.csproj ./MeasurementClasses/

RUN dotnet restore

COPY DatastoreService/. ./DatastoreService/
COPY Utils/. ./Utils/
COPY MeasurementClasses/. ./MeasurementClasses/

WORKDIR /app/DatastoreService
RUN dotnet publish -c Release -o out

FROM mcr.microsoft.com/dotnet/aspnet:6.0
WORKDIR /app
COPY --from=build-env /app/DatastoreService/out ./

EXPOSE 80
ENTRYPOINT ["dotnet", "DatastoreService.dll"]