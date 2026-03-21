using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace StreetGames
{
    public partial class ManageEmployeesForm : Form
    {
        private Employee _selected = null;
        private bool _isCreateMode = false;

        private const string PHONE_PLACEHOLDER = "XXX-XXXXXXX";
        private bool _phonePlaceholderActive = false;

        // Compact filter modes (start at Employed and cycle only through these)
        private enum StatusFilterMode
        {
            Employed,
            Candidate,
            Left,
            Rejected
        }

        // Start with Employed so the button initially shows "FILTER: EMPLOYED"
        private StatusFilterMode _filterMode = StatusFilterMode.Employed;

        public ManageEmployeesForm()
        {
            InitializeComponent();
            UiTheme.ApplyArcade(this);

            if (Program.currentUser == null)
            {
                Close();
                return;
            }

            // Roles: populate the role combobox with the allowed EmployeeRole enum values.
            cmbRole.Items.Clear();
            cmbRole.Items.Add(EmployeeRole.cashier);
            cmbRole.Items.Add(EmployeeRole.branchManager);
            cmbRole.Items.Add(EmployeeRole.technician);

            // Status (in-screen combobox): populate employment statuses and set default to candidate.
            cmbStatus.Items.Clear();
            cmbStatus.Items.Add(EmploymentStatus.candidate);
            cmbStatus.Items.Add(EmploymentStatus.employed);
            cmbStatus.Items.Add(EmploymentStatus.rejected);
            cmbStatus.Items.Add(EmploymentStatus.left);
            cmbStatus.SelectedItem = EmploymentStatus.candidate; // default

            // Hook placeholder behavior for the phone textbox.
            txtPhone.Enter += txtPhone_Enter;
            txtPhone.Leave += txtPhone_Leave;

            SetPhonePlaceholder();

            UpdateFilterButtonText();
            LoadGrid();
        }

        private void btnFilterStatus_Click(object sender, EventArgs e)
        {
            ExitCreateMode();
            _selected = null;

            // Cycle through the compact filter sequence only.
            if (_filterMode == StatusFilterMode.Employed) _filterMode = StatusFilterMode.Candidate;
            else if (_filterMode == StatusFilterMode.Candidate) _filterMode = StatusFilterMode.Left;
            else if (_filterMode == StatusFilterMode.Left) _filterMode = StatusFilterMode.Rejected;
            else /* Rejected */ _filterMode = StatusFilterMode.Employed;

            UpdateFilterButtonText();
            ClearEditorInputs();
            SetPhonePlaceholder();
            LoadGrid();
        }

        private void UpdateFilterButtonText()
        {
            if (_filterMode == StatusFilterMode.Employed) btnFilterStatus.Text = "FILTER: EMPLOYED";
            else if (_filterMode == StatusFilterMode.Candidate) btnFilterStatus.Text = "FILTER: CANDIDATE";
            else if (_filterMode == StatusFilterMode.Left) btnFilterStatus.Text = "FILTER: LEFT";
            else if (_filterMode == StatusFilterMode.Rejected) btnFilterStatus.Text = "FILTER: REJECTED";
            else btnFilterStatus.Text = "FILTER";
        }

        private bool MatchesFilter(Employee e)
        {
            if (e == null) return false;

            if (_filterMode == StatusFilterMode.Employed)
                return e.employmentStatusId == (int)EmploymentStatus.employed;

            if (_filterMode == StatusFilterMode.Candidate)
                return e.employmentStatusId == (int)EmploymentStatus.candidate;

            if (_filterMode == StatusFilterMode.Left)
                return e.employmentStatusId == (int)EmploymentStatus.left;

            if (_filterMode == StatusFilterMode.Rejected)
                return e.employmentStatusId == (int)EmploymentStatus.rejected;

            return true;
        }

        private string GetStatusText(int statusId)
        {
            if (Enum.IsDefined(typeof(EmploymentStatus), statusId))
                return ((EmploymentStatus)statusId).ToString();
                else
                return "unknown(" + statusId + ")";
        }

        private string GetRoleText(int roleId)
        {
            // Safely map numeric roleId to the EmployeeRole enum name if it exists.
            if (Enum.IsDefined(typeof(EmployeeRole), roleId))
                return ((EmployeeRole)roleId).ToString();

            return "unknown(" + roleId + ")";
        }

        // ---------------------------
        // Placeholder logic (PHONE)
        // ---------------------------

        private void SetPhonePlaceholder()
        {
            // Show a placeholder text in the phone box when there's no real value.
            _phonePlaceholderActive = true;
            txtPhone.Text = PHONE_PLACEHOLDER;
            txtPhone.ForeColor = Color.Gray;
        }

        private void ClearPhonePlaceholder()
        {
            // Remove placeholder and restore the "active input" color.
            if (_phonePlaceholderActive)
            {
                _phonePlaceholderActive = false;
                txtPhone.Text = "";
                txtPhone.ForeColor = Color.FromArgb(0, 255, 255);
            }
        }

        private void txtPhone_Enter(object sender, EventArgs e)
        {
            // If the user clicks into the phone box while placeholder is active, clear it.
            if (_phonePlaceholderActive)
                ClearPhonePlaceholder();
        }

        private void txtPhone_Leave(object sender, EventArgs e)
        {
            // If user leaves phone box empty, restore placeholder; otherwise keep active color.
            if (string.IsNullOrWhiteSpace(txtPhone.Text))
                SetPhonePlaceholder();
            else
                txtPhone.ForeColor = Color.FromArgb(0, 255, 255);
        }

        private string GetPhoneValueForSave()
        {
            // Prevent saving placeholder text into DB/model.
            if (_phonePlaceholderActive) return "";
            return txtPhone.Text.Trim();
        }

        // ---------------------------
        // "New employee" mode
        // ---------------------------

        private void EnterCreateMode()
        {
            // Switch UI into "create new employee" state and clear current selection.
            _isCreateMode = true;
            _selected = null;

            dgvEmployees.ClearSelection();
            dgvEmployees.CurrentCell = null;

            ClearEditorInputs();
            SetPhonePlaceholder();

            DateTime d = DateTime.Today;
            if (d < dtpHireDate.MinDate) d = dtpHireDate.MinDate;
            if (d > dtpHireDate.MaxDate) d = dtpHireDate.MaxDate;
            dtpHireDate.Value = d;

            // Default status for new employee
            cmbStatus.SelectedItem = EmploymentStatus.candidate;

            btnCreate.Text = "► SAVE NEW ◄";
            txtFirstName.Focus();
        }

        private void ExitCreateMode()
        {
            // Return UI back to normal browsing/editing state.
            _isCreateMode = false;
            btnCreate.Text = "► CREATE ◄";
        }

        private void ClearEditorInputs()
        {
            // Reset all editor fields to defaults.
            txtFirstName.Text = "";
            txtLastName.Text = "";
            txtEmail.Text = "";
            cmbRole.SelectedItem = null;

            // Status default
            cmbStatus.SelectedItem = EmploymentStatus.candidate;

            _phonePlaceholderActive = false;
            txtPhone.Text = "";
            txtPhone.ForeColor = Color.FromArgb(0, 255, 255);

            DateTime d = DateTime.Today;
            if (d < dtpHireDate.MinDate) d = dtpHireDate.MinDate;
            if (d > dtpHireDate.MaxDate) d = dtpHireDate.MaxDate;
            dtpHireDate.Value = d;
        }

        // ---------------------------
        // Grid
        // ---------------------------

        private void LoadGrid(int? keepSelectedEmployeeId = null)
        {
            dgvEmployees.Rows.Clear();

            // Fill the grid from the in-memory employees list, applying the current filter.
            foreach (var e in Program.employees.Where(MatchesFilter))
            {
                dgvEmployees.Rows.Add(
                    e.employeeId,
                    e.firstName,
                    e.lastName,
                    e.email,
                    e.phone,
                    e.hireDate.ToString("dd/MM/yyyy"),
                    GetStatusText(e.employmentStatusId),
                    GetRoleText(e.roleId)
                );
            }

            // Optionally re-select a specific employee row after reload (useful after insert/update).
            if (keepSelectedEmployeeId.HasValue)
            {
                bool found = false;

                foreach (DataGridViewRow row in dgvEmployees.Rows)
                {
                    if (row.Cells[0].Value != null &&
                        Convert.ToInt32(row.Cells[0].Value) == keepSelectedEmployeeId.Value)
                    {
                        row.Selected = true;
                        dgvEmployees.CurrentCell = row.Cells[0];
                        found = true;
                        break;
                    }
                }

                if (!found)
                {
                    dgvEmployees.ClearSelection();
                    dgvEmployees.CurrentCell = null;
                }
            }
        }

        private void dgvEmployees_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvEmployees.SelectedRows.Count == 0) return;

            if (_isCreateMode)
                ExitCreateMode();

            // When selecting a row, load that employee into the editor controls.
            int id = Convert.ToInt32(dgvEmployees.SelectedRows[0].Cells[0].Value);
            _selected = Program.employees.FirstOrDefault(x => x.employeeId == id);
            if (_selected == null) return;

            txtFirstName.Text = _selected.firstName;
            txtLastName.Text = _selected.lastName;
            txtEmail.Text = _selected.email;

            _phonePlaceholderActive = false;
            txtPhone.ForeColor = Color.FromArgb(0, 255, 255);
            txtPhone.Text = _selected.phone ?? "";
            if (string.IsNullOrWhiteSpace(txtPhone.Text))
                SetPhonePlaceholder();

            DateTime d = _selected.hireDate;
            if (d < dtpHireDate.MinDate) d = dtpHireDate.MinDate;
            if (d > dtpHireDate.MaxDate) d = dtpHireDate.MaxDate;
            dtpHireDate.Value = d;

            var roleEnum = (EmployeeRole)_selected.roleId;
            if (cmbRole.Items.Contains(roleEnum))
                cmbRole.SelectedItem = roleEnum;
            else
                cmbRole.SelectedItem = null;

            // Set status in UI
            var statusEnum = (EmploymentStatus)_selected.employmentStatusId;
            if (cmbStatus.Items.Contains(statusEnum))
                cmbStatus.SelectedItem = statusEnum;
            else
                cmbStatus.SelectedItem = EmploymentStatus.candidate;
        }

        // ---------------------------
        // Buttons
        // ---------------------------

        private void btnCreate_Click(object sender, EventArgs e)
        {
            if (!_isCreateMode)
            {
                EnterCreateMode();
                return;
            }

            string fn = txtFirstName.Text.Trim();
            string ln = txtLastName.Text.Trim();
            string rawPhone = GetPhoneValueForSave();
            string em = txtEmail.Text.Trim();
            DateTime hd = dtpHireDate.Value.Date;

            // Basic required-field validation before DB insert.
            if (fn == "" || ln == "" || em == "")
            {
                MessageBox.Show("First name, last name and email are required.", "Validation", MessageBoxButtons.OK);
                return;
            }

            if (!Employee.IsValidEmail(em))
            {
                MessageBox.Show("Email must be valid and contain '@'.", "Validation", MessageBoxButtons.OK);
                return;
            }

            if (cmbRole.SelectedItem == null)
            {
                MessageBox.Show("Please select role.", "Validation", MessageBoxButtons.OK);
                return;
            }

            string phoneFormatted;
            string phoneErr;
            if (!Employee.TryNormalizePhone(rawPhone, out phoneFormatted, out phoneErr))
            {
                MessageBox.Show(phoneErr, "Validation", MessageBoxButtons.OK);
                return;
            }

            int roleId = (int)(EmployeeRole)cmbRole.SelectedItem;

            // ✅ Status from UI (default candidate)
            int statusId = (cmbStatus.SelectedItem == null)
                ? (int)EmploymentStatus.candidate
                : (int)(EmploymentStatus)cmbStatus.SelectedItem;

            try
            {
                SQL_CON sql = new SQL_CON();

                var newEmp = new Employee(0, fn, ln, phoneFormatted, em, hd, statusId, roleId);

                string err;
                int newId = newEmp.Insert(sql, out err);
                if (newId <= 0)
                {
                    MessageBox.Show(err, "DB Error", MessageBoxButtons.OK);
                    return;
                }

                // Keep in-memory list in sync with DB changes.
                Program.employees.Add(newEmp);

                ExitCreateMode();
                LoadGrid(keepSelectedEmployeeId: newId);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "DB Error", MessageBoxButtons.OK);
            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (_selected == null)
            {
                MessageBox.Show("Select an employee to update.", "Validation", MessageBoxButtons.OK);
                return;
            }

            ExitCreateMode();

            string fn = txtFirstName.Text.Trim();
            string ln = txtLastName.Text.Trim();
            string rawPhone = GetPhoneValueForSave();
            string em = txtEmail.Text.Trim();
            DateTime hd = dtpHireDate.Value.Date;

            if (fn == "" || ln == "" || em == "")
            {
                MessageBox.Show("First name, last name and email are required.", "Validation", MessageBoxButtons.OK);
                return;
            }

            if (!Employee.IsValidEmail(em))
            {
                MessageBox.Show("Email must be valid and contain '@'.", "Validation", MessageBoxButtons.OK);
                return;
            }

            string phoneFormatted;
            string phoneErr;
            if (!Employee.TryNormalizePhone(rawPhone, out phoneFormatted, out phoneErr))
            {
                MessageBox.Show(phoneErr, "Validation", MessageBoxButtons.OK);
                return;
            }

            // If role is not explicitly changed in the UI, keep the current one.
            int roleId = _selected.roleId;
            if (cmbRole.SelectedItem != null)
                roleId = (int)(EmployeeRole)cmbRole.SelectedItem;

            // ✅ Status from UI (NO popup)
            int newEmploymentStatusId = (cmbStatus.SelectedItem == null)
                ? _selected.employmentStatusId
                : (int)(EmploymentStatus)cmbStatus.SelectedItem;

            try
            {
                SQL_CON sql = new SQL_CON();

                // Update local object
                _selected.firstName = fn;
                _selected.lastName = ln;
                _selected.phone = phoneFormatted;
                _selected.email = em;
                _selected.hireDate = hd;
                _selected.roleId = roleId;
                _selected.employmentStatusId = newEmploymentStatusId;

                string err;
                if (!_selected.Update(sql, out err))
                {
                    MessageBox.Show(err, "DB Error", MessageBoxButtons.OK);
                    return;
                }

                // If after status change the employee no longer matches the current filter, clear the editor.
                bool stillVisible = MatchesFilter(_selected);
                if (stillVisible)
                {
                    LoadGrid(keepSelectedEmployeeId: _selected.employeeId);
                }
                else
                {
                    _selected = null;
                    LoadGrid(null);
                    ClearEditorInputs();
                    SetPhonePlaceholder();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "DB Error", MessageBoxButtons.OK);
            }
        }

        // Soft Delete = set status LEFT (no success popup)
        private void btnSoftDelete_Click(object sender, EventArgs e)
        {
            if (_selected == null) return;

            ExitCreateMode();

            int newStatus = (int)EmploymentStatus.left;

            try
            {
                SQL_CON sql = new SQL_CON();

                string err;
                if (!_selected.UpdateEmploymentStatus(sql, newStatus, out err))
                {
                    MessageBox.Show(err, "DB Error", MessageBoxButtons.OK);
                    return;
                }

                // Update local object status already done inside UpdateEmploymentStatus.
                bool stillVisible = MatchesFilter(_selected);
                if (stillVisible)
                {
                    LoadGrid(keepSelectedEmployeeId: _selected.employeeId);
                }
                else
                {
                    _selected = null;
                    LoadGrid(null);
                    ClearEditorInputs();
                    SetPhonePlaceholder();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "DB Error", MessageBoxButtons.OK);
            }
        }

        private void dtpHireDate_ValueChanged(object sender, EventArgs e)
        {
        }
    }
}