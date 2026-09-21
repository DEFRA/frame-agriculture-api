using FrameAgricultureApi.Generic;
using static FrameAgricultureApi.Enumerators.Enumerators;

namespace FrameAgricultureApi.Libraries.NonGHG;

/// <summary>
/// Non GHG Livestock emissions class
/// </summary>
public class NonGHGLivestockEmissions
{
    /// <summary>
    /// Dictionary of non-GHG emissions keyed by non-GHG emissions enumerator
    /// </summary>
    public Dictionary<NonGHG_Emissions, Emission> nonGHGEmissions = [];
    /// <summary>
    /// Dictionary of additional outputs by crop type
    /// </summary>
    public Dictionary<NonGHGEmissions_AdditionalLivestock, Emission> AdditionalOutputs = [];
    /// <summary>
    /// Constructor for non-GHG emissions from crop and grassland
    /// </summary>
    public void InitialiseNonGHGLivestockEmissions()
    {
        nonGHGEmissions = [];
        nonGHGEmissions.Clear();
        nonGHGEmissions.Add(NonGHG_Emissions.NMVOC, new Emission("NMVOC", "kg", double.NaN));
        nonGHGEmissions.Add(NonGHG_Emissions.PM2_5, new Emission("PM2.5", "kg", double.NaN));
        nonGHGEmissions.Add(NonGHG_Emissions.PM10, new Emission("PM10", "kg", double.NaN));
        nonGHGEmissions.Add(NonGHG_Emissions.TSP, new Emission("TSP", "kg", double.NaN));

        AdditionalOutputs.Add(NonGHGEmissions_AdditionalLivestock.NMVOC_Grazing, new Emission("NMVOC (Grazing)", "kg", double.NaN));
        AdditionalOutputs.Add(NonGHGEmissions_AdditionalLivestock.NMVOC_Housing, new Emission("NMVOC (Housing)", "kg", double.NaN));
        AdditionalOutputs.Add(NonGHGEmissions_AdditionalLivestock.NMVOC_Storage, new Emission("NMVOC (Manure Storage)", "kg", double.NaN));
        AdditionalOutputs.Add(NonGHGEmissions_AdditionalLivestock.NMVOC_Spreading, new Emission("NMVOC (Manure Spreading)", "kg", double.NaN));
        AdditionalOutputs.Add(NonGHGEmissions_AdditionalLivestock.NMVOC_SilageFeed, new Emission("NMVOC (Silage Feed)", "kg", double.NaN));
        AdditionalOutputs.Add(NonGHGEmissions_AdditionalLivestock.NMVOC_SilageStore, new Emission("NMVOC (Silage Storage)", "kg", double.NaN));
    }

    /// <summary>
    /// Function to export the emissions as a list fo the defualt Emission object
    /// This is used to filter out the unused emission categories
    /// </summary>
    /// <returns></returns>
    public List<Emission> ExportEmissions()
    {
        List<Emission> retList = [];
        foreach(NonGHG_Emissions key in nonGHGEmissions.Keys)
        {
            if(!double.IsNaN(nonGHGEmissions[key].Value))
            {
                retList.Add(nonGHGEmissions[key]);
            }
        }

        foreach(NonGHGEmissions_AdditionalLivestock key in AdditionalOutputs.Keys)
        {
            if(!double.IsNaN(AdditionalOutputs[key].Value))
            {
                retList.Add(AdditionalOutputs[key]);
            }
        }

        return retList;
    }
    /// <summary>
    /// Helper function to round emissions. Useful for testing
    /// </summary>
    /// <param name="decimalPlaces"></param>
    public void RoundEmissions(int decimalPlaces)
    {
        foreach(NonGHG_Emissions key in nonGHGEmissions.Keys)
        {
            if(!double.IsNaN(nonGHGEmissions[key].Value))
            {
                nonGHGEmissions[key].Value = Math.Round(nonGHGEmissions[key].Value, decimalPlaces);
            }
        }

        foreach(NonGHGEmissions_AdditionalLivestock key in AdditionalOutputs.Keys)
        {
            if(!double.IsNaN(AdditionalOutputs[key].Value))
            {
                AdditionalOutputs[key].Value = Math.Round(AdditionalOutputs[key].Value, decimalPlaces);
            }
        }
    }
}
