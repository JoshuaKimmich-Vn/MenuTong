using System;

namespace health
{
    public class MealRecord
    {
        public DateTime Time { get; set; } = DateTime.Now;
        public string MealTime { get; set; } = "";
        public string FoodName { get; set; } = "";
        public double Calories { get; set; }
        public string Note { get; set; } = "";
    }
}
