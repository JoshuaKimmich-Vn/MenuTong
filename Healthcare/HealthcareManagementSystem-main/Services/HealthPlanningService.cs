using System.Collections.Generic;
using System.Text;

namespace health
{
    public static class HealthPlanningService
    {
        public static double CalculateDailyProteinGrams(UserProfile profile)
        {
            double factor;

            if (profile.Goal == HealthGoal.LoseWeight)
                factor = 1.6;
            else if (profile.Goal == HealthGoal.GainWeight)
                factor = 1.8;
            else
                factor = 1.2;

            if (profile.ActivityFactor >= 1.55)
                factor += 0.2;

            return profile.WeightKg * factor;
        }

        public static string BuildProteinSuggestion(UserProfile profile)
        {
            double dailyProtein = CalculateDailyProteinGrams(profile);
            double breakfast = dailyProtein * 0.25;
            double lunch = dailyProtein * 0.35;
            double dinner = dailyProtein * 0.30;
            double snack = dailyProtein * 0.10;

            StringBuilder sb = new StringBuilder();
            sb.AppendLine("Protein gợi ý/ngày: " + dailyProtein.ToString("0") + " g");
            sb.AppendLine("- Bữa sáng: khoảng " + breakfast.ToString("0") + " g");
            sb.AppendLine("- Bữa trưa: khoảng " + lunch.ToString("0") + " g");
            sb.AppendLine("- Bữa tối: khoảng " + dinner.ToString("0") + " g");
            sb.AppendLine("- Bữa phụ: khoảng " + snack.ToString("0") + " g");
            sb.AppendLine();
            sb.AppendLine("Giá trị phục vụ mô phỏng ứng dụng, không thay thế tư vấn y tế/dinh dưỡng.");
            return sb.ToString();
        }

        public static string BuildBmiImprovementAdvice(UserProfile profile)
        {
            double bmi = CalorieCalculator.CalculateBmi(profile);
            double heightM = profile.HeightCm / 100.0;
            double minNormalWeight = 18.5 * heightM * heightM;
            double maxNormalWeight = 24.9 * heightM * heightM;
            StringBuilder sb = new StringBuilder();

            sb.AppendLine("BMI hiện tại: " + bmi.ToString("0.0"));
            sb.AppendLine("Chiều cao: " + profile.HeightCm.ToString("0.#") + " cm");
            sb.AppendLine("Cân nặng: " + profile.WeightKg.ToString("0.#") + " kg");
            sb.AppendLine("Khoảng cân nặng BMI bình thường tham khảo: " + minNormalWeight.ToString("0.#") + " - " + maxNormalWeight.ToString("0.#") + " kg");
            sb.AppendLine();

            if (bmi < 18.5)
            {
                double needGain = minNormalWeight - profile.WeightKg;
                sb.AppendLine("Đánh giá: Thiếu cân.");
                sb.AppendLine("Hướng cải thiện: tăng khoảng " + needGain.ToString("0.#") + " kg để chạm ngưỡng BMI 18.5.");
                sb.AppendLine("Gợi ý: tăng calo từ cơm, trứng, sữa, thịt/cá, bữa phụ; kết hợp tập kháng lực để tăng cân lành mạnh.");
            }
            else if (bmi < 25)
            {
                sb.AppendLine("Đánh giá: BMI đang trong vùng bình thường.");
                sb.AppendLine("Hướng cải thiện: duy trì calo ổn định, ngủ đủ, vận động đều và theo dõi cân nặng định kỳ.");
            }
            else if (bmi < 30)
            {
                double needLose = profile.WeightKg - maxNormalWeight;
                sb.AppendLine("Đánh giá: Thừa cân.");
                sb.AppendLine("Hướng cải thiện: giảm khoảng " + needLose.ToString("0.#") + " kg để về gần ngưỡng BMI 24.9.");
                sb.AppendLine("Gợi ý: giảm đồ chiên/nước ngọt, tăng rau xanh và đi bộ/chạy nhẹ đều đặn.");
            }
            else
            {
                double needLose = profile.WeightKg - maxNormalWeight;
                sb.AppendLine("Đánh giá: BMI cao.");
                sb.AppendLine("Hướng cải thiện: giảm khoảng " + needLose.ToString("0.#") + " kg để về gần ngưỡng BMI 24.9.");
                sb.AppendLine("Gợi ý: kiểm soát calo, tăng vận động từ từ và theo dõi các chỉ số cơ thể.");
            }

            return sb.ToString();
        }

