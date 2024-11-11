# Stage 1: Build
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copy the project file and restore any dependencies (via dotnet restore)
COPY ["ExamService/ExamService.csproj", "ExamService/"]
RUN dotnet restore "ExamService/ExamService.csproj"

# Copy everything else and build the project
COPY . .
WORKDIR "/src/ExamService"
RUN dotnet build "ExamService.csproj" -c Release -o /app/build

# Stage 2: Publish
FROM build AS publish
RUN dotnet publish "ExamService/ExamService.csproj" -c Release -o /app/publish

# Stage 3: Create final runtime image
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ExamService.dll"]
