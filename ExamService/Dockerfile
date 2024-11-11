# Stage 1: Build the application
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copy the csproj and restore the dependencies
COPY ["ExamService/ExamService.csproj", "ExamService/"]
RUN dotnet restore "ExamService/ExamService.csproj"

# Build the application and specify the output directory for the build artifacts
RUN dotnet build "ExamService/ExamService.csproj" -c Release -o /app/build/ExamService

# Stage 2: Publish the application
FROM build AS publish
RUN dotnet publish "ExamService/ExamService.csproj" -c Release -o /app/publish

# Stage 3: Create final runtime image
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app

# Copy the published application from the publish stage
COPY --from=publish /app/publish .

# Set the entry point to run the application
ENTRYPOINT ["dotnet", "ExamService.dll"]
