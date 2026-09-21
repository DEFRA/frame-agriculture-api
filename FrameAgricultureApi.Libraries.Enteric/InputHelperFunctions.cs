using FrameAgricultureApi.CustomErrorHandling;
using FrameAgricultureApi.Libraries.Enteric.DefaultInputs;
using FrameAgricultureApi.Libraries.Enteric.DefaultInputs.UserInputs;
using FrameAgricultureApi.Libraries.Enteric.DTO;
using FrameAgricultureApi.Libraries.LookUps;
using static FrameAgricultureApi.Enumerators.Enumerators;

namespace FrameAgricultureApi.Libraries.Enteric;

public class InputHelperFunctions
{
    #region LiveWeight
    public static DefaultBeefLiveweightInputs GetDefaultBeefLiveWeightInputs(BeefCattleBreed cattleBreed, BeefCattleType cattleType)
    {
        DefaultBeefLiveweightInputs defaultBeefLiveweightInputs = new()
        {
            MatureWeight = BeefCattleLiveWeightParameterLookUp.RetrieveMatureWeight(cattleType, cattleBreed),
            BirthWeight = BeefCattleLiveWeightParameterLookUp.RetrieveBirthWeight(cattleType, cattleBreed)
        };

        return defaultBeefLiveweightInputs;
    }
    /// <summary>
    /// Retrieve default liveweight inputs for dairy cattle based on country and breed size.
    /// </summary>
    /// <param name="country"></param>
    /// <param name="dairyCattleSize"></param>
    /// <returns></returns>
    /// <exception cref="CustomAppException"></exception>
    public static DefaultDairyLiveweightInputs GetDefaultDairyLiveWeightInputs(Country country, DairyBreedSize dairyCattleSize)
    {
        DefaultDairyLiveweightInputs defaultDairyLiveweightInputs = new();

        if(dairyCattleSize == DairyBreedSize.NotSet)
        {
            throw new CustomAppException();
        }

        defaultDairyLiveweightInputs.MatureWeight = DairyCattleLiveWeightLookUp.RetrieveMatureWeight(country, dairyCattleSize);
        defaultDairyLiveweightInputs.AgeAtFirstConception = DairyCattleLiveWeightLookUp.RetrieveAgeAtFirstConception(country, dairyCattleSize);
        defaultDairyLiveweightInputs.AgeAtFirstCalving = DairyCattleLiveWeightLookUp.RetrieveAgeAtFirstCalving(country, dairyCattleSize);
        defaultDairyLiveweightInputs.AgeAtDeath = DairyCattleLiveWeightLookUp.RetrieveAgeAtDeath(country, dairyCattleSize);

        return defaultDairyLiveweightInputs;
    }

    #endregion

    #region Diet
    /// <summary>
    /// Retrieve default dairy concentrate diet inputs based on country, cattle type, and breed size.
    /// </summary>
    /// <param name="key"></param>
    /// <returns></returns>
    /// <exception cref="CustomAppException"></exception>
    public static DefaultDairyConcentrateInputs GetDefaultDairyConcentrateDietInputs(DairyConcentrateKey key)
    {
        DefaultDairyConcentrateInputs defaultDairyConcentrateInputs = new();

        if(key.BreedSize == DairyBreedSize.NotSet)
            throw new CustomAppException();

        defaultDairyConcentrateInputs.Intake = DairyDietLookup.RetrieveConcentrateUse(key.Country, key.CattleType, key.BreedSize);
        defaultDairyConcentrateInputs.ME = DairyDietLookup.RetrieveConcentrateME(key.Country, key.CattleType, key.BreedSize);
        defaultDairyConcentrateInputs.GE = DairyDietLookup.RetrieveConcentrateGE(key.Country, key.CattleType, key.BreedSize);
        defaultDairyConcentrateInputs.CP = DairyDietLookup.RetrieveConcentrateCP(key.Country, key.CattleType, key.BreedSize);
        defaultDairyConcentrateInputs.DMC = DairyDietLookup.RetrieveConcentrateDMC(key.Country, key.CattleType, key.BreedSize);
        defaultDairyConcentrateInputs.DMD = DairyDietLookup.RetrieveConcentrateDMD(key.Country, key.CattleType, key.BreedSize);

        return defaultDairyConcentrateInputs;
    }
    /// <summary>
    /// Retrieve default dairy forage diet inputs based on country, cattle type, management regime, and month.
    /// </summary>
    /// <param name="key"></param>
    /// <returns></returns>
    public static DefaultForageInputs GetDefaultDairyForageDietInputs(DairyForageKey key)
    {
        DefaultForageInputs defaultForageInputs = new()
        {
            ME = DairyDietLookup.RetrieveForageME(key.Country, key.CattleType, key.ManagementRegime, key.Month),
            GE = DairyDietLookup.RetrieveForageGE(key.Country, key.CattleType, key.ManagementRegime, key.Month),
            CP = DairyDietLookup.RetrieveForageCP(key.Country, key.CattleType, key.ManagementRegime, key.Month),
            DMC = DairyDietLookup.RetrieveForageDMC(key.Country, key.CattleType, key.ManagementRegime, key.Month),
            DMD = DairyDietLookup.RetrieveForageDMD(key.Country, key.CattleType, key.ManagementRegime, key.Month)
        };

        return defaultForageInputs;
    }
    /// <summary>
    /// Retrieve default dairy milk inputs based on country, cattle type, and breed size.
    /// </summary>
    /// <param name="key"></param>
    /// <returns></returns>
    public static DefaultDairyMilkInput GetDefaultDairyMilkInputs(DairyMilkKey key)
    {
        DefaultDairyMilkInput defaultDairyMilkInput = new()
        {
            MilkYield = DairyDietLookup.RetrieveMilkYield(key.Country, key.CattleType, key.BreedSize),
            MilkFat = DairyDietLookup.RetrieveMilkFat(key.Country, key.CattleType, key.BreedSize),
            MilkProtein = DairyDietLookup.RetrieveMilkProtein(key.Country, key.CattleType, key.BreedSize)
        };

        return defaultDairyMilkInput;
    }

