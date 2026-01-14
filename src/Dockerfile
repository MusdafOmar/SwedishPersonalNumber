# ---- Build stage ----
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src

# Copy solution + csproj files first (better caching)
COPY SwedishPersonalNumber.sln ./
COPY src/PersonalNumber.Core/PersonalNumber.Core.csproj src/PersonalNumber.Core/
COPY src/PersonalNumber.ConsoleApp/PersonalNumber.ConsoleApp.csproj src/PersonalNumber.ConsoleApp/

RUN dotnet restore

# Copy everything else and publish
COPY src/ src/
RUN dotnet publish src/PersonalNumber.ConsoleApp/PersonalNumber.ConsoleApp.csproj -c Release -o /app/publish

# ---- Runtime stage ----
FROM mcr.microsoft.com/dotnet/runtime:9.0
WORKDIR /app
COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "PersonalNumber.ConsoleApp.dll"]
