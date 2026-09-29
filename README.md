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
- [x] Basic authentication

## What's Planned

- [ ] Fill this in with what's left, e.g.:
- [ ] - [ ] Visual design and styling (CSS, layout, overall UI polish)

## Why I Built This

I wanted a project that reflects the kind of records-management work I did during my OJT, while giving me hands-on practice with a modern, industry-standard web stack (ASP.NET Core, EF Core, MVC) instead of just Windows-only desktop apps.
