# StreetGames – Information System Analysis, Design & Implementation
<p align="center">
  <img src="Screenshots/banner.png" alt="StreetGames Banner" width="900">
</p>

## Overview

**StreetGames** is a university Information Systems Analysis and Design project based on a real business, **StreetGames**, an arcade and entertainment chain in Israel.

The idea behind the project was to take a real business environment and think about what kind of internal information system could help it run more effectively. Instead of inventing a completely imaginary company, we worked around an existing business context and tried to understand the operational side behind it, such as managing employees, schedules, inventory, and event-related tasks.

Rather than starting directly with coding, we began by thinking about the business itself: how it works, what information needs to be tracked, which users interact with the system, and which daily processes could be supported better through a structured system.

From there, the project developed into a full end-to-end information system process:
- analyzing business needs and workflows
- translating needs into requirements and system logic
- modeling the system with UML diagrams
- designing the database structure
- implementing the application in C#
- connecting the interface and workflows to SQL Server

The final result is **StreetGames**, a business management system designed for an entertainment and event operations environment. In our version of the system, we focused on internal processes such as employee management, weekly scheduling, inventory monitoring, and event task tracking.

This repository presents the project not only as software, but as a full **analysis, design, and implementation project**.

---

## The Story Behind the Project

This project was created as part of an academic process that aimed to simulate how information systems are actually built in the real world. The main idea was not to jump straight into development, but first to understand the organization, its needs, and its workflows.

As students, we approached the project step by step. We chose to work around the context of StreetGames and think about the kind of system a business like this could need behind the scenes in order to manage its operations more efficiently.

That meant asking questions like:
- What information does the business need to manage on a daily basis?
- Which processes involve managers, and which involve employees?
- What tasks repeat regularly and should be organized in the system?
- Which business rules should the system enforce?
- How should the system be designed so that it supports daily work in a clear and practical way?

Only after building that foundation did we move on to modeling and implementation.

What made this project meaningful for us was that it was not just about building forms or writing SQL queries. It was about taking a business context, analyzing it like systems analysts, and turning it into a structured solution through design, database thinking, and application development.

---

## Project Purpose

The purpose of this project was to design and implement an information system from scratch for a realistic business environment, based on business needs and supported by structured analysis and design methods.

The project aimed to show how knowledge from:
- information systems analysis
- object-oriented design
- database modeling
- SQL programming
- application development

can be combined into one complete working system.

The system was built to support key operational activities such as:
- employee management
- role-based system access
- weekly shift scheduling
- inventory monitoring
- event-task tracking
- workflow support through structured logic and data operations

Overall, the project was an opportunity for us to take what we learned in class and apply it in a more realistic way by designing a system around the needs of an actual business environment.

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

This project was developed as a team project of 4 Bsc. Industrial Engineering and Management students.

- **Roi Laniado**
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
