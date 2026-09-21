using FrameAgricultureApi.CustomErrorHandling;
using System.Reflection;

namespace FrameAgricultureApi.Generic;

/// <summary>
/// Helper functions class
/// </summary>
public static class HelperFunctions
{
    /// <summary>
    /// Cionstant for number of days in a week
    /// </summary>
    public static readonly double DaysPerWeek = 7.0;
    /// <summary>
    /// Constant for months in year
    /// </summary>
    public static double MonthsInYear = 12.0;
    /// <summary>
    /// Days in year constant
    /// </summary>
    public static readonly double Days_per_year = 365.0;
    /// <summary>
    /// Constant multiplier to convert from percentage to propoortion
    /// </summary>
    public static readonly double Percent_to_Proportion = 0.01;
    /// <summary>
    /// Constant multiplier to convert tonnes to kg
    /// </summary>
    public static readonly double tonnestokg = 1000.0;
    /// <summary>
    /// Creates mitigation method lst from nullabel array input
    /// </summary>
    /// <param name="mitigationMethods">Mitigaiton methods as nullable integer array </param>
    /// <returns>A list of integers</returns>
    public static List<int> CreateMitigationMethodList(int[]? mitigationMethods)
    {

        if(mitigationMethods == null || mitigationMethods.Length < 1)
        { return []; }
        else
        {
            return [.. mitigationMethods];
        }
    }

    /// <summary>
    /// Sum Prodyuct function that multiplier the corrsponding elements fo two arrays togetehr and then sums the resulting products
    /// </summary>
    /// <param name="array1">The first array</param>
    /// <param name="array2">The second array</param>
    /// <returns>The sum of the product of the corresponding elements</returns>
    public static double SumProduct(double[] array1, double[] array2)
    {
        if(array1.Length != array2.Length)
        { return double.NaN; }

        double sum = 0;
        for(int i = 0; i < array1.Length; i++)
        {
            sum += array1[i] * array2[i];
        }
        return sum;
    }
    /// <summary>
    /// Retrieves lookup table as memeory strema from assembly resources
    /// </summary>
    /// <param name="assembly"></param>
    /// <param name="filename"></param>
    /// <returns></returns>
    /// <exception cref="Exception"></exception>
    public static MemoryStream GetLUT(Assembly assembly, string filename)
    {
        MemoryStream retstream = new();
        string resourcename = assembly.GetManifestResourceNames()
               .Single(str => str.EndsWith(filename));
        using(Stream? stream = assembly.GetManifestResourceStream(resourcename))
        {
            if(stream == null)
            {
                throw new CustomAppException("Unable to locate the LUT file " + filename + ".\nMissing files need to be added to the project - contact the FLEA developer.");
            }
            else
            {
                stream.CopyTo(retstream);
            }
        }
        retstream.Position = 0;
        return retstream;
    }
    /// <summary>
    /// Function to convert daily inputs ot monthly inputs
    /// </summary>
    /// <param name="input">Value ot be converted</param>
    /// <returns>Converetd value</returns>
    public static double DailyToMonthlyConverter(double input)
    {
        return input * 365.0 / 12.0;
    }

    /// <summary>
    /// Function to convert daily inputs to annual inputs
    /// </summary>
    /// <param name="input">Value ot be converted</param>
    /// <returns>Converetd value</returns>
    public static double DailyToAnnualConverter(double input)
    {
        return input * 365.0;
    }
}
