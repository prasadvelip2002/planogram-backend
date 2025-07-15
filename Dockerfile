# Use the official .NET 8 SDK image
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY . .

# Update the path below if your .csproj is inside a subfolder
RUN dotnet restore "PlanogramBackend/PlanogramBackend.csproj"
RUN dotnet publish "PlanogramBackend/PlanogramBackend.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "PlanogramBackend.dll"]
