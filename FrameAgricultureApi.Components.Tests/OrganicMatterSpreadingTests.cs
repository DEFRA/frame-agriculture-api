using FrameAgricultureApi.Libraries.LookUps;
using FrameAgricultureApi.Libraries.OrganicMatterApplication;
using System.Text.Json;
using System.Text.Json.Serialization;
using static FrameAgricultureApi.Enumerators.Enumerators;

namespace FrameAgricultureApi.Components.Tests;

public class OrganicMatterSpreadingTests
{
    public static IEnumerable<object[]> UnmitigatedOrganicMatterSpreadingEmissions_TestGenerator()
    {
        var dataList = new List<object[]>();

        OrganicMatterSpreadingEmission emissions = new();
        emissions.InitialiseOrganicMatterSpreadingEmissions();

        emissions.EmissionsCore[CoreEmissions.DirectNH3N].Value = 5.92 * 1000;
        emissions.EmissionsCore[CoreEmissions.DirectN2ON].Value = 0.37 * 1000;
        emissions.EmissionsCore[CoreEmissions.DirectNON].Value = 0.1480 * 1000;
        emissions.EmissionsCore[CoreEmissions.LeachedNO3N].Value = 11.1 * 1000;
        emissions.EmissionsCore[CoreEmissions.N2ONleached].Value = 0.08325 * 1000;
        emissions.EmissionsCore[CoreEmissions.N2ONvolatalised].Value = 0.084952 * 1000;
        emissions.EmissionsCore[CoreEmissions.DirectN2N].Value = 1.11 * 1000;

        emissions.AdditionalOutputs[AdditionalOrganicMatterSpreadingOutputs.NTotalNApplied].Value = 37 * 1000;
        emissions.AdditionalOutputs[AdditionalOrganicMatterSpreadingOutputs.TANApplied].Value = 18.5 * 1000;
        emissions.AdditionalOutputs[AdditionalOrganicMatterSpreadingOutputs.OrganicNApplied].Value = 18.5 * 1000;

        emissions.RoundEmissions(4);

        dataList.Add(
            new object[] { OrganicMatterType.SewageSludgeLiquid, 1000.00, SpreadingLandUse.Arable, Month.January, 37.00, 18.5 / 37 * 100,
                emissions });

        emissions = new OrganicMatterSpreadingEmission();
        emissions.InitialiseOrganicMatterSpreadingEmissions();

        emissions.EmissionsCore[CoreEmissions.DirectNH3N].Value = 5.92 * 1000;
        emissions.EmissionsCore[CoreEmissions.DirectN2ON].Value = 0.37 * 1000;
        emissions.EmissionsCore[CoreEmissions.DirectNON].Value = 0.222 * 1000;
        emissions.EmissionsCore[CoreEmissions.LeachedNO3N].Value = 3.7 * 1000;
        emissions.EmissionsCore[CoreEmissions.N2ONleached].Value = 0.02775 * 1000;
        emissions.EmissionsCore[CoreEmissions.N2ONvolatalised].Value = 0.085988 * 1000;
        emissions.EmissionsCore[CoreEmissions.DirectN2N].Value = 1.11 * 1000;

        emissions.AdditionalOutputs[AdditionalOrganicMatterSpreadingOutputs.NTotalNApplied].Value = 37 * 1000;
        emissions.AdditionalOutputs[AdditionalOrganicMatterSpreadingOutputs.TANApplied].Value = 18.5 * 1000;
        emissions.AdditionalOutputs[AdditionalOrganicMatterSpreadingOutputs.OrganicNApplied].Value = 18.5 * 1000;

        emissions.RoundEmissions(4);

        dataList.Add(
            new object[] { OrganicMatterType.SewageSludgeLiquid, 1000.00, SpreadingLandUse.Grassland, Month.January, 37.00, 18.5 / 37 * 100,
                emissions });

        emissions = new OrganicMatterSpreadingEmission();
        emissions.InitialiseOrganicMatterSpreadingEmissions();

        emissions.EmissionsCore[CoreEmissions.DirectNH3N].Value = 1.720 * 1000;
        emissions.EmissionsCore[CoreEmissions.DirectN2ON].Value = 0.050 * 1000;
        emissions.EmissionsCore[CoreEmissions.DirectNON].Value = 0.020 * 1000;
        emissions.EmissionsCore[CoreEmissions.LeachedNO3N].Value = 1.50 * 1000;
        emissions.EmissionsCore[CoreEmissions.N2ONleached].Value = 0.01125 * 1000;
        emissions.EmissionsCore[CoreEmissions.N2ONvolatalised].Value = 0.024360 * 1000;
        emissions.EmissionsCore[CoreEmissions.DirectN2N].Value = 0.15 * 1000;

        emissions.AdditionalOutputs[AdditionalOrganicMatterSpreadingOutputs.NTotalNApplied].Value = 5.0 * 1000;
        emissions.AdditionalOutputs[AdditionalOrganicMatterSpreadingOutputs.TANApplied].Value = 4.0 * 1000;
        emissions.AdditionalOutputs[AdditionalOrganicMatterSpreadingOutputs.OrganicNApplied].Value = 1.0 * 1000;

        emissions.RoundEmissions(4);

        dataList.Add(
            new object[] { OrganicMatterType.DigestateFoodbased, 1000.00, SpreadingLandUse.Arable, Month.January, 5.0, 4.0 / 5.0 * 100,
                emissions });

        emissions = new OrganicMatterSpreadingEmission();
        emissions.InitialiseOrganicMatterSpreadingEmissions();

        emissions.EmissionsCore[CoreEmissions.DirectNH3N].Value = 1.720 * 1000;
        emissions.EmissionsCore[CoreEmissions.DirectN2ON].Value = 0.050 * 1000;
        emissions.EmissionsCore[CoreEmissions.DirectNON].Value = 0.030 * 1000;
        emissions.EmissionsCore[CoreEmissions.LeachedNO3N].Value = 0.50 * 1000;
        emissions.EmissionsCore[CoreEmissions.N2ONleached].Value = 0.00375 * 1000;
        emissions.EmissionsCore[CoreEmissions.N2ONvolatalised].Value = 0.0245 * 1000;
        emissions.EmissionsCore[CoreEmissions.DirectN2N].Value = 0.15 * 1000;

        emissions.AdditionalOutputs[AdditionalOrganicMatterSpreadingOutputs.NTotalNApplied].Value = 5.0 * 1000;
        emissions.AdditionalOutputs[AdditionalOrganicMatterSpreadingOutputs.TANApplied].Value = 4.0 * 1000;
        emissions.AdditionalOutputs[AdditionalOrganicMatterSpreadingOutputs.OrganicNApplied].Value = 1.0 * 1000;

        emissions.RoundEmissions(4);

        dataList.Add(
            new object[] { OrganicMatterType.DigestateFoodbased, 1000.00, SpreadingLandUse.Grassland, Month.January, 5.0, 4.0 / 5.0 * 100,
                emissions });

        emissions = new OrganicMatterSpreadingEmission();
        emissions.InitialiseOrganicMatterSpreadingEmissions();

        emissions.EmissionsCore[CoreEmissions.DirectNH3N].Value = 1.36568 * 1000;
        emissions.EmissionsCore[CoreEmissions.DirectN2ON].Value = 0.0397 * 1000;
        emissions.EmissionsCore[CoreEmissions.DirectNON].Value = 0.02382 * 1000;
        emissions.EmissionsCore[CoreEmissions.LeachedNO3N].Value = 0.3970 * 1000;
        emissions.EmissionsCore[CoreEmissions.N2ONleached].Value = 0.0029775 * 1000;
        emissions.EmissionsCore[CoreEmissions.N2ONvolatalised].Value = 0.019453 * 1000;
        emissions.EmissionsCore[CoreEmissions.DirectN2N].Value = 0.1191 * 1000;

        emissions.AdditionalOutputs[AdditionalOrganicMatterSpreadingOutputs.NTotalNApplied].Value = 3.97 * 1000;
        emissions.AdditionalOutputs[AdditionalOrganicMatterSpreadingOutputs.TANApplied].Value = 3.176 * 1000;
        emissions.AdditionalOutputs[AdditionalOrganicMatterSpreadingOutputs.OrganicNApplied].Value = 0.794 * 1000;

        emissions.RoundEmissions(4);

        dataList.Add(
            new object[] { OrganicMatterType.DigestateCropbased, 1000.00, SpreadingLandUse.Grassland, Month.April, 3.97, 3.176 / 3.97 * 100,
                emissions });

        emissions = new OrganicMatterSpreadingEmission();
        emissions.InitialiseOrganicMatterSpreadingEmissions();

        emissions.EmissionsCore[CoreEmissions.DirectNH3N].Value = 2.09 * 1000;
        emissions.EmissionsCore[CoreEmissions.DirectN2ON].Value = 0.7475 * 1000;
        emissions.EmissionsCore[CoreEmissions.DirectNON].Value = 0.299 * 1000;
        emissions.EmissionsCore[CoreEmissions.LeachedNO3N].Value = 30.00 * 1000;
        emissions.EmissionsCore[CoreEmissions.N2ONleached].Value = 0.225 * 1000;
        emissions.EmissionsCore[CoreEmissions.N2ONvolatalised].Value = 0.033446 * 1000;
        emissions.EmissionsCore[CoreEmissions.DirectN2N].Value = 2.2425 * 1000;

        emissions.AdditionalOutputs[AdditionalOrganicMatterSpreadingOutputs.NTotalNApplied].Value = 100 * 1000;
        emissions.AdditionalOutputs[AdditionalOrganicMatterSpreadingOutputs.TANApplied].Value = 10 * 1000;
        emissions.AdditionalOutputs[AdditionalOrganicMatterSpreadingOutputs.OrganicNApplied].Value = 90 * 1000;

        emissions.RoundEmissions(4);

        dataList.Add(
            new object[] { OrganicMatterType.CattleSlurry, 1000.00, SpreadingLandUse.Arable, Month.February, 100, 10.0 / 100 * 100,
                emissions });

        emissions = new OrganicMatterSpreadingEmission();
        emissions.InitialiseOrganicMatterSpreadingEmissions();

        emissions.EmissionsCore[CoreEmissions.DirectNH3N].Value = 6.830 * 1000;
        emissions.EmissionsCore[CoreEmissions.DirectN2ON].Value = 0.33 * 1000;
        emissions.EmissionsCore[CoreEmissions.DirectNON].Value = 0.1320 * 1000;
        emissions.EmissionsCore[CoreEmissions.LeachedNO3N].Value = 30.0 * 1000;
        emissions.EmissionsCore[CoreEmissions.N2ONleached].Value = 0.225 * 1000;
        emissions.EmissionsCore[CoreEmissions.N2ONvolatalised].Value = 0.097468 * 1000;
        emissions.EmissionsCore[CoreEmissions.DirectN2N].Value = 0.990 * 1000;

        emissions.AdditionalOutputs[AdditionalOrganicMatterSpreadingOutputs.NTotalNApplied].Value = 100 * 1000;
        emissions.AdditionalOutputs[AdditionalOrganicMatterSpreadingOutputs.TANApplied].Value = 10 * 1000;
        emissions.AdditionalOutputs[AdditionalOrganicMatterSpreadingOutputs.OrganicNApplied].Value = 90 * 1000;

        emissions.RoundEmissions(4);

        dataList.Add(
            new object[] { OrganicMatterType.CattleFYM, 1000.00, SpreadingLandUse.Arable, Month.January, 100, 10.0 / 100 * 100,
                emissions });

        emissions = new OrganicMatterSpreadingEmission();
        emissions.InitialiseOrganicMatterSpreadingEmissions();

        emissions.EmissionsCore[CoreEmissions.DirectNH3N].Value = 2.090 * 1000;
        emissions.EmissionsCore[CoreEmissions.DirectN2ON].Value = 0.7475 * 1000;
        emissions.EmissionsCore[CoreEmissions.DirectNON].Value = 0.299 * 1000;
        emissions.EmissionsCore[CoreEmissions.LeachedNO3N].Value = 30.0 * 1000;
        emissions.EmissionsCore[CoreEmissions.N2ONleached].Value = 0.225 * 1000;
        emissions.EmissionsCore[CoreEmissions.N2ONvolatalised].Value = 0.033446 * 1000;
        emissions.EmissionsCore[CoreEmissions.DirectN2N].Value = 2.2425 * 1000;

        emissions.AdditionalOutputs[AdditionalOrganicMatterSpreadingOutputs.NTotalNApplied].Value = 100 * 1000;
        emissions.AdditionalOutputs[AdditionalOrganicMatterSpreadingOutputs.TANApplied].Value = 10 * 1000;
        emissions.AdditionalOutputs[AdditionalOrganicMatterSpreadingOutputs.OrganicNApplied].Value = 90 * 1000;

        emissions.RoundEmissions(4);

        dataList.Add(
            new object[] { OrganicMatterType.DigestateCattleSlurry, 1000.00, SpreadingLandUse.Arable, Month.January, 100, 10.0 / 100 * 100,
                emissions });

        emissions = new();
        emissions.InitialiseOrganicMatterSpreadingEmissions();

        emissions.EmissionsCore[CoreEmissions.DirectNH3N].Value = 2.090 * 1000;
        emissions.EmissionsCore[CoreEmissions.DirectN2ON].Value = 0.74750 * 1000;
        emissions.EmissionsCore[CoreEmissions.DirectNON].Value = 0.29900 * 1000;
        emissions.EmissionsCore[CoreEmissions.LeachedNO3N].Value = 30.0 * 1000;
        emissions.EmissionsCore[CoreEmissions.N2ONleached].Value = 0.225 * 1000;
        emissions.EmissionsCore[CoreEmissions.N2ONvolatalised].Value = 0.033446 * 1000;
        emissions.EmissionsCore[CoreEmissions.DirectN2N].Value = 2.24250 * 1000;

        emissions.AdditionalOutputs[AdditionalOrganicMatterSpreadingOutputs.NTotalNApplied].Value = 100 * 1000;
        emissions.AdditionalOutputs[AdditionalOrganicMatterSpreadingOutputs.TANApplied].Value = 10 * 1000;
        emissions.AdditionalOutputs[AdditionalOrganicMatterSpreadingOutputs.OrganicNApplied].Value = 90 * 1000;

        emissions.RoundEmissions(4);
        dataList.Add(
            new object[] { OrganicMatterType.CattleSlurry, 1000.00, SpreadingLandUse.Arable, Month.February, 100, 10.0 / 100 * 100,
                emissions });

        return dataList;
    }

