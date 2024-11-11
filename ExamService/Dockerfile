# Stage 1: Build the application
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build

# Set environment variable for build configuration
ARG BUILD_CONFIGURATION=Release

# Set working directory
WORKDIR /src

# Copy the ExamService project files and restore dependencies
COPY ./ExamService/ExamService.csproj ./ExamService/
RUN dotnet restore "./ExamService/ExamService.csproj"

# Copy the rest of the ExamService source code
COPY ./ExamService ./ExamService

# Change to the ExamService directory
WORKDIR /src/ExamService

# Build the application and specify the output directory for the build artifacts
RUN dotnet build "./ExamService.csproj" -c $BUILD_CONFIGURATION -o /app/build/ExamService

# Stage 2: Publish the application for final deployment
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime

# Set working directory in runtime image
WORKDIR /app

# Copy the build artifacts from the build stage
COPY --from=build /app/build/ExamService /app/ExamService

# Expose any necessary ports (adjust as per your configuration)
EXPOSE 5000

# Define the entry point for the container
ENTRYPOINT ["dotnet", "ExamService/ExamService.dll"]
