# 🏥 VitalScope API

Современный веб-API для медицинского мониторинга и анализа данных пациентов, построенный на .NET 8 и PostgreSQL.

## 📋 Описание

VitalScope - это система для сбора, хранения и анализа медицинских данных пациентов. API предоставляет RESTful интерфейс для работы с информацией о пациентах, исследованиях и метаданных.

## 🚀 Быстрый старт с Docker

### Предварительные требования

- [Docker](https://www.docker.com/get-started) (версия 20.10+)
- [Docker Compose](https://docs.docker.com/compose/install/) (опционально)

### 🐳 Запуск приложения

#### Вариант 1: Прямой запуск через Docker

```bash
# Клонируйте репозиторий
git clone <your-repository-url>
cd devops/dotnet-backend-service

# Соберите Docker образ
docker build -t vitalscope-api .

# Запустите контейнер
docker run -d \
  --name vitalscope-api \
  -p 8080:8080 \
  -e ASPNETCORE_ENVIRONMENT=Production \
  vitalscope-api
```

#### Вариант 2: Запуск с переменными окружения

```bash
# Запуск с настройкой подключения к базе данных
docker run -d \
  --name vitalscope-api \
  -p 8080:8080 \
  -e ASPNETCORE_ENVIRONMENT=Production \
  -e ConnectionStrings__ConnectionVitalScope="Host=your-db-host;Port=5432;Database=vital_scope;Username=your-user;Password=your-password" \
  vitalscope-api
```

#### Вариант 3: Использование Docker Compose

Создайте файл `docker-compose.yml`:

```yaml
version: '3.8'

services:
  vitalscope-api:
    build: .
    ports:
      - "8080:8080"
    environment:
      - ASPNETCORE_ENVIRONMENT=Production
      - ConnectionStrings__ConnectionVitalScope=Host=postgres;Port=5432;Database=vital_scope;Username=postgres;Password=postgres
    depends_on:
      - postgres
    networks:
      - vitalscope-network

  postgres:
    image: postgres:15
    environment:
      - POSTGRES_DB=vital_scope
      - POSTGRES_USER=postgres
      - POSTGRES_PASSWORD=postgres
    ports:
      - "5432:5432"
    volumes:
      - postgres_data:/var/lib/postgresql/data
    networks:
      - vitalscope-network

volumes:
  postgres_data:

networks:
  vitalscope-network:
    driver: bridge
```

Запустите:

```bash
docker-compose up -d
```

## 🔧 Настройка

### Переменные окружения

| Переменная | Описание | По умолчанию |
|------------|----------|--------------|
| `ASPNETCORE_ENVIRONMENT` | Окружение приложения | `Production` |
| `ASPNETCORE_URLS` | URL для привязки | `http://+:8080` |
| `ConnectionStrings__ConnectionVitalScope` | Строка подключения к PostgreSQL | См. appsettings.json |

### Конфигурация базы данных

Приложение использует PostgreSQL для хранения данных. Убедитесь, что база данных доступна и содержит необходимые таблицы (миграции выполняются автоматически при запуске).

## 📚 API Документация

После запуска приложения, Swagger UI доступен по адресу:
- **Swagger UI**: http://localhost:8080/swagger
- **OpenAPI JSON**: http://localhost:8080/swagger/v1/swagger.json

### Основные эндпоинты

| Метод | Путь | Описание |
|-------|------|----------|
| `GET` | `/add-info` | Добавление информации |
| `GET` | `/id` | Получение данных по ID |
| `GET` | `/all` | Получение всех метаданных |

## 🏗️ Архитектура

Проект следует принципам Clean Architecture:

```
📁 VitalScope.Api/          # Веб API слой
📁 VitalScope.Logic/        # Бизнес логика
📁 VitalScope.Infrastructure/ # Доступ к данным
📁 VitalScope.Common/       # Общие компоненты
```

### Технологический стек

- **.NET 8.0** - Основной фреймворк
- **ASP.NET Core** - Веб API
- **Entity Framework Core** - ORM
- **PostgreSQL** - База данных
- **NLog** - Логирование
- **Swagger/OpenAPI** - Документация API

## 🔍 Мониторинг и логирование

Приложение использует NLog для логирования. Логи записываются в консоль и могут быть настроены через `nlog.config`.

### Health Checks

Доступны health checks по адресу:
- http://localhost:8080/health

## 🛠️ Разработка

### Локальная разработка

```bash
# Установите .NET 8.0 SDK
# Восстановите зависимости
dotnet restore

# Запустите миграции
dotnet ef database update --project src/VitalScope.Infrastructure --startup-project src/VitalScope.Api

# Запустите приложение
dotnet run --project src/VitalScope.Api
```

### Структура проекта

```
dotnet-backend-service/
├── Dockerfile                 # Docker конфигурация
├── README.md                 # Документация
└── src/
    ├── VitalScope.Api/       # API слой
    ├── VitalScope.Logic/     # Бизнес логика
    ├── VitalScope.Infrastructure/ # Инфраструктура
    └── VitalScope.Common/    # Общие компоненты
```

## 🐛 Устранение неполадок

### Частые проблемы

1. **Ошибка подключения к базе данных**
   - Проверьте доступность PostgreSQL сервера
   - Убедитесь в правильности строки подключения

2. **Порт уже используется**
   ```bash
   # Измените порт при запуске
   docker run -p 8081:8080 vitalscope-api
   ```

3. **Ошибки миграции**
   - Проверьте права доступа к базе данных
   - Убедитесь, что база данных существует

### Просмотр логов

```bash
# Просмотр логов контейнера
docker logs vitalscope-api

# Следить за логами в реальном времени
docker logs -f vitalscope-api
```



