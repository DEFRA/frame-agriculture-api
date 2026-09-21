using FrameAgricultureApi.CustomErrorHandling;
using FrameAgricultureApi.Generic;
using System.Reflection;
using static FrameAgricultureApi.Enumerators.Enumerators;

namespace FrameAgricultureApi.Libraries.LookUps;

public class DairyDietLookup
{
    private static DairyDietLookup? _instance;

    public static DairyDietLookup Instance
    {
        get
        {
            if(_instance == null)
            {
                _instance = new DairyDietLookup();
            }
            return _instance;
        }
    }

    private Dictionary<ConcentrateDietKey, ConcentrateDietData> _ConcentrateInfo = new Dictionary<ConcentrateDietKey, ConcentrateDietData>(new ConcentrateDietKey.EqualityComparer());
    private Dictionary<MilkDietKey, MilkDietData> _MilkInfo = new Dictionary<MilkDietKey, MilkDietData>(new MilkDietKey.EqualityComparer());
    private Dictionary<ForageDietKey, ForageDietData> _ForageInfo = new Dictionary<ForageDietKey, ForageDietData>(new ForageDietKey.EqualityComparer());

    private DairyDietLookup()
    {
        _ConcentrateInfo.Clear();

        MemoryStream memoryStream = HelperFunctions.GetLUT(Assembly.GetExecutingAssembly(), "FLEA_ConcentrateDiet_LUT.dat");
        try
        {
            using(BinaryReader binaryReader = new BinaryReader(memoryStream))
            {
                while(binaryReader.BaseStream.Position < binaryReader.BaseStream.Length)
                {
                    int year = binaryReader.ReadInt32();
                    int country = binaryReader.ReadInt32();
                    int cattleType = binaryReader.ReadInt32();
                    int cattleBreed = binaryReader.ReadInt32();

                    ConcentrateDietKey theKey = new()
                    {
                        Country = country,
                        CattleType = cattleType,
                        CattleBreed = cattleBreed,
                    };

                    ConcentrateDietData data = new()
                    {
                        ConcentrateUse = binaryReader.ReadDouble(),
                        ME = binaryReader.ReadDouble(),
                        GE = binaryReader.ReadDouble(),
                        CP = binaryReader.ReadDouble(),
                        DMC = binaryReader.ReadDouble(),
                        DMD = binaryReader.ReadDouble(),
                    };

                    _ConcentrateInfo.Add(theKey, data);
                }
            }
            memoryStream.Close();
        }
        catch(Exception ex)
        {
            throw new CustomAppException("Error reading Concentrate Diet lookup: " + ex.Message);
        }
        finally
        {
            memoryStream.Close();
        }

        _MilkInfo.Clear();

        memoryStream = HelperFunctions.GetLUT(Assembly.GetExecutingAssembly(), "FLEA_MilkDiet_LUT.dat");
        try
        {
            using(BinaryReader binaryReader = new BinaryReader(memoryStream))
            {
                while(binaryReader.BaseStream.Position < binaryReader.BaseStream.Length)
                {
                    int country = binaryReader.ReadInt32();
                    int cattleType = binaryReader.ReadInt32();
                    int cattleBreed = binaryReader.ReadInt32();

                    MilkDietKey theKey = new()
                    {
                        Country = country,
                        CattleType = cattleType,
                        CattleBreed = cattleBreed,
                    };

                    MilkDietData data = new()
                    {
                        MilkYield = binaryReader.ReadDouble(),
                        MilkFat = binaryReader.ReadDouble(),
                        MilkProtein = binaryReader.ReadDouble(),
                    };

                    _MilkInfo.Add(theKey, data);
                }
            }
            memoryStream.Close();
        }
        catch(Exception ex)
        {
            throw new CustomAppException("Error reading Milk Diet lookup: " + ex.Message);
        }
        finally
        {
            memoryStream.Close();
        }

        _ForageInfo.Clear();

        memoryStream = HelperFunctions.GetLUT(Assembly.GetExecutingAssembly(), "FLEA_DairyForageDiet_LUT.dat");
        try
        {
            using(BinaryReader binaryReader = new BinaryReader(memoryStream))
            {
                while(binaryReader.BaseStream.Position < binaryReader.BaseStream.Length)
                {
                    int country = binaryReader.ReadInt32();
                    int cattleType = binaryReader.ReadInt32();
                    int managementRegime = binaryReader.ReadInt32();
                    int month = binaryReader.ReadInt32();

                    ForageDietKey theKey = new()
                    {
                        Country = country,
                        CattleType = cattleType,
                        ManagementRegime = managementRegime,
                        Month = month
                    };

                    ForageDietData data = new()
                    {
                        ME = binaryReader.ReadDouble(),
                        GE = binaryReader.ReadDouble(),
                        CP = binaryReader.ReadDouble(),
                        DMC = binaryReader.ReadDouble(),
                        DMD = binaryReader.ReadDouble(),
                    };

                    _ForageInfo.Add(theKey, data);
                }
            }
            memoryStream.Close();
        }
        catch(Exception ex)
        {
            throw new CustomAppException("Error reading Forage Diet lookup: " + ex.Message);
        }
        finally
        {
            memoryStream.Close();
        }
    }

