using Serilog;

namespace WeightliftingCalculator.Tests
{
    /// <summary>
    /// Initializes a Console Logger
    /// </summary>
    public class LoggerFactory
    {
        public readonly ILogger Logger;

        public LoggerFactory()
        {
            Logger = new LoggerConfiguration()
               .MinimumLevel.Debug()
               .WriteTo.File(@"C:\WeightLiftingCalculator\WeightliftingCalculator_Tests.log")
               .CreateLogger();
        }
    }
}
