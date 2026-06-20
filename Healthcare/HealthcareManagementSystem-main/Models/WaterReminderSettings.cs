using System.Collections.Generic;

namespace health
{
    public class WaterReminderSettings
    {
        public bool Enabled { get; set; } = false;
        public List<WaterReminderSlot> Slots { get; set; } = new List<WaterReminderSlot>();
    }
}
