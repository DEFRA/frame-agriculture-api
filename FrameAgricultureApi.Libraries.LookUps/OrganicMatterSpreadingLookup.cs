using FrameAgricultureApi.CustomErrorHandling;
using FrameAgricultureApi.Generic;
using System.Reflection;
using static FrameAgricultureApi.Enumerators.Enumerators;

namespace FrameAgricultureApi.Libraries.LookUps;

/// <summary>
/// The Organic Matter SPreading lookup class
/// </summary>
public class OrganicMatterSpreadingLookup
{
    /// <summary>
    /// private instance of Organic Matter Spreading Lookuo Singleton
    /// </summary>
    public static OrganicMatterSpreadingLookup? _instance;

    /// <summary>
    /// Public Instance for the organic matter applicatoin lookup class
    /// </summary>
    public static OrganicMatterSpreadingLookup Instance
    {
        get
        {
            _instance ??= new OrganicMatterSpreadingLookup();
            return _instance;
        }
    }

    //Define Parameters
    /// <summary>
    /// Non-manure based organic matter lookup dictionary (of NonManureOrganicMatter objects) indexed by
    /// (non-manure) organic matter type
    /// </summary>
    public readonly Dictionary<Tuple<int, int>, OrganicMatter> NonManureOrganicMatterLookup;
    /// <summary>
    /// Non-cattle manure lookup dictionary (of ManureOrganicMatter objects) indexed by organic matter (manure) type and land use type
    /// </summary>
    public readonly Dictionary<Tuple<int, int>, OrganicMatter> NoncattleManureOrganicMatterLookup;
    /// <summary>
    /// Cattle manure lookup dictionary (of manure organic matter objects), indexed by organic matter (manure type), month and land use type
    /// </summary>
    public readonly Dictionary<Tuple<int, int, int>, OrganicMatter> CattleManureOrganicMatterLookup;

