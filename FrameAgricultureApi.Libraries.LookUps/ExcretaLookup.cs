using FrameAgricultureApi.CustomErrorHandling;
using FrameAgricultureApi.Generic;
using System.Reflection;
using static FrameAgricultureApi.Enumerators.Enumerators;

namespace FrameAgricultureApi.Libraries.LookUps;

/// <summary>
/// Excreta N and TAN lookup class
/// </summary>
public class ExcretaLookup
{
    private static ExcretaLookup? _instance = null;

    /// <summary>
    /// Singleton Instance method
    /// </summary>
    /// <returns></returns>
    public static ExcretaLookup Instance
    {
        get
        {
            if(_instance == null)
            {
                _instance = new ExcretaLookup();
            }
            return _instance;
        }
    }
    /// <summary>
    /// Constructor
    /// </summary>
    public ExcretaLookup()
    {
        //Add code to populate data here

        //clear dictionaries
        PigNitrogenExcretion.Clear();
        PoultryNitrogenExcretion.Clear();
        MinorLivestockNitrogenExcretion.Clear();

        PigVolatileSolidsExcretion.Clear();
        PoultryVolatileSolidsExcretion.Clear();
        MinorLivestockVolatileSolidsExcretion.Clear();

        PigTanPercent.Clear();
        PoultryTanPercent.Clear();
        MinorLiveStockTanPercent.Clear();

        MemoryStream memoryStream = HelperFunctions.GetLUT(Assembly.GetExecutingAssembly(), "FLEA_Excreta_LUT.dat");
        try
        {
            using BinaryReader binaryReader = new BinaryReader(memoryStream);
            while(binaryReader.BaseStream.Position < binaryReader.BaseStream.Length)
            {
                int sector = binaryReader.ReadInt32();
                int animalType = binaryReader.ReadInt32();
                double nExcretion = binaryReader.ReadDouble();
                double tanPercent = binaryReader.ReadDouble();
                double volatileSolids = binaryReader.ReadDouble();

                switch((Sector)sector)
                {
                    case Sector.Pigs:
                        PigNitrogenExcretion.Add((PigType)animalType, nExcretion);
                        PigVolatileSolidsExcretion.Add((PigType)animalType, volatileSolids);
                        PigTanPercent.Add((PigType)animalType, tanPercent);
                        break;
                    case Sector.Poultry:
                        PoultryNitrogenExcretion.Add((PoultryType)animalType, nExcretion);
                        PoultryVolatileSolidsExcretion.Add((PoultryType)animalType, volatileSolids);
                        PoultryTanPercent.Add((PoultryType)animalType, tanPercent);
                        break;
                    case Sector.MinorLivestock:
                        MinorLivestockNitrogenExcretion.Add((MinorLivestockType)animalType, nExcretion);
                        MinorLivestockVolatileSolidsExcretion.Add((MinorLivestockType)animalType, volatileSolids);
                        MinorLiveStockTanPercent.Add((MinorLivestockType)animalType, tanPercent);
                        break;
                    default:
                        break;
                }
            }
        }
        catch(Exception ex)
        {
            throw new CustomAppException("Error reading excreta lookup table: " + ex.Message);
        }
        finally
        {
            memoryStream.Close();
        }
    }

    // Nitrogen and TAN
    private Dictionary<PigType, double> PigNitrogenExcretion = [];
    private Dictionary<PoultryType, double> PoultryNitrogenExcretion = [];
    private Dictionary<MinorLivestockType, double> MinorLivestockNitrogenExcretion = [];

    // TAN Percent
    private Dictionary<PigType, double> PigTanPercent = [];
    private Dictionary<PoultryType, double> PoultryTanPercent = [];
    private Dictionary<MinorLivestockType, double> MinorLiveStockTanPercent = [];

