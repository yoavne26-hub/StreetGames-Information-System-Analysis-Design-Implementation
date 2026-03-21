using System;
using System.Data;
using System.Data.SqlClient;

namespace StreetGames
{
    public class EventTask
    {
        public int eventTaskId { get; set; }
        public int eventId { get; set; }
        public string description { get; set; }
        public string title { get; set; }
        public TimeSpan dueTime { get; set; }
        public int statusId { get; set; }

        public string statusName =>
            Enum.IsDefined(typeof(TaskStatus), statusId)
                ? ((TaskStatus)statusId).ToString()
                : "Unknown";

        public EventTask(int eventTaskId, int eventId, string description, string title, TimeSpan dueTime, int statusId)
        {
            this.eventTaskId = eventTaskId;
            this.eventId = eventId;
            this.description = description;
            this.title = title;
            this.dueTime = dueTime;
            this.statusId = statusId;
        }

        // Model helpers

        // Returns true when the task is completed.
        public bool IsCompleted()
        {
            return this.statusId == (int)TaskStatus.completed;
        }

        // Persist status change to DB. Returns true on success; error set on failure.
        public bool UpdateStatus(SQL_CON sql, int newStatus, out string error)
        {
            error = "";
            try
            {
                SqlCommand cmd = new SqlCommand
                {
                    CommandType = CommandType.StoredProcedure,
                    CommandText = "Update_EventTaskStatus"
                };

                cmd.Parameters.AddWithValue("@taskId", this.eventTaskId);
                cmd.Parameters.AddWithValue("@newStatusId", newStatus);

                // use silent non-query helper (returns -1 on failure)
                int res = sql.execute_non_query_no_msg(cmd);
                if (res < 0)
                {
                    error = "DB update failed.";
                    return false;
                }

                // update local state after successful DB write
                this.statusId = newStatus;
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
