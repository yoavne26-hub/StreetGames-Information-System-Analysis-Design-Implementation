using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

namespace StreetGames
{
    public class WeekSchedule
    {
        public int weekScheduleId { get; set; }
        public DateTime weekStartDate { get; set; }
        public DateTime? submissionDeadline { get; set; }
        public int statusId { get; set; }

        public WeekSchedule(int weekScheduleId, DateTime weekStartDate, DateTime? submissionDeadline, int statusId)
        {
            this.weekScheduleId = weekScheduleId;
            this.weekStartDate = weekStartDate;
            this.submissionDeadline = submissionDeadline;
            this.statusId = statusId;
        }

        // -----------------------
        // Stored-proc fallback helpers (moved from form)
        // -----------------------

        private static void ExecuteStoredProcNonQueryWithFallback(string connStr, string[] procedureNames, Action<SqlCommand> addParameters)
        {
            Exception last = null;

            using (var con = new SqlConnection(connStr))
            {
                con.Open();

                foreach (var procName in procedureNames)
                {
                    try
                    {
                        using (var cmd = new SqlCommand(procName, con))
                        {
                            cmd.CommandType = CommandType.StoredProcedure;
                            addParameters?.Invoke(cmd);
                            cmd.ExecuteNonQuery();
                            return; // success
                        }
                    }
                    catch (SqlException ex)
                    {
                        // 2812 = Could not find stored procedure
                        if (ex.Number == 2812)
                        {
                            last = ex;
                            continue;
                        }
                        throw;
                    }
                    catch (Exception ex)
                    {
                        last = ex;
                    }
                }

                throw new Exception(
                    "Stored procedure not found / failed.\n" +
                    $"Tried: {string.Join(", ", procedureNames)}\n" +
                    $"Last error: {last?.Message}", last);
            }
        }

        private static object ExecuteStoredProcScalarWithFallback(string connStr, string[] procedureNames, Action<SqlCommand> addParameters)
        {
            Exception last = null;

            using (var con = new SqlConnection(connStr))
            {
                con.Open();

                foreach (var procName in procedureNames)
                {
                    try
                    {
                        using (var cmd = new SqlCommand(procName, con))
                        {
                            cmd.CommandType = CommandType.StoredProcedure;
                            addParameters?.Invoke(cmd);
                            return cmd.ExecuteScalar();
                        }
                    }
                    catch (SqlException ex)
                    {
                        if (ex.Number == 2812)
                        {
                            last = ex;
                            continue;
                        }
                        throw;
                    }
                    catch (Exception ex)
                    {
                        last = ex;
                    }
                }

                throw new Exception(
                    "Stored procedure not found / failed.\n" +
                    $"Tried: {string.Join(", ", procedureNames)}\n" +
                    $"Last error: {last?.Message}", last);
            }
        }

        // -----------------------
        // High-level model persistence helpers
        // -----------------------

        // Ensures ScheduleStatus lookup table exists/seeded in DB (fallback list).
        public static void SeedScheduleStatusLookup_SP(string connStr)
        {
            ExecuteStoredProcNonQueryWithFallback(
                connStr,
                new[]
                {
                    "dbo.Seed_Lookup_ScheduleStatus",
                    "Seed_Lookup_ScheduleStatus",
                    "dbo.SeedLookupScheduleStatus",
                    "SeedLookupScheduleStatus"
                },
                cmd => { }
            );
        }

        // Inserts this WeekSchedule into DB and returns the new identity id.
        public int InsertToDb(string connStr)
        {
            object idObj = ExecuteStoredProcScalarWithFallback(
                connStr,
                new[]
                {
                    "dbo.InsertWeekSchedule",
                    "InsertWeekSchedule",
                    "dbo.Insert_WeekSchedule",
                    "Insert_WeekSchedule"
                },
                cmd =>
                {
                    cmd.Parameters.AddWithValue("@weekStartDate", this.weekStartDate.Date);
                    cmd.Parameters.AddWithValue("@submissionDeadline",
                        this.submissionDeadline.HasValue ? (object)this.submissionDeadline.Value : DBNull.Value);
                    cmd.Parameters.AddWithValue("@statusId", this.statusId);
                }
            );

            if (idObj == null || idObj == DBNull.Value)
                throw new Exception("InsertWeekSchedule failed: ExecuteScalar returned NULL.");

            return Convert.ToInt32(idObj);
        }

        // Updates this week schedule in DB.
        public void UpdateInDb(string connStr)
        {
            ExecuteStoredProcNonQueryWithFallback(
                connStr,
                new[]
                {
                    "dbo.UpdateWeekSchedule",
                    "UpdateWeekSchedule",
                    "dbo.Update_WeekSchedule",
                    "Update_WeekSchedule"
                },
                cmd =>
                {
                    cmd.Parameters.AddWithValue("@weekScheduleId", this.weekScheduleId);
                    cmd.Parameters.AddWithValue("@submissionDeadline",
                        this.submissionDeadline.HasValue ? (object)this.submissionDeadline.Value : DBNull.Value);
                    cmd.Parameters.AddWithValue("@statusId", this.statusId);
                }
            );
        }

        // Inserts only shifts that are still "new" (negative shiftId) and updates their IDs in memory.
        // This helper uses Program.shifts and Program.shiftParticipations to update in-memory objects.
        public static void InsertNewShiftsForWeekToDb_SP(int weekScheduleId, string connStr)
        {
            var newShifts = Program.shifts
                .Where(s => s.weekScheduleId == weekScheduleId && s.shiftId < 0)
                .ToList();

            if (newShifts.Count == 0)
                return;

            foreach (var sh in newShifts)
            {
                object idObj = ExecuteStoredProcScalarWithFallback(
                    connStr,
                    new[]
                    {
                        "dbo.InsertShift",
                        "InsertShift",
                        "dbo.Insert_Shift",
                        "Insert_Shift"
                    },
                    cmd =>
                    {
                        cmd.Parameters.AddWithValue("@weekScheduleId", sh.weekScheduleId);
                        cmd.Parameters.AddWithValue("@shiftTypeId", sh.shiftTypeId);
                        cmd.Parameters.AddWithValue("@shiftDate", sh.shiftDate.Date);
                    }
                );

                if (idObj == null || idObj == DBNull.Value)
                    throw new Exception("InsertShift failed: ExecuteScalar returned NULL.");

                int newShiftId = Convert.ToInt32(idObj);

                int oldTempId = sh.shiftId;
                sh.shiftId = newShiftId;

                // Re-point any participations that referenced the temp id.
                foreach (var p in Program.shiftParticipations.Where(x => x.shiftId == oldTempId))
                    p.shiftId = newShiftId;
            }
        }
    }
}