    // Volatile Solids
    private Dictionary<PigType, double> PigVolatileSolidsExcretion = [];
    private Dictionary<PoultryType, double> PoultryVolatileSolidsExcretion = [];
    private Dictionary<MinorLivestockType, double> MinorLivestockVolatileSolidsExcretion = [];

    /// <summary>
    /// Internal method to get pig nitrogen excretio parameters
    /// </summary>
    /// <param name="type">Pig type</param>
    /// <returns>N excretion as double or percent TAN as double</returns>
    /// <exception cref="Exception">incorrect pig type provided exception</exception>
    private static double RetrievePigNitrogenExcretion(PigType type)
    {
        if(Instance.PigNitrogenExcretion.ContainsKey(type))
        {
            return Instance.PigNitrogenExcretion[type];
        }
        else
        {
            throw new CustomAppException("Pig type, " + type.ToString() + " is not a valid pig type.");
        }
    }
    /// <summary>
    /// Internal method to get  poultry nitrogen excretio parameters
    /// </summary>
    /// <param name="type">Poultry type</param>
    /// <returns>N excretion as double or percent TAN as double</returns>
    /// <exception cref="Exception">incorrect poultry type provided exception</exception>
    private static double RetrievePoultryNitrogenExcretion(PoultryType type)
    {
        if(Instance.PoultryNitrogenExcretion.ContainsKey(type))
        {
            return Instance.PoultryNitrogenExcretion[type];
        }
        else
        {
            throw new CustomAppException("Poultry type, " + type.ToString() + " is not a valid poultry type.");
        }
    }
    /// <summary>
    /// Internal method to get  minor livestock nitrogen excretio parameters
    /// </summary>
    /// <param name="type">Pig type</param>
    /// <returns>N excretion as double or percent TAN as double</returns>
    /// <exception cref="Exception">incorrect minor livestock type provided exception</exception>
    private static double RetrieveMinorLivestockNitrogenExcretion(MinorLivestockType type)
    {
        if(Instance.MinorLivestockNitrogenExcretion.ContainsKey(type))
        {
            return Instance.MinorLivestockNitrogenExcretion[type];
        }
        else
        {
            throw new CustomAppException("Minor livestock type, " + type.ToString() + " is not a valid minor livestock type.");
        }
    }

    private static double RetrieveMinorLivestockTanPercent(MinorLivestockType type)
    {
        if(Instance.MinorLiveStockTanPercent.ContainsKey(type))
        {
            return Instance.MinorLiveStockTanPercent[type];
        }
        else
        {
            throw new CustomAppException("Minor livestock type, " + type.ToString() + " is not a valid minor livestock type.");
        }
    }

    private static double RetrievePigTanPercent(PigType type)
    {
        if(Instance.PigTanPercent.ContainsKey(type))
        {
            return Instance.PigTanPercent[type];
        }
        else
        {
            throw new CustomAppException("Pig type, " + type.ToString() + " is not a valid pig type.");
        }
    }

