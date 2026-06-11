[English](README.md) | [Русский](README.ru.md)

# Contact Manager SPA

![.NET 8](https://img.shields.io/badge/.NET_8.0-512BD4?style=for-the-badge&logo=dotnet&logoColor=white)
![React 19](https://img.shields.io/badge/React_19-20232A?style=for-the-badge&logo=react&logoColor=61DAFB)
![PostgreSQL](https://img.shields.io/badge/PostgreSQL_15-316192?style=for-the-badge&logo=postgresql&logoColor=white)
![Docker](https://img.shields.io/badge/Docker-2496ED?style=for-the-badge&logo=docker&logoColor=white)
![Tailwind CSS](https://img.shields.io/badge/Tailwind_CSS-38B2AC?style=for-the-badge&logo=tailwind-css&logoColor=white)

> **Кроссплатформенное Single Page Application для управления контактами.**  
> Данный проект служит практической демонстрацией принципов чистой архитектуры (Clean Architecture), проектирования RESTful API и интеграции современного пользовательского интерфейса в контейнеризованной среде.

---

## Визуальная демонстрация

<img width="2559" height="1353" alt="Главный экран приложения" src="https://github.com/user-attachments/assets/fb778324-ebaf-4e1e-bf78-b60aa5c9de87" />
 
<img width="655" height="661" alt="Модальное окно валидации" src="https://github.com/user-attachments/assets/207f89e7-ab2c-4930-9cf9-1da5c202b03a" />

---

## Ключевые технические решения

Данный репозиторий демонстрирует следующие компетенции в области программной инженерии:

*   **Clean Architecture**: Строгое разделение ответственности (Separation of Concerns) между слоями Domain, Application, Infrastructure и Presentation (.NET 8 Web API).
*   **Надежная валидация**: Серверные бизнес-правила реализованы через `FluentValidation` с зеркальным дублированием на клиенте для минимизации лишних сетевых запросов.
*   **Централизованная обработка ошибок**: Кастомный middleware `GlobalExceptionHandler` для стандартизации HTTP-ответов.
*   **Современный Frontend**: Компонентный пользовательский интерфейс на React 19 с адаптивной версткой на Tailwind CSS и эффективным управлением состоянием через React Hooks.
*   **DevOps и Инфраструктура**: Многоэтапная сборка (multi-stage builds) в Docker для компиляции API и React-приложения в изолированных контейнерах, оркестрация которых вместе с базой данных PostgreSQL выполняется через Docker Compose с настройкой проверок работоспособности (healthchecks).

---

## Стек технологий

| Слой | Технологии |
| --- | --- |
| **Backend** | C#, ASP.NET Core Web API, EF Core, PostgreSQL, FluentValidation |
| **Frontend** | React 19, Vite, Tailwind CSS, JavaScript (ES6+) |
| **Инфраструктура** | Docker, Docker Compose |

---

## Архитектура API

Серверная часть предоставляет RESTful интерфейс. Интерактивная документация Swagger UI доступна в окружении `Development`.

| Метод | Эндпоинт | Описание |
| --- | --- | --- |
| `GET` | `/api/contacts` | Получение списка контактов (с поддержкой пагинации) |
| `GET` | `/api/contacts/{id}` | Получение конкретного контакта по ID |
| `POST` | `/api/contacts` | Создание нового контакта |
| `PUT` | `/api/contacts/{id}` | Обновление существующего контакта |
| `DELETE` | `/api/contacts/{id}` | Удаление контакта |

<details>
<summary>Пример ответа: Ошибка валидации (400 Bad Request)</summary>

```json
{
  "statusCode": 400,
  "message": "One or more validation failures occurred.",
  "errors": {
    "MobilePhone": [
      "Invalid phone number format."
    ]
  }
}
```

</details>

---

## Локальное развертывание

Для локального запуска приложения требуется установленный [Docker Desktop](https://www.docker.com/products/docker-desktop/).

1. Клонируйте репозиторий:
   ```bash
   git clone https://github.com/YourUsername/romansevryuk375-contactmanager-spa.git
   cd romansevryuk375-contactmanager-spa
   ```

2. Запустите сборку и старт контейнеров:
   ```bash
   docker compose up --build
   ```

3. Приложение будет доступно по адресам:
   * **Web UI**: `http://localhost:5000`
   * **Swagger API Docs** (при локальном запуске без Docker): `http://localhost:5109/swagger`

> Примечание: База данных PostgreSQL инициализируется и мигрирует автоматически при запуске контейнеров.
