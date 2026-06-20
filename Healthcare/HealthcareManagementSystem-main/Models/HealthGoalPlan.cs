using System;

namespace health
{
    public class HealthGoalPlan
    {
        public double StartWeightKg { get; set; } = 65;
        public DateTime StartDate { get; set; } = DateTime.Today;
        public DateTime TargetDate { get; set; } = DateTime.Today.AddMonths(3);
        public double TargetWeightKg { get; set; } = 60;
        public double TargetBmi { get; set; } = 22;
        public double DailyCalorieGoal { get; set; } = 1800;
        public int DailyStepsGoal { get; set; } = 8000;
        public double DailyWaterMlGoal { get; set; } = 2000;
        public double DailySleepHoursGoal { get; set; } = 7.5;
    }
}
