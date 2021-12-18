using Serilog;

namespace WeightliftingCalculator.App
{
    public static class Logger
    {
        /// <summary>
        /// Configures and returns a Serilog <see cref="Logger"/>
        /// </summary>
        /// <returns></returns>
        public static ILogger SetupLogger()
        {
            Log.Logger = new LoggerConfiguration()
               .MinimumLevel.Debug()
               .WriteTo.File(@"C:\WeightLiftingCalculator\WeightliftingCalculator.log")
               .CreateLogger();

            return Log.Logger;
        }
    }
}
