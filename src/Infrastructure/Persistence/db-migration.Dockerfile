FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build

WORKDIR /src

COPY "src/Enums/*.csproj" ./Enums/
COPY "src/Domain/*.csproj" ./Domain/
COPY "src/DTOs/*.csproj" ./DTOs/
COPY "src/Application/*.csproj" ./Application/
COPY "src/Infrastructure/*.csproj" ./Infrastructure/
COPY "src/WebUI/*.csproj" ./WebUI/

RUN dotnet restore ./WebUI/WebUI.csproj

COPY "src/Enums/." ./Enums/
COPY "src/Domain/." ./Domain/
COPY "src/DTOs/." ./DTOs/
COPY "src/Application/." ./Application/
COPY "src/Infrastructure/." ./Infrastructure/
COPY "src/WebUI/." ./WebUI/

ENTRYPOINT ["sh", "-c", "dotnet ef database update --project .\Infrastructure"]