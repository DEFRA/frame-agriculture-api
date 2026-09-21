using FrameAgricultureApi.Generic;
using FrameAgricultureApi.Libraries.LookUps;
using FrameAgricultureApi.Libraries.ManureManagement;
using FrameAgricultureApi.Libraries.OutdoorLivestock;
using static FrameAgricultureApi.Enumerators.Enumerators;

namespace FrameAgricultureApi.Components;

/// <summary>
/// Outdoor livestock component class
/// </summary>
public static class OutdoorLivestock
{
    /// <summary>
    /// Method to calcualte emissions from excreta deposited outdoors by cattle
    /// Note that there is no mitigation for these emissions
    /// </summary>
    /// <param name="sector">Sector enumerator (dairy or beef)</param>
    /// <param name="animalType">Animal type enumerator as integer</param>
    /// <param name="urine">nitrogen in urine deposited outdoors (kg per head)</param>
    /// <param name="dung">nitrogen in dung deposited outdoors (kg per head)</param>
    /// <param name="volatilesolids">volatilse solids in excreta deposited outdoors (kg per head)</param>
    /// <returns>An outdoor livestock emissions object</returns>
    public static OutdoorLivestockEmission OutdoorExcretaEmissions_Cattle(Sector sector, int animalType, double urine, double dung, double volatilesolids)
    {
        OutdoorLivestockEmission myemissions = new();
        myemissions.InitialiseGrazingEmissions();

        //Calculate and set additionalOutputs
        myemissions.AdditionalOutputs[AdditionalOutdoorExcretaOutputs.UrineNitrogen].Value = urine;
        myemissions.AdditionalOutputs[AdditionalOutdoorExcretaOutputs.DungNitrogen].Value = dung;
        myemissions.AdditionalOutputs[AdditionalOutdoorExcretaOutputs.ExcretaTotalN].Value = (urine + dung);
        myemissions.AdditionalOutputs[AdditionalOutdoorExcretaOutputs.ExcretaTAN].Value = urine;
        myemissions.AdditionalOutputs[AdditionalOutdoorExcretaOutputs.ExcretaOrganicN].Value = dung;

        //Calculate emissions
        //Ammonia
        myemissions.EmissionsCore[CoreEmissions.DirectNH3N].Value = OutdoorExcretalDeposition.NH3NEmission(sector, urine, dung);

        //Direct Nitrous Oxide
        myemissions.EmissionsCore[CoreEmissions.DirectN2ON].Value = OutdoorExcretalDeposition.N2ONEmission(sector, urine, dung);

        //Nitrate Leached
        myemissions.EmissionsCore[CoreEmissions.LeachedNO3N].Value = OutdoorExcretalDeposition.NO3NEmissionCattle(sector, urine, dung);

        //Indirect Nitrous Oxide from leached nitrate
        myemissions.EmissionsCore[CoreEmissions.N2ONleached].Value = GenericEquations.Emission_PercentageEF(myemissions.EmissionsCore[CoreEmissions.LeachedNO3N].Value, GenericLookup.IPCCEF5());

        //Nitric Oxide
        myemissions.EmissionsCore[CoreEmissions.DirectNON].Value = OutdoorExcretalDeposition.NOEmissions(sector, myemissions.EmissionsCore[CoreEmissions.DirectN2ON].Value);

        //Dinitrogen
        myemissions.EmissionsCore[CoreEmissions.DirectN2N].Value = OutdoorExcretalDeposition.N2Emissions(sector, myemissions.EmissionsCore[CoreEmissions.DirectN2ON].Value);

        //Indirect Nitrous Oxide from redeposition of volatilised nitrogen
        double source = myemissions.EmissionsCore[CoreEmissions.DirectNH3N].Value + myemissions.EmissionsCore[CoreEmissions.DirectNON].Value;
        myemissions.EmissionsCore[CoreEmissions.N2ONvolatalised].Value = GenericEquations.Emission_PercentageEF(source, GenericLookup.IPCCEF4());

        //Methane
        myemissions.MethaneEmissions[LivestockEmissions.OutdoorExcretionMethane].Value = OutdoorExcretalDeposition.CH4Emissions(sector, animalType, volatilesolids);

        return myemissions;
    }
    /// <summary>
    /// Method to calcualte emissions from excreta deposited outdoors by sheep
    /// Note that there is no mitigation for these emissions
    /// </summary>
    /// <param name="sheepEnergyBalance">Sheep Energy Balance object</param>
    /// <param name="sector">Sector enumerator</param>
    /// <param name="animalType">Sheep type enumerator as integer</param>
    /// <param name="fertiliserRate">total rate of N applied to grazing (kg N per hectare)</param>
    /// <param name="GrassFracLeachEquationCoefficients">The Grass Frac Leach Equation Coefficients object</param>
    /// <param name="GrazingNitrogen">The graxzing Nitrogen equation coefficients object</param>
    /// <param name="geneticGrazedFracLeachScalar">Grass Genetic Gain scalar for frac leached on grazed grass</param>
    /// <param name="geneticYieldDilutionScalar">Grass Genetic Gain scalar for Yield dilution (adjusts crude protein content)</param>
    /// <returns>An outdoor livestock emissions object</returns>
    public static OutdoorLivestockEmission OutdoorExcretaEmissions_Sheep(SheepEnergyBalance sheepEnergyBalance, Sector sector, int animalType, double fertiliserRate, GrassEquationCoefficients GrassFracLeachEquationCoefficients, GrassEquationCoefficients GrazingNitrogen, double geneticGrazedFracLeachScalar, double geneticYieldDilutionScalar)
    {
        OutdoorLivestockEmission myemissions = new();
        myemissions.InitialiseGrazingEmissions();

        //Get urine, dung and volatile solids

        double tmp_CrudeProtein = Math.Max(0.0, Math.Min(4.0, GrazingNitrogen.Coeff_X4 * Math.Pow(fertiliserRate, 4.0) +
                   GrazingNitrogen.Coeff_X3 * Math.Pow(fertiliserRate, 3.0) +
                   GrazingNitrogen.Coeff_X2 * Math.Pow(fertiliserRate, 2.0) +
                   GrazingNitrogen.Coeff_X * fertiliserRate +
                   GrazingNitrogen.C) * 6.25 * 10.0);           // Note conversion of percent nitrogen to g / kg crude protein;
        tmp_CrudeProtein *= geneticYieldDilutionScalar;

        double tmp_FieldTotalNitrogen = (sheepEnergyBalance.FieldUrineNitrogenCP100 + sheepEnergyBalance.FieldDungNitrogenCP100) + ((tmp_CrudeProtein - 100.0) / 100.0) *
                   (sheepEnergyBalance.FieldUrineNitrogenCP200 + sheepEnergyBalance.FieldDungNitrogenCP200 - sheepEnergyBalance.FieldUrineNitrogenCP100 - sheepEnergyBalance.FieldDungNitrogenCP100);
        sheepEnergyBalance.FieldNitrogenIntake = sheepEnergyBalance.FieldNitrogenIntakeCP100 + ((tmp_CrudeProtein - 100.0) / 100.0) * (sheepEnergyBalance.FieldNitrogenIntakeCP200 - sheepEnergyBalance.FieldNitrogenIntakeCP100);
        double tmp_Discard = Math.Pow(((sheepEnergyBalance.FieldUrineNitrogenCP100 / (sheepEnergyBalance.FieldUrineNitrogenCP100 + sheepEnergyBalance.FieldDungNitrogenCP100)) / 0.1484), 2.0) +
        ((tmp_CrudeProtein - 100.0) / 100.0) * (Math.Pow(((sheepEnergyBalance.FieldUrineNitrogenCP200 / (sheepEnergyBalance.FieldUrineNitrogenCP200 + sheepEnergyBalance.FieldDungNitrogenCP200)) / 0.1484), 2.0) -
            Math.Pow(((sheepEnergyBalance.FieldUrineNitrogenCP100 / (sheepEnergyBalance.FieldUrineNitrogenCP100 + sheepEnergyBalance.FieldDungNitrogenCP100)) / 0.1484), 2.0));

        double urine = (0.1484 * Math.Pow(tmp_Discard, 0.5)) * tmp_FieldTotalNitrogen;
        double dung = tmp_FieldTotalNitrogen - urine;

        double volatilesolids = sheepEnergyBalance.FieldVolatileSolids;

        //Calculate and set additionalOutputs
        myemissions.AdditionalOutputs[AdditionalOutdoorExcretaOutputs.UrineNitrogen].Value = urine;
        myemissions.AdditionalOutputs[AdditionalOutdoorExcretaOutputs.DungNitrogen].Value = dung;
        myemissions.AdditionalOutputs[AdditionalOutdoorExcretaOutputs.ExcretaTotalN].Value = (urine + dung);
        myemissions.AdditionalOutputs[AdditionalOutdoorExcretaOutputs.ExcretaTAN].Value = ManureManagementGenericEquations.SheepTAN(urine, dung);
        myemissions.AdditionalOutputs[AdditionalOutdoorExcretaOutputs.ExcretaOrganicN].Value = myemissions.AdditionalOutputs[AdditionalOutdoorExcretaOutputs.ExcretaTotalN].Value -
            myemissions.AdditionalOutputs[AdditionalOutdoorExcretaOutputs.ExcretaTAN].Value;

        //Calculate emissions
        //Ammonia
        myemissions.EmissionsCore[CoreEmissions.DirectNH3N].Value = OutdoorExcretalDeposition.NH3NEmission(Sector.Sheep, urine, dung);

        //Direct Nitrous Oxide
        myemissions.EmissionsCore[CoreEmissions.DirectN2ON].Value = OutdoorExcretalDeposition.N2ONEmission(Sector.Sheep, urine, dung);

        //Nitrate Leached
        myemissions.EmissionsCore[CoreEmissions.LeachedNO3N].Value = OutdoorExcretalDeposition.NO3NEmissionSheep(myemissions.AdditionalOutputs[AdditionalOutdoorExcretaOutputs.ExcretaTotalN].Value, fertiliserRate, GrassFracLeachEquationCoefficients, geneticGrazedFracLeachScalar);

        //Indirect Nitrous Oxide from leached nitrate
        myemissions.EmissionsCore[CoreEmissions.N2ONleached].Value = GenericEquations.Emission_PercentageEF(myemissions.EmissionsCore[CoreEmissions.LeachedNO3N].Value, GenericLookup.IPCCEF5());

        //Nitric Oxide
        myemissions.EmissionsCore[CoreEmissions.DirectNON].Value = OutdoorExcretalDeposition.NOEmissions(Sector.Sheep, myemissions.EmissionsCore[CoreEmissions.DirectN2ON].Value);

        //Dinitrogen
        myemissions.EmissionsCore[CoreEmissions.DirectN2N].Value = OutdoorExcretalDeposition.N2Emissions(Sector.Sheep, myemissions.EmissionsCore[CoreEmissions.DirectN2ON].Value);

        //Indirect Nitrous Oxide from redeposition of volatilised nitrogen
        double source = myemissions.EmissionsCore[CoreEmissions.DirectNH3N].Value + myemissions.EmissionsCore[CoreEmissions.DirectNON].Value;
        myemissions.EmissionsCore[CoreEmissions.N2ONvolatalised].Value = GenericEquations.Emission_PercentageEF(source, GenericLookup.IPCCEF4());

        //Methane
        myemissions.MethaneEmissions[LivestockEmissions.OutdoorExcretionMethane].Value = OutdoorExcretalDeposition.CH4Emissions(sector, animalType, volatilesolids);

        return myemissions;
    }
    /// <summary>
    /// Method to calcualte emissions from excreta deposited outdoors by livestock other than sheep or cattle
    /// Note that there is no mitigation for these emissions
    /// </summary>
    /// <param name="sector">Sector enumerator</param>
    /// <param name="animalType">Animal type enumerator as integer</param>
    /// <param name="availableNitrogen">Total available nitrogen in excreta deposited outdoors as Kg per head</param>
    /// <param name="totalNitrogen">Total nitrogen in excreta deposited outdoors as kg per head</param>
    /// <param name="volatilesolids">volatilse solids in excreta deposited outdoors (kg per head)</param>
    /// <returns>An outdoor livestock emissions object</returns>
    public static OutdoorLivestockEmission OutdoorExcretaEmissions_PigsPoultryMinorLivestock(Sector sector, int animalType, double availableNitrogen, double totalNitrogen, double volatilesolids)
    {
        OutdoorLivestockEmission myemissions = new();
        myemissions.InitialiseGrazingEmissions();

        //Calculate and set additionalOutputs

        myemissions.AdditionalOutputs[AdditionalOutdoorExcretaOutputs.ExcretaTotalN].Value = totalNitrogen;
        myemissions.AdditionalOutputs[AdditionalOutdoorExcretaOutputs.ExcretaTAN].Value = availableNitrogen;
        myemissions.AdditionalOutputs[AdditionalOutdoorExcretaOutputs.ExcretaOrganicN].Value = totalNitrogen - availableNitrogen;

        //Calculate emissions
        //Ammonia
        myemissions.EmissionsCore[CoreEmissions.DirectNH3N].Value = OutdoorExcretalDeposition.NH3NEmission(sector, availableNitrogen);

        //Direct Nitrous Oxide
        myemissions.EmissionsCore[CoreEmissions.DirectN2ON].Value = OutdoorExcretalDeposition.N2ONEmission(sector, totalNitrogen);

        //Nitrate Leached
        myemissions.EmissionsCore[CoreEmissions.LeachedNO3N].Value = OutdoorExcretalDeposition.NO3NEmissionGeneric(sector, totalNitrogen);

        //Indirect Nitrous Oxide from leached nitrate
        myemissions.EmissionsCore[CoreEmissions.N2ONleached].Value = GenericEquations.Emission_PercentageEF(myemissions.EmissionsCore[CoreEmissions.LeachedNO3N].Value, GenericLookup.IPCCEF5());

        //Nitric Oxide
        myemissions.EmissionsCore[CoreEmissions.DirectNON].Value = OutdoorExcretalDeposition.NOEmissions(sector, myemissions.EmissionsCore[CoreEmissions.DirectN2ON].Value);

        //Dinitrogen
        myemissions.EmissionsCore[CoreEmissions.DirectN2N].Value = OutdoorExcretalDeposition.N2Emissions(sector, myemissions.EmissionsCore[CoreEmissions.DirectN2ON].Value);

        //Indirect Nitrous Oxide from redeposition of volatilised nitrogen
        double source = myemissions.EmissionsCore[CoreEmissions.DirectNH3N].Value + myemissions.EmissionsCore[CoreEmissions.DirectNON].Value;
        myemissions.EmissionsCore[CoreEmissions.N2ONvolatalised].Value = GenericEquations.Emission_PercentageEF(source, GenericLookup.IPCCEF4());

        //Methane
        myemissions.MethaneEmissions[LivestockEmissions.OutdoorExcretionMethane].Value = OutdoorExcretalDeposition.CH4Emissions(sector, animalType, volatilesolids);

        return myemissions;
    }
}
