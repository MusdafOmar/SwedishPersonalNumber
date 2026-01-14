# Build stage
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /app

# Copy solution and project files
COPY SwedishPersonalNumber.sln .
COPY src/PersonalNumber.Core/PersonalNumber.Core.csproj src/PersonalNumber.Core/
COPY src/PersonalNumber.ConsoleApp/PersonalNumber.ConsoleApp.csproj src/PersonalNumber.ConsoleApp/
COPY tests/PersonalNumber.Tests/PersonalNumber.Tests.csproj tests/PersonalNumber.Tests/

# Restore dependencies
RUN dotnet restore

# Copy everything else
COPY . .

# Build and publish
RUN dotnet publish src/PersonalNumber.ConsoleApp/PersonalNumber.ConsoleApp.csproj -c Release -o /app/publish

# Runtime stage
FROM mcr.microsoft.com/dotnet/runtime:9.0
WORKDIR /app
COPY --from=build /app/publish .

ENTRYPOINT ["dotnet", "PersonalNumber.ConsoleApp.dll"]
