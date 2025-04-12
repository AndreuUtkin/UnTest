namespace CatNS
{
    public class CatWeightCalculator : IWeightCalculator
    {
        // Параметры расчета с значениями по умолчанию
        public int BaseWeight { get; }
        public double HungerWeightFactor { get; }
        public double CleanlinessWeightFactor { get; }
        public double HappinessWeightFactor { get; }
        public double LivesWeightFactor { get; }

        // Конструктор с параметрами по умолчанию
        public CatWeightCalculator(
            int baseWeight = 3000,
            double hungerFactor = 15.0,
            double cleanlinessFactor = 10.0,
            double happinessFactor = 5.0,
            double livesFactor = 100.0)
        {
            BaseWeight = baseWeight;
            HungerWeightFactor = hungerFactor;
            CleanlinessWeightFactor = cleanlinessFactor;
            HappinessWeightFactor = happinessFactor;
            LivesWeightFactor = livesFactor;
        }

        public double CalculateWeight(Cat cat)
        {
            return BaseWeight
                   + cat.Hunger * HungerWeightFactor
                   + cat.Cleanliness * CleanlinessWeightFactor
                   + cat.Happiness * HappinessWeightFactor
                   + cat.Lives * LivesWeightFactor;
        }

        public string GetWeightDescription(double weightInGrams)
        {
            double weightInKg = weightInGrams / 1000;

            if (weightInKg < 2.5)
                return $"Очень худой кот ({weightInKg:F2} кг)";
            if (weightInKg < 4.0)
                return $"Кот нормального веса ({weightInKg:F2} кг)";
            if (weightInKg < 6.0)
                return $"Толстый кот ({weightInKg:F2} кг)";

            return $"Очень толстый кот ({weightInKg:F2} кг)";
        }
    }
}