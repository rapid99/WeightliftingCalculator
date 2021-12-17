namespace WeightliftingCalculator.Models
{
    public class PlateSet
    {
        /// <summary>
        /// Weight of a single plate
        /// </summary>
        public double PlateWeight { get; set; }

        /// <summary>
        /// Number of plate sets needed
        /// </summary>
        public int SetCount { get; set; }

        /// <summary>
        /// Weight of a plate set (2 of the same plates)
        /// </summary>
        public double SetWeight => PlateWeight * 2;
    }
}
