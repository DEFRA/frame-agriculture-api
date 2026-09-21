using FrameAgricultureApi.Libraries.LookUps;

namespace FrameAgricultureApi.Components;

/// <summary>
/// Data helpers component - functions to retrieve default and core data for use in calcualtions
/// </summary>
public class DataHelpers
{
    #region Grass Helper Functions
    /// <summary>
    /// Helper Function to return full set of coefficients for all key grass output equations
    /// </summary>
    /// <param name="ClimateRegion">The Soil Climate Region as an integer</param>
    /// <param name="SoilTextureType">Soil Texture type enumerator as integer</param>
    /// <param name="GrassType">Grass type enuermator as integer</param>
    /// <param name="GrassUseType">Grass Use type enumerator as integer</param>
    /// <param name="WhiteCloverSown">Boolean indicating whetehr grass is sown with white clover</param>
    /// <param name="RecievesManagedManure">Boolean indicating if grass receives managed manure</param>
    /// <returns>A dictionaty of EquationType Enumerator as integer and array of doubles with relevant polynomial paramter values</returns>
    public static Dictionary<int, GrassEquationCoefficients> RetrieveAllGrassEquationCoefficients(int ClimateRegion, int SoilTextureType, int GrassType, int GrassUseType, bool WhiteCloverSown, bool RecievesManagedManure)
    {
        return GrassLookup.Instance.GrassModelCoefficients(ClimateRegion, SoilTextureType, GrassType, GrassUseType, WhiteCloverSown, RecievesManagedManure);
    }
    /// <summary>
    /// Helper Function to provide coefficients for estimation of residual nitrogen
    /// </summary>
    /// <param name="ClimateRegion">The Soil Climate Region as an integer</param>
    /// <param name="SoilTextureType">Soil Texture type enumerator as integer</param>
    /// <param name="GrassType">Grass type enuermator as integer</param>
    /// <param name="GrassUseType">Grass Use type enumerator as integer</param>
    /// <param name="WhiteCloverSown">Boolean indicating whetehr grass is sown with white clover</param>
    /// <param name="RecievesManagedManure">Boolean indicating if grass receives managed manure</param>
    /// <returns>The class contaning the polynomial paramter values</returns>
    public static GrassEquationCoefficients GrassModelCoefficients_ResidualNitrogen(int ClimateRegion, int SoilTextureType, int GrassType, int GrassUseType, bool WhiteCloverSown, bool RecievesManagedManure)
    {
        return GrassLookup.Instance.ResidualNitrogen(ClimateRegion, SoilTextureType, GrassType, GrassUseType, WhiteCloverSown, RecievesManagedManure);
    }
    /// <summary>
    /// Helper Function to provide coefficient for estimate of Frac Leach
    /// </summary>
    /// <param name="SoilClimateRegion">The Soil Climate Region as an integer</param>
    /// <param name="SoilTextureType">Soil Texture type enumerator as integer</param>
    /// <param name="GrassType">Grass type enuermator as integer</param>
    /// <param name="GrassUseType">Grass Use type enumerator as integer</param>
    /// <param name="WhiteCloverSown">Boolean indicating whetehr grass is sown with white clover</param>
    /// <param name="RecievesManagedManure">Boolean indicating if grass receives managed manure</param>
    /// <returns>The class contaning the polynomial paramter values</returns>
    public static GrassEquationCoefficients GrassModelCoefficients_FracLeach(int SoilClimateRegion, int SoilTextureType, int GrassType, int GrassUseType, bool WhiteCloverSown, bool RecievesManagedManure)
    {
        return GrassLookup.Instance.GrassFracLeachCoefficients(SoilClimateRegion, SoilTextureType, GrassType, GrassUseType, WhiteCloverSown, RecievesManagedManure);
    }
    /// <summary>
    /// Helper Function to provid coefficients for estimation of Yield dry matter
    /// </summary>
    /// <param name="SoilClimateRegion">The Soil Climate Region as an integer</param>
    /// <param name="SoilTextureType">Soil Texture type enumerator as integer</param>
    /// <param name="GrassType">Grass type enuermator as integer</param>
    /// <param name="GrassUseType">Grass Use type enumerator as integer</param>
    /// <param name="WhiteCloverSown">Boolean indicating whetehr grass is sown with white clover</param>
    /// <param name="RecievesManagedManure">Boolean indicating if grass receives managed manure</param>
    /// <returns>The class contaning the polynomial paramter values</returns>
    public static GrassEquationCoefficients GrassModelCoefficients_YieldDryMatter(int SoilClimateRegion, int SoilTextureType, int GrassType, int GrassUseType, bool WhiteCloverSown, bool RecievesManagedManure)
    {
        return GrassLookup.Instance.YieldDryMatter(SoilClimateRegion, SoilTextureType, GrassType, GrassUseType, WhiteCloverSown, RecievesManagedManure);
    }
    /// <summary>
    /// Helper Function to provide coefficients for estimation of above groudn dry matter
    /// </summary>
    /// <param name="SoilClimateRegion">The Soil Climate Region as an integer</param>
    /// <param name="SoilTextureType">Soil Texture type enumerator as integer</param>
    /// <param name="GrassType">Grass type enuermator as integer</param>
    /// <param name="GrassUseType">Grass Use type enumerator as integer</param>
    /// <param name="WhiteCloverSown">Boolean indicating whetehr grass is sown with white clover</param>
    /// <param name="RecievesManagedManure">Boolean indicating if grass receives managed manure</param>
    /// <returns>The class contaning the polynomial paramter values</returns>
    public static GrassEquationCoefficients GrassModelCoefficients_AboveGroundDryMatter(int SoilClimateRegion, int SoilTextureType, int GrassType, int GrassUseType, bool WhiteCloverSown, bool RecievesManagedManure)
    {
        return GrassLookup.Instance.AboveGroundDryMatter(SoilClimateRegion, SoilTextureType, GrassType, GrassUseType, WhiteCloverSown, RecievesManagedManure);
    }
    /// <summary>
    /// Helper Function to provifde coefficients for estimation of below ground dry matter
    /// </summary>
    /// <param name="SoilClimateRegion">The Soil Climate Region as an integer</param>
    /// <param name="SoilTextureType">Soil Texture type enumerator as integer</param>
    /// <param name="GrassType">Grass type enuermator as integer</param>
    /// <param name="GrassUseType">Grass Use type enumerator as integer</param>
    /// <param name="WhiteCloverSown">Boolean indicating whetehr grass is sown with white clover</param>
    /// <param name="RecievesManagedManure">Boolean indicating if grass receives managed manure</param>
    /// <returns>The class contaning the polynomial paramter values</returns>
    public static GrassEquationCoefficients GrassModelCoefficients_BelowGroundDryMatter(int SoilClimateRegion, int SoilTextureType, int GrassType, int GrassUseType, bool WhiteCloverSown, bool RecievesManagedManure)
    {
        return GrassLookup.Instance.BelowGroundDryMatter(SoilClimateRegion, SoilTextureType, GrassType, GrassUseType, WhiteCloverSown, RecievesManagedManure);
    }
    /// <summary>
    /// Helper Function to provifde coefficients for the nitrogem in grass that is eaten by livestock or cut
    /// </summary>
    /// <param name="SoilClimateRegion">The Soil Climate Region as an integer</param>
    /// <param name="SoilTextureType">Soil Texture type enumerator as integer</param>
    /// <param name="GrassType">Grass type enuermator as integer</param>
    /// <param name="GrassUseType">Grass Use type enumerator as integer</param>
    /// <param name="WhiteCloverSown">Boolean indicating whetehr grass is sown with white clover</param>
    /// <param name="RecievesManagedManure">Boolean indicating if grass receives managed manure</param>
    /// <returns>The class contaning the polynomial paramter values</returns>
    public static GrassEquationCoefficients GrassModelCoefficients_GrassNitrogenConsumedbyLivestockorCut(int SoilClimateRegion, int SoilTextureType, int GrassType, int GrassUseType, bool WhiteCloverSown, bool RecievesManagedManure)
    {
        return GrassLookup.Instance.NitrogeninGrassCutorEaten(SoilClimateRegion, SoilTextureType, GrassType, GrassUseType, WhiteCloverSown, RecievesManagedManure);
    }
    /// <summary>
    /// Helper Function to provifde coefficients for the nitrogen due to clover fixation
    /// </summary>
    /// <param name="climateRegion">The Soil Climate Region as an integer</param>
    /// <param name="soilTextureType">Soil Texture type enumerator as integer</param>
    /// <param name="grassType">Grass type enuermator as integer</param>
    /// <param name="grassUseType">Grass Use type enumerator as integer</param>
    /// <param name="whiteCloverSown">Boolean indicating whetehr grass is sown with white clover</param>
    /// <param name="recievesManagedManure">Boolean indicating if grass receives managed manure</param>
    /// <returns>The class contaning the polynomial paramter values</returns>
    public static GrassEquationCoefficients GrassModelCoefficients_NitrogenfromCloverFixation(int climateRegion, int soilTextureType, int grassType, int grassUseType, bool whiteCloverSown, bool recievesManagedManure)
    {
        return GrassLookup.Instance.NitrogenFromCloverFixation(climateRegion, soilTextureType, grassType, grassUseType, whiteCloverSown, recievesManagedManure);
    }
    #endregion

    #region Sheep Helper Functions
    /// <summary>
    /// Retrieval of sheep energy balance model outputs
    /// </summary>
    /// <param name="country">Country enumerator as integer</param>
    /// <param name="sheepType">Sheep type enumerator as integer</param>
    /// <param name="sheepsubtype">Sheep subtype enumerator as integer</param>
    /// <param name="sheepsystemtype">Sheep system type enumerator as integer</param>
    /// <returns></returns>
    public static SheepEnergyBalance SheepEnergyBalanceOutputs(int country, int sheepType, int sheepsubtype, int sheepsystemtype)
    {
        return SheepLookup.Instance.EnergyParameters(country, sheepType, sheepsubtype, sheepsystemtype);
    }
    /// <summary>
    /// Retrieval of first winter manure frac leach coefficient
    /// </summary>
    /// <param name="cell">UK 10 km grid cell ID</param>
    /// <param name="soiltype">soil type enumerator as integer</param>
    /// <returns></returns>
    public static double FirstWinterManureFracLeach(int cell, int soiltype)
    {
        return SheepLookup.Instance.SheepFirstWinterManureFracLeach(cell, soiltype);
    }

    #endregion
}

