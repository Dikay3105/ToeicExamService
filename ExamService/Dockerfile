# This stage is used when running from VS in fast mode (Default for Debug configuration)
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 5000
EXPOSE 5001

# This stage is used to build the service project
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["ExamService/ExamService.csproj", "ExamService/"]
RUN dotnet restore "./ExamService/ExamService.csproj"
COPY . . 
WORKDIR "/src/ExamService"

# Tạo thư mục Data trước khi build để tránh lỗi không tìm thấy đường dẫn
RUN mkdir -p /app/build/ExamService/Data

RUN dotnet build "./ExamService.csproj" -c $BUILD_CONFIGURATION -o /app/build

# This stage is used to publish the service project to be copied to the final stage
FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./ExamService.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

# This stage is used in production or when running from VS in regular mode (Default when not using the Debug configuration)
FROM base AS final
WORKDIR /app

# Sao chép thư mục chứa SQL scripts vào thư mục trong container
COPY ./ExamService/ExamService/Data /app/Data

COPY --from=publish /app/publish .

ENTRYPOINT ["dotnet", "ExamService.dll"]
