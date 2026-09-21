using FrameAgricultureApi.Libraries.GrassResidues;
using FrameAgricultureApi.Libraries.LookUps;
using static FrameAgricultureApi.Enumerators.Enumerators;

namespace FrameAgricultureApi.IOClasses;

/// <summary>
/// Inputs for calculation of emissions from crop residues using a Harvest Index
/// </summary>
public class CropResidueInputs_HarvestIndex
{
    /// <summary>
    /// Fresh weight yield of crop in tonnes (double)
    /// </summary>
    public double YieldFreshWeight { get; set; }
    /// <summary>
    /// Dry matter content of crop as a percentage (double)
    /// </summary>
    public double CropDryMatterContent { get; set; }
    /// <summary>
    /// Crop harvest index (double)
    /// </summary>
    public double CropHarvestIndex
    {
        get; set;
    }
    /// <summary>
    /// Boolean indicating whether crop residue is incorporated or not.
    /// (Not is equivalent to baled and removed)
    /// </summary>
    public bool Incorporated { get; set; }
    /// <summary>
    /// The crop type (enum)
    /// </summary>
    public CropType CropType { get; set; }
    /// <summary>
    /// The 10km UK grid square ID (integer)
    /// </summary>
    /// <summary>
    /// The list of mitigation method IDs
    /// </summary>
    public int[]? MitigationMethods { get; set; }
}
/// <summary>
/// Inputs for calculation of emissions from crop residues using IPCC methodology
/// </summary>
public class CropResidueInputs_IPCC
{
    /// <summary>
    /// Fresh weight yield of crop in tonnes (double)
    /// </summary>
    public double YieldFreshWeight { get; set; }
    /// <summary>
    /// Dry matter content of crop as a percentage (double)
    /// </summary>
    public double CropDryMatterContent { get; set; }
    /// <summary>
    /// Slope of IPCC equation (double)
    /// </summary>
    public double SlopeIPCC
    {
        get; set;
    }
    /// <summary>
    /// Intercept of IPCC equation (double)
    /// </summary>
    public double InterceptIPCC
    {
        get; set;
    }
    /// <summary>
    /// Boolean indicating whether crop residue is incorporated or not.
    /// (Not is equivalent to baled and removed)
    /// </summary>
    public bool Incorporated { get; set; }
    /// <summary>
    /// The crop type (enum)
    /// </summary>
    public CropType CropType { get; set; }
    /// <summary>
    /// The 10km UK grid square ID (integer)
    /// </summary>
    /// <summary>
    /// The list of mitigation method IDs
    /// </summary>
    public int[]? MitigationMethods { get; set; }
}

/// <summary>
/// Inputs fro calcualtion of non-GHG emissions from crops
/// </summary>
public class NonGHGInputsCrop
{
    /// <summary>
    /// The crop type (integer value of enumerator)
    /// </summary>
    public CropType CropType { get; set; }
}
/// <summary>
/// Inputs for calculation of non GHG emissions from cattle
/// </summary>
public class NonGHGInputsLivestock_Cattle
{
    /// <summary>
    /// Sector enumerator (Dairy or beef)
    /// </summary>
    public Sector Sector { get; set; }
    /// <summary>
    /// Animal type enumerator (as integer)
    /// </summary>
    public int AnimalType { get; set; }
    /// <summary>
    /// Monthyl gross energy intake (MJ)
    /// </summary>
    public double MonthlyGrossEnergyIntake { get; set; }
    /// <summary>
    /// Percentage of month spent on yards or in housing
    /// </summary>
    public double PercentTimeHousingYards { get; set; }
    /// <summary>
    /// Monthly emission of ammonia from housing for animal type (kg)
    /// </summary>
    public double MonthlyAmmoniaHousing { get; set; }
    /// <summary>
    /// Monthly emission of ammonia from storage for animal type (kg)
    /// </summary>
    public double MonthlyAmmoniaStorage { get; set; }
    /// <summary>
    /// Monthly emission of ammonia from spreading for animal type (kg)
    /// </summary>
    public double MonthlyAmmoniaSpreading { get; set; }
}
/// <summary>
/// Non GHG INputs for livestock other than cattle
/// </summary>
public class NonGHGInputsLivestock_NonCattle
{
    /// <summary>
    /// Sector enumerator
    /// </summary>
    public Sector Sector { get; set; }
    /// <summary>
    /// Animal type enumerator (as integer)
    /// </summary>
    public int AnimalType { get; set; }
    /// <summary>
    /// Annual volatile solids excretion for animal type(kg)
    /// </summary>
    public double VolatileSolidsExcretion { get; set; }
    /// <summary>
    /// Number of days spent in housing in the year
    /// </summary>
    public double HouseDays { get; set; }
    /// <summary>
    /// Annual emission of ammonia from storage for animal type (kg)
    /// </summary>
    public double AmmoniaHousing { get; set; }
    /// <summary>
    /// Annual emission of ammonia from storage for animal type (kg)
    /// </summary>
    public double AmmoniaStorage { get; set; }
    /// <summary>
    /// Annual emission of ammonia from spreading for animal type (kg)
    /// </summary>
    public double AmmoniaSpreading { get; set; }
}

