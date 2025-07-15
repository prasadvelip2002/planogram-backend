# Use the official .NET SDK image to build the app
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copy csproj and restore as distinct layers
COPY . .
RUN dotnet restore "PlanogramBackend.csproj"

# Build and publish the app
RUN dotnet publish "PlanogramBackend.csproj" -c Release -o /app/publish

# Use the ASP.NET image for runtime
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
COPY --from=build /app/publish .

# Run the app
ENTRYPOINT ["dotnet", "PlanogramBackend.dll"]
