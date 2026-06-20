namespace health
{
    public static class CalorieCalculator
    {
        public static double CalculateBmr(UserProfile profile)
        {
            double bmr = 10 * profile.WeightKg + 6.25 * profile.HeightCm - 5 * profile.Age;
            return profile.Gender == Gender.Male ? bmr + 5 : bmr - 161;
        }

        public static double CalculateTdee(UserProfile profile)
        {
            return CalculateBmr(profile) * profile.ActivityFactor;
        }

        public static double CalculateTargetCalories(UserProfile profile)
        {
            double tdee = CalculateTdee(profile);

            if (profile.Goal == HealthGoal.LoseWeight)
                return tdee - 500;

            if (profile.Goal == HealthGoal.GainWeight)
                return tdee + 300;

            return tdee;
        }

        public static double CalculateBmi(UserProfile profile)
        {
            double heightM = profile.HeightCm / 100.0;
            if (heightM <= 0)
                return 0;

            return profile.WeightKg / (heightM * heightM);
        }

        public static double CalculateRecommendedWaterMl(UserProfile profile)
        {
            return profile.WeightKg * 35;
        }
    }
}
