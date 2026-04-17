FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 8080

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src

# 修正路徑：從 D:\Project 看，這些專案檔的正確位置
COPY ["WorkItem_Backend/WorkItem_Backend/WorkItem_Backend.csproj", "WorkItem_Backend/WorkItem_Backend/"]
COPY ["WorkItem_Backend/BusinessRule/BusinessRule.csproj", "WorkItem_Backend/BusinessRule/"]
COPY ["WorkItem_Backend/Common/Common.csproj", "WorkItem_Backend/Common/"]
COPY ["WorkItem_Backend/DataAccess/DataAccess.csproj", "WorkItem_Backend/DataAccess/"]

# 還原 NuGet
RUN dotnet restore "WorkItem_Backend/WorkItem_Backend/WorkItem_Backend.csproj"

# 複製整個 WorkItem_Backend 目錄進去
# 因為 .dockerignore 可能會過濾，我們直接複製後端主目錄
COPY . .

# 注意：切換到編譯目錄時，路徑也要正確
WORKDIR "/src/WorkItem_Backend/WorkItem_Backend"
RUN dotnet build "WorkItem_Backend.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "WorkItem_Backend.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "WorkItem_Backend.dll"]