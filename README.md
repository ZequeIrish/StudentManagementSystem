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

- [x] Project structure and database setup (the database and tables are created automatically on first run)
- [x] Student records: add, view, edit, delete
- [x] Course and department records: add, view, edit, delete
- [x] Department summary with a student count, and a list of students per department
- [x] Search and sorting of students
- [x] Input validation with clear error messages (names accept accents, apostrophes and hyphens, like Peña or O'Brien)
- [x] Data safety: courses and departments that still have students can't be deleted, and renaming a course updates its students
- [x] Admin login and logout, with first-time admin setup
- [x] Admin management: add and remove admin accounts
- [x] Passwords stored as one-way hashes, never as plain text
- [x] Protection against forged requests (anti-forgery tokens) and against code hidden in names or addresses (XSS)

## What's Planned

- [ ] Visual design and styling (CSS, layout, overall UI polish)
- [ ] Screenshots of the main pages in this README
- [ ] Change password for admins
- [ ] Lock an account for a short time after many wrong passwords
- [ ] Link students to courses by ID (like departments), instead of by course name
- [ ] Demo mode with sample data, for an online demo

## Why I Built This

I wanted a project that reflects the kind of records-management work I did during my OJT, while giving me hands-on practice with a modern, industry-standard web stack (ASP.NET Core, EF Core, MVC) instead of just Windows-only desktop apps.
