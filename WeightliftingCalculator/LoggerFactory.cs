using Serilog;

namespace WeightliftingCalculator.App
{
    /// <summary>
    /// Initializes a File Logger
    /// </summary>
    public class LoggerFactory
    {
        public readonly ILogger Logger;

        public LoggerFactory()
        {
            Logger = new LoggerConfiguration()
               .MinimumLevel.Debug()
               .WriteTo.File(@"C:\WeightLiftingCalculator\WeightliftingCalculator_App.log")
               .CreateLogger();
        }
    }
}
