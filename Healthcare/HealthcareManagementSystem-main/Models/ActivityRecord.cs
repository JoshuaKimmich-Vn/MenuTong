using System;

namespace health
{
    public class ActivityRecord
    {
        public DateTime Time { get; set; } = DateTime.Now;
        public string Name { get; set; } = "";
        public double DistanceKm { get; set; }
        public double DurationMinutes { get; set; }
        public double CaloriesBurned { get; set; }
    }
}
