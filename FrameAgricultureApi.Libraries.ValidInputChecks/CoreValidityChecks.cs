using FrameAgricultureApi.CustomErrorHandling;
using FrameAgricultureApi.Libraries.LookUps;
using static FrameAgricultureApi.Enumerators.Enumerators;

namespace FrameAgricultureApi.Libraries.ValidInputChecks;

/// <summary>
/// Class containing the checks to be done for valid calls to the FLEA endpoints
/// </summary>
public static class CoreValidityChecks
{
    /// <summary>
    /// Checks if country integer is valid
    /// </summary>
    /// <param name="country"></param>
    /// <returns>true if valid country, false otherwise</returns>
    public static bool CheckValidCountry(int country)
    {
        return Enum.IsDefined(typeof(Country), country);
    }
    /// <summary>
    /// CHecks whether a grid square is valid
    /// </summary>
    /// <param name="gridSquare"></param>
    /// <returns>true if grid square exists in the Physical Lookup list, false otherwise</returns>
    public static bool CheckValidGridSquare(int gridSquare)
    {
        return PhysicalLookup.IsValidGridSquare(gridSquare);
    }
    /// <summary>
    /// Checks if climate region is valid
    /// </summary>
    /// <param name="climateRegion">climate region ID</param>
    /// <returns>true if valid climate region, otherwise false</returns>
    public static bool CheckValidClimateRegion(int climateRegion)
    {
        return PhysicalLookup.IsValidClimateRegion(climateRegion);
    }
    /// <summary>
    /// Checks whetehr a crop type is valid
    /// </summary>
    /// <param name="cropType"></param>
    /// <returns>True if crop type exsits in enumerator, otherwise false</returns>
    public static bool CheckValidCropType(int cropType)
    {
        return Enum.IsDefined(typeof(CropType), cropType);
    }
    /// <summary>
    /// Checks whether a fertiliser type is valid
    /// </summary>
    /// <param name="fertiliserType"></param>
    /// <returns>True if fertiliser type exsits in enumerator, otherwise false</returns>
    public static bool CheckValidFertiliserType(int fertiliserType)
    {
        return Enum.IsDefined(typeof(FertiliserType), fertiliserType);
    }
    /// <summary>
    /// Checks whetr a soil type is valid
    /// </summary>
    /// <param name="soilType">soil type enumerator as integer</param>
    /// <returns>True if is valid soil type enumerator, otherwise false</returns>
    public static bool CheckValidSoilType(int soilType)
    {
        return Enum.IsDefined(typeof(SoilTextureType), soilType);
    }
    /// <summary>
    /// Checks that fertiliser array is of correct length
    /// </summary>
    /// <param name="array"></param>
    /// <returns>True if length equals 12, otherwise false</returns>
    public static bool CheckFertiliserApplicationArrayLength(double[] array)
    {
        return array.Length.Equals(12);
    }
    /// <summary>
    /// Checks to ensure that fertiliser applied is greater than zero
    /// </summary>
    /// <param name="array"></param>
    /// <returns>True if a positive amoutn of fertiliser is applied, false otherwise</returns>
    public static bool CheckFertiliserAppliedNonZero(double[] array)
    {
        return array.Sum() > 0;
    }
    /// <summary>
    /// Checks if valid organic matter tpye has been provided
    /// </summary>
    /// <param name="type">organic matter type as integer</param>
    /// <returns>True if organic matter type exsits in enumerator, otherwise false</returns>
    public static bool CheckValidOrganicMatterType(int type)
    {
        return Enum.IsDefined(typeof(OrganicMatterType), type);
    }
    /// <summary>
    /// Checks if spreading land use has been proivided correctly
    /// </summary>
    /// <param name="type">spreading land use type as integer</param>
    /// <returns>true if a valid spreading alnd use type</returns>
    public static bool CheckValidLandUse(int type)
    {
        return Enum.IsDefined(typeof(SpreadingLandUse), type);
    }
    /// <summary>
    /// Checks if animal type provided exists in the sector provided
    /// </summary>
    /// <param name="sector">Sector as enumerator</param>
    /// <param name="animalType">Animal type enumerator as integer</param>
    /// <returns>true if animal typ eexists in sector, false if not</returns>
    /// <exception cref="Exception">Sector not animal sector</exception>
    public static bool CheckValidAnimalType(Sector sector, int animalType)
    {
        return sector switch
        {
            Sector.Beef => Enum.IsDefined(typeof(BeefCattleType), animalType),
            Sector.Dairy => Enum.IsDefined(typeof(DairyCattle), animalType),
            Sector.MinorLivestock => Enum.IsDefined(typeof(MinorLivestockType), animalType),
            Sector.Pigs => Enum.IsDefined(typeof(PigType), animalType),
            Sector.Poultry => Enum.IsDefined(typeof(PoultryType), animalType),
            Sector.Sheep => Enum.IsDefined(typeof(SheepType), animalType),
            _ => throw new CustomAppException("Sector enumerator provided was not an aminal sector."),
        };
    }
    /// <summary>
    /// Checks if housing type is valid
    /// </summary>
    /// <param name="type">Housing type enumerator as integer</param>
    /// <returns>true if valid, false otherwise</returns>
    public static bool CheckValidHousingType(int type)
    {
        return Enum.IsDefined(typeof(ManureHousingSystem), type);
    }
    /// <summary>
    /// Checks if storage type is valid
    /// </summary>
    /// <param name="type">Storage type enumerator as integer</param>
    /// <returns>True if valid, false otherwise</returns>
    public static bool CheckValidStorageType(int type)
    {
        return Enum.IsDefined(typeof(ManureStorageSystem), type);
    }
    /// <summary>
    /// Checks if grass type is valid
    /// </summary>
    /// <param name="type">Grass Type enumerator as integer</param>
    /// <returns>true if valid, false otherwise</returns>
    public static bool CheckValidGrassType(int type)
    {
        return Enum.IsDefined(typeof(GrassType), type);
    }
    /// <summary>
    /// Check grass use type is valid
    /// </summary>
    /// <param name="type">Grass use type enumerator as ineger</param>
    /// <returns></returns>
    public static bool CheckValidGrassUseType(int type)
    {
        return Enum.IsDefined(typeof(GrassUseType), type);
    }
    /// <summary>
    /// Checks if polynomial coefficients have been set
    /// </summary>
    /// <param name="fracLeachCoefficients">A Grass Equation Coefficients object</param>
    /// <returns></returns>
    internal static bool CheckValidPolynomialCOefficients(GrassEquationCoefficients fracLeachCoefficients)
    {
        if(double.IsNaN(fracLeachCoefficients.C))
        {
            return false;
        }
        else if(double.IsNaN(fracLeachCoefficients.Coeff_X))
        {
            return false;
        }
        else if(double.IsNaN(fracLeachCoefficients.Coeff_X2))
        {
            return false;
        }
        else if(double.IsNaN(fracLeachCoefficients.Coeff_X3))
        {
            return false;
        }
        else if(double.IsNaN(fracLeachCoefficients.Coeff_X4))
        {
            return false;
        }
        else
        {
            return true;
        }
    }
    /// <summary>
    /// Checks that sheep subtype is valid
    /// </summary>
    /// <param name="sheepType">the sheeptye enumerator as integer</param>
    /// <param name="sheepSubType">the Sheep sub type as integer</param>
    /// <returns>true if valid, false otherwise</returns>
    internal static bool CheckValidSheepSubType(int sheepType, int sheepSubType)
    {
        bool result = false;

        switch(sheepType)
        {
            case (int)SheepType.Lamb:
                result = Enum.IsDefined(typeof(LambSubtype), sheepSubType);
                break;

            case (int)SheepType.Ewe:
                result = Enum.IsDefined(typeof(EweSubtype), sheepSubType);
                break;

            case (int)SheepType.Ram:
                result = Enum.IsDefined(typeof(RamSubtype), sheepSubType);
                break;
        }
        return result;
    }
    /// <summary>
    /// Check that sheep system can be converted to an enum
    /// </summary>
    /// <param name="sheepSystemType"></param>
    /// <returns></returns>
    internal static bool CheckValidSheepSystem(int sheepSystemType)
    {
        return Enum.IsDefined(typeof(SheepSystemType), sheepSystemType);
    }
    /// <summary>
    /// Check that the grass coefficient can be converted to an enum
    /// </summary>
    /// <param name="grassCoefficientRequired"></param>
    /// <returns></returns>
    internal static bool CheckValidGrassCoefficient(int grassCoefficientRequired)
    {
        return Enum.IsDefined(typeof(GrassEquations), grassCoefficientRequired);
    }
}
