using Serilog;

namespace WeightliftingCalculator.Tests
{
    /// <summary>
    /// Initializes a Console Logger
    /// </summary>
    public class LoggingService
    {
        public readonly ILogger Logger;

        public LoggingService()
        {
            Logger = new LoggerConfiguration()
               .MinimumLevel.Debug()
               .WriteTo.File(@"C:\WeightLiftingCalculator\WeightliftingCalculator_Tests.log")
               .CreateLogger();
        }
    }
}