    [Theory]
    [MemberData(nameof(UnmitigatedOrganicMatterSpreadingEmissions_TestGenerator))]
    public void UnmitigatedOrganicMatterSpreadingEmissions_AllOutputsCorrect(OrganicMatterType organicMatterType, double quantityperhectare, SpreadingLandUse landAppliedTo, Month month, double unitTotalNperkg, double percentTAN, OrganicMatterSpreadingEmission expectedResult)
    {

        OrganicMatterSpreadingEmission actualResult = OrganicMatterSpreading.UnmitigatedOrganicMatterSpreadingEmissions(organicMatterType, quantityperhectare, landAppliedTo, month, unitTotalNperkg, percentTAN);
        actualResult.RoundEmissions(4);

        string actualResultJson = JsonSerializer.Serialize(actualResult, new JsonSerializerOptions
        {
            NumberHandling = JsonNumberHandling.AllowNamedFloatingPointLiterals
        });
        string expectedResultJson = JsonSerializer.Serialize(expectedResult, new JsonSerializerOptions
        {
            NumberHandling = JsonNumberHandling.AllowNamedFloatingPointLiterals
        });

        Assert.Equal(expectedResultJson, actualResultJson);
    }

    public static IEnumerable<object[]> MitigatedOrganicMatterSpreadingEmissions_TestGenerator()
    {
        var dataList = new List<object[]>();

        OrganicMatterSpreadingEmission emissions = new();
        emissions.InitialiseOrganicMatterSpreadingEmissions();

        emissions.EmissionsCore[CoreEmissions.DirectNH3N].Value = 0.195360;
        emissions.EmissionsCore[CoreEmissions.DirectN2ON].Value = 0.037000;
        emissions.EmissionsCore[CoreEmissions.DirectNON].Value = 0.022200;
        emissions.EmissionsCore[CoreEmissions.LeachedNO3N].Value = 0.370000;
        emissions.EmissionsCore[CoreEmissions.N2ONleached].Value = 0.002775;
        emissions.EmissionsCore[CoreEmissions.N2ONvolatalised].Value = 0.003045840;
        emissions.EmissionsCore[CoreEmissions.DirectN2N].Value = 0.11100000;

        emissions.AdditionalOutputs[AdditionalOrganicMatterSpreadingOutputs.NTotalNApplied].Value = 3.7;
        emissions.AdditionalOutputs[AdditionalOrganicMatterSpreadingOutputs.TANApplied].Value = 1.85;
        emissions.AdditionalOutputs[AdditionalOrganicMatterSpreadingOutputs.OrganicNApplied].Value = 1.85;

        List<int> mitigationMethods = new List<int>() { 22 };

        emissions.RoundEmissions(4);

        dataList.Add(
            new object[] { OrganicMatterType.SewageSludgeLiquid, 100.00, SpreadingLandUse.Grassland, mitigationMethods, Month.January, 0.037, 50,
                emissions });

        emissions = new();
        emissions.InitialiseOrganicMatterSpreadingEmissions();

        emissions.EmissionsCore[CoreEmissions.DirectNH3N].Value = 1.16160 * 1000;
        emissions.EmissionsCore[CoreEmissions.DirectN2ON].Value = 0.74750 * 1000;
        emissions.EmissionsCore[CoreEmissions.DirectNON].Value = 0.44850 * 1000;
        emissions.EmissionsCore[CoreEmissions.LeachedNO3N].Value = 10.0 * 1000;
        emissions.EmissionsCore[CoreEmissions.N2ONleached].Value = 0.07500 * 1000;
        emissions.EmissionsCore[CoreEmissions.N2ONvolatalised].Value = 0.0225414 * 1000;
        emissions.EmissionsCore[CoreEmissions.DirectN2N].Value = 2.24250 * 1000;

        emissions.AdditionalOutputs[AdditionalOrganicMatterSpreadingOutputs.NTotalNApplied].Value = 100 * 1000;
        emissions.AdditionalOutputs[AdditionalOrganicMatterSpreadingOutputs.TANApplied].Value = 10 * 1000;
        emissions.AdditionalOutputs[AdditionalOrganicMatterSpreadingOutputs.OrganicNApplied].Value = 90 * 1000;

        mitigationMethods = new List<int>() { 23 };

        emissions.RoundEmissions(4);

        dataList.Add(
            new object[] { OrganicMatterType.PigSlurry, 1000.00, SpreadingLandUse.Grassland, mitigationMethods, Month.May, 100, 10.0 / 100 * 100,
                emissions });

        emissions = new();
        emissions.InitialiseOrganicMatterSpreadingEmissions();

        emissions.EmissionsCore[CoreEmissions.DirectNH3N].Value = 1.12860 * 1000;
        emissions.EmissionsCore[CoreEmissions.DirectN2ON].Value = 0.74750 * 1000;
        emissions.EmissionsCore[CoreEmissions.DirectNON].Value = 0.29900 * 1000;
        emissions.EmissionsCore[CoreEmissions.LeachedNO3N].Value = 30.0 * 1000;
        emissions.EmissionsCore[CoreEmissions.N2ONleached].Value = 0.225 * 1000;
        emissions.EmissionsCore[CoreEmissions.N2ONvolatalised].Value = 0.0199864 * 1000;
        emissions.EmissionsCore[CoreEmissions.DirectN2N].Value = 2.24250 * 1000;

        emissions.AdditionalOutputs[AdditionalOrganicMatterSpreadingOutputs.NTotalNApplied].Value = 100 * 1000;
        emissions.AdditionalOutputs[AdditionalOrganicMatterSpreadingOutputs.TANApplied].Value = 10 * 1000;
        emissions.AdditionalOutputs[AdditionalOrganicMatterSpreadingOutputs.OrganicNApplied].Value = 90 * 1000;

        mitigationMethods = new List<int>() { 24 };

        emissions.RoundEmissions(4);

        dataList.Add(
            new object[] { OrganicMatterType.CattleSlurry, 1000.0, SpreadingLandUse.Arable, mitigationMethods, Month.February, 100, 10.0 / 100 * 100,
                emissions });

        return dataList;
    }

