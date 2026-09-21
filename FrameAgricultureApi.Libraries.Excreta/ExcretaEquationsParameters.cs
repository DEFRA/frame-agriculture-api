namespace FrameAgricultureApi.Libraries.Excreta;

public class ExcretaEquationsParameters
{
    public const double MilkProteinToNRatio = 6.38;
    public const double NetProteinContentOfLiveWeightGainInLactatingRuminantsCoefficient = 138;
    public const double NetProteinContentOfLiveWeightGain = 138;
    public const double ManureAshContent = 0.08;
    public const double ProteinToNRatio = 6.25;
    public const double WeightedAverageDryMatterContent = 167;
    public const double CH4m3ToKg = 0.67;
    // Todo this should be a look up table
    public static Dictionary<CorrectionParameterKey, double> CorrectionFactorForNitrogenRetention = new Dictionary<CorrectionParameterKey, double>(new CorrectionParameterKey.EqualityComparer())
    {
        { new CorrectionParameterKey(1, 4), 1},
        { new CorrectionParameterKey(2, 4), 1},
        { new CorrectionParameterKey(3, 4), 1.2},
        { new CorrectionParameterKey(4, 4), 1.2},
        { new CorrectionParameterKey(5, 4), 1.1},
        { new CorrectionParameterKey(6, 4), 1},

        { new CorrectionParameterKey(1, 5), 1},
        { new CorrectionParameterKey(2, 5), 1},
        { new CorrectionParameterKey(3, 5), 1.2},
        { new CorrectionParameterKey(4, 5), 1.2},
        { new CorrectionParameterKey(5, 5), 1.1},
        { new CorrectionParameterKey(6, 5), 1},

        { new CorrectionParameterKey(1, 6), 0.9},
        { new CorrectionParameterKey(2, 6), 0.9},
        { new CorrectionParameterKey(3, 6), 1.1},
        { new CorrectionParameterKey(4, 6), 1.1},
        { new CorrectionParameterKey(5, 6), 1},
        { new CorrectionParameterKey(6, 6), 0.9},

        { new CorrectionParameterKey(1, 7), 0.9},
        { new CorrectionParameterKey(2, 7), 0.9},
        { new CorrectionParameterKey(3, 7), 1.1},
        { new CorrectionParameterKey(4, 7), 1.1},
        { new CorrectionParameterKey(5, 7), 1},
        { new CorrectionParameterKey(6, 7), 0.9},

    };

    public static Dictionary<CorrectionParameterKey, double> CorrectionFactorForEnergyContentLiveWeightGain = new Dictionary<CorrectionParameterKey, double>(new CorrectionParameterKey.EqualityComparer())
    {
        { new CorrectionParameterKey(1, 4), 1},
        { new CorrectionParameterKey(2, 4), 1},
        { new CorrectionParameterKey(3, 4), 0.7},
        { new CorrectionParameterKey(4, 4), 0.7},
        { new CorrectionParameterKey(5, 4), 0.85},
        { new CorrectionParameterKey(6, 4), 1},

        { new CorrectionParameterKey(1, 5), 1},
        { new CorrectionParameterKey(2, 5), 1},
        { new CorrectionParameterKey(3, 5), 0.7},
        { new CorrectionParameterKey(4, 5), 0.7},
        { new CorrectionParameterKey(5, 5), 0.85},
        { new CorrectionParameterKey(6, 5), 1},

        { new CorrectionParameterKey(1, 6), 1.15},
        { new CorrectionParameterKey(2, 6), 1.15},
        { new CorrectionParameterKey(3, 6), 0.85},
        { new CorrectionParameterKey(4, 6), 0.85},
        { new CorrectionParameterKey(5, 6), 1},
        { new CorrectionParameterKey(6, 6), 1.15},

        { new CorrectionParameterKey(1, 7), 0.85},
        { new CorrectionParameterKey(2, 7), 0.85},
        { new CorrectionParameterKey(3, 7), 1},
        { new CorrectionParameterKey(4, 7), 1},
        { new CorrectionParameterKey(5, 7), 1},
        { new CorrectionParameterKey(6, 7), 1},

    };

    public static Dictionary<int, double> AverageGestationPeriod = new Dictionary<int, double>()
    {
        {1, 280},
        {2, 280},
        {3, 280},
        {4, 289},
        {5, 283},
        {6, 283}
    };

    public static Dictionary<int, double> EnergyRetentionInFullTermGravidUterus = new Dictionary<int, double>()
    {
        {4, 338},
        {5, 300},
        {6, 300},
        {7, 0},
    };

    public class CorrectionParameterKey
    {
        public int BeefCattleRole { get; set; }
        public int BeefCattleBreed { get; set; }

        public CorrectionParameterKey(int beefCattleRole, int beefCattleBreed)
        {
            BeefCattleBreed = beefCattleBreed;
            BeefCattleRole = beefCattleRole;
        }

        public CorrectionParameterKey() { }
        public class EqualityComparer : IEqualityComparer<CorrectionParameterKey>
        {
            public bool Equals(CorrectionParameterKey x, CorrectionParameterKey y)
            {
                return x.BeefCattleBreed == y.BeefCattleBreed && x.BeefCattleRole == y.BeefCattleRole;
            }

            public int GetHashCode(CorrectionParameterKey key)
            {
                return key.BeefCattleBreed ^ key.BeefCattleRole;
            }
        }
    }
}