    /// <summary>
    /// Initilaises OrganicMatterSpreadingLookpup singleton class
    /// Loads data from LUT file
    /// </summary>
    /// <exception cref="CustomAppException"></exception>
    public OrganicMatterSpreadingLookup()
    {
        //Clear dictionaries
        NonManureOrganicMatterLookup = [];
        CattleManureOrganicMatterLookup = [];
        NoncattleManureOrganicMatterLookup = [];

        //Data
        //Non-manure based organic matter
        MemoryStream memoryStream = HelperFunctions.GetLUT(Assembly.GetExecutingAssembly(), "FLEA_OrganicMatterSpreading_LUT.dat");
        try
        {
            using BinaryReader binaryReader = new(memoryStream);
            //Read in the manure lookup information
            while(binaryReader.BaseStream.Position < binaryReader.BaseStream.Length)
            {
                int index = binaryReader.ReadInt32();
                int landUseID = binaryReader.ReadInt32();
                double N_kgpert = binaryReader.ReadDouble();
                OrganicMatter item = new OrganicMatter();

                item.contentN = N_kgpert / 1000.0; //convert from kg per tonne to kg per kg by dividing by 1000
                item.percentTAN = binaryReader.ReadDouble();
                item.NH3N_EF = binaryReader.ReadDouble();
                item.N2ON_EF = binaryReader.ReadDouble();
                item.ratioNON = binaryReader.ReadDouble();
                item.ratioN2N = binaryReader.ReadDouble();
                item.fracLeach = binaryReader.ReadDouble();
                NonManureOrganicMatterLookup.Add(new Tuple<int, int>(index, landUseID), item);
            }
        }
        catch(Exception ex)
        {
            throw new CustomAppException("Error loading non-manure-based organic matter information: " + ex.Message);
        }

        memoryStream.Close();

        //Non-cattle manure - minor livestock
        memoryStream.Close();
        memoryStream.Dispose();
        memoryStream = new MemoryStream();
        memoryStream = HelperFunctions.GetLUT(Assembly.GetExecutingAssembly(), "FLEA_MinorLivestockManureSpreading_LUT.dat");

        try
        {
            using BinaryReader binaryReader = new(memoryStream);
            //Read in the manure lookup information
            while(binaryReader.BaseStream.Position < binaryReader.BaseStream.Length)
            {
                int index = binaryReader.ReadInt32();
                int landUseID = binaryReader.ReadInt32();

                OrganicMatter item = new OrganicMatter();
                item.NH3N_EF = binaryReader.ReadDouble();
                item.N2ON_EF = binaryReader.ReadDouble();
                item.ratioNON = binaryReader.ReadDouble();
                item.ratioN2N = binaryReader.ReadDouble();
                item.fracLeach = binaryReader.ReadDouble();
                NoncattleManureOrganicMatterLookup.Add(new Tuple<int, int>(index, landUseID), item);
            }
        }
        catch(Exception ex)
        {
            throw new CustomAppException("Error loading minor livestock manure information: " + ex.Message);
        }

        memoryStream.Close();

        //Non-cattle manure - Pigs
        memoryStream = HelperFunctions.GetLUT(Assembly.GetExecutingAssembly(), "FLEA_PigManureSpreading_LUT.dat");

        try
        {
            using BinaryReader binaryReader = new(memoryStream);
            //Read in the manure lookup information
            while(binaryReader.BaseStream.Position < binaryReader.BaseStream.Length)
            {
                int index = binaryReader.ReadInt32();
                int landUseID = binaryReader.ReadInt32();
                OrganicMatter item = new OrganicMatter();
                item.NH3N_EF = binaryReader.ReadDouble();
                item.N2ON_EF = binaryReader.ReadDouble();
                item.ratioNON = binaryReader.ReadDouble();
                item.ratioN2N = binaryReader.ReadDouble();
                item.fracLeach = binaryReader.ReadDouble();
                NoncattleManureOrganicMatterLookup.Add(new Tuple<int, int>(index, landUseID), item);
            }
        }
        catch(Exception ex)
        {
            throw new CustomAppException("Error loading pig manure information: " + ex.Message);
        }

        memoryStream.Close();

        //Non-cattle manure - Poultry
        memoryStream.Close();
        memoryStream.Dispose();
        memoryStream = new MemoryStream();
        memoryStream = HelperFunctions.GetLUT(Assembly.GetExecutingAssembly(), "FLEA_PoultryManureSpreading_LUT.dat");

        try
        {
            using BinaryReader binaryReader = new(memoryStream);
            //Read in the manure lookup information
            while(binaryReader.BaseStream.Position < binaryReader.BaseStream.Length)
            {
                int index = binaryReader.ReadInt32();
                int landUseID = binaryReader.ReadInt32();
                OrganicMatter item = new OrganicMatter();
                item.NH3N_EF = binaryReader.ReadDouble();
                item.N2ON_EF = binaryReader.ReadDouble();
                item.ratioNON = binaryReader.ReadDouble();
                item.ratioN2N = binaryReader.ReadDouble();
                item.fracLeach = binaryReader.ReadDouble();
                NoncattleManureOrganicMatterLookup.Add(new Tuple<int, int>(index, landUseID), item);
            }
        }
        catch(Exception ex)
        {
            throw new CustomAppException("Error loading poultry manure information: " + ex.Message);
        }

        memoryStream.Close();

        //Non-cattle manure - Sheep (temporary)
        memoryStream.Close();
        memoryStream.Dispose();
        memoryStream = new MemoryStream();
        memoryStream = HelperFunctions.GetLUT(Assembly.GetExecutingAssembly(), "FLEA_SheepManureSpreading_LUT.dat");
        try
        {
            using BinaryReader binaryReader = new(memoryStream);
            //Read in the manure lookup information
            while(binaryReader.BaseStream.Position < binaryReader.BaseStream.Length)
            {
                int index = binaryReader.ReadInt32();
                int landUseID = binaryReader.ReadInt32();
                OrganicMatter item = new OrganicMatter();
                item.NH3N_EF = binaryReader.ReadDouble();
                item.N2ON_EF = binaryReader.ReadDouble();
                item.ratioNON = binaryReader.ReadDouble();
                item.ratioN2N = binaryReader.ReadDouble();
                item.fracLeach = binaryReader.ReadDouble();
                NoncattleManureOrganicMatterLookup.Add(new Tuple<int, int>(index, landUseID), item);
            }
        }
        catch(Exception ex)
        {
            throw new CustomAppException("Error loading sheep manure (temporary) information: " + ex.Message);
        }

        memoryStream.Close();

        //Cattle manure
        memoryStream.Close();
        memoryStream.Dispose();
        memoryStream = new MemoryStream();
        memoryStream = HelperFunctions.GetLUT(Assembly.GetExecutingAssembly(), "FLEA_CattleManureSpreading_LUT.dat");
        try
        {
            using BinaryReader binaryReader = new(memoryStream);
            //Read in the manure lookup information
            while(binaryReader.BaseStream.Position < binaryReader.BaseStream.Length)
            {
                int index = binaryReader.ReadInt32();
                int month = binaryReader.ReadInt32();
                int landUseID = binaryReader.ReadInt32();
                OrganicMatter item = new OrganicMatter();
                item.NH3N_EF = binaryReader.ReadDouble();
                item.N2ON_EF = binaryReader.ReadDouble();
                item.ratioNON = binaryReader.ReadDouble();
                item.ratioN2N = binaryReader.ReadDouble();
                item.fracLeach = binaryReader.ReadDouble();
                CattleManureOrganicMatterLookup.Add(new Tuple<int, int, int>(index, month, landUseID), item);
            }
        }
        catch(Exception ex)
        {
            throw new CustomAppException("Error loading cattle manure information: " + ex.Message);
        }

        memoryStream.Close();
    }

