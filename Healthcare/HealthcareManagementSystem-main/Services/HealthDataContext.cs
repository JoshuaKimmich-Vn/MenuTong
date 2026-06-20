namespace health
{
    public class HealthDataContext
    {
        public UserProfile Profile { get; set; }
        public DailyHealthLog Today { get; set; }
        public WaterReminderSettings WaterReminder { get; set; }
        public HealthGoalPlan GoalPlan { get; set; }

        public HealthDataContext()
        {
            Profile = new UserProfile();
            Today = new DailyHealthLog();
            WaterReminder = new WaterReminderSettings();
            GoalPlan = new HealthGoalPlan();
            GoalPlan.StartWeightKg = Profile.WeightKg;
            GoalPlan.TargetWeightKg = Profile.WeightKg;
            GoalPlan.DailyCalorieGoal = CalorieCalculator.CalculateTargetCalories(Profile);
            GoalPlan.DailyWaterMlGoal = CalorieCalculator.CalculateRecommendedWaterMl(Profile);
        }
    }
}
