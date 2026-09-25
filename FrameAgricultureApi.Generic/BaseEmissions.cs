using System;

namespace FrameAgricultureApi.Libraries.CoverCrops;

/// <summary>
/// Standard emissions (Nitrogen emissions) class. Contains a dictionary of emission objects by CoreEmission enumerator
/// </summary>
public class BaseEmissions
{
    /// <summary>
    /// Dictionary containing core emissions as name, unit and value
    /// </summary>
    public Dictionary<CoreEmissions, Emission> EmissionsCore { get; set; } = [];

    /// <summary>
    /// Initalisation of Emissions Dictionary
    /// </summary>
    public void InitialiseBaseEmissions()
    {
        EmissionsCore = [];
        EmissionsCore.Clear();
        EmissionsCore.Add(CoreEmissions.DirectN2ON, new Emission("Direct N2O-N", "kg N2O-N", double.NaN));
        EmissionsCore.Add(CoreEmissions.DirectNH3N, new Emission("Direct NH3-N", "kg NH3-N", double.NaN));
        EmissionsCore.Add(CoreEmissions.N2ONleached, new Emission("Indirect N2O-N (leached)", "kg N2O-N", double.NaN));
        EmissionsCore.Add(CoreEmissions.N2ONvolatalised, new Emission("Indirect N2O-N (volatilisation)", "kg N2O-N", double.NaN));
        EmissionsCore.Add(CoreEmissions.LeachedNO3N, new Emission("Direct NO3-N (leached)", "kg NO3-N", double.NaN));
        EmissionsCore.Add(CoreEmissions.DirectNON, new Emission("Direct NO-N", "kg NO-N", double.NaN));
        EmissionsCore.Add(CoreEmissions.DirectN2N, new Emission("Direct N2-N", "kg N2-N", double.NaN));
    }

    /// <summary>
    /// Method to round core emission to fixed number of decimal places
    /// </summary>
    /// <param name="decimalPlaces">Numebr of decimal places to be used</param>
    public void RoundCoreEmissions(int decimalPlaces)
    {
        foreach (CoreEmissions key in EmissionsCore.Keys)
        {
            if (!double.IsNaN(EmissionsCore[key].Value))
            {
                EmissionsCore[key].Value = Math.Round(EmissionsCore[key].Value, decimalPlaces);
            }
        }
    }
}