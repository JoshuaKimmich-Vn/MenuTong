using System;

namespace health
{
    public class HealthRecordEntry
    {
        public DateTime Time { get; set; } = DateTime.Now;
        public string Category { get; set; } = "";
        public string Name { get; set; } = "";
        public string Value { get; set; } = "";
        public string Note { get; set; } = "";
    }
}
