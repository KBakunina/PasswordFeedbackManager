FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src

COPY ["PasswordFeedbackManager/PasswordFeedbackManager.csproj", "PasswordFeedbackManager/"]
COPY ["Tests/Tests.csproj", "Tests/"]
RUN dotnet restore "PasswordFeedbackManager/PasswordFeedbackManager.csproj"
RUN dotnet restore "Tests/Tests.csproj"

COPY . .
WORKDIR "/src/PasswordFeedbackManager"
RUN dotnet build "PasswordFeedbackManager.csproj" -c $BUILD_CONFIGURATION -o /app/build

WORKDIR "/src/Tests"
RUN dotnet test "Tests.csproj" --no-restore --verbosity normal -c $BUILD_CONFIGURATION --logger "trx;LogFileName=testresults.trx"

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
WORKDIR "/src/PasswordFeedbackManager"
RUN dotnet publish "PasswordFeedbackManager.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "PasswordFeedbackManager.dll"]