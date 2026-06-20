using System.Collections.Generic;
using System.Text;

namespace health
{
    public class HealthReportService
    {
        private readonly HealthDataContext data;

        public HealthReportService(HealthDataContext data)
        {
            this.data = data;
        }

        public string BuildTextReport()
        {
            UserProfile profile = data.Profile;
            DailyHealthLog log = data.Today;
            StringBuilder sb = new StringBuilder();

            sb.AppendLine("HEALTHCARE DAILY REPORT");
            sb.AppendLine("=======================");
            sb.AppendLine("Người dùng: " + profile.FullName);
            sb.AppendLine("BMI: " + CalorieCalculator.CalculateBmi(profile).ToString("0.0"));
            sb.AppendLine("BMR: " + CalorieCalculator.CalculateBmr(profile).ToString("0") + " kcal");
            sb.AppendLine("Calo mục tiêu: " + CalorieCalculator.CalculateTargetCalories(profile).ToString("0") + " kcal");
            sb.AppendLine("Nước mục tiêu: " + CalorieCalculator.CalculateRecommendedWaterMl(profile).ToString("0") + " ml");
            sb.AppendLine();
            sb.AppendLine("Calo đã nạp: " + log.TotalCaloriesIn.ToString("0") + " kcal");
            sb.AppendLine("Calo ròng: " + log.NetCalories.ToString("0") + " kcal");
            sb.AppendLine("Tổng nước: " + log.TotalWaterMl.ToString("0") + " ml");
            sb.AppendLine("Tổng ngủ: " + log.TotalSleepHours.ToString("0.0") + " giờ");
            sb.AppendLine("Tổng vận động: " + log.TotalActivityDistanceKm.ToString("0.##") + " km");
            sb.AppendLine("Calo vận động: " + log.TotalCaloriesBurned.ToString("0") + " kcal");
            sb.AppendLine();
            sb.AppendLine("Cảnh báo:");

            List<string> alerts = HealthAlertService.GenerateAlerts(profile, log);
            foreach (string alert in alerts)
                sb.AppendLine("- " + alert);

            return sb.ToString();
        }
    }
}
