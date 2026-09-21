using FrameAgricultureApi.Generic;
using static FrameAgricultureApi.Enumerators.Enumerators;

namespace FrameAgricultureApi.Libraries.Housing;

public class HousingEmissions : BaseEmissions
{
    public Dictionary<AdditionalHousingManureManagementOutputs, Emission> AdditionalOutputs { get; set; } = [];
    public Dictionary<LivestockEmissions, Emission> MethaneEmissions { get; set; } = [];
    /// <summary>
    /// Initialisation of class
    /// </summary>
    public void InitaliseHousingEmissions()
    {
        InitialiseBaseEmissions();
        AdditionalOutputs = [];
        AdditionalOutputs.Clear();
        AdditionalOutputs.Add(AdditionalHousingManureManagementOutputs.Urinein, new Emission("Nitrogen in urine excreted", "kg/head", double.NaN));
        AdditionalOutputs.Add(AdditionalHousingManureManagementOutputs.Dungin, new Emission("Nitrogen in dung excreted", "kg/head", double.NaN));
        AdditionalOutputs.Add(AdditionalHousingManureManagementOutputs.TotalNin, new Emission("Total Nitrogen entering housing", "kg/head", double.NaN));
        AdditionalOutputs.Add(AdditionalHousingManureManagementOutputs.TANin, new Emission("Total ammoniacal nitrogen entering housing", "kg/head", double.NaN));
        AdditionalOutputs.Add(AdditionalHousingManureManagementOutputs.OrganicNin, new Emission("Organic nitrogen entering housing", "kg/head", double.NaN));
        AdditionalOutputs.Add(AdditionalHousingManureManagementOutputs.NinBedding, new Emission("Total nitrogen in bedding", "kg/head", double.NaN));
        AdditionalOutputs.Add(AdditionalHousingManureManagementOutputs.NImmobilised, new Emission("Total nitrogen immobilised", "kg/head", double.NaN));
        AdditionalOutputs.Add(AdditionalHousingManureManagementOutputs.TotalNout, new Emission("Total nitrogen leaving housing", "kg/head", double.NaN));
        AdditionalOutputs.Add(AdditionalHousingManureManagementOutputs.TANout, new Emission("Total ammoniacal nitrogen leaving housing", "kg/head", double.NaN));
        AdditionalOutputs.Add(AdditionalHousingManureManagementOutputs.OrganicNout, new Emission("Organic nitrogen leaving housing", "kg/head", double.NaN));
        MethaneEmissions = [];
        MethaneEmissions.Clear();
        MethaneEmissions.Add(LivestockEmissions.ManureManagementMethane, new Emission("Methane from excreta deposited in housing", "kg/head", double.NaN));
    }
    /// <summary>
    /// Export of emissions
    /// </summary>
    /// <returns>List of Emissions objects</returns>
    public List<Emission> ExportEmissions()
    {
        List<Emission> retList = [];
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

        foreach(AdditionalHousingManureManagementOutputs key in AdditionalOutputs.Keys)
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

        foreach(AdditionalHousingManureManagementOutputs key in AdditionalOutputs.Keys)
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
