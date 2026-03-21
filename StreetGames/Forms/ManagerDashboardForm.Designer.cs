namespace StreetGames
{
    partial class ManagerDashboardForm
    {
        private System.ComponentModel.IContainer components = null;

        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Label lblUser;

        private System.Windows.Forms.Button btnManageEmployees;
        private System.Windows.Forms.Button btnWeekSchedule;
        private System.Windows.Forms.Button btnInventory;

        private System.Windows.Forms.Panel pnlFrame;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null)) components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.lblTitle = new System.Windows.Forms.Label();
            this.lblUser = new System.Windows.Forms.Label();
            this.btnManageEmployees = new System.Windows.Forms.Button();
            this.btnWeekSchedule = new System.Windows.Forms.Button();
            this.btnInventory = new System.Windows.Forms.Button();
            this.pnlFrame = new System.Windows.Forms.Panel();
            this.pnlFrame.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("Courier New", 20F, System.Drawing.FontStyle.Bold);
            this.lblTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(0)))));
            this.lblTitle.Location = new System.Drawing.Point(28, 20);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(394, 31);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "★★ MANAGER DASHBOARD ★★";
            // 
            // lblUser
            // 
            this.lblUser.AutoSize = true;
            this.lblUser.Font = new System.Drawing.Font("Courier New", 11F, System.Drawing.FontStyle.Bold);
            this.lblUser.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.lblUser.Location = new System.Drawing.Point(30, 65);
            this.lblUser.Name = "lblUser";
            this.lblUser.Size = new System.Drawing.Size(0, 17);
            this.lblUser.TabIndex = 1;
            // 
            // btnManageEmployees
            // 
            this.btnManageEmployees.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(43)))), ((int)(((byte)(226)))));
            this.btnManageEmployees.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnManageEmployees.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.btnManageEmployees.FlatAppearance.BorderSize = 3;
            this.btnManageEmployees.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnManageEmployees.Font = new System.Drawing.Font("Courier New", 12F, System.Drawing.FontStyle.Bold);
            this.btnManageEmployees.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.btnManageEmployees.Location = new System.Drawing.Point(42, 105);
            this.btnManageEmployees.Name = "btnManageEmployees";
            this.btnManageEmployees.Size = new System.Drawing.Size(320, 50);
            this.btnManageEmployees.TabIndex = 2;
            this.btnManageEmployees.Text = "► MANAGE EMPLOYEES ◄";
            this.btnManageEmployees.UseVisualStyleBackColor = false;
            this.btnManageEmployees.Click += new System.EventHandler(this.btnManageEmployees_Click);
            // 
            // btnWeekSchedule
            // 
            this.btnWeekSchedule.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(10)))), ((int)(((byte)(10)))), ((int)(((byte)(20)))));
            this.btnWeekSchedule.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnWeekSchedule.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(20)))), ((int)(((byte)(147)))));
            this.btnWeekSchedule.FlatAppearance.BorderSize = 3;
            this.btnWeekSchedule.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnWeekSchedule.Font = new System.Drawing.Font("Courier New", 12F, System.Drawing.FontStyle.Bold);
            this.btnWeekSchedule.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(20)))), ((int)(((byte)(147)))));
            this.btnWeekSchedule.Location = new System.Drawing.Point(42, 165);
            this.btnWeekSchedule.Name = "btnWeekSchedule";
            this.btnWeekSchedule.Size = new System.Drawing.Size(320, 50);
            this.btnWeekSchedule.TabIndex = 3;
            this.btnWeekSchedule.Text = "► WEEK SCHEDULING ◄";
            this.btnWeekSchedule.UseVisualStyleBackColor = false;
            this.btnWeekSchedule.Click += new System.EventHandler(this.btnWeekSchedule_Click);
            // 
            // btnInventory
            // 
            this.btnInventory.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(10)))), ((int)(((byte)(10)))), ((int)(((byte)(20)))));
            this.btnInventory.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnInventory.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.btnInventory.FlatAppearance.BorderSize = 3;
            this.btnInventory.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnInventory.Font = new System.Drawing.Font("Courier New", 12F, System.Drawing.FontStyle.Bold);
            this.btnInventory.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.btnInventory.Location = new System.Drawing.Point(42, 225);
            this.btnInventory.Name = "btnInventory";
            this.btnInventory.Size = new System.Drawing.Size(320, 50);
            this.btnInventory.TabIndex = 4;
            this.btnInventory.Text = "► INVENTORY MONITORING ◄";
            this.btnInventory.UseVisualStyleBackColor = false;
            this.btnInventory.Click += new System.EventHandler(this.btnInventory_Click);
            // 
            // pnlFrame
            // 
            this.pnlFrame.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(12)))), ((int)(((byte)(12)))), ((int)(((byte)(24)))));
            this.pnlFrame.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlFrame.Controls.Add(this.lblTitle);
            this.pnlFrame.Controls.Add(this.lblUser);
            this.pnlFrame.Controls.Add(this.btnManageEmployees);
            this.pnlFrame.Controls.Add(this.btnWeekSchedule);
            this.pnlFrame.Controls.Add(this.btnInventory);
            this.pnlFrame.Location = new System.Drawing.Point(28, 12);
            this.pnlFrame.Name = "pnlFrame";
            this.pnlFrame.Size = new System.Drawing.Size(432, 304);
            this.pnlFrame.TabIndex = 0;
            // 
            // ManagerDashboardForm
            // 
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(10)))), ((int)(((byte)(10)))), ((int)(((byte)(20)))));
            this.ClientSize = new System.Drawing.Size(485, 340);
            this.Controls.Add(this.pnlFrame);
            this.Name = "ManagerDashboardForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Street Games - Manager Dashboard";
            this.pnlFrame.ResumeLayout(false);
            this.pnlFrame.PerformLayout();
            this.ResumeLayout(false);

        }
    }
}
