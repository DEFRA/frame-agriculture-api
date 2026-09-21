namespace FrameAgricultureApi.Libraries.GrassResidues;

/// <summary>
/// Parameters defined for genetic gain calculations
/// </summary>
public static class GrassGeneticGainParameters
{
    /// <summary>
    /// Fixed proportional increase in nitrogen uptake due to genetic advances (default)
    /// </summary>
    public static readonly double geneticIncreaseNitrogenUptake = 4.95;
    /// <summary>
    /// Fixed proportional increase in yield due to genetc advances (default)
    /// </summary>
    public static readonly double geneticIncreaseYield = 9.9;
    /// <summary>
    /// Method to calculate the proportional increase in a parameter due to genetic gains
    /// [This is included to allow historical calculation or future change to genetic gain impacts]
    /// </summary>
    /// <param name="currentYear">Current year</param>
    /// <param name="startYear">start year of time period over whcih genetic gains have been observed</param>
    /// <param name="endYear">end year of time period over which genetic gains have been observed</param>
    /// <param name="multiplier">Proportional multiplier for annual change in nitrogen uptake due to genetic advancement
    /// (default values are 0.15 for nitrogen uptake and 0.3 for yield)</param>
    /// <returns>Proportional change in parameter for the current year</returns>
    public static double CalculateGeneticIncreaseParameter(double currentYear, double startYear, double endYear, double multiplier)
    {
        return ((Math.Min(endYear, currentYear) - startYear) * multiplier);
    }
}
