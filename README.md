# MessageExpert
Messaging application 

## Appsettings
Database configuration managed on DatabaseConfig, you can choose database provider like postgre or mssql

 "DatabaseConfig": {
    "Provider": "Postgre",
    "ConnectionString": "User ID=postgres;Password=postgres;Host=localhost;Port=5432;Database=MessageExpert.Services.ApiDb;Pooling=true;"
  },

#### update database command

> set environment before update database

> windows

$env:DATABASE_CONNECTION_STRING='User ID=srm;Password=123456;Host=localhost;Port=5432;Database=Srm.Services.ApiDb;Pooling=true;'
dotnet ef database update --project src/Services/API/Srm.Data/Srm.Data.csproj --startup-project src/Services/API/Srm.Services.Api/Srm.Services.Api.csproj

> linux

export set for global 

export DATABASE_CONNECTION_STRING='User ID=postgres;Password=test1234;Host=localhost;Port=5432;Database=MessageExpert.Services.Api.TestDb;Pooling=true;'

set for this session

DATABASE_CONNECTION_STRING='User ID=srm;Password=123456;Host=localhost;Port=5432;Database=Srm.Services.ApiDb;Pooling=true;'
DATABASE_CONNECTION_STRING='User ID=srm;Password=123456;Host=localhost;Port=5432;Database=Srm.Services.ApiDb;Pooling=true;'  &&
dotnet ef database update --project src/Services/API/Srm.Data/Srm.Data.csproj --startup-project src/Services/API/Srm.Services.Api/Srm.Services.Api.csproj

remove environment variable
unset DATABASE_CONNECTION_STRING

### add migrations

dotnet ef migrations add [migration_name] --project src/Services/API/MessageExpert.Data/MessageExpert.Data.csproj --startup-project src/Services/API/MessageExpert.Api/
dotnet ef migrations add corporation_migrations --project src/Services/API/Srm.Data/Srm.Data.csproj --startup-project src/Services/API/Srm.Services.Api/Srm.Services.Api.csproj

## Linux 

#### Install git

sudo apt-get update

sudo apt-get upgrade

sudo apt-get install git

#### Install Docker

sudo apt install docker.io

sudo apt install docker-compose

### Install Dotnet 

wget -q https://packages.microsoft.com/config/ubuntu/18.04/packages-microsoft-prod.deb

sudo dpkg -i packages-microsoft-prod.deb

sudo add-apt-repository universe

sudo apt-get install apt-transport-https

sudo apt-get update

sudo apt-get install dotnet-sdk-2.2

### create environments folder

sudo mkdir /environments

sudo mkdir /environments/test

sudo mkdir /environments/prod

cd MessageExpert

### copy docker settings to environments

sudo cp src/Services/API/MessageExpert.Api/appsettings.Docker.json /environments

sudo cp src/Services/API/MessageExpert.Api/appsettings.json /environments/test

sudo cp src/Services/API/MessageExpert.Api/appsettings.Docker.json /environments/prod

### editting file

nano docker-compose.yml

### after at all running docker on linux

docker-compose up --build

### firewall add port

connect to gcloud linux

gcloud compute --project "PROJECT_NAME" ssh --zone "us-east1-b" "instance-1"

gcloud allow port 

gcloud compute --project "PROJECT_NAME" firewall-rules create  api-allow-http --allow tcp:81;tcp:83;tcp:85;tcp:87;tcp:89;tcp:91

gcloud compute --project "PROJECT_NAME" firewall-rules create  web-allow-http --allow  tcp:80;tcp:82;tcp:84;tcp:86;tcp:88;tcp:90

gcloud compute --project "PROJECT_NAME" firewall-rules create  postgre-allow-http --allow  tcp:5432

allow firewall 

ufw allow 81

ufw allow 82

#DbConvert
#db_dump
!!!USE ONLY COMMAND PROMT!!!! POWER SHELL CHANGE ENCODING

wOs3mODbEvu5xb26
##Trial string replace
1) replace "'," to "',
" (string satırları regexin doğru çalışması için tek satır haline getiriliyor )

2) regex
'(\d{0,3})-TRIAL-(.* ?) (\d{0,3})'
replace
'\2'


#Git

#Git password remember
>command
git config --global credential.helper cache

### reset branch to head

> clean local change

git clean -fd

> fetch all change

git fetch --all

> reset local to last change and remove local commit

git reset --hard origin/master


**Edit a file, create a new file, and clone from Bitbucket in under 2 minutes**

When you're done, you can delete the content in this README and update the file with details for others getting started with your repository.

*We recommend that you open this README in another tab as you perform the tasks below. You can [watch our video](https://youtu.be/0ocf7u76WSo) for a full demo of all the steps in this tutorial. Open the video in a new tab to avoid leaving Bitbucket.*

---

## Edit a file

You’ll start by editing this README file to learn how to edit a file in Bitbucket.

1. Click **Source** on the left side.
2. Click the README.md link from the list of files.
3. Click the **Edit** button.
4. Delete the following text: *Delete this line to make a change to the README from Bitbucket.*
5. After making your change, click **Commit** and then **Commit** again in the dialog. The commit page will open and you’ll see the change you just made.
6. Go back to the **Source** page.

---

## Create a file

Next, you’ll add a new file to this repository.

1. Click the **New file** button at the top of the **Source** page.
2. Give the file a filename of **contributors.txt**.
3. Enter your name in the empty file space.
4. Click **Commit** and then **Commit** again in the dialog.
5. Go back to the **Source** page.

Before you move on, go ahead and explore the repository. You've already seen the **Source** page, but check out the **Commits**, **Branches**, and **Settings** pages.

---

## Clone a repository

Use these steps to clone from SourceTree, our client for using the repository command-line free. Cloning allows you to work on your files locally. If you don't yet have SourceTree, [download and install first](https://www.sourcetreeapp.com/). If you prefer to clone from the command line, see [Clone a repository](https://confluence.atlassian.com/x/4whODQ).

1. You’ll see the clone button under the **Source** heading. Click that button.
2. Now click **Check out in SourceTree**. You may need to create a SourceTree account or log in.
3. When you see the **Clone New** dialog in SourceTree, update the destination path and name if you’d like to and then click **Clone**.
4. Open the directory you just created to see your repository’s files.

Now that you're more familiar with your Bitbucket repository, go ahead and add a new file locally. You can [push your change back to Bitbucket with SourceTree](https://confluence.atlassian.com/x/iqyBMg), or you can [add, commit,](https://confluence.atlassian.com/x/8QhODQ) and [push from the command line](https://confluence.atlassian.com/x/NQ0zDQ).