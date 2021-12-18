using Serilog;

namespace WeightliftingCalculator.App
{
    /// <summary>
    /// Initializes a File Logger
    /// </summary>
    public class LoggingService
    {
        public readonly ILogger Logger;

        public LoggingService()
        {
            Logger = new LoggerConfiguration()
               .MinimumLevel.Debug()
               .WriteTo.File(@"C:\WeightLiftingCalculator\WeightliftingCalculator_App.log")
               .CreateLogger();
        }
    }
}
