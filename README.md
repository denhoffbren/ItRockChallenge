# ItRock Challenge

API REST desarrollada en .NET 8 para la gestión de tareas con autenticación JWT, arquitectura limpia y contenedores Docker.

## Tecnologías utilizadas

- .NET 8
- ASP.NET Core Web API
- Entity Framework Core
- SQL Server
- JWT Authentication
- Swagger
- Docker
- FluentAssertions

---

# Arquitectura

El proyecto sigue principios de Clean Architecture separando responsabilidades en distintas capas:

```text
TaskService
│
├── TaskService.Api
├── TaskService.Application
├── TaskService.Domain
├── TaskService.Infrastructure
└── TaskService.Tests
```

## Capas

### API
Contiene:
- Controllers
- Middlewares
- Configuración JWT
- Swagger
- Dependency Injection

### Application
Contiene:
- Casos de uso
- DTOs
- Interfaces
- Validaciones

### Domain
Contiene:
- Entidades
- Reglas de negocio

### Infrastructure
Contiene:
- Entity Framework
- Persistencia
- Repositories
- Seguridad JWT

---

# Funcionalidades

- Registro/Login de usuarios
- Autenticación JWT
- CRUD de tareas
- Eliminación lógica
- Paginación
- Importación automática de tareas
- Swagger Documentation
- Docker Compose

---

# Requisitos

- Docker Desktop
---

# Ejecutar con Docker

## 1. Clonar repositorio

```bash
git clone https://github.com/denhoffbren/ItRockChallenge.git
```

## 2. Entrar al proyecto

```bash
cd ItRockChallenge
```

## 3. Levantar contenedores

```bash
docker compose up --build
```

---

# Swagger

Una vez levantado el proyecto:

```text
http://localhost:8080/swagger
```

---
# Endpoints principales

## Auth

| Método | Endpoint |
|---|---|
| POST | /api/auth |
| POST | /api/auth/login |

## Tasks

| Método | Endpoint |
|---|---|
| GET | /api/tasks |
| GET | /api/tasks?pageNumber={pageNumer}&pageSize={pageSize} |
| POST | /api/tasks |
| PATCH | /api/tasks/{id} |
| DELETE | /api/tasks/{id} |
| POST | /api/tasks/import |


# Decisiones técnicas

- Clean Architecture para separación de responsabilidades.
- JWT para autenticación stateless.
- DTOs para desacoplar entidades.
- Eliminación lógica para preservar la integridad y trazabilidad de los datos.
- Uso de interfaces e inyección de dependencias para desacoplamiento.
- Docker para simplificar la ejecución del proyecto.

---

# Autor

Brenda Denhoff
