using System.Collections.Generic;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace health
{
    public class ReportPage : BasePage
    {
        private readonly HealthReportService reportService;

        public ReportPage(HealthDataContext data, HealthReportService reportService) : base(data)
        {
            this.reportService = reportService;
            BuildUI();
        }

        private void BuildUI()
        {
            AddHeader("Báo cáo & cảnh báo", "Báo cáo tổng hợp dạng hồ sơ sức khỏe, hiển thị chỉ số, cảnh báo, bữa ăn và lịch sử ghi nhận.");
            Root.AutoScrollMinSize = new Size(0, 1800);

            Panel hospitalHeader = new Panel();
            hospitalHeader.Width = PageWidth();
            hospitalHeader.Height = 110;
            hospitalHeader.BackColor = AppTheme.DarkBlue;
            hospitalHeader.Margin = new Padding(0, 0, 0, 18);

            Label title = new Label();
            title.Text = "HEALTHCARE DAILY REPORT";
            title.Font = new Font("Segoe UI", 22, FontStyle.Bold);
            title.ForeColor = Color.White;
            title.Left = 25;
            title.Top = 18;
            title.Width = 700;
            title.Height = 38;

            Label patient = new Label();
            patient.Text = "Người dùng: " + Data.Profile.FullName + "    |    Mục tiêu: " + FormatGoal(Data.Profile.Goal) + "    |    Ngày: " + System.DateTime.Now.ToString("dd/MM/yyyy");
            patient.Font = new Font("Segoe UI", 10);
            patient.ForeColor = AppTheme.LightBlue;
            patient.Left = 28;
            patient.Top = 62;
            patient.Width = 900;
            patient.Height = 28;

            hospitalHeader.Controls.Add(title);
            hospitalHeader.Controls.Add(patient);
            Root.Controls.Add(hospitalHeader);

            double bmi = CalorieCalculator.CalculateBmi(Data.Profile);
            double bmr = CalorieCalculator.CalculateBmr(Data.Profile);
            double targetCalories = CalorieCalculator.CalculateTargetCalories(Data.Profile);
            double waterNeed = CalorieCalculator.CalculateRecommendedWaterMl(Data.Profile);

            TableLayoutPanel cards = new TableLayoutPanel();
            cards.Width = PageWidth();
            cards.Height = 180;
            cards.ColumnCount = 4;
            cards.RowCount = 1;
            cards.Margin = new Padding(0, 0, 0, 18);
            for (int i = 0; i < 4; i++) cards.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25));

            cards.Controls.Add(CreateMetricCard("BMI", bmi.ToString("0.0"), GetBmiStatus(bmi)), 0, 0);
            cards.Controls.Add(CreateMetricCard("BMR", bmr.ToString("0") + " kcal", "Năng lượng nền"), 1, 0);
            cards.Controls.Add(CreateMetricCard("Calo mục tiêu", targetCalories.ToString("0") + " kcal", "Theo mục tiêu"), 2, 0);
            cards.Controls.Add(CreateMetricCard("Nước mục tiêu", waterNeed.ToString("0") + " ml", "Theo cân nặng"), 3, 0);
            Root.Controls.Add(cards);

            GroupBox summaryBox = CreateSection("Tổng hợp dữ liệu trong ngày", 240);
            RichTextBox summaryText = CreateReadOnlyBox();
            summaryText.Text =
                "Calo đã nạp: " + Data.Today.TotalCaloriesIn.ToString("0") + " kcal" + System.Environment.NewLine +
                "Calo ròng: " + Data.Today.NetCalories.ToString("0") + " kcal" + System.Environment.NewLine +
                "Tổng nước uống: " + Data.Today.TotalWaterMl.ToString("0") + " ml" + System.Environment.NewLine +
                "Tổng thời gian ngủ: " + Data.Today.TotalSleepHours.ToString("0.0") + " giờ" + System.Environment.NewLine +
                "Tổng quãng đường vận động: " + Data.Today.TotalActivityDistanceKm.ToString("0.##") + " km" + System.Environment.NewLine +
                "Calo tiêu thụ do vận động: " + Data.Today.TotalCaloriesBurned.ToString("0") + " kcal" + System.Environment.NewLine +
                "Số bản ghi trong ngày: " + Data.Today.ReportEntries.Count;
            summaryBox.Controls.Add(summaryText);
            Root.Controls.Add(summaryBox);

            GroupBox alertBox = CreateSection("Cảnh báo và nhắc nhở", 260);
            RichTextBox alertText = CreateReadOnlyBox();
            alertText.Text = BuildAlertText();
            alertBox.Controls.Add(alertText);
            Root.Controls.Add(alertBox);

            GroupBox mealBox = CreateSection("Gợi ý bữa ăn theo calo", 330);
            DataGridView mealGrid = CreateMealSuggestionGrid();
            LoadMealSuggestions(mealGrid);
            mealBox.Controls.Add(mealGrid);
            Root.Controls.Add(mealBox);

            GroupBox recordBox = CreateSection("Lịch sử ghi nhận dữ liệu", 420);
            DataGridView recordGrid = CreateRecordGrid();
            RefreshRecordGrid(recordGrid);
            recordBox.Controls.Add(recordGrid);
            Root.Controls.Add(recordBox);

            GroupBox textBox = CreateSection("Báo cáo dạng văn bản", 340);
            RichTextBox textReport = CreateReadOnlyBox();
            textReport.Text = reportService.BuildTextReport();
            textBox.Controls.Add(textReport);
            Root.Controls.Add(textBox);
        }

        private RichTextBox CreateReadOnlyBox()
        {
            RichTextBox box = new RichTextBox();
            box.Dock = DockStyle.Fill;
            box.ReadOnly = true;
            box.BorderStyle = BorderStyle.None;
            box.BackColor = Color.White;
            box.Font = new Font("Segoe UI", 11);
            return box;
        }

        private string BuildAlertText()
        {
            List<string> alerts = HealthAlertService.GenerateAlerts(Data.Profile, Data.Today);
            StringBuilder sb = new StringBuilder();
            foreach (string alert in alerts)
            {
                sb.AppendLine("• " + alert);
                sb.AppendLine();
            }
            return sb.ToString();
        }
    }
}
