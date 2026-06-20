using System.Collections.Generic;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace health
{
    public class HomePage : BasePage
    {
        public HomePage(HealthDataContext data) : base(data)
        {
            BuildUI();
        }

        private void BuildUI()
        {
            AddHeader("Trang chủ", "Tổng quan dữ liệu sức khỏe, nước uống, giấc ngủ, vận động và gợi ý bữa ăn.");

            Panel hero = new Panel();
            hero.Width = PageWidth();
            hero.Height = 145;
            hero.BackColor = AppTheme.Blue;
            hero.Margin = new Padding(0, 0, 0, 18);

            Label hello = new Label();
            hello.Text = "Xin chào, " + Data.Profile.FullName;
            hello.Font = new Font("Segoe UI", 22, FontStyle.Bold);
            hello.ForeColor = Color.White;
            hello.Left = 25;
            hello.Top = 24;
            hello.Width = 700;
            hello.Height = 42;

            Label desc = new Label();
            desc.Text = "Theo dõi sức khỏe cá nhân, ghi nhận dữ liệu và nhận gợi ý sinh hoạt theo mục tiêu.";
            desc.Font = new Font("Segoe UI", 10);
            desc.ForeColor = AppTheme.LightBlue;
            desc.Left = 28;
            desc.Top = 75;
            desc.Width = 800;
            desc.Height = 35;

            hero.Controls.Add(hello);
            hero.Controls.Add(desc);
            Root.Controls.Add(hero);

            double bmi = CalorieCalculator.CalculateBmi(Data.Profile);
            double bmr = CalorieCalculator.CalculateBmr(Data.Profile);
            double targetCalories = CalorieCalculator.CalculateTargetCalories(Data.Profile);
            double waterNeed = CalorieCalculator.CalculateRecommendedWaterMl(Data.Profile);

            TableLayoutPanel cards = new TableLayoutPanel();
            cards.Width = PageWidth();
            cards.Height = 175;
            cards.ColumnCount = 4;
            cards.RowCount = 1;
            cards.Margin = new Padding(0, 0, 0, 18);
            for (int i = 0; i < 4; i++) cards.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25));

            cards.Controls.Add(CreateMetricCard("BMI", bmi.ToString("0.0"), GetBmiStatus(bmi)), 0, 0);
            cards.Controls.Add(CreateMetricCard("BMR", bmr.ToString("0") + " kcal", "Năng lượng nền"), 1, 0);
            cards.Controls.Add(CreateMetricCard("Calo", Data.Today.TotalCaloriesIn.ToString("0") + " / " + targetCalories.ToString("0") + " kcal", "Đã nạp / mục tiêu"), 2, 0);
            cards.Controls.Add(CreateMetricCard("Nước mục tiêu", waterNeed.ToString("0") + " ml", "Theo cân nặng"), 3, 0);
            Root.Controls.Add(cards);

            GroupBox bmiBox = CreateSection("Đánh giá BMI và hướng cải thiện", 260);
            RichTextBox bmiText = new RichTextBox();
            bmiText.Dock = DockStyle.Fill;
            bmiText.ReadOnly = true;
            bmiText.BorderStyle = BorderStyle.None;
            bmiText.BackColor = Color.White;
            bmiText.Font = new Font("Segoe UI", 10);
            bmiText.Text = HealthPlanningService.BuildBmiImprovementAdvice(Data.Profile);
            bmiBox.Controls.Add(bmiText);
            Root.Controls.Add(bmiBox);

            TableLayoutPanel cards2 = new TableLayoutPanel();
            cards2.Width = PageWidth();
            cards2.Height = 175;
            cards2.ColumnCount = 4;
            cards2.RowCount = 1;
            cards2.Margin = new Padding(0, 0, 0, 18);
            for (int i = 0; i < 4; i++) cards2.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25));

            cards2.Controls.Add(CreateMetricCard("Nước đã uống", Data.Today.TotalWaterMl.ToString("0") + " ml", "Dữ liệu hôm nay"), 0, 0);
            cards2.Controls.Add(CreateMetricCard("Giấc ngủ", Data.Today.TotalSleepHours.ToString("0.0") + " giờ", "Dữ liệu hôm nay"), 1, 0);
            cards2.Controls.Add(CreateMetricCard("Vận động", Data.Today.TotalActivityDistanceKm.ToString("0.##") + " km", "Quãng đường"), 2, 0);
            cards2.Controls.Add(CreateMetricCard("Calo vận động", Data.Today.TotalCaloriesBurned.ToString("0") + " kcal", "Ước tính"), 3, 0);
            Root.Controls.Add(cards2);

            HealthScoreResult score = HealthScoreService.Calculate(Data.Profile, Data.Today, Data.GoalPlan);
            GroupBox scoreBox = CreateSection("Điểm sức khỏe hôm nay", 230);
            RichTextBox scoreText = new RichTextBox();
            scoreText.Dock = DockStyle.Fill;
            scoreText.ReadOnly = true;
            scoreText.BorderStyle = BorderStyle.None;
            scoreText.BackColor = Color.White;
            scoreText.Font = new Font("Segoe UI", 11);
            scoreText.Text = "Điểm sức khỏe: " + score.Score + "/100\n" +
                             "Đánh giá: " + score.Rating + "\n" +
                             score.Comment + "\n" +
                             "Lời khuyên hôm nay: " + DailyTipService.GetTodayTip();
            scoreBox.Controls.Add(scoreText);
            Root.Controls.Add(scoreBox);

            GroupBox alertBox = CreateSection("Cảnh báo và nhắc nhở", 220);
            RichTextBox alertText = new RichTextBox();
            alertText.Dock = DockStyle.Fill;
            alertText.ReadOnly = true;
            alertText.BorderStyle = BorderStyle.None;
            alertText.BackColor = Color.White;
            alertText.Font = new Font("Segoe UI", 10);
            alertText.Text = BuildAlertText();
            alertBox.Controls.Add(alertText);
            Root.Controls.Add(alertBox);

            GroupBox mealBox = CreateSection("Gợi ý bữa ăn theo calo", 320);
            DataGridView mealGrid = CreateMealSuggestionGrid();
            LoadMealSuggestions(mealGrid);
            mealBox.Controls.Add(mealGrid);
            Root.Controls.Add(mealBox);
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
