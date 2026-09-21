using FrameAgricultureApi.CustomErrorHandling;
using FrameAgricultureApi.Libraries.Excreta.DTO;
using FrameAgricultureApi.Generic;
using FrameAgricultureApi.Libraries.LookUps;
using static FrameAgricultureApi.Enumerators.Enumerators;

namespace FrameAgricultureApi.Libraries.Excreta;

public static class ExcretaFunctions
{

    #region pigs, poultry and minor livestock
    /// <summary>
    /// Calculates Nitrogen, Volatile Solids and methand for free range poultry broken down into indoor and outdoor emissions.
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    public static ExcretaEmissionsFreeRangePoultry CalculateExcretaEmissionFreeRangePoultry(InitialNitrogenFreeRangePoultryUserInput input)
    {
        ExcretaEmissionsFreeRangePoultry output = new();

        var nitrogenIndoors = InitialNitrogenFreeRangePoultry(input.PoultryType, input.PercentageIndoors, true);
        output.IndoorsEmissions.Nitrogen = nitrogenIndoors;
        var initialVolatileSolidsIndoors = InitialVolatileSolidPoultry((int)input.PoultryType, input.PercentageIndoors, true);
        output.IndoorsEmissions.VolatileSolids = initialVolatileSolidsIndoors;

        var tanPercent = ExcretaLookup.GetTANPercent(Sector.Poultry, (int)input.PoultryType);
        output.IndoorsEmissions.TAN = ExcretaEquations.CalculateTAN(nitrogenIndoors, tanPercent);
        output.IndoorsEmissions.EntericMethane = 0.0;

        var nitrogenOutdoors = InitialNitrogenFreeRangePoultry(input.PoultryType, input.PercentageIndoors, false);
        output.OutdoorsEmissions.Nitrogen = nitrogenOutdoors;
        var initialVolatileSolidsOutdoors = InitialVolatileSolidPoultry((int)input.PoultryType, input.PercentageIndoors, false);
        output.OutdoorsEmissions.VolatileSolids = initialVolatileSolidsOutdoors;

        output.OutdoorsEmissions.TAN = ExcretaEquations.CalculateTAN(nitrogenOutdoors, tanPercent);
        output.OutdoorsEmissions.EntericMethane = ExcretaEquations.CH4EmissionOutdoorPoultry(input.PoultryType, output.OutdoorsEmissions.VolatileSolids);

        return output;
    }
    /// <summary>
    /// Calculates TAN, Nitrogen, Volatile Solids and enteric methane for pigs, poultry and minor livestock.
    /// </summary>
    /// <param name="sector"></param>
    /// <param name="animalType"></param>
    /// <returns></returns>
    /// <exception cref="CustomAppException"></exception>
    public static ExcretaEmissionsPigsPoultryMinorLivestock CalculateExcretaEmissionPigsPoultryMinorLivestock(Sector sector, int animalType)
    {
        ExcretaEmissionsPigsPoultryMinorLivestock output = new();
        var percentTAN = ExcretaLookup.GetTANPercent(sector, animalType);
        output.Nitrogen = ExcretaLookup.GetNitrogenExcretion(sector, animalType);
        output.VolatileSolids = ExcretaLookup.GetExcretaVolatileSolids(sector, animalType) * HelperFunctions.Days_per_year;
        output.TAN = ExcretaEquations.CalculateTAN(output.Nitrogen, percentTAN);
        output.EntericMethane = sector switch
        {
            Sector.Pigs or Sector.MinorLivestock => EntericMethaneLookup.RetrieveCH4(sector, animalType),
            Sector.Poultry => 0.0,
            _ => throw new CustomAppException("The Sector is invalid"),
        };
        return output;
    }
    /// <summary>
    /// Calculates poultry nitrogen for free range and not free range
    /// </summary>
    /// <param name="animalType"></param>
    /// <param name="percentageIndoors"></param>
    /// <param name="indoors"></param>
    /// <returns></returns>
    public static double InitialNitrogenFreeRangePoultry(PoultryType animalType, double percentageIndoors, bool indoors)
    {
        var freeRangeCoefficient = OutdoorExcretionLookup.RetrieveNitrogenCoefficientFreeRangePoultry(Sector.Poultry, (int)animalType, indoors);

        var baseNitrogen = ExcretaLookup.GetNitrogenExcretion(Sector.Poultry, (int)animalType);
        var freeRangeMultiplier = freeRangeCoefficient * baseNitrogen;

        if(indoors)
        {
            return ExcretaEquations.CalculatePoultryFreeRange(freeRangeMultiplier, percentageIndoors * HelperFunctions.Percent_to_Proportion);
        }

        return ExcretaEquations.CalculatePoultryFreeRange(freeRangeMultiplier, 1 - (percentageIndoors * HelperFunctions.Percent_to_Proportion));

    }
    /// <summary>
    /// Calculates volatile solids for poultry adjusted by time spent indoors or outdoors.
    /// </summary>
    /// <param name="animalType"></param>
    /// <param name="percentageIndoors"></param>
    /// <param name="indoors"></param>
    /// <returns></returns>
    public static double InitialVolatileSolidPoultry(int animalType, double percentageIndoors, bool indoors)
    {

        var totalVolatileSolids = ExcretaEquations.InitialVolatileSolid(Sector.Poultry, animalType);

        if(indoors)
        {
            return totalVolatileSolids * percentageIndoors * HelperFunctions.Percent_to_Proportion;
        }

        return totalVolatileSolids * (1 - (percentageIndoors * HelperFunctions.Percent_to_Proportion));

    }
    #endregion

