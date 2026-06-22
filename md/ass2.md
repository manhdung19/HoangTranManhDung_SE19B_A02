# PRN222 Assignment 02: Building a News Management System
**Platform:** ASP.NET Core Razor Pages with SignalR

---

## 1. Introduction
A News Management System (NMS) is a software application that helps universities and educational institutions efficiently manage, organize, and publish news and content to their website and other channels. The NMS typically includes features such as content creation, approval workflow, scheduling, publishing, and analytics. This can help universities to arrange appropriately their news operations, improve communication with students and the wider community, and better engage with their target audience.

Imagine you're a developer of a News Management System named **FUNewsManagementSystem**. To implement a part of this system your tasks include: 
- Manage account information. 
- Manage news article. 

> **Note:** The application has a default account (admin account) whose email is `admin@FUNewsManagementSystem.org` and password is `@@abc123@@` that stored in the `appsettings.json`.

---

## 2. Assignment Objectives
In this assignment, you will:
- Use Visual Studio .NET to create **ASP.NET Core Razor Pages with SignalR** and Class Library (.dll) projects.
- Perform CRUD actions using Entity Framework Core.
- Use LINQ to query and sort data.
- Apply 3-Layers architecture to develop the website.
- Apply Repository pattern and Singleton pattern in a project.
- Add CRUD and searching actions to the website.
- Apply validation data type for all fields.

---

## 3. Database Design

*(Use the same FUNewsManagement Database from Assignment 01)*

### Relationship & Rules:
- A news article will belong to only **one** news category (`Category`). 
- An account with staff’s role can create **many** news articles in this system. *(Staff role = 1, Lecturer role = 2; Admin role will get from `appsettings.json` file)*.
- A news article will have **many** tags and one tag will belong to **zero or one** news article.
- **Status definition:**
  - News status = active(1) / inactive(0).
  - Category status = active(1) / inactive(0).

---

## 4. Main Functions

### Public Access
- Do **not** need authentication to view the news articles (news status must be active) in this system.

### Authentication
- Member (Admin/Staff) authentication by Email and Password. 

### Role Authorization
- **Lecturer:** Allowed to view the news article (news status must be active) in this system.
- **Admin:** Allowed to:
  - Manage account information.
  - Create a report statistic by the period from `StartDate` to `EndDate` (it depends to the news’ created date), and sort data in descending order.
- **Staff:** Allowed to:
  - Manage category information. *(The delete action will delete an item in the case this item is not belong to any news articles. If the item is already stored in a news article cannot delete.)*
  - Manage news article (includes tags). **Manage (CRUD) news must be applied with real-time communication (SignalR).**
  - Manage his/her profile.
  - View news history created by him/her.

### Real-time updates with SignalR
- Lecturer and Admin users receive real-time notifications when:
  - A news article status changes (active/inactive).
  - A new news article is created.
- *This notification should be shown in the browser as a toast or banner message, allowing users to click and navigate to the affected news article.*

### UI/UX Requirements
- News article Management, Account Management, and Category Management: Includes Read, Create, Update, Delete and Search actions.
- Creating and Updating actions **must be performed by popup dialog**.
- Delete action **always combines with confirmation**.

---

## 5. Note
- **Development Tools:** You must use Visual Studio 2019 or above (.NET5/.NET6/.NET7/.NET8), MSSQL Server 2012 or above.
- **Framework:** You must use **ASP.NET Core Razor Pages with SignalR**.
- **Architecture Restriction:** You are **not allowed to connect directly to the database from Razor Pages**. Every database connection must be used through Repository and Data Access Objects (DAO). 
- **Configuration:** The database connection string must get from `appsettings.json` file.
- **Naming Conventions:**
  - Create Solution in Visual Studio named: `StudentName_ClassCode_A02.sln`.
  - Inside your Solution, the Project Razor Pages must be named: `StudentNameRazorPages`.
  - Create your MS SQL database named `FUNewsManagement` by running code in script `FUNewsManagement.sql`.
- **Startup:** Set the default user interface for your project as **Login page/window**.