        public static List<MealSuggestion> GenerateMealSuggestions(UserProfile profile)
        {
            List<MealSuggestion> suggestions = new List<MealSuggestion>();
            double target = CalorieCalculator.CalculateTargetCalories(profile);
            double bmi = CalorieCalculator.CalculateBmi(profile);

            double breakfast = target * 0.25;
            double lunch = target * 0.35;
            double dinner = target * 0.30;
            double snack = target * 0.10;

            if (profile.Goal == HealthGoal.LoseWeight || bmi >= 25)
            {
                suggestions.Add(CreateMeal("Bữa sáng", "Yến mạch + sữa chua không đường + 1 quả chuối nhỏ", breakfast * 0.95, "Ít calo, nhiều chất xơ"));
                suggestions.Add(CreateMeal("Bữa sáng", "Bánh mì đen + 2 trứng luộc + dưa leo", breakfast, "Giàu đạm, no lâu"));
                suggestions.Add(CreateMeal("Bữa trưa", "Cơm gạo lứt + ức gà áp chảo + rau luộc", lunch, "Phù hợp giảm cân"));
                suggestions.Add(CreateMeal("Bữa trưa", "Cơm vừa + cá hấp + canh rau + đậu hũ", lunch * 0.95, "Ít dầu mỡ"));
                suggestions.Add(CreateMeal("Bữa tối", "Cá basa/cá thu + salad rau + khoai lang", dinner * 0.9, "Nhẹ bụng buổi tối"));
                suggestions.Add(CreateMeal("Bữa tối", "Đậu hũ sốt cà + rau luộc + nửa chén cơm", dinner * 0.85, "Giảm tinh bột"));
                suggestions.Add(CreateMeal("Bữa phụ", "Táo + sữa chua không đường", snack, "Bữa phụ ít calo"));
                suggestions.Add(CreateMeal("Bữa phụ", "Hạt hạnh nhân lượng nhỏ + trà không đường", snack * 1.05, "Kiểm soát khẩu phần"));
            }
            else if (profile.Goal == HealthGoal.GainWeight || bmi < 18.5)
            {
                suggestions.Add(CreateMeal("Bữa sáng", "Bánh mì trứng + sữa tươi + chuối", breakfast * 1.1, "Tăng năng lượng dễ ăn"));
                suggestions.Add(CreateMeal("Bữa sáng", "Cơm trứng cuộn + xúc xích nhỏ + sữa", breakfast * 1.15, "Nhiều calo hơn"));
                suggestions.Add(CreateMeal("Bữa trưa", "Cơm bò xào + trứng ốp la + canh rau", lunch * 1.1, "Giàu năng lượng và đạm"));
                suggestions.Add(CreateMeal("Bữa trưa", "Cơm gà sốt nấm + đậu hũ + rau", lunch, "Tăng cân có kiểm soát"));
                suggestions.Add(CreateMeal("Bữa tối", "Cơm cá hồi/cá thu + khoai + rau", dinner, "Bổ sung chất béo tốt"));
                suggestions.Add(CreateMeal("Bữa tối", "Mì Ý bò bằm + salad + sữa", dinner * 1.15, "Nhiều calo"));
                suggestions.Add(CreateMeal("Bữa phụ", "Sinh tố chuối + sữa + bơ đậu phộng", snack * 1.25, "Bữa phụ tăng cân"));
                suggestions.Add(CreateMeal("Bữa phụ", "Sữa chua + granola + hạt + mật ong", snack * 1.2, "Bổ sung năng lượng"));
            }
            else
            {
                suggestions.Add(CreateMeal("Bữa sáng", "Trứng + bánh mì nguyên cám + sữa", breakfast, "Cân bằng năng lượng"));
                suggestions.Add(CreateMeal("Bữa sáng", "Phở bò nhỏ + rau thơm", breakfast * 1.05, "Đủ năng lượng buổi sáng"));
                suggestions.Add(CreateMeal("Bữa trưa", "Cơm + thịt nạc/cá + rau xanh + canh", lunch, "Bữa chính cân bằng"));
                suggestions.Add(CreateMeal("Bữa trưa", "Cơm bò + trứng + salad", lunch * 1.05, "Duy trì cân nặng"));
                suggestions.Add(CreateMeal("Bữa tối", "Cơm vừa phải + cá/thịt + rau", dinner, "Không quá nặng buổi tối"));
                suggestions.Add(CreateMeal("Bữa tối", "Bún thịt nướng ít dầu + rau", dinner * 0.95, "Đổi món"));
                suggestions.Add(CreateMeal("Bữa phụ", "Sữa chua / trái cây / hạt", snack, "Bổ sung nhẹ"));
                suggestions.Add(CreateMeal("Bữa phụ", "Bánh mì nhỏ + sữa", snack * 1.1, "Bữa phụ cân bằng"));
            }

            return suggestions;
        }

        private static MealSuggestion CreateMeal(string mealTime, string foodName, double calories, string note)
        {
            return new MealSuggestion
            {
                MealTime = mealTime,
                FoodName = foodName,
                Calories = calories,
                Note = note
            };
        }

        public static double EstimateActivityCalories(UserProfile profile, string activityName, double distanceKm)
        {
            return EstimateActivityCalories(profile, activityName, distanceKm, 0);
        }

        public static double EstimateActivityCalories(UserProfile profile, string activityName, double distanceKm, double durationMinutes)
        {
            string name = activityName == null ? "" : activityName.ToLower();

            if (distanceKm > 0)
            {
                double distanceFactor;

                if (name.Contains("chạy") || name.Contains("chay"))
                    distanceFactor = 1.0;
                else if (name.Contains("đi bộ") || name.Contains("di bo"))
                    distanceFactor = 0.55;
                else if (name.Contains("đạp xe") || name.Contains("dap xe"))
                    distanceFactor = 0.35;
                else
                    distanceFactor = 0.7;

                return profile.WeightKg * distanceKm * distanceFactor;
            }

            if (durationMinutes <= 0)
                return 0;

            double met = GetMetValue(name);
            return met * 3.5 * profile.WeightKg / 200.0 * durationMinutes;
        }

        private static double GetMetValue(string name)
        {
            if (name.Contains("bơi") || name.Contains("boi")) return 6.0;
            if (name.Contains("nhảy dây") || name.Contains("nhay day")) return 10.0;
            if (name.Contains("gym") || name.Contains("tạ") || name.Contains("ta")) return 5.0;
            if (name.Contains("yoga")) return 2.5;
            if (name.Contains("bóng đá") || name.Contains("bong da")) return 7.0;
            if (name.Contains("bóng rổ") || name.Contains("bong ro")) return 6.5;
            if (name.Contains("cầu lông") || name.Contains("cau long")) return 5.5;
            if (name.Contains("nhảy") || name.Contains("nhay")) return 5.0;
            if (name.Contains("đạp xe") || name.Contains("dap xe")) return 6.0;
            return 4.0;
        }
    }
}