    #region Beef and Dairy

    #region Dairy
    /// <summary>
    /// Calculates both dairy nitrogen and enteric methane. Using DN_9, DN_10, DN_11, DM_29.
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    public static NitrogenAndEntericMethaneOutputs DairyNitrogenExcretionAndEntericMethane(DairyNitrogenAndEntericMethaneInputs input)
    {
        NitrogenAndEntericMethaneOutputs outputs = new();

        var nitrogenOutputs = DairyNitrogen(input.NitrogenInputs, input.TimeScalar, input.CattleType);
        outputs.NitrogenOutputs = nitrogenOutputs;

        outputs.TotalVolatileSolidsExcretion = DairyVolatileSolids(input.VolatileSolidsInputs, input.TimeScalar);

        outputs.TotalGEIntake = input.VolatileSolidsInputs.DailyGEIntake * input.TimeScalar;
        outputs.TotalDMIIntake = input.VolatileSolidsInputs.DailyDMIIntake * input.TimeScalar;
        outputs.TotalMERequirement = input.VolatileSolidsInputs.DailyMERequirement * input.TimeScalar;
        //Todo should this be scaled by time?
        outputs.EntericMethane = ExcretaEquations.CalculateEntericMethaneDairy(input.CattleType, input.VolatileSolidsInputs.DailyDMIIntake);

        return outputs;
    }

    /// <summary>
    /// Calculates the dairy nitrogen using DN_9, DN_10, DN_11.
    /// </summary>
    /// <param name="nitrogenInputs"></param>
    /// <param name="timeScalar"></param>
    /// <param name="cattleType"></param>
    /// <returns></returns>
    public static NitrogenOutputs DairyNitrogen(NitrogenInputs nitrogenInputs, double timeScalar, DairyCattle cattleType)
    {
        var outputs = new NitrogenOutputs();

        var percentageNitrogenExcretionAsUrine = ExcretaEquations.PercentOfTotalNExcretionAsUrinaryN(cattleType, nitrogenInputs.DailyNIntake);

        var nitrogenExcretionAsUrineDaily = ExcretaEquations.NitrogenExcretionAsUrineDairy(nitrogenInputs.DailyNExcretion, percentageNitrogenExcretionAsUrine);

        var nitrogenExcretionAsFaecesDaily = ExcretaEquations.NitrogenExcretionAsFaeces(nitrogenInputs.DailyNExcretion, nitrogenExcretionAsUrineDaily);

        outputs.TotalNitrogenIntake = nitrogenInputs.DailyNIntake * timeScalar;
        outputs.TotalNitrogenExcretion = nitrogenInputs.DailyNExcretion * timeScalar;
        outputs.TotalNitrogenExcretionAsUrine = nitrogenExcretionAsUrineDaily * timeScalar;
        outputs.TotalNitrogenExcretionAsFaeces = nitrogenExcretionAsFaecesDaily * timeScalar;

        return outputs;
    }
    /// <summary>
    /// Scales the volatile solids from DN_12 with the given time scalar.
    /// </summary>
    /// <param name="volatileSolidsInputs"></param>
    /// <param name="timeScalar"></param>
    /// <returns></returns>
    public static double DairyVolatileSolids(VolatileSolidsInputs volatileSolidsInputs, double timeScalar)
    {
        var volatileSolidsExcretionDaily = ExcretaEquations.DailyVSExcretion(volatileSolidsInputs.DailyGEIntake,
            volatileSolidsInputs.DailyMERequirement, volatileSolidsInputs.DailyDMIIntake, ExcretaEquationsParameters.ManureAshContent);

        var totalVolatileSolidsExcretion = volatileSolidsExcretionDaily * timeScalar;
        return totalVolatileSolidsExcretion;
    }
    public static double DairyEntericMethane(DairyCattle cattleType, double dailyDMIIntake)
    {
        var entericMethane = ExcretaEquations.CalculateEntericMethaneDairy(cattleType, dailyDMIIntake);
        return entericMethane;
    }
    #endregion