/// <summary>
/// Inputs for calcualtion of non-GHG emissions from grass
/// </summary>
public class NonGHGInputsGrass
{
    /// <summary>
    /// The grass type (integer value of enumerator)
    /// </summary>
    public GrassType GrassType { get; set; }
}
/// <summary>
/// Fertiliser input class
/// </summary>
public class FertiliserInput
{
    /// <summary>
    /// The fertiliser type
    /// </summary>
    public FertiliserType FertiliserType { get; set; }

    /// <summary>
    /// The total amount of N applied to the crop in kilograms per hectare
    /// </summary>
    public double TotalNApplied { get; set; }

    /// <summary>
    /// The percentage of the total N applied attributed to a specific fertiliser type
    /// </summary>
    public double PercentageFertiliserType { get; set; }

    /// <summary>
    /// The 10km grid square in the UK containing the farm or field
    /// </summary>
    public int GridSquare { get; set; }

    /// <summary>
    /// A flag indicating whether the soil is acidic or not.
    /// </summary>
    public bool AcidicSoil { get; set; }

    /// <summary>
    /// A dictionary providing the month of application and rate of N applied (kg.ha) for all applications of a given fertilier type.
    /// </summary>
    public required double[] FertiliserApplications { get; set; }
    /// <summary>
    /// Mitigation method UIDs as a list of integers
    /// </summary>
    public int[]? MitigationMethods { get; set; }
}
/// <summary>
/// Fertiliser to grass input class
/// </summary>
public class GrassFertiliserInput
{
    /// <summary>
    /// The fertiliser type
    /// </summary>
    public FertiliserType FertiliserType { get; set; }

    /// <summary>
    /// The total amount of N applied to the crop in kilograms per hectare
    /// </summary>
    public double TotalNApplied { get; set; }

    /// <summary>
    /// The percentage of the total N applied attributed to a specific fertiliser type
    /// </summary>
    public double PercentageFertiliserType { get; set; }

    /// <summary>
    /// The 10km grid square in the UK containing the farm or field
    /// </summary>
    public int GridSquare { get; set; }

    /// <summary>
    /// Soil type enumerator as integer
    /// </summary>
    public int SoilType { get; set; }
    /// <summary>
    /// Grass type enumerator as intger
    /// </summary>
    public int GrassType { get; set; }
    /// <summary>
    /// Grass use type enumerator as integer
    /// </summary>
    public int GrassUseType { get; set; }
    /// <summary>
    /// Clover sown in grasss sward
    /// </summary>
    public bool SownWithClover { get; set; }
    /// <summary>
    /// Grass receives managed manure
    /// </summary>
    public bool ReceivesManagedManure { get; set; }
    /// <summary>
    /// A flag indicating whether the soil is acidic or not.
    /// </summary>
    public bool AcidicSoil { get; set; }
    /// <summary>
    /// A dictionary providing the month of application and rate of N applied (kg.ha) for all applications of a given fertilier type.
    /// </summary>
    public required double[] FertiliserApplications { get; set; }
    /// <summary>
    /// Mitigation method UIDs as a list of integers
    /// </summary>
    public int[]? MitigationMethods { get; set; }
}
/// <summary>
/// Grass residue input class
/// </summary>
public class GrassProductionandRenewalInput
{
    /// <summary>
    /// The total amount of N applied to the crop in kilograms per hectare
    /// </summary>
    public double TotalNApplied { get; set; }
    /// <summary>
    /// The 10km grid square in the UK containing the farm or field
    /// </summary>
    public int GridSquare { get; set; }
    /// <summary>
    /// Soil type enumerator as integer
    /// </summary>
    public int SoilType { get; set; }
    /// <summary>
    /// Grass type enumerator as intger
    /// </summary>
    public int GrassType { get; set; }
    /// <summary>
    /// Grass use type enumerator as integer
    /// </summary>
    public int GrassUseType { get; set; }
    /// <summary>
    /// Clover sown in grasss sward
    /// </summary>
    public bool SownWithClover { get; set; }
    /// <summary>
    /// Grass receives managed manure
    /// </summary>
    public bool ReceivesManagedManure { get; set; }
    /// <summary>
    /// Genetic Gain Scalars class
    /// </summary>
    public GeneticGainScalars? GeneticGainScalars { get; set; }
    /// <summary>
    /// Mitigation method UIDs as a list of integers
    /// </summary>
    public int[]? MitigationMethods { get; set; }
}
/// <summary>
/// Inputs for calculation of emissions from crop burning
/// </summary>
public class CropBurningInputs
{

