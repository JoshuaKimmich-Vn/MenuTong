namespace health
{
    public class FoodItem
    {
        public string Name { get; set; } = "";
        public string MealType { get; set; } = "";   // Sáng, Trưa, Tối, Phụ
        public string Category { get; set; } = "";   // Cơm, Bún, Bánh mì, Đạm, Rau...
        public string GoalTag { get; set; } = "Any"; // Lose, Gain, Maintain, Any
        public double Calories { get; set; }
        public string Description { get; set; } = "";
    }
}
