using System.Reflection;
using FrameAgricultureApi.Enumerators;
using FrameAgricultureApi.Generic;
using FrameAgricultureApi.Libraries.CoverCrops;

namespace FrameAgricultureApi.Libraries.LookUps;

public class CropLookup
{
    /// <summary>
    /// Private instance
    /// </summary>
    private static CropLookup? _instance;

    /// <summary>
    /// Public Instance
    /// </summary>
    public static CropLookup Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = new CropLookup();
            }
            return _instance;
        }
    }

    //Define variables
    private Dictionary<int, double> uncertaintyN2ONFertiliserModelCoefficient = new Dictionary<int, double>();
    private Dictionary<int, double> uncertaintyNH3NFertiliserModelCoefficient = new Dictionary<int, double>();
    private double ratioNON = double.NaN;
    private double ratioN2N = double.NaN;
    private double fracLeach = double.NaN;
    private double iPCCEF1 = double.NaN;
    private double iPCCEF4 = double.NaN;
    private double iPCCEF5 = double.NaN;
    private Dictionary<int, double> dryMatterContent = new Dictionary<int, double>();
    private Dictionary<int, double> residuesRemaining = new Dictionary<int, double>();
    private Dictionary<int, double> aboveGroundResidueN = new Dictionary<int, double>();
    private Dictionary<int, double> iPCCSlope = new Dictionary<int, double>();
    private Dictionary<int, double> iPCCIntercept = new Dictionary<int, double>();
    private Dictionary<int, double> ratioAbovetoBelowGround = new Dictionary<int, double>();
    private Dictionary<int, double> belowGroundResidueN = new Dictionary<int, double>();
    private Dictionary<int, double> yieldN = new Dictionary<int, double>();
    private Dictionary<int, double> contributionAmmonia = new Dictionary<int, double>();
    private Dictionary<int, double> harvestIndex = new Dictionary<int, double>();
    private double additionalNCoverCrop = double.NaN;
    private double coverCropFracLeachReduction = double.NaN;
    private Dictionary<int, double> efNMVOC = new Dictionary<int, double>();
    private Dictionary<int, double> efPM2_5Cultivation = new Dictionary<int, double>();
    private Dictionary<int, double> efPM2_5Harvesting = new Dictionary<int, double>();
    private Dictionary<int, double> efPM2_5Cleaning = new Dictionary<int, double>();
    private Dictionary<int, double> efPM2_5Drying = new Dictionary<int, double>();
    private Dictionary<int, double> efPM10Cultivation = new Dictionary<int, double>();
    private Dictionary<int, double> efPM10Harvesting = new Dictionary<int, double>();
    private Dictionary<int, double> efPM10Cleaning = new Dictionary<int, double>();
    private Dictionary<int, double> efPM10Drying = new Dictionary<int, double>();
    private Dictionary<int, double> burnCombustionEfficiency = new Dictionary<int, double>();
    private Dictionary<int, double> burnNOxN = new Dictionary<int, double>();
    private Dictionary<int, double> burnNMVOC = new Dictionary<int, double>();
    private Dictionary<int, double> burnSO2 = new Dictionary<int, double>();
    private Dictionary<int, double> burnCO = new Dictionary<int, double>();
    private Dictionary<int, double> burnTSP = new Dictionary<int, double>();
    private Dictionary<int, double> burnPM10 = new Dictionary<int, double>();
    private Dictionary<int, double> burnPM2_5 = new Dictionary<int, double>();
    private Dictionary<int, double> burnCH4 = new Dictionary<int, double>();
    private Dictionary<int, double> burnN2ON = new Dictionary<int, double>();
    Dictionary<int, double> burnNH3N = new Dictionary<int, double>();

    private CropLookup()
    {
        //Instantiate Dictionaries

        //clear dictionaries
        uncertaintyN2ONFertiliserModelCoefficient.Clear();
        uncertaintyNH3NFertiliserModelCoefficient.Clear();

        //Read Fertiliser data
        MemoryStream memoryStream = HelperFunctions.GetLUT(Assembly.GetExecutingAssembly(), "FLEA_Fertiliser_RatioEFs_LUT.dat");
        try
        {
            using (BinaryReader binaryReader = new BinaryReader(memoryStream))
            {
                //Read in the 6 fertiliser type uncertainties for N2ON
                uncertaintyN2ONFertiliserModelCoefficient.Add(binaryReader.ReadInt32(), binaryReader.ReadDouble());
                uncertaintyN2ONFertiliserModelCoefficient.Add(binaryReader.ReadInt32(), binaryReader.ReadDouble());
                uncertaintyN2ONFertiliserModelCoefficient.Add(binaryReader.ReadInt32(), binaryReader.ReadDouble());
                uncertaintyN2ONFertiliserModelCoefficient.Add(binaryReader.ReadInt32(), binaryReader.ReadDouble());
                uncertaintyN2ONFertiliserModelCoefficient.Add(binaryReader.ReadInt32(), binaryReader.ReadDouble());
                uncertaintyN2ONFertiliserModelCoefficient.Add(binaryReader.ReadInt32(), binaryReader.ReadDouble());

                //Read in the 6 fertiliser type uncertainties for NH3N
                uncertaintyNH3NFertiliserModelCoefficient.Add(binaryReader.ReadInt32(), binaryReader.ReadDouble());
                uncertaintyNH3NFertiliserModelCoefficient.Add(binaryReader.ReadInt32(), binaryReader.ReadDouble());
                uncertaintyNH3NFertiliserModelCoefficient.Add(binaryReader.ReadInt32(), binaryReader.ReadDouble());
                uncertaintyNH3NFertiliserModelCoefficient.Add(binaryReader.ReadInt32(), binaryReader.ReadDouble());
                uncertaintyNH3NFertiliserModelCoefficient.Add(binaryReader.ReadInt32(), binaryReader.ReadDouble());
                uncertaintyNH3NFertiliserModelCoefficient.Add(binaryReader.ReadInt32(), binaryReader.ReadDouble());

                ratioNON = binaryReader.ReadDouble();
                ratioN2N = binaryReader.ReadDouble();
                fracLeach = binaryReader.ReadDouble();
                iPCCEF1 = binaryReader.ReadDouble();
                iPCCEF4 = binaryReader.ReadDouble();
                iPCCEF5 = binaryReader.ReadDouble();
            }
            memoryStream.Close();
        }
        catch (Exception ex)
        {
            throw new Exception("Error reading fertiliser lookup table: " + ex.Message);
        }
        finally
        {
            memoryStream.Close();
        }

        //Read Residue Data
        dryMatterContent.Clear();
        residuesRemaining.Clear();
        aboveGroundResidueN.Clear();
        iPCCSlope.Clear();
        iPCCIntercept.Clear();
        ratioAbovetoBelowGround.Clear();
        belowGroundResidueN.Clear();
        yieldN.Clear();
        contributionAmmonia.Clear();
        memoryStream.Close();
        memoryStream.Dispose();
        memoryStream = new MemoryStream();
        memoryStream = HelperFunctions.GetLUT(Assembly.GetExecutingAssembly(), "FLEA_Crop_Residue_LUT.dat");
        try
        {
            using (BinaryReader binaryReader = new BinaryReader(memoryStream))
            {
                while (binaryReader.BaseStream.Position < binaryReader.BaseStream.Length)
                {
                    int index = binaryReader.ReadInt32();
                    dryMatterContent.Add(index, binaryReader.ReadDouble());
                    residuesRemaining.Add(index, binaryReader.ReadDouble());
                    aboveGroundResidueN.Add(index, binaryReader.ReadDouble());
                    iPCCSlope.Add(index, binaryReader.ReadDouble());
                    iPCCIntercept.Add(index, binaryReader.ReadDouble());
                    ratioAbovetoBelowGround.Add(index, binaryReader.ReadDouble());
                    belowGroundResidueN.Add(index, binaryReader.ReadDouble());
                    harvestIndex.Add(index, binaryReader.ReadDouble());
                    yieldN.Add(index, binaryReader.ReadDouble());
                    contributionAmmonia.Add(index, binaryReader.ReadDouble());
                }
            }
            memoryStream.Close();
        }
        catch (Exception ex)
        {
            throw new Exception("Error reading crop residue lookup: " + ex.Message);
        }
        finally
        {
            memoryStream.Close();
        }

        //Read Cover Crop data
        memoryStream.Close();
        memoryStream.Dispose();
        memoryStream = new MemoryStream();
        memoryStream = HelperFunctions.GetLUT(Assembly.GetExecutingAssembly(), "FLEA_CoverCrop_LUT.dat");
        try
        {
            using (BinaryReader binaryReader = new BinaryReader(memoryStream))
            {
                while (binaryReader.BaseStream.Position < binaryReader.BaseStream.Length)
                {

                    //Cover Crops
                    additionalNCoverCrop = binaryReader.ReadDouble();
                    coverCropFracLeachReduction = binaryReader.ReadDouble();
                }
            }

            memoryStream.Close();
        }
        catch (Exception ex)
        {
            throw new Exception("Error reading Cover crop lookup: " + ex.Message);
        }
        finally
        {
            memoryStream.Close();
        }

        //Read Crop NonGHG data
        efNMVOC.Clear();
        efPM10Cleaning.Clear();
        efPM10Cultivation.Clear();
        efPM10Drying.Clear();
        efPM10Harvesting.Clear();
        efPM2_5Cleaning.Clear();
        efPM2_5Cultivation.Clear();
        efPM2_5Drying.Clear();
        efPM2_5Harvesting.Clear();
        memoryStream.Close();
        memoryStream.Dispose();
        memoryStream = new MemoryStream();
        memoryStream = HelperFunctions.GetLUT(Assembly.GetExecutingAssembly(), "FLEA_Crop_NonGHG_LUT.dat");
        try
        {
            using (BinaryReader binaryReader = new BinaryReader(memoryStream))
            {
                while (binaryReader.BaseStream.Position < binaryReader.BaseStream.Length)
                {
                    int index = binaryReader.ReadInt32();
                    efNMVOC.Add(index, binaryReader.ReadDouble());
                    efPM2_5Cultivation.Add(index, binaryReader.ReadDouble());
                    efPM2_5Harvesting.Add(index, binaryReader.ReadDouble());
                    efPM2_5Cleaning.Add(index, binaryReader.ReadDouble());
                    efPM2_5Drying.Add(index, binaryReader.ReadDouble());
                    efPM10Cultivation.Add(index, binaryReader.ReadDouble());
                    efPM10Harvesting.Add(index, binaryReader.ReadDouble());
                    efPM10Cleaning.Add(index, binaryReader.ReadDouble());
                    efPM10Drying.Add(index, binaryReader.ReadDouble());
                }
            }
            memoryStream.Close();
        }
        catch (Exception ex)
        {
            throw new Exception("Error reading nonGHG lookup: " + ex.Message);
        }
        finally
        {
            memoryStream.Close();
        }

        //Read Crop Burning data
        burnCombustionEfficiency.Clear();
        burnNOxN.Clear();
        burnNMVOC.Clear();
        burnSO2.Clear();
        burnCO.Clear();
        burnTSP.Clear();
        burnPM10.Clear();
        burnPM2_5.Clear();
        burnCH4.Clear();
        burnN2ON.Clear();
        burnNH3N.Clear();
        memoryStream.Close();
        memoryStream.Dispose();
        memoryStream = new MemoryStream();
        memoryStream = HelperFunctions.GetLUT(Assembly.GetExecutingAssembly(), "FLEA_CropBurning_LUT.dat");
        try
        {
            using (BinaryReader binaryReader = new BinaryReader(memoryStream))
            {
                while (binaryReader.BaseStream.Position < binaryReader.BaseStream.Length)
                {

                    int index = binaryReader.ReadInt32();

                    burnCombustionEfficiency.Add(index, binaryReader.ReadDouble());
                    burnNOxN.Add(index, binaryReader.ReadDouble());
                    burnNMVOC.Add(index, binaryReader.ReadDouble());
                    burnSO2.Add(index, binaryReader.ReadDouble());
                    burnCO.Add(index, binaryReader.ReadDouble());
                    burnTSP.Add(index, binaryReader.ReadDouble());
                    burnPM10.Add(index, binaryReader.ReadDouble());
                    burnPM2_5.Add(index, binaryReader.ReadDouble());
                    burnCH4.Add(index, binaryReader.ReadDouble());
                    burnN2ON.Add(index, binaryReader.ReadDouble());
                    burnNH3N.Add(index, binaryReader.ReadDouble());

                }
            }
            memoryStream.Close();
        }
        catch (Exception ex)
        {
            throw new Exception("Error reading crop burning lookup: " + ex.Message);
        }
        finally { memoryStream.Close(); }
    }

    //Retrieve Fertiliser data
    /// <summary>
    /// Returns uncertainty value for N2ON fertiliser calculation based on fertiliser type
    /// </summary>
    /// <param name="fertiliserType">the fertiliser type enumerator</param>
    /// <returns>uncertainty as a double</returns>
    public static double Uncertainty_N2ON_FertiliserModelCoefficient(FertiliserType fertiliserType)
    {
        return Instance.uncertaintyN2ONFertiliserModelCoefficient[(int)fertiliserType];
    }
    /// <summary>
    /// Returns uncertainty value for NH3N fertiliser calculation based on fertiliser type
    /// </summary>
    /// <param name="fertiliserType">the fertiliser type enumerator</param>
    /// <returns>uncertainty as a double</returns>
    public static double Uncertainty_AmmoniaFertiliserModelCoefficient(FertiliserType fertiliserType)
    {
        return Instance.uncertaintyNH3NFertiliserModelCoefficient[(int)fertiliserType];
    }

    //Retrieve Generic data
    /// <summary>
    /// Returns ratio of NON to N2ON
    /// </summary>
    /// <returns>ratio as double</returns>
    public static double RetrieveRatioNON() { return Instance.ratioNON; }
    /// <summary>
    /// returns ratio of N2N to N2ON
    /// </summary>
    /// <returns>rario as double</returns>
    public static double RetrieveRatioN2N() { return Instance.ratioN2N; }
    /// <summary>
    /// Returns fraction of N leached as NO3N
    /// </summary>
    /// <returns>Fracleach value as double</returns>
    public static double RetrieveFracLeach() { return Instance.fracLeach; }
    /// <summary>
    /// Returns IPCC emision Factor 1
    /// </summary>
    /// <returns>EF as double</returns>
    public static double RetrieveIPCCEmissionFactor1() { return Instance.iPCCEF1; }
    /// <summary>
    /// Returns IPCC Emission Factor 4
    /// </summary>
    /// <returns>EF as double</returns>
    public static double RetrieveIPCCEmissionFactor4() { return Instance.iPCCEF4; }
    /// <summary>
    /// Returns IPCC Emission Factor 5
    /// </summary>
    /// <returns>EF as double</returns>
    public static double RetrieveIPCCEmissionFactor5() { return Instance.iPCCEF5; }

    //Retrieve Residue data
    /// <summary>
    /// Retrieces dry matter content for crop type as percentage of fresh weight yield
    /// </summary>
    /// <param name="cropType">Crop Type as an enumerator</param>
    /// <returns>Percentage dry matter content as double</returns>
    public static double RetrieveDryMatterContent(CropType cropType)
    {
        return Instance.dryMatterContent[(int)cropType];
    }
    /// <summary>
    /// Returns the residues remaining in the field if resiudes are baled and removed as a percentage
    /// </summary>
    /// <param name="cropType">Crop type as enumerator</param>
    /// <returns>Percentage of residue remainin in field as double</returns>
    public static double RetrieveResiduesRemaining(CropType cropType)
    {
        return Instance.residuesRemaining[(int)cropType];
    }
    /// <summary>
    /// Returns the N content of the crop residues, either above or below ground
    /// </summary>
    /// <param name="cropType">Crop type as ennumerator</param>
    /// <param name="aboveGround">boolean to indicate if above ground or below groudn residue N content required (set to true for above ground, false for below ground)</param>
    /// <returns>N content of residues (kg/kg) as double</returns>
    public static double RetrieveResidueNContent(CropType cropType, bool aboveGround)
    {
        if (aboveGround)
        { return Instance.aboveGroundResidueN[(int)cropType]; }
        else
        {
            return Instance.belowGroundResidueN[(int)cropType];
        }
    }
    /// <summary>
    /// Returns the N content of yield in kg per tonne
    /// </summary>
    /// <param name="cropType"></param>
    /// <returns>N content of yield (kg/tonne) as double</returns>
    public static double RetrieveYieldNContent(CropType cropType)
    {
        return Instance.yieldN[(int)cropType];
    }
    /// <summary>
    /// Returns the contributing fraction of residues to ammonia emission
    /// </summary>
    /// <param name="cropType"></param>
    /// <returns>Contributing proportion of residues to ammonia emission</returns>
    public static double RetrieveAmmoniaContribution(CropType cropType)
    {
        return Instance.contributionAmmonia[(int)cropType];
    }
    /// <summary>
    /// Returns slope of IPCC regression of above to below ground residues
    /// </summary>
    /// <param name="cropType">Crop type as enumerator</param>
    /// <returns>Slope of regression line as double</returns>
    public static double RetrieveIPCCSlope(CropType cropType)
    {
        return Instance.iPCCSlope[(int)cropType];
    }

    /// <summary>
    /// Returns intercept of IPCC regression of above to below ground residues
    /// </summary>
    /// <param name="cropType">crop type as enumerator</param>
    /// <returns></returns>
    public static double RetrieveIPCCIntercept(CropType cropType)
    {
        return Instance.iPCCIntercept[(int)cropType];
    }
    /// <summary>
    /// Retrieve ratio of above to belwo groudn residues
    /// </summary>
    /// <param name="cropType">Crop type as enumerator</param>
    /// <returns>Ratio as double</returns>
    public static double RetrieveAbovetoBelowGroundRatio(CropType cropType)
    {
        return Instance.ratioAbovetoBelowGround[(int)cropType];
    }
    /// <summary>
    /// Retrieve the IPCC Emission Factor 1 based on crop type
    /// </summary>
    /// <param name="cropType">Integer enumerator indicating the crop type</param>
    /// <returns>IPCC Emission Factor 1 as double</returns>
    public static double RetrieveIPCCEmissionFactor1(CropType cropType)
    {
        return RetrieveIPCCEmissionFactor1();
    }
    /// <summary>
    /// Retrieve the harvest index
    /// </summary>
    /// <param name="cropType"></param>
    /// <returns>harvest index if exists, double.NaN if not</returns>
    public static double RetrieveHarvestIndex(CropType cropType)
    {
        return Instance.harvestIndex[(int)cropType];
    }
    /// <summary>
    /// MRetrieve FracLeach based on crop type
    /// </summary>
    /// <param name="cropType">Integer enumerator indicating the crop type</param>
    /// <returns>Percentage of N Leached as double</returns>
    public static double RetrieveFracLeach(CropType cropType)
    {
        return Instance.fracLeach;
    }
    /// <summary>
    /// Retrieve IPCC Emission Factor 5 based on crop type
    /// </summary>
    /// <param name="cropType">Integer enumerator indicating the crop type</param>
    /// <returns>IPCC Emission Factor 5 as double</returns>
    public static double RetrieveIPCCEmissionFactor5(CropType cropType)
    {
        return Instance.iPCCEF5;
    }

    //Retrieve Cover Crop data
    /// <summary>
    /// Retrive the additional N added to the soil as a result of using cover crops
    /// </summary>
    /// <param name="coverCropType">Integer enumerator indicating the cover crop type</param>
    /// <returns>kg N per hectare returned to soil as double</returns>
    public static double RetrieveAdditionalNReturn_CoverCrop(CoverCropType coverCropType)
    {
        //no lookup at present so single value define in Instance
        return Instance.additionalNCoverCrop;
    }
    /// <summary>
    /// Retrieve Frac Leach resulting from use of cover crops
    /// </summary>
    /// <returns>Percentage of N leached as double</returns>
    public static double RetrievedCoverCropFracLeachReduction() { return Instance.coverCropFracLeachReduction; }

    //Retrieve NonGHG Emission Factor data
    /// <summary>
    /// Retrieves the Non-methance Volatile Orgainc COmpounds emitted due to crop production
    /// </summary>
    /// <param name="cropType">Integer enumerator indicating the crop type</param>
    /// <returns>kg NMVOC emitted per hectare as double</returns>
    public static double RetrieveNMVOC(CropType cropType)
    {
        return Instance.efNMVOC[(int)cropType];
    }
    /// <summary>
    /// Retrieves the 2.5 nanonmeter particulate matter emitted due to crop cultivation
    /// </summary>
    /// <param name="cropType">Integer enumerator indicating the crop type</param>
    /// <returns>kg PM2.5 emitted per hecattre as double</returns>
    public static double RetrieveCultivationPM2_5(CropType cropType)
    { return Instance.efPM2_5Cultivation[(int)cropType]; }
    /// <summary>
    /// Retrieves the 2.5 nanometer particulate matter emitted due to crop harvesting
    /// </summary>
    /// <param name="cropType">Integer enumerator indicating the crop type</param>
    /// <returns>kg PM2.5 emitted per hectare as double</returns>
    public static double RetrieveHarvestingPM2_5(CropType cropType)
    { return Instance.efPM2_5Harvesting[(int)cropType]; }
    /// <summary>
    /// Retrieves the 2.5 nanometer particulate matter emitted due to crop cleaning
    /// </summary>
    /// <param name="cropType">Integer enumerator indicating the crop type</param>
    /// <returns>kg PM2.5 emitted per hectare</returns>
    public static double RetrieveCleaningPM2_5(CropType cropType)
    { return Instance.efPM2_5Cleaning[(int)cropType]; }
    /// <summary>
    /// Retrieves the 2.5 nanometer particulate matter emitted due to crop drying
    /// </summary>
    /// <param name="cropType">Integer enumerator indicating the crop type</param>
    /// <returns>kg PM2.5 emitted per hectare</returns>
    public static double RetrieveDryingPM2_5(CropType cropType)
    { return Instance.efPM2_5Drying[(int)cropType]; }
    /// <summary>
    /// Retrieves the 10 nanonmeter particulate matter emitted due to crop cultivation
    /// </summary>
    /// <param name="cropType">Integer enumerator indicating the crop type</param>
    /// <returns>kg PM10 emitted per hecattre as double</returns>
    public static double RetrieveCultivationPM10(CropType cropType)
    { return Instance.efPM10Cultivation[(int)cropType]; }
    /// <summary>
    /// Retrieves the 10 nanonmeter particulate matter emitted due to crop harvesting
    /// </summary>
    /// <param name="cropType">Integer enumerator indicating the crop type</param>
    /// <returns>kg PM10 emitted per hecattre as double</returns>
    public static double RetrieveHarvestingPM10(CropType cropType)
    { return Instance.efPM10Harvesting[(int)cropType]; }
    /// <summary>
    /// Retrieves the 10 nanonmeter particulate matter emitted due to crop cleaning
    /// </summary>
    /// <param name="cropType">Integer enumerator indicating the crop type</param>
    /// <returns>kg PM10 emitted per hecattre as double</returns>
    public static double RetrieveCleaningPM10(CropType cropType)
    { return Instance.efPM10Cleaning[(int)cropType]; }
    /// <summary>
    /// Retrieves the 10 nanonmeter particulate matter emitted due to crop drying
    /// </summary>
    /// <param name="cropType">Integer enumerator indicating the crop type</param>
    /// <returns>kg PM10 emitted per hecattre as double</returns>
    public static double RetrieveDryingPM10(CropType cropType)
    { return Instance.efPM10Drying[(int)cropType]; }

    //Retrieve Crop Burning data
    /// <summary>
    /// Retrieve methane emissions from crop burning
    /// </summary>
    /// <param name="cropType">Integer enumerator indicating the crop type</param>
    /// <returns>CH4 emission (kg) per kg DM burned</returns>
    public static double RetrieveCombustionCH4(CropType cropType) { return Instance.burnCH4[(int)cropType]; }
    /// <summary>
    /// Retrieve carbon monoxide emission from burning
    /// </summary>
    /// <param name="cropType">Integer enumerator indicating the crop typ</param>
    /// <returns>CO emission (kg per kg DM burned)</returns>
    public static double RetrieveCombustionCO(CropType cropType) { return Instance.burnCO[(int)cropType]; }
    /// <summary>
    /// Retrieve nitrous oxide emission from crop burning
    /// </summary>
    /// <param name="cropType">Integer enumerator indicating the crop typ</param>
    /// <returns>N2O-N emissino (kg per kg DM burned)</returns>
    public static double RetrieveCombustionN2ON(CropType cropType) { return Instance.burnN2ON[(int)cropType]; }
    /// <summary>
    /// Retrieve ammonia emission from crop burning
    /// </summary>
    /// <param name="cropType">Integer enumerator indicating the crop typ</param>
    /// <returns>NH3-N emission (kg per kg DM burned)</returns>
    public static double RetrieveCombustionNH3N(CropType cropType) { return Instance.burnNH3N[(int)cropType]; }
    /// <summary>
    /// Retrieve NMVOC emission from crop burning
    /// </summary>
    /// <param name="cropType">Integer enumerator indicating the crop typ</param>
    /// <returns>NMVOC emission (kg per kg DM burned)</returns>
    public static double RetrieveCombustionNMVOC(CropType cropType) { return Instance.burnNMVOC[(int)cropType]; }
    /// <summary>
    /// Retrieve NOxN emission from crop burning
    /// </summary>
    /// <param name="cropType">Integer enumerator indicating the crop typ</param>
    /// <returns>NOx-N emission (kg per kg DM burned)</returns>
    public static double RetrieveCombustionNOxN(CropType cropType) { return Instance.burnNOxN[(int)cropType]; }
    /// <summary>
    /// Retrieve PM2.5 emission from crop burning
    /// </summary>
    /// <param name="cropType">Integer enumerator indicating the crop typ</param>
    /// <returns>".5 nanometer particulate matter emission (kg per kg DM burned)</returns>
    public static double RetrieveCombustionPM2_5(CropType cropType) { return Instance.burnPM2_5[(int)cropType]; }
    /// <summary>
    /// Retrieve PM10 emission from crop burning
    /// </summary>
    /// <param name="cropType">Integer enumerator indicating the crop typ</param>
    /// <returns>10 nanometer particulate matter emission (kg per kg DM burned)</returns>
    public static double RetrieveCombustionPM10(CropType cropType) { return Instance.burnPM10[(int)cropType]; }
    /// <summary>
    /// Retrieve Sulpher dioxide emission from crop burning
    /// </summary>
    /// <param name="cropType">Integer enumerator indicating the crop typ</param>
    /// <returns>SO2 emission (kg per kg DM burned)</returns>
    public static double RetrieveCombustionSO2(CropType cropType) { return Instance.burnSO2[(int)cropType]; }
    /// <summary>
    /// Retrieve total suspended particulates emitted from crop burning
    /// </summary>
    /// <param name="cropType">Integer enumerator indicating the crop typ</param>
    /// <returns>TSP emission (kg per kg DM burned)</returns>
    public static double RetrieveCombustionTSP(CropType cropType) { return Instance.burnTSP[(int)cropType]; }
    /// <summary>
    /// Retireve combustion efficieny for corp burning
    /// </summary>
    /// <param name="cropType">Integer enumerator indicating the crop typ</param>
    /// <returns>Burning efficiency (unitless)</returns>
    public static double RetrieveCombustionEfficiency(CropType cropType) { return Instance.burnCombustionEfficiency[(int)cropType]; ; }
}