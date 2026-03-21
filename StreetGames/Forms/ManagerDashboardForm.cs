using System;
using System.Windows.Forms;


namespace StreetGames
{
    public partial class ManagerDashboardForm : Form
    {
        public ManagerDashboardForm()
        {
            InitializeComponent();
            UiTheme.ApplyArcade(this);

            // Displays the currently logged-in manager's full name (from the global Program.currentUser)
            lblUser.Text = $"Manager: {Program.currentUser.firstName} {Program.currentUser.lastName}";

            // Ensure the application exits if this dashboard is closed
            this.FormClosed -= ManagerDashboardForm_FormClosed;
            this.FormClosed += ManagerDashboardForm_FormClosed;
        }

        // Opens the employees management screen as a modal dialog
        private void btnManageEmployees_Click(object sender, EventArgs e)
        {
            new ManageEmployeesForm().ShowDialog();
        }

        // Opens the weekly schedule screen as a modal dialog
        private void btnWeekSchedule_Click(object sender, EventArgs e)
        {
            new WeekScheduleForm().ShowDialog();
        }

        // Opens the inventory monitoring screen as a modal dialog
        private void btnInventory_Click(object sender, EventArgs e)
        {
            new InventoryMonitoringForm().ShowDialog();
        }

        // When this dashboard is closed, terminate the application.
        private void ManagerDashboardForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }
    }
}



