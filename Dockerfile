# Используем официальный образ .NET 8.0 SDK для сборки
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Копируем все файлы проекта
COPY src/ ./

# Восстанавливаем зависимости
RUN dotnet restore

# Собираем и публикуем приложение в Release конфигурации
RUN dotnet publish VitalScope.Api/VitalScope.Api.csproj -c Release -o /app/publish

# Используем runtime образ для финального контейнера
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app

# Копируем опубликованное приложение из build этапа
COPY --from=build /app/publish .

# Открываем порт 8080 (стандартный для ASP.NET Core в контейнерах)
EXPOSE 8080

# Устанавливаем переменные окружения
ENV ASPNETCORE_URLS=http://+:8080
ENV ASPNETCORE_ENVIRONMENT=Production

# Команда запуска приложения
ENTRYPOINT ["dotnet", "VitalScope.Api.dll"]