    #region Concentrate Retrieve
    /// <summary>
    /// Retrieve the default concentrate intake for a given country, cattle type and breed size.
    /// </summary>
    /// <param name="country"></param>
    /// <param name="cattleType"></param>
    /// <param name="cattleBreed"></param>
    /// <returns></returns>
    /// <exception cref="CustomAppException"></exception>
    public static double RetrieveConcentrateUse(Country country, DairyCattle cattleType, DairyBreedSize cattleBreed)
    {
        ConcentrateDietKey key = new((int)country, (int)cattleType, (int)cattleBreed);

        if(Instance._ConcentrateInfo.ContainsKey(key))
        {
            return Instance._ConcentrateInfo[key].ConcentrateUse;
        }

        throw new CustomAppException("The key specified does not exist");
    }
    /// <summary>
    /// Retrieve the default Metabolisable Energy (ME) content of the concentrate diet for a given country, cattle type and breed size.
    /// </summary>
    /// <param name="country"></param>
    /// <param name="cattleType"></param>
    /// <param name="cattleBreed"></param>
    /// <returns></returns>
    /// <exception cref="CustomAppException"></exception>
    public static double RetrieveConcentrateME(Country country, DairyCattle cattleType, DairyBreedSize cattleBreed)
    {
        ConcentrateDietKey key = new((int)country, (int)cattleType, (int)cattleBreed);

        if(Instance._ConcentrateInfo.ContainsKey(key))
        {
            return Instance._ConcentrateInfo[key].ME;
        }

        throw new CustomAppException("The key specified does not exist");
    }
    /// <summary>
    /// Retrieve the default Gross Energy (GE) content of the concentrate diet for a given country, cattle type and breed size.
    /// </summary>
    /// <param name="country"></param>
    /// <param name="cattleType"></param>
    /// <param name="cattleBreed"></param>
    /// <returns></returns>
    /// <exception cref="CustomAppException"></exception>
    public static double RetrieveConcentrateGE(Country country, DairyCattle cattleType, DairyBreedSize cattleBreed)
    {
        ConcentrateDietKey key = new((int)country, (int)cattleType, (int)cattleBreed);

        if(Instance._ConcentrateInfo.ContainsKey(key))
        {
            return Instance._ConcentrateInfo[key].GE;
        }

        throw new CustomAppException("The key specified does not exist");
    }
    /// <summary>
    /// Retrieve the default Crude Protein (CP) content of the concentrate diet for a given country, cattle type and breed size.
    /// </summary>
    /// <param name="country"></param>
    /// <param name="cattleType"></param>
    /// <param name="cattleBreed"></param>
    /// <returns></returns>
    /// <exception cref="CustomAppException"></exception>
    public static double RetrieveConcentrateCP(Country country, DairyCattle cattleType, DairyBreedSize cattleBreed)
    {
        ConcentrateDietKey key = new((int)country, (int)cattleType, (int)cattleBreed);

        if(Instance._ConcentrateInfo.ContainsKey(key))
        {
            return Instance._ConcentrateInfo[key].CP;
        }

        throw new CustomAppException("The key specified does not exist");
    }
    /// <summary>
    /// Retrieve the default Dry Matter Content (DMC) of the concentrate diet for a given country, cattle type and breed size.
    /// </summary>
    /// <param name="country"></param>
    /// <param name="cattleType"></param>
    /// <param name="cattleBreed"></param>
    /// <returns></returns>
    /// <exception cref="CustomAppException"></exception>
    public static double RetrieveConcentrateDMC(Country country, DairyCattle cattleType, DairyBreedSize cattleBreed)
    {
        ConcentrateDietKey key = new((int)country, (int)cattleType, (int)cattleBreed);

        if(Instance._ConcentrateInfo.ContainsKey(key))
        {
            return Instance._ConcentrateInfo[key].DMC;
        }

        throw new CustomAppException("The key specified does not exist");
    }
    /// <summary>
    /// Retrieve the default Dry Matter Digestibility (DMD) of the concentrate diet for a given country, cattle type and breed size.
    /// </summary>
    /// <param name="country"></param>
    /// <param name="cattleType"></param>
    /// <param name="cattleBreed"></param>
    /// <returns></returns>
    /// <exception cref="CustomAppException"></exception>
    public static double RetrieveConcentrateDMD(Country country, DairyCattle cattleType, DairyBreedSize cattleBreed)
    {
        ConcentrateDietKey key = new((int)country, (int)cattleType, (int)cattleBreed);

        if(Instance._ConcentrateInfo.ContainsKey(key))
        {
            return Instance._ConcentrateInfo[key].DMD;
        }

        throw new CustomAppException("The key specified does not exist");
    }
    #endregion

