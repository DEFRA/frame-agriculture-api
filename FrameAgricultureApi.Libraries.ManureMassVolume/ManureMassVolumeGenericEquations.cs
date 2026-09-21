using FrameAgricultureApi.Generic;

namespace FrameAgricultureApi.Libraries.ManureMassVolume;

public static class ManureMassVolumeGenericEquations
{
    /// <summary>
    /// MMV_2 Cattle faeces dry matter production (𝐹𝐷𝑀𝐼) per unit of dry matter intake (kg kg-1).
    /// </summary>
    /// <param name="wholeDryMatterDigestibility"></param>
    /// <returns></returns>
    public static double FaecesDryMatter(double wholeDryMatterDigestibility)
    {
        return 1.0 - (wholeDryMatterDigestibility * HelperFunctions.Percent_to_Proportion);
    }
    /// <summary>
    /// MMV_3 The mass of faeces, including faecal water, produced daily (𝑀𝐹) in kilograms (kg).
    /// </summary>
    /// <param name="intakeDryMatter"></param>
    /// <param name="faecesDryMatter"></param>
    /// <returns></returns>
    public static double CattleFaecesMass(double intakeDryMatter, double faecesDryMatter)
    {
        return intakeDryMatter * faecesDryMatter * (1.0 / ManureMassVolumeGenericEquationParameters.faecesDryMatterContent);
    }
    /// <summary>
    /// MMV_4 The volume of faeces, including faecal water, produced daily (𝑉𝐹) in litres.
    /// </summary>
    /// <param name="intakeDryMatter"></param>
    /// <param name="faecesDryMatter"></param>
    /// <returns></returns>
    public static double CattleFaecesVolume(double intakeDryMatter, double faecesDryMatter)
    {
        return (intakeDryMatter * faecesDryMatter * (1.0 / ManureMassVolumeGenericEquationParameters.faecesMasstoVolumeRatio));
    }
    /// <summary>
    /// MMV_5 Mass of urine per unit dry matter production (𝑈𝐷𝑀𝐼) in kg kg-1.
    /// </summary>
    /// <param name="drymattercontent"></param>
    /// <returns></returns>
    public static double CattleUrineDryMatter(double drymattercontent)
    {
        return ManureMassVolumeGenericEquationParameters.urinePerUnitDryMatter * (ManureMassVolumeGenericEquationParameters.urineConstant -
            ((drymattercontent - ManureMassVolumeGenericEquationParameters.urineDryMatterEffectThreshold) * ManureMassVolumeGenericEquationParameters.urineCoefficient));
    }
    /// <summary>
    /// MMV_6 The mass of urine produced daily (𝑀𝑈) in kilograms (kg) by cattle.
    /// </summary>
    /// <param name="intakeDryMatter"></param>
    /// <param name="urineDryMatter"></param>
    /// <returns></returns>
    public static double CattleUrineMass(double intakeDryMatter, double urineDryMatter)
    {
        return (intakeDryMatter * urineDryMatter);
    }
    /// <summary>
    /// MMV_7 The volume of urine (Vu)produced daily is estimated from the urine mass by using a measured average specific density of 1.03 kg l-1 (Holter and Urban, 1992).
    /// </summary>
    /// <param name="urineMass"></param>
    /// <returns></returns>
    public static double CattleUrineVolume(double urineMass)
    {
        return urineMass / ManureMassVolumeGenericEquationParameters.cattleUrineSpecificDensity;
    }
    /// <summary>
    /// MMV_8 and MMV_9 This equation will calculate the excreta mass (Me) and volume (Ve) at each stage; grazing, yarding (feeding and collecting) and housing.
    /// </summary>
    /// <param name="faecesMassVolume"></param>
    /// <param name="urineMassVolume"></param>
    /// <param name="percentTimeinLocation"></param>
    /// <returns></returns>
    public static double CattleExcretaMassVolume(double faecesMassVolume, double urineMassVolume, double percentTimeinLocation)
    {
        return (faecesMassVolume + urineMassVolume) * (percentTimeinLocation * HelperFunctions.Percent_to_Proportion);
    }
}
