using FrameAgricultureApi.Generic;
using static FrameAgricultureApi.Enumerators.Enumerators;

namespace FrameAgricultureApi.Libraries.NonGHG;

/// <summary>
/// The non-GHG emissions class
/// </summary>
public class NonGHGCropGrassEmissions
{
    /// <summary>
    /// Dictionary of non-GHG emissions keyed by non-GHG emissions enumerator
    /// </summary>
    public Dictionary<NonGHG_Emissions, Emission> nonGHGEmissions = [];
    /// <summary>
    /// Dictionary of additional outputs by crop type
    /// </summary>
    public Dictionary<NonGHGEmissions_AdditionalCropGrass, Emission> AdditionalOutputs = [];
    /// <summary>
    /// Constructor for non-GHG emissions from crop and grassland
    /// </summary>
    public void InitialiseNonGHGCropGrassEmissions()
    {
        nonGHGEmissions = [];
        nonGHGEmissions.Clear();
        nonGHGEmissions.Add(NonGHG_Emissions.NMVOC, new Emission("NMVOC", "kg", double.NaN));
        nonGHGEmissions.Add(NonGHG_Emissions.PM2_5, new Emission("PM2.5", "kg", double.NaN));
        nonGHGEmissions.Add(NonGHG_Emissions.PM10, new Emission("PM10", "kg", double.NaN));
        nonGHGEmissions.Add(NonGHG_Emissions.TSP, new Emission("TSP", "kg", double.NaN));
        AdditionalOutputs = [];
        AdditionalOutputs.Clear();
        AdditionalOutputs.Add(NonGHGEmissions_AdditionalCropGrass.PM2_5_Cultivation, new Emission("PM 2.5 Cultivation", "kg", double.NaN));
        AdditionalOutputs.Add(NonGHGEmissions_AdditionalCropGrass.PM2_5_Harvesting, new Emission("PM 2.5 Harvesting", "kg", double.NaN));
        AdditionalOutputs.Add(NonGHGEmissions_AdditionalCropGrass.PM2_5_Cleaning, new Emission("PM 2.5 Cleaning", "kg", double.NaN));
        AdditionalOutputs.Add(NonGHGEmissions_AdditionalCropGrass.PM2_5_Drying, new Emission("PM 2.5 Drying", "kg", double.NaN));
        AdditionalOutputs.Add(NonGHGEmissions_AdditionalCropGrass.PM10_Cultivation, new Emission("PM 10 Cultivation", "kg", double.NaN));
        AdditionalOutputs.Add(NonGHGEmissions_AdditionalCropGrass.PM10_Harvesting, new Emission("PM 10 Harvesting", "kg", double.NaN));
        AdditionalOutputs.Add(NonGHGEmissions_AdditionalCropGrass.PM10_Cleaning, new Emission("PM 10 Cleaning", "kg", double.NaN));
        AdditionalOutputs.Add(NonGHGEmissions_AdditionalCropGrass.PM10_Drying, new Emission("PM 10 Drying", "kg", double.NaN));
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

        foreach(NonGHGEmissions_AdditionalCropGrass key in AdditionalOutputs.Keys)
        {
            if(!double.IsNaN(AdditionalOutputs[key].Value))
            {
                retList.Add(AdditionalOutputs[key]);
            }
        }

        return retList;
    }
}
