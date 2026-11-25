# Student Management System

**Status: Work in Progress**

A web-based student management system built with ASP.NET Core MVC, created as a portfolio project to practice full-stack .NET development and to mirror the records-management work I did during my government on-the-job training.

## Tech Stack

| Part | Technology |
|---|---|
| Framework | ASP.NET Core |
| Architecture | MVC (Model–View–Controller) |
| Language | C# |
| Frontend | Razor Views, HTML, CSS, JavaScript |
| Database | SQL Server |
| ORM | Entity Framework Core |
| IDE | Visual Studio 2022 |

## Features

- Manage student records (add, view, update, delete)
- Manage courses and departments
- Relational data linking students, courses, and departments through Entity Framework Core

## How to Run

**You need:** the [.NET 9 SDK](https://dotnet.microsoft.com/download) and one of these (both free):
- **SQL Server Express**, or
- **SQL Server LocalDB**, which is already installed if you have Visual Studio with the "ASP.NET and web development" workload.

1. Download or clone this repository.
2. Open `StudentManagementSystem/appsettings.json` and set the `Server=` part of `DefaultConnection` to match your SQL Server:

   | You have | Use |
   |---|---|
   | SQL Server Express | `Server=.\\SQLEXPRESS;` (already set) |
   | LocalDB (Visual Studio) | `Server=(localdb)\\MSSQLLocalDB;` |

3. Run the app, from Visual Studio (press **F5**) or from a terminal in the `StudentManagementSystem` folder:
   ```
   dotnet run --launch-profile https
   ```
4. Open `https://localhost:7294`.
5. **First run only:** the app asks you to **create the first admin account**. Choose any username and a password (at least 8 characters), and you're logged in. This page closes itself once an admin exists. Your account is only on your own computer.

On the first run, the app also **creates the `StudentManagementDB` database and its tables by itself**. If the database already exists with tables, nothing is changed.

**Tip:** start by adding a few departments and courses, then add students.

## Project Structure

```
StudentManagementSystem
│
├── Controllers
│   ├── HomeController.cs
│   ├── StudentsController.cs
│   ├── CoursesController.cs
│   └── DepartmentsController.cs
│
├── Models
│   ├── Student.cs
│   ├── Course.cs
│   └── Department.cs
│
├── Views
│   ├── Home
│   ├── Students
│   ├── Courses
│   └── Departments
│
├── wwwroot
│   ├── css
│   └── js
│
└── appsettings.json
```

## What's Done

- [ ] Fill this in as you complete each part, e.g.:
- [x] Project structure and database setup
- [x] Student CRUD operations
- [x] Course and Department CRUD operations
- [x] Input validation and error handling
- [x] Search and filtering
- [ ] Basic authentication

## What's Planned

- [ ] Fill this in with what's left, e.g.:
- [ ] - [ ] Visual design and styling (CSS, layout, overall UI polish)

## Why I Built This

I wanted a project that reflects the kind of records-management work I did during my OJT, while giving me hands-on practice with a modern, industry-standard web stack (ASP.NET Core, EF Core, MVC) instead of just Windows-only desktop apps.
