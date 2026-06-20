using System;
using System.Linq;

namespace health
{
    public static class ActivityService
    {
        public static int CalculateActivityStreak(DailyHealthLog log)
        {
            var days = log.ActivityRecords
                .Select(x => x.Time.Date)
                .Distinct()
                .OrderByDescending(x => x)
                .ToList();

            if (days.Count == 0)
                return 0;

            int streak = 0;
            DateTime expected = DateTime.Today;

            foreach (DateTime day in days)
            {
                if (day == expected)
                {
                    streak++;
                    expected = expected.AddDays(-1);
                }
                else if (streak == 0 && day == DateTime.Today.AddDays(-1))
                {
                    streak++;
                    expected = day.AddDays(-1);
                }
                else
                {
                    break;
                }
            }

            return streak;
        }
    }
}
