[English](README.md) | [Русский](README.ru.md)

# Contact Manager SPA

![.NET 8](https://img.shields.io/badge/.NET_8.0-512BD4?style=for-the-badge&logo=dotnet&logoColor=white)
![React 19](https://img.shields.io/badge/React_19-20232A?style=for-the-badge&logo=react&logoColor=61DAFB)
![PostgreSQL](https://img.shields.io/badge/PostgreSQL_15-316192?style=for-the-badge&logo=postgresql&logoColor=white)
![Docker](https://img.shields.io/badge/Docker-2496ED?style=for-the-badge&logo=docker&logoColor=white)
![Tailwind CSS](https://img.shields.io/badge/Tailwind_CSS-38B2AC?style=for-the-badge&logo=tailwind-css&logoColor=white)

> **A cross-platform Single Page Application for contact management.**  
> This project serves as a practical demonstration of Clean Architecture principles, RESTful API design, and modern UI integration within a containerized environment.

---

## Visual Preview

<img width="2559" height="1353" alt="изображение" src="https://github.com/user-attachments/assets/fb778324-ebaf-4e1e-bf78-b60aa5c9de87" />
 
<img width="655" height="661" alt="изображение" src="https://github.com/user-attachments/assets/207f89e7-ab2c-4930-9cf9-1da5c202b03a" />


---

## Technical Highlights

This repository highlights the following software engineering competencies:

*   **Clean Architecture**: Strict separation of concerns across Domain, Application, Infrastructure, and Presentation (.NET 8 Web API) layers.
*   **Robust Validation**: Server-side business rules enforced via `FluentValidation`, with mirrored client-side validation to minimize unnecessary network payload.
*   **Centralized Error Handling**: Custom `GlobalExceptionHandler` middleware for standardized HTTP responses.
*   **Modern Frontend**: Component-based React 19 UI with responsive Tailwind CSS styling and efficient state management using React Hooks.
*   **DevOps & Infrastructure**: Multi-stage Docker builds to compile the API and React app in isolated containers, orchestrated alongside a PostgreSQL database via Docker Compose with proper healthchecks.

---

## Tech Stack

| Layer | Technologies |
| --- | --- |
| **Backend** | C#, ASP.NET Core Web API, EF Core, PostgreSQL, FluentValidation |
| **Frontend** | React 19, Vite, Tailwind CSS, JavaScript (ES6+) |
| **Infrastructure** | Docker, Docker Compose |

---

## API Architecture

The backend provides a RESTful interface. Swagger UI is available in the `Development` environment.

| Method | Endpoint | Description |
| --- | --- | --- |
| `GET` | `/api/contacts` | Retrieve a paginated list of contacts |
| `GET` | `/api/contacts/{id}` | Retrieve a specific contact by ID |
| `POST` | `/api/contacts` | Create a new contact |
| `PUT` | `/api/contacts/{id}` | Update an existing contact |
| `DELETE` | `/api/contacts/{id}` | Delete a contact |

<details>
<summary>Example: Validation Error Response (400 Bad Request)</summary>

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

## Getting Started

[Docker Desktop](https://www.docker.com/products/docker-desktop/) is required to run the application locally.

1. Clone the repository:
   ```bash
   git clone https://github.com/YourUsername/romansevryuk375-contactmanager-spa.git
   cd romansevryuk375-contactmanager-spa
   ```

2. Build and spin up the containers:
   ```bash
   docker compose up --build
   ```

3. Access the application:
   * **Web UI**: `http://localhost:5000`
   * **Swagger API Docs** (local run without Docker): `http://localhost:5109/swagger`

> Note: The PostgreSQL database is automatically initialized and migrated upon container startup.
