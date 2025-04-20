# VkPostAnalyzerUdv

![VkPostAnalyzer](https://img.shields.io/badge/ASP.NET_Core-9.0-blue.svg) ![PostgreSQL](https://img.shields.io/badge/PostgreSQL-17-blue.svg) ![Swagger](https://img.shields.io/badge/Swagger-UI-green.svg)

VkPostAnalyzer — это веб-приложение, которое анализирует последние 5 постов пользователя ВКонтакте, подсчитывает частоту букв в текстах постов и сохраняет результаты в базу данных PostgreSQL. Взаимодействие с backend осуществляется через Swagger UI.

## Функциолнал
- Авторизация через VK OAuth 2.0
- Получение 5 последних постов пользователя VK
- Подсчет вхождения каждой буквы (регистронезависимо)
- Сортировка результата по алфавиту перед загрузкой в бд
- Сохранение результатов анализа в базу данных PostgreSQL
- Логирование процесса в локальный файл analysis.log
- Swagger UI для взаимодействия с API

## Технологии
- **Backend:** ASP.NET Core 9.0, Entity Framework Core
- **Database:** PostgreSQL
- **API Documentation:** Swagger
- **VK API:** Официальный VkOAuth 2.0 и метод wall.get для получения данных о стене

## Как запустить проект

### 1. Клонирование репозитория
```bash
git clone https://github.com/Ruzlex/VkPostAnalyzerUdv.git
cd VkPostAnalyzerUdv
```

### 2. Конфигурация проекта
В файле appsettings.json в директории API добавьте параметры БД для локального использования. VK API уже настроены по умолчанию для локального тестирования

```json
{
  "ConnectionStrings": {
    "VkPostAnalyzerDb": "Host=localhost;Port=5432;Database=VkPostAnalyzerUdv;Username=your_user;Password=your_password"
  }
}
```

### 3. Разворачивание базы данных PostgreSQL
Запустите PostgreSQL и создайте базу данных `VkPostAnalyzerUdv`.

### 4. Запуск приложения
Для локального запуска запускайте через ваш IDEA

### 5. Открытие Swagger UI
Swagger UI доступен по адресу:
```
https://localhost/swagger/index.html
```

### Авторизация
- `GET /api/auth/url` — Получение OAuth URL для VK
- `GET /api/auth/response` — Обработка ответа VK

  **При переходе по адресу из /api/auth/url в адресной строке заменится адрес с информацией для GET /api/auth/response**

### Анализ постов
- `POST /api/posts/analyze` — Анализ 5 последних постов
  ```json
  {
    "accessToken": "your_access_token",
    "ownerId": 12345678
  }
  ```
  По умолчанию ownerId равен 0, при таком значении посты берутся с текущего авторизированого в вк локального пользователя

  ##  Логирование
Логи записываются в `API/logs/analysis.log` и содержат информацию о запуске и завершении анализа постов.
