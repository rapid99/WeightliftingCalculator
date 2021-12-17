﻿using System;
using System.Linq;

using Serilog;
using Serilog.Events;

namespace WeightliftingCalculator
{
    class Program
    {
        static void Main()
        {
            SetupLogging();

            try
            {
                Console.WriteLine("Weightlifting Calculator");
                Console.WriteLine("Created by Matt Hynes");
                Console.WriteLine("-----------");
                Console.WriteLine("Enter a weight and the calculator will provide the number of each plate sets that are needed to satisfy the weight entered.");
                Console.WriteLine("Weight: ");
                var requestedWeight = Console.ReadLine();
                var validWeightInput = false;
                while (!validWeightInput)
                {
                    if (!double.TryParse(requestedWeight, out var requestedWeightParsed))
                    {
                        Console.WriteLine("Invalid weight. Please try again.");
                        Console.WriteLine("Weight: ");
                        requestedWeight = Console.ReadLine();
                    }
                    else
                    {
                        validWeightInput = true;
                        var plateCalculator = new PlateCalculator(Log.Logger);
                        var plateSetResult = plateCalculator.CalculatePlates(requestedWeightParsed);

                        Console.WriteLine("-----------");
                        Console.WriteLine("Plate sets:");
                        Console.WriteLine("-----------");

                        var fortyFiveSets = plateSetResult.FirstOrDefault(w => w.PlateWeight == 45);
                        Console.WriteLine($"45lb sets: {fortyFiveSets?.SetCount ?? 0} (Total weight: {(fortyFiveSets == null ? 0 : fortyFiveSets.SetWeight * fortyFiveSets.SetCount)} lbs)");

                        var thirtyFiveSets = plateSetResult.FirstOrDefault(w => w.PlateWeight == 35);
                        Console.WriteLine($"35lb sets: {thirtyFiveSets?.SetCount ?? 0} (Total weight: {(thirtyFiveSets == null ? 0 : thirtyFiveSets.SetWeight * thirtyFiveSets.SetCount)} lbs)");

                        var twentyFiveSets = plateSetResult.FirstOrDefault(w => w.PlateWeight == 25);
                        Console.WriteLine($"25lb sets: {twentyFiveSets?.SetCount ?? 0} (Total weight: {(twentyFiveSets == null ? 0 : twentyFiveSets.SetWeight * twentyFiveSets.SetCount)} lbs)");

                        var tenSets = plateSetResult.FirstOrDefault(w => w.PlateWeight == 10);
                        Console.WriteLine($"10lb sets: {tenSets?.SetCount ?? 0} (Total weight: {(tenSets == null ? 0 : tenSets.SetWeight * tenSets.SetCount)} lbs)");

                        var fiveSets = plateSetResult.FirstOrDefault(w => w.PlateWeight == 5);
                        Console.WriteLine($"5lb sets: {fiveSets?.SetCount ?? 0} (Total weight: {(fiveSets == null ? 0 : fiveSets.SetWeight * fiveSets.SetCount)} lbs)");

                        var twoFiveSets = plateSetResult.FirstOrDefault(w => w.PlateWeight == 2.5);
                        Console.WriteLine($"2.5lb sets: {twoFiveSets?.SetCount ?? 0} (Total weight: {(twoFiveSets == null ? 0 : twoFiveSets.SetWeight * twoFiveSets.SetCount)} lbs)");

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
                            Main();
                    }
                }
            }
            catch (Exception ex)
            {
                Log.Logger.Error(ex, "Unexpected error: {errorMessage}", ex.Message);
            }     
        }

        /// <summary>
        /// Initialize Serilog file logger
        /// </summary>
        static void SetupLogging()
        {
            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Debug()
                .WriteTo.File(@"C:\WeightLiftingCalculator\WeightliftingCalculator.log")
                .CreateLogger();
        }
    }
}