    private static double RetrievePoultryTanPercent(PoultryType type)
    {
        if(Instance.PoultryTanPercent.ContainsKey(type))
        {
            return Instance.PoultryTanPercent[type];
        }
        else
        {
            throw new CustomAppException("Poultry type, " + type.ToString() + " is not a valid poultry type.");
        }
    }
    /// <summary>
    /// Public method to get nitrogen excretion
    /// </summary>
    /// <param name="sector">sector (should only be pig, poultry or minor livestock)</param>
    /// <param name="animaltype">integer value for animal type enumerator</param>
    /// <returns>N excretion (kg/head) as double</returns>
    /// <exception cref="Exception">Incorrect sector or animal type exception</exception>
    public static double GetNitrogenExcretion(Sector sector, int animaltype)
    {
        try
        {
            return sector switch
            {
                Sector.Poultry => RetrievePoultryNitrogenExcretion((PoultryType)animaltype),
                Sector.Pigs => RetrievePigNitrogenExcretion((PigType)animaltype),
                Sector.MinorLivestock => RetrieveMinorLivestockNitrogenExcretion((MinorLivestockType)animaltype),
                _ => throw new CustomAppException("No nitrogen excretion lookup exists for the " + sector.ToString() + " sector."),
            };
        }
        catch(Exception excep) { throw new CustomAppException("Error retrieving nitrogen excretion: " + excep.Message + "."); }
    }
    /// <summary>
    /// Public method to get percent TAN in excreta
    /// </summary>
    /// <param name="sector">sector (should only be pig, poultry or minor livestock)</param>
    /// <param name="animaltype">integer value for animal type enumerator</param>
    /// <returns>Percentage of N in excreta that is TAN </returns>
    /// <exception cref="Exception">Incorrect sector or animal type exception</exception>
    public static double GetTANPercent(Sector sector, int animaltype)
    {
        try
        {
            return sector switch
            {
                Sector.Poultry => RetrievePoultryTanPercent((PoultryType)animaltype),
                Sector.Pigs => RetrievePigTanPercent((PigType)animaltype),
                Sector.MinorLivestock => RetrieveMinorLivestockTanPercent((MinorLivestockType)animaltype),
                _ => throw new CustomAppException("No nitrogen excretion lookup exists for the " + sector.ToString() + " sector."),
            };
        }
        catch(Exception excep) { throw new CustomAppException("Error retrieving percent TAN for excreta: " + excep.Message + "."); }
    }
    /// <summary>
    /// Public method to get volatile solids in excreta
    /// </summary>
    /// <param name="sector">sector (should only be pig, poultry or minor livestock)</param>
    /// <param name="animaltype">integer value for animal type enumerator</param>
    /// <returns>Volatile solids excretion in kg per head</returns>
    /// <exception cref="CustomAppException"></exception>
    public static double GetExcretaVolatileSolids(Sector sector, int animaltype)
    {
        try
        {
            return sector switch
            {
                Sector.Poultry => Instance.PoultryVolatileSolidsExcretion[(PoultryType)animaltype],
                Sector.Pigs => Instance.PigVolatileSolidsExcretion[(PigType)animaltype],
                Sector.MinorLivestock => Instance.MinorLivestockVolatileSolidsExcretion[(MinorLivestockType)animaltype],
                _ => throw new CustomAppException("No nitrogen excretion lookup exists for the " + sector.ToString() + " sector."),
            };
        }
        catch(Exception excep) { throw new CustomAppException("Error retrieving percent TAN for excreta: " + excep.Message + "."); }
    }

    public static double RetrieveNMVOCGrazing(Sector sector, int animalType)
    {
        return LivestockNonGHGLookup.Instance.RetrieveNMVOCGrazing((int)sector, animalType);
    }

    public static double FractionSilageStore(Sector sector, int animalType)
    {
        return LivestockNonGHGLookup.Instance.RetrieveFractionSilageStore((int)sector, animalType);
    }

    public static double FractionSilage(Sector sector, int animalType)
    {
        return LivestockNonGHGLookup.Instance.RetrieveFractionSilage((int)sector, animalType);
    }

    public static double RetrieveNMVOCHouse(Sector sector, int animalType)
    {
        return LivestockNonGHGLookup.Instance.RetrieveNMVOCHousing((int)sector, animalType);
    }

    public static double RetrievePM2_5(Sector sector, int animalType)
    {
        return LivestockNonGHGLookup.Instance.RetrievePM25((int)sector, animalType);
    }

    public static double RetrievePM10(Sector sector, int animalType)
    {
        return LivestockNonGHGLookup.Instance.RetrievePM10((int)sector, animalType);
    }

    public static double RetrieveTSP(Sector sector, int animalType)
    {
        return LivestockNonGHGLookup.Instance.RetrieveTSP((int)sector, animalType);
    }

    public static double RetrieveNMVOCSilage(Sector sector, int animalType)
    {
        return LivestockNonGHGLookup.Instance.RetrieveNMVOCSilage((int)sector, animalType);
    }
}
