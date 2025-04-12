using Xunit;
using Moq;
using CatNS;
using System;
using Assert = Microsoft.VisualStudio.TestTools.UnitTesting.Assert;

namespace CatTests
{
    [TestClass]
    public class CatMockTests
    {
        [TestMethod]
        public void Feed_ShouldIncreaseHungerAndHappiness()
        {
            // Arrange
            var mockCalculator = new Mock<IWeightCalculator>();
            var cat = new Cat(mockCalculator.Object);

            var initialHunger = cat.Hunger;
            var initialHappiness = cat.Happiness;

            // Act
            cat.Feed();

            // Assert
            Microsoft.VisualStudio.TestTools.UnitTesting.Assert.IsTrue(cat.Hunger > initialHunger);
            Assert.IsTrue(cat.Happiness > initialHappiness);
            mockCalculator.VerifyNoOtherCalls(); // Калькулятор не должен вызываться
        }

        [TestMethod]
        public void GetWeightDescription_ShouldCallCalculator()
        {
            // Arrange
            var mockCalculator = new Mock<IWeightCalculator>();
            mockCalculator.Setup(x => x.CalculateWeight(It.IsAny<Cat>())).Returns(3500.0);
            mockCalculator.Setup(x => x.GetWeightDescription(3500.0)).Returns("Test Weight Description");

            var cat = new Cat(mockCalculator.Object);

            // Act
            var result = cat.GetWeightDescription();

            // Assert
            Assert.AreEqual("Test Weight Description", result); 
            mockCalculator.Verify(x => x.CalculateWeight(cat), Times.Once);
            mockCalculator.Verify(x => x.GetWeightDescription(3500.0), Times.Once);
        }
        
        [TestMethod]
        public void Play_ShouldAffectMultipleStats()
        {
            // Arrange
            var mockCalculator = new Mock<IWeightCalculator>();
            var cat = new Cat(mockCalculator.Object);

            var initialHunger = cat.Hunger;
            var initialHappiness = cat.Happiness;
            var initialCleanliness = cat.Cleanliness;

            // Act
            cat.Play();

            // Assert
            Assert.IsTrue(cat.Hunger < initialHunger);
            Assert.IsTrue(cat.Happiness > initialHappiness);
            Assert.IsTrue(cat.Cleanliness < initialCleanliness);
            mockCalculator.VerifyNoOtherCalls();
        }
    }
}