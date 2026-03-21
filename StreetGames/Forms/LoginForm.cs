using System;
using System.Windows.Forms;

namespace StreetGames
{
    public partial class LoginForm : Form
    {
        public LoginForm()
        {
            InitializeComponent();
            UiTheme.ApplyArcade(this); // Apply the app's arcade UI theme to this form.
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            lblMessage.Text = ""; // Clear any previous error/status message.

            // Validate that the Employee ID is a valid integer (basic input validation).
            if (!int.TryParse(txtEmployeeId.Text.Trim(), out int id))
            {
                lblMessage.Text = "Invalid employee number";
                return;
            }

            string email = txtEmail.Text.Trim(); // Read email input (trim spaces).

            // Attempt to login via a central method. On failure, show the returned error message.
            if (!Program.TryLogin(id, email, out string err))
            {
                lblMessage.Text = err;
                return;
            }

            // Route by role.
            if (Program.currentUser.roleId == (int)EmployeeRole.branchManager)
            {
                Hide(); // Hide login form before opening the next screen.
                new ManagerDashboardForm().Show(); // Open manager dashboard for branch managers.
            }
            else if (Program.currentUser.roleId == (int)EmployeeRole.technician)
            {
                // Technicians are not allowed to access the employee system (events/tasks/inventory screens).
                lblMessage.Text = "Access denied: you do not have permission to access the employee system.";
                return;
            }
            else
            {
                Hide(); // Hide login form before opening the next screen.
                new EmployeeDashboardForm().Show(); // Open employee dashboard for non-managers.
            }
        }
    }
}



