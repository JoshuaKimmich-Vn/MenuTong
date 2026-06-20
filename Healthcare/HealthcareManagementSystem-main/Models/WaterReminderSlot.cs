using System;

namespace health
{
    public class WaterReminderSlot
    {
        public TimeSpan Time { get; set; }
        public double AmountMl { get; set; }
        public DateTime LastTriggeredDate { get; set; } = DateTime.MinValue;
    }
}
