# Student Management System

A simple **Student Management System** built with **ASP.NET Core Web API (.NET 8)** and a **C# Console Application** client.

The project demonstrates how a console application can communicate with a RESTful Web API using `HttpClient` and JSON. It supports common student-management operations such as creating, reading, updating, deleting, filtering, and calculating student grades.

## ✨ Features

### Student queries

- Get all students
- Get a student by ID
- Get students by age range
- Get passed students (`Grade >= 50`)
- Get failed students (`Grade < 50`)
- Get students with a grade greater than or equal to a specified grade
- Get students with a grade below a specified grade
- Get students within a grade range
- Calculate the average grade of all students

### Student management

- Create a new student
- Update an existing student
- Delete a student by ID
- Automatically generate a new student ID from the current maximum ID

### Console client

The console application provides an interactive menu for working with the API and displays student data in a formatted table.

## 🏗️ Architecture

The project is divided into two applications:

```text
Student Management System
│
├── WebApplicationAPI
│   └── ASP.NET Core Web API
│       ├── Controllers
│       │   └── StudentsController.cs
│       ├── DataSimulation
│       │   └── clsDataSimulation.cs
│       ├── model
│       │   └── Students.cs
│       └── Program.cs
│
└── ConsoleAppClient
    └── C# Console Application
        ├── Students
        │   ├── Students.cs
        │   ├── clsStudent_GetPart.cs
        │   ├── clsStudents_PostPart.cs
        │   ├── clsStudents_PutPart.cs
        │   ├── clsStudents_deleting.cs
        │   └── clsStudents_Utility.cs
        ├── clsUtility.cs
        ├── Program.cs
        └── Run.cs
```

The overall flow is:

```text
┌──────────────────────┐
│   Console Application│
│       Client         │
└──────────┬───────────┘
           │ HTTP / JSON
           ▼
┌──────────────────────┐
│   ASP.NET Core API   │
│  StudentsController  │
└──────────┬───────────┘
           │
           ▼
┌──────────────────────┐
│  In-Memory Student   │
│        List          │
└──────────────────────┘
```

## 🛠️ Technologies

- **C#**
- **.NET 8**
- **ASP.NET Core Web API**
- **REST API**
- **HttpClient**
- **System.Text.Json**
- **Swagger / OpenAPI**
- **Visual Studio / .NET CLI**

The API project uses **Swashbuckle.AspNetCore 6.6.2** for Swagger/OpenAPI.

## 📋 Student Model

Each student contains four properties:

| Property | Type | Description |
|---|---|---|
| `Id` | `int` | Unique student identifier |
| `Name` | `string` | Student name |
| `Age` | `int` | Student age |
| `Grad` | `int` | Student grade |

Example JSON:

```json
{
  "id": 10,
  "name": "Ali",
  "age": 22,
  "grad": 85
}
```

## 💾 Data Storage

This project currently uses an **in-memory `List<Students>`** instead of a database.

The initial data is defined in:

```text
WebApplicationAPI/DataSimulation/clsDataSimulation.cs
```

Example initial records include students such as:

```text
ID   Name     Age   Grade
1    Ali      23    54
8    sousen   11    90
2    Mazen    13    90
3    Hoda     21    60
4    jasem    30    48
5    slman    18    78
6    smerah   23    80
7    sawsen   32    60
9    Noor     32    40
```

### Important

Because the data is stored in memory, changes made through POST, PUT, and DELETE operations are **not persistent**. Restarting the API resets the data to the initial list.

## 🚀 Getting Started

### Prerequisites

Install:

1. **.NET 8 SDK**
2. An IDE such as **Visual Studio 2022** or another C# development environment

You can verify the installed .NET version with:

```bash
dotnet --version
```

## ▶️ Run the API

Open a terminal in the API project:

```bash
cd WebApplicationAPI
```

Then run:

```bash
dotnet run
```

The configured development URLs are:

```text
HTTP:  http://localhost:5295
HTTPS: https://localhost:7079
```

Swagger is available at:

```text
https://localhost:7079/swagger
```

The API uses HTTPS, so if the local development certificate is not trusted on your machine, you may need to trust the .NET development certificate:

```bash
dotnet dev-certs https --trust
```

## ▶️ Run the Console Client

After the API is running, open another terminal:

```bash
cd ConsoleAppClient
```

Run the client:

```bash
dotnet run
```

The console application communicates with the API using the HTTPS endpoint:

```text
https://localhost:7079
```

### Recommended order

Start the API first:

```text
WebApplicationAPI
      ↓
https://localhost:7079
      ↓
ConsoleAppClient
```

The console client expects the API to be available before performing API operations.

## 📚 API Endpoints

All endpoints use the following base URL:

```text
https://localhost:7079/api/StudentsController
```

### GET endpoints

| Method | Endpoint | Description |
|---|---|---|
| `GET` | `/GetAllStudents` | Return all students |
| `GET` | `/GetStudentInfoByID?id={id}` | Return a student by ID |
| `GET` | `/GetStudentByID?Id={id}` | Return a student by ID |
| `GET` | `/GetStudentByAgeFromTo?from={from}&to={to}` | Filter students by age range |
| `GET` | `/GetPassStudrnts` | Return students with grade ≥ 50 |
| `GET` | `/GetAllFailuresStudents` | Return students with grade < 50 |
| `GET` | `/GetStudentsHowHaveGraterThanTheGrade?grade={grade}` | Return students with grade ≥ specified grade |
| `GET` | `/GetStudentsHowHaveLessThanTheGrade?grade={grade}` | Return students with grade < specified grade |
| `GET` | `/GetStudentsHowHaveGradeFromTo?FromLessGrade={from}&ToGrateGrade={to}` | Filter students by grade range |
| `GET` | `/GetAvgGradeForAllStudents` | Return the average grade |

