# CRM Project - Тестовое задание

## Описание
ASP.NET Web API приложение с React SPA для управления контактами.
Проект построен по  Clean Architecture и контейнеризован с помощью Docker.

В качестве базы данных используется PostgreSQL.

## Стек технологий
- **Backend:** .NET 8, ASP.NET Core Web API, Entity Framework Core, FluentValidation.
- **Frontend:** React 19, Vite, Tailwind CSS.
- **Database:** PostgreSQL 15.
- **Infrastructure:** Docker Compose.

## Инструкция по запуску 

**Должен быть запущен Docker Desktop.**

1. Откройте терминал в корневой папке проекта.
2. Выполните команду:
   ```bash
   docker compose up --build
   ```