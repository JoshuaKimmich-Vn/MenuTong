using System;
using System.Collections.Generic;
using System.Linq;

namespace health
{
    public class DailyHealthLog
    {
        public DateTime Date { get; set; } = DateTime.Today;
        public List<MealRecord> MealRecords { get; set; } = new List<MealRecord>();
        public List<SleepRecord> SleepRecords { get; set; } = new List<SleepRecord>();
        public List<WaterRecord> WaterRecords { get; set; } = new List<WaterRecord>();
        public List<ActivityRecord> ActivityRecords { get; set; } = new List<ActivityRecord>();
        public List<BodyMetrics> Metrics { get; set; } = new List<BodyMetrics>();
        public List<HealthRecordEntry> ReportEntries { get; set; } = new List<HealthRecordEntry>();

        public double TotalCaloriesIn => MealRecords.Sum(x => x.Calories);
        public double TotalCaloriesBurned => ActivityRecords.Sum(x => x.CaloriesBurned);
        public double NetCalories => TotalCaloriesIn - TotalCaloriesBurned;
        public double TotalWaterMl => WaterRecords.Sum(x => x.AmountMl);
        public double TotalSleepHours => SleepRecords.Sum(x => x.Hours);
        public double TotalActivityDistanceKm => ActivityRecords.Sum(x => x.DistanceKm);
    }
}
