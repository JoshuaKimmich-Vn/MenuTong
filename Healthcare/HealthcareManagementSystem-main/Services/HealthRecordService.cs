namespace health
{
    public class HealthRecordService
    {
        private readonly HealthDataContext data;

        public HealthRecordService(HealthDataContext data)
        {
            this.data = data;
        }


        public bool AddSuggestedMeal(MealSuggestion suggestion)
        {
            if (suggestion == null || suggestion.Calories <= 0)
                return false;

            MealRecord meal = new MealRecord
            {
                MealTime = suggestion.MealTime,
                FoodName = suggestion.FoodName,
                Calories = suggestion.Calories,
                Note = suggestion.Note
            };

            data.Today.MealRecords.Add(meal);

            data.Today.ReportEntries.Add(new HealthRecordEntry
            {
                Category = "Bữa ăn",
                Name = meal.MealTime + " - " + meal.FoodName,
                Value = meal.Calories.ToString("0") + " kcal",
                Note = meal.Note
            });

            return true;
        }

        public bool AddManualMeal(string mealType, string foodName, double calories, string note)
        {
            if (string.IsNullOrWhiteSpace(foodName) || calories <= 0)
                return false;

            MealRecord meal = new MealRecord
            {
                MealTime = string.IsNullOrWhiteSpace(mealType) ? "Bữa ăn" : mealType,
                FoodName = foodName,
                Calories = calories,
                Note = note
            };

            data.Today.MealRecords.Add(meal);

            data.Today.ReportEntries.Add(new HealthRecordEntry
            {
                Category = "Bữa ăn",
                Name = meal.MealTime + " - " + meal.FoodName,
                Value = meal.Calories.ToString("0") + " kcal",
                Note = meal.Note
            });

            return true;
        }

        public bool AddWater(double amountMl, string note)
        {
            if (amountMl <= 0)
                return false;

            WaterRecord water = new WaterRecord { AmountMl = amountMl };
            data.Today.WaterRecords.Add(water);
            data.Today.ReportEntries.Add(new HealthRecordEntry
            {
                Category = "Nước uống",
                Name = "Nước",
                Value = water.AmountMl.ToString("0") + " ml",
                Note = note
            });

            return true;
        }

        public bool AddSleep(double hours)
        {
            if (hours <= 0)
                return false;

            SleepRecord sleep = new SleepRecord { Hours = hours };
            data.Today.SleepRecords.Add(sleep);
            data.Today.ReportEntries.Add(new HealthRecordEntry
            {
                Category = "Giấc ngủ",
                Name = "Ngủ",
                Value = sleep.Hours.ToString("0.0") + " giờ",
                Note = "Ghi nhận thời gian ngủ"
            });

            return true;
        }

        public bool AddActivity(string name, double distanceKm, double durationMinutes)
        {
            if (distanceKm <= 0 && durationMinutes <= 0)
                return false;

            if (string.IsNullOrWhiteSpace(name))
                name = "Hoạt động";

            double estimatedCalories = HealthPlanningService.EstimateActivityCalories(data.Profile, name, distanceKm, durationMinutes);
            ActivityRecord activity = new ActivityRecord
            {
                Name = name,
                DistanceKm = distanceKm,
                DurationMinutes = durationMinutes,
                CaloriesBurned = estimatedCalories
            };

            data.Today.ActivityRecords.Add(activity);
            data.Today.ReportEntries.Add(new HealthRecordEntry
            {
                Category = "Hoạt động",
                Name = activity.Name,
                Value = activity.DistanceKm > 0 ? activity.DistanceKm.ToString("0.##") + " km" : activity.DurationMinutes.ToString("0") + " phút",
                Note = "Ước tính tiêu thụ: " + activity.CaloriesBurned.ToString("0") + " kcal"
            });

            return true;
        }

        public bool AddBodyMetrics(double weightKg, int heartRate, int systolic, int diastolic, double temperature)
        {
            bool hasData = weightKg > 0 || heartRate > 0 || systolic > 0 || diastolic > 0 || temperature > 0;

            if (!hasData)
                return false;

            if (weightKg > 0)
                data.Profile.WeightKg = weightKg;

            BodyMetrics metric = new BodyMetrics
            {
                WeightKg = weightKg > 0 ? weightKg : data.Profile.WeightKg,
                HeartRate = heartRate,
                SystolicBloodPressure = systolic,
                DiastolicBloodPressure = diastolic,
                BodyTemperature = temperature
            };

            data.Today.Metrics.Add(metric);
            data.Today.ReportEntries.Add(new HealthRecordEntry
            {
                Category = "Chỉ số cơ thể",
                Name = "Body Metrics",
                Value = "Đã ghi nhận",
                Note = "Cân nặng: " + metric.WeightKg.ToString("0.#") + " kg"
            });

            return true;
        }
    }
}
