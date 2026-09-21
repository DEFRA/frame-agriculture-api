using FrameAgricultureApi.Generic;
using static FrameAgricultureApi.Enumerators.Enumerators;

namespace FrameAgricultureApi.Libraries.OrganicMatterApplication;

public class OrganicMatterSpreadingEmission : BaseEmissions
{
    public Dictionary<AdditionalOrganicMatterSpreadingOutputs, Emission> AdditionalOutputs { get; set; } = [];

    public void InitialiseOrganicMatterSpreadingEmissions()
    {
        InitialiseBaseEmissions();
        AdditionalOutputs = [];
        AdditionalOutputs.Clear();
        AdditionalOutputs.Add(AdditionalOrganicMatterSpreadingOutputs.NTotalNApplied, new Emission("Total N in organic matter applied", "kg/ha", double.NaN));
        AdditionalOutputs.Add(AdditionalOrganicMatterSpreadingOutputs.TANApplied, new Emission("Total Ammoniacal Nitrogen (TAN) in organic matter applied", "kg/ha", double.NaN));
        AdditionalOutputs.Add(AdditionalOrganicMatterSpreadingOutputs.OrganicNApplied, new Emission("Organic N in organic matter applied", "kg/ha", double.NaN));

    }

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

        foreach(AdditionalOrganicMatterSpreadingOutputs key in AdditionalOutputs.Keys)
        {
            if(!double.IsNaN(AdditionalOutputs[key].Value))
            {
                retList.Add(AdditionalOutputs[key]);
            }
        }
        return retList;
    }

    public void RoundEmissions(int decimalPlaces)
    {
        base.RoundCoreEmissions(decimalPlaces);

        foreach(AdditionalOrganicMatterSpreadingOutputs key in AdditionalOutputs.Keys)
        {
            if(!double.IsNaN(AdditionalOutputs[key].Value))
            {
                AdditionalOutputs[key].Value = Math.Round(AdditionalOutputs[key].Value, decimalPlaces);
            }
        }
    }
}

