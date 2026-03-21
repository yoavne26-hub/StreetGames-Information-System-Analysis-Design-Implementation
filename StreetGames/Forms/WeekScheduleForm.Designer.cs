using System;
using System.Drawing;
using System.Windows.Forms;

namespace StreetGames
{
    partial class WeekScheduleForm
    {
        private System.ComponentModel.IContainer components = null;

        private Label lblTitle;
        private Label lblWeek;
        private Label lblMode;

        private Button btnPrevWeek;
        private Button btnNextWeek;

        private Label lblDeadline;
        private DateTimePicker dtpDeadline;

        private Label lblStatus;
        private ComboBox cmbWeekStatus;

        private Button btnCreateOrUpdate;

        private ListBox lstShiftTypes;

        private Panel pnlSun;
        private Panel pnlMon;
        private Panel pnlTue;
        private Panel pnlWed;
        private Panel pnlThu;
        private Panel pnlFri;
        private Panel pnlSat;

        private Label lblSun;
        private Label lblMon;
        private Label lblTue;
        private Label lblWed;
        private Label lblThu;
        private Label lblFri;
        private Label lblSat;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null)) components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.lblTitle = new System.Windows.Forms.Label();
            this.lblWeek = new System.Windows.Forms.Label();
            this.lblMode = new System.Windows.Forms.Label();
            this.btnPrevWeek = new System.Windows.Forms.Button();
            this.btnNextWeek = new System.Windows.Forms.Button();
            this.lblDeadline = new System.Windows.Forms.Label();
            this.dtpDeadline = new System.Windows.Forms.DateTimePicker();
            this.lblStatus = new System.Windows.Forms.Label();
            this.cmbWeekStatus = new System.Windows.Forms.ComboBox();
            this.btnCreateOrUpdate = new System.Windows.Forms.Button();
            this.lstShiftTypes = new System.Windows.Forms.ListBox();
            this.pnlSun = new System.Windows.Forms.Panel();
            this.pnlMon = new System.Windows.Forms.Panel();
            this.pnlTue = new System.Windows.Forms.Panel();
            this.pnlWed = new System.Windows.Forms.Panel();
            this.pnlThu = new System.Windows.Forms.Panel();
            this.pnlFri = new System.Windows.Forms.Panel();
            this.pnlSat = new System.Windows.Forms.Panel();
            this.lblSun = new System.Windows.Forms.Label();
            this.lblMon = new System.Windows.Forms.Label();
            this.lblTue = new System.Windows.Forms.Label();
            this.lblWed = new System.Windows.Forms.Label();
            this.lblThu = new System.Windows.Forms.Label();
            this.lblFri = new System.Windows.Forms.Label();
            this.lblSat = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("Courier New", 22F, System.Drawing.FontStyle.Bold);
            this.lblTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(20)))), ((int)(((byte)(147)))));
            this.lblTitle.Location = new System.Drawing.Point(20, 15);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(429, 34);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "=== WEEK SCHEDULING ===";
            // 
            // lblWeek
            // 
            this.lblWeek.AutoSize = true;
            this.lblWeek.Font = new System.Drawing.Font("Courier New", 11F, System.Drawing.FontStyle.Bold);
            this.lblWeek.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.lblWeek.Location = new System.Drawing.Point(20, 60);
            this.lblWeek.Name = "lblWeek";
            this.lblWeek.Size = new System.Drawing.Size(62, 17);
            this.lblWeek.TabIndex = 1;
            this.lblWeek.Text = "WEEK: ";
            // 
            // lblMode
            // 
            this.lblMode.AutoSize = true;
            this.lblMode.Font = new System.Drawing.Font("Courier New", 10F, System.Drawing.FontStyle.Bold);
            this.lblMode.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(57)))), ((int)(((byte)(255)))), ((int)(((byte)(20)))));
            this.lblMode.Location = new System.Drawing.Point(20, 90);
            this.lblMode.Name = "lblMode";
            this.lblMode.Size = new System.Drawing.Size(55, 16);
            this.lblMode.TabIndex = 2;
            this.lblMode.Text = "MODE: ";
            // 
            // btnPrevWeek
            // 
            this.btnPrevWeek.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(43)))), ((int)(((byte)(226)))));
            this.btnPrevWeek.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnPrevWeek.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.btnPrevWeek.FlatAppearance.BorderSize = 2;
            this.btnPrevWeek.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnPrevWeek.Font = new System.Drawing.Font("Courier New", 14F, System.Drawing.FontStyle.Bold);
            this.btnPrevWeek.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.btnPrevWeek.Location = new System.Drawing.Point(420, 55);
            this.btnPrevWeek.Name = "btnPrevWeek";
            this.btnPrevWeek.Size = new System.Drawing.Size(50, 40);
            this.btnPrevWeek.TabIndex = 3;
            this.btnPrevWeek.Text = "◄";
            this.btnPrevWeek.UseVisualStyleBackColor = false;
            this.btnPrevWeek.Click += new System.EventHandler(this.btnPrevWeek_Click);
            // 
            // btnNextWeek
            // 
            this.btnNextWeek.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(43)))), ((int)(((byte)(226)))));
            this.btnNextWeek.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnNextWeek.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.btnNextWeek.FlatAppearance.BorderSize = 2;
            this.btnNextWeek.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnNextWeek.Font = new System.Drawing.Font("Courier New", 14F, System.Drawing.FontStyle.Bold);
            this.btnNextWeek.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.btnNextWeek.Location = new System.Drawing.Point(480, 55);
            this.btnNextWeek.Name = "btnNextWeek";
            this.btnNextWeek.Size = new System.Drawing.Size(50, 40);
            this.btnNextWeek.TabIndex = 4;
            this.btnNextWeek.Text = "►";
            this.btnNextWeek.UseVisualStyleBackColor = false;
            this.btnNextWeek.Click += new System.EventHandler(this.btnNextWeek_Click);
            // 
            // lblDeadline
            // 
            this.lblDeadline.AutoSize = true;
            this.lblDeadline.Font = new System.Drawing.Font("Courier New", 10F, System.Drawing.FontStyle.Bold);
            this.lblDeadline.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(57)))), ((int)(((byte)(255)))), ((int)(((byte)(20)))));
            this.lblDeadline.Location = new System.Drawing.Point(560, 65);
            this.lblDeadline.Name = "lblDeadline";
            this.lblDeadline.Size = new System.Drawing.Size(71, 16);
            this.lblDeadline.TabIndex = 5;
            this.lblDeadline.Text = "DEADLINE";
            // 
            // dtpDeadline
            // 
            this.dtpDeadline.CalendarForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.dtpDeadline.CalendarMonthBackground = System.Drawing.Color.FromArgb(((int)(((byte)(10)))), ((int)(((byte)(10)))), ((int)(((byte)(20)))));
            this.dtpDeadline.CustomFormat = "dd/MM/yyyy HH:mm";
            this.dtpDeadline.Font = new System.Drawing.Font("Courier New", 9F);
            this.dtpDeadline.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpDeadline.Location = new System.Drawing.Point(650, 63);
            this.dtpDeadline.Name = "dtpDeadline";
            this.dtpDeadline.ShowCheckBox = true;
            this.dtpDeadline.ShowUpDown = true;
            this.dtpDeadline.Size = new System.Drawing.Size(220, 21);
            this.dtpDeadline.TabIndex = 6;
            // 
            // lblStatus
            // 
            this.lblStatus.AutoSize = true;
            this.lblStatus.Font = new System.Drawing.Font("Courier New", 10F, System.Drawing.FontStyle.Bold);
            this.lblStatus.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(57)))), ((int)(((byte)(255)))), ((int)(((byte)(20)))));
            this.lblStatus.Location = new System.Drawing.Point(885, 65);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(55, 16);
            this.lblStatus.TabIndex = 7;
            this.lblStatus.Text = "STATUS";
            // 
            // cmbWeekStatus
            // 
            this.cmbWeekStatus.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(10)))), ((int)(((byte)(10)))), ((int)(((byte)(20)))));
            this.cmbWeekStatus.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbWeekStatus.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmbWeekStatus.Font = new System.Drawing.Font("Courier New", 9F);
            this.cmbWeekStatus.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.cmbWeekStatus.Location = new System.Drawing.Point(955, 63);
            this.cmbWeekStatus.Name = "cmbWeekStatus";
            this.cmbWeekStatus.Size = new System.Drawing.Size(130, 23);
            this.cmbWeekStatus.TabIndex = 8;
            // 
            // btnCreateOrUpdate
            // 
            this.btnCreateOrUpdate.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(20)))), ((int)(((byte)(147)))));
            this.btnCreateOrUpdate.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnCreateOrUpdate.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.btnCreateOrUpdate.FlatAppearance.BorderSize = 3;
            this.btnCreateOrUpdate.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCreateOrUpdate.Font = new System.Drawing.Font("Courier New", 10F, System.Drawing.FontStyle.Bold);
            this.btnCreateOrUpdate.ForeColor = System.Drawing.Color.White;
            this.btnCreateOrUpdate.Location = new System.Drawing.Point(1100, 55);
            this.btnCreateOrUpdate.Name = "btnCreateOrUpdate";
            this.btnCreateOrUpdate.Size = new System.Drawing.Size(150, 40);
            this.btnCreateOrUpdate.TabIndex = 9;
            this.btnCreateOrUpdate.Text = "▶ CREATE WEEK ◀";
            this.btnCreateOrUpdate.UseVisualStyleBackColor = false;
            this.btnCreateOrUpdate.Click += new System.EventHandler(this.btnCreateOrUpdate_Click);
            // 
            // lstShiftTypes
            // 
            this.lstShiftTypes.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(10)))), ((int)(((byte)(10)))), ((int)(((byte)(20)))));
            this.lstShiftTypes.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lstShiftTypes.Font = new System.Drawing.Font("Courier New", 9F);
            this.lstShiftTypes.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.lstShiftTypes.ItemHeight = 15;
            this.lstShiftTypes.Location = new System.Drawing.Point(20, 130);
            this.lstShiftTypes.Name = "lstShiftTypes";
            this.lstShiftTypes.Size = new System.Drawing.Size(260, 407);
            this.lstShiftTypes.TabIndex = 10;
            this.lstShiftTypes.MouseDown += new System.Windows.Forms.MouseEventHandler(this.lstShiftTypes_MouseDown);
            // 
            // pnlSun
            // 
            this.pnlSun.Location = new System.Drawing.Point(304, 126);
            this.pnlSun.Name = "pnlSun";
            this.pnlSun.Size = new System.Drawing.Size(129, 100);
            this.pnlSun.TabIndex = 11;
            // 
            // pnlMon
            // 
            this.pnlMon.Location = new System.Drawing.Point(439, 126);
            this.pnlMon.Name = "pnlMon";
            this.pnlMon.Size = new System.Drawing.Size(129, 100);
            this.pnlMon.TabIndex = 12;
            // 
            // pnlTue
            // 
            this.pnlTue.Location = new System.Drawing.Point(573, 126);
            this.pnlTue.Name = "pnlTue";
            this.pnlTue.Size = new System.Drawing.Size(129, 100);
            this.pnlTue.TabIndex = 13;
            // 
            // pnlWed
            // 
            this.pnlWed.Location = new System.Drawing.Point(708, 126);
            this.pnlWed.Name = "pnlWed";
            this.pnlWed.Size = new System.Drawing.Size(129, 100);
            this.pnlWed.TabIndex = 14;
            // 
            // pnlThu
            // 
            this.pnlThu.Location = new System.Drawing.Point(843, 126);
            this.pnlThu.Name = "pnlThu";
            this.pnlThu.Size = new System.Drawing.Size(129, 100);
            this.pnlThu.TabIndex = 15;
            // 
            // pnlFri
            // 
            this.pnlFri.Location = new System.Drawing.Point(978, 126);
            this.pnlFri.Name = "pnlFri";
            this.pnlFri.Size = new System.Drawing.Size(129, 100);
            this.pnlFri.TabIndex = 16;
            // 
            // pnlSat
            // 
            this.pnlSat.Location = new System.Drawing.Point(1113, 126);
            this.pnlSat.Name = "pnlSat";
            this.pnlSat.Size = new System.Drawing.Size(129, 100);
            this.pnlSat.TabIndex = 17;
            // 
            // lblSun
            // 
            this.lblSun.Location = new System.Drawing.Point(0, 0);
            this.lblSun.Name = "lblSun";
            this.lblSun.Size = new System.Drawing.Size(100, 23);
            this.lblSun.TabIndex = 0;
            // 
            // lblMon
            // 
            this.lblMon.Location = new System.Drawing.Point(0, 0);
            this.lblMon.Name = "lblMon";
            this.lblMon.Size = new System.Drawing.Size(100, 23);
            this.lblMon.TabIndex = 0;
            // 
            // lblTue
            // 
            this.lblTue.Location = new System.Drawing.Point(0, 0);
            this.lblTue.Name = "lblTue";
            this.lblTue.Size = new System.Drawing.Size(100, 23);
            this.lblTue.TabIndex = 0;
            // 
            // lblWed
            // 
            this.lblWed.Location = new System.Drawing.Point(0, 0);
            this.lblWed.Name = "lblWed";
            this.lblWed.Size = new System.Drawing.Size(100, 23);
            this.lblWed.TabIndex = 0;
            // 
            // lblThu
            // 
            this.lblThu.Location = new System.Drawing.Point(0, 0);
            this.lblThu.Name = "lblThu";
            this.lblThu.Size = new System.Drawing.Size(100, 23);
            this.lblThu.TabIndex = 0;
            // 
            // lblFri
            // 
            this.lblFri.Location = new System.Drawing.Point(0, 0);
            this.lblFri.Name = "lblFri";
            this.lblFri.Size = new System.Drawing.Size(100, 23);
            this.lblFri.TabIndex = 0;
            // 
            // lblSat
            // 
            this.lblSat.Location = new System.Drawing.Point(0, 0);
            this.lblSat.Name = "lblSat";
            this.lblSat.Size = new System.Drawing.Size(100, 23);
            this.lblSat.TabIndex = 0;
            // 
            // WeekScheduleForm
            // 
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(10)))), ((int)(((byte)(10)))), ((int)(((byte)(20)))));
            this.ClientSize = new System.Drawing.Size(1270, 600);
            this.Controls.Add(this.lblTitle);
            this.Controls.Add(this.lblWeek);
            this.Controls.Add(this.lblMode);
            this.Controls.Add(this.btnPrevWeek);
            this.Controls.Add(this.btnNextWeek);
            this.Controls.Add(this.lblDeadline);
            this.Controls.Add(this.dtpDeadline);
            this.Controls.Add(this.lblStatus);
            this.Controls.Add(this.cmbWeekStatus);
            this.Controls.Add(this.btnCreateOrUpdate);
            this.Controls.Add(this.lstShiftTypes);
            this.Controls.Add(this.pnlSun);
            this.Controls.Add(this.pnlMon);
            this.Controls.Add(this.pnlTue);
            this.Controls.Add(this.pnlWed);
            this.Controls.Add(this.pnlThu);
            this.Controls.Add(this.pnlFri);
            this.Controls.Add(this.pnlSat);
            this.Name = "WeekScheduleForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Street Games - Week Scheduling";
            this.Load += new System.EventHandler(this.WeekScheduleForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }
    }
}



