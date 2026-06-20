using carlender.Helpers;
using carlender.Models;
using carlender.Services;
using System.Drawing;

namespace carlender
{
    public partial class MainForm : Form
    {
        private DateTime currentMonth;
        private DateTime selectedDate;

        private readonly ScheduleManager scheduleManager;
        private readonly FileStorage fileStorage;

        public MainForm()
        {
            InitializeComponent();

            currentMonth = DateTime.Now;
            selectedDate = DateTime.Now;

            scheduleManager = new ScheduleManager();
            fileStorage = new FileStorage("events.json");

            scheduleManager.SetEvents(fileStorage.LoadEvents());

            SetupForm();
            LoadCalendar();
            LoadEventsBySelectedDate();
        }

        // =========================
        // SETUP GIAO DIỆN BAN ĐẦU
        // =========================

        private void SetupForm()
        {
            ApplyModernStyle();
            EnableSmoothUI();

            lstEvents.HorizontalScrollbar = true;
            lstEvents.IntegralHeight = false;
            lstEvents.SelectionMode = SelectionMode.MultiExtended;
        }

        private void EnableSmoothUI()
        {
            DoubleBuffered = true;
            SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
            EnableDoubleBuffer(tabelCalendar);
        }

        private void ApplyModernStyle() 
        {
            BackColor = Color.FromArgb(245, 247, 250);
            Font = new Font("Segoe UI", 10);

            StyleHeader();
            StyleButtons();
            StyleEventList();
        }

        private void StyleHeader()
        {
            lblMonth.Font = new Font("Segoe UI", 18, FontStyle.Bold);
            lblMonth.ForeColor = Color.FromArgb(30, 30, 30);

            lblSelectedDate.Font = new Font("Segoe UI", 11, FontStyle.Bold);
            lblSelectedDate.ForeColor = Color.FromArgb(30, 30, 30);
        }

        private void StyleButtons()
        {
            StyleButton(btnPreviousMonth, Color.White, Color.FromArgb(0, 122, 255), 13);
            StyleButton(btnNextMonth, Color.White, Color.FromArgb(0, 122, 255), 13);

            StyleButton(btnAddEvent, Color.FromArgb(0, 122, 255), Color.White);
            StyleButton(btnEditEvent, Color.FromArgb(255, 193, 7), Color.Black);
            StyleButton(btnDeleteEvent, Color.FromArgb(255, 59, 48), Color.White);
        }

        private void StyleButton(Button button, Color backColor, Color foreColor, int fontSize = 10)
        {
            button.FlatStyle = FlatStyle.Flat;
            button.BackColor = backColor;
            button.ForeColor = foreColor;
            button.FlatAppearance.BorderSize = 0;
            button.Font = new Font("Segoe UI", fontSize, FontStyle.Bold);
        }

        private void StyleEventList()
        {
            lstEvents.BorderStyle = BorderStyle.None;
            lstEvents.BackColor = Color.White;
            lstEvents.Font = new Font("Segoe UI", 10);

            // Nếu ListBox bên phải còn bị hẹp, chỉnh thêm trong Designer:
            // lstEvents.Size = khoảng 320, 260 hoặc lớn hơn.
            lstEvents.Width = Math.Max(lstEvents.Width, 320);
        }

        // =========================
        // LOAD LỊCH
        // =========================

        private void LoadCalendar()
        {
            tabelCalendar.SuspendLayout();

            ClearCalendar();
            SetupCalendarLayout();
            AddDayOfWeekHeaders();
            AddMonthDays();

            tabelCalendar.ResumeLayout();
        }

        private void ClearCalendar()
        {
            tabelCalendar.Controls.Clear();
            tabelCalendar.ColumnStyles.Clear();
            tabelCalendar.RowStyles.Clear();

            tabelCalendar.ColumnCount = 7;
            tabelCalendar.RowCount = 7;
        }

        private void SetupCalendarLayout()
        {
            for (int i = 0; i < 7; i++)
            {
                tabelCalendar.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100f / 7f));
            }