    #region Milk Retrieve
    /// <summary>
    /// Retrieve the default milk yield for a given country, cattle type and breed size.
    /// </summary>
    /// <param name="country"></param>
    /// <param name="cattleType"></param>
    /// <param name="cattleBreed"></param>
    /// <returns></returns>
    /// <exception cref="CustomAppException"></exception>
    public static double RetrieveMilkYield(Country country, DairyCattle cattleType, DairyBreedSize cattleBreed)
    {
        MilkDietKey key = new((int)country, (int)cattleType, (int)cattleBreed);

        if(Instance._MilkInfo.ContainsKey(key))
        {
            return Instance._MilkInfo[key].MilkYield;
        }

        throw new CustomAppException("The key specified does not exist");
    }
    /// <summary>
    /// Retrieve the default milk fat content for a given country, cattle type and breed size.
    /// </summary>
    /// <param name="country"></param>
    /// <param name="cattleType"></param>
    /// <param name="cattleBreed"></param>
    /// <returns></returns>
    /// <exception cref="CustomAppException"></exception>
    public static double RetrieveMilkFat(Country country, DairyCattle cattleType, DairyBreedSize cattleBreed)
    {
        MilkDietKey key = new((int)country, (int)cattleType, (int)cattleBreed);

        if(Instance._MilkInfo.ContainsKey(key))
        {
            return Instance._MilkInfo[key].MilkFat;
        }

        throw new CustomAppException("The key specified does not exist");
    }
    /// <summary>
    /// Retrieve the default milk protein content for a given country, cattle type and breed size.
    /// </summary>
    /// <param name="country"></param>
    /// <param name="cattleType"></param>
    /// <param name="cattleBreed"></param>
    /// <returns></returns>
    /// <exception cref="CustomAppException"></exception>
    public static double RetrieveMilkProtein(Country country, DairyCattle cattleType, DairyBreedSize cattleBreed)
    {
        MilkDietKey key = new((int)country, (int)cattleType, (int)cattleBreed);

        if(Instance._MilkInfo.ContainsKey(key))
        {
            return Instance._MilkInfo[key].MilkProtein;
        }

        throw new CustomAppException("The key specified does not exist");
    }

    #endregion

