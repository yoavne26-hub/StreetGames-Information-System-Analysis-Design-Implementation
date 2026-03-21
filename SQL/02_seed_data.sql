SET NOCOUNT ON;
GO
BEGIN TRY
BEGIN TRAN;
/*
============================================================
0) Ensure DynamicForm formId=2 exists (needed for DEFAULT 2)
===========================================================
= */
IF NOT EXISTS (SELECT 1 FROM DynamicForm WHERE formId = 2)
BEGIN
SET IDENTITY_INSERT DynamicForm ON;
INSERT INTO DynamicForm (formId, name, description, statusId, lastUpdated)
VALUES
(2, 'MainForm2', 'Default system form (ID=2)',
(SELECT TOP 1 statusId FROM Lookup_ActivationStatus WHERE name='active'),
GETDATE()
);
SET IDENTITY_INSERT DynamicForm OFF;
END
/*
============================================================
1) Lookup IDs (MUST match your lookup names)
===========================================================
= */
DECLARE @act_active INT = (SELECT TOP 1 statusId FROM Lookup_ActivationStatus
WHERE name='active');
DECLARE @role_cashier INT = (SELECT TOP 1 roleId FROM Lookup_EmployeeRole WHERE name='cashier');
DECLARE @role_manager INT = (SELECT TOP 1 roleId FROM Lookup_EmployeeRole WHERE name='branchManager');
DECLARE @role_tech INT = (SELECT TOP 1 roleId FROM Lookup_EmployeeRole WHERE name='technician');
DECLARE @emp_employed INT = (SELECT TOP 1 statusId FROM Lookup_EmploymentStatus WHERE name='employed');
DECLARE @sch_open INT = (SELECT TOP 1 statusId FROM Lookup_ScheduleStatus WHERE name='open');
DECLARE @sch_closed INT = (SELECT TOP 1 statusId FROM Lookup_ScheduleStatus WHERE name='closed');
DECLARE @av_available INT = (SELECT TOP 1 statusId FROM Lookup_AvailabilityStatus WHERE name='available');
DECLARE @av_unavailable INT = (SELECT TOP 1 statusId FROM Lookup_AvailabilityStatus WHERE name='unavailable');
DECLARE @av_preferNot INT = (SELECT TOP 1 statusId FROM Lookup_AvailabilityStatus WHERE name='preferNot');
DECLARE @as_assigned INT = (SELECT TOP 1 statusId FROM Lookup_AssignmentStatus WHERE name='assigned');
DECLARE @as_notAssigned INT = (SELECT TOP 1 statusId FROM Lookup_AssignmentStatus WHERE name='notAssigned');
DECLARE @sr_submitted INT = (SELECT TOP 1 statusId FROM Lookup_SwapRequestStatus WHERE name='submitted');
DECLARE @sr_pending INT = (SELECT TOP 1 statusId FROM Lookup_SwapRequestStatus WHERE name='pending');
DECLARE @sr_approved INT = (SELECT TOP 1 statusId FROM Lookup_SwapRequestStatus WHERE name='approved');
DECLARE @sr_rejected INT = (SELECT TOP 1 statusId FROM Lookup_SwapRequestStatus WHERE name='rejected');
DECLARE @sr_canceled INT = (SELECT TOP 1 statusId FROM Lookup_SwapRequestStatus WHERE name='canceled');
DECLARE @sr_archived INT = (SELECT TOP 1 statusId FROM Lookup_SwapRequestStatus WHERE name='archived');
DECLARE @sr_revision INT = (SELECT TOP 1 statusId FROM Lookup_SwapRequestStatus WHERE name='revisionRequired');
DECLARE @sr_updated INT = (SELECT TOP 1 statusId FROM Lookup_SwapRequestStatus WHERE name='updated');
DECLARE @card_active INT = (SELECT TOP 1 statusId FROM Lookup_CardStatus WHERE name='active');
DECLARE @card_blocked INT = (SELECT TOP 1 statusId FROM Lookup_CardStatus WHERE name='blocked');
DECLARE @card_expired INT = (SELECT TOP 1 statusId FROM Lookup_CardStatus WHERE name='expired');
/* Event statuses לפי מה שיש אצלך */
DECLARE @ev_requested INT = (SELECT TOP 1 statusId FROM Lookup_EventStatus WHERE name='requested');
DECLARE @ev_scheduled INT = (SELECT TOP 1 statusId FROM Lookup_EventStatus WHERE name='scheduled');
DECLARE @ev_settled INT = (SELECT TOP 1 statusId FROM Lookup_EventStatus WHERE name='settled');
DECLARE @ev_approved INT = (SELECT TOP 1 statusId FROM Lookup_EventStatus WHERE name='approved');
DECLARE @ev_canceled INT = (SELECT TOP 1 statusId FROM Lookup_EventStatus WHERE name='canceled');
DECLARE @task_open INT = (SELECT TOP 1 statusId FROM Lookup_TaskStatus WHERE name='open');
DECLARE @task_inProgress INT = (SELECT TOP 1 statusId FROM Lookup_TaskStatus WHERE name='inProgress');
DECLARE @task_completed INT = (SELECT TOP 1 statusId FROM Lookup_TaskStatus WHERE name='completed');
DECLARE @cat_candy INT = (SELECT TOP 1 categoryId FROM Lookup_ItemCategory WHERE name='candy');
DECLARE @cat_bigToy INT = (SELECT TOP 1 categoryId FROM Lookup_ItemCategory WHERE name='bigToy');
DECLARE @cat_smallToy INT = (SELECT TOP 1 categoryId FROM Lookup_ItemCategory WHERE name='smallToy');
DECLARE @cat_ball INT = (SELECT TOP 1 categoryId FROM Lookup_ItemCategory WHERE name='ball');
/* safety checks (prevents NULL insert into NOT NULL columns) */
IF @act_active IS NULL THROW 50001, 'Missing Lookup_ActivationStatus name=active', 1;
IF @role_cashier IS NULL OR @role_manager IS NULL OR @role_tech IS NULL THROW 50002, 'Missing roles in Lookup_EmployeeRole', 1;
IF @emp_employed IS NULL THROW 50003, 'Missing employmentStatus employed', 1;
IF @sch_open IS NULL OR @sch_closed IS NULL THROW 50004, 'Missing scheduleStatus open/closed', 1;
IF @ev_requested IS NULL OR @ev_scheduled IS NULL OR @ev_settled IS NULL OR @ev_approved IS NULL OR @ev_canceled IS NULL
THROW 50005, 'Missing required event statuses (requested/scheduled/settled/approved/canceled)', 1;
IF @task_open IS NULL OR @task_inProgress IS NULL OR @task_completed IS NULL
THROW 50006, 'Missing task statuses (open/inProgress/completed)', 1;
/* ============================================================
2) Employees (7) - NOT NULL everywhere
============================================================ */
INSERT INTO Employee(firstName,lastName,phone,email,hireDate,employmentStatusId,roleId)
VALUES
רועי','ליניאדו',' ') 4112233-050','roilaniado@gmail.com','2024-04-10',@emp_employed,@role_cashier),
(' מאיה','רז',' 5001122-052','mayaraz@gmail.com','2024-06-01',@emp_employed,@role_cashier),
(' יואב','נשר',' 7603344-054','yoavnesher@gmail.com','2023-11-15',@emp_employed,@role_cashier),
(' אורי','שרף',' 2209988-053','urisharef@gmail.com','2025-01-05',@emp_employed,@role_cashier),
(' משה','צופי',' 9997711-050','moshezofi@gmail.com','2024-09-20',@emp_employed,@role_cashier),
(' דוד','קודיש',' 1234567-050','davidcodish@streetgames.co.il','2022-02-01',@emp_employed,@role_manager),
(' אדיר','אבן',' 7778899-052','adir.even@streetgames.co.il','2023-05-12',@emp_employed,@role_tech);
DECLARE
@e_roi INT = (SELECT employeeId FROM Employee WHERE email='roilaniado@gmail.com'),
@e_maya INT = (SELECT employeeId FROM Employee WHERE email='mayaraz@gmail.com'),
@e_yoav INT = (SELECT employeeId FROM Employee WHERE email='yoavnesher@gmail.com'),
@e_uri INT = (SELECT employeeId FROM Employee WHERE email='urisharef@gmail.com'),
@e_moshe INT= (SELECT employeeId FROM Employee WHERE email='moshezofi@gmail.com'),
@e_david INT= (SELECT employeeId FROM Employee WHERE email='davidcodish@streetgames.co.il'),
@e_adir INT = (SELECT employeeId FROM Employee WHERE email='adir.even@streetgames.co.il');
/* ============================================================
3) ShiftType (8 options) - activeStatusId NOT NULL
============================================================ */
INSERT INTO ShiftType(name,startTime,endTime,requiredEmployees,description,activeStatusId)
VALUES
('Morning_10to16','10:00','16:00',3,'Regular morning',@act_active),
('Morning_12to16','12:00','16:00',2,'Late open variant',@act_active),
('Morning_10to15','10:00','15:00',1,'Short morning',@act_active),
('Evening_16to23','16:00','23:00',2,'Regular evening',@act_active),
('Evening_15to23','15:00','23:00',3,'Early start evening',@act_active),
('Evening_17to23','17:00','23:00',2,'Late start evening',@act_active),
('Holiday_12to18','12:00','18:00',1,'Holiday short day',@act_active),
('Holiday_14to23','14:00','23:00',1,'Holiday late open',@act_active);
DECLARE
@st_m1016 INT = (SELECT shiftTypeId FROM ShiftType WHERE name='Morning_10to16'),
@st_m1216 INT = (SELECT shiftTypeId FROM ShiftType WHERE name='Morning_12to16'),
@st_e1623 INT = (SELECT shiftTypeId FROM ShiftType WHERE name='Evening_16to23'),
@st_e1523 INT = (SELECT shiftTypeId FROM ShiftType WHERE name='Evening_15to23'),
@st_h1218 INT = (SELECT shiftTypeId FROM ShiftType WHERE name='Holiday_12to18');
/* ============================================================
4) WeekSchedule (2) - statusId NOT NULL
============================================================ */
INSERT INTO WeekSchedule(weekStartDate, submissionDeadline, statusId)
VALUES
('2026-01-05', '2026-01-04 18:00:00', @sch_open),
('2025-12-29', '2025-12-28 18:00:00', @sch_closed);
DECLARE
@ws_this INT = (SELECT weekScheduleId FROM WeekSchedule WHERE weekStartDate='2026-01-05'),
@ws_last INT = (SELECT weekScheduleId FROM WeekSchedule WHERE weekStartDate='2025-12-29');
/* ============================================================
5) Shift (10) - shiftDate NOT NULL
============================================================ */
INSERT INTO Shift(weekScheduleId, shiftTypeId, shiftDate)
VALUES
(@ws_last, @st_m1016, '2025-12-29'),
(@ws_last, @st_e1623, '2025-12-29'),
(@ws_last, @st_m1216, '2025-12-30'),
(@ws_last, @st_e1523, '2025-12-30'),
(@ws_last, @st_h1218, '2025-12-31'),
(@ws_last, @st_e1623, '2025-12-31'),
(@ws_this, @st_m1016, '2026-01-05'),
(@ws_this, @st_e1623, '2026-01-05'),
(@ws_this, @st_m1216, '2026-01-06'),
(@ws_this, @st_e1523, '2026-01-06');
DECLARE
@sh1 INT = (SELECT shiftId FROM Shift WHERE weekScheduleId=@ws_last AND shiftDate='2025-12-29' AND shiftTypeId=@st_m1016),
@sh2 INT = (SELECT shiftId FROM Shift WHERE weekScheduleId=@ws_last AND shiftDate='2025-12-29' AND shiftTypeId=@st_e1623),
@sh3 INT = (SELECT shiftId FROM Shift WHERE weekScheduleId=@ws_last AND shiftDate='2025-12-30' AND shiftTypeId=@st_m1216),
@sh7 INT = (SELECT shiftId FROM Shift WHERE weekScheduleId=@ws_this AND shiftDate='2026-01-05' AND shiftTypeId=@st_m1016),
@sh8 INT = (SELECT shiftId FROM Shift WHERE weekScheduleId=@ws_this AND shiftDate='2026-01-05' AND shiftTypeId=@st_e1623);
/* ============================================================
6) ShiftParticipation (10) - many NOT NULL + form_id NOT NULL default 2
============================================================ */
INSERT INTO ShiftParticipation(shiftId, employeeId, availabilityStatusId, assignmentStatusId, submittedAt, assignedAt, form_id)
VALUES
(@sh1, @e_roi, @av_available, @as_assigned, '2025-12-26 10:00:00', '2025-12-27 12:00:00', 2),
(@sh1, @e_maya, @av_available, @as_assigned, '2025-12-26 10:05:00', '2025-12-27 12:01:00', 2),
(@sh2, @e_yoav, @av_available, @as_assigned, '2025-12-26 10:10:00', '2025-12-27 12:05:00', 2),
(@sh2, @e_uri, @av_available, @as_assigned, '2025-12-26 10:12:00', '2025-12-27 12:06:00', 2),
(@sh3, @e_moshe, @av_available, @as_assigned, '2025-12-27 09:00:00', '2025-12-28 11:00:00', 2),
(@sh3, @e_adir, @av_preferNot, @as_notAssigned, '2025-12-27 09:05:00', NULL, 2),
(@sh7, @e_roi, @av_available, @as_assigned, '2026-01-02 10:00:00', '2026-01-03 12:00:00', 2),
(@sh7, @e_maya, @av_available, @as_notAssigned, '2026-01-02 10:02:00', NULL, 2),
(@sh8, @e_yoav, @av_available, @as_assigned, '2026-01-02 10:10:00', '2026-01-03 12:10:00', 2),
(@sh8, @e_uri, @av_unavailable, @as_notAssigned, '2026-01-02 10:12:00', NULL, 2);
DECLARE
@p1 INT = (SELECT participationId FROM ShiftParticipation WHERE shiftId=@sh7 AND employeeId=@e_roi),
@p2 INT = (SELECT participationId FROM ShiftParticipation WHERE shiftId=@sh7 AND employeeId=@e_maya),
@p3 INT = (SELECT participationId FROM ShiftParticipation WHERE shiftId=@sh8 AND employeeId=@e_yoav),
@p4 INT = (SELECT participationId FROM ShiftParticipation WHERE shiftId=@sh8 AND employeeId=@e_uri);
/* ============================================================
7) ShiftSwapRequest (<=10) - only statusId + feedback
============================================================ */
INSERT INTO ShiftSwapRequest(requesterParticipationId, targetParticipationId, statusId, feedback)
VALUES
(@p1, @p2, @sr_pending, NULL),
(@p3, @p4, @sr_approved, 'Approved'),
(@p2, @p1, @sr_rejected, 'Rejected - no coverage'),
(@p1, @p4, @sr_canceled, 'Canceled by requester'),
(@p3, @p2, @sr_archived, 'Archived history');
/* ============================================================
8) Customers (6) - registeredViaFormId NOT NULL default 2
============================================================ */
INSERT INTO Customer(firstName,lastName,phone,email,registeredViaFormId)
VALUES
איציק','גורביץ',' ') 1112233-052','itzik.g@customers.mail',2),
(' הדר','כחלון',' 2223344-052','hadar.k@customers.mail',2),
(' דייגו','קאלפ',' 3334455-052','diego.c@customers.mail',2),
(' סיגל','ברמן',' 4445566-052','sigal.b@customers.mail',2),
(' בועז','גורדון',' 5556677-052','boaz.g@customers.mail',2),
(' אורי','סויסה',' 6667788-052','ori.s@customers.mail',2);
DECLARE
@c_itzik INT = (SELECT customerId FROM Customer WHERE email='itzik.g@customers.mail'),
@c_hadar INT = (SELECT customerId FROM Customer WHERE email='hadar.k@customers.mail'),
@c_diego INT = (SELECT customerId FROM Customer WHERE email='diego.c@customers.mail'),
@c_sigal INT = (SELECT customerId FROM Customer WHERE email='sigal.b@customers.mail'),
@c_boaz INT = (SELECT customerId FROM Customer WHERE email='boaz.g@customers.mail');
/* ============================================================
9) CustomerCard (4)
============================================================ */
INSERT INTO CustomerCard(customerId,cardNumber,issuedAt,expiryDate,balance,statusId)
VALUES
(@c_itzik,'SG-100001','2025-01-10 12:00:00','2030-01-10',120,@card_active),
(@c_hadar,'SG-100002','2025-02-01 12:00:00','2030-02-01', 40,@card_active),
(@c_diego,'SG-100003','2024-12-20 12:00:00','2029-12-20', 10,@card_blocked),
(@c_sigal,'SG-100004','2020-01-01 12:00:00','2025-01-01', 0,@card_expired);
/* ============================================================
10) Event (5) - statusId NOT NULL; eventDate/startTime/duration/participantsCount NOT NULL
============================================================ */
INSERT INTO Event(customerId,responsibleEmployeeId,eventDate,startTime,duration,participantsCount,statusId,notes,askedViaFormId)
VALUES
(@c_itzik, @e_roi, '2026-01-20','19:00',3.0,25,@ev_scheduled,' יום הולדת לקודיש', 2),
(@c_hadar, @e_maya, '2026-01-22','18:30',2.0,18,@ev_approved,' גיבוש לצוות ניתוצ', 2),
(@c_diego, NULL, '2026-01-25','20:00',2.5,30,@ev_requested,' מסיבת מתן בונוס לקבוצה
2 ,' 2),
(@c_sigal, @e_yoav, '2026-02-05','17:00',4.0,40,@ev_scheduled,' מסיבת סיום תואר', 2),
(@c_boaz, @e_roi, '2026-02-10','19:30',3.5,35,@ev_settled,' מסיבת סיום פרויקט', 2);
DECLARE
@ev1 INT = (SELECT TOP 1 eventId FROM Event WHERE notes='יום הולדת לקודיש ' ORDER BY eventId DESC),
@ev2 INT = (SELECT TOP 1 eventId FROM Event WHERE notes='גיבוש לצוות ניתוצ ' ORDER BY eventId DESC),
@ev5 INT = (SELECT TOP 1 eventId FROM Event WHERE notes='מסיבת סיום פרויקט ' ORDER BY eventId DESC);
/* ============================================================
11) EventTask (<=10) - dueTime NOT NULL, statusId NOT NULL
============================================================ */
INSERT INTO EventTask(eventId,title,description,dueTime,statusId)
VALUES
(@ev1,' ניקיון אזור אירוע','סידור וניקוי לפני תחילת האירוע',' 17:30',@task_open),
(@ev1,' ניפוח בלונים','בלונים בצבעים תואמים',' 18:00',@task_inProgress),
(@ev2,' הכנת כרטיסים','כרטיסי שם לצוות',' 17:00',@task_open),
(@ev2,' סידור עמדות משחק','בדיקת תקינות והצבה',' 18:00',@task_open),
(@ev5,' איסוף ציוד אחרי אירוע','איסוף וחזרה למחסן',' 22:30',@task_inProgress),
(@ev5,' סגירת סניף','סגירה מסודרת בסוף האירוע',' 23:00',@task_completed);
/* ============================================================
12) InventoryItem (6)
============================================================ */
INSERT INTO InventoryItem(name,categoryId,quantity,minLevel,expirationDate,statusId,addedViaFormId)
VALUES
(' בובה קטנה - דובי ', @cat_smallToy, 25,10, NULL, @act_active, 2),
('מחזיק מפתחות ', @cat_smallToy, 40,15, NULL, @act_active, 2),
('צעצוע הפתעה ', @cat_smallToy, 12,10, NULL, @act_active, 2),
(' בובה גדולה - דרקון ', @cat_bigToy, 3, 5, NULL, @act_active, 2),
('כדור גומי ', @cat_ball, 18,10, NULL, @act_active, 2),
('ממתק סוכריות ', @cat_candy, 60,20, '2026-03-01', @act_active, 2);
COMMIT TRAN;
SELECT 'SEED OK - adapted to your new schema + your lookup names' AS result;
END TRY
BEGIN CATCH
IF @@TRANCOUNT > 0 ROLLBACK TRAN;
SELECT ERROR_NUMBER() AS ErrorNumber, ERROR_MESSAGE() AS ErrorMessage, ERROR_LINE() AS ErrorLine;
END CATCH;
GO
SELECT 'Employee NULL check' AS chk,
SUM(CASE WHEN phone IS NULL OR email IS NULL OR hireDate IS NULL OR employmentStatusId IS NULL OR roleId IS NULL THEN 1 ELSE 0 END) AS badRows
FROM Employee;
SELECT 'Customer NULL check' AS chk,
SUM(CASE WHEN phone IS NULL OR email IS NULL OR registeredViaFormId IS NULL THEN 1 ELSE 0 END) AS badRows
FROM Customer;
SELECT 'Event NULL check' AS chk,
SUM(CASE WHEN eventDate IS NULL OR startTime IS NULL OR duration IS NULL OR participantsCount IS NULL OR statusId IS NULL THEN 1 ELSE 0 END) AS badRows
FROM Event;
SELECT 'EventTask NULL check' AS chk,
SUM(CASE WHEN dueTime IS NULL OR statusId IS NULL THEN 1 ELSE 0 END) AS badRows
FROM EventTask;
SELECT
r.swapRequestId,
r.requesterParticipationId,
r.targetParticipationId
FROM ShiftSwapRequest r
LEFT JOIN ShiftParticipation a ON a.participationId = r.requesterParticipationId
LEFT JOIN ShiftParticipation b ON b.participationId = r.targetParticipationId
WHERE a.participationId IS NULL OR b.participationId IS NULL;