using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace StreetGames
{
    public class Program
    {
        public static List<Employee> employees = new List<Employee>();
        public static List<ShiftType> shiftTypes = new List<ShiftType>();
        public static List<Customer> customers = new List<Customer>();
        public static List<Event> events = new List<Event>();
        public static List<Shift> shifts = new List<Shift>();
        public static List<EventTask> eventTasks = new List<EventTask>();
        public static List<ShiftParticipation> shiftParticipations = new List<ShiftParticipation>();
        public static List<WeekSchedule> weekSchedules = new List<WeekSchedule>();
        public static List<InventoryItem> inventoryItems = new List<InventoryItem>();
        public static Employee currentUser = null;

        public static void initLists()
        {
            init_Employees();
            init_ShiftTypes();
            init_Customers();
            init_Events();
            init_Shifts();
            init_EventTasks();
            init_ShiftParticipations();
            init_WeekSchedules();
            init_InventoryItems();

        }


        public static void init_Employees()
        {
            employees.Clear();

            SQL_CON sql = new SQL_CON();

            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "EXECUTE dbo.Get_Employees";
            cmd.CommandType = CommandType.Text;

            SqlDataReader rdr = sql.execute_query(cmd);
            if (rdr == null) return;

            while (rdr.Read())
            {
                int employeeId = Convert.ToInt32(rdr.GetValue(0));
                string firstName = rdr.GetValue(1).ToString();
                string lastName = rdr.GetValue(2).ToString();
                string phone = rdr.GetValue(3).ToString();
                string email = rdr.GetValue(4).ToString();
                DateTime hireDate = Convert.ToDateTime(rdr.GetValue(5));
                int employmentStatusId = Convert.ToInt32(rdr.GetValue(6));
                int roleId = Convert.ToInt32(rdr.GetValue(7));

                Employee e = new Employee(employeeId, firstName, lastName, phone, email, hireDate, employmentStatusId, roleId);
                employees.Add(e);
            }

            rdr.Close();
        }



        public static void init_ShiftTypes()
        {
            shiftTypes.Clear();

            SQL_CON sql = new SQL_CON();

            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "EXECUTE dbo.Get_ShiftTypes";
            cmd.CommandType = CommandType.Text;

            SqlDataReader rdr = sql.execute_query(cmd);

            if (rdr == null)
                return;

            while (rdr.Read())
            {

                int shiftTypeId = Convert.ToInt32(rdr.GetValue(0));
                string name = rdr.GetValue(1).ToString();
                TimeSpan startTime = rdr.GetTimeSpan(2);
                TimeSpan endTime = rdr.GetTimeSpan(3);
                int requiredEmployees = Convert.ToInt32(rdr.GetValue(4));
                string description = rdr.IsDBNull(5) ? "no description" : rdr.GetString(5);
                ActivationStatus activationStatus = (ActivationStatus)Convert.ToInt32(rdr.GetValue(6));

                ShiftType st = new ShiftType(shiftTypeId, name, startTime, endTime, requiredEmployees, description);
                shiftTypes.Add(st);
            }

            rdr.Close();
        }

        public static void init_Customers()
        {
            customers.Clear(); // 🔹 same as employees

            SQL_CON sql = new SQL_CON();

            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "EXECUTE dbo.Get_Customers";
            cmd.CommandType = CommandType.Text;

            SqlDataReader rdr = sql.execute_query(cmd);
            if (rdr == null) return;

            while (rdr.Read())
            {
                int customerId = Convert.ToInt32(rdr.GetValue(0));
                string firstName = rdr.GetValue(1).ToString();
                string lastName = rdr.GetValue(2).ToString();
                string phone = rdr.GetValue(3).ToString();
                string email = rdr.GetValue(4).ToString();
                int customerViaFormId = Convert.ToInt32(rdr.GetValue(5));

                Customer c = new Customer(customerId, firstName, lastName, phone, email, customerViaFormId);
                customers.Add(c);
            }

            rdr.Close();
        }


        public static void init_Events()
        {
            try
            {
                events.Clear();
                SQL_CON sql = new SQL_CON();

                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "EXECUTE dbo.Get_Events";
                cmd.CommandType = CommandType.Text;

                SqlDataReader rdr = sql.execute_query(cmd);

                if (rdr == null)
                    return;

                while (rdr.Read())
                {
                    int eventId = Convert.ToInt32(rdr.GetValue(0));
                    int customerId = Convert.ToInt32(rdr.GetValue(1));
                    int? responsibleEmployeeId = rdr.IsDBNull(2) ? (int?)null : rdr.GetInt32(2);
                    DateTime eventDate = Convert.ToDateTime(rdr.GetValue(3));
                    TimeSpan startTime = rdr.GetTimeSpan(4);
                    double duration = Convert.ToDouble(rdr.GetValue(5));
                    int participantsCount = Convert.ToInt32(rdr.GetValue(6));
                    int statusId = Convert.ToInt32(rdr.GetValue(7));
                    string notes = rdr.IsDBNull(8) ? "no notes" : rdr.GetString(8);
                    int? askedViaFormId = rdr.IsDBNull(9) ? (int?)null : Convert.ToInt32(rdr.GetValue(9));

                    Event e = new Event(eventId, customerId, responsibleEmployeeId, eventDate, startTime, duration, participantsCount, statusId, notes, askedViaFormId);
                    events.Add(e);
                }

                rdr.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "init_Events fail" +
                    "" +
                    "" +
                    "" +
                    "" +
                    "" +
                    "" +
                    "ed");
            }
        }



        public static void init_Shifts()
        {

            shifts.Clear();

            SQL_CON sql = new SQL_CON();

            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "EXECUTE dbo.Get_Shifts";
            cmd.CommandType = CommandType.Text;

            SqlDataReader rdr = sql.execute_query(cmd);

            if (rdr == null)
                return;

            while (rdr.Read())
            {

                int shiftId = Convert.ToInt32(rdr.GetValue(0));
                int weekScheduleId = Convert.ToInt32(rdr.GetValue(1));
                int shiftTypeId = Convert.ToInt32(rdr.GetValue(2));
                DateTime shiftDate = Convert.ToDateTime(rdr.GetValue(3));

                Shift s = new Shift(shiftId, weekScheduleId, shiftTypeId, shiftDate);
                shifts.Add(s);
            }

            rdr.Close();
        }

        public static void init_EventTasks()
        {
            eventTasks.Clear();

            SQL_CON sql = new SQL_CON();

            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "EXECUTE dbo.Get_EventTasks";
            cmd.CommandType = CommandType.Text;

            SqlDataReader rdr = sql.execute_query(cmd);

            if (rdr == null)
                return;

            while (rdr.Read())
            {
                int eventTaskId = Convert.ToInt32(rdr.GetValue(0));
                int eventId = Convert.ToInt32(rdr.GetValue(1));
                string title = rdr.GetValue(2).ToString();
                string description = rdr.IsDBNull(3) ? "no description" : rdr.GetString(3);
                TimeSpan dueTime = rdr.GetTimeSpan(4);
                int statusId = Convert.ToInt32(rdr.GetValue(5));

                EventTask et = new EventTask(eventTaskId, eventId, title, description, dueTime, statusId);
                eventTasks.Add(et);
            }

            rdr.Close();
        }

        public static void init_ShiftParticipations()
        {
            SQL_CON sql = new SQL_CON();

            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "EXECUTE dbo.Get_ShiftParticipations";
            cmd.CommandType = CommandType.Text;

            SqlDataReader rdr = sql.execute_query(cmd);

            if (rdr == null)
                return;

            while (rdr.Read())
            {

                int shiftParticipationId = Convert.ToInt32(rdr.GetValue(0));
                int shiftId = Convert.ToInt32(rdr.GetValue(1));
                int employeeId = Convert.ToInt32(rdr.GetValue(2));
                int availabilityStatus = Convert.ToInt32(rdr.GetValue(3));
                int assignmentStatus = Convert.ToInt32(rdr.GetValue(4));
                DateTime submittedAt = Convert.ToDateTime(rdr.GetValue(5));
                DateTime? assignedAt = rdr.IsDBNull(6) ? (DateTime?)DateTime.Today : rdr.GetDateTime(6);
                int formId = Convert.ToInt32(rdr.GetValue(7));

                ShiftParticipation sp = new ShiftParticipation(shiftParticipationId, shiftId, employeeId, availabilityStatus, assignmentStatus, submittedAt, assignedAt, formId);
                shiftParticipations.Add(sp);
            }

            rdr.Close();
        }

        public static void init_WeekSchedules()
        {

            SQL_CON sql = new SQL_CON();

            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "EXECUTE dbo.Get_WeekSchedules";
            cmd.CommandType = CommandType.Text;

            SqlDataReader rdr = sql.execute_query(cmd);

            if (rdr == null)
                return;

            while (rdr.Read())
            {
                int weekScheduleId = Convert.ToInt32(rdr.GetValue(0));
                DateTime weekStartDate = Convert.ToDateTime(rdr.GetValue(1));
                DateTime? rawDeadline = rdr.IsDBNull(2) ? (DateTime?)null : rdr.GetDateTime(2);
                int statusId = Convert.ToInt32(rdr.GetValue(3));
                // Determine whether to replace the deadline
                DateTime weekStartDateOnly = weekStartDate.Date;
                bool replaceDeadline = false;

                if (!rawDeadline.HasValue)
                {
                    replaceDeadline = true;
                }
                else
                {
                    DateTime rdDate = rawDeadline.Value.Date;
                    if (rdDate < weekStartDateOnly || rdDate >= weekStartDateOnly.AddDays(7))
                        replaceDeadline = true;
                }

                DateTime? submissionDeadline;
                if (replaceDeadline)
                {
                    /*
                        - We need to ensure the submissionDeadline is set to the Thursday of the same week as weekStartDate at 18:00
                          whenever rawDeadline is absent or falls outside the week [weekStartDate .. weekStartDate + 6 days].
                        - To compute the Thursday belonging to the week of weekStartDate robustly (in case weekStartDate isn't strictly a Sunday):
                            - Compute daysToThursday = (Thursday - weekStartDate.DayOfWeek + 7) % 7
                              This yields a value in [0..6] mapping the nearest Thursday on-or-after weekStartDate within that week.
                            - thursday = weekStartDate.Date.AddDays(daysToThursday).AddHours(18)
                        - If rawDeadline exists and its Date part is within [weekStartDate.Date .. weekStartDate.Date + 6], keep rawDeadline.
                          Otherwise replace with computed Thursday at 18:00.
                    */
                    int daysToThursday = ((int)DayOfWeek.Thursday - (int)weekStartDateOnly.DayOfWeek + 7) % 7;
                    DateTime thursday = weekStartDateOnly.AddDays(daysToThursday);
                    submissionDeadline = thursday.AddHours(18); // Thursday at 18:00
                }
                else
                {
                    submissionDeadline = rawDeadline;
                }

                WeekSchedule ws = new WeekSchedule(weekScheduleId, weekStartDate, submissionDeadline, statusId);
                weekSchedules.Add(ws);
            }

            rdr.Close();
        }

        public static DateTime GetStartOfWeek(DateTime date)
        {
            int diff = (int)date.DayOfWeek - (int)DayOfWeek.Sunday;
            if (diff < 0) diff += 7;
            return date.Date.AddDays(-diff);
        }

        public static void init_InventoryItems()
        {
            inventoryItems.Clear();

            SQL_CON sql = new SQL_CON();

            SqlCommand cmd = new SqlCommand();
            // NOTE: If you have Get_InventoryItems SP, replace this line with it.
            // Example: cmd.CommandText = "EXECUTE dbo.Get_InventoryItems";
            cmd.CommandText = "EXECUTE dbo.Get_InventoryItems";
            cmd.CommandType = CommandType.Text;

            SqlDataReader rdr = sql.execute_query(cmd);
            if (rdr == null) return;

            while (rdr.Read())
            {
                int itemId = Convert.ToInt32(rdr.GetValue(0));
                string name = rdr.IsDBNull(1) ? "" : rdr.GetString(1);

                int? categoryId = rdr.IsDBNull(2) ? (int?)null : Convert.ToInt32(rdr.GetValue(2));
                int quantity = rdr.IsDBNull(3) ? 0 : Convert.ToInt32(rdr.GetValue(3));
                int minLevel = rdr.IsDBNull(4) ? 0 : Convert.ToInt32(rdr.GetValue(4));

                DateTime? expirationDate = rdr.IsDBNull(5) ? (DateTime?)null : Convert.ToDateTime(rdr.GetValue(5));
                int? statusId = rdr.IsDBNull(6) ? (int?)null : Convert.ToInt32(rdr.GetValue(6));
                int? addedViaFormId = rdr.IsDBNull(7) ? (int?)null : Convert.ToInt32(rdr.GetValue(7));

                InventoryItem it = new InventoryItem(itemId, name, categoryId, quantity, minLevel, expirationDate, statusId, addedViaFormId);
                inventoryItems.Add(it);
            }

            rdr.Close();
        }

        public static InventoryItem FindInventoryItemById(int itemId)
        {
            foreach (var it in inventoryItems)
                if (it.itemId == itemId)
                    return it;
            return null;
        }

        public static bool IsActiveStatusId(int? statusId)
        {
            // TODO: update the numeric mapping once you know it for Lookup_ActivationStatus
            // For now, treat NULL as not active to be safe.
            if (!statusId.HasValue) return false;

            // Example assumption (very common): active=1, notActive=2
            return statusId.Value == 1;
        }

        public static List<InventoryItem> GetActiveInventoryItems()
        {
            List<InventoryItem> res = new List<InventoryItem>();
            foreach (var it in inventoryItems)
                if (IsActiveStatusId(it.statusId))
                    res.Add(it);
            return res;
        }

        public static bool UpdateInventoryQuantity(int itemId, int newQuantity)
        {
            // Basic validation
            if (newQuantity < 0) return false;

            InventoryItem it = FindInventoryItemById(itemId);
            if (it == null) return false;

            // Only ACTIVE items can be updated
            if (!IsActiveStatusId(it.statusId)) return false;

            // Update in-memory first
            it.quantity = newQuantity;

            // Persist to DB
            SQL_CON sql = new SQL_CON();
            SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "UPDATE InventoryItem SET quantity = @q WHERE itemId = @id;";
            cmd.Parameters.AddWithValue("@q", newQuantity);
            cmd.Parameters.AddWithValue("@id", itemId);

            sql.execute_non_query(cmd);
            return true;
        }


        public static bool TryLogin(int employeeId, string email, out string err)
        {
            err = "";
            currentUser = null;

            if (string.IsNullOrWhiteSpace(email))
            {
                err = "Email is required";
                return false;
            }

            var user = employees.FirstOrDefault(e =>
                e.employeeId == employeeId &&
                string.Equals(e.email?.Trim(), email.Trim(), StringComparison.OrdinalIgnoreCase));

            if (user == null)
            {
                err = "Employee not found (check ID/email)";
                return false;
            }

            if (user.employmentStatusId != (int)EmploymentStatus.employed)
            {
                err = "User is not active (not employed)";
                return false;
            }
            currentUser = user;
            return true;
        }

        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Program.initLists();
            Application.Run(new LoginForm());
        }
    }
}
