#Publish sdk version
FROM ahmetcagriakca/core-web-angular:latest AS base
WORKDIR /app
EXPOSE 443

#Build sdk version
FROM microsoft/dotnet:2.2-sdk AS build
WORKDIR /src
COPY . .
RUN dotnet restore IdentityServer/IdentityServer.csproj
WORKDIR /src/IdentityServer
RUN dotnet build --no-restore -c Release -o /app

FROM build AS publish
RUN dotnet publish --no-restore -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "IdentityServer.dll"]
