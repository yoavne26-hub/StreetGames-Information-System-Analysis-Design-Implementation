using System;
using System.Linq;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Windows.Forms;

namespace StreetGames
{
    public partial class EmployeeEventTasksForm : Form
    {
        private EventTask _selected = null;

        public EmployeeEventTasksForm()
        {
            InitializeComponent();
            dgvTasks.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                UiTheme.ApplyArcade(this);

            LoadGrid();
        }

        private string GetTaskStatusText(int statusId)
        {
            // Convert numeric statusId into a readable enum name for the grid.
            if (Enum.IsDefined(typeof(TaskStatus), statusId))
                return ((TaskStatus)statusId).ToString();

            return "unknown(" + statusId + ")";
        }

        // Build a human-friendly event label for display in the grid.
        // Show the event notes when available; otherwise fall back to the numeric eventId.
        private string GetEventDisplayName(int eventId)
        {
            var ev = Program.events.FirstOrDefault(x => x.eventId == eventId);
            if (ev == null) return eventId.ToString();

            if (!string.IsNullOrWhiteSpace(ev.notes))
                return ev.notes;

            return eventId.ToString();
        }

        private void LoadGrid(int? keepTaskId = null)
        {
            dgvTasks.Rows.Clear();

            if (Program.currentUser == null) return;

            // Build a set of eventIds where the current user is the responsible employee.
            HashSet<int> myEventIds = new HashSet<int>(
                Program.events
                    .Where(ev => ev.responsibleEmployeeId.HasValue &&
                                 ev.responsibleEmployeeId.Value == Program.currentUser.employeeId)
                    .Select(ev => ev.eventId)
            );

            // Show only tasks that belong to events assigned to the current user.
            foreach (var t in Program.eventTasks.Where(x => myEventIds.Contains(x.eventId)))
            {
                string eventName = GetEventDisplayName(t.eventId);

                dgvTasks.Rows.Add(
                    t.eventTaskId,
                    eventName,         // now shows notes (if available) or eventId
                    t.title,
                    t.dueTime.ToString(@"hh\:mm"),
                    GetTaskStatusText(t.statusId) // ✅ name instead of id
                );
            }

            // Optionally re-select a task row after reload (useful after update).
            if (keepTaskId.HasValue)
            {
                foreach (DataGridViewRow row in dgvTasks.Rows)
                {
                    if (row.Cells[0].Value != null && Convert.ToInt32(row.Cells[0].Value) == keepTaskId.Value)
                    {
                        row.Selected = true;
                        dgvTasks.CurrentCell = row.Cells[0];
                        break;
                    }
                }
            }
        }

        private void LoadTaskStatus(EventTask task)
        {
            // Populate status combobox, but lock it if the task is already completed.
            cmbStatus.Items.Clear();

            bool isCompleted = task.IsCompleted();

            if (!isCompleted)
            {
                cmbStatus.Items.Add(TaskStatus.open);
                cmbStatus.Items.Add(TaskStatus.inProgress);
                cmbStatus.Items.Add(TaskStatus.completed);

                cmbStatus.Enabled = true;
                btnUpdate.Enabled = true;
            }
            else
            {
                cmbStatus.Items.Add(TaskStatus.completed);
                cmbStatus.SelectedIndex = 0;

                cmbStatus.Enabled = false;
                btnUpdate.Enabled = false;
            }

            // Select the current task status in the combobox (if it exists in the items list).
            TaskStatus st = (TaskStatus)task.statusId;
            if (cmbStatus.Items.Contains(st))
                cmbStatus.SelectedItem = st;
        }

        private void dgvTasks_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvTasks.SelectedRows.Count == 0) return;

            // Track which task is selected and update the UI accordingly.
            int id = Convert.ToInt32(dgvTasks.SelectedRows[0].Cells[0].Value);
            _selected = Program.eventTasks.FirstOrDefault(x => x.eventTaskId == id);
            if (_selected == null) return;

            lblSelected.Text = $"Selected Task: {_selected.eventTaskId} - {_selected.title}";
            LoadTaskStatus(_selected);
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (_selected == null) return;
            if (cmbStatus.SelectedItem == null) return;

            // Prevent changing status if task is already completed.
            if (_selected.IsCompleted())
                return;

            int newStatusId = (int)(TaskStatus)cmbStatus.SelectedItem;

            try
            {
                SQL_CON sql = new SQL_CON();

                string err;
                if (!_selected.UpdateStatus(sql, newStatusId, out err))
                {
                    MessageBox.Show(err, "DB Error", MessageBoxButtons.OK);
                    return;
                }

                // In-memory status already updated by UpdateStatus
                LoadGrid(keepTaskId: _selected.eventTaskId);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "DB Error", MessageBoxButtons.OK);
            }
        }

        private void dgvTasks_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
        }
    }
}



