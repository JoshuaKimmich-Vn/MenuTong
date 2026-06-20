using System;

namespace health
{
    public class WaterReminderService
    {
        private readonly HealthDataContext data;

        public WaterReminderService(HealthDataContext data)
        {
            this.data = data;
        }

        public void AddSlot(TimeSpan time, double amountMl)
        {
            if (amountMl <= 0)
                return;

            data.WaterReminder.Slots.Add(new WaterReminderSlot
            {
                Time = time,
                AmountMl = amountMl
            });
        }

        public void ClearSlots()
        {
            data.WaterReminder.Slots.Clear();
        }

        public void AutoGenerateSlots()
        {
            ClearSlots();
            double totalWater = CalorieCalculator.CalculateRecommendedWaterMl(data.Profile);
            double amountPerSlot = totalWater / 6.0;

            AddSlot(new TimeSpan(8, 0, 0), amountPerSlot);
            AddSlot(new TimeSpan(10, 0, 0), amountPerSlot);
            AddSlot(new TimeSpan(12, 30, 0), amountPerSlot);
            AddSlot(new TimeSpan(15, 0, 0), amountPerSlot);
            AddSlot(new TimeSpan(17, 30, 0), amountPerSlot);
            AddSlot(new TimeSpan(20, 30, 0), amountPerSlot);
        }

        public bool TryGetDueReminder(DateTime now, out WaterReminderSlot dueSlot)
        {
            dueSlot = null;

            if (!data.WaterReminder.Enabled)
                return false;

            foreach (WaterReminderSlot slot in data.WaterReminder.Slots)
            {
                DateTime scheduledTime = now.Date.Add(slot.Time);
                bool alreadyTriggeredToday = slot.LastTriggeredDate.Date == now.Date;

                if (!alreadyTriggeredToday && now >= scheduledTime && now <= scheduledTime.AddMinutes(5))
                {
                    slot.LastTriggeredDate = now.Date;
                    dueSlot = slot;
                    return true;
                }
            }

            return false;
        }
    }
}
