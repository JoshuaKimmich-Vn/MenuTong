using System;
using System.Text;

namespace health
{
    public class HealthScoreResult
    {
        public int Score { get; set; }
        public string Rating { get; set; } = "";
        public string Comment { get; set; } = "";
    }

    public static class HealthScoreService
    {
        public static HealthScoreResult Calculate(UserProfile profile, DailyHealthLog log, HealthGoalPlan plan)
        {
            int score = 0;
            StringBuilder comment = new StringBuilder();

            double bmi = CalorieCalculator.CalculateBmi(profile);
            if (bmi >= 18.5 && bmi < 25) score += 20;
            else if (bmi >= 17 && bmi < 30) score += 12;
            else score += 6;

            if (log.TotalWaterMl >= plan.DailyWaterMlGoal * 0.9) score += 20;
            else if (log.TotalWaterMl >= plan.DailyWaterMlGoal * 0.6) score += 12;
            else comment.AppendLine("- Nên uống thêm nước.");

            if (log.TotalSleepHours >= plan.DailySleepHoursGoal - 0.5) score += 20;
            else if (log.TotalSleepHours >= 6) score += 12;
            else comment.AppendLine("- Nên ngủ nhiều hơn.");

            if (log.TotalActivityDistanceKm > 0 || log.TotalCaloriesBurned >= 150) score += 20;
            else comment.AppendLine("- Hôm nay chưa ghi nhận vận động.");

            if (log.TotalCaloriesIn > 0)
            {
                double diff = Math.Abs(log.TotalCaloriesIn - plan.DailyCalorieGoal);
                if (diff <= 250) score += 20;
                else if (diff <= 500) score += 12;
                else comment.AppendLine("- Calo nạp vào lệch khá nhiều so với mục tiêu.");
            }
            else
            {
                comment.AppendLine("- Chưa ghi nhận calo nạp vào.");
            }

            if (score > 100) score = 100;

            string rating;
            if (score >= 80) rating = "Tốt";
            else if (score >= 60) rating = "Khá";
            else if (score >= 40) rating = "Cần cải thiện";
            else rating = "Thấp";

            return new HealthScoreResult
            {
                Score = score,
                Rating = rating,
                Comment = comment.Length == 0 ? "Các chỉ số hôm nay tương đối ổn." : comment.ToString()
            };
        }
    }
}
