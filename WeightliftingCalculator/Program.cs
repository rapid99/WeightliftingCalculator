using System;
using System.Linq;

using Serilog;

using WeightliftingCalculator.App;

namespace WeightliftingCalculator
{
    class Program
    {
        static void Main()
        {
            Console.WriteLine("Weightlifting Calculator");
            Console.WriteLine("Created by Matt Hynes");
            Console.WriteLine("-----------");
            Console.WriteLine("Enter a weight and the calculator will provide the number of each plate sets that are needed to satisfy the weight entered.");

            InitializePlateCalculator(new LoggerFactory().Logger);
        }

        /// <summary>
        /// Validates the user's input and runs the main Plate Calculator program
        /// </summary>
        /// <param name="logger"></param>
        static void InitializePlateCalculator(ILogger logger)
        {
            try
            {
                Console.WriteLine("Total Desired Weight: ");
                var requestedWeight = Console.ReadLine();
                var validWeightInput = false;
                while (!validWeightInput)
                {
                    if (!double.TryParse(requestedWeight, out var requestedWeightParsed))
                    {
                        Console.WriteLine("Invalid weight. Please try again.");
                        Console.WriteLine("Total Desired Weight: ");
                        requestedWeight = Console.ReadLine();
                    }
                    else
                    {
                        validWeightInput = true;
                        var plateCalculator = new PlateCalculator(logger);
                        var plateSetResult = plateCalculator.CalculatePlates(requestedWeightParsed);

                        Console.WriteLine("-----------");
                        Console.WriteLine("Plate sets:");
                        Console.WriteLine("-----------");

                        foreach (var plate in plateSetResult)
                        {
                            var plateSet = plateSetResult.FirstOrDefault(w => w.PlateWeight == plate.PlateWeight);
                            Console.WriteLine($"{plate.PlateWeight}lb sets: {plateSet?.SetCount ?? 0} (Total weight: {(plateSet == null ? 0 : plateSet.SetWeight * plateSet.SetCount)} lbs)");
                        }                    

                        Console.WriteLine("-----------");
                        Console.WriteLine($"Barbell weight: {plateCalculator.BarbellWeight} lbs");
                        Console.WriteLine("-----------");

                        // total possible weight with plates + the barbell
                        var totalWeightAchieved = plateSetResult.Sum(w => w.SetWeight * w.SetCount) + plateCalculator.BarbellWeight;
                        Console.WriteLine($"Total Weight Achieved: {totalWeightAchieved} lbs");
                        Console.WriteLine($"Difference between requested weight and acheived weight: {totalWeightAchieved - requestedWeightParsed} lbs");

                        Console.WriteLine("-----------");

                        Console.WriteLine("Start over (Y/N)?");
                        var startOver = Console.ReadLine();
                        if (startOver.ToLower() == "y")
                            InitializePlateCalculator(logger);
                    }
                }
            }
            catch (Exception ex)
            {
                logger.Error(ex, "Unexpected error calculating plate sets: {errorMessage}", ex.Message);
                throw;
            }       
        }
    }
}
