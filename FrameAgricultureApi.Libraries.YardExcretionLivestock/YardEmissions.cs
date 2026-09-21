using FrameAgricultureApi.Generic;
using static FrameAgricultureApi.Enumerators.Enumerators;

namespace FrameAgricultureApi.Libraries.YardExcretionLivestock;

public class YardEmissions : BaseEmissions
{
    public Dictionary<AdditionalYardExcretaOutputs, Emission> AdditionalOutputs { get; set; } = [];
    public Dictionary<LivestockEmissions, Emission> MethaneEmissions { get; set; } = [];
    /// <summary>
    /// Initialisation of class
    /// </summary>
    public void InitialiseYardEmissions()
    {
        InitialiseBaseEmissions();
        AdditionalOutputs = [];
        AdditionalOutputs.Clear();
        AdditionalOutputs.Add(AdditionalYardExcretaOutputs.UrineNitrogen, new Emission("Nitrogen in urine excreted", "kg/head", double.NaN));
        AdditionalOutputs.Add(AdditionalYardExcretaOutputs.DungNitrogen, new Emission("Nitrogen in dung excreted", "kg/head", double.NaN));
        AdditionalOutputs.Add(AdditionalYardExcretaOutputs.ExcretaTotalN, new Emission("Total nitrogen in excreta", "kg/head", double.NaN));
        AdditionalOutputs.Add(AdditionalYardExcretaOutputs.ExcretaTAN, new Emission("TAN in excreta", "kg/head", double.NaN));
        AdditionalOutputs.Add(AdditionalYardExcretaOutputs.ExcretaOrganicN, new Emission("Organic nitrogen in excreta", "kg/head", double.NaN));
        AdditionalOutputs.Add(AdditionalYardExcretaOutputs.TotalNout, new Emission("Total nitrogen leaving yards", "kg/head", double.NaN));
        AdditionalOutputs.Add(AdditionalYardExcretaOutputs.TANout, new Emission("Total available nitrogen leaving yards", "kg/head", double.NaN));
        AdditionalOutputs.Add(AdditionalYardExcretaOutputs.OrganicNout, new Emission("Organic nitrogen leaving yards", "kg/head", double.NaN));
        MethaneEmissions = [];
        MethaneEmissions.Clear();
        MethaneEmissions.Add(LivestockEmissions.ManureManagementMethane, new Emission("Methane from excreta deposited on yards", "kg/head", double.NaN));
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

        foreach(AdditionalYardExcretaOutputs key in AdditionalOutputs.Keys)
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

        foreach(AdditionalYardExcretaOutputs key in AdditionalOutputs.Keys)
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

