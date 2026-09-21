namespace FrameAgricultureApi.Libraries.ManureMassVolume;

public class ManureMassVolumeGenericEquationParameters
{
    public static readonly double faecesDryMatterContent = 0.001 * 167.0; //kg per kg (value is 167g per kg)

    public static readonly double faecesMasstoVolumeRatio = 0.160;

    public static readonly double urinePerUnitDryMatter = 1.12;

    public static readonly double urineConstant = 1.139;

    public static readonly double urineCoefficient = 0.00046;

    public static readonly double urineDryMatterEffectThreshold = 300.0; //kg dry matter above which impact on urine production

    public static readonly double cattleUrineSpecificDensity = 1.03; // kg per litre

    public static readonly double reductionFYMComposting = 28.0; //percentage reduction in FYM mass and volume due to the composting processes

    public static readonly double slurryDilutionRatio = 1.6;

    public static readonly double strawUrineAbsorbance = 2.0; // g per g of straw for absorbance of urine

    public static readonly double bulkDensityCompressedStraw = 80.0;

    public static readonly double cattleAnaerobicDigestionMassLossAdjustment = 0.85;

}
