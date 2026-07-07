# TodoApp - Full-Stack Task Management 📝

A comprehensive, full-stack Task Management application inspired by Microsoft To-Do. This project demonstrates a complete implementation of a modern web application featuring secure user authentication, categorization, pagination, and real-time search capabilities.

## ✨ Features

* **User Authentication:** Secure registration and login using JWT (JSON Web Tokens).
* **Task Management (CRUD):** Create, read, update, and delete tasks.
* **Categories:** Organize tasks by custom categories.
* **Pagination:** Efficiently load and display large lists of tasks.
* **Search & Filtering:** Instantly filter tasks by category or search by title.
* **Responsive UI:** Clean and modern interface built with Bootstrap, accessible on both desktop and mobile devices.

## 🛠️ Tech Stack

**Backend (`/todo-backend`)**
* **Framework:** .NET Core REST API
* **Architecture:** 4-Tier Architecture (Controllers, Services, Interfaces, Data Access)
* **Database:** MS SQL Server
* **ORM:** Entity Framework Core
* **Security:** JWT Authentication & Authorization

**Frontend (`/todo-frontend`)**
* **Framework:** Angular (Standalone Components, modern `@for`/`@if` control flow)
* **Styling:** Bootstrap 5 & Custom CSS
* **State Management & Networking:** RxJS, Angular HttpClient, HTTP Interceptors
* **Routing:** Angular Router with Route Guards (AuthGuard)

---

## 🚀 Getting Started

Follow these instructions to get a copy of the project up and running on your local machine for development and testing purposes.

### Prerequisites

Make sure you have the following installed on your machine:
* [.NET SDK](https://dotnet.microsoft.com/download) (v8.0 or later)
* [Node.js and npm](https://nodejs.org/) (v18.0 or later)
* [Angular CLI](https://angular.io/cli) (`npm install -g @angular/cli`)
* [MS SQL Server](https://www.microsoft.com/en-us/sql-server/sql-server-downloads) & SQL Server Management Studio (SSMS) or Azure Data Studio

### 1. Backend Setup

1. Navigate to the backend directory:
   ```bash
   cd todo-backend
   ```
2. Update the database connection string:
   Open `appsettings.json` and ensure the `DefaultConnection` points to your local MS SQL Server instance.
3. Apply Entity Framework migrations to create the database:
   ```bash
   dotnet ef database update
   ```
   *(Alternatively, if using Visual Studio, run `Update-Database` in the Package Manager Console).*
4. Run the API:
   ```bash
   dotnet run
   ```
   The backend will start, typically on `https://localhost:7015`. You can explore the endpoints using the automatically generated Swagger UI.

### 2. Frontend Setup

1. Open a new terminal window and navigate to the frontend directory:
   ```bash
   cd todo-frontend
   ```
2. Install the necessary npm packages:
   ```bash
   npm install
   ```
3. Start the Angular development server:
   ```bash
   ng serve
   ```
4. Open your browser and navigate to `http://localhost:4200`.

---

## 📁 Architecture Overview

The backend strictly follows a **4-tier architecture** to ensure separation of concerns, scalability, and maintainability:
1.  **Controllers Layer:** Handles incoming HTTP requests, validates input, and returns appropriate HTTP responses.
2.  **Services Layer:** Contains all the business logic of the application.
3.  **Interfaces Layer:** Defines contracts (abstractions) for services and repositories to implement Dependency Injection.
4.  **Data Access Layer:** Manages database interactions using the Repository Pattern and Entity Framework Core.
