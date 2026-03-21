using System;
using System.Linq;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;
using System.Drawing;

namespace StreetGames
{
    public partial class InventoryMonitoringForm : Form
    {
        private const int COL_ID = 0;
        private const int COL_ALERT = 6;

        // Thresholds used to generate alerts (near-min and expiry-soon)
        private const double NearMinThresholdRatio = 1.2;
        private const int ExpiryNearDays = 7;

        // Holds the currently selected inventory item from the grid
        private InventoryItem _selected = null;

        public InventoryMonitoringForm()
        {
            InitializeComponent();
            UiTheme.ApplyArcade(this);
            LoadGrid();
        }

        // Loads the DataGridView from Program.inventoryItems (in-memory) and optionally reselects an item by id
        private void LoadGrid(int? keepSelectedId = null)
        {
            dgvInv.Rows.Clear();

            foreach (var it in Program.inventoryItems)
            {
                // Build alert string based on quantity/min-level and expiration date
                string alert = it.GetAlertText(NearMinThresholdRatio, ExpiryNearDays);

                dgvInv.Rows.Add(
                    it.itemId,
                    it.name,
                    it.quantity,
                    it.minLevel,
                    it.expirationDate.HasValue ? it.expirationDate.Value.ToString("dd/MM/yyyy") : "",
                    it.statusId.HasValue && Enum.IsDefined(typeof(ActivationStatus), it.statusId.Value)
                        ? ((ActivationStatus)it.statusId.Value).ToString()
                        : "",
                    alert
                );
            }

            ApplyRowStyles();
            ReselectRow(keepSelectedId);
        }

        // Re-selects a row in the grid by itemId and scrolls it into view
        private void ReselectRow(int? itemId)
        {
            if (!itemId.HasValue) return;

            foreach (DataGridViewRow row in dgvInv.Rows)
            {
                // Use the numeric column index (COL_ID) instead of a string name that may not exist.
                var cell = row.Cells[COL_ID];
                if (cell == null || cell.Value == null) continue;

                int id;
                if (!int.TryParse(Convert.ToString(cell.Value), out id)) continue;

                if (id == itemId.Value)
                {
                    row.Selected = true;
                    if (row.Index >= 0)
                        dgvInv.FirstDisplayedScrollingRowIndex = row.Index;
                    return;
                }
            }
        }

        // Applies background colors to rows according to the alert text (visual highlighting)
        private void ApplyRowStyles()
        {
            foreach (DataGridViewRow row in dgvInv.Rows)
            {
                row.DefaultCellStyle.BackColor = dgvInv.DefaultCellStyle.BackColor;

                string alert = (row.Cells[COL_ALERT].Value ?? "").ToString();

                if (alert.Contains("BELOW MIN"))
                    row.DefaultCellStyle.BackColor = Color.FromArgb(160, 30, 30);
                else if (alert.Contains("NEAR MIN"))
                    row.DefaultCellStyle.BackColor = Color.FromArgb(160, 110, 30);
                else if (alert.Contains("EXPIRED"))
                    row.DefaultCellStyle.BackColor = Color.FromArgb(120, 20, 20);
                else if (alert.Contains("EXPIRY"))
                    row.DefaultCellStyle.BackColor = Color.FromArgb(120, 60, 170);
            }
        }

        // When the selection changes, sync UI controls to the selected item and enforce "active only" editing
        private void dgvInv_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvInv.SelectedRows.Count == 0) return;

            int id = Convert.ToInt32(dgvInv.SelectedRows[0].Cells[COL_ID].Value);

            _selected = Program.inventoryItems.FirstOrDefault(x => x.itemId == id);
            if (_selected == null) return;

            txtItemName.Text = _selected.name;

            // Clamp quantity into NumericUpDown bounds before assigning Value
            int qty = _selected.quantity;
            if (qty < (int)numQuantity.Minimum) qty = (int)numQuantity.Minimum;
            if (qty > (int)numQuantity.Maximum) qty = (int)numQuantity.Maximum;
            numQuantity.Value = qty;

            bool isActive = _selected.IsActive();

            // Only active items can be updated
            numQuantity.Enabled = isActive;
            btnSetQuantity.Enabled = isActive;

            lblHint.Text = isActive ? "" : "Only ACTIVE items can be updated.";
        }

        // Persists the quantity change to DB via model method and then refreshes the grid
        private void btnSetQuantity_Click(object sender, EventArgs e)
        {
            if (_selected == null) return;

            if (!_selected.IsActive()) return;

            int newQty = (int)numQuantity.Value;

            try
            {
                SQL_CON sql = new SQL_CON();

                string err;
                if (!_selected.UpdateQuantity(sql, newQty, out err))
                {
                    lblHint.Text = "DB Error: " + err;
                    return;
                }

                // In-memory object already updated by UpdateQuantity
                LoadGrid(_selected.itemId);
                lblHint.Text = "Quantity updated.";
            }
            catch (Exception ex)
            {
                lblHint.Text = "DB Error: " + ex.Message;
            }
        }

        private void InventoryMonitoringForm_Load(object sender, EventArgs e)
        {
                
        }
    }
}




