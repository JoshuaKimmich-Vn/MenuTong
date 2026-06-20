using System;
using System.Text;

namespace health
{
    public static class HealthGoalService
    {
        public static double CalculateWeightProgressPercent(UserProfile profile, HealthGoalPlan plan)
        {
            double total = Math.Abs(plan.TargetWeightKg - plan.StartWeightKg);
            if (total <= 0.01) return 100;

            double achieved = Math.Abs(profile.WeightKg - plan.StartWeightKg);
            double percent = achieved / total * 100.0;

            if (percent < 0) percent = 0;
            if (percent > 100) percent = 100;
            return percent;
        }

        public static string BuildGoalProgressText(UserProfile profile, HealthGoalPlan plan)
        {
            double targetChange = plan.TargetWeightKg - plan.StartWeightKg;
            double achieved = profile.WeightKg - plan.StartWeightKg;
            double percent = CalculateWeightProgressPercent(profile, plan);

            StringBuilder sb = new StringBuilder();
            sb.AppendLine("Mục tiêu cân nặng: " + plan.StartWeightKg.ToString("0.#") + " kg → " + plan.TargetWeightKg.ToString("0.#") + " kg");
            sb.AppendLine("Mức thay đổi mục tiêu: " + targetChange.ToString("0.#") + " kg");
            sb.AppendLine("Đã thay đổi: " + achieved.ToString("0.#") + " kg");
            sb.AppendLine("Tiến độ: " + percent.ToString("0") + "%");
            sb.AppendLine("Ngày bắt đầu: " + plan.StartDate.ToString("dd/MM/yyyy"));
            sb.AppendLine("Ngày mục tiêu: " + plan.TargetDate.ToString("dd/MM/yyyy"));
            sb.AppendLine();
            sb.AppendLine("Mục tiêu mỗi ngày:");
            sb.AppendLine("- Calo: " + plan.DailyCalorieGoal.ToString("0") + " kcal");
            sb.AppendLine("- Bước chân: " + plan.DailyStepsGoal + " bước");
            sb.AppendLine("- Nước: " + plan.DailyWaterMlGoal.ToString("0") + " ml");
            sb.AppendLine("- Ngủ: " + plan.DailySleepHoursGoal.ToString("0.0") + " giờ");
            return sb.ToString();
        }
    }
}
