using System;

namespace CatNS
{
    public interface IWeightCalculator
    {
        double CalculateWeight(Cat cat);
        string GetWeightDescription(double weight);
    }
    public class Cat
    {

        private int hunger;
        private int cleanliness;
        private int happiness;
        private int lives;

        private readonly IWeightCalculator weightCalculator;

        

        // Публичные свойства только для чтения
        public int Hunger => hunger;
        public int Cleanliness => cleanliness;
        public int Happiness => happiness;
        public int Lives => lives;

        public event Action OnCatDied;
        public Cat(IWeightCalculator weightCalculator = null)
        {
            this.weightCalculator = weightCalculator ?? new CatWeightCalculator();
            hunger = 50;
            cleanliness = 50;
            happiness = 50;
            lives = 9;
        }
        public Cat()
        {
            hunger = 50;  // максимальное значение сытости
            cleanliness = 50;  // максимальное значение чистоты
            happiness = 50;  // максимальное значение счастья
            lives = 9;  // изначальное количество жизней
        }

        public void Feed()
        {
            hunger += 20;
            happiness += 10;
            cleanliness -= 5;
            CheckStatus();
        }

        public void Play()
        {
            hunger -= 10;
            happiness += 20;
            cleanliness -= 10;
            CheckStatus();
        }

        public string GetWeightDescription()
        {
            var weight = weightCalculator.CalculateWeight(this);
            return weightCalculator.GetWeightDescription(weight);
        }

        public void Wash()
        {
            hunger -= 15;
            happiness -= 10;
            cleanliness += 30;
            CheckStatus();
        }

        private void CheckStatus()
        {
            // Ограничиваем значения шкал
            hunger = Math.Clamp(hunger, 0, 100);
            cleanliness = Math.Clamp(cleanliness, 0, 100);
            happiness = Math.Clamp(happiness, 0, 100);

            Console.WriteLine($"Сытость: {hunger}, Чистота: {cleanliness}, Счастье: {happiness}, Жизни: {lives}");

            // Проверяем, не обнулились ли шкалы
            if (hunger == 0 || cleanliness == 0 || happiness == 0)
            {
                lives--;
                ResetStats();
                Console.WriteLine($"Одна из шкал обнулилась. Количество жизней уменьшилось до {lives}.");

                if (lives <= 0)
                {
                    OnCatDied?.Invoke();
                    throw new InvalidOperationException("больше нет жизней");
                }
            }
        }

        private void ResetStats()
        {
            hunger = 100;
            cleanliness = 100;
            happiness = 100;
        }

        public static void Main(string[] args)
        {
            Cat myCat = new Cat();
            CatWeightCalculator cwc = new CatWeightCalculator();

            
            myCat.OnCatDied += () =>
            {
                Console.WriteLine("Кот умер! Вес: " +cwc.CalculateWeight(myCat));
            };

            while (true)
            {
                Console.WriteLine("Выберите действие: 1 - Кормить, 2 - Играть, 3 - Мыть");
                string choice = Console.ReadLine();

                try
                {
                    switch (choice)
                    {
                        case "1":
                            myCat.Feed();
                            break;
                        case "2":
                            myCat.Play();
                            break;
                        case "3":
                            myCat.Wash();
                            break;
                        default:
                            Console.WriteLine("Неверный ввод. Пожалуйста, выберите 1, 2 или 3.");
                            continue;
                    }

                    
                    
                    double weight = cwc.CalculateWeight(myCat);
                    string weightDescription = cwc.GetWeightDescription(weight);
                    Console.WriteLine($"Текущий вес кота: {weightDescription}");
                }

                catch (InvalidOperationException ex)
                {
                    Console.WriteLine(ex.Message);
                    break;
                }
            }
        }
    }
}