    /// <summary>
    /// The crop type (integer value of enumerator)
    /// </summary>
    public CropType CropType { get; set; }

    /// <summary>
    /// residue dry matter burned (kg per hectare)
    /// </summary>
    public double ResidueDryMatterBurned { get; set; }
}
/// <summary>
/// Inputs fro calcualtion of emissions from cover crop use
/// </summary>
public class CoverCropInputs
{
    /// <summary>
    /// The crop type (integer value of enumerator)
    /// </summary>
    public CropType CropType { get; set; }

    /// <summary>
    /// Cover Crop Type (integer value of enumerator)
    /// </summary>
    public CoverCropType CoverCropType { get; set; }
}
/// <summary>
/// Inputs for calculation of emissions from applicatoin of organic matter to land
/// </summary>
public class OrganicMatterApplicationInputs
{
    /// <summary>
    /// Enumerator specifying the Organic Matter Type
    /// </summary>
    public OrganicMatterType OrganicMatterType { get; set; }

    /// <summary>
    /// Quantity of organic matter applied in kg per hectare
    /// </summary>
    public double QuantityApplied { get; set; }

    /// <summary>
    /// The total N content of the organic matter in kg per kg
    /// </summary>
    public double? ContentN { get; set; }

    /// <summary>
    /// Percetnage of N in organic matter that is TAN
    /// </summary>
    public double? PercentTAN { get; set; }
    /// <summary>
    /// Boolean set to true if organic matter is applied to arable crops, false if applied to grass crops
    /// </summary>
    public SpreadingLandUse LandAppliedTo { get; set; }
    /// <summary>
    /// The list of mitigaiton methods to be applied to organic matter spreading
    /// </summary>
    public int[]? MitigationMethods { get; set; }
    /// <summary>
    /// Enumerator specifiying month, where 0 = January and 11 = December
    /// </summary>
    public Month Month { get; set; }
}
/// <summary>
/// Class containing inputs for livestock manure applications to land
/// </summary>
public class LivestockManureApplicationInputs
{
    /// <summary>
    /// Enumerator specifying the Organic Matter Type
    /// </summary>
    public OrganicMatterType OrganicMatterType { get; set; }
    /// <summary>
    /// The total N content in manure applied (kg/ha)
    /// </summary>
    public double TotalN { get; set; }
    /// <summary>
    /// Total available nitrogen in manure applied (kg/ha)
    /// </summary>
    public double TAN { get; set; }
    /// <summary>
    /// Land use applied to enumerator
    /// </summary>
    public SpreadingLandUse LandAppliedTo { get; set; }
    /// <summary>
    /// The list of mitigaiton methods to be applied to organic matter spreading
    /// </summary>
    public int[]? MitigationMethods { get; set; }
    /// <summary>
    /// Enumerator specifiying month, where 0 = January and 11 = December
    /// </summary>
    public Month? Month { get; set; }
}
/// <summary>
/// Class containing inputs for livestock manure applications to land
/// </summary>
public class SheepManureApplicationInputs
{
    /// <summary>
    /// The total N content in manure applied (kg/ha)
    /// </summary>
    public double TotalN { get; set; }
    /// <summary>
    /// Total available nitrogen in manure applied (kg/ha)
    /// </summary>
    public double TAN { get; set; }
    /// <summary>
    /// Land use applied to enumerator
    /// </summary>
    public int[]? MitigationMethods { get; set; }
    /// <summary>
    /// Enumerator specifiying month, where 0 = January and 11 = December
    /// </summary>
    public Month? Month { get; set; }
    /// <summary>
    /// The first winter manure frac leach (as a percentage)
    /// </summary>
    public double FristWinterManureFracLeach { get; set; }
    /// <summary>
    /// Coefficients for calculatin of manure frac leach
    /// </summary>
    public required GrassEquationCoefficients ManureFracLeachCoefficients { get; set; }
    /// <summary>
    /// The rate of applicatino of synthetic fertilisers to grass (kg N/ha)
    /// </summary>
    public double GrassFetiliserRate { get; set; }
    /// <summary>
    /// genetic gain scalar for frac leach in spread sheep manure
    /// </summary>
    public double? GeneticManuredFracLeachScalar { get; set; }
}
/// <summary>
/// Base class for inputs from excreat deposited outdoors
/// </summary>
public class BaseOutdoorExcretaInputs
{
    /// <summary>
    /// Sector in which emission are being calculated
    /// </summary>
    public Sector MySector { get; set; }
    /// <summary>
    /// Animal type enumerator as integer
    /// </summary>
    public int AnimalType { get; set; }
    /// <summary>
    /// Total nitrogen in excreta deposited outdoors (kg)
    /// </summary>
    public double TotalNitrogen { get; set; }
    /// <summary>
    /// Total Ammoniacal nitroge in excfreta deposited outdoors (kg)
    /// </summary>
    public double TotalAmmoniacalNitrogen { get; set; }
    /// <summary>
    /// Volatile solids in excreta deposited outdoors (kg)
    /// </summary>
    public double VolatileSolids { get; set; }
}
/// <summary>
/// Class for inputs from cattle urine and dung deposited outdoors
/// </summary>
public class CattleOutdoorExcretaInputs
{
    /// <summary>
    /// Cattle sector (Dairy or Beef)
    /// </summary>
    public Sector MySector { get; set; }
    /// <summary>
    /// Animal type enumerator as integer
    /// </summary>
    public int AnimalType { get; set; }
    /// <summary>
    /// Nitrogen in urine deposited outdoors (kg)
    /// </summary>
    public double UrineNitrogen { get; set; }
    /// <summary>
    /// Nitrogen in dung deposited outdoors
    /// </summary>
    public double DungNitrogen { get; set; }
    /// <summary>
    /// Volatile solids in excreta deposited outdoors (kg)
    /// </summary>
    public double VolatileSolids { get; set; }
}
/// <summary>
/// Inputs for sheep excreta deposited outdoors
/// </summary>
public class SheepOutdoorExcretaInputs
{
    /// <summary>
    /// Sheep energy balance model output
    /// </summary>
    public required SheepEnergyBalance EnergyBalance { get; set; }
    /// <summary>
    /// Sector (sheep)
    /// </summary>
    public Sector MySector { get; set; }
    /// <summary>
    /// Animal type enumerator as integer
    /// </summary>
    public int AnimalType { get; set; }

