using System;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
namespace StreetGames
{
    public class Employee
    {
        public int employeeId { get; set; }
        public string firstName { get; set; }
        public string lastName { get; set; }
        public string phone { get; set; }
        public string email { get; set; }
        public DateTime hireDate { get; set; }
        public int employmentStatusId { get; set; }
        public int roleId { get; set; }

        //For showing ints as text
        public string roleName => ((EmployeeRole)roleId).ToString();
        public string statusName => ((EmploymentStatus)employmentStatusId).ToString();


        public Employee(int employeeId, string firstName, string lastName, string phone, string email, DateTime hireDate, int employmentStatusId, int roleId)
        {
            this.employeeId = employeeId;
            this.firstName = firstName;
            this.lastName = lastName;
            this.phone = phone;
            this.email = email;
            this.hireDate = hireDate;
            this.employmentStatusId = employmentStatusId;
            this.roleId = roleId;
        }

        // ---------------------------
        // Validation helpers (moved from form)
        // ---------------------------

        public static bool IsValidEmail(string email)
        {
            if (string.IsNullOrWhiteSpace(email)) return false;
            email = email.Trim();

            if (email.Contains(" ")) return false;
            int at = email.IndexOf('@');
            if (at <= 0 || at >= email.Length - 1) return false;
            int dotAfter = email.IndexOf('.', at + 1);
            if (dotAfter <= at + 1 || dotAfter >= email.Length - 1) return false;

            return true;
        }

        public static bool TryNormalizePhone(string raw, out string formatted, out string error)
        {
            formatted = "";
            error = "";

            if (string.IsNullOrWhiteSpace(raw))
            {
                error = "Phone is required.";
                return false;
            }

            string digits = new string(raw.Where(char.IsDigit).ToArray());

            if (digits.Length != 10)
            {
                error = "Phone must contain exactly 10 digits (e.g. 050-1234567).";
                return false;
            }

            formatted = digits.Substring(0, 3) + "-" + digits.Substring(3, 7);
            return true;
        }

        // ---------------------------
        // Database helpers (moved from form)
        // ---------------------------

        private static void ExecuteStoredProcNonQueryWithFallback(SQL_CON sql, string[] procedureNames, Action<SqlCommand> addParameters)
        {
            Exception last = null;

            foreach (string procName in procedureNames)
            {
                try
                {
                    SqlCommand cmd = new SqlCommand
                    {
                        CommandType = CommandType.StoredProcedure,
                        CommandText = procName
                    };

                    addParameters(cmd);
                    sql.execute_non_query(cmd);
                    return;
                }
                catch (SqlException ex)
                {
                    // 2812 = "Could not find stored procedure"
                    if (ex.Number == 2812)
                    {
                        last = ex;
                        continue;
                    }
                    throw;
                }
                catch (Exception ex2)
                {
                    last = ex2;
                }
            }

            if (last != null) throw last;
            throw new Exception("Could not execute stored procedure (unknown error).");
        }

        // Insert this employee into DB. Returns new id on success, -1 on failure and sets error.
        public int Insert(SQL_CON sql, out string error)
        {
            error = "";
            try
            {
                SqlCommand cmd = new SqlCommand
                {
                    CommandType = CommandType.StoredProcedure,
                    CommandText = "InsertEmployee"
                };

                cmd.Parameters.AddWithValue("@firstName", this.firstName);
                cmd.Parameters.AddWithValue("@lastName", this.lastName);
                cmd.Parameters.AddWithValue("@phone", this.phone ?? "");
                cmd.Parameters.AddWithValue("@email", this.email);
                cmd.Parameters.AddWithValue("@hireDate", this.hireDate);
                cmd.Parameters.AddWithValue("@employmentStatusId", this.employmentStatusId);
                cmd.Parameters.AddWithValue("@roleId", this.roleId);

                object idObj = sql.execute_scalar(cmd);
                if (idObj == null || idObj == DBNull.Value)
                {
                    error = "Insert failed (no id returned).";
                    return -1;
                }

                int newId = Convert.ToInt32(idObj);
                this.employeeId = newId;
                return newId;
            }
            catch (Exception ex)
            {
                error = ex.Message;
                return -1;
            }
        }

        // Update this employee in DB. Returns true on success; error set on failure.
        public bool Update(SQL_CON sql, out string error)
        {
            error = "";
            try
            {
                SqlCommand cmd = new SqlCommand
                {
                    CommandType = CommandType.StoredProcedure,
                    CommandText = "UpdateEmployee"
                };

                cmd.Parameters.AddWithValue("@employeeId", this.employeeId);
                cmd.Parameters.AddWithValue("@firstName", this.firstName);
                cmd.Parameters.AddWithValue("@lastName", this.lastName);
                cmd.Parameters.AddWithValue("@phone", this.phone ?? "");
                cmd.Parameters.AddWithValue("@email", this.email);
                cmd.Parameters.AddWithValue("@hireDate", this.hireDate);
                cmd.Parameters.AddWithValue("@employmentStatusId", this.employmentStatusId);
                cmd.Parameters.AddWithValue("@roleId", this.roleId);

                sql.execute_non_query(cmd);
                return true;
            }
            catch (Exception ex)
            {
                error = ex.Message;
                return false;
            }
        }

        // Update employment status using stored-proc fallback. Returns true on success; error set on failure.
        public bool UpdateEmploymentStatus(SQL_CON sql, int newStatus, out string error)
        {
            error = "";
            try
            {
                ExecuteStoredProcNonQueryWithFallback(
                    sql,
                    new string[] { "Update_EmployeeEmploymentStatus", "UpdateEmployeeEmploymentStatus" },
                    (cmd) =>
                    {
                        cmd.Parameters.AddWithValue("@employeeId", this.employeeId);
                        cmd.Parameters.AddWithValue("@employmentStatusId", newStatus);
                    }
                );

                this.employmentStatusId = newStatus;
                return true;
            }
            catch (Exception ex)
            {
                error = ex.Message;
                return false;
            }
        }
    }
}