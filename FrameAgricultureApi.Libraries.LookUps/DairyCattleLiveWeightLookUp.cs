using FrameAgricultureApi.CustomErrorHandling;
using FrameAgricultureApi.Generic;
using System.Reflection;
using static FrameAgricultureApi.Enumerators.Enumerators;

namespace FrameAgricultureApi.Libraries.LookUps;

/// <summary>
/// Class that gives access to look up values for dairy cattle.
/// </summary>
public class DairyCattleLiveWeightLookUp
{
    private static DairyCattleLiveWeightLookUp? _instance;
    /// <summary>
    /// Reference to the public singleton instance of the DairyCattleLiveWeightLookUp class
    /// </summary>
    public static DairyCattleLiveWeightLookUp Instance
    {
        get
        {
            if(_instance == null)
            {
                _instance = new DairyCattleLiveWeightLookUp();
            }
            return _instance;
        }
    }

    private Dictionary<DairyCattleLiveWeightKey, DairyCattleLiveWeightData> _DairyCattleLiveWeightInfo = new(new DairyCattleLiveWeightKey.EqualityComparer());

    private DairyCattleLiveWeightLookUp()
    {
        _DairyCattleLiveWeightInfo.Clear();

        MemoryStream memoryStream = HelperFunctions.GetLUT(Assembly.GetExecutingAssembly(), "FLEA_DairyCattleLiveWeight_LUT.dat");
        try
        {
            using BinaryReader binaryReader = new(memoryStream);
            while(binaryReader.BaseStream.Position < binaryReader.BaseStream.Length)
            {
                int country = binaryReader.ReadInt32();
                int breedSize = binaryReader.ReadInt32();

                DairyCattleLiveWeightKey theKey = new()
                {
                    Country = country,
                    BreedSize = breedSize
                };

                DairyCattleLiveWeightData data = new()
                {
                    AgeAtFirstConception = binaryReader.ReadDouble(),
                    AgeAtFirstCalving = binaryReader.ReadDouble(),
                    AgeAtDeath = binaryReader.ReadDouble(),
                    MatureWeight = binaryReader.ReadDouble(),
                };

                _DairyCattleLiveWeightInfo.Add(theKey, data);
            }
        }
        catch(Exception ex)
        {
            throw new CustomAppException("Error reading dairy Cattle liveweight lookup: " + ex.Message);
        }
        finally
        {
            memoryStream.Close();
        }
    }
    /// <summary>
    /// Retrieves the default age at first conception for a dairy cow based on the country and breed size.
    /// </summary>
    /// <param name="country"></param>
    /// <param name="size"></param>
    /// <returns></returns>
    /// <exception cref="CustomAppException"></exception>
    public static double RetrieveAgeAtFirstConception(Country country, DairyBreedSize size)
    {
        DairyCattleLiveWeightKey key = new((int)country, (int)size);

        if(Instance._DairyCattleLiveWeightInfo.ContainsKey(key))
        {
            return Instance._DairyCattleLiveWeightInfo[key].AgeAtFirstConception;
        }

        throw new CustomAppException("The key specified does not exist");
    }
    /// <summary>
    /// Retrieves the default age at first calving for a dairy cow based on the country and breed size.
    /// </summary>
    /// <param name="country"></param>
    /// <param name="size"></param>
    /// <returns></returns>
    /// <exception cref="CustomAppException"></exception>
    public static double RetrieveAgeAtFirstCalving(Country country, DairyBreedSize size)
    {
        DairyCattleLiveWeightKey key = new((int)country, (int)size);

        if(Instance._DairyCattleLiveWeightInfo.ContainsKey(key))
        {
            return Instance._DairyCattleLiveWeightInfo[key].AgeAtFirstCalving;
        }

        throw new CustomAppException("The key specified does not exist");
    }
    /// <summary>
    /// Retrieves the default age at death for a dairy cow based on the country and breed size.
    /// </summary>
    /// <param name="country"></param>
    /// <param name="size"></param>
    /// <returns></returns>
    /// <exception cref="CustomAppException"></exception>
    public static double RetrieveAgeAtDeath(Country country, DairyBreedSize size)
    {
        DairyCattleLiveWeightKey key = new((int)country, (int)size);

        if(Instance._DairyCattleLiveWeightInfo.ContainsKey(key))
        {
            return Instance._DairyCattleLiveWeightInfo[key].AgeAtDeath;
        }

        throw new CustomAppException("The key specified does not exist");
    }
    /// <summary>
    /// Retrieves the default mature weight for a dairy cow based on the country and breed size.
    /// </summary>
    /// <param name="country"></param>
    /// <param name="size"></param>
    /// <returns></returns>
    /// <exception cref="CustomAppException"></exception>
    public static double RetrieveMatureWeight(Country country, DairyBreedSize size)
    {
        DairyCattleLiveWeightKey key = new((int)country, (int)size);

        if(Instance._DairyCattleLiveWeightInfo.ContainsKey(key))
        {
            return Instance._DairyCattleLiveWeightInfo[key].MatureWeight;
        }

        throw new CustomAppException("The key specified does not exist");
    }

    private class DairyCattleLiveWeightKey
    {
        public int Country { get; set; }
        public int BreedSize { get; set; }

        public DairyCattleLiveWeightKey(int country, int breedSize)
        {
            Country = country;
            BreedSize = breedSize;
        }

        public DairyCattleLiveWeightKey() { }

        public class EqualityComparer : IEqualityComparer<DairyCattleLiveWeightKey>
        {
            public bool Equals(DairyCattleLiveWeightKey? x, DairyCattleLiveWeightKey? y)
            {
                if(x == null && y == null)
                {
                    return false;
                }

                return x.Country == y.Country && x.BreedSize == y.BreedSize;
            }

            public int GetHashCode(DairyCattleLiveWeightKey key)
            {
                return key.Country ^ key.BreedSize;
            }
        }
    }

    private class DairyCattleLiveWeightData
    {
        public double MatureWeight { get; set; }
        public double AgeAtFirstConception { get; set; }
        public double AgeAtFirstCalving { get; set; }
        public double AgeAtDeath { get; set; }

        public DairyCattleLiveWeightData(double matureWeight, double ageAtFirstConception,
            double ageAtFirstCalving, double ageAtDeath)
        {
            MatureWeight = matureWeight;
            AgeAtFirstConception = ageAtFirstConception;
            AgeAtFirstCalving = ageAtFirstCalving;
            AgeAtDeath = ageAtDeath;
        }

        public DairyCattleLiveWeightData() { }
    }
}