    [Theory]
    [MemberData(nameof(MitigatedOrganicMatterSpreadingEmissions_TestGenerator))]
    public void MitigatedOrganicMatterSpreadingEmissions_AllOutputsCorrect(OrganicMatterType organicMatterType, double quantityperhectare, SpreadingLandUse landAppliedTo, List<int> methods, Month month, double unitTotalNperkg, double percentTAN, OrganicMatterSpreadingEmission expectedResult)
    {

        OrganicMatterSpreadingEmission actualResult = OrganicMatterSpreading.MitigatedOrganicMatterSpreadingEmissions(organicMatterType, quantityperhectare, landAppliedTo, methods, month, unitTotalNperkg, percentTAN);
        actualResult.RoundEmissions(4);

        string actualResultJson = JsonSerializer.Serialize(actualResult, new JsonSerializerOptions
        {
            NumberHandling = JsonNumberHandling.AllowNamedFloatingPointLiterals
        });
        string expectedResultJson = JsonSerializer.Serialize(expectedResult, new JsonSerializerOptions
        {
            NumberHandling = JsonNumberHandling.AllowNamedFloatingPointLiterals
        });

        Assert.Equal(expectedResultJson, actualResultJson);
    }

    public static IEnumerable<object[]> UnmitigatedManureSpreadingEmissionSheep_TestGenerator()
    {
        var dataList = new List<object[]>();

        var totalN = 0.087281826;
        var TAN = 0.013129228;
        var firstWinterFracLeach = 5.022704739;
        var manureFracLeachCoefficients = new GrassEquationCoefficients
        {
            Coeff_X = -0.00384815,
            Coeff_X2 = 4.87105E-05,
            Coeff_X3 = -1.38E-07,
            Coeff_X4 = 0.0000000001289,
            C = 1.975738077
        };
        var grassFertiliserRate = 100;
        var month = Month.NotSet;

        OrganicMatterSpreadingEmission emissions = new();
        emissions.InitialiseOrganicMatterSpreadingEmissions();

        emissions.EmissionsCore[CoreEmissions.DirectNH3N].Value = 0.008969888;
        emissions.EmissionsCore[CoreEmissions.DirectN2ON].Value = 0.00028803;
        emissions.EmissionsCore[CoreEmissions.DirectNON].Value = 0.000172818;
        emissions.EmissionsCore[CoreEmissions.LeachedNO3N].Value = 0.005848915;
        emissions.EmissionsCore[CoreEmissions.N2ONleached].Value = 4.3866860970E-05;
        emissions.EmissionsCore[CoreEmissions.N2ONvolatalised].Value = 0.000127998;
        emissions.EmissionsCore[CoreEmissions.DirectN2N].Value = 0.00086409;

        emissions.AdditionalOutputs[AdditionalOrganicMatterSpreadingOutputs.NTotalNApplied].Value = 0.087281826;
        emissions.AdditionalOutputs[AdditionalOrganicMatterSpreadingOutputs.TANApplied].Value = 0.013129228;
        emissions.AdditionalOutputs[AdditionalOrganicMatterSpreadingOutputs.OrganicNApplied].Value = totalN - TAN;

        double geneticManureFracLeachScalar = 0.9604;

        dataList.Add(
            new object[] { totalN, TAN, firstWinterFracLeach,manureFracLeachCoefficients,grassFertiliserRate,month, geneticManureFracLeachScalar,
                emissions });

        return dataList;
    }

