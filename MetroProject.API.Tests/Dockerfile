# MetroProject.API.Tests/Dockerfile
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS testrunner
WORKDIR /app

# Copy solution and restore as distinct layers
COPY . ./
RUN dotnet restore

# Build only the test project
RUN dotnet build MetroProject.API.Tests/MetroProject.API.Tests.csproj --no-restore

# Run tests
ENTRYPOINT ["dotnet", "test", "MetroProject.API.Tests/MetroProject.API.Tests.csproj", "--logger:trx"]