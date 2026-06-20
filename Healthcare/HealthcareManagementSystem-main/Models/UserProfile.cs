namespace health
{
    public class UserProfile
    {
        public string FullName { get; set; } = "Người dùng";
        public int Age { get; set; } = 20;
        public Gender Gender { get; set; } = Gender.Male;
        public double HeightCm { get; set; } = 170;
        public double WeightKg { get; set; } = 65;
        public double ActivityFactor { get; set; } = 1.375;
        public HealthGoal Goal { get; set; } = HealthGoal.MaintainWeight;
    }
}
