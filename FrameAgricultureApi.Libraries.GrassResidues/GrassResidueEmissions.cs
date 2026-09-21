using FrameAgricultureApi.Generic;
using static FrameAgricultureApi.Enumerators.Enumerators;

namespace FrameAgricultureApi.Libraries.GrassResidues;

public class GrassResidueEmissions : BaseEmissions
{
    /// <summary>
    /// Dictionary of additional outputs in redicue emission calculations
    /// </summary>
    public Dictionary<AdditionalGrassResidueEmissions, Emission> AdditionalOutputs { get; set; } = [];

    /// <summary>
    /// Initialisation method
    /// </summary>
    public void InitialiseGrassResidueEmissions()
    {
        InitialiseBaseEmissions();
        AdditionalOutputs = [];
        AdditionalOutputs.Clear();
        AdditionalOutputs.Add(AdditionalGrassResidueEmissions.DryMatterOfftake, new Emission("Yield Dry Matter Offtake", "kg", double.NaN));
        AdditionalOutputs.Add(AdditionalGrassResidueEmissions.AboveGroundResidueDryMatter, new Emission("Above Ground Residue Dry Matter", "kg", double.NaN));
        //additionalOutputs.Add(AdditionalGrassResidueEmissions.AboveGroundResidueDryMatterN, new Emission("Above Ground Residue Dry Matter N", "kg", double.NaN));
        AdditionalOutputs.Add(AdditionalGrassResidueEmissions.BelowGroundResidueDryMatter, new Emission("Below Ground Residue Dry Matter", "kg", double.NaN));
        //additionalOutputs.Add(AdditionalGrassResidueEmissions.BelowGroundResidueDryMatterN, new Emission("Below Ground Residue Dry Matter N", "kg", double.NaN));
        AdditionalOutputs.Add(AdditionalGrassResidueEmissions.RenewalResidueNitrogen, new Emission("Nitrogen in Residues from Grass Renewal", "kg N", double.NaN));
        AdditionalOutputs.Add(AdditionalGrassResidueEmissions.GrassFracLeach, new Emission("Frac Leach for Grass Residues", "%", double.NaN));
        AdditionalOutputs.Add(AdditionalGrassResidueEmissions.NitrogeninGrassHarvested, new Emission("Nitrogen in grass harvested", "kg", double.NaN));
        AdditionalOutputs.Add(AdditionalGrassResidueEmissions.NitrogenfromCloverfixation, new Emission("Nitrogen from clover fixation", "kg", double.NaN));
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

        foreach(AdditionalGrassResidueEmissions key in AdditionalOutputs.Keys)
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

        foreach(AdditionalGrassResidueEmissions key in AdditionalOutputs.Keys)
        {
            if(!double.IsNaN(AdditionalOutputs[key].Value))
            {
                AdditionalOutputs[key].Value = Math.Round(AdditionalOutputs[key].Value, decimalPlaces);
            }
        }
    }
}
