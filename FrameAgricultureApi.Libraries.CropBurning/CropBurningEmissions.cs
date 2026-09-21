using FrameAgricultureApi.Generic;
using static FrameAgricultureApi.Enumerators.Enumerators;

namespace FrameAgricultureApi.Libraries.CropBurning;

/// <summary>
/// Crop burning emissions class
/// </summary>
public class CropBurningEmissions
{
    /// <summary>
    /// Dictionary of emissions from crop burning in the format name,units and value
    /// </summary>
    public Dictionary<CombustionEmissions, Emission> CombustionEmissions { get; set; } = [];
    /// <summary>
    /// Initialise crp burning emissions object
    /// </summary>
    public void InitialiseCropBurningEmissions()
    {
        CombustionEmissions = [];
        CombustionEmissions.Clear();
        CombustionEmissions.Add(Enumerators.Enumerators.CombustionEmissions.ResidueDryMatterBurned, new Emission("Residue Dry Matter Burned", "kg", double.NaN));
        CombustionEmissions.Add(Enumerators.Enumerators.CombustionEmissions.BurningEfficiency, new Emission("Burning Efficiency", "N/A", double.NaN));
        CombustionEmissions.Add(Enumerators.Enumerators.CombustionEmissions.CombustionCO, new Emission("CO from crop burning", "kg", double.NaN));
        CombustionEmissions.Add(Enumerators.Enumerators.CombustionEmissions.CombustionN2ON, new Emission("N2O-N from crop burning", "kg", double.NaN));
        CombustionEmissions.Add(Enumerators.Enumerators.CombustionEmissions.CombustionNH3N, new Emission("NH3-N from crop burning", "kg", double.NaN));
        CombustionEmissions.Add(Enumerators.Enumerators.CombustionEmissions.CombustionNOxN, new Emission("NOx-N from crop burning", "kg", double.NaN));
        CombustionEmissions.Add(Enumerators.Enumerators.CombustionEmissions.CombustionSO2, new Emission("SO2 from crop burning", "kg", double.NaN));
        CombustionEmissions.Add(Enumerators.Enumerators.CombustionEmissions.CombustionCH4, new Emission("CH4 from crop burning", "kg", double.NaN));
        CombustionEmissions.Add(Enumerators.Enumerators.CombustionEmissions.CombustionNMVOC, new Emission("NMVOC from crop burning", "kg", double.NaN));
        CombustionEmissions.Add(Enumerators.Enumerators.CombustionEmissions.CombustionPM2_5, new Emission("PM2.5 from crop burning", "kg", double.NaN));
        CombustionEmissions.Add(Enumerators.Enumerators.CombustionEmissions.CombustionPM10, new Emission("PM10 from crop burning", "kg", double.NaN));
        CombustionEmissions.Add(Enumerators.Enumerators.CombustionEmissions.CombustionTSP, new Emission("TSP from crop burning", "kg", double.NaN));

    }
    /// <summary>
    /// Export emissions method
    /// </summary>
    /// <returns>List of emission objects</returns>
    public List<Emission> ExportEmissions()
    {
        List<Emission> retList = [];
        foreach(CombustionEmissions key in CombustionEmissions.Keys)
        {
            if(!double.IsNaN(CombustionEmissions[key].Value))
            {
                retList.Add(CombustionEmissions[key]);
            }
        }
        return retList;
    }
    /// <summary>
    /// MEthod to round emissions to fixed number of decimal places
    /// </summary>
    /// <param name="decimalPlaces">Number of decimal places to round to</param>
    public void RoundEmissions(int decimalPlaces)
    {
        foreach(CombustionEmissions key in CombustionEmissions.Keys)
        {
            if(!double.IsNaN(CombustionEmissions[key].Value))
            {
                CombustionEmissions[key].Value = Math.Round(CombustionEmissions[key].Value, decimalPlaces);
            }
        }
    }
}