    #endregion

    #region EnergyBalance
    /// <summary>
    /// Retrieve default dairy energy balance inputs based on dairy cattle breed size.
    /// </summary>
    /// <param name="cattleSize"></param>
    /// <returns></returns>
    /// <exception cref="CustomAppException"></exception>
    public static DefaultDairyEnergyBalanceInputs GetDefaultDairyEnergyBalanceInput(DairyBreedSize cattleSize)
    {

        DefaultDairyEnergyBalanceInputs dairyEnergyBalanceInputs = new()
        {
            HerdPregnancyProportion = 0.9,
            CalvingInterval = cattleSize switch
            {
                DairyBreedSize.Small => 414,
                DairyBreedSize.Medium => 402,
                DairyBreedSize.Large => 398,
                _ => throw new CustomAppException("The value for Dairy cattle size is invalid."),
            }
        };
        return dairyEnergyBalanceInputs;
    }

    #endregion
    /// <summary>
    /// Retrieve default dairy cattle values for calving interval, gestation period, mature weight, age at conception, age at calving, and age at death based on country and breed size.
    /// </summary>
    /// <param name="dairyKey"></param>
    /// <returns></returns>
    public static DairyKeyPerformanceIndicators DairyKeyPerformanceIndicators(DairyLiveweightKey dairyKey)
    {
        var liveweightDefaults = GetDefaultDairyLiveWeightInputs(dairyKey.Country, dairyKey.DairyBreedSize);
        var energyBalanceDefaults = GetDefaultDairyEnergyBalanceInput(dairyKey.DairyBreedSize);
        var gestationPeriod = EntericEquationsParameters.AverageGestationPeriod;

        var outputs = new DairyKeyPerformanceIndicators
        {
            CalvingInterval = energyBalanceDefaults.CalvingInterval,
            GestationPeriod = gestationPeriod,
            MatureWeight = liveweightDefaults.MatureWeight,
            AgeAtConception = liveweightDefaults.AgeAtFirstConception,
            AgeAtCalving = liveweightDefaults.AgeAtFirstCalving,
            AgeAtDeath = liveweightDefaults.AgeAtDeath
        };

        return outputs;

    }
    /// <summary>
    /// Retrieve default beef cattle values for mature weight, percent heifers lactating, percent heifers gestating, percent cows lactating, percent cows gestating, milk fat, milk protein, lactation scalar, and milk yield based on cattle type, breed, and age.
    /// </summary>
    /// <param name="key"></param>
    /// <returns></returns>
    public static BeefKeyPerformanceIndicators BeefKeyPerformanceIndicators(BeefDefaultKey key)
    {
        BeefKeyPerformanceIndicators outputs = new()
        {
            MatureWeight = BeefCattleLiveWeightParameterLookUp.RetrieveMatureWeight(key.CattleType, key.CattleBreed),
            PercentHeifersLactating = EntericEquationsParameters.PercentBeefHeifersLactating[key.CattleAge],
            PercentHeifersGestating = EntericEquationsParameters.PercentBeefHeifersGestating[key.CattleAge],
            PercentCowsLactating = EntericEquationsParameters.PercentBeefCowsLactating,
            PercentCowsGestating = EntericEquationsParameters.PercentBeefCowsGestating[key.CattleAge],
            MilkFat = EntericEquationsParameters.DefaultBeefMilkFat,
            MilkProtein = EntericEquationsParameters.DefaultBeefMilkProtein,
            LactationScalar = EntericEquationsParameters.BeefLactationScalarDictionary[key.CattleBreed]
        };

        var lactationLength = 182.5;

        outputs.MilkYield = EntericEquations.MilkYield(lactationLength, outputs.LactationScalar);

        return outputs;
    }
}