            for (int i = 0; i < 7; i++)
            {
                tabelCalendar.RowStyles.Add(new RowStyle(SizeType.Percent, 100f / 7f));
            }
        }

        private void AddDayOfWeekHeaders()
        {
            string[] daysOfWeek = { "T2", "T3", "T4", "T5", "T6", "T7", "CN" };

            for (int col = 0; col < 7; col++)
            {
                Label label = new Label
                {
                    Text = daysOfWeek[col],
                    TextAlign = ContentAlignment.MiddleCenter,
                    Dock = DockStyle.Fill,
                    Font = new Font("Segoe UI", 10, FontStyle.Bold),
                    ForeColor = Color.Black,
                    BackColor = Color.FromArgb(245, 247, 250)
                };

                tabelCalendar.Controls.Add(label, col, 0);
            }
        }

        private void AddMonthDays()
        {
            int year = currentMonth.Year;
            int month = currentMonth.Month;

            lblMonth.Text = $"Tháng {month}, {year}";

            int daysInMonth = CalenderHelper.GetDaysInMonth(year, month);
            int firstDayIndex = CalenderHelper.GetFirstDayOfMonth(year, month);

            int day = 1;

            for (int row = 1; row < 7; row++)
            {
                for (int col = 0; col < 7; col++)
                {
                    int cellIndex = (row - 1) * 7 + col;

                    Button dayButton;

                    if (cellIndex >= firstDayIndex && day <= daysInMonth)
                    {
                        DateTime date = new DateTime(year, month, day);
                        dayButton = CreateDayButton(day, date);
                        day++;
                    }
                    else
                    {
                        dayButton = CreateEmptyDayButton();
                    }

                    tabelCalendar.Controls.Add(dayButton, col, row);
                }
            }
        }

        private Button CreateDayButton(int day, DateTime date)
        {
            Button button = new Button
            {
                Dock = DockStyle.Fill,
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Segoe UI", 10, FontStyle.Regular),
                BackColor = Color.White,
                ForeColor = Color.FromArgb(40, 40, 40),
                Margin = new Padding(4),
                TextAlign = ContentAlignment.MiddleCenter,
                Tag = date,
                Text = day.ToString()
            };

            button.FlatAppearance.BorderSize = 0;

            List<ScheduleEvent> eventList = scheduleManager.GetEventsByDate(date);
            bool hasEvent = eventList.Count > 0;

            // Nếu ngày có sự kiện thì chỉ vẽ dấu chấm màu, không đổi màu cả ô theo sự kiện nữa.
            if (hasEvent)
            {
                AddEventDotsToDayButton(button, eventList);
            }

            // Nếu là hôm nay thì tô xanh dương.
            if (date.Date == DateTime.Now.Date)
            {
                ApplyTodayStyle(button, day);
            }

            // Nếu là ngày đang chọn thì tô xanh lá.
            if (date.Date == selectedDate.Date)
            {
                ApplySelectedDateStyle(button, day);
            }

            button.Click += BtnDay_Click;

            return button;
        }

        private Button CreateEmptyDayButton()
        {
            Button button = new Button
            {
                Dock = DockStyle.Fill,
                Enabled = false,
                Text = "",
                BackColor = Color.FromArgb(235, 235, 235),
                Margin = new Padding(4),
                FlatStyle = FlatStyle.Flat
            };

            button.FlatAppearance.BorderSize = 0;

            return button;
        }


        private void ApplyTodayStyle(Button button, int day)
        {
            button.Text = day.ToString();
            button.BackColor = Color.FromArgb(26, 115, 232);
            button.ForeColor = Color.White;
            button.Font = new Font("Segoe UI", 10, FontStyle.Bold);
        }

        private void ApplySelectedDateStyle(Button button, int day)
        {
            button.Text = day.ToString();
            button.BackColor = Color.FromArgb(52, 199, 89);
            button.ForeColor = Color.White;
            button.Font = new Font("Segoe UI", 10, FontStyle.Bold);
        }

        // =========================
        // LOAD SỰ KIỆN THEO NGÀY
        // =========================

        private void LoadEventsBySelectedDate()
        {
            lblSelectedDate.Text = $"Sự kiện ngày {selectedDate:dd/MM/yyyy}";

            lstEvents.Items.Clear();

            List<ScheduleEvent> events = scheduleManager.GetEventsByDate(selectedDate);

            foreach (ScheduleEvent scheduleEvent in events)
            {
                lstEvents.Items.Add(scheduleEvent);
            }
        }

        // =========================
        // XỬ LÝ CLICK LỊCH
        // =========================

        private void BtnDay_Click(object? sender, EventArgs e)
        {
            if (sender is Button button && button.Tag is DateTime date)
            {
                selectedDate = date;

                LoadCalendar();
                LoadEventsBySelectedDate();
            }
        }

        private void btnPreviousMonth_Click(object sender, EventArgs e)
        {
            currentMonth = currentMonth.AddMonths(-1);
            LoadCalendar();
        }

        private void btnNextMonth_Click(object sender, EventArgs e)
        {
            currentMonth = currentMonth.AddMonths(1);
            LoadCalendar();
        }

        // =========================
        // THÊM - SỬA - XÓA SỰ KIỆN
        // =========================

        private void btnAddEvent_Click(object sender, EventArgs e)
        {
            EventForm eventForm = new EventForm(selectedDate);

            if (eventForm.ShowDialog() == DialogResult.OK && eventForm.ScheduleEvent != null)
            {
                try
                {
                    scheduleManager.AddEvent(eventForm.ScheduleEvent);
                    SaveAndReload(eventForm.ScheduleEvent.StartTime.Date);

                    MessageBox.Show("Thêm sự kiện thành công.");
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void btnEditEvent_Click(object sender, EventArgs e)
        {
            ScheduleEvent? selectedEvent = GetSelectedEvent();

            if (selectedEvent == null)
            {
                MessageBox.Show("Vui lòng chọn sự kiện cần sửa.");
                return;
            }

            EventForm eventForm = new EventForm(selectedEvent);

            if (eventForm.ShowDialog() == DialogResult.OK && eventForm.ScheduleEvent != null)
            {
                try
                {
                    scheduleManager.UpdateEvent(eventForm.ScheduleEvent);
                    SaveAndReload(eventForm.ScheduleEvent.StartTime.Date);

                    MessageBox.Show("Sửa sự kiện thành công.");
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void btnDeleteEvent_Click(object sender, EventArgs e)
        {
            if (lstEvents.SelectedItems.Count == 0)
            {
                MessageBox.Show("Vui lòng chọn ít nhất một sự kiện cần xóa.");
                return;
            }

            DialogResult result = MessageBox.Show(
                $"Bạn có chắc muốn xóa {lstEvents.SelectedItems.Count} sự kiện đã chọn không?",
                "Xác nhận xóa",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question
            );

            if (result != DialogResult.Yes)
            {
                return;
            }

            List<ScheduleEvent> selectedEvents = new List<ScheduleEvent>();

            foreach (object item in lstEvents.SelectedItems)
            {
                if (item is ScheduleEvent scheduleEvent)
                {
                    selectedEvents.Add(scheduleEvent);
                }
            }

            foreach (ScheduleEvent scheduleEvent in selectedEvents)
            {
                scheduleManager.DeleteEvent(scheduleEvent.Id);
            }

            fileStorage.SaveEvents(scheduleManager.GetAllEvents());

            LoadCalendar();
            LoadEventsBySelectedDate();

            MessageBox.Show("Xóa sự kiện thành công.");
        }

        private void btnDeleteAllEvents_Click(object sender, EventArgs e)
        {
            List<ScheduleEvent> eventsOfSelectedDate = scheduleManager.GetEventsByDate(selectedDate);

            if (eventsOfSelectedDate.Count == 0)
            {
                MessageBox.Show("Ngày này không có sự kiện nào để xóa.");
                return;
            }

            DialogResult result = MessageBox.Show(
                $"Bạn có chắc muốn xóa tất cả {eventsOfSelectedDate.Count} sự kiện trong ngày {selectedDate:dd/MM/yyyy} không?",
                "Xác nhận xóa tất cả",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning
            );

            if (result != DialogResult.Yes)
            {
                return;
            }

            foreach (ScheduleEvent scheduleEvent in eventsOfSelectedDate)
            {
                scheduleManager.DeleteEvent(scheduleEvent.Id);
            }

            fileStorage.SaveEvents(scheduleManager.GetAllEvents());

            LoadCalendar();
            LoadEventsBySelectedDate();

            MessageBox.Show("Đã xóa tất cả sự kiện trong ngày.");
        }

        private ScheduleEvent? GetSelectedEvent()
        {
            return lstEvents.SelectedItem as ScheduleEvent;
        }

        private void SaveAndReload(DateTime dateToSelect)
        {
            fileStorage.SaveEvents(scheduleManager.GetAllEvents());

            selectedDate = dateToSelect.Date;
            currentMonth = selectedDate;

            LoadCalendar();
            LoadEventsBySelectedDate();
        }

        // =========================
        // MÀU SỰ KIỆN THEO LOẠI
        // =========================

        private Color GetCategoryColor(string category)
        {
            string normalizedCategory = NormalizeCategory(category);

            switch (normalizedCategory)
            {
                case "học tập":
                    return Color.FromArgb(0, 122, 255);

                case "công việc":
                    return Color.FromArgb(255, 149, 0);

                case "cá nhân":
                    return Color.FromArgb(175, 82, 222);

                case "khác":
                    return Color.FromArgb(142, 142, 147);

                default:
                    return Color.FromArgb(142, 142, 147);
            }
        }

        private void AddEventDotsToDayButton(Button button, List<ScheduleEvent> eventList)
        {
            button.Paint += (sender, e) =>
            {
                if (eventList.Count == 0)
                {
                    return;
                }

                Graphics graphics = e.Graphics;
                graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;

                int dotSize = 7;
                int dotSpacing = 5;

                int totalWidth = eventList.Count * dotSize + (eventList.Count - 1) * dotSpacing;

                // Nếu quá nhiều sự kiện thì chỉ vẽ số chấm vừa đủ trong ô.
                int maxDotsCanFit = Math.Max(1, (button.Width - 12) / (dotSize + dotSpacing));
                int dotsToDraw = Math.Min(eventList.Count, maxDotsCanFit);

                totalWidth = dotsToDraw * dotSize + (dotsToDraw - 1) * dotSpacing;

                int startX = (button.Width - totalWidth) / 2;
                int y = button.Height - dotSize - 10;

                for (int i = 0; i < dotsToDraw; i++)
                {
                    ScheduleEvent scheduleEvent = eventList[i];
                    Color dotColor = GetCategoryDotColor(scheduleEvent.Category);

                    int x = startX + i * (dotSize + dotSpacing);

                    using (SolidBrush brush = new SolidBrush(dotColor))
                    {
                        graphics.FillEllipse(brush, x, y, dotSize, dotSize);
                    }

                    // Viền trắng nhẹ để dấu chấm nổi rõ trên nền xanh/hôm nay/ngày chọn.
                    using (Pen pen = new Pen(Color.White, 1))
                    {
                        graphics.DrawEllipse(pen, x, y, dotSize, dotSize);
                    }
                }
            };
        }
        private Color GetCategoryDotColor(string category)
        {
            string normalizedCategory = NormalizeCategory(category);

            switch (normalizedCategory)
            {
                case "học tập":
                    return Color.FromArgb(66, 133, 244); // Xanh Google

                case "công việc":
                    return Color.FromArgb(251, 188, 5); // Cam / vàng Google

                case "cá nhân":
                    return Color.FromArgb(175, 82, 222); // Tím

                case "khác":
                    return Color.FromArgb(142, 142, 147); // Xám

                default:
                    return Color.FromArgb(142, 142, 147);
            }
        }

        private string NormalizeCategory(string category)
        {
            if (string.IsNullOrWhiteSpace(category))
            {
                return "khác";
            }

            return category.Trim().ToLower();
        }

        // =========================
        // HÀM PHỤ
        // =========================

        private void EnableDoubleBuffer(Control control)
        {
            typeof(Control).GetProperty(
                "DoubleBuffered",
                System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance
            )?.SetValue(control, true, null);
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
        }

        private void label1_Click(object sender, EventArgs e)
        {
        }

        private void lstEvents_SelectedIndexChanged(object sender, EventArgs e)
        {
        }

        private void lblMyCalendarTitle_Click(object sender, EventArgs e)
        {

        }
    }
}