using System.Linq;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace WeightliftingCalculator.Tests
{
    [TestClass]
    public class PlateCalculatorTests
    {

        [TestMethod]
        public void Requested_Weight_Divisible_By_5_Returns_Exact_Weight()
        {
            var plateCalculator = new PlateCalculator(new LoggerFactory().Logger);
            var requestedWeight = 265;

            var plateSetResult = plateCalculator.CalculatePlates(requestedWeight);
            var totalWeightAchieved = plateSetResult.Sum(w => w.SetWeight * w.SetCount) + plateCalculator.BarbellWeight;
            var weightDifference = totalWeightAchieved - requestedWeight;

            Assert.AreEqual(requestedWeight, totalWeightAchieved);
            Assert.AreEqual(0, weightDifference);
        }

        [TestMethod]
        public void Requested_Weight_Not_Divisible_By_5_Returns_Difference()
        {
            var plateCalculator = new PlateCalculator(new LoggerFactory().Logger);
            var requestedWeight = 312;

            var plateSetResult = plateCalculator.CalculatePlates(requestedWeight);
            var totalWeightAchieved = plateSetResult.Sum(w => w.SetWeight * w.SetCount) + plateCalculator.BarbellWeight;
            var weightDifference = totalWeightAchieved - requestedWeight;

            Assert.AreNotEqual(requestedWeight, totalWeightAchieved);
            Assert.AreNotEqual(0, weightDifference);
        }
    }
}
