using System;
using System.Collections.Generic;

using WeightliftingCalculator.Models;

namespace WeightliftingCalculator
{
    public class PlateCalculator
    {
        /// <summary>
        /// List of all possible plate weights
        /// </summary>
        readonly List<PlateSet> _availablePlates;

        /// <summary>
        /// Weight of the barbell (this will always be applied to the total weight)
        /// </summary>
        public readonly int BarbellWeight;

        public PlateCalculator()
        {
            _availablePlates = new()
            {
                new PlateSet { PlateWeight = 45, SetCount = 0 },
                new PlateSet { PlateWeight = 35, SetCount = 0 },
                new PlateSet { PlateWeight = 25, SetCount = 0 },
                new PlateSet { PlateWeight = 10, SetCount = 0 },
                new PlateSet { PlateWeight = 5, SetCount = 0 },
                new PlateSet { PlateWeight = 2.5, SetCount = 0 },
            };

            BarbellWeight = 45;
        }

        /// <summary>
        /// Calculates the required plate combination to meet the provided weight total
        /// </summary>
        /// <param name="totalWeight"></param>
        /// <returns></returns>
        public List<PlateSet> CalculatePlates(double totalWeight)
        {
            try
            {
                // Remove the barbell weight from the total to get the actual weight needed from just the plates
                var weightNeeded = totalWeight - BarbellWeight;

                var plateSet = new List<PlateSet>();
                foreach (var plate in _availablePlates)
                {
                    while (weightNeeded >= plate.SetWeight)
                    {
                        plate.SetCount++;
                        weightNeeded -= plate.SetWeight;
                    }

                    if (plate.SetCount >= 1)
                        plateSet.Add(plate);
                }

                return plateSet;
            }
            catch (Exception e)
            {
                var xx = e;
                throw;
            }
        }
    }
}
