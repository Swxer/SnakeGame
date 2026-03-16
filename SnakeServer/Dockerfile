# Stage 1: Build
FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /src

# Copy project files
COPY ["SnakeServer/SnakeServer.csproj", "SnakeServer/"]
COPY ["SnakeShared/SnakeShared.csproj", "SnakeShared/"]

# Restore dependencies
RUN dotnet restore "SnakeServer/SnakeServer.csproj"

# Copy all source code
COPY SnakeServer/ SnakeServer/
COPY SnakeShared/ SnakeShared/

# Build the application
WORKDIR "/src/SnakeServer"
RUN dotnet build "SnakeServer.csproj" -c Release -o /app/build

# Stage 2: Publish
FROM build AS publish
RUN dotnet publish "SnakeServer.csproj" -c Release -o /app/publish /p:UseAppHost=false

# Stage 3: Runtime
FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS final
WORKDIR /app

# Expose the port
EXPOSE 8080

# Copy published files
COPY --from=publish /app/publish .

# Run the application
ENTRYPOINT ["dotnet", "SnakeServer.dll"]
