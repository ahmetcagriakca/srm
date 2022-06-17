#Publish sdk version
FROM ahmetcagriakca/core-web-angular:latest AS base
WORKDIR /app
EXPOSE 80
#Build sdk version
FROM ahmetcagriakca/core-web-angular:build AS build
WORKDIR /src
COPY . .
RUN dotnet restore SRM.WebSPA.csproj
RUN cd ClientApp \
&& npm i --silent
RUN dotnet build --no-restore -c Release -o /app
FROM build AS publish
RUN dotnet publish --no-restore -c Release -o /app
FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "SRM.WebSPA.dll"]
