using System;

namespace health
{
    public class WaterRecord
    {
        public DateTime Time { get; set; } = DateTime.Now;
        public double AmountMl { get; set; }
    }
}
