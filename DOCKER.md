
# Docker

## Docker Compose 

Docker compose configuration seperated for environment like test, staging or prod

## Api docker up 

### retrieving context from api parent directory

docker build -f src\Services\API\MessageExpert.Api\Dockerfile src\Services\API\.

#### -p attaching to port. 

docker run -p 90:80 mexpert/api

#### remove all docker images

docker rmi $( docker images -q) -f

#### remove all docker containers

docker rm $( docker ps -q) -f


#### remove all docker volumes

docker volume rm $(docker volume ps -qa) -f

#### run docker with volume

docker run -p 90:80 -v c:/environments/appsettings.json:/app/appsettings.json mexpert/api

#### connect to docker container

docker exec -it 21f61c5df5ef sh

### restart docker container

docker container restart [Container_Id]

#### create docker volume

docker volume create --driver local \
    --opt type=nfs \
    --opt o=addr=192.168.1.1,rw \
    --opt device=:/path/to/dir \



docker volume create -d local -o type=bind -o device=/environments  --name test
docker volume create -d local -o type=nfs -o device=c:\environments --name test1
docker volume create -d local -o type=nfs -o device=c:\environments -o o=addr=192.168.1.1 --name test2
docker volume create -d local -o type=bind -o device=c:\environments --name test3

### add volume to docker container
docker run -p 92:80 -v test1:/app mexpert/api
docker run -p 92:80 -v test2:/app mexpert/api
docker run -p 92:80 -v test3:/app mexpert/api

docker volume copy to local
docker cp $CID:/68a1fb5f0fe2 ./
docker cp $CID:/pgdata ./

# Working with docker compose

### docker compose build

docker-compose build

### docker compose up 

docker-compose up --build

### docker compose build service 

docker-compose build core-api

### docker compose restart service

docker-compose restart core-api

### docker compose up with environment

docker-compose -f docker-compose.prod.yml -f docker-compose.yml -p srm-prod up  -d  --build

docker-compose -f docker-compose.test.yml -f docker-compose.yml -p srm-test up  -d  --build



docker-compose -f docker-compose.windows.yml -f docker-compose.yml -p srm-test up  -d  --build


web spa docker file 
#Publish sdk version
FROM ahmetcagriakca/core-web-angular:latest AS base
WORKDIR /app
EXPOSE 80
#Build sdk version
FROM ahmetcagriakca/core-web-angular:build AS build
WORKDIR /src/SRM.WebSPA/ClientApp
COPY ClientApp/package.json .
RUN npm install
WORKDIR /src
COPY . .
RUN dotnet restore SRM.WebSPA.csproj

RUN dotnet build --no-restore -c Release -o /app
FROM build AS publish
RUN dotnet publish --no-restore -c Release -o /app
FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "SRM.WebSPA.dll"]
