using FrameAgricultureApi.Libraries.CropBurning;
using FrameAgricultureApi.Libraries.LookUps;
using static FrameAgricultureApi.Enumerators.Enumerators;

namespace FrameAgricultureApi.Components;

/// <summary>
/// The crop burning component class
/// </summary>
public static class CropBurning
{
    /// <summary>
    /// Calculation of emissions from crop burning
    /// </summary>
    /// <param name="residueDryMatterBurned">The kg of residue dry matter burned</param>
    /// <param name="cropType">Enumerator indicating the crop type</param>
    /// <returns>Object of type CropBurningEmissions</returns>
    public static CropBurningEmissions CropBurningEmissions(double residueDryMatterBurned, CropType cropType)
    {
        CropBurningEmissions myEmissions = new();
        myEmissions.InitialiseCropBurningEmissions();

        myEmissions.CombustionEmissions[CombustionEmissions.ResidueDryMatterBurned].Value = residueDryMatterBurned;
        myEmissions.CombustionEmissions[CombustionEmissions.BurningEfficiency].Value = CropLookup.RetrieveCombustionEfficiency(cropType);
        myEmissions.CombustionEmissions[CombustionEmissions.CombustionCO].Value = residueDryMatterBurned / 1000.0 * CropLookup.RetrieveCombustionCO(cropType) * CropLookup.RetrieveCombustionEfficiency(cropType);
        myEmissions.CombustionEmissions[CombustionEmissions.CombustionCH4].Value = residueDryMatterBurned / 1000.0 * CropLookup.RetrieveCombustionCH4(cropType) * CropLookup.RetrieveCombustionEfficiency(cropType);
        myEmissions.CombustionEmissions[CombustionEmissions.CombustionN2ON].Value = residueDryMatterBurned / 1000.0 * CropLookup.RetrieveCombustionN2ON(cropType) * CropLookup.RetrieveCombustionEfficiency(cropType);
        myEmissions.CombustionEmissions[CombustionEmissions.CombustionNH3N].Value = residueDryMatterBurned / 1000.0 * CropLookup.RetrieveCombustionNH3N(cropType) * CropLookup.RetrieveCombustionEfficiency(cropType);
        myEmissions.CombustionEmissions[CombustionEmissions.CombustionNMVOC].Value = residueDryMatterBurned / 1000.0 * CropLookup.RetrieveCombustionNMVOC(cropType) * CropLookup.RetrieveCombustionEfficiency(cropType);
        myEmissions.CombustionEmissions[CombustionEmissions.CombustionNOxN].Value = residueDryMatterBurned / 1000.0 * CropLookup.RetrieveCombustionNOxN(cropType) * CropLookup.RetrieveCombustionEfficiency(cropType);
        myEmissions.CombustionEmissions[CombustionEmissions.CombustionPM10].Value = residueDryMatterBurned / 1000.0 * CropLookup.RetrieveCombustionPM10(cropType) * CropLookup.RetrieveCombustionEfficiency(cropType);
        myEmissions.CombustionEmissions[CombustionEmissions.CombustionPM2_5].Value = residueDryMatterBurned / 1000.0 * CropLookup.RetrieveCombustionPM2_5(cropType) * CropLookup.RetrieveCombustionEfficiency(cropType);
        myEmissions.CombustionEmissions[CombustionEmissions.CombustionSO2].Value = residueDryMatterBurned / 1000.0 * CropLookup.RetrieveCombustionSO2(cropType) * CropLookup.RetrieveCombustionEfficiency(cropType);
        myEmissions.CombustionEmissions[CombustionEmissions.CombustionTSP].Value = residueDryMatterBurned / 1000.0 * CropLookup.RetrieveCombustionTSP(cropType) * CropLookup.RetrieveCombustionEfficiency(cropType);

        return myEmissions;

    }
}
