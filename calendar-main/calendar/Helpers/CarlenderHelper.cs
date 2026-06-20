using System;
using System.Collections.Generic;
using System.Text;

namespace carlender.Helpers
{
    public static class CalenderHelper
    {
        public static int GetDaysInMonth(int year, int month)
        {
            return DateTime.DaysInMonth(year, month);
        }

        public static int GetFirstDayOfMonth(int year, int month)
        {
            DateTime firstDay = new DateTime(year, month, 1);

            int dayOfWeek = (int)firstDay.DayOfWeek;

            // Sunday = 0, Monday = 1, ...
            // Đổi lại thành Monday = 0, ..., Sunday = 6
            if (dayOfWeek == 0)
            {
                return 6;
            }

            return dayOfWeek - 1;
        }
    }
}