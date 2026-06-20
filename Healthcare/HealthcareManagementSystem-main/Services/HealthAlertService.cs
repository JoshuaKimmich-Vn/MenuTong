using System.Collections.Generic;

namespace health
{
    public static class HealthAlertService
    {
        public static List<string> GenerateAlerts(UserProfile profile, DailyHealthLog log)
        {
            List<string> alerts = new List<string>();
            double waterNeed = CalorieCalculator.CalculateRecommendedWaterMl(profile);
            double targetCalories = CalorieCalculator.CalculateTargetCalories(profile);

            if (log.TotalCaloriesIn > 0 && log.TotalCaloriesIn < targetCalories * 0.65)
                alerts.Add("Calo đã nạp hôm nay còn thấp so với mục tiêu.");

            if (log.TotalCaloriesIn > targetCalories * 1.25)
                alerts.Add("Calo đã nạp đang vượt khá nhiều so với mục tiêu.");

            if (log.TotalWaterMl > 0 && log.TotalWaterMl < waterNeed * 0.7)
                alerts.Add("Lượng nước uống hôm nay còn thấp so với mục tiêu.");

            if (log.TotalWaterMl > waterNeed * 1.6)
                alerts.Add("Lượng nước uống đang vượt khá nhiều so với mục tiêu mô phỏng.");

            if (log.TotalSleepHours > 0 && log.TotalSleepHours < 6)
                alerts.Add("Thời gian ngủ đang thấp, nên ngủ đủ hơn.");

            if (log.TotalSleepHours > 10)
                alerts.Add("Thời gian ngủ khá nhiều, nên duy trì lịch ngủ ổn định.");

            if (log.TotalActivityDistanceKm > 15)
                alerts.Add("Quãng đường vận động hôm nay khá cao, nên chú ý phục hồi.");

            if (log.Metrics.Count > 0)
            {
                BodyMetrics last = log.Metrics[log.Metrics.Count - 1];

                if (last.HeartRate > 0 && (last.HeartRate < 50 || last.HeartRate > 110))
                    alerts.Add("Nhịp tim nằm ngoài vùng mô phỏng thông thường.");

                if ((last.SystolicBloodPressure >= 140 && last.SystolicBloodPressure > 0) ||
                    (last.DiastolicBloodPressure >= 90 && last.DiastolicBloodPressure > 0))
                    alerts.Add("Huyết áp đang cao trong mô phỏng, nên theo dõi lại.");

                if (last.BodyTemperature > 0 && (last.BodyTemperature < 35.5 || last.BodyTemperature >= 37.5))
                    alerts.Add("Nhiệt độ cơ thể nằm ngoài mức thông thường trong mô phỏng.");
            }

            if (alerts.Count == 0)
                alerts.Add("Chưa có cảnh báo nghiêm trọng.");

            return alerts;
        }
    }
}
