# FUNews Management System

**Student Name:** Hoang Tran Manh Dung
**Class:** SE19B
**Assignment:** Assignment 01 - PRN222

## Overview
FUNews Management System is an ASP.NET Core MVC web application designed to manage news articles, categories, and system accounts. The project is built following a strict **3-Tier Architecture** (DAO -> Repository -> Service) to ensure separation of concerns and maintainability.

## Technology Stack
- **Framework:** ASP.NET Core 6.0/8.0 MVC
- **ORM:** Entity Framework Core (Database First)
- **Database:** Microsoft SQL Server (`FUNewsManagement`)
- **Frontend:** Bootstrap 5, jQuery, AJAX
- **Authentication:** ASP.NET Core Session

## Architecture
The project is divided into the following layers:
1. **FUNewsManagement.DAL (Data Access Layer):**
   - Contains EF Core generated Models.
   - Contains DAOs (Data Access Objects) implemented using the **Singleton Design Pattern**.
   - Handles all direct database queries.

2. **FUNewsManagement.BLL (Business Logic Layer):**
   - Contains Repositories and Services (Interfaces & Implementations).
   - Serves as the bridge between the MVC Controller and the DAO.

3. **HoangTranManhDungMVC (Presentation Layer):**
   - ASP.NET Core MVC application.
   - Contains Controllers, Views, and configures Dependency Injection in `Program.cs`.

## Features by Role
The application uses session-based authorization to divide features across 4 types of users:

### 1. Admin (Role = 0)
- **Account Management:** Full CRUD operations and Search functionality for `SystemAccount`. 
- **Report Statistic:** View a report of news articles filtered by `StartDate` and `EndDate`, sorted descending by date.
- *Note: Create and Edit actions use Bootstrap Popups (AJAX).*

### 2. Staff (Role = 1)
- **Category Management:** Full CRUD operations and Search functionality for `Category`. A category cannot be deleted if it is already attached to a news article.
- **News Article Management:** Full CRUD operations and Search functionality for `NewsArticle`. Staff can assign multiple Tags to an article.
- **Profile Management:** Update personal information.
- **News History:** View the list of news articles created by themselves.
- *Note: Create and Edit actions use Bootstrap Popups (AJAX).*

### 3. Lecturer (Role = 2)
- Can log in and view the list of all **Active** news articles.

### 4. Public User (Guest)
- No login required. Can view the list of all **Active** news articles directly from the Home page.

## How to Run
1. Update the `appsettings.json` in the MVC project with your local SQL Server connection string.
2. Build the solution to restore all NuGet packages.
3. Run the MVC project. The default route will direct to the Login window (`Auth/Login`) as per the assignment requirements.
4. Log in to access Role-specific features, or navigate to the Public News page from the navigation menu to view articles as a guest.
   - Admin account is configured in `appsettings.json`.
   - Other accounts (Staff, Lecturer) are located in the `SystemAccount` table of the database.
