using System.Linq;

using Serilog;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace WeightliftingCalculator.Tests
{
    [TestClass]
    public class PlateCalculatorTests
    {

        /// <summary>
        /// Initialize Serilog file logger
        /// </summary>
        static ILogger SetupLogging()
        {
            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Debug()
                .WriteTo.File(@"C:\WeightLiftingCalculator\WeightliftingCalculator.log")
                .CreateLogger();

            return Log.Logger;
        }

        [TestMethod]
        public void Requested_Weight_Divisible_By_5_Returns_Exact_Weight()
        {
            var plateCalculator = new PlateCalculator(SetupLogging());
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
            var plateCalculator = new PlateCalculator(SetupLogging());
            var requestedWeight = 312;

            var plateSetResult = plateCalculator.CalculatePlates(requestedWeight);
            var totalWeightAchieved = plateSetResult.Sum(w => w.SetWeight * w.SetCount) + plateCalculator.BarbellWeight;
            var weightDifference = totalWeightAchieved - requestedWeight;

            Assert.AreNotEqual(requestedWeight, totalWeightAchieved);
            Assert.AreNotEqual(0, weightDifference);
        }
    }
}
