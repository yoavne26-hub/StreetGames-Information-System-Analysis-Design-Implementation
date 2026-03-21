using System;
using System.Windows.Forms;

namespace StreetGames
{
    public partial class EmployeeDashboardForm : Form
    {
        public EmployeeDashboardForm()
        {
            InitializeComponent();
            UiTheme.ApplyArcade(this);

            // Show the currently logged-in employee's full name on the dashboard header/label.
            lblUser.Text = $"Employee: {Program.currentUser.firstName} {Program.currentUser.lastName}";

            // Ensure the application exits if this dashboard is closed
            this.FormClosed -= EmployeeDashboardForm_FormClosed;
            this.FormClosed += EmployeeDashboardForm_FormClosed;
        }

        private void btnTasks_Click(object sender, EventArgs e)
        {
            // Open the employee tasks screen as a modal dialog (blocks until closed).
            new EmployeeEventTasksForm().ShowDialog();
        }

        private void btnInventory_Click(object sender, EventArgs e)
        {
            // Open the inventory monitoring screen as a modal dialog (blocks until closed).
            new InventoryMonitoringForm().ShowDialog();
        }

        // When this dashboard is closed, terminate the application.
        private void EmployeeDashboardForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }
    }
}



