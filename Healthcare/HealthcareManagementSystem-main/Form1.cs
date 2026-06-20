using System;
using System.Drawing;
using System.Windows.Forms;

namespace health
{
    public class Form1 : Form
    {
        private readonly HealthDataContext data;
        private readonly HealthRecordService recordService;
        private readonly HealthReportService reportService;
        private readonly WaterReminderService waterReminderService;

        private Panel sidebarPanel;
        private Panel contentPanel;
        private System.Windows.Forms.Timer waterReminderTimer;

        public Form1()
        {
            data = new HealthDataContext();
            recordService = new HealthRecordService(data);
            reportService = new HealthReportService(data);
            waterReminderService = new WaterReminderService(data);

            BuildMainLayout();
            StartWaterReminderTimer();
            ShowPage(new HomePage(data));
        }

        private void BuildMainLayout()
        {
            Text = "Healthcare Management System";
            Size = new Size(1250, 760);
            MinimumSize = new Size(1100, 680);
            StartPosition = FormStartPosition.CenterScreen;
            BackColor = AppTheme.Background;

            sidebarPanel = new Panel();
            sidebarPanel.Dock = DockStyle.Left;
            sidebarPanel.Width = 250;
            sidebarPanel.BackColor = AppTheme.DarkBlue;
            sidebarPanel.Padding = new Padding(15, 20, 15, 20);

            contentPanel = new Panel();
            contentPanel.Dock = DockStyle.Fill;
            contentPanel.BackColor = AppTheme.Background;
            contentPanel.Padding = new Padding(25);

            Label title = new Label();
            title.Text = "HEALTHCARE";
            title.ForeColor = Color.White;
            title.Font = new Font("Segoe UI", 18, FontStyle.Bold);
            title.Dock = DockStyle.Top;
            title.Height = 70;
            title.TextAlign = ContentAlignment.MiddleCenter;

            Button btnHome = CreateNavButton("Trang chủ", 90);
            btnHome.Click += delegate { ShowPage(new HomePage(data)); };

            Button btnProfile = CreateNavButton("Hồ sơ sức khỏe", 145);
            btnProfile.Click += delegate { ShowPage(new ProfilePage(data)); };

            Button btnGoal = CreateNavButton("Mục tiêu", 200);
            btnGoal.Click += delegate { ShowPage(new GoalPage(data)); };

            Button btnRecord = CreateNavButton("Ghi nhận dữ liệu", 255);
            btnRecord.Click += delegate { ShowPage(new RecordPage(data, recordService)); };

            Button btnActivity = CreateNavButton("Hoạt động", 310);
            btnActivity.Click += delegate { ShowPage(new ActivityPage(data, recordService)); };

            Button btnMealPlan = CreateNavButton("Thực đơn hôm nay", 365);
            btnMealPlan.Click += delegate { ShowPage(new MealPlanPage(data, recordService)); };

            Button btnWater = CreateNavButton("Nhắc uống nước", 420);
            btnWater.Click += delegate { ShowPage(new WaterReminderPage(data, recordService, waterReminderService)); };

            Button btnReport = CreateNavButton("Báo cáo", 475);
            btnReport.Click += delegate { ShowPage(new ReportPage(data, reportService)); };

            sidebarPanel.Controls.Add(title);
            sidebarPanel.Controls.Add(btnHome);
            sidebarPanel.Controls.Add(btnProfile);
            sidebarPanel.Controls.Add(btnGoal);
            sidebarPanel.Controls.Add(btnRecord);
            sidebarPanel.Controls.Add(btnActivity);
            sidebarPanel.Controls.Add(btnMealPlan);
            sidebarPanel.Controls.Add(btnWater);
            sidebarPanel.Controls.Add(btnReport);

            Controls.Add(contentPanel);
            Controls.Add(sidebarPanel);
        }

        private Button CreateNavButton(string text, int top)
        {
            Button button = new Button();
            button.Text = text;
            button.Left = 15;
            button.Top = top;
            button.Width = 220;
            button.Height = 45;
            button.FlatStyle = FlatStyle.Flat;
            button.FlatAppearance.BorderSize = 0;
            button.BackColor = AppTheme.Blue;
            button.ForeColor = Color.White;
            button.Font = new Font("Segoe UI", 10, FontStyle.Bold);
            button.TextAlign = ContentAlignment.MiddleLeft;
            button.Padding = new Padding(15, 0, 0, 0);
            button.Cursor = Cursors.Hand;
            button.MouseEnter += delegate { button.BackColor = AppTheme.DarkBlue; };
            button.MouseLeave += delegate { button.BackColor = AppTheme.Blue; };
            return button;
        }

        private void ShowPage(UserControl page)
        {
            contentPanel.Controls.Clear();
            page.Dock = DockStyle.Fill;
            contentPanel.Controls.Add(page);
        }

        private void StartWaterReminderTimer()
        {
            waterReminderTimer = new System.Windows.Forms.Timer();
            waterReminderTimer.Interval = 60000;
            waterReminderTimer.Tick += CheckWaterReminder;
            waterReminderTimer.Start();
        }

        private void CheckWaterReminder(object sender, EventArgs e)
        {
            WaterReminderSlot dueSlot;
            if (waterReminderService.TryGetDueReminder(DateTime.Now, out dueSlot))
            {
                MessageBox.Show(
                    "Đến giờ uống nước.\nLượng nước cần uống: " + dueSlot.AmountMl.ToString("0") + " ml",
                    "Nhắc uống nước",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information
                );
            }
        }
    }
}
