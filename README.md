# User Management API

A simple RESTful API built with **ASP.NET Core 8** for managing users. It supports CRUD operations, input validation, dependency injection, Swagger documentation, and custom middleware.

## Features

- **CRUD Operations** – Create, retrieve, update, and delete users.
- **Input Validation** – Validates names, email addresses, and ages.
- **Duplicate Detection** – Prevents duplicate email addresses.
- **Dependency Injection** – Service-based architecture for user management.
- **Swagger/OpenAPI** – Interactive API documentation and testing.
- **Custom Middleware** – Centralized exception handling and request logging.
- **In-Memory Storage** – Lightweight storage with sample users initialized at startup.

## Tech Stack

- .NET 8
- ASP.NET Core Web API
- C#
- Swagger / OpenAPI

## Getting Started

### Prerequisites

- [.NET 8 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)
- Git

### Installation

Clone the repository:

```bash
git clone https://github.com/YOUR-USERNAME/UserManagementAPI.git
cd UserManagementAPI
````

Restore dependencies:

```
dotnet restore
```

Run the application:

```
dotnet run
```

Open Swagger UI:

[http://localhost:5100/swagger](http://localhost:5100/swagger?utm_source=chatgpt.com)

## **API Endpoints**

Base URL: `/api/users`

| Method | Endpoint          | Description             |
| ------ | ----------------- | ----------------------- |
| GET    | `/api/users`      | Retrieve all users      |
| GET    | `/api/users/{id}` | Retrieve a user by ID   |
| POST   | `/api/users`      | Create a new user       |
| PUT    | `/api/users/{id}` | Update an existing user |
| DELETE | `/api/users/{id}` | Delete a user           |

### **Example Request**

**POST** `/api/users`

```
{
  "name": "Sam Taylor",
  "email": "sam.taylor@example.com",
  "age": 25
}
```

## **Project Structure**

```
UserManagementAPI/
├── Controllers/
│   └── UsersController.cs
├── Middleware/
│   ├── ExceptionHandlingMiddleware.cs
│   └── RequestLoggingMiddleware.cs
├── Models/
│   └── User.cs
├── Properties/
├── Services/
│   ├── IUserService.cs
│   └── InMemoryUserService.cs
├── Program.cs
├── appsettings.json
└── UserManagementAPI.csproj
```