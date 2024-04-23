# Use the official .NET SDK image as the base image
FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build-env

# Set the working directory inside the container
WORKDIR /app

# Copy the .NET API project file(s) and restore dependencies
COPY *.csproj ./
RUN dotnet restore

# Copy the remaining source code
COPY . ./

# Build the application
RUN dotnet publish -c Release -o out -p 8080:8080 swaggerapi/swagger-ui

# Build the runtime image
FROM mcr.microsoft.com/dotnet/aspnet:6.0

# Set the working directory inside the container
WORKDIR /app

# Copy the published app from the build environment
COPY --from=build-env /app/out .

# Expose port 80 to the outside world
EXPOSE 80
EXPOSE 8080
EXPOSE 443

# Define the entry point for the container
ENTRYPOINT ["dotnet", "CebuFitApi.dll"]