    /// <summary>
    /// Total nitrogen rate applied in fertiliser to grass (kg/ha)
    /// </summary>
    public double FertiliserRate { get; set; }
    /// <summary>
    /// Grass equation Coefficients for frac leach
    /// </summary>
    public required GrassEquationCoefficients FracLeachCoefficients { get; set; }
    /// <summary>
    /// Grass Equation Coefficients for Grazing Nitrogen
    /// </summary>
    public required GrassEquationCoefficients GrazingNitrogen { get; set; }
    /// <summary>
    /// Genetic gain scalar for frac leach on grazed grass
    /// </summary>
    public double GeneticGrazedFracLeachScalar { get; set; }
    /// <summary>
    /// Genetic gain scalar for grass yield to adjust crude protien content
    /// </summary>
    public double GeneticYieldDilutionScalar { get; set; }
}
/// <summary>
/// Housing inputs class
/// </summary>
public class HousingInputs
{
    /// <summary>
    /// Sector enumerator
    /// </summary>
    public Sector Sector { get; set; }
    /// <summary>
    /// Animal type as integer, specific to sector
    /// </summary>
    public int AnimalType { get; set; }
    /// <summary>
    /// Manure hosduing system
    /// </summary>
    public ManureHousingSystem ManureHousingSystem { get; set; }
    /// <summary>
    /// Organic matter type
    /// </summary>
    public OrganicMatterType OrganicMatterType { get; set; }
    /// <summary>
    /// Total nitrogen entering stage - can be null if urine and dung provided (but only for cattle at housing and yards)
    /// </summary>
    public double TotalNitrogen { get; set; }
    /// <summary>
    /// Total ammoniacal nitrogen entering stage - can be null if urine and dung provided (but only for cattle at housing and yards)
    /// </summary>
    public double TotalAmmoniacalNitrogen { get; set; }
    /// <summary>
    /// Total volatile solids entering stage
    /// </summary>
    public double VolatileSolids { get; set; }
    /// <summary>
    /// Bedding Nitrogen entering stage
    /// </summary>
    public double? BeddingNitrogen { get; set; }
    /// <summary>
    /// The list of mitigation method IDs
    /// </summary>
    public int[]? MitigationMethods { get; set; }
}
/// <summary>
/// Housing inputs class for sheep
/// </summary>
public class SheepHousingInputs
{
    /// <summary>
    /// Sector enumerator
    /// </summary>
    public Sector Sector { get; set; }
    /// <summary>
    /// Animal type as integer, specific to sector
    /// </summary>
    public int AnimalType { get; set; }
    /// <summary>
    /// Sheep Energy Balance Object
    /// </summary>
    public required SheepEnergyBalance SheepEnergyBalance { get; set; }
    /// <summary>
    /// The list of mitigation method IDs
    /// </summary>
    public int[]? MitigationMethods { get; set; }
}
/// <summary>
/// Yard inputs
/// </summary>
public class YardInputs
{
    /// <summary>
    /// Sector enumerator
    /// </summary>
    public Sector Sector { get; set; }
    /// <summary>
    /// Animal type as integer, specific to sector
    /// </summary>
    public int AnimalType { get; set; }
    /// <summary>
    /// Total nitrogen entering stage - can be null if urine and dung provided (but only for cattle at housing and yards)
    /// </summary>
    public double TotalNitrogen { get; set; }
    /// <summary>
    /// Total ammoniacal nitrogen entering stage - can be null if urine and dung provided (but only for cattle at housing and yards)
    /// </summary>
    public double TotalAmmoniacalNitrogen { get; set; }
    /// <summary>
    /// Total volatile solids entering stage
    /// </summary>
    public double VolatileSolids { get; set; }
    ///// <summary>
    ///// Urine deposited at housing or on yards (for cattle only)
    ///// </summary>
    //public double? UrineNitrogen { get; set; }
    ///// <summary>
    ///// dung deposited at housing or on yards (for cattle only)
    ///// </summary>
    //public double? DungNitrogen { get; set; }
    /// <summary>
    /// Boolean indicating if collecting yard (true) or feeding yard (false)
    /// </summary>
    public bool CollectingYard { get; set; }
    /// <summary>
    /// The list of mitigation method IDs
    /// </summary>
    public int[]? MitigationMethods { get; set; }
}
/// <summary>
/// Storage inputs
/// </summary>
public class StorageInputs
{
    /// <summary>
    /// Sector enumerator
    /// </summary>
    public Sector Sector { get; set; }
    /// <summary>
    /// Animal type as integer, specific to sector
    /// </summary>
    public int AnimalType { get; set; }
    /// <summary>
    /// Organic Matter Type enumerator
    /// </summary>
    public OrganicMatterType OrganicMatterType { get; set; }
    /// <summary>
    /// Manure Storage System enumerator
    /// </summary>
    public ManureStorageSystem ManureStorageSystem { get; set; }
    /// <summary>
    /// Total nitrogen entering stage - can be null if urine and dung provided (but only for cattle at housing and yards)
    /// </summary>
    public double TotalNitrogen { get; set; }
    /// <summary>
    /// Total ammoniacal nitrogen entering stage - can be null if urine and dung provided (but only for cattle at housing and yards)
    /// </summary>
    public double TotalAmmoniacalNitrogen { get; set; }
    /// <summary>
    /// Total volatile solids entering stage
    /// </summary>
    public double VolatileSolids { get; set; }
    /// <summary>
    /// The list of mitigation method IDs
    /// </summary>
    public int[]? MitigationMethods { get; set; }
}
public class SheepStorageInputs
{
    /// <summary>
    /// Animal type as integer, specific to sector
    /// </summary>
    public int AnimalType { get; set; }
    /// <summary>
    /// Total nitrogen entering stage - can be null if urine and dung provided (but only for cattle at housing and yards)
    /// </summary>
    public double TotalNitrogen { get; set; }
    /// <summary>
    /// Total ammoniacal nitrogen entering stage - can be null if urine and dung provided (but only for cattle at housing and yards)
    /// </summary>
    public double TotalAmmoniacalNitrogen { get; set; }
    /// <summary>
    /// Sheep energy balance object
    /// </summary>
    public required SheepEnergyBalance SheepEnergyBalance { get; set; }
    /// <summary>
    /// The list of mitigation method IDs
    /// </summary>
    public int[]? MitigationMethods { get; set; }
}
/// <summary>
/// Cattle manure mass and volume inputs
/// </summary>
public class ManureMassVolumeCattleInputs
{
    /// <summary>
    /// Sector enumerator
    /// </summary>
    public Sector Sector { get; set; }
    /// <summary>
    /// Animal type ennumerator as integer
    /// </summary>
    public int AnimalType { get; set; }
    /// <summary>
    /// Organic matter type enumerator
    /// </summary>
    public OrganicMatterType OrganicMatterType { get; set; }
    /// <summary>
    /// Time cattle spend at grazing, yards and housing as percentages
    /// </summary>
    public PercentTimeInLocation TimeInLocation { get; set; }
    /// <summary>
    /// Dry matter intake for cattle
    /// </summary>
    public double CattleDryMatterIntake { get; set; }
    /// <summary>
    /// Dry matter digestibility for cattle
    /// </summary>
    public double CattleDryMatterDigestibility { get; set; }
    public double CattleDryMatterContent { get; set; }
    /// <summary>
    /// Boolean indicating if manure is stored via anaerobic digestion
    /// </summary>
    public bool AnaerobicDigestionAtStorage { get; set; }
}
/// <summary>
/// Class storing the time at grazing, on yards and in housing
/// </summary>
public class PercentTimeInLocation
{
    /// <summary>
    /// Percentage of time at graxing
    /// </summary>
    public double TimeGrazing { get; set; }
    /// <summary>
    /// Percentgate of time on Yards
    /// </summary>
    public double TimeYards { get; set; }
    /// <summary>
    /// Percentage of time in housing
    /// </summary>
    public double TimeHousing { get; set; }
    /// <summary>
    /// function to return sum of percentages
    /// </summary>
    /// <returns>total percentage as double</returns>
    public double Sum()
    {
        return TimeGrazing + TimeYards + TimeHousing;
    }
    public void Redistribute()
    {
        double oursum = Sum();
        double multiplier = 100.0 / oursum;
        TimeGrazing *= multiplier;
        TimeYards *= multiplier;
        TimeHousing *= multiplier;
    }
}
/// <summary>
/// Manure mass and volume inputs for poultry
/// </summary>
public class ManureMassVolumeInputs
{
    /// <summary>
    /// Sector enumerator
    /// </summary>
    public Sector Sector { get; set; }
    /// <summary>
    /// Animal type ennumerator as integer
    /// </summary>
    public int AnimalType { get; set; }
    /// <summary>
    /// Organic matter type enumerator
    /// </summary>
    public OrganicMatterType OrganicMatterType { get; set; }
    /// <summary>
    /// Percentage of excreta that is deposited outdoors
    /// </summary>
    public double? ExcretaOutdoorPercentage { get; set; }
}
public class ManureMassVolumeSheepInputs
{
    /// <summary>
    /// Animal type ennumerator as integer
    /// </summary>
    public int AnimalType { get; set; }
    /// <summary>
    /// Sheep energy balance object
    /// </summary>
    public required SheepEnergyBalance SheepEnergyBalance { get; set; }
}
