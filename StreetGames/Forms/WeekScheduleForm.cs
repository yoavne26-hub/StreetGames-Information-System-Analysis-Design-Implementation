using System;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace StreetGames
{
    public partial class WeekScheduleForm : Form
    {
        private DateTime _weekStart;
        private WeekSchedule _currentWeek;

        // Temp negative IDs for "in-memory only" objects
        private int _tempWeekId = -1;
        private int _tempShiftId = -1;

        // Reads the DB connection string from App.config (ConnectionStrings section)
        private static string ConnStr =>
            ConfigurationManager.ConnectionStrings["SAD_DB"].ConnectionString;

        public WeekScheduleForm()
        {
            InitializeComponent();
            UiTheme.ApplyArcade(this);
            ApplyStaticLayout();
            HookShiftTypeListFormatting();

            // Initialize current week start (Sunday-based)
            _weekStart = GetWeekStart(DateTime.Today);

            // Load UI data and render from in-memory lists
            LoadScheduleStatusCombo();
            LoadWeekHeader();
            LoadShiftTypes();
            SetupDayPanels();
            RenderWeek();
        }

        // -----------------------
        // UI helpers / formatting
        // -----------------------

        // Enables custom display formatting for items in the shift types list.
        private void HookShiftTypeListFormatting()
        {
            lstShiftTypes.FormattingEnabled = true;
            lstShiftTypes.Format -= LstShiftTypes_Format;
            lstShiftTypes.Format += LstShiftTypes_Format;
        }

        // Controls how ShiftType appears inside the ListBox (time range + name).
        private void LstShiftTypes_Format(object sender, ListControlConvertEventArgs e)
        {
            if (e.ListItem is ShiftType st)
            {
                e.Value = $"{st.startTime:hh\\:mm} - {st.endTime:hh\\:mm}   {st.name}";
            }
        }

        // Updates header labels/buttons based on whether the week exists in memory.
        private void UpdateModeUi()
        {
            bool existsInMemory = (_currentWeek != null);

            lblMode.Text = existsInMemory
                ? "MODE: EXISTING WEEK (EDIT)"
                : "MODE: NEW WEEK (NOT CREATED)";

            lblMode.ForeColor = existsInMemory
                ? Color.FromArgb(57, 255, 20)
                : Color.FromArgb(255, 20, 147);

            btnCreateOrUpdate.Text = existsInMemory ? "▶ SAVE ◀" : "▶ CREATE WEEK ◀";

            lblTitle.Text = existsInMemory
                ? "=== WEEK SCHEDULING (EXISTING / IN MEMORY) ==="
                : "=== WEEK SCHEDULING (NEW / NOT CREATED) ===";
        }

        // -----------------------
        // Status (Enum)
        // -----------------------

        // Populates status combobox from enum; locks it to DRAFT for this UI flow.
        private void LoadScheduleStatusCombo()
        {
            cmbWeekStatus.Items.Clear();

            foreach (ScheduleStatus s in Enum.GetValues(typeof(ScheduleStatus)))
                cmbWeekStatus.Items.Add(s);

            cmbWeekStatus.SelectedItem = ScheduleStatus.draft;
            cmbWeekStatus.Enabled = false;
        }

        private ScheduleStatus GetSelectedStatus()
        {
            if (cmbWeekStatus.SelectedItem is ScheduleStatus st)
                return st;

            return ScheduleStatus.draft;
        }

        // Maps DB integer statusId into the ScheduleStatus enum safely.
        private bool TryMapDbStatusToEnum(int statusId, out ScheduleStatus status)
        {
            if (Enum.IsDefined(typeof(ScheduleStatus), statusId))
            {
                status = (ScheduleStatus)statusId;
                return true;
            }

            status = ScheduleStatus.draft;
            return false;
        }

        // -----------------------
        // Week calculations
        // -----------------------

        // Week starts on Sunday (DayOfWeek=0), so subtract DayOfWeek to reach Sunday.
        private DateTime GetWeekStart(DateTime d)
        {
            return d.Date.AddDays(-(int)d.DayOfWeek);
        }

        // Default deadline is Thursday 18:00 relative to week start.
        private DateTime GetDefaultDeadlineForWeek(DateTime weekStart)
        {
            return weekStart.AddDays(4).AddHours(18);
        }

        // Uses default deadline unless user checked the DateTimePicker and chose an in-range date.
        private DateTime ResolveDeadline(DateTime weekStart)
        {
            DateTime def = GetDefaultDeadlineForWeek(weekStart);

            if (!dtpDeadline.Checked)
                return def;

            DateTime chosen = dtpDeadline.Value;

            DateTime min = weekStart;
            DateTime max = weekStart.AddDays(7);

            if (chosen < min || chosen > max)
                return def;

            return chosen;
        }

        // Applies a safe value into dtpDeadline within MinDate/MaxDate bounds and sets check-state.
        private void SetDeadlineSafe(DateTime value, bool isChecked)
        {
            if (value < dtpDeadline.MinDate) value = dtpDeadline.MinDate;
            if (value > dtpDeadline.MaxDate) value = dtpDeadline.MaxDate;

            dtpDeadline.Value = value;
            dtpDeadline.Checked = isChecked;
        }

        // Updates week header, loads week object from Program.weekSchedules (in-memory), sets deadline/status UI.
        private void LoadWeekHeader()
        {
            lblWeek.Text = $"Week: {_weekStart:dd/MM/yyyy} - {_weekStart.AddDays(6):dd/MM/yyyy}";

            _currentWeek = Program.weekSchedules
                .FirstOrDefault(w => w.weekStartDate.Date == _weekStart.Date);

            DateTime def = GetDefaultDeadlineForWeek(_weekStart);

            if (_currentWeek == null)
            {
                SetDeadlineSafe(def, false);
                cmbWeekStatus.SelectedItem = ScheduleStatus.draft;
            }
            else
            {
                if (_currentWeek.submissionDeadline.HasValue)
                    SetDeadlineSafe(_currentWeek.submissionDeadline.Value, true);
                else
                    SetDeadlineSafe(def, false);

                if (TryMapDbStatusToEnum(_currentWeek.statusId, out var st))
                    cmbWeekStatus.SelectedItem = st;
                else
                    cmbWeekStatus.SelectedItem = ScheduleStatus.draft;
            }

            UpdateModeUi();
        }

        // -----------------------
        // Shift types list (memory)
        // -----------------------

        // Loads ShiftTypes from the in-memory Program.shiftTypes list into the ListBox.
        private void LoadShiftTypes()
        {
            lstShiftTypes.Items.Clear();
            foreach (var st in Program.shiftTypes)
                lstShiftTypes.Items.Add(st);
        }

        // -----------------------
        // Panels drag/drop (ALL IN MEMORY)
        // -----------------------

        // Initializes all 7 day panels for drag/drop and tags each panel with its day index.
        private void SetupDayPanels()
        {
            SetupDayPanel(pnlSun, 0);
            SetupDayPanel(pnlMon, 1);
            SetupDayPanel(pnlTue, 2);
            SetupDayPanel(pnlWed, 3);
            SetupDayPanel(pnlThu, 4);
            SetupDayPanel(pnlFri, 5);
            SetupDayPanel(pnlSat, 6);
        }

        private void SetupDayPanel(Panel panel, int dayIndex)
        {
            panel.AllowDrop = true;
            panel.Tag = dayIndex;
            panel.BorderStyle = BorderStyle.FixedSingle;
            panel.BackColor = Color.FromArgb(24, 24, 40);

            panel.DragEnter -= DayPanel_DragEnter;
            panel.DragDrop -= DayPanel_DragDrop;

            panel.DragEnter += DayPanel_DragEnter;
            panel.DragDrop += DayPanel_DragDrop;
        }

        // Allows drop only when week exists AND status is draft AND payload is ShiftType.
        private void DayPanel_DragEnter(object sender, DragEventArgs e)
        {
            if (_currentWeek != null &&
                _currentWeek.statusId == (int)ScheduleStatus.draft &&
                e.Data.GetDataPresent(typeof(ShiftType)))
            {
                e.Effect = DragDropEffects.Copy;
            }
            else
            {
                e.Effect = DragDropEffects.None;
            }
        }

        // Adds a Shift into Program.shifts (in-memory) when user drops a ShiftType onto a day panel.
        private void DayPanel_DragDrop(object sender, DragEventArgs e)
        {
            if (_currentWeek == null || _currentWeek.statusId != (int)ScheduleStatus.draft)
            {
                MessageBox.Show("Schedule is not editable. Create the week first (DRAFT).", "Rule");
                return;
            }

            ShiftType st = e.Data.GetData(typeof(ShiftType)) as ShiftType;
            if (st == null) return;

            Panel panel = (Panel)sender;
            int dayIndex = (int)panel.Tag;
            DateTime shiftDate = _weekStart.AddDays(dayIndex);

            bool exists = Program.shifts.Any(s =>
                s.weekScheduleId == _currentWeek.weekScheduleId &&
                s.shiftDate.Date == shiftDate.Date &&
                s.shiftTypeId == st.shiftTypeId);

            if (exists)
            {
                MessageBox.Show("This shift already exists on that day.");
                return;
            }

            int tempId = _tempShiftId--;
            Program.shifts.Add(new Shift(tempId, _currentWeek.weekScheduleId, st.shiftTypeId, shiftDate));
            RenderWeek();
        }

        // -----------------------
        // Rendering (from memory)
        // -----------------------

        // Renders all shifts of the selected week into the 7 panels using Labels (double-click to delete).
        private void RenderWeek()
        {
            ClearPanels();
            if (_currentWeek == null) return;

            var weekShifts = Program.shifts
                .Where(s => s.weekScheduleId == _currentWeek.weekScheduleId)
                .Select(s => new
                {
                    Shift = s,
                    ShiftType = Program.shiftTypes.FirstOrDefault(t => t.shiftTypeId == s.shiftTypeId)
                })
                .OrderBy(x => x.Shift.shiftDate)
                .ThenBy(x => x.ShiftType != null ? x.ShiftType.startTime : TimeSpan.Zero)
                .ToList();

            foreach (var item in weekShifts)
            {
                int dayIndex = (int)(item.Shift.shiftDate.Date - _weekStart.Date).TotalDays;
                Panel panel = GetPanelByDayIndex(dayIndex);
                if (panel == null) continue;

                string text = item.ShiftType == null
                    ? "UNKNOWN SHIFT TYPE"
                    : $"{item.ShiftType.startTime:hh\\:mm} - {item.ShiftType.name}";

                Label lbl = new Label
                {
                    Height = 32,
                    Dock = DockStyle.Top,
                    TextAlign = ContentAlignment.MiddleCenter,
                    BackColor = Color.FromArgb(255, 190, 70),
                    ForeColor = Color.Black,
                    Font = new Font("Segoe UI", 9F, FontStyle.Bold),
                    Text = text,
                    Tag = item.Shift.shiftId
                };

                lbl.DoubleClick -= ShiftLabel_DoubleClick;
                lbl.DoubleClick += ShiftLabel_DoubleClick;

                panel.Controls.Add(lbl);
                panel.Controls.SetChildIndex(lbl, 0);
            }
        }

        // Deletes a shift from memory on double-click, but only if week is draft and shift has no participations.
        private void ShiftLabel_DoubleClick(object sender, EventArgs e)
        {
            if (_currentWeek == null || _currentWeek.statusId != (int)ScheduleStatus.draft)
            {
                MessageBox.Show("Schedule is not editable.", "Rule");
                return;
            }

            int shiftId = (int)((Label)sender).Tag;

            if (Program.shiftParticipations.Any(p => p.shiftId == shiftId))
            {
                MessageBox.Show("Cannot delete shift with participations.");
                return;
            }

            var shift = Program.shifts.FirstOrDefault(s => s.shiftId == shiftId);
            if (shift == null) return;

            if (MessageBox.Show("Delete shift?", "Confirm", MessageBoxButtons.YesNo) != DialogResult.Yes)
                return;

            Program.shifts.Remove(shift);
            RenderWeek();
        }

        // Clears all day panels before re-rendering.
        private void ClearPanels()
        {
            pnlSun.Controls.Clear();
            pnlMon.Controls.Clear();
            pnlTue.Controls.Clear();
            pnlWed.Controls.Clear();
            pnlThu.Controls.Clear();
            pnlFri.Controls.Clear();
            pnlSat.Controls.Clear();
        }

        // Maps 0..6 day indices to the matching panel.
        private Panel GetPanelByDayIndex(int idx)
        {
            switch (idx)
            {
                case 0: return pnlSun;
                case 1: return pnlMon;
                case 2: return pnlTue;
                case 3: return pnlWed;
                case 4: return pnlThu;
                case 5: return pnlFri;
                case 6: return pnlSat;
                default: return null;
            }
        }

        // -----------------------
        // Navigation buttons
        // -----------------------

        // Moves one week back and re-loads header + re-renders.
        private void btnPrevWeek_Click(object sender, EventArgs e)
        {
            _weekStart = _weekStart.AddDays(-7);
            LoadWeekHeader();
            RenderWeek();
        }

        // Moves one week forward and re-loads header + re-renders.
        private void btnNextWeek_Click(object sender, EventArgs e)
        {
            _weekStart = _weekStart.AddDays(7);
            LoadWeekHeader();
            RenderWeek();
        }

        // -----------------------
        // Create (IN MEMORY) / Save (WRITE TO SQL)  ==> ONLY STORED PROCEDURES
        // -----------------------

        // First click: create the week in memory with a temporary negative ID.
        // Next clicks: sync UI -> memory, then persist week/shifts to DB via stored procedures.
        private void btnCreateOrUpdate_Click(object sender, EventArgs e)
        {
            // Create in memory (first click)
            if (_currentWeek == null)
            {
                int tempId = _tempWeekId--;
                int statusId = (int)ScheduleStatus.draft;

                DateTime? dl = dtpDeadline.Checked ? (DateTime?)ResolveDeadline(_weekStart) : null;

                _currentWeek = new WeekSchedule(tempId, _weekStart.Date, dl, statusId);
                Program.weekSchedules.Add(_currentWeek);

                cmbWeekStatus.SelectedItem = ScheduleStatus.draft;

                LoadWeekHeader();
                RenderWeek();
                return;
            }

            try
            {
                // Sync from UI into memory
                SyncWeekFromUi();

                // 1) Seed lookup via stored procedure (fallback) - moved to model
                WeekSchedule.SeedScheduleStatusLookup_SP(ConnStr);

                // 2) Insert / Update week via model helpers
                if (_currentWeek.weekScheduleId < 0)
                {
                    int newWeekId = _currentWeek.InsertToDb(ConnStr);
                    ReplaceTempWeekIdInMemory(_currentWeek.weekScheduleId, newWeekId);
                    _currentWeek.weekScheduleId = newWeekId;
                }
                else
                {
                    _currentWeek.UpdateInDb(ConnStr);
                }

                // 3) Insert NEW shifts via model helper (fallback)
                WeekSchedule.InsertNewShiftsForWeekToDb_SP(_currentWeek.weekScheduleId, ConnStr);

                MessageBox.Show("Saved to DB successfully.", "OK");

                LoadWeekHeader();
                RenderWeek();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "DB Error");
            }
        }

        // After inserting the week to DB, update all shifts that still reference the old temp week ID.
        private void ReplaceTempWeekIdInMemory(int tempWeekId, int realWeekId)
        {
            foreach (var sh in Program.shifts.Where(s => s.weekScheduleId == tempWeekId))
                sh.weekScheduleId = realWeekId;
        }

        // Copies UI selection into the in-memory WeekSchedule object.
        private void SyncWeekFromUi()
        {
            if (_currentWeek == null) return;

            _currentWeek.statusId = (int)GetSelectedStatus();

            if (!dtpDeadline.Checked)
                _currentWeek.submissionDeadline = null;
            else
                _currentWeek.submissionDeadline = ResolveDeadline(_weekStart);
        }

        // Useful helper to detect design-time so runtime-only logic won't run inside the designer.
        private bool IsInDesigner()
        {
            return LicenseManager.UsageMode == LicenseUsageMode.Designtime
                   || (this.Site != null && this.Site.DesignMode);
        }

        // Applies a fixed visual layout/theme programmatically (colors, fonts, sizes, positions).
        private void ApplyStaticLayout()
        {
            Color arcadeBlack = Color.FromArgb(10, 10, 20);
            Color arcadePurple = Color.FromArgb(138, 43, 226);
            Color arcadeCyan = Color.FromArgb(0, 255, 255);
            Color arcadePink = Color.FromArgb(255, 20, 147);
            Color arcadeGreen = Color.FromArgb(57, 255, 20);

            this.BackColor = arcadeBlack;

            lblTitle.Font = new Font("Courier New", 22F, FontStyle.Bold);
            lblTitle.ForeColor = arcadePink;
            lblTitle.Location = new Point(20, 15);

            lblWeek.Font = new Font("Courier New", 11F, FontStyle.Bold);
            lblWeek.ForeColor = arcadeCyan;
            lblWeek.Location = new Point(20, 60);

            lblMode.Font = new Font("Courier New", 10F, System.Drawing.FontStyle.Bold);
            lblMode.ForeColor = arcadeGreen;
            lblMode.Location = new Point(20, 90);

            btnPrevWeek.BackColor = arcadePurple;
            btnPrevWeek.FlatStyle = FlatStyle.Flat;
            btnPrevWeek.FlatAppearance.BorderColor = arcadeCyan;
            btnPrevWeek.FlatAppearance.BorderSize = 2;
            btnPrevWeek.ForeColor = arcadeCyan;

            btnNextWeek.BackColor = arcadePurple;
            btnNextWeek.FlatStyle = FlatStyle.Flat;
            btnNextWeek.FlatAppearance.BorderColor = arcadeCyan;
            btnNextWeek.FlatAppearance.BorderSize = 2;
            btnNextWeek.ForeColor = arcadeCyan;

            dtpDeadline.Font = new Font("Courier New", 9F);
            dtpDeadline.Format = DateTimePickerFormat.Custom;
            dtpDeadline.CustomFormat = "dd/MM/yyyy HH:mm";
            dtpDeadline.ShowUpDown = true;
            dtpDeadline.ShowCheckBox = true;

            lstShiftTypes.BackColor = arcadeBlack;
            lstShiftTypes.ForeColor = arcadeCyan;
            lstShiftTypes.Font = new Font("Courier New", 9F);

            int x0 = 300, y0 = 160, w = 130, h = 390, gap = 8;

            string[] dayNames = { "SUN", "MON", "TUE", "WED", "THU", "FRI", "SAT" };
            Panel[] panels = { pnlSun, pnlMon, pnlTue, pnlWed, pnlThu, pnlFri, pnlSat };
            Label[] labels = { lblSun, lblMon, lblTue, lblWed, lblThu, lblFri, lblSat };

            for (int i = 0; i < 7; i++)
            {
                if (panels[i].Parent == null) this.Controls.Add(panels[i]);
                if (labels[i].Parent == null) this.Controls.Add(labels[i]);

                labels[i].Text = dayNames[i];
                labels[i].Font = new Font("Courier New", 11F, FontStyle.Bold);
                labels[i].ForeColor = arcadePink;
                labels[i].AutoSize = false;
                labels[i].TextAlign = ContentAlignment.MiddleCenter;
                labels[i].Size = new Size(w, 25);
                labels[i].Location = new Point(x0 + (w + gap) * i, y0 - 30);
                labels[i].BackColor = Color.Transparent;
                labels[i].Visible = true;

                panels[i].Location = new Point(x0 + (w + gap) * i, y0);
                panels[i].Size = new Size(w, h);
                panels[i].BackColor = Color.FromArgb(20, 20, 35);
                panels[i].BorderStyle = BorderStyle.FixedSingle;
                panels[i].Visible = true;

                panels[i].BringToFront();
                labels[i].BringToFront();
            }

            this.ClientSize = new Size(1270, 600);
        }

        private void WeekScheduleForm_Load(object sender, EventArgs e)
        {
        }

        private void lstShiftTypes_MouseDown(object sender, MouseEventArgs e)
        {
            int idx = lstShiftTypes.IndexFromPoint(e.Location);
            if (idx < 0) return;

            ShiftType st = lstShiftTypes.Items[idx] as ShiftType;
            if (st == null) return;

            lstShiftTypes.SelectedIndex = idx;
            lstShiftTypes.DoDragDrop(st, DragDropEffects.Copy);
        }
    }
}


