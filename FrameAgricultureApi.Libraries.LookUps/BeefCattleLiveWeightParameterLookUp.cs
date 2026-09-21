using FrameAgricultureApi.CustomErrorHandling;
using FrameAgricultureApi.Generic;
using System.Reflection;
using static FrameAgricultureApi.Enumerators.Enumerators;

namespace FrameAgricultureApi.Libraries.LookUps;

/// <summary>
/// Class that gives access to look up values for beef cattle.
/// </summary>
public class BeefCattleLiveWeightParameterLookUp
{
    private static BeefCattleLiveWeightParameterLookUp? _instance;
    /// <summary>
    /// Reference to the public singleton instance of the BeefCattleLiveWeightParameterLookUp class
    /// </summary>
    public static BeefCattleLiveWeightParameterLookUp Instance
    {
        get
        {
            if(_instance == null)
            {
                _instance = new BeefCattleLiveWeightParameterLookUp();
            }
            return _instance;
        }
    }

    private Dictionary<BeefCattleLiveWeightParameterKey, BeefCattleLiveWeightParameterData> _BeefCattleLiveWeightParametersInfo = new Dictionary<BeefCattleLiveWeightParameterKey, BeefCattleLiveWeightParameterData>(new BeefCattleLiveWeightParameterKey.EqualityComparer());

    private BeefCattleLiveWeightParameterLookUp()
    {
        _BeefCattleLiveWeightParametersInfo.Clear();

        MemoryStream memoryStream = HelperFunctions.GetLUT(Assembly.GetExecutingAssembly(), "FLEA_BeefCattleLiveWeightParameters_LUT.dat");
        try
        {
            using(BinaryReader binaryReader = new BinaryReader(memoryStream))
            {
                while(binaryReader.BaseStream.Position < binaryReader.BaseStream.Length)
                {
                    int beefCattleType = binaryReader.ReadInt32();
                    int beefCattleBreed = binaryReader.ReadInt32();

                    BeefCattleLiveWeightParameterKey theKey = new()
                    {
                        BeefCattleType = beefCattleType,
                        BeefCattleBreed = beefCattleBreed
                    };

                    BeefCattleLiveWeightParameterData data = new()
                    {
                        LiveWeightCurveParameterC = binaryReader.ReadDouble(),
                        LiveWeightCurveParameterK = binaryReader.ReadDouble(),
                    };

                    _BeefCattleLiveWeightParametersInfo.Add(theKey, data);
                }
            }
            memoryStream.Close();
        }
        catch(Exception ex)
        {
            throw new CustomAppException("Error reading Beef Cattle liveweight parameter lookup: " + ex.Message);
        }
        finally
        {
            memoryStream.Close();
        }
    }
    /// <summary>
    /// Retrieves the k parameter for beef cattle used in the BW_1 equation
    /// </summary>
    /// <param name="type"></param>
    /// <param name="breed"></param>
    /// <returns></returns>
    public static double RetrieveLiveWeightCurveParameterK(BeefCattleType type, BeefCattleBreed breed)
    {
        BeefCattleLiveWeightParameterKey key = new((int)type, (int)breed);
        return Instance._BeefCattleLiveWeightParametersInfo[key].LiveWeightCurveParameterK;
    }
    /// <summary>
    /// Rerieves the c parameter for beef cattle used in the BW_1 equation
    /// </summary>
    /// <param name="type"></param>
    /// <param name="breed"></param>
    /// <returns></returns>
    public static double RetrieveLiveWeightCurveParameterC(BeefCattleType type, BeefCattleBreed breed)
    {
        BeefCattleLiveWeightParameterKey key = new((int)type, (int)breed);
        return Instance._BeefCattleLiveWeightParametersInfo[key].LiveWeightCurveParameterC;
    }
    /// <summary>
    /// Gets the default mature weight for different Beef Cattle types and breeds.
    /// </summary>
    /// <param name="type"></param>
    /// <param name="breed"></param>
    /// <returns></returns>
    public static double RetrieveMatureWeight(BeefCattleType type, BeefCattleBreed breed)
    {
        BeefCattleLiveWeightParameterKey key = new((int)type, (int)breed);
        return Instance._BeefCattleLiveWeightParametersInfo[key].MatureWeight;
    }
    /// <summary>
    /// Gets the default birth weight for different Beef Cattle types and breeds.
    /// </summary>
    /// <param name="type"></param>
    /// <param name="breed"></param>
    /// <returns></returns>
    public static double RetrieveBirthWeight(BeefCattleType type, BeefCattleBreed breed)
    {
        BeefCattleLiveWeightParameterKey key = new((int)type, (int)breed);
        return Instance._BeefCattleLiveWeightParametersInfo[key].BirthWeight;
    }

    private class BeefCattleLiveWeightParameterKey
    {
        public int BeefCattleType { get; set; }
        public int BeefCattleBreed { get; set; }

        public BeefCattleLiveWeightParameterKey(int beefCattleType, int beefCattleBreed)
        {
            BeefCattleType = beefCattleType;
            BeefCattleBreed = beefCattleBreed;
        }

        public BeefCattleLiveWeightParameterKey() { }

        public class EqualityComparer : IEqualityComparer<BeefCattleLiveWeightParameterKey>
        {
            public bool Equals(BeefCattleLiveWeightParameterKey x, BeefCattleLiveWeightParameterKey y)
            {
                return x.BeefCattleType == y.BeefCattleType && x.BeefCattleBreed == y.BeefCattleBreed;
            }

            public int GetHashCode(BeefCattleLiveWeightParameterKey key)
            {
                return key.BeefCattleType ^ key.BeefCattleBreed;
            }
        }
    }

    private class BeefCattleLiveWeightParameterData
    {
        public double LiveWeightCurveParameterC { get; set; }
        public double LiveWeightCurveParameterK { get; set; }
        public double MatureWeight { get; set; }
        public double BirthWeight { get; set; }

        public BeefCattleLiveWeightParameterData(double liveWeightCurveParameterC, double liveWeightCurveParameterK,
            double matureWeight, double birthWeight)
        {
            LiveWeightCurveParameterC = liveWeightCurveParameterC;
            LiveWeightCurveParameterK = liveWeightCurveParameterK;
            MatureWeight = matureWeight;
            BirthWeight = birthWeight;
        }

        public BeefCattleLiveWeightParameterData() { }
    }
}
