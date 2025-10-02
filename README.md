# backend services


## ИНСТРУКЦИЯ К ЗАПУСКУ

### Предварительные требования
- Установленный Docker

Работа Бэкенд части приложения реализована в контейнере.
Для его запуска необходимо установить Docker.
Далее собрать обзаз из Dockeefile.
-    docker build -t rhammatov/vital_scope:latest .
После сборки образа запустить из него контейнер, пробросив из контейнера порты 8080 и 8081
-   docker run -d -ti --name dotnet-backend-service -p 8080:8080 rhammatov/vital_scope:latest

Для работы серверной части приложение также необходими перед сборкой указать в файле   
- src/VitalScope.Api/appsettings.json в строчке "ConnectionVitalScope": "Host=localhost;Port=5432;Database=vital_scope;Username="";Password="""

параметры базы данных

### CI/CD

ci/cd часть в репозитории реализованна с помощью Github action
для деплоя приложения создан action и worklfow file
для его запуска необходимо:
    - перейти во вкладку ACTION
    - выбрать Deploy Application
    - запустить action с кнпки Run workflow, выбрав нужную ветку
В файле dotnet-backend-service/.github/workflows/main.yml прописан скрипт для деплоя, в нем указан сервер для запуска, а также имя пользователя и ssh ключ для деплоя приложения на сервер:
            host: 176.108.245.175
            username: user1
            key: ${{ secrets.SSHKEY2 }}