    #region Forage Retireve
    /// <summary>
    /// Retrieve the default Metabolisable Energy (ME) content of the forage diet for a given country, cattle type, management regime and month.
    /// </summary>
    /// <param name="country"></param>
    /// <param name="cattleType"></param>
    /// <param name="managementRegime"></param>
    /// <param name="month"></param>
    /// <returns></returns>
    /// <exception cref="CustomAppException"></exception>
    public static double RetrieveForageME(Country country, DairyCattle cattleType, ManagementRegime managementRegime, Month month)
    {
        ForageDietKey key = new((int)country, (int)cattleType, (int)managementRegime, (int)month);

        if(Instance._ForageInfo.ContainsKey(key))
        {
            return Instance._ForageInfo[key].ME;
        }

        throw new CustomAppException("The key specified does not exist");
    }
    /// <summary>
    /// Retrieve the default Gross Energy (GE) content of the forage diet for a given country, cattle type, management regime and month.
    /// </summary>
    /// <param name="country"></param>
    /// <param name="cattleType"></param>
    /// <param name="managementRegime"></param>
    /// <param name="month"></param>
    /// <returns></returns>
    /// <exception cref="CustomAppException"></exception>
    public static double RetrieveForageGE(Country country, DairyCattle cattleType, ManagementRegime managementRegime, Month month)
    {
        ForageDietKey key = new((int)country, (int)cattleType, (int)managementRegime, (int)month);

        if(Instance._ForageInfo.ContainsKey(key))
        {
            return Instance._ForageInfo[key].GE;
        }

        throw new CustomAppException("The key specified does not exist");
    }
    /// <summary>
    /// Retrieve the default Crude Protein (CP) content of the forage diet for a given country, cattle type, management regime and month.
    /// </summary>
    /// <param name="country"></param>
    /// <param name="cattleType"></param>
    /// <param name="managementRegime"></param>
    /// <param name="month"></param>
    /// <returns></returns>
    /// <exception cref="CustomAppException"></exception>
    public static double RetrieveForageCP(Country country, DairyCattle cattleType, ManagementRegime managementRegime, Month month)
    {
        ForageDietKey key = new((int)country, (int)cattleType, (int)managementRegime, (int)month);

        if(Instance._ForageInfo.ContainsKey(key))
        {
            return Instance._ForageInfo[key].CP;
        }

        throw new CustomAppException("The key specified does not exist");
    }
    /// <summary>
    /// Retrieve the default Dry Matter Content (DMC) of the forage diet for a given country, cattle type, management regime and month.
    /// </summary>
    /// <param name="country"></param>
    /// <param name="cattleType"></param>
    /// <param name="managementRegime"></param>
    /// <param name="month"></param>
    /// <returns></returns>
    /// <exception cref="CustomAppException"></exception>
    public static double RetrieveForageDMC(Country country, DairyCattle cattleType, ManagementRegime managementRegime, Month month)
    {
        ForageDietKey key = new((int)country, (int)cattleType, (int)managementRegime, (int)month);

        if(Instance._ForageInfo.ContainsKey(key))
        {
            return Instance._ForageInfo[key].DMC;
        }

        throw new CustomAppException("The key specified does not exist");
    }
    /// <summary>
    /// Retrieve the default Dry Matter Digestibility (DMD) of the forage diet for a given country, cattle type, management regime and month.
    /// </summary>
    /// <param name="country"></param>
    /// <param name="cattleType"></param>
    /// <param name="managementRegime"></param>
    /// <param name="month"></param>
    /// <returns></returns>
    /// <exception cref="CustomAppException"></exception>
    public static double RetrieveForageDMD(Country country, DairyCattle cattleType, ManagementRegime managementRegime, Month month)
    {
        ForageDietKey key = new((int)country, (int)cattleType, (int)managementRegime, (int)month);

        if(Instance._ForageInfo.ContainsKey(key))
        {
            return Instance._ForageInfo[key].DMD;
        }

        throw new CustomAppException("The key specified does not exist");
    }

    #endregion

    #region Milk dictionary key
    /// <summary>
    /// Class representing key for the milk diet lookup dictionary
    /// </summary>
    public class MilkDietKey
    {
        /// <summary>
        /// Country for lookup key
        /// </summary>
        public int Country { get; set; }
        /// <summary>
        /// Cattle type for lookup key
        /// </summary>
        public int CattleType { get; set; }
        /// <summary>
        /// Cattle breed size for lookup key
        /// </summary>
        public int CattleBreed { get; set; }
        /// <summary>
        /// Constructor for the MilkDietKey class, which initializes the country, cattle type and cattle breed properties.
        /// </summary>
        /// <param name="country"></param>
        /// <param name="cattleType"></param>
        /// <param name="cattleBreed"></param>
        public MilkDietKey(int country, int cattleType, int cattleBreed)
        {
            Country = country;
            CattleType = cattleType;
            CattleBreed = cattleBreed;
        }
        /// <summary>
        /// Constructor for the MilkDietKey class, which initializes an empty key. This can be used when the properties will be set manually after instantiation.
        /// </summary>
        public MilkDietKey() { }