    [Theory]
    [MemberData(nameof(UnmitigatedManureSpreadingEmissionSheep_TestGenerator))]
    public void UnmitigatedManureSpreadingEmissionSheep_AllOutputsCorrect(double TotalN, double TAN, double firstWinterFracLeach, GrassEquationCoefficients manureFracLeachCoefficients, double GrassFertiliserRate, Month month,
        double geneticManureFracLeachScalar, OrganicMatterSpreadingEmission expectedResult)
    {

        OrganicMatterSpreadingEmission actualResult = OrganicMatterSpreading.UnmitigatedManureSpreadingEmissionSheep(TotalN, TAN, firstWinterFracLeach, manureFracLeachCoefficients, GrassFertiliserRate, month, geneticManureFracLeachScalar);
        actualResult.RoundEmissions(4);

        string actualResultJson = JsonSerializer.Serialize(actualResult, new JsonSerializerOptions
        {
            NumberHandling = JsonNumberHandling.AllowNamedFloatingPointLiterals
        });

        expectedResult.RoundEmissions(4);

        string expectedResultJson = JsonSerializer.Serialize(expectedResult, new JsonSerializerOptions
        {
            NumberHandling = JsonNumberHandling.AllowNamedFloatingPointLiterals
        });

        Assert.Equal(expectedResultJson, actualResultJson);
    }
}
