using System;
using System.Collections.Generic;
using System.Linq;

namespace health
{
    public static class MealPlanService
    {
        public static List<MealSuggestion> GenerateDailyMealPlan(UserProfile profile, int seedOffset = 0)
        {
            double target = CalorieCalculator.CalculateTargetCalories(profile);
            List<FoodItem> foods = FoodCatalogService.GetAllFoods();
            Random random = new Random(DateTime.Today.DayOfYear + profile.Age + (int)profile.WeightKg + seedOffset * 37);

            List<MealSuggestion> plan = new List<MealSuggestion>();
            plan.Add(PickMeal(foods, profile, "Sáng", target * 0.25, random));
            plan.Add(PickMeal(foods, profile, "Trưa", target * 0.35, random));
            plan.Add(PickMeal(foods, profile, "Tối", target * 0.30, random));
            plan.Add(PickMeal(foods, profile, "Phụ", target * 0.10, random));

            return plan;
        }

        public static List<MealSuggestion> GenerateSuggestionTable(UserProfile profile)
        {
            double target = CalorieCalculator.CalculateTargetCalories(profile);
            List<FoodItem> foods = FoodCatalogService.GetAllFoods();
            Random random = new Random(DateTime.Today.DayOfYear + profile.Age + (int)profile.HeightCm);
            List<MealSuggestion> suggestions = new List<MealSuggestion>();

            suggestions.AddRange(PickMany(foods, profile, "Sáng", target * 0.25, random, 3));
            suggestions.AddRange(PickMany(foods, profile, "Trưa", target * 0.35, random, 3));
            suggestions.AddRange(PickMany(foods, profile, "Tối", target * 0.30, random, 3));
            suggestions.AddRange(PickMany(foods, profile, "Phụ", target * 0.10, random, 3));

            return suggestions;
        }

        private static MealSuggestion PickMeal(List<FoodItem> foods, UserProfile profile, string mealType, double targetCalories, Random random)
        {
            List<FoodItem> candidates = GetCandidates(foods, profile, mealType)
                .OrderBy(x => Math.Abs(x.Calories - targetCalories))
                .Take(8)
                .ToList();

            if (candidates.Count == 0)
                candidates = foods.Where(x => x.MealType == mealType).ToList();

            FoodItem selected = candidates[random.Next(candidates.Count)];
            return ToSuggestion(selected, targetCalories);
        }

        private static List<MealSuggestion> PickMany(List<FoodItem> foods, UserProfile profile, string mealType, double targetCalories, Random random, int count)
        {
            List<FoodItem> candidates = GetCandidates(foods, profile, mealType)
                .OrderBy(x => Math.Abs(x.Calories - targetCalories))
                .Take(12)
                .OrderBy(x => random.Next())
                .Take(count)
                .ToList();

            List<MealSuggestion> result = new List<MealSuggestion>();
            foreach (FoodItem item in candidates)
                result.Add(ToSuggestion(item, targetCalories));

            return result;
        }

        private static IEnumerable<FoodItem> GetCandidates(List<FoodItem> foods, UserProfile profile, string mealType)
        {
            string goalTag = "Maintain";
            double bmi = CalorieCalculator.CalculateBmi(profile);

            if (profile.Goal == HealthGoal.LoseWeight || bmi >= 25)
                goalTag = "Lose";
            else if (profile.Goal == HealthGoal.GainWeight || bmi < 18.5)
                goalTag = "Gain";

            return foods.Where(x => x.MealType == mealType && (x.GoalTag == goalTag || x.GoalTag == "Any" || x.GoalTag == "Maintain"));
        }

        private static MealSuggestion ToSuggestion(FoodItem item, double targetCalories)
        {
            return new MealSuggestion
            {
                MealTime = "Bữa " + item.MealType.ToLower(),
                FoodName = item.Name,
                Calories = item.Calories,
                Note = item.Description + " | Mục tiêu bữa: khoảng " + targetCalories.ToString("0") + " kcal"
            };
        }
    }
}
