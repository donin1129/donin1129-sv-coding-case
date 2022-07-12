# Coding Case


## Prerequisites
Install Docker
Install .NET 6.0 SDK
Install Angular

## Set up the environment

1. Set working directory to current repository directory
2. Migrate database **.\assets\configuration-items\docker\Build_db.ps1**
3. Spin up database **docker compose -f ".\assets\configuration-items\docker\db.yml" up**
4. Start Backend and Frontend application **dotnet run --project .\src\WebUI**
5. Start Angular ClientApp by open **https://localhost:5001/** in web browser. Will be automatically redirected to **https://localhost:44447/**
6. Start the simplified stand alone the licensing generator service **dotnet run  --project .\assets\license-signature-generator\LicenseSignatureGenerator**

## TODO

Add automatic tests to validate the performances of the application. 
Improve search engine query performance by implementing linq translatable methods. 
Containerize license signature generator service. 
Encrypt SignalR hub and client communication via auth workflow. 
Host the services. 