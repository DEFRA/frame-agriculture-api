using FrameAgricultureApi.Generic;
using static FrameAgricultureApi.Enumerators.Enumerators;

namespace FrameAgricultureApi.Libraries.Storage;

public class StorageEmissions : BaseEmissions
{
    public Dictionary<AdditionalStorageManureManagementOutputs, Emission> AdditionalOutputs { get; set; } = [];
    public Dictionary<LivestockEmissions, Emission> MethaneOutputs { get; set; } = [];
    public OrganicMatterType TypeIn { get; set; } = OrganicMatterType.NotSet;
    public OrganicMatterType TypeOut { get; set; } = OrganicMatterType.NotSet;

    /// <summary>
    /// Initialisation method for housing emissions
    /// </summary>
    public void InitialiseStorageEmissions()
    {

        InitialiseBaseEmissions();
        AdditionalOutputs = [];
        AdditionalOutputs.Clear();
        AdditionalOutputs.Add(AdditionalStorageManureManagementOutputs.TotalNin, new Emission("Total Nitrogen enteringstorage", "kg/head", double.NaN));
        AdditionalOutputs.Add(AdditionalStorageManureManagementOutputs.TANin, new Emission("Total Ammoniacal Nitrogen entering storage", "kg/head", double.NaN));
        AdditionalOutputs.Add(AdditionalStorageManureManagementOutputs.OrganicNin, new Emission("Organic Nitrogen entering storage", "kg/head", double.NaN));
        AdditionalOutputs.Add(AdditionalStorageManureManagementOutputs.TotalNout, new Emission("Total Nitrogen exiting storage", "kg/head", double.NaN));
        AdditionalOutputs.Add(AdditionalStorageManureManagementOutputs.TANout, new Emission("Total Ammoniacal Nitrogen exiting storage", "kg/head", double.NaN));
        AdditionalOutputs.Add(AdditionalStorageManureManagementOutputs.OrganicNout, new Emission("Organic Nitrogen exiting storage", "kg/head", double.NaN));
        AdditionalOutputs.Add(AdditionalStorageManureManagementOutputs.NMineralised, new Emission("Nitrogen mineralised in storage", "kg/head", double.NaN));

        MethaneOutputs.Add(LivestockEmissions.ManureManagementMethane, new Emission("Methane emission from storage", "kg/head", double.NaN));
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

        foreach(LivestockEmissions key in MethaneOutputs.Keys)
        {
            if(!double.IsNaN(MethaneOutputs[key].Value))
            {
                retList.Add((Emission)MethaneOutputs[key]);
            }
        }

        foreach(AdditionalStorageManureManagementOutputs key in AdditionalOutputs.Keys)
        {
            if(!double.IsNaN(AdditionalOutputs[key].Value))
            {
                retList.Add(AdditionalOutputs[key]);
            }
        }

        retList.Add(new Emission("Manure Type entering storage", TypeIn.ToString(), (double)(int)TypeIn));
        retList.Add(new Emission("Manure Type leaving storage", TypeOut.ToString(), (double)(int)TypeOut));

        return retList;
    }
    /// <summary>
    /// Round emissions to a fixed number of decimal places
    /// </summary>
    /// <param name="decimalPlaces">Number of decimal places to round to</param>
    public void RoundEmissions(int decimalPlaces)
    {
        base.RoundCoreEmissions(decimalPlaces);

        foreach(AdditionalStorageManureManagementOutputs key in AdditionalOutputs.Keys)
        {
            if(!double.IsNaN(AdditionalOutputs[key].Value))
            {
                AdditionalOutputs[key].Value = Math.Round(AdditionalOutputs[key].Value, decimalPlaces);
            }
        }

        foreach(LivestockEmissions key in MethaneOutputs.Keys)
        {
            if(!double.IsNaN(MethaneOutputs[key].Value))
            {
                MethaneOutputs[key].Value = Math.Round(MethaneOutputs[key].Value, decimalPlaces);
            }
        }
    }
}
