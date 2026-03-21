
GO
/****** Object: Table [dbo].[Customer] Script Date:
11/01/2026 20:27:33 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Customer](
[customerId] [int]
IDENTITY(1,1) NOT NULL,
[firstName]
[varchar](100) NOT NULL,
[lastName]
[varchar](100) NOT NULL,
[phone]
[varchar](20) NOT NULL,
[email]
[varchar](100) NOT NULL,
[registeredViaFormId]
[int] NOT NULL,
PRIMARY KEY CLUSTERED
(
[customerId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY =
OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY =
OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object: Table [dbo].[CustomerCard] Script
Date: 11/01/2026 20:27:33 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CustomerCard](
[cardId] [int]
IDENTITY(1,1) NOT NULL,
[customerId] [int]
NOT NULL,
[cardNumber]
[varchar](50) NULL,
[issuedAt]
[datetime] NULL,
[expiryDate] [date]
NULL,
[balance] [int] NOT
NULL,
[statusId] [int]
NULL,
PRIMARY KEY CLUSTERED
(
[cardId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY =
OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY =
OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object: Table [bdo].[DynamicForm] Script
Date: 11/01/2026 20:27:33 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[DynamicForm](
[formId] [int]
IDENTITY(1,1) NOT NULL,
[name]
[varchar](100) NOT NULL,
[description]
[varchar](max) NULL,
[statusId] [int]
NULL,
[lastUpdated]
[datetime] NULL,
PRIMARY KEY CLUSTERED
(
[formId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY =
OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY =
OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object: Table [dbo].[Employee] Script Date:
11/01/2026 20:27:33 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Employee](
[employeeId] [int]
IDENTITY(1,1) NOT NULL,
[firstName]
[varchar](max) NOT NULL,
[lastName]
[varchar](max) NOT NULL,
[phone]
[varchar](20) NOT NULL,
[email]
[varchar](100) NOT NULL,
[hireDate] [date]
NOT NULL,
[employmentStatusId]
[int] NOT NULL,
[roleId] [int] NOT
NULL,
PRIMARY KEY CLUSTERED
(
[employeeId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY =
OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY =
OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object: Table [dbo].[Event] Script Date:
11/01/2026 20:27:33 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Event](
[eventId] [int]
IDENTITY(1,1) NOT NULL,
[customerId] [int]
NOT NULL,
[responsibleEmployeeId]
[int] NULL,
[eventDate] [date]
NOT NULL,
[startTime]
[time](7) NOT NULL,
[duration] [float]
NOT NULL,
[participantsCount]
[int] NOT NULL,
[statusId] [int] NOT
NULL,
[notes]
[varchar](max) NULL,
[askedViaFormId]
[int] NULL,
PRIMARY KEY CLUSTERED
(
[eventId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY =
OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY =
OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object: Table [dbo].[EventTask] Script Date:
11/01/2026 20:27:33 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[EventTask](
[taskId] [int]
IDENTITY(1,1) NOT NULL,
[eventId] [int] NOT
NULL,
[title]
[varchar](200) NOT NULL,
[description]
[varchar](max) NULL,
[dueTime] [time](7)
NOT NULL,
[statusId] [int] NOT
NULL,
PRIMARY KEY CLUSTERED
(
[taskId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY =
OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY =
OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object: Table [dbo].[FieldOption] Script
Date: 11/01/2026 20:27:33 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[FieldOption](
[optionId] [int]
IDENTITY(1,1) NOT NULL,
[fieldId] [int] NOT
NULL,
[optionText]
[varchar](max) NULL,
PRIMARY KEY CLUSTERED
(
[optionId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY =
OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY =
OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object: Table [dbo].[FormField] Script Date:
11/01/2026 20:27:33 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[FormField](
[fieldId] [int]
IDENTITY(1,1) NOT NULL,
[formId] [int] NOT
NULL,
[label]
[varchar](255) NULL,
[typeId] [int] NULL,
[required] [bit]
NULL,
[orderIndex] [int]
NULL,
[activationStatusId]
[int] NULL,
PRIMARY KEY CLUSTERED
(
[fieldId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY =
OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY =
OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object: Table [dbo].[InventoryItem] Script
Date: 11/01/2026 20:27:33 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[InventoryItem](
[itemId] [int]
IDENTITY(1,1) NOT NULL,
[name]
[varchar](100) NULL,
[categoryId] [int]
NULL,
[quantity] [int] NOT
NULL,
[minLevel] [int] NOT
NULL,
[expirationDate]
[date] NULL,
[statusId] [int]
NULL,
[addedViaFormId]
[int] NULL,
PRIMARY KEY CLUSTERED
(
[itemId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY =
OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY =
OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object: Table [dbo].[Lookup_ActivationStatus]
Script Date: 11/01/2026 20:27:33 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Lookup_ActivationStatus](
[statusId] [int]
IDENTITY(1,1) NOT NULL,
[name] [varchar](50)
NOT NULL,
PRIMARY KEY CLUSTERED
(
[statusId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY =
OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY =
OFF) ON [PRIMARY],
UNIQUE NONCLUSTERED
(
[name] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY =
OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY =
OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object: Table [dbo].[Lookup_AssignmentStatus]
Script Date: 11/01/2026 20:27:33 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Lookup_AssignmentStatus](
[statusId] [int]
IDENTITY(1,1) NOT NULL,
[name] [varchar](50)
NOT NULL,
PRIMARY KEY CLUSTERED
(
[statusId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY =
OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY =
OFF) ON [PRIMARY],
UNIQUE NONCLUSTERED
(
[name] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY =
OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY =
OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object: Table [dbo].[Lookup_AvailabilityStatus]
Script Date: 11/01/2026 20:27:33 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Lookup_AvailabilityStatus](
[statusId] [int]
IDENTITY(1,1) NOT NULL,
[name] [varchar](50)
NOT NULL,
PRIMARY KEY CLUSTERED
(
[statusId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY =
OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY =
OFF) ON [PRIMARY],
UNIQUE NONCLUSTERED
(
[name] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY =
OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY =
OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object: Table [dbo].[Lookup_CardStatus]
Script Date: 11/01/2026 20:27:33 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Lookup_CardStatus](
[statusId] [int]
IDENTITY(1,1) NOT NULL,
[name] [varchar](50)
NOT NULL,
PRIMARY KEY CLUSTERED
(
[statusId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY =
OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY =
OFF) ON [PRIMARY],
UNIQUE NONCLUSTERED
(
[name] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY =
OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY =
OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object: Table [dbo].[Lookup_EmployeeRole]
Script Date: 11/01/2026 20:27:33 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Lookup_EmployeeRole](
[roleId] [int]
IDENTITY(1,1) NOT NULL,
[name] [varchar](50)
NOT NULL,
PRIMARY KEY CLUSTERED
(
[roleId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY =
OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY =
OFF) ON [PRIMARY],
UNIQUE NONCLUSTERED
(
[name] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY =
OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY =
OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object: Table [dbo].[Lookup_EmploymentStatus]
Script Date: 11/01/2026 20:27:33 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Lookup_EmploymentStatus](
[statusId] [int]
IDENTITY(1,1) NOT NULL,
[name] [varchar](50)
NOT NULL,
PRIMARY KEY CLUSTERED
(
[statusId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY =
OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY =
OFF) ON [PRIMARY],
UNIQUE NONCLUSTERED
(
[name] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY =
OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY =
OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object: Table [dbo].[Lookup_EventStatus]
Script Date: 11/01/2026 20:27:33 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Lookup_EventStatus](
[statusId] [int]
IDENTITY(1,1) NOT NULL,
[name] [varchar](50)
NOT NULL,
PRIMARY KEY CLUSTERED
(
[statusId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY =
OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY =
OFF) ON [PRIMARY],
UNIQUE NONCLUSTERED
(
[name] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY =
OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY =
OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object: Table [dbo].[Lookup_FieldType] Script
Date: 11/01/2026 20:27:33 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Lookup_FieldType](
[typeId] [int]
IDENTITY(1,1) NOT NULL,
[name] [varchar](50)
NOT NULL,
PRIMARY KEY CLUSTERED
(
[typeId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY =
OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY =
OFF) ON [PRIMARY],
UNIQUE NONCLUSTERED
(
[name] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY =
OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY =
OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object: Table [dbo].[Lookup_ItemCategory]
Script Date: 11/01/2026 20:27:33 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Lookup_ItemCategory](
[categoryId] [int]
IDENTITY(1,1) NOT NULL,
[name] [varchar](50)
NOT NULL,
PRIMARY KEY CLUSTERED
(
[categoryId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY =
OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY =
OFF) ON [PRIMARY],
UNIQUE NONCLUSTERED
(
[name] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY =
OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY =
OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object: Table [dbo].[Lookup_ScheduleStatus]
Script Date: 11/01/2026 20:27:33 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Lookup_ScheduleStatus](
[statusId] [int]
IDENTITY(1,1) NOT NULL,
[name] [varchar](50)
NOT NULL,
PRIMARY KEY CLUSTERED
(
[statusId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY =
OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY =
OFF) ON [PRIMARY],
UNIQUE NONCLUSTERED
(
[name] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY =
OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY =
OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object: Table [dbo].[Lookup_SwapRequestStatus]
Script Date: 11/01/2026 20:27:33 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Lookup_SwapRequestStatus](
[statusId] [int]
IDENTITY(1,1) NOT NULL,
[name] [varchar](50)
NOT NULL,
PRIMARY KEY CLUSTERED
(
[statusId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY =
OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY =
OFF) ON [PRIMARY],
UNIQUE NONCLUSTERED
(
[name] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY =
OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY =
OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object: Table [dbo].[Lookup_TaskStatus]
Script Date: 11/01/2026 20:27:33 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Lookup_TaskStatus](
[statusId] [int]
IDENTITY(1,1) NOT NULL,
[name] [varchar](50)
NOT NULL,
PRIMARY KEY CLUSTERED
(
[statusId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY =
OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY =
OFF) ON [PRIMARY],
UNIQUE NONCLUSTERED
(
[name] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY =
OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY =
OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object: Table [dbo].[Shift] Script Date:
11/01/2026 20:27:33 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Shift](
[shiftId] [int]
IDENTITY(1,1) NOT NULL,
[weekScheduleId]
[int] NOT NULL,
[shiftTypeId] [int]
NOT NULL,
[shiftDate] [date]
NOT NULL,
PRIMARY KEY CLUSTERED
(
[shiftId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY =
OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY =
OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object: Table [dbo].[ShiftParticipation]
Script Date: 11/01/2026 20:27:33 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ShiftParticipation](
[participationId]
[int] IDENTITY(1,1) NOT NULL,
[shiftId] [int] NOT
NULL,
[employeeId] [int]
NOT NULL,
[availabilityStatusId]
[int] NOT NULL,
[assignmentStatusId]
[int] NOT NULL,
[submittedAt]
[datetime] NOT NULL,
[assignedAt]
[datetime] NULL,
[form_id] [int] NOT
NULL,
PRIMARY KEY CLUSTERED
(
[participationId]
ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY =
OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY =
OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object: Table [dbo].[ShiftSwapRequest] Script
Date: 11/01/2026 20:27:33 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ShiftSwapRequest](
[swapRequestId]
[int] IDENTITY(1,1) NOT NULL,
[requesterParticipationId]
[int] NOT NULL,
[targetParticipationId]
[int] NOT NULL,
[statusId] [int]
NULL,
[feedback]
[varchar](max) NULL,
PRIMARY KEY CLUSTERED
(
[swapRequestId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY =
OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY =
OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object: Table [dbo].[ShiftType] Script Date:
11/01/2026 20:27:33 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [bdbo].[ShiftType](
[shiftTypeId] [int]
IDENTITY(1,1) NOT NULL,
[name]
[varchar](100) NOT NULL,
[startTime]
[time](7) NOT NULL,
[endTime] [time](7)
NOT NULL,
[requiredEmployees]
[int] NOT NULL,
[description]
[varchar](max) NULL,
[activeStatusId]
[int] NOT NULL,
PRIMARY KEY CLUSTERED
(
[shiftTypeId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY =
OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY =
OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object: Table [dbo].[WeekSchedule] Script
Date: 11/01/2026 20:27:33 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[WeekSchedule](
[weekScheduleId]
[int] IDENTITY(1,1) NOT NULL,
[weekStartDate]
[date] NOT NULL,
[submissionDeadline]
[datetime] NULL,
[statusId] [int] NOT
NULL,
PRIMARY KEY CLUSTERED
(
[weekScheduleId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY =
OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY =
OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object: Table [dbo].[Lookup_ActivationStatus]
Script Date: 11/01/2026 20:27:33 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Lookup_ActivationStatus](
[statusId] [int]
IDENTITY(1,1) NOT NULL,
[name] [varchar](50)
NOT NULL,
PRIMARY KEY CLUSTERED
(
[statusId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY =
OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY =
OFF) ON [PRIMARY],
UNIQUE NONCLUSTERED
(
[name] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY =
OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY =
OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object: Table [dbo].[Lookup_AssignmentStatus]
Script Date: 11/01/2026 20:27:33 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Lookup_AssignmentStatus](
[statusId] [int]
IDENTITY(1,1) NOT NULL,
[name] [varchar](50)
NOT NULL,
PRIMARY KEY CLUSTERED
(
[statusId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY =
OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY =
OFF) ON [PRIMARY],
UNIQUE NONCLUSTERED
(
[name] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY =
OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY =
OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object: Table [dbo].[Lookup_AvailabilityStatus]
Script Date: 11/01/2026 20:27:33 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Lookup_AvailabilityStatus](
[statusId] [int]
IDENTITY(1,1) NOT NULL,
[name] [varchar](50)
NOT NULL,
PRIMARY KEY CLUSTERED
(
[statusId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY =
OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY =
OFF) ON [PRIMARY],
UNIQUE NONCLUSTERED
(
[name] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY =
OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY =
OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object: Table [dbo].[Lookup_CardStatus]
Script Date: 11/01/2026 20:27:33 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Lookup_CardStatus](
[statusId] [int]
IDENTITY(1,1) NOT NULL,
[name] [varchar](50)
NOT NULL,
PRIMARY KEY CLUSTERED
(
[statusId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY =
OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY =
OFF) ON [PRIMARY],
UNIQUE NONCLUSTERED
(
[name] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY =
OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY =
OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object: Table [dbo].[Lookup_EmployeeRole] Script
Date: 11/01/2026 20:27:33 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Lookup_EmployeeRole](
[roleId] [int]
IDENTITY(1,1) NOT NULL,
[name] [varchar](50)
NOT NULL,
PRIMARY KEY CLUSTERED
(
[roleId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY =
OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY =
OFF) ON [PRIMARY],
UNIQUE NONCLUSTERED
(
[name] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY =
OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY =
OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object: Table [dbo].[Lookup_EmploymentStatus]
Script Date: 11/01/2026 20:27:33 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Lookup_EmploymentStatus](
[statusId] [int]
IDENTITY(1,1) NOT NULL,
[name] [varchar](50)
NOT NULL,
PRIMARY KEY CLUSTERED
(
[statusId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY =
OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY =
OFF) ON [PRIMARY],
UNIQUE NONCLUSTERED
(
[name] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY =
OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY =
OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object: Table [dbo].[Lookup_EventStatus]
Script Date: 11/01/2026 20:27:33 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Lookup_EventStatus](
[statusId] [int]
IDENTITY(1,1) NOT NULL,
[name] [varchar](50)
NOT NULL,
PRIMARY KEY CLUSTERED
(
[statusId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY =
OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY =
OFF) ON [PRIMARY],
UNIQUE NONCLUSTERED
(
[name] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY =
OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY =
OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object: Table [dbo].[Lookup_FieldType] Script
Date: 11/01/2026 20:27:33 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Lookup_FieldType](
[typeId] [int]
IDENTITY(1,1) NOT NULL,
[name] [varchar](50)
NOT NULL,
PRIMARY KEY CLUSTERED
(
[typeId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY =
OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY =
OFF) ON [PRIMARY],
UNIQUE NONCLUSTERED
(
[name] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY =
OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY =
OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object: Table [dbo].[Lookup_ItemCategory]
Script Date: 11/01/2026 20:27:33 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Lookup_ItemCategory](
[categoryId] [int]
IDENTITY(1,1) NOT NULL,
[name] [varchar](50)
NOT NULL,
PRIMARY KEY CLUSTERED
(
[categoryId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY =
OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY =
OFF) ON [PRIMARY],
UNIQUE NONCLUSTERED
(
[name] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY =
OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY =
OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object: Table [dbo].[Lookup_ScheduleStatus]
Script Date: 11/01/2026 20:27:33 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Lookup_ScheduleStatus](
[statusId] [int]
IDENTITY(1,1) NOT NULL,
[name] [varchar](50)
NOT NULL,
PRIMARY KEY CLUSTERED
(
[statusId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY =
OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY =
OFF) ON [PRIMARY],
UNIQUE NONCLUSTERED
(
[name] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY =
OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY =
OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object: Table [dbo].[Lookup_SwapRequestStatus]
Script Date: 11/01/2026 20:27:33 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Lookup_SwapRequestStatus](
[statusId] [int]
IDENTITY(1,1) NOT NULL,
[name] [varchar](50)
NOT NULL,
PRIMARY KEY CLUSTERED
(
[statusId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY =
OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY =
OFF) ON [PRIMARY],
UNIQUE NONCLUSTERED
(
[name] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY =
OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY =
OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object: Table [dbo].[Lookup_TaskStatus]
Script Date: 11/01/2026 20:27:33 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Lookup_TaskStatus](
[statusId] [int]
IDENTITY(1,1) NOT NULL,
[name] [varchar](50)
NOT NULL,
PRIMARY KEY CLUSTERED
(
[statusId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY =
OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY =
OFF) ON [PRIMARY],
UNIQUE NONCLUSTERED
(
[name] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY =
OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY =
OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Customer] ADD CONSTRAINT
[DF_Customer_RegisteredViaForm] DEFAULT ((2)) FOR [registeredViaFormId]
GO
ALTER TABLE [dbo].[CustomerCard] ADD CONSTRAINT
[DF_CustomerCard_Balance] DEFAULT ((0)) FOR [balance]
GO
ALTER TABLE [dbo].[InventoryItem] ADD CONSTRAINT
[DF_InventoryItem_Quantity] DEFAULT ((0)) FOR [quantity]
GO
ALTER TABLE [dbo].[InventoryItem] ADD CONSTRAINT
[DF_InventoryItem_MinLevel] DEFAULT ((0)) FOR [minLevel]
GO
ALTER TABLE [dbo].[ShiftParticipation] ADD CONSTRAINT
[DF_ShiftParticipation_Form] DEFAULT ((2)) FOR [form_id]
GO
ALTER TABLE [dbo].[ShiftType] ADD CONSTRAINT
[DF_ShiftType_ActiveStatus] DEFAULT ((1)) FOR [activeStatusId]
GO
ALTER TABLE [dbo].[Customer] WITH CHECK ADD FOREIGN
KEY([registeredViaFormId])
REFERENCES [dbo].[DynamicForm] ([formId])
GO
ALTER TABLE [dbo].[Customer] WITH CHECK ADD FOREIGN
KEY([registeredViaFormId])
REFERENCES [dbo].[DynamicForm] ([formId])
GO
ALTER TABLE [dbo].[CustomerCard] WITH CHECK ADD FOREIGN
KEY([customerId])
REFERENCES [dbo].[Customer] ([customerId])
GO
ALTER TABLE [dbo].[CustomerCard] WITH CHECK ADD FOREIGN
KEY([customerId])
REFERENCES [dbo].[Customer] ([customerId])
GO
ALTER TABLE [dbo].[CustomerCard] WITH CHECK ADD FOREIGN
KEY([statusId])
REFERENCES [dbo].[Lookup_CardStatus] ([statusId])
GO
ALTER TABLE [dbo].[CustomerCard] WITH CHECK ADD FOREIGN
KEY([statusId])
REFERENCES [dbo].[Lookup_CardStatus] ([statusId])
GO
ALTER TABLE [dbo].[DynamicForm] WITH CHECK ADD FOREIGN
KEY([statusId])
REFERENCES [dbo].[Lookup_ActivationStatus] ([statusId])
GO
ALTER TABLE [dbo].[DynamicForm] WITH CHECK ADD FOREIGN
KEY([statusId])
REFERENCES [dbo].[Lookup_ActivationStatus] ([statusId])
GO
ALTER TABLE [dbo].[Employee] WITH CHECK ADD FOREIGN
KEY([employmentStatusId])
REFERENCES [dbo].[Lookup_EmploymentStatus] ([statusId])
GO
ALTER TABLE [dbo].[Employee] WITH CHECK ADD FOREIGN
KEY([employmentStatusId])
REFERENCES [dbo].[Lookup_EmploymentStatus] ([statusId])
GO
ALTER TABLE [dbo].[Employee] WITH CHECK ADD FOREIGN KEY([roleId])
REFERENCES [dbo].[Lookup_EmployeeRole] ([roleId])
GO
ALTER TABLE [dbo].[Employee] WITH CHECK ADD FOREIGN KEY([roleId])
REFERENCES [dbo].[Lookup_EmployeeRole] ([roleId])
GO
ALTER TABLE [dbo].[Event] WITH CHECK ADD FOREIGN
KEY([askedViaFormId])
REFERENCES [dbo].[DynamicForm] ([formId])
GO
ALTER TABLE [dbo].[Event] WITH CHECK ADD FOREIGN
KEY([askedViaFormId])
REFERENCES [dbo].[DynamicForm] ([formId])
GO
ALTER TABLE [dbo].[Event] WITH CHECK ADD FOREIGN KEY([customerId])
REFERENCES [dbo].[Customer] ([customerId])
GO
ALTER TABLE [dbo].[Event] WITH CHECK ADD FOREIGN
KEY([customerId])
REFERENCES [dbo].[Customer] ([customerId])
GO
ALTER TABLE [dbo].[Event] WITH CHECK ADD FOREIGN
KEY([responsibleEmployeeId])
REFERENCES [dbo].[Employee] ([employeeId])
GO
ALTER TABLE [dbo].[Event] WITH CHECK ADD FOREIGN
KEY([responsibleEmployeeId])
REFERENCES [dbo].[Employee] ([employeeId])
GO
ALTER TABLE [dbo].[Event] WITH CHECK ADD FOREIGN KEY([statusId])
REFERENCES [dbo].[Lookup_EventStatus] ([statusId])
GO
ALTER TABLE [dbo].[Event] WITH CHECK ADD FOREIGN KEY([statusId])
REFERENCES [dbo].[Lookup_EventStatus] ([statusId])
GO
ALTER TABLE [dbo].[EventTask] WITH CHECK ADD FOREIGN
KEY([eventId])
REFERENCES [dbo].[Event] ([eventId])
GO
ALTER TABLE [dbo].[EventTask] WITH CHECK ADD FOREIGN
KEY([eventId])
REFERENCES [dbo].[Event] ([eventId])
GO
ALTER TABLE [dbo].[EventTask] WITH CHECK ADD FOREIGN
KEY([statusId])
REFERENCES [dbo].[Lookup_TaskStatus] ([statusId])
GO
ALTER TABLE [dbo].[EventTask] WITH CHECK ADD FOREIGN
KEY([statusId])
REFERENCES [dbo].[Lookup_TaskStatus] ([statusId])
GO
ALTER TABLE [dbo].[FieldOption] WITH CHECK ADD FOREIGN
KEY([fieldId])
REFERENCES [dbo].[FormField] ([fieldId])
GO
ALTER TABLE [dbo].[FieldOption] WITH CHECK ADD FOREIGN
KEY([fieldId])
REFERENCES [dbo].[FormField] ([fieldId])
GO
ALTER TABLE [dbo].[FormField] WITH CHECK ADD FOREIGN
KEY([activationStatusId])
REFERENCES [dbo].[Lookup_ActivationStatus] ([statusId])
GO
ALTER TABLE [dbo].[FormField] WITH CHECK ADD FOREIGN
KEY([activationStatusId])
REFERENCES [dbo].[Lookup_ActivationStatus] ([statusId])
GO
ALTER TABLE [dbo].[FormField] WITH CHECK ADD FOREIGN KEY([formId])
REFERENCES [dbo].[DynamicForm] ([formId])
GO
ALTER TABLE [dbo].[FormField] WITH CHECK ADD FOREIGN KEY([formId])
REFERENCES [dbo].[DynamicForm] ([formId])
GO
ALTER TABLE [dbo].[FormField] WITH CHECK ADD FOREIGN KEY([typeId])
REFERENCES [dbo].[Lookup_FieldType] ([typeId])
GO
ALTER TABLE [dbo].[FormField] WITH CHECK ADD FOREIGN KEY([typeId])
REFERENCES [dbo].[Lookup_FieldType] ([typeId])
GO
ALTER TABLE [dbo].[InventoryItem] WITH CHECK ADD FOREIGN
KEY([addedViaFormId])
REFERENCES [dbo].[DynamicForm] ([formId])
GO
ALTER TABLE [dbo].[InventoryItem] WITH CHECK ADD FOREIGN
KEY([addedViaFormId])
REFERENCES [dbo].[DynamicForm] ([formId])
GO
ALTER TABLE [dbo].[InventoryItem] WITH CHECK ADD FOREIGN
KEY([categoryId])
REFERENCES [dbo].[Lookup_ItemCategory] ([categoryId])
GO
ALTER TABLE [dbo].[InventoryItem] WITH CHECK ADD FOREIGN
KEY([categoryId])
REFERENCES [dbo].[Lookup_ItemCategory] ([categoryId])
GO
ALTER TABLE [dbo].[InventoryItem] WITH CHECK ADD FOREIGN
KEY([statusId])
REFERENCES [dbo].[Lookup_ActivationStatus] ([statusId])
GO
ALTER TABLE [dbo].[InventoryItem] WITH CHECK ADD FOREIGN
KEY([statusId])
REFERENCES [dbo].[Lookup_ActivationStatus] ([statusId])
GO
ALTER TABLE [dbo].[Shift] WITH CHECK ADD FOREIGN
KEY([shiftTypeId])
REFERENCES [dbo].[ShiftType] ([shiftTypeId])
GO
ALTER TABLE [dbo].[Shift] WITH CHECK ADD FOREIGN
KEY([shiftTypeId])
REFERENCES [dbo].[ShiftType] ([shiftTypeId])
GO
ALTER TABLE [dbo].[Shift] WITH CHECK ADD FOREIGN
KEY([weekScheduleId])
REFERENCES [dbo].[WeekSchedule] ([weekScheduleId])
GO
ALTER TABLE [dbo].[Shift] WITH CHECK ADD FOREIGN
KEY([weekScheduleId])
REFERENCES [dbo].[WeekSchedule] ([weekScheduleId])
GO
ALTER TABLE [dbo].[ShiftParticipation] WITH CHECK ADD
FOREIGN KEY([assignmentStatusId])
REFERENCES [dbo].[Lookup_AssignmentStatus] ([statusId])
GO
ALTER TABLE [dbo].[ShiftParticipation] WITH CHECK ADD
FOREIGN KEY([assignmentStatusId])
REFERENCES [dbo].[Lookup_AssignmentStatus] ([statusId])
GO
ALTER TABLE [dbo].[ShiftParticipation] WITH CHECK ADD
FOREIGN KEY([availabilityStatusId])
REFERENCES [dbo].[Lookup_AvailabilityStatus] ([statusId])
GO
ALTER TABLE [dbo].[ShiftParticipation] WITH CHECK ADD
FOREIGN KEY([availabilityStatusId])
REFERENCES [dbo].[Lookup_AvailabilityStatus] ([statusId])
GO
ALTER TABLE [dbo].[ShiftParticipation] WITH CHECK ADD
FOREIGN KEY([employeeId])
REFERENCES [dbo].[Employee] ([employeeId])
GO
ALTER TABLE [dbo].[ShiftParticipation] WITH CHECK ADD
FOREIGN KEY([employeeId])
REFERENCES [dbo].[Employee] ([employeeId])
GO
ALTER TABLE [dbo].[ShiftParticipation] WITH CHECK ADD
FOREIGN KEY([form_id])
REFERENCES [dbo].[DynamicForm] ([formId])
GO
ALTER TABLE [dbo].[ShiftParticipation] WITH CHECK ADD
FOREIGN KEY([form_id])
REFERENCES [dbo].[DynamicForm] ([formId])
GO
ALTER TABLE [dbo].[ShiftParticipation] WITH CHECK ADD
FOREIGN KEY([shiftId])
REFERENCES [dbo].[Shift] ([shiftId])
GO
ALTER TABLE [dbo].[ShiftParticipation] WITH CHECK ADD
FOREIGN KEY([shiftId])
REFERENCES [dbo].[Shift] ([shiftId])
GO
ALTER TABLE [dbo].[ShiftSwapRequest] WITH CHECK ADD
FOREIGN KEY([requesterParticipationId])
REFERENCES [dbo].[ShiftParticipation] ([participationId])
GO
ALTER TABLE [dbo].[ShiftSwapRequest] WITH CHECK ADD
FOREIGN KEY([requesterParticipationId])
REFERENCES [dbo].[ShiftParticipation] ([participationId])
GO
ALTER TABLE [dbo].[ShiftSwapRequest] WITH CHECK ADD
FOREIGN KEY([statusId])
REFERENCES [dbo].[Lookup_SwapRequestStatus] ([statusId])
GO
ALTER TABLE [dbo].[ShiftSwapRequest] WITH CHECK ADD
FOREIGN KEY([statusId])
REFERENCES [dbo].[Lookup_SwapRequestStatus] ([statusId])
GO
ALTER TABLE [dbo].[ShiftSwapRequest] WITH CHECK ADD
FOREIGN KEY([targetParticipationId])
REFERENCES [dbo].[ShiftParticipation] ([participationId])
GO
ALTER TABLE [dbo].[ShiftSwapRequest] WITH CHECK ADD
FOREIGN KEY([targetParticipationId])
REFERENCES [dbo].[ShiftParticipation] ([participationId])
GO
ALTER TABLE [dbo].[ShiftType] WITH CHECK ADD FOREIGN
KEY([activeStatusId])
REFERENCES [dbo].[Lookup_ActivationStatus] ([statusId])
GO
ALTER TABLE [dbo].[ShiftType] WITH CHECK ADD FOREIGN
KEY([activeStatusId])
REFERENCES [dbo].[Lookup_ActivationStatus] ([statusId])
GO
ALTER TABLE [dbo].[WeekSchedule] WITH CHECK ADD
CONSTRAINT [FK_WeekSchedule_Status] FOREIGN KEY([statusId])
REFERENCES [dbo].[Lookup_ScheduleStatus] ([statusId])
GO
ALTER TABLE [dbo].[WeekSchedule] CHECK CONSTRAINT
[FK_WeekSchedule_Status]
GO
/****** Object: StoredProcedure [dbo].[Get_Customers]
Script Date: 11/01/2026 20:27:33 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[Get_Customers]
AS
BEGIN
SET NOCOUNT ON;
SELECT * FROM [dbo].[Customer];
END
GO
/****** Object: StoredProcedure [dbo].[Get_Employees]
Script Date: 11/01/2026 20:27:33 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/* =========================
GET procedures for init_*
Using your actual table names (singular)
========================= */
CREATE PROCEDURE [dbo].[Get_Employees]
AS
BEGIN
SET NOCOUNT ON;
SELECT * FROM dbo.Employee;
END
GO
/****** Object: StoredProcedure [dbo].[Get_Events]
Script Date: 11/01/2026 20:27:33 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[Get_Events]
WITH EXECUTE AS OWNER
AS
BEGIN
SET NOCOUNT ON;
SELECT
eventId,
-- 0
customerId,
-- 1
responsibleEmployeeId,
-- 2 (nullable)
eventDate,
-- 3
startTime,
-- 4
duration,
-- 5
participantsCount,
-- 6
statusId,
-- 7
notes,
-- 8 (nullable)
askedViaFormId
-- 9 (nullable)
FROM [dbo].[Event];
END
GO
/****** Object: StoredProcedure [dbo].[Get_EventTasks]
Script Date: 11/01/2026 20:27:33 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[Get_EventTasks]
AS
BEGIN
SET NOCOUNT ON;
SELECT taskId,
eventId,
title,
description,
dueTime,
statusId
FROM [dbo].EventTask;
END
GO
/****** Object: StoredProcedure [dbo].[Get_InventoryItems]
Script Date: 11/01/2026 20:27:33 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[Get_InventoryItems]
WITH EXECUTE AS OWNER
AS
BEGIN
SET NOCOUNT ON;
SELECT
itemId,
name,
categoryId,
quantity,
minLevel,
expirationDate,
statusId,
addedViaFormId
FROM [dbo].[InventoryItem]
ORDER BY itemId;
END
GO
/****** Object: StoredProcedure
[dbo].[Get_ShiftParticipations] Script Date: 11/01/2026 20:27:33
******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[Get_ShiftParticipations]
AS
BEGIN
SELECT * FROM dbo.ShiftParticipation;
END
GO
/****** Object: StoredProcedure [dbo].[Get_Shifts] Script
Date: 11/01/2026 20:27:33 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[Get_Shifts]
AS
BEGIN
SELECT * FROM dbo.[Shift];
END
GO
/****** Object: StoredProcedure [dbo].[Get_ShiftTypes]
Script Date: 11/01/2026 20:27:33 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[Get_ShiftTypes]
AS
BEGIN
SET NOCOUNT ON;
SELECT * FROM dbo.ShiftType;
END
GO
/****** Object: StoredProcedure [dbo].[Get_WeekSchedules]
Script Date: 11/01/2026 20:27:33 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[Get_WeekSchedules]
AS
BEGIN
SELECT * FROM dbo.WeekSchedule;
END
GO
/****** Object: StoredProcedure [dbo].[Insert_Shift]
Script Date: 11/01/2026 20:27:33 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- 1.4 Insert Shift (returns new shiftId)
CREATE PROCEDURE [dbo].[Insert_Shift]
@weekScheduleId INT,
@shiftTypeId INT,
@shiftDate DATE
AS
BEGIN
SET NOCOUNT ON;
INSERT INTO dbo.[Shift] (weekScheduleId,
shiftTypeId, shiftDate)
OUTPUT INSERTED.shiftId
VALUES (@weekScheduleId, @shiftTypeId,
@shiftDate);
END
GO
/****** Object: StoredProcedure [dbo].[Insert_WeekSchedule]
Script Date: 11/01/2026 20:27:33 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- 1.2 Insert WeekSchedule (returns new weekScheduleId)
CREATE PROCEDURE [dbo].[Insert_WeekSchedule]
@weekStartDate DATE,
@submissionDeadline DATETIME = NULL,
@statusId INT
AS
BEGIN
SET NOCOUNT ON;
INSERT INTO dbo.WeekSchedule (weekStartDate,
submissionDeadline, statusId)
OUTPUT INSERTED.weekScheduleId
VALUES (@weekStartDate, @submissionDeadline,
@statusId);
END
GO
/****** Object: StoredProcedure [dbo].[InsertEmployee]
Script Date: 11/01/2026 20:27:33 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[InsertEmployee]
@firstName VARCHAR(MAX),
@lastName VARCHAR(MAX),
@phone VARCHAR(20),
@email VARCHAR(100),
@hireDate DATE,
@employmentStatusId INT,
@roleId INT
AS
BEGIN
INSERT INTO dbo.Employee
(firstName, lastName,
phone, email, hireDate, employmentStatusId, roleId)
VALUES
(@firstName, @lastName,
@phone, @email, @hireDate, @employmentStatusId, @roleId);
SELECT SCOPE_IDENTITY() AS employeeId;
END
GO
/****** Object: StoredProcedure
[dbo].[Seed_Lookup_ScheduleStatus] Script Date: 11/01/2026
20:27:33 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/* =========================================================
1) NEW/UPDATED PROCS (to replace inline SQL from C#)
=========================================================
*/
-- 1.1 Seed Lookup_ScheduleStatus (the FK table is in [dbo])
CREATE PROCEDURE [dbo].[Seed_Lookup_ScheduleStatus]
AS
BEGIN
SET NOCOUNT ON;
DECLARE @objId INT =
OBJECT_ID(N'[dbo].[Lookup_ScheduleStatus]');
IF @objId IS NULL
RETURN;
DECLARE @isIdentity BIT =
(SELECT TOP(1)
CONVERT(BIT, is_identity)
FROM sys.columns
WHERE object_id =
@objId AND name = 'statusId');
IF @isIdentity = 1
EXEC(N'SET
IDENTITY_INSERT [dbo].[Lookup_ScheduleStatus] ON;');
IF NOT EXISTS (SELECT 1 FROM
[dbo].[Lookup_ScheduleStatus] WHERE statusId = 1)
INSERT INTO
[dbo].[Lookup_ScheduleStatus] (statusId, [name]) VALUES (1,
'open');
IF NOT EXISTS (SELECT 1 FROM
[dbo].[Lookup_ScheduleStatus] WHERE statusId = 2)
INSERT INTO
[dbo].[Lookup_ScheduleStatus] (statusId, [name]) VALUES (2,
'closed');
IF NOT EXISTS (SELECT 1 FROM
[dbo].[Lookup_ScheduleStatus] WHERE statusId = 3)
INSERT INTO
[dbo].[Lookup_ScheduleStatus] (statusId, [name]) VALUES (3,
'published');
IF NOT EXISTS (SELECT 1 FROM
[dbo].[Lookup_ScheduleStatus] WHERE statusId = 4)
INSERT INTO
[dbo].[Lookup_ScheduleStatus] (statusId, [name]) VALUES (4,
'draft');
IF @isIdentity = 1
EXEC(N'SET
IDENTITY_INSERT [dbo].[Lookup_ScheduleStatus] OFF;');
END
GO
/****** Object: StoredProcedure
[dbo].[SetEmployeeEmploymentStatus] Script Date: 11/01/2026
20:27:33 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- 1.5 Soft delete employee / set employment status
CREATE PROCEDURE [dbo].[SetEmployeeEmploymentStatus]
@employeeId INT,
@employmentStatusId INT
AS
BEGIN
SET NOCOUNT ON;
UPDATE dbo.Employee
SET employmentStatusId = @employmentStatusId
WHERE employeeId = @employeeId;
END
GO
/****** Object: StoredProcedure
[dbo].[SetEmployeeStatusToLeft] Script Date: 11/01/2026 20:27:33
******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[SetEmployeeStatusToLeft]
@employeeId INT
AS
BEGIN
SET NOCOUNT ON;
DECLARE @leftStatusId INT = (
SELECT TOP (1) statusId
FROM
dbo.EmploymentStatus
WHERE UPPER(name) =
'LEFT'
);
UPDATE dbo.Employees
SET employmentStatusId = @leftStatusId
WHERE employeeId = @employeeId;
END
GO
/****** Object: StoredProcedure
[dbo].[Update_EmployeeEmploymentStatus] Script Date: 11/01/2026
20:27:33 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[Update_EmployeeEmploymentStatus]
@employeeId INT,
@employmentStatusId INT
AS
BEGIN
SET NOCOUNT ON;
UPDATE dbo.Employee
SET employmentStatusId = @employmentStatusId
WHERE employeeId = @employeeId;
END
GO
/****** Object: StoredProcedure
[dbo].[Update_EventTaskStatus] Script Date: 11/01/2026 20:27:33
******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[Update_EventTaskStatus]
@taskId INT,
@newStatusId INT
AS
BEGIN
UPDATE dbo.EventTask
SET statusId = @newStatusId
WHERE taskId = @taskId;
END
GO
/****** Object: StoredProcedure
[dbo].[Update_InventoryItemQuantity] Script Date: 11/01/2026
20:27:33 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[Update_InventoryItemQuantity]
@itemId INT,
@newQuantity INT,
@rowsAffected INT OUTPUT
AS
BEGIN
SET NOCOUNT OFF;
-- Make sure the table reference is correct for your DB (use
dbo.InventoryItem)
UPDATE dbo.InventoryItem
SET quantity = @newQuantity
WHERE itemId = @itemId;
-- Return rows affected through the OUTPUT
parameter
SET @rowsAffected = @@ROWCOUNT;
END
GO
/****** Object: StoredProcedure [dbo].[Update_WeekSchedule]
Script Date: 11/01/2026 20:27:33 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- 1.3 Update WeekSchedule
CREATE PROCEDURE [dbo].[Update_WeekSchedule]
@weekScheduleId INT,
@submissionDeadline DATETIME = NULL,
@statusId INT
AS
BEGIN
SET NOCOUNT ON;
UPDATE dbo.WeekSchedule
SET submissionDeadline = @submissionDeadline,
statusId = @statusId
WHERE weekScheduleId = @weekScheduleId;
END
GO
/****** Object: StoredProcedure [dbo].[UpdateEmployee]
Script Date: 11/01/2026 20:27:33 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[UpdateEmployee]
@employeeId INT,
@firstName VARCHAR(MAX),
@lastName VARCHAR(MAX),
@phone VARCHAR(20),
@email VARCHAR(100),
@hireDate DATE,
@employmentStatusId INT,
@roleId INT
AS
BEGIN
UPDATE dbo.Employee
SET firstName = @firstName,
lastName = @lastName,
phone = @phone,
email = @email,
hireDate = @hireDate,
employmentStatusId =
@employmentStatusId,
roleId = @roleId
WHERE employeeId = @employeeId;
END
GO
/****** Object: StoredProcedure [dbo].[UpdateWeekSchedule]
Script Date: 11/01/2026 20:27:33 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[UpdateWeekSchedule]
@weekScheduleId INT,
@submissionDeadline DATETIME = NULL,
@statusId
INT
AS
BEGIN
SET NOCOUNT ON;
DECLARE @target NVARCHAR(300);
DECLARE @hasLastUpdated BIT = 0;
;WITH T AS
(
SELECT
s.name
AS schema_name,
t.name
AS table_name,
t.object_id,
MAX(CASE
WHEN c.name = 'weekScheduleId' THEN 1 ELSE 0 END) AS
hasWeekId,
MAX(CASE
WHEN c.name = 'statusId' THEN 1 ELSE 0
END) AS hasStatus,
MAX(CASE
WHEN c.name = 'weekStartDate' THEN 1 ELSE 0 END) AS
hasWeekStart,
MAX(CASE
WHEN c.name = 'submissionDeadline' THEN 1 ELSE 0 END) AS hasDeadline,
MAX(CASE
WHEN c.name = 'lastUpdated' THEN 1 ELSE 0 END) AS
hasLastUpdated
FROM sys.tables t
JOIN sys.schemas s ON
s.schema_id = t.schema_id
JOIN sys.columns c ON
c.object_id = t.object_id
GROUP BY s.name, t.name,
t.object_id
)
SELECT TOP 1
@target =
QUOTENAME(schema_name) + '.' + QUOTENAME(table_name),
@hasLastUpdated =
CAST(hasLastUpdated AS BIT)
FROM T
WHERE hasWeekId = 1 AND hasStatus = 1 AND
hasDeadline = 1
ORDER BY hasWeekStart DESC, hasLastUpdated DESC,
table_name;
IF @target IS NULL
BEGIN
-- give a clearer error
with discovery hint
THROW 50001, 'Could not
auto-detect WeekSchedule table. No table found with columns: weekScheduleId,
statusId, submissionDeadline.', 1;
END
DECLARE @sql NVARCHAR(MAX) =
N'UPDATE ' + @target +
N'
SET
submissionDeadline = @submissionDeadline,
statusId
= @statusId';
IF @hasLastUpdated = 1
SET @sql += N',
lastUpdated = GETDATE()';
SET @sql += N'
WHERE weekScheduleId =
@weekScheduleId;';
EXEC sp_executesql
@sql,
N'@weekScheduleId INT,
@submissionDeadline DATETIME, @statusId INT',
@weekScheduleId =
@weekScheduleId,
@submissionDeadline =
@submissionDeadline,
@statusId = @statusId;
END
GO