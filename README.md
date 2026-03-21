# StreetGames – Information System Analysis, Design & Implementation

## Overview

**StreetGames** is a university Information Systems Analysis and Design project built around a real business-oriented idea: contacting a business domain, understanding its operational needs, and designing an information system from scratch using the knowledge we gained in the course.

Instead of starting from code, we started from the business itself. The purpose of the project was to take a real-world operational environment, analyze its workflows, identify its information needs, model its processes, and then turn that analysis into a full system design and working implementation.

From that point, the project developed as a full end-to-end information system effort:
- understanding the business and its day-to-day operational challenges
- translating needs into requirements and system logic
- modeling the system with UML diagrams
- designing the database structure
- implementing the application in C#
- connecting the interface and business workflows to SQL Server

The result is **StreetGames**, a business management system for a street games and event-operations environment, built as a combination of:
- **UML-based information system design**
- **SQL Server relational database development**
- **C# WinForms desktop application implementation**

This repository presents the project not only as software, but as a full **analysis, design, and implementation story**.

---

## The Story Behind the Project

This project was created as part of an academic process focused on building a system the way information systems are built in practice: not by jumping directly into code, but by first understanding the organization and the business processes behind it.

We approached the project as a team and worked from the ground up. The goal was to build an information system for a business environment that manages employees, scheduling, inventory, and event-related activity. That meant thinking like analysts and designers before thinking like developers.

We began by asking:
- What does the business actually need?
- Which processes are repetitive, manual, or hard to manage?
- Which users interact with the system?
- What information must be tracked?
- Which actions should managers perform, and which should employees perform?
- What business rules should the system enforce?

Only after that foundation was clear did we move into modeling and implementation.

That process is what makes this project meaningful to us. It was not just about building forms and tables. It was about learning how to transform a business problem into a structured information system through analysis, design, database thinking, and software development.

---

## Project Purpose

The purpose of this project was to design and implement an information system from scratch for a business-oriented operational environment, based on real needs and supported by structured analysis and design methods.

The project aimed to demonstrate how academic knowledge in:
- information systems analysis
- object-oriented design
- database modeling
- SQL programming
- application development

can be combined into one complete working solution.

The system was built to support core operational activities such as:
- employee management
- role-based system access
- weekly shift scheduling
- inventory monitoring
- event-task tracking
- business workflow support through structured logic and data operations

---

## Main Features

### 1. Role-Based Login

The system includes an employee login flow and routes users to different interfaces based on role and permissions.

### 2. Manager Dashboard

Managers can access the main operational modules from a central dashboard, including:
- employee management
- weekly scheduling
- inventory monitoring

### 3. Employee Dashboard

Employees can access the modules relevant to their responsibilities, including:
- personal event tasks
- inventory monitoring

### 4. Employee Management

Managers can manage employee records through a dedicated interface with functionality such as:
- creating employee records
- updating employee details
- soft-deleting or deactivating employees
- filtering by status
- validating required inputs

### 5. Weekly Scheduling

The scheduling module supports planning and maintaining weekly work schedules, including shift-related operations and status handling.

### 6. Inventory Monitoring

The inventory module supports operational monitoring of stock items, including:
- inventory visibility
- quantity updates
- minimum stock threshold checks
- alerts for low stock or expiration risk
- active or inactive item handling

### 7. Event Task Management

Employees can view their event-related tasks and update task status through a dedicated screen.

### 8. Database-Driven Business Logic

The project relies on SQL Server tables, relationships, lookup values, and stored procedures to support structured data operations across the system.

---

## Tech Stack

### Application Layer
- **C#**
- **WinForms**
- **.NET / Visual Studio project structure**

### Database Layer
- **Microsoft SQL Server**
- **T-SQL**
- stored procedures
- relational schema design
- seed and sample data

### Analysis and Design
- **UML**
- use case modeling
- class diagram modeling
- state diagrams
- sequence diagrams

---

## System Modules

### Login Module

Handles employee authentication and routes users to the relevant dashboard based on system role.

![Login](Screenshots/login.png)

### Manager Dashboard

Provides managers with navigation to the main management modules.

![Manager Dashboard](Screenshots/manager-dashboard.png)

### Employee Dashboard

Provides employees with access to operational task-related modules.

![Employee Dashboard](Screenshots/employee-dashboard.png)

### Manage Employees

Supports employee record management, including creation, update, filtering, and status-based handling.

![Manage Employees](Screenshots/manage-employees.png)

### Inventory Monitoring

Displays inventory records, status indicators, alerts, and supports quantity updates.

![Inventory Monitoring](Screenshots/inventory-monitoring.png)

### Event Tasks

Displays employee event tasks and supports status updates.

![Event Tasks](Screenshots/event-tasks.png)

### Weekly Scheduling

