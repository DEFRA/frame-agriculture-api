using FrameAgricultureApi.Generic;
using static FrameAgricultureApi.Enumerators.Enumerators;

namespace FrameAgricultureApi.Libraries.OutdoorLivestock;

/// <summary>
/// Emissions from excretion outdoors
/// </summary>
public class OutdoorLivestockEmission : BaseEmissions
{
    /// <summary>
    /// Additional outdoor excreta outputs dictionary
    /// </summary>
    public Dictionary<AdditionalOutdoorExcretaOutputs, Emission> AdditionalOutputs { get; set; } = [];
    /// <summary>
    /// Methane emissions dictionary
    /// </summary>
    public Dictionary<LivestockEmissions, Emission> MethaneEmissions { get; set; } = [];
    /// <summary>
    /// Initialisation of class
    /// </summary>
    public void InitialiseGrazingEmissions()
    {
        InitialiseBaseEmissions();
        AdditionalOutputs = [];
        AdditionalOutputs.Clear();
        AdditionalOutputs.Add(AdditionalOutdoorExcretaOutputs.UrineNitrogen, new Emission("Nitrogen in urine excreted", "kg/head", double.NaN));
        AdditionalOutputs.Add(AdditionalOutdoorExcretaOutputs.DungNitrogen, new Emission("Nitrogen in dung excreted", "kg/head", double.NaN));
        AdditionalOutputs.Add(AdditionalOutdoorExcretaOutputs.ExcretaTotalN, new Emission("Total nitrogen in excreta", "kg/head", double.NaN));
        AdditionalOutputs.Add(AdditionalOutdoorExcretaOutputs.ExcretaTAN, new Emission("TAN in excreta", "kg/head", double.NaN));
        AdditionalOutputs.Add(AdditionalOutdoorExcretaOutputs.ExcretaOrganicN, new Emission("Organic nitrogen in excreta", "kg/head", double.NaN));
        MethaneEmissions = [];
        MethaneEmissions.Clear();
        MethaneEmissions.Add(LivestockEmissions.OutdoorExcretionMethane, new Emission("Methane from excreta deposited outdoors", "kg/head", double.NaN));
    }
    /// <summary>
    /// Export of emissions
    /// </summary>
    /// <returns>List of Emissions objects</returns>
    public List<Emission> ExportEmissions()
    {
        List<Emission> retList = new();
        foreach(CoreEmissions key in EmissionsCore.Keys)
        {
            if(!double.IsNaN(EmissionsCore[key].Value))
            {
                retList.Add(EmissionsCore[key]);
            }
        }

        foreach(LivestockEmissions key in MethaneEmissions.Keys)
        {
            if(!double.IsNaN(MethaneEmissions[key].Value))
            {
                retList.Add(MethaneEmissions[key]);
            }
        }

        foreach(AdditionalOutdoorExcretaOutputs key in AdditionalOutputs.Keys)
        {
            if(!double.IsNaN(AdditionalOutputs[key].Value))
            {
                retList.Add(AdditionalOutputs[key]);
            }
        }
        return retList;
    }

    /// <summary>
    /// Round emissions to a fixed number of decimal places
    /// </summary>
    /// <param name="decimalPlaces">Number of decimal places to round to</param>
    public void RoundEmissions(int decimalPlaces)
    {
        base.RoundCoreEmissions(decimalPlaces);

        foreach(AdditionalOutdoorExcretaOutputs key in AdditionalOutputs.Keys)
        {
            if(!double.IsNaN(AdditionalOutputs[key].Value))
            {
                AdditionalOutputs[key].Value = Math.Round(AdditionalOutputs[key].Value, decimalPlaces);
            }
        }
        foreach(LivestockEmissions key in MethaneEmissions.Keys)
        {
            if(!double.IsNaN(MethaneEmissions[key].Value))
            {
                MethaneEmissions[key].Value = Math.Round(MethaneEmissions[key].Value, decimalPlaces);
            }
        }
    }
}
