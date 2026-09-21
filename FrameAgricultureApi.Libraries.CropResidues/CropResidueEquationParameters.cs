namespace FrameAgricultureApi.Libraries.CropResidues;

/// <summary>
/// Crop residue equations class (Empty)
/// </summary>
public static class CropResidueEquationParameters
{
    /// <summary>
    /// COnstant in equation to predict ammonia emissions from residue nitrogen
    /// </summary>
    public static readonly double residueNitrogenEmittedEquationconstant = -5.42;
    /// <summary>
    /// Sllope of equation to predict ammonia emissions frmo residue nitrogen
    /// </summary>
    public static readonly double residueNitrogenEmittedEquationSlope = 0.41;
}