        /// <summary>
        /// Eqaulity comparer for the MilkDietKey class, which compares two keys based on their country, cattle type and cattle breed properties. This is used to ensure that the dictionary can correctly identify keys and retrieve the corresponding values.
        /// </summary>
        public class EqualityComparer : IEqualityComparer<MilkDietKey>
        {
            /// <summary>
            /// Equals override for the MilkDietKey class
            /// </summary>
            /// <param name="x"></param>
            /// <param name="y"></param>
            /// <returns></returns>
            public bool Equals(MilkDietKey? x, MilkDietKey? y)
            {
                if(x == null || y == null)
                {
                    return false;
                }

                return x.Country == y.Country && x.CattleType == y.CattleType && x.CattleBreed == y.CattleBreed;
            }
            /// <summary>
            /// Hash code override for the MilkDietKey class
            /// </summary>
            /// <param name="key"></param>
            /// <returns></returns>
            public int GetHashCode(MilkDietKey key)
            {
                return key.Country ^ key.CattleType ^ key.CattleBreed;
            }
        }
    }
    /// <summary>
    /// Class representing the data for the milk diet lookup dictionary
    /// </summary>
    public class MilkDietData
    {
        /// <summary>
        /// Represents the default milk yield for a given country cattle type and breed.
        /// </summary>
        public double MilkYield { get; set; }
        /// <summary>
        /// Represents the default milk fat content for a given country cattle type and breed.
        /// </summary>
        public double MilkFat { get; set; }
        /// <summary>
        /// Represents the default milk protein content for a given country cattle type and breed.
        /// </summary>
        public double MilkProtein { get; set; }
        /// <summary>
        /// Constructor for the MilkDietData class, which initializes the milk yield, milk fat and milk protein properties.
        /// </summary>
        /// <param name="milkYield"></param>
        /// <param name="milkFat"></param>
        /// <param name="milkProtein"></param>
        public MilkDietData(double milkYield, double milkFat, double milkProtein)
        {
            MilkYield = milkYield;
            MilkFat = milkFat;
            MilkProtein = milkProtein;
        }
        /// <summary>
        /// Constructor for the MilkDietData class, which initializes an empty data object. This can be used when the properties will be set manually after instantiation.
        /// </summary>
        public MilkDietData() { }
    }
    #endregion

    #region Concentrate dictionary key
    /// <summary>
    /// Class representing the data for the concentrate diet lookup dictionary
    /// </summary>
    public class ConcentrateDietData
    {
        /// <summary>
        ///
        /// </summary>
        public double ConcentrateUse { get; set; }
        public double ME { get; set; }
        public double GE { get; set; }
        public double CP { get; set; }
        public double DMC { get; set; }
        public double DMD { get; set; }

        public ConcentrateDietData(double concentrateUse, double me, double ge, double cp, double dmc, double dmd)
        {
            ConcentrateUse = concentrateUse;
            ME = me;
            GE = ge;
            CP = cp;
            DMC = dmc;
            DMD = dmd;
        }

        public ConcentrateDietData() { }
    }
    /// <summary>
    /// Class representing the key for the concentrate diet lookup dictionary
    /// </summary>
    public class ConcentrateDietKey
    {
        /// <summary>
        /// Country for lookup key
        /// </summary>
        public int Country { get; set; }
        public int CattleType { get; set; }
        public int CattleBreed { get; set; }

        public ConcentrateDietKey(int country, int cattleType, int cattleBreed)
        {
            Country = country;
            CattleType = cattleType;
            CattleBreed = cattleBreed;
        }

        public ConcentrateDietKey() { }

        public class EqualityComparer : IEqualityComparer<ConcentrateDietKey>
        {
            public bool Equals(ConcentrateDietKey? x, ConcentrateDietKey? y)
            {
                if(x == null || y == null)
                {
                    return false;
                }

                return x.Country == y.Country && x.CattleType == y.CattleType && x.CattleBreed == y.CattleBreed;
            }

            public int GetHashCode(ConcentrateDietKey key)
            {
                return key.Country ^ key.CattleType ^ key.CattleBreed;
            }
        }
    }

    #endregion

    #region Forage Dictionary

    public class ForageDietData
    {
        public double ME { get; set; }
        public double GE { get; set; }
        public double CP { get; set; }
        public double DMC { get; set; }
        public double DMD { get; set; }
        public double PercentageForageDiet { get; set; }
        public ForageDietData(double me, double ge, double cp, double dmc, double dmd, double percentageForageDiet)
        {
            ME = me;
            GE = ge;
            CP = cp;
            DMC = dmc;
            DMD = dmd;
            PercentageForageDiet = percentageForageDiet;
        }

        public ForageDietData() { }
    }

    public class ForageDietKey
    {
        public int Country { get; set; }
        public int CattleType { get; set; }
        public int ManagementRegime { get; set; }
        public int Month { get; set; }

        public ForageDietKey(int country, int cattleType, int managementRegime, int month)
        {
            Country = country;
            CattleType = cattleType;
            ManagementRegime = managementRegime;
            Month = month;
        }

        public ForageDietKey() { }

        public class EqualityComparer : IEqualityComparer<ForageDietKey>
        {

            public bool Equals(ForageDietKey? x, ForageDietKey? y)
            {
                if(x == null || y == null)
                {
                    return false;
                }

                return x.Country == y.Country && x.CattleType == y.CattleType && x.ManagementRegime == y.ManagementRegime && x.Month == y.Month;
            }

            public int GetHashCode(ForageDietKey key)
            {
                return key.Country ^ key.CattleType ^ key.ManagementRegime ^ key.Month;
            }
        }
    }

    #endregion
}

