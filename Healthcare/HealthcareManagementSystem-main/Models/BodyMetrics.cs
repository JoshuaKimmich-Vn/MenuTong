using System;

namespace health
{
    public class BodyMetrics
    {
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public double WeightKg { get; set; }
        public int HeartRate { get; set; }
        public int SystolicBloodPressure { get; set; }
        public int DiastolicBloodPressure { get; set; }
        public double BodyTemperature { get; set; }
    }
}
