using System;
using FrameAgricultureApi.Core.Enums;

namespace FrameAgricultureApi.Libraries.CoverCrops;

/// <summary>
/// Standard emissions (Nitrogen emissions) class. Contains emission objects grouped by pollutant and pathway.
/// </summary>
public class BaseEmissions
{
    /// <summary>
    /// Dictionary containing core emissions by pollutant, then pathway, as name, unit and value.
    /// </summary>
    public Dictionary<SummarisePollutantType, Dictionary<SummarisePathwayType, Emission>> EmissionsCore { get; set; } = [];

    /// <summary>
    /// Initialisation of Emissions Dictionary
    /// </summary>
    public void InitialiseBaseEmissions()
    {
        EmissionsCore = new()
        {
            [SummarisePollutantType.NitrousOxide] = new()
            {
                [SummarisePathwayType.Direct] = new Emission("Direct N2O-N", "kg N2O-N", double.NaN),
                [SummarisePathwayType.IndirectLeaching] = new Emission("Indirect N2O-N (leached)", "kg N2O-N", double.NaN),
                [SummarisePathwayType.IndirectDeposition] = new Emission("Indirect N2O-N (volatilisation)", "kg N2O-N", double.NaN)
            },
            [SummarisePollutantType.Ammonia] = new()
            {
                [SummarisePathwayType.Direct] = new Emission("Direct NH3-N", "kg NH3-N", double.NaN)
            },
            [SummarisePollutantType.Nitrate] = new()
            {
                [SummarisePathwayType.Direct] = new Emission("Direct NO3-N (leached)", "kg NO3-N", double.NaN)
            },
            [SummarisePollutantType.NitricOxide] = new()
            {
                [SummarisePathwayType.Direct] = new Emission("Direct NO-N", "kg NO-N", double.NaN)
            },
            [SummarisePollutantType.Dinitrogen] = new()
            {
                [SummarisePathwayType.Direct] = new Emission("Direct N2-N", "kg N2-N", double.NaN)
            }
        };
    }

    /// <summary>
    /// Method to round core emission to fixed number of decimal places
    /// </summary>
    /// <param name="decimalPlaces">Number of decimal places to be used</param>
    public void RoundCoreEmissions(int decimalPlaces)
    {
        foreach (var pathways in EmissionsCore.Values)
        {
            foreach (var emission in pathways.Values)
            {
                if (!double.IsNaN(emission.Value))
                {
                    emission.Value = Math.Round(emission.Value, decimalPlaces);
                }
            }
        }
    }
}