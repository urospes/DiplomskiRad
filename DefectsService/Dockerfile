FROM mcr.microsoft.com/dotnet/sdk:6.0 as build-env
WORKDIR /app


COPY *.sln .
COPY DefectsServiceAPI/*.csproj ./DefectsServiceAPI/

RUN dotnet restore

COPY DefectsServiceAPI/. ./DefectsServiceAPI/

WORKDIR /app/DefectsServiceAPI
RUN dotnet publish -c Release -o out

FROM mcr.microsoft.com/dotnet/aspnet:6.0
WORKDIR /app
COPY --from=build-env /app/DefectsServiceAPI/out ./

EXPOSE 80
ENTRYPOINT ["dotnet", "DefectsServiceAPI.dll"]