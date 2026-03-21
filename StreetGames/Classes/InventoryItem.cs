using System;
using System.Data;
using System.Data.SqlClient;

namespace StreetGames
{
    public class InventoryItem
    {
        // Represents InventoryItem entity from DB table InventoryItem.
        public int itemId { get; set; }
        public string name { get; set; }
        public int? categoryId { get; set; }
        public int quantity { get; set; }
        public int minLevel { get; set; }
        public DateTime? expirationDate { get; set; }
        public int? statusId { get; set; }
        public int? addedViaFormId { get; set; }

        public InventoryItem(int itemId,
                             string name,
                             int? categoryId,
                             int quantity,
                             int minLevel,
                             DateTime? expirationDate,
                             int? statusId,
                             int? addedViaFormId)
        {
            this.itemId = itemId;
            this.name = name ?? string.Empty;
            this.categoryId = categoryId;
            this.quantity = quantity;
            this.minLevel = minLevel;
            this.expirationDate = expirationDate;
            this.statusId = statusId;
            this.addedViaFormId = addedViaFormId;
        }

        public override string ToString()
        {
            // Helpful for debugging / list controls.
            return $"{itemId} - {name} (qty={quantity})";
        }

        // Returns the alert text for this item given thresholds (mirrors previous form logic).
        public string GetAlertText(double nearMinThresholdRatio, int expiryNearDays)
        {
            string alert = "";

            if (this.minLevel > 0)
            {
                if (this.quantity < this.minLevel)
                    alert += "BELOW MIN  ";
                else if (this.quantity <= (int)Math.Ceiling(this.minLevel * nearMinThresholdRatio))
                    alert += "NEAR MIN  ";
            }

            if (this.expirationDate.HasValue)
            {
                var days = (this.expirationDate.Value.Date - DateTime.Today.Date).TotalDays;

                if (days < 0)
                    alert += "EXPIRED  ";
                else if (days <= expiryNearDays)
                    alert += "EXPIRY SOON  ";
            }

            return alert.Trim();
        }

        // Convenience to check if this item is active.
        public bool IsActive()
        {
            return this.statusId.HasValue && this.statusId.Value == (int)ActivationStatus.active;
        }

        // Persist a quantity change to DB. Uses SQL_CON helpers; returns true on success and sets error message on failure.
        public bool UpdateQuantity(SQL_CON sql, int newQuantity, out string error)
        {
            error = "";
            try
            {
                SqlCommand cmd = new SqlCommand
                {
                    CommandType = CommandType.StoredProcedure,
                    CommandText = "Update_InventoryItemQuantity"
                };

                cmd.Parameters.AddWithValue("@itemId", this.itemId);
                cmd.Parameters.AddWithValue("@newQuantity", newQuantity);

                var outParam = new SqlParameter("@rowsAffected", System.Data.SqlDbType.Int)
                {
                    Direction = System.Data.ParameterDirection.Output
                };
                cmd.Parameters.Add(outParam);

                // Execute stored-proc (silent). execRes is ExecuteNonQuery() return (may be -1 if NOCOUNT or other).
                int execRes = sql.execute_non_query_no_msg(cmd);

                // Try read output param first (recommended if SP was updated to set it)
                object val = cmd.Parameters["@rowsAffected"].Value;
                int rows = (val == null || val == DBNull.Value) ? int.MinValue : Convert.ToInt32(val);

                if (rows > 0)
                {
                    this.quantity = newQuantity;
                    return true;
                }

                if (rows == 0)
                {
                    error = "DB update executed but no rows were affected (item not found or WHERE didn't match).";
                    return false;
                }

                // rows == int.MinValue -> output param not set / unknown. Fall back to verification SELECT.
                // Also tolerate execRes > 0 as success when OUTPUT isn't available.
                if (execRes > 0)
                {
                    this.quantity = newQuantity;
                    return true;
                }

                // Verify by reading the quantity directly from the DB (reliable regardless of NOCOUNT).
                using (var sel = new SqlCommand("SELECT quantity FROM dbo.InventoryItem WHERE itemId = @itemId"))
                {
                    sel.Parameters.AddWithValue("@itemId", this.itemId);
                    using (var rdr = sql.execute_query(sel))
                    {
                        if (rdr == null)
                        {
                            error = "Could not verify DB row after update (reader failure).";
                            return false;
                        }

                        if (!rdr.Read())
                        {
                            error = "Item not found in DB after stored-proc execution.";
                            return false;
                        }

                        int dbQty = Convert.ToInt32(rdr.GetValue(0));
                        if (dbQty == newQuantity)
                        {
                            this.quantity = newQuantity;
                            return true;
                        }

                        error = $"DB executed but quantity is still {dbQty} (expected {newQuantity}).";
                        return false;
                    }
                }
            }
            catch (Exception ex)
            {
                error = ex.Message;
                return false;
            }
        }
    }
}