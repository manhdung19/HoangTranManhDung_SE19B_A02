# PRN222 Assignment 01
## [cite_start]Building a News Management System with ASP.NET Core Web App (Model View Controller) [cite: 1, 2]

---

## [cite_start]1. Introduction [cite: 3]
[cite_start]A News Management System (NMS) is a software application that helps universities and educational institutions to efficiently manage, organize, and publish news and content to their website and other channels[cite: 4]. [cite_start]The NMS typically includes features such as content creation, approval workflow, scheduling, publishing, and analytics[cite: 5]. [cite_start]This can help universities to arrange appropriately their news operations, improve communication with students and the wider community, and better engage with their target audience[cite: 6].

[cite_start]Imagine you're a developer of a News Management System named **FUNewsManagementSystem**[cite: 7]. [cite_start]To implement a part of this system your tasks include[cite: 8]:
* [cite_start]Manage account information[cite: 9].
* [cite_start]Manage news article[cite: 10].

> [cite_start]**Note:** The application has a default account (admin account) whose email is `admin@FUNewsManagementSystem.org` and password is `@@abc123@@` that stored in the `appsettings.json`[cite: 11].

---

## [cite_start]2. Assignment Objectives [cite: 12]
[cite_start]In this assignment, you will[cite: 13]:
* [cite_start]Use the Visual Studio.NET to create ASP.NET Core MVC and Class Library (.dll) projects[cite: 14].
* [cite_start]Perform CRUD actions using Entity Framework Core[cite: 15].
* [cite_start]Use LINQ to query and sort data[cite: 16].
* [cite_start]Apply 3-Layers architecture to develop the website[cite: 17].
* [cite_start]Apply Repository pattern and Singleton pattern in a project[cite: 18].
* [cite_start]Add CRUD and searching actions to the website[cite: 19].
* [cite_start]Apply to validate data type for all fields[cite: 20].
* [cite_start]Run the project and test the Web application actions[cite: 21].

---

## [cite_start]3. Database Design [cite: 22]



### Relationship & Rules:
* [cite_start]A news article will belong to only one news category (Category)[cite: 23].
* [cite_start]An account with staff’s role can create many news articles in this system[cite: 24].
  * [cite_start]**Staff role** = `1` [cite: 25]
  * [cite_start]**Lecturer role** = `2` [cite: 25]
  * [cite_start]**Admin role** will get from `appsettings.json` file[cite: 25].
* [cite_start]A news article will have many tags and one tag will belong to zero or one news article[cite: 26].
* **Status definition:**
  * [cite_start]News status = active(`1`) / inactive(`0`)[cite: 26].
  * [cite_start]Category status = active(`1`) / inactive(`0`)[cite: 27].

---

## [cite_start]4. Main Functions [cite: 28]

### Public Access
* [cite_start]Do not need authentication to view the news article (news status must be active) in this system[cite: 29].

### Authentication
* [cite_start]Member (Admin/Staff/Lecturer) authentication by Email and Password[cite: 30].

### Role Authorization
* [cite_start]**Lecturer:** Allowed to view the news article (news status must be active) in this system[cite: 31].
* [cite_start]**Admin:** Allowed to[cite: 32]:
  * [cite_start]Manage account information[cite: 33].
  * [cite_start]Create a report statistic by the period from `StartDate` to `EndDate` (it depends to the news’ created date), and sort data in descending order[cite: 34].
* [cite_start]**Staff:** Allowed to[cite: 35]:
  * Manage category information. [cite_start]*(The delete action will delete an item in the case this item is not belong to any news articles. If the item is already stored in a news article cannot delete.)* [cite: 36]
  * [cite_start]Manage news article (includes tags)[cite: 37].
  * [cite_start]Manage his/her the profile[cite: 38].
  * [cite_start]View news history created by him/her[cite: 39].

### UI/UX Requirements
* [cite_start]**News article Management, Account Management, and Category Management:** Includes Read, Create, Update, Delete and Search actions[cite: 40].
* [cite_start]Creating and Updating actions **must be performed by popup dialog**[cite: 41].
* [cite_start]Delete action **always combines with confirmation**[cite: 41].

---

## [cite_start]5. Note [cite: 42]
* [cite_start]**Development Tools:** You must use Visual Studio 2019 or above (.NET5/.NET6/.NET7/.NET8), MSSQL Server 2012 or above[cite: 43].
* [cite_start]**Framework:** You must use **ASP.NET Core Web App (Model-View-Controller)**[cite: 44].
* **Architecture Restriction:** You are **not allowed to connect directly to the database from Controller**. [cite_start]Every database connection must be used through Service, Repository and Data Access Objects (DAO)[cite: 45].
* [cite_start]**Configuration:** The database connection string must get from `appsettings.json` file[cite: 46].
* **Naming Conventions:**
  * [cite_start]Create Solution in Visual Studio named: `StudentName_ClassCode_A01.sln`[cite: 47].
  * [cite_start]Inside your Solution, the Project WPF must be named: `StudentNameMVC`[cite: 47].
  * [cite_start]Create your MS SQL database named `FUNewsManagement` by running code in script `FUNewsManagement.sql`[cite: 48].
* [cite_start]**Startup:** Set the default user interface for your project as **Login** window[cite: 49].