Supports weekly planning and scheduling operations.

![Week Scheduling](Screenshots/week-scheduling.png)

---

## Analysis and Design Artifacts

This repository also includes the system design deliverables used during the analysis and design phase.

### Class Diagram

Represents the main entities, relationships, and structural design of the system.

![Class Diagram](Screenshots/class-diagram.jpg)

### Sequence Diagram

Illustrates a core system interaction flow for inventory monitoring and update behavior.

![Sequence Diagram](Screenshots/sequence-diagram.png)

### State Diagrams

The project includes state-based behavioral modeling for key workflows such as shift requests and event management.

#### Shift Swap Request State Diagram

![Shift Swap Request State Diagram](Screenshots/state-diagram-shift-swap.png)

#### Manage Event State Diagram

![Manage Event State Diagram](Screenshots/state-diagram-manage-event.png)

Additional modeling files are available in the `Diagrams/` folder as Visual Paradigm project files.

---

## Database Design

The SQL layer includes:
- relational table creation
- lookup and status tables
- stored procedures for data access and updates
- sample data for testing and demonstration

### SQL Files
- `01_schema_and_procedures.sql` – database schema, relationships, and stored procedures
- `02_seed_data.sql` – sample and initialization data

The schema covers entities such as:
- Employee
- Customer
- Event
- EventTask
- InventoryItem
- Shift
- ShiftParticipation
- ShiftType
- WeekSchedule

along with supporting lookup and status entities used by the application.

---

## How to Run the Project

### Prerequisites
- **Visual Studio**
- **Microsoft SQL Server**
- access to create and run a SQL Server database
- Windows environment for WinForms execution

### Setup Steps
1. Clone or download the repository.
2. Open the solution or project in Visual Studio.
3. Create a SQL Server database.
4. Run the SQL scripts in this order:
   - `SQL/01_schema_and_procedures.sql`
   - `SQL/02_seed_data.sql`
5. Update the database connection string in `StreetGames/SQL_CON.cs`.
6. Build and run the WinForms project.

---

## Important Configuration Note

This repository is intended for portfolio and educational purposes.

Before running the application, update the connection string in:

`StreetGames/SQL_CON.cs`

Use your own SQL Server instance and database name.

Example placeholder:

```csharp
public static string connStr =
    "Server=YOUR_SERVER;Database=YOUR_DATABASE;Trusted_Connection=True;";
```

> Do not commit private credentials or sensitive connection details to a public repository.

---

## What This Project Demonstrates

This project highlights experience in:
- end-to-end information systems design
- translating business requirements into UML models
- building a relational SQL database from a conceptual design
- implementing a desktop information system in C#
- connecting application workflows to stored procedures and structured data logic
- designing role-based operational modules for business processes

---

## Academic Context

This project was developed as part of an **Information Systems Analysis and Design** course and reflects both the analytical and design process and the technical implementation phase.

It is presented here as a portfolio project that combines:
- systems thinking
- structured design
- SQL development
- desktop application implementation

---

## Future Improvements

Potential future upgrades could include:
- stronger authentication and authorization logic
- configuration via app settings instead of direct connection string editing
- improved validation and exception handling
- reporting and analytics dashboards
- migration from WinForms to a modern web-based or desktop UI stack
- cleaner packaging of SQL deployment scripts

---

## Notes

- Generated folders such as `.vs/`, `bin/`, and `obj/` are intentionally excluded from version control.
- The project includes both implementation assets and design documentation to show the full system lifecycle.
- Some project files reflect the academic development process and are preserved to represent the original end-to-end work.

---

## Team

This project was developed as a team project.

- **Roi Laniado**
- **Yoav Nesher**
- **Maya Raz**
- **Uri Sharf**
- **Yoav Nesher**


---

## Repository Structure

```text
StreetGames/
│
├── README.md
├── .gitignore
├── ProjectOverview.pdf
│
├── Screenshots/
│   ├── login.png
│   ├── manager-dashboard.png
│   ├── employee-dashboard.png
│   ├── manage-employees.png
│   ├── inventory-monitoring.png
│   ├── event-tasks.png
│   ├── week-scheduling.png
│   ├── class-diagram.jpg
│   ├── sequence-diagram.png
│   ├── state-diagram-shift-swap.png
│   └── state-diagram-manage-event.png
│
├── Diagrams/
│   ├── Use Case Diagram.vpp
│   └── Class Diagram and State Diagrams.vpp
│
├── SQL/
│   ├── 01_schema_and_procedures.sql
│   └── 02_seed_data.sql
│
└── StreetGames/
    ├── Classes/
    ├── Forms/
    ├── Properties/
    ├── Program.cs
    ├── SQL_CON.cs
    ├── StreetGames.csproj
    └── StreetGames.slnx
```