### POST endpoint

Create a new student:

```http
POST /api/StudentsController/AddNewStudent
```

Request body:

```json
{
  "id": 10,
  "name": "Ali",
  "age": 22,
  "grad": 85
}
```

A successful creation returns:

```text
201 Created
```

### PUT endpoint

Update an existing student:

```http
PUT /api/StudentsController/UpdateStudent
```

Request body:

```json
{
  "id": 10,
  "name": "Ali Ahmed",
  "age": 23,
  "grad": 90
}
```

A successful update returns:

```text
200 OK
```

### DELETE endpoint

Delete a student by ID:

```http
DELETE /api/StudentsController/DeleteStudentById?id=10
```

A successful deletion returns:

```text
200 OK
```

## 🔎 Example API Requests

### Get all students

```bash
curl -X GET "https://localhost:7079/api/StudentsController/GetAllStudents"
```

### Get a student by ID

```bash
curl -X GET "https://localhost:7079/api/StudentsController/GetStudentInfoByID?id=1"
```

### Get passed students

```bash
curl -X GET "https://localhost:7079/api/StudentsController/GetPassStudrnts"
```

### Get students in a grade range

```bash
curl -X GET "https://localhost:7079/api/StudentsController/GetStudentsHowHaveGradeFromTo?FromLessGrade=60&ToGrateGrade=90"
```

### Create a student

```bash
curl -X POST "https://localhost:7079/api/StudentsController/AddNewStudent" \
  -H "Content-Type: application/json" \
  -d '{
    "id": 10,
    "name": "Ali",
    "age": 22,
    "grad": 85
  }'
```

### Update a student

```bash
curl -X PUT "https://localhost:7079/api/StudentsController/UpdateStudent" \
  -H "Content-Type: application/json" \
  -d '{
    "id": 10,
    "name": "Ali Ahmed",
    "age": 23,
    "grad": 90
  }'
```

### Delete a student

```bash
curl -X DELETE "https://localhost:7079/api/StudentsController/DeleteStudentById?id=10"
```

## 🖥️ Console Menu

The console client provides operations similar to:

```text
STUDENT MANAGEMENT SYSTEM

0. Exit

─────────────── Student Queries ───────────────
1. Get All Students
2. Get Passed Students
3. Get Failed Students
4. Get Students With Grade Greater Than
5. Get Students With Grade Less Than
6. Get Students Within Grade Range
7. Get Average Grade
8. Get All Students Without URL
9. Get Student By ID

─────────── Student Management ────────────────
10. Create New Student
11. Delete Student By ID
12. Update Student By ID
```

The console application validates user input for several operations and asks for confirmation before creating, updating, or deleting students.

## 📁 Important Files

### API

**`Controllers/StudentsController.cs`**

Contains the REST API endpoints for student queries and CRUD operations.

**`model/Students.cs`**

Defines the student model and helper methods used by the API.

**`DataSimulation/clsDataSimulation.cs`**

Contains the initial in-memory student collection.

**`Program.cs`**

Configures controllers, Swagger, HTTPS redirection, and the application's HTTP pipeline.

### Console Client

**`Students/clsStudent_GetPart.cs`**

Contains methods for retrieving students from the API and displaying them.

**`Students/clsStudents_PostPart.cs`**

Contains methods for creating students through the API.

**`Students/clsStudents_PutPart.cs`**

Contains the student update functionality.

**`Students/clsStudents_deleting.cs`**

Contains the student deletion functionality.

**`Students/clsStudents_Utility.cs`**

Contains helper methods such as ID generation and student comparison.

**`Run.cs`**

Contains the interactive console workflow and menu.

## ⚠️ Current Project Notes

This project is primarily a learning/demo project and has a few areas that can be improved before using it in production:

- Student data is stored only in memory; there is no database.
- API URLs are currently hard-coded in the console client.
- The API does not currently use a dedicated service/repository layer.
- There is no authentication or authorization.
- Some older/unused client code references endpoint names that are no longer present in `StudentsController`.
- Input validation can be strengthened, especially around grade and student-ID rules.
- The student `IsValid()` implementation in the current model should be reviewed before relying on it for strict validation.
- `GetStudentInfoByID` and `GetStudentByID` provide overlapping functionality and could potentially be consolidated.

These notes reflect the current source code and are not blockers for using the project as a learning exercise.

## 🔮 Possible Future Improvements

Some useful next steps would be:

- Add Entity Framework Core
- Add SQL Server or another database
- Move API URLs into configuration
- Add a service layer
- Add repository/data-access abstractions
- Add DTOs
- Improve validation with data annotations or FluentValidation
- Add global exception handling
- Add structured logging
- Add authentication and authorization
- Add automated unit/integration tests
- Improve API naming consistency
- Add pagination and sorting
- Add Docker support
- Add CI/CD

## 🤝 Contributing

Contributions and improvements are welcome.

A typical workflow is:

```bash
git checkout -b feature/my-feature
```

Make your changes, test them, and then create a pull request.

## 📄 License

No license is currently specified for this project.

If you plan to publish the project publicly, consider adding an appropriate `LICENSE` file.

---

## 👨‍💻 Project Summary

This project is a practical example of connecting a **C# console application** to an **ASP.NET Core Web API**.

It demonstrates:

- REST API design
- HTTP communication
- JSON serialization/deserialization
- CRUD operations
- Query parameters
- HTTP status codes
- Swagger/OpenAPI
- Async/await
- Basic input validation
- Separation of client and server applications