    /// <summary>
    /// Retrieves N content of organic matter based on the organic matter type.
    /// The optional land use type parameter is required for all manure types, but the month is only
    /// required for cattle manures
    /// </summary>
    /// <param name="type">organic matter type enumerator</param>
    /// <param name="month">Month of application as enumerator</param>
    /// <param name="slutype">land use organic matter applied to as enumerator</param>
    /// <returns>The N content in kg per kg organic matter if key exsits, otherwise double.NaN</returns>
    public static double RetrieveNContent(OrganicMatterType type, SpreadingLandUse slutype, Month month)
    {

        switch(type)
        {
            case OrganicMatterType.SewageSludgeLiquid:
            case OrganicMatterType.SewageSludgeCake:
            case OrganicMatterType.DigestateFoodbased:
            case OrganicMatterType.DigestateCropbased:
            case OrganicMatterType.DigestateOtherOrganicResidue:
            case OrganicMatterType.Compost:
                Tuple<int, int> key = new Tuple<int, int>((int)type, (int)slutype);
                return Instance.NonManureOrganicMatterLookup.ContainsKey(key) ? Instance.NonManureOrganicMatterLookup[key].contentN : double.NaN;

            // should not be getting N content for manures using this call so all below will return double.NaN
            case OrganicMatterType.DigestatePigFYM:
            case OrganicMatterType.DigestatePigSlurry:
            case OrganicMatterType.PigFYM:
            case OrganicMatterType.PigSlurry:
            case OrganicMatterType.MinorLivestockFYM:
            case OrganicMatterType.SheepFYM:
            case OrganicMatterType.PoultryManureDigestate:
            case OrganicMatterType.PoultryLayerManure:
            case OrganicMatterType.PoultryLitter:
            case OrganicMatterType.DuckFYM:
                Tuple<int, int> mkey = new Tuple<int, int>((int)type, (int)slutype);
                return Instance.NoncattleManureOrganicMatterLookup.ContainsKey(mkey) ? Instance.NoncattleManureOrganicMatterLookup[mkey].contentN : double.NaN;

            case OrganicMatterType.CattleFYM:
            case OrganicMatterType.CattleSlurry:
            case OrganicMatterType.DigestateCattleSlurry:
            case OrganicMatterType.DigestateCattleFYM:
                Tuple<int, int, int> cmkey = new Tuple<int, int, int>((int)type, (int)month, (int)slutype);
                return Instance.CattleManureOrganicMatterLookup.ContainsKey(cmkey) ? Instance.CattleManureOrganicMatterLookup[cmkey].contentN : double.NaN;

            default:
                return double.NaN;
        }
    }
    /// <summary>
    /// Retrieves Percent TAN content of organic matter based on the organic matter type.
    /// The optional land use type parameter is required for all manure types, but the month is only
    /// required for cattle manures
    /// </summary>
    /// <param name="type">organic matter type enumerator</param>
    /// <param name="month">Month of application as enumerator</param>
    /// <param name="slutype">land use organic matter applied to as enumerator</param>
    /// <returns>The percent TAN in organic matter if key exsits, otherwise double.NaN</returns>
    public static double RetrievePercentTan(OrganicMatterType type, SpreadingLandUse slutype, Month month)
    {
        switch(type)
        {
            case OrganicMatterType.SewageSludgeLiquid:
            case OrganicMatterType.SewageSludgeCake:
            case OrganicMatterType.DigestateFoodbased:
            case OrganicMatterType.DigestateCropbased:
            case OrganicMatterType.DigestateOtherOrganicResidue:
            case OrganicMatterType.Compost:
                Tuple<int, int> key = new Tuple<int, int>((int)type, (int)slutype);
                return Instance.NonManureOrganicMatterLookup.ContainsKey(key) ? Instance.NonManureOrganicMatterLookup[key].percentTAN : double.NaN;

            // should not be getting percent TAN for manures using this call so all below will return double.NaN
            case OrganicMatterType.DigestatePigFYM:
            case OrganicMatterType.DigestatePigSlurry:
            case OrganicMatterType.PigFYM:
            case OrganicMatterType.PigSlurry:
            case OrganicMatterType.MinorLivestockFYM:
            case OrganicMatterType.SheepFYM:
            case OrganicMatterType.PoultryManureDigestate:
            case OrganicMatterType.PoultryLayerManure:
            case OrganicMatterType.PoultryLitter:
            case OrganicMatterType.DuckFYM:
                Tuple<int, int> mkey = new Tuple<int, int>((int)type, (int)slutype);
                return Instance.NoncattleManureOrganicMatterLookup.ContainsKey(mkey) ? Instance.NoncattleManureOrganicMatterLookup[mkey].percentTAN : double.NaN;

            case OrganicMatterType.CattleFYM:
            case OrganicMatterType.CattleSlurry:
            case OrganicMatterType.DigestateCattleSlurry:
            case OrganicMatterType.DigestateCattleFYM:
                Tuple<int, int, int> cmkey = new Tuple<int, int, int>((int)type, (int)month, (int)slutype);
                return Instance.CattleManureOrganicMatterLookup.ContainsKey(cmkey) ? Instance.CattleManureOrganicMatterLookup[cmkey].percentTAN : double.NaN;

            default:
                return double.NaN;
        }
    }
    /// <summary>
    /// Retrieves NH3-N Emission Factor for application of organic matter.
    /// The optional land use type parameter is required for all manure types, but the month is only
    /// required for cattle manures
    /// </summary>
    /// <param name="type">organic matter type enumerator</param>
    /// <param name="month">Month of application as enumerator</param>
    /// <param name="slutype"> land use organic matter applied to as enumerator</param>
    /// <returns> NH3-N Emission Factor for application of organic matter as a percentage if key exists, otherwise double.NaN</returns>
    public static double RetrieveNH3NEF(OrganicMatterType type, SpreadingLandUse slutype, Month month)
    {
        switch(type)
        {
            case OrganicMatterType.SewageSludgeLiquid:
            case OrganicMatterType.SewageSludgeCake:
            case OrganicMatterType.DigestateFoodbased:
            case OrganicMatterType.DigestateCropbased:
            case OrganicMatterType.DigestateOtherOrganicResidue:
            case OrganicMatterType.Compost:
                Tuple<int, int> key = new Tuple<int, int>((int)type, (int)slutype);
                return Instance.NonManureOrganicMatterLookup.ContainsKey(key) ? Instance.NonManureOrganicMatterLookup[key].NH3N_EF : double.NaN;

            case OrganicMatterType.DigestatePigFYM:
            case OrganicMatterType.DigestatePigSlurry:
            case OrganicMatterType.PigFYM:
            case OrganicMatterType.PigSlurry:
            case OrganicMatterType.MinorLivestockFYM:
            case OrganicMatterType.SheepFYM:
            case OrganicMatterType.PoultryManureDigestate:
            case OrganicMatterType.PoultryLayerManure:
            case OrganicMatterType.PoultryLitter:
            case OrganicMatterType.DuckFYM:
                Tuple<int, int> mkey = new Tuple<int, int>((int)type, (int)slutype);
                return Instance.NoncattleManureOrganicMatterLookup.ContainsKey(mkey) ? Instance.NoncattleManureOrganicMatterLookup[mkey].NH3N_EF : double.NaN;

            case OrganicMatterType.CattleFYM:
            case OrganicMatterType.CattleSlurry:
            case OrganicMatterType.DigestateCattleSlurry:
            case OrganicMatterType.DigestateCattleFYM:
                Tuple<int, int, int> cmkey = new Tuple<int, int, int>((int)type, (int)month, (int)slutype);
                return Instance.CattleManureOrganicMatterLookup.ContainsKey(cmkey) ? Instance.CattleManureOrganicMatterLookup[cmkey].NH3N_EF : double.NaN;

            default:
                return double.NaN;
        }
    }
    /// <summary>
    /// Retrieves N2O-N Emission Factor for application of organic matter.
    /// The optional land use type parameter is required for all manure types, but the month is only
    /// required for cattle manures
    /// </summary>
    /// <param name="type">organic matter type enumerator</param>
    /// <param name="month">Month of application as enumerator</param>
    /// <param name="slutype">(Optional) land use organic matter applied to as enumerator</param>
    /// <returns> N2O-N Emission Factor for application of organic matter as a percentage if key exists, otherwise double.NaN</returns>
    public static double RetrieveN2ONEF(OrganicMatterType type, SpreadingLandUse slutype, Month month)
    {
        switch(type)
        {
            case OrganicMatterType.SewageSludgeLiquid:
            case OrganicMatterType.SewageSludgeCake:
            case OrganicMatterType.DigestateFoodbased:
            case OrganicMatterType.DigestateCropbased:
            case OrganicMatterType.DigestateOtherOrganicResidue:
            case OrganicMatterType.Compost:
                Tuple<int, int> key = new Tuple<int, int>((int)type, (int)slutype);
                return Instance.NonManureOrganicMatterLookup.ContainsKey(key) ? Instance.NonManureOrganicMatterLookup[key].N2ON_EF : double.NaN;

            case OrganicMatterType.DigestatePigFYM:
            case OrganicMatterType.DigestatePigSlurry:
            case OrganicMatterType.PigFYM:
            case OrganicMatterType.PigSlurry:
            case OrganicMatterType.MinorLivestockFYM:
            case OrganicMatterType.SheepFYM:
            case OrganicMatterType.PoultryManureDigestate:
            case OrganicMatterType.PoultryLayerManure:
            case OrganicMatterType.PoultryLitter:
            case OrganicMatterType.DuckFYM:
                Tuple<int, int> mkey = new Tuple<int, int>((int)type, (int)slutype);
                return Instance.NoncattleManureOrganicMatterLookup.ContainsKey(mkey) ? Instance.NoncattleManureOrganicMatterLookup[mkey].N2ON_EF : double.NaN;

            case OrganicMatterType.CattleFYM:
            case OrganicMatterType.CattleSlurry:
            case OrganicMatterType.DigestateCattleSlurry:
            case OrganicMatterType.DigestateCattleFYM:
                Tuple<int, int, int> cmkey = new Tuple<int, int, int>((int)type, (int)month, (int)slutype);
                return Instance.CattleManureOrganicMatterLookup.ContainsKey(cmkey) ? Instance.CattleManureOrganicMatterLookup[cmkey].N2ON_EF : double.NaN;

            default:
                return double.NaN;
        }
    }
    /// <summary>
    /// Retrieves NO-N Ratio for application of organic matter.
    /// The optional land use type parameter is required for all manure types, but the month is only
    /// required for cattle manures
    /// </summary>
    /// <param name="type">organic matter type enumerator</param>
    /// <param name="month">Month of application as enumerator</param>
    /// <param name="slutype">land use organic matter applied to as enumerator</param>
    /// <returns> NO-N Ratio for application of organic matter if key exists, otherwise double.NaN</returns>
    public static double RetrieveNONRatio(OrganicMatterType type, SpreadingLandUse slutype, Month month)
    {
        switch(type)
        {
            case OrganicMatterType.SewageSludgeLiquid:
            case OrganicMatterType.SewageSludgeCake:
            case OrganicMatterType.DigestateFoodbased:
            case OrganicMatterType.DigestateCropbased:
            case OrganicMatterType.DigestateOtherOrganicResidue:
            case OrganicMatterType.Compost:
                Tuple<int, int> key = new Tuple<int, int>((int)type, (int)slutype);
                return Instance.NonManureOrganicMatterLookup.ContainsKey(key) ? Instance.NonManureOrganicMatterLookup[key].ratioNON : double.NaN;

            case OrganicMatterType.DigestatePigFYM:
            case OrganicMatterType.DigestatePigSlurry:
            case OrganicMatterType.PigFYM:
            case OrganicMatterType.PigSlurry:
            case OrganicMatterType.MinorLivestockFYM:
            case OrganicMatterType.SheepFYM:
            case OrganicMatterType.PoultryManureDigestate:
            case OrganicMatterType.PoultryLayerManure:
            case OrganicMatterType.PoultryLitter:
            case OrganicMatterType.DuckFYM:
                Tuple<int, int> mkey = new Tuple<int, int>((int)type, (int)slutype);
                return Instance.NoncattleManureOrganicMatterLookup.ContainsKey(mkey) ? Instance.NoncattleManureOrganicMatterLookup[mkey].ratioNON : double.NaN;

            case OrganicMatterType.CattleFYM:
            case OrganicMatterType.CattleSlurry:
            case OrganicMatterType.DigestateCattleSlurry:
            case OrganicMatterType.DigestateCattleFYM:
                Tuple<int, int, int> cmkey = new Tuple<int, int, int>((int)type, (int)month, (int)slutype);
                return Instance.CattleManureOrganicMatterLookup.ContainsKey(cmkey) ? Instance.CattleManureOrganicMatterLookup[cmkey].ratioNON : double.NaN;

            default:
                return double.NaN;
        }
    }
    /// <summary>
    /// Retrieves N2-N Ratio for application of organic matter.
    /// The optional land use type parameter is required for all manure types, but the month is only
    /// required for cattle manures
    /// </summary>
    /// <param name="type">organic matter type enumerator</param>
    /// <param name="month">Month of application as enumerator</param>
    /// <param name="slutype">land use organic matter applied to as enumerator</param>
    /// <returns> NO-N Ratio for application of organic matter if key exists, otherwise double.NaN</returns>
    public static double RetrieveN2NRatio(OrganicMatterType type, SpreadingLandUse slutype, Month month)
    {
        switch(type)
        {
            case OrganicMatterType.SewageSludgeLiquid:
            case OrganicMatterType.SewageSludgeCake:
            case OrganicMatterType.DigestateFoodbased:
            case OrganicMatterType.DigestateCropbased:
            case OrganicMatterType.DigestateOtherOrganicResidue:
            case OrganicMatterType.Compost:
                Tuple<int, int> key = new Tuple<int, int>((int)type, (int)slutype);
                return Instance.NonManureOrganicMatterLookup.ContainsKey(key) ? Instance.NonManureOrganicMatterLookup[key].ratioN2N : double.NaN;

            case OrganicMatterType.DigestatePigFYM:
            case OrganicMatterType.DigestatePigSlurry:
            case OrganicMatterType.PigFYM:
            case OrganicMatterType.PigSlurry:
            case OrganicMatterType.MinorLivestockFYM:
            case OrganicMatterType.SheepFYM:
            case OrganicMatterType.PoultryManureDigestate:
            case OrganicMatterType.PoultryLayerManure:
            case OrganicMatterType.PoultryLitter:
            case OrganicMatterType.DuckFYM:
                Tuple<int, int> mkey = new Tuple<int, int>((int)type, (int)slutype);
                return Instance.NoncattleManureOrganicMatterLookup.ContainsKey(mkey) ? Instance.NoncattleManureOrganicMatterLookup[mkey].ratioN2N : double.NaN;

            case OrganicMatterType.CattleFYM:
            case OrganicMatterType.CattleSlurry:
            case OrganicMatterType.DigestateCattleSlurry:
            case OrganicMatterType.DigestateCattleFYM:
                Tuple<int, int, int> cmkey = new Tuple<int, int, int>((int)type, (int)month, (int)slutype);
                return Instance.CattleManureOrganicMatterLookup.ContainsKey(cmkey) ? Instance.CattleManureOrganicMatterLookup[cmkey].ratioN2N : double.NaN;

            default:
                return double.NaN;
        }
    }
    /// <summary>
    /// Retrieves Frac Leach Ratio for application of organic matter.
    /// The optional land use type parameter is required for all manure types, but the month is only
    /// required for cattle manures
    /// </summary>
    /// <param name="type">organic matter type enumerator</param>
    /// <param name="month">Month of application as enumerator</param>
    /// <param name="slutype">land use organic matter applied to as enumerator</param>
    /// <returns> Frac Leach Percentage for application of organic matter if key exists, otherwise double.NaN</returns>
    public static double RetrieveFracLeach(OrganicMatterType type, SpreadingLandUse slutype, Month month)
    {
        switch(type)
        {

            case OrganicMatterType.SewageSludgeLiquid:
            case OrganicMatterType.SewageSludgeCake:
            case OrganicMatterType.DigestateFoodbased:
            case OrganicMatterType.DigestateCropbased:
            case OrganicMatterType.DigestateOtherOrganicResidue:
            case OrganicMatterType.Compost:
                Tuple<int, int> key = new Tuple<int, int>((int)type, (int)slutype);
                return Instance.NonManureOrganicMatterLookup.ContainsKey(key) ? Instance.NonManureOrganicMatterLookup[key].fracLeach : double.NaN;

            case OrganicMatterType.DigestatePigFYM:
            case OrganicMatterType.DigestatePigSlurry:
            case OrganicMatterType.PigFYM:
            case OrganicMatterType.PigSlurry:
            case OrganicMatterType.MinorLivestockFYM:
            case OrganicMatterType.SheepFYM:
            case OrganicMatterType.PoultryManureDigestate:
            case OrganicMatterType.PoultryLayerManure:
            case OrganicMatterType.PoultryLitter:
            case OrganicMatterType.DuckFYM:
                Tuple<int, int> mkey = new Tuple<int, int>((int)type, (int)slutype);
                return Instance.NoncattleManureOrganicMatterLookup.ContainsKey(mkey) ? Instance.NoncattleManureOrganicMatterLookup[mkey].fracLeach : double.NaN;

            case OrganicMatterType.CattleFYM:
            case OrganicMatterType.CattleSlurry:
            case OrganicMatterType.DigestateCattleSlurry:
            case OrganicMatterType.DigestateCattleFYM:
                Tuple<int, int, int> cmkey = new Tuple<int, int, int>((int)type, (int)month, (int)slutype);
                return Instance.CattleManureOrganicMatterLookup.ContainsKey(cmkey) ? Instance.CattleManureOrganicMatterLookup[cmkey].fracLeach : double.NaN;

            default:
                return double.NaN;
        }
    }
}
/// <summary>
/// Organic Matter class to store
/// </summary>
public class OrganicMatter
{
    //Define Parameters
    /// <summary>
    ///Total N content as kg per kg of organic matter
    /// </summary>
    public double contentN { get; set; } = double.NaN;
    /// <summary>
    /// TAN content of organic matter as a percentage of Total N
    /// </summary>
    public double percentTAN { get; set; } = double.NaN;
    /// <summary>
    /// The emission factor for NH3-N as a percentage
    /// </summary>
    public double NH3N_EF { get; set; } = double.NaN;
    /// <summary>
    /// The emission factor for N2O-N as a percentage
    /// </summary>
    public double N2ON_EF { get; set; } = double.NaN;
    /// <summary>
    /// The ratio of NO-N to N2O-N
    /// </summary>
    public double ratioNON { get; set; } = double.NaN;
    /// <summary>
    /// The ratio of N2-N to N2O-N
    /// </summary>
    public double ratioN2N { get; set; } = double.NaN;
    /// <summary>
    /// The fraction of N leached to NO3-N
    /// </summary>
    public double fracLeach { get; set; } = double.NaN;
}
