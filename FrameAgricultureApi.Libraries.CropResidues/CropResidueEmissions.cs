using FrameAgricultureApi.Generic;
using static FrameAgricultureApi.Enumerators.Enumerators;

namespace FrameAgricultureApi.Libraries.CropResidues;

/// <summary>
/// Crop residue emissions class
/// </summary>
public class CropResidueEmissions : BaseEmissions
{
    /// <summary>
    /// Dictionary of additional outputs in redicue emission calculations
    /// </summary>
    public Dictionary<AdditionalCropResidueEmissions, Emission> AdditionalOutputs { get; set; } = [];

    /// <summary>
    /// Initialisation method
    /// </summary>
    public void InitialiseCropResidueEmissions()
    {
        InitialiseBaseEmissions();
        AdditionalOutputs = [];
        AdditionalOutputs.Clear();
        AdditionalOutputs.Add(AdditionalCropResidueEmissions.DryMatterOfftake, new Emission("Dry Matter Offtake", "kg", double.NaN));
        AdditionalOutputs.Add(AdditionalCropResidueEmissions.AboveGroundResidueDryMatter, new Emission("Above Ground Residue Dry Matter", "kg", double.NaN));
        AdditionalOutputs.Add(AdditionalCropResidueEmissions.AboveGroundResidueDryMatterN, new Emission("Above Ground Residue Dry Matter N", "kg", double.NaN));
        AdditionalOutputs.Add(AdditionalCropResidueEmissions.BelowGroundResidueDryMatter, new Emission("Below Ground Residue Dry Matter", "kg", double.NaN));
        AdditionalOutputs.Add(AdditionalCropResidueEmissions.BelowGroundResidueDryMatterN, new Emission("Below Ground Residue Dry Matter N", "kg", double.NaN));
        AdditionalOutputs.Add(AdditionalCropResidueEmissions.NitrogeninHarvestedYield, new Emission("Nitrogen in yield removed", "kg", double.NaN));
        AdditionalOutputs.Add(AdditionalCropResidueEmissions.NitrogeninStrawRemoved, new Emission("Nitrogen in residues removed", "kg", double.NaN));
    }

    /// <summary>
    /// EMissoin expoert method
    /// </summary>
    /// <returns>List of emissions objects</returns>
    public List<Emission> ExportEmissions()
    {
        List<Emission> retList = new List<Emission>();
        foreach(CoreEmissions key in EmissionsCore.Keys)
        {
            if(!double.IsNaN(EmissionsCore[key].Value))
            {
                retList.Add(EmissionsCore[key]);
            }
        }

        foreach(AdditionalCropResidueEmissions key in AdditionalOutputs.Keys)
        {
            if(!double.IsNaN(AdditionalOutputs[key].Value))
            {
                retList.Add(AdditionalOutputs[key]);
            }
        }
        return retList;
    }
    /// <summary>
    /// Method to round emissions to a fixed number of decimal places
    /// </summary>
    /// <param name="decimalPlaces">Decimal places to be rounded to</param>
    public void RoundEmissions(int decimalPlaces)
    {
        base.RoundCoreEmissions(decimalPlaces);

        foreach(AdditionalCropResidueEmissions key in AdditionalOutputs.Keys)
        {
            if(!double.IsNaN(AdditionalOutputs[key].Value))
            {
                AdditionalOutputs[key].Value = Math.Round(AdditionalOutputs[key].Value, decimalPlaces);
            }
        }
    }
}
