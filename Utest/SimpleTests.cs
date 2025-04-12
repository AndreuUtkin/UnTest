using Microsoft.VisualStudio.TestTools.UnitTesting;
using CatNS;
namespace Utest
{
    [TestClass]
    public class CatTests
    {
        [TestMethod]
        public void Constructor_InitializesWithFullStatsAnd9Lives()
        {
            var cat = new Cat();

            Assert.AreEqual(50, cat.Hunger);
            Assert.AreEqual(50, cat.Cleanliness);
            Assert.AreEqual(50, cat.Happiness);
            Assert.AreEqual(9, cat.Lives);
        }

        [TestMethod]
        public void Feed_IncreasesHungerAndHappiness_DecreasesCleanliness()
        {
            var cat = new Cat();
            cat.Feed();

            Assert.AreEqual(50 + 20, cat.Hunger); // Проверяем, что голод увеличился на 20
            Assert.AreEqual(50 - 5, cat.Cleanliness); // Чистота уменьшилась на 5
            Assert.AreEqual(50 + 10, cat.Happiness); // Счастье увеличилось на 10
        }

        [TestMethod]
        public void Play_DecreasesHungerAndCleanliness_IncreasesHappiness()
        {
            var cat = new Cat();
            cat.Play();

            Assert.AreEqual(50 - 10, cat.Hunger);
            Assert.AreEqual(50 - 10, cat.Cleanliness);
            Assert.AreEqual(50 + 20, cat.Happiness);
        }

        [TestMethod]
        public void Wash_IncreasesCleanliness_DecreasesHungerAndHappiness()
        {
            var cat = new Cat();
            cat.Wash();

            Assert.AreEqual(50 - 15, cat.Hunger);
            Assert.AreEqual(50 + 30, cat.Cleanliness);
            Assert.AreEqual(50 - 10, cat.Happiness);
        }

        [TestMethod]
        public void CheckStatus_ClampsStatsBetween0And100()
        {
            var cat = new Cat();

            // Искусственно портим статистику
            cat.Feed(); // +20 голод (120 → должно стать 100)
            cat.Feed();
            cat.Feed();

            // Проверяем, что значения ограничены [0; 100]
            Assert.AreEqual(100, cat.Hunger);
        }

        [TestMethod]
        public void CheckStatus_WhenStatReachesZero_CatLosesLifeAndStatsReset()
        {
            var cat = new Cat();

            // Имитируем смерть по голоду
            cat.Feed(); // +20 голода (120 → 100 после CheckStatus)
            cat.Play(); // -10 голода (90)
            cat.Play(); // -10 голода (80)
            cat.Play(); // -10 голода (70)
            cat.Play(); // -10 голода (60)
            cat.Play(); // -10 голода (50)
            cat.Play(); // -10 голода (40)
            cat.Play(); // -10 голода (30)
            cat.Play(); // -10 голода (20)
            cat.Play(); // -10 голода (10)
            cat.Play(); // -10 голода (0 → смерть)

            Assert.AreEqual(8, cat.Lives); // Должно остаться 8 жизней
            Assert.AreEqual(50, cat.Hunger); // Статы должны сброситься
        }
    }
}