    #region Beef
    /// <summary>
    /// Calculates the nitrogen excretion and enteric methane for beef cattle.
    /// </summary>
    /// <param name="input"></param>
    /// <param name="dailyDMIIntake"></param>
    /// <returns></returns>
    public static NitrogenAndEntericMethaneOutputs BeefNitrogenExcretionAndEntericMethane(BeefNitrogenAndEntericMethaneInputs input, double dailyDMIIntake)
    {
        NitrogenAndEntericMethaneOutputs outputs = new();
        NitrogenOutputs nitrogenOutputs = BeefNitrogen(input.NitrogenInputs, input.TimeScalar, input.CattleType);

        outputs.NitrogenOutputs = nitrogenOutputs;
        outputs.TotalVolatileSolidsExcretion = BeefVolatileSolids(input.VolatileSolidsInputs, input.TimeScalar);

        outputs.EntericMethane = BeefEntericMethane(input.CattleType, dailyDMIIntake);
        outputs.TotalGEIntake = input.VolatileSolidsInputs.DailyGEIntake * input.TimeScalar;
        outputs.TotalDMIIntake = input.VolatileSolidsInputs.DailyDMIIntake * input.TimeScalar;
        outputs.TotalMERequirement = input.VolatileSolidsInputs.DailyMERequirement * input.TimeScalar;

        return outputs;
    }
    /// <summary>
    /// BN_12 The equation calculates the volatile solids excretion (VSex) in kilograms per month (kg month-1). This equation is calculated separately depending on the amount of time spent at each stage.
    /// </summary>
    /// <param name="volatileSolidsInputs"></param>
    /// <param name="timeScalar"></param>
    /// <returns></returns>
    public static double BeefVolatileSolids(VolatileSolidsInputs volatileSolidsInputs, double timeScalar)
    {
        var monthlyDmiIntake = HelperFunctions.DailyToMonthlyConverter(volatileSolidsInputs.DailyDMIIntake);
        var volatileSolidsExcretionMonthly = ExcretaEquations.DailyVSExcretion(volatileSolidsInputs.DailyGEIntake,
            volatileSolidsInputs.DailyMERequirement, monthlyDmiIntake, ExcretaEquationsParameters.ManureAshContent);
        var volatileSolidsExcretionDaily = volatileSolidsExcretionMonthly * HelperFunctions.MonthsInYear / HelperFunctions.Days_per_year;

        var totalVolatileSolidsExcretion = volatileSolidsExcretionDaily * timeScalar;
        return totalVolatileSolidsExcretion;
    }

    /// <summary>
    /// Calculates beef nitrogen outputs using BN_10, BN_11
    /// </summary>
    /// <param name="nitrogenInputs"></param>
    /// <param name="timeScalar"></param>
    /// <param name="cattleType"></param>
    /// <returns></returns>
    public static NitrogenOutputs BeefNitrogen(NitrogenInputs nitrogenInputs, double timeScalar, BeefCattleType cattleType)
    {
        var outputs = new NitrogenOutputs();

        var nitrogenExcretionAsUrineMonthly = ExcretaEquations.ExcretedNAsUrine(nitrogenInputs.DailyNIntake, nitrogenInputs.DailyNExcretion);
        var nitrogenExcretionAsUrineDaily = nitrogenExcretionAsUrineMonthly * (HelperFunctions.MonthsInYear / HelperFunctions.Days_per_year);
        var nitrogenExcretionAsFaecesDaily = ExcretaEquations.ExcretedNAsFaeces(nitrogenExcretionAsUrineMonthly, nitrogenInputs.DailyNExcretion) * (HelperFunctions.MonthsInYear / HelperFunctions.Days_per_year);

        outputs.TotalNitrogenIntake = nitrogenInputs.DailyNIntake * timeScalar;
        outputs.TotalNitrogenExcretion = nitrogenInputs.DailyNExcretion * timeScalar;
        outputs.TotalNitrogenExcretionAsUrine = nitrogenExcretionAsUrineDaily * timeScalar;
        outputs.TotalNitrogenExcretionAsFaeces = nitrogenExcretionAsFaecesDaily * timeScalar;

        return outputs;
    }

    public static double BeefEntericMethane(BeefCattleType cattleType, double dailyDMIIntake)
    {
        var dailyEntericMethane = ExcretaEquations.CalculateEntericMethaneBeef(cattleType, dailyDMIIntake);
        return dailyEntericMethane;
    }

    #endregion

    #endregion

}
