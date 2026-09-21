using static FrameAgricultureApi.Enumerators.Enumerators;

namespace FrameAgricultureApi.Libraries.Enteric;

public static class EntericEquationsParameters
{
    public static readonly Dictionary<int, double> AgeLowerBoundariesBeefCattle = new Dictionary<int, double>()
    {
        {0, 0},
        {1, 13.04},
        {2, 26.07},
        {3, 39.11},
        {4, 52.14},
        {5, 65.18},
        {6, 78.21},
        {7, 91.25},
        {8, 104.29},
        {9, 117.32},
        {10, 130.36},
        {11, 143.39},
        {12, 156.43},
        {13, 208.57},
        {14, 260.71},
        {15, 1042.86},
        {16, 1303.57},
    };

    public const double GenderCorrectionParameter = 1.0;
    public const double ActivityCoefficient = 0.0071;
    public const double CorrectionFactorEnergyContentLiveWeightGain = 1.3;
    public const double NetEnergyValueOfWeightGain = 19.3;
    public const double TotalEnergyRetentionGravidFoetus = 251.0;
    public const double UEMEGrowthConcepta = 0.133;
    public const double AverageGestationPeriod = 280.0;
    public const double EUMEDairyCowsWeightGain = 0.65;
    public const double DairyLiveWeightCurveParameterK = 57.96;
    public const double DairyLiveWeightCurveParameterC = 1.5215;
    public const double ErActivityEnergy = 0.01376;
    public const double DefaultBeefMilkFat = 36;
    public const double DefaultBeefMilkProtein = 32;
    public const double PercentBeefCowsLactating = 91;
    public static Dictionary<int, double> AgeIndexDictionary = new Dictionary<int, double>()
    {
        { 0, 0 },
        { 1, 13.04 },
        { 2, 26.07 },
        { 3, 39.11 },
        { 4, 52.14 },
        { 5, 65.18 },
        { 6, 78.21 },
        { 7, 91.25 },
        { 8, 104.29 },
        { 9, 117.32 },
        { 10, 130.36 },
        { 11, 143.39 },
        { 12, 156.43 },
        { 13, 208.57 },
        { 14, 260.71 },
        { 15, 1042.86 },
        { 16, 1303.57 },
    };

    public static Dictionary<BeefCattleBreed, double> BeefLactationScalarDictionary = new Dictionary<BeefCattleBreed, double>()
    {
        { BeefCattleBreed.Continental, 1.05 },
        { BeefCattleBreed.Lowland, 1.0 },
        { BeefCattleBreed.Upland, 0.95 },
    };

    public static Dictionary<int, double> PercentBeefCowsGestating = new Dictionary<int, double>()
    {
        { 0, 0},
        { 1, 0},
        { 2, 0},
        { 3, 0},
        { 4, 0},
        { 5, 12.1},
        { 6, 32.3},
        { 7, 50},
        { 8, 62},
        { 9, 85},
        { 10, 90.1},
        { 11, 90.2},
        { 12, 93},
        { 13, 93},
        { 14, 93},
        { 15, 93}
    };

    public static Dictionary<int, double> PercentBeefHeifersGestating = new Dictionary<int, double>()
    {
        { 0, 0},
        { 1, 0},
        { 2, 0},
        { 3, 0.01},
        { 4, 0.37},
        { 5, 9.7},
        { 6, 25.88},
        { 7, 39.18},
        { 8, 46.32},
        { 9, 53.82},
        { 10, 46.26},
        { 11, 32.21},
        { 12, 8.94},
        { 13, 0},
        { 14, 0},
        { 15, 0}
    };

    public static Dictionary<int, double> PercentBeefHeifersLactating = new Dictionary<int, double>()
    {
        { 0, 0},
        { 1, 0},
        { 2, 0},
        { 3, 0},
        { 4, 0},
        { 5, 0},
        { 6, 0.43},
        { 7, 1.68},
        { 8, 6.68},
        { 9, 11.7},
        { 10, 14.16},
        { 11, 16.9},
        { 12, 10.66},
        { 13, 0},
        { 14, 0},
        { 15, 0}
    };

}
