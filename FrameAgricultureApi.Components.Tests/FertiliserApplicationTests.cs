using FrameAgricultureApi.Libraries.Fertiliser;
using System.Text.Json;
using System.Text.Json.Serialization;
using static FrameAgricultureApi.Enumerators.Enumerators;

namespace FrameAgricultureApi.Components.Tests;

public class FertiliserApplicationTests
{

    public static IEnumerable<object[]> UnmitigatedEmissionsFertiliser_TestGenerator()
    {
        var dataList = new List<object[]>();

        FertiliserEmissions fertiliserEmissions = new FertiliserEmissions();
        fertiliserEmissions.InitialiseFertiliserEmissions();
        fertiliserEmissions.EmissionsCore[CoreEmissions.DirectN2ON].Value = 1.064765;
        fertiliserEmissions.EmissionsCore[CoreEmissions.DirectNH3N].Value = 36.79613;
        fertiliserEmissions.EmissionsCore[CoreEmissions.DirectNON].Value = 0.425906;
        fertiliserEmissions.EmissionsCore[CoreEmissions.DirectN2N].Value = 3.194295;
        fertiliserEmissions.EmissionsCore[CoreEmissions.N2ONleached].Value = 0.45;
        fertiliserEmissions.EmissionsCore[CoreEmissions.N2ONvolatalised].Value = 0.521109;
        fertiliserEmissions.EmissionsCore[CoreEmissions.LeachedNO3N].Value = 60;
        fertiliserEmissions.AdditionalOutputs[FertiliserType.Urea].Value = 200;

        fertiliserEmissions.RoundEmissions(4);

        dataList.Add(
            new object[] {FertiliserType.Urea, new double[]{0, 0, 50, 50, 0, 100, 0, 0, 0, 0, 0, 0 }, 500, 40, 219, true,
                fertiliserEmissions });

        fertiliserEmissions = new FertiliserEmissions();
        fertiliserEmissions.InitialiseFertiliserEmissions();
        fertiliserEmissions.EmissionsCore[CoreEmissions.DirectN2ON].Value = 6.493269;
        fertiliserEmissions.EmissionsCore[CoreEmissions.DirectNH3N].Value = 4.0608;
        fertiliserEmissions.EmissionsCore[CoreEmissions.DirectNON].Value = 2.597307;
        fertiliserEmissions.EmissionsCore[CoreEmissions.DirectN2N].Value = 19.47981;
        fertiliserEmissions.EmissionsCore[CoreEmissions.N2ONleached].Value = 0.54;
        fertiliserEmissions.EmissionsCore[CoreEmissions.N2ONvolatalised].Value = 0.093214;
        fertiliserEmissions.EmissionsCore[CoreEmissions.LeachedNO3N].Value = 72;
        fertiliserEmissions.AdditionalOutputs[FertiliserType.AmmoniumNitrate].Value = 240;

        fertiliserEmissions.RoundEmissions(4);

        dataList.Add(
            new object[] {FertiliserType.AmmoniumNitrate, new double[]{ 0, 0, 10, 20, 0, 100, 0, 10, 100, 0, 0, 0}, 600, 40, 402, true,
                fertiliserEmissions });

        fertiliserEmissions = new FertiliserEmissions();
        fertiliserEmissions.InitialiseFertiliserEmissions();
        fertiliserEmissions.EmissionsCore[CoreEmissions.DirectN2ON].Value = 7.925858;
        fertiliserEmissions.EmissionsCore[CoreEmissions.DirectNH3N].Value = 103.4034;
        fertiliserEmissions.EmissionsCore[CoreEmissions.DirectNON].Value = 3.170343;
        fertiliserEmissions.EmissionsCore[CoreEmissions.DirectN2N].Value = 23.77757;
        fertiliserEmissions.EmissionsCore[CoreEmissions.N2ONleached].Value = 1.4625;
        fertiliserEmissions.EmissionsCore[CoreEmissions.N2ONvolatalised].Value = 1.492033;
        fertiliserEmissions.EmissionsCore[CoreEmissions.LeachedNO3N].Value = 195;
        fertiliserEmissions.AdditionalOutputs[FertiliserType.AmmoniumSulphate_DiammoniumPhosphate].Value = 650;

        fertiliserEmissions.RoundEmissions(4);

        dataList.Add(
            new object[] {FertiliserType.AmmoniumSulphate_DiammoniumPhosphate, new double[]{ 150, 0, 100, 50, 0, 100, 0, 50, 100, 0, 100, 0}, 812.5, 80, 5193, false,
                fertiliserEmissions });

        return dataList;
    }

    [Theory]
    [MemberData(nameof(UnmitigatedEmissionsFertiliser_TestGenerator))]
    public void UnmitigatedEmissionsFertiliser_AllOutputsCorrect(FertiliserType fertiliserType, double[] FertiliserApplications, double totalNapplied, double percentageFertiliserType, int gridSquare_ID, bool acidicSoil, FertiliserEmissions expectedResult)
    {

        FertiliserEmissions actualEmissions = FrameAgricultureApi.Components.FertiliserApplication.UnmitigatedEmissionsFertiliser(fertiliserType, FertiliserApplications, totalNapplied, percentageFertiliserType, gridSquare_ID, acidicSoil);
        actualEmissions.RoundEmissions(4);

        string actualEmissionsJson = JsonSerializer.Serialize(actualEmissions, new JsonSerializerOptions
        {
            NumberHandling = JsonNumberHandling.AllowNamedFloatingPointLiterals
        });
        string expectedResultJson = JsonSerializer.Serialize(expectedResult, new JsonSerializerOptions
        {
            NumberHandling = JsonNumberHandling.AllowNamedFloatingPointLiterals
        });

        Assert.Equal(expectedResultJson, actualEmissionsJson);

    }

    public static IEnumerable<object[]> MitigatedEmissionsFertiliser_TestGenerator()
    {
        var dataList = new List<object[]>();

        FertiliserEmissions fertiliserEmissions = new FertiliserEmissions();
        fertiliserEmissions.InitialiseFertiliserEmissions();
        fertiliserEmissions.EmissionsCore[CoreEmissions.DirectN2ON].Value = 0.978514;
        fertiliserEmissions.EmissionsCore[CoreEmissions.DirectNH3N].Value = 36.79613;
        fertiliserEmissions.EmissionsCore[CoreEmissions.DirectNON].Value = 0.391406;
        fertiliserEmissions.EmissionsCore[CoreEmissions.DirectN2N].Value = 2.935543;
        fertiliserEmissions.EmissionsCore[CoreEmissions.N2ONleached].Value = 0.45 * 0.55;
        fertiliserEmissions.EmissionsCore[CoreEmissions.N2ONvolatalised].Value = 0.520626;
        fertiliserEmissions.EmissionsCore[CoreEmissions.LeachedNO3N].Value = 60 * 0.55;
        fertiliserEmissions.AdditionalOutputs[FertiliserType.Urea].Value = 200;

        fertiliserEmissions.RoundEmissions(4);

        List<int> mitigationMethods = new List<int>() { 1 };

        dataList.Add(
            new object[] {FertiliserType.Urea, new double[]{0, 0, 50, 50, 0, 100, 0, 0, 0, 0, 0, 0 }, 400, 50, 219, true, mitigationMethods,
                fertiliserEmissions });

        fertiliserEmissions = new FertiliserEmissions();
        fertiliserEmissions.InitialiseFertiliserEmissions();
        fertiliserEmissions.EmissionsCore[CoreEmissions.DirectN2ON].Value = 1.256124;
        fertiliserEmissions.EmissionsCore[CoreEmissions.DirectNH3N].Value = 58.45722;
        fertiliserEmissions.EmissionsCore[CoreEmissions.DirectNON].Value = 0.50245;
        fertiliserEmissions.EmissionsCore[CoreEmissions.DirectN2N].Value = 3.768373;
        fertiliserEmissions.EmissionsCore[CoreEmissions.N2ONleached].Value = 0.297;
        fertiliserEmissions.EmissionsCore[CoreEmissions.N2ONvolatalised].Value = 0.825435;
        fertiliserEmissions.EmissionsCore[CoreEmissions.LeachedNO3N].Value = 39.6;
        fertiliserEmissions.AdditionalOutputs[FertiliserType.Urea].Value = 240;

        fertiliserEmissions.RoundEmissions(4);

        mitigationMethods = new List<int>() { 1, 2 };

        dataList.Add(
            new object[] {FertiliserType.Urea, new double[]{ 0, 0, 10, 20, 0, 100, 0, 10, 100, 0, 0, 0}, 480, 50, 402, true, mitigationMethods,
                fertiliserEmissions });

        fertiliserEmissions = new FertiliserEmissions();
        fertiliserEmissions.InitialiseFertiliserEmissions();
        fertiliserEmissions.EmissionsCore[CoreEmissions.DirectN2ON].Value = 4.559927;
        fertiliserEmissions.EmissionsCore[CoreEmissions.DirectNH3N].Value = 30.53312;
        fertiliserEmissions.EmissionsCore[CoreEmissions.DirectNON].Value = 1.823971;
        fertiliserEmissions.EmissionsCore[CoreEmissions.DirectN2N].Value = 13.67978;
        fertiliserEmissions.EmissionsCore[CoreEmissions.N2ONleached].Value = 0.804375;
        fertiliserEmissions.EmissionsCore[CoreEmissions.N2ONvolatalised].Value = 0.452999;
        fertiliserEmissions.EmissionsCore[CoreEmissions.LeachedNO3N].Value = 107.25;
        fertiliserEmissions.AdditionalOutputs[FertiliserType.UreaAmmoniumNitrate].Value = 650;

        fertiliserEmissions.RoundEmissions(4);

        mitigationMethods = new List<int>() { 1, 3 };

        dataList.Add(
            new object[] {FertiliserType.UreaAmmoniumNitrate, new double[]{ 150, 0, 100, 50, 0, 100, 0, 50, 100, 0, 100, 0}, 812.5, 80, 863, false, mitigationMethods,
                fertiliserEmissions });

        fertiliserEmissions = new FertiliserEmissions();
        fertiliserEmissions.InitialiseFertiliserEmissions();
        fertiliserEmissions.EmissionsCore[CoreEmissions.DirectN2ON].Value = 4.559927;
        fertiliserEmissions.EmissionsCore[CoreEmissions.DirectNH3N].Value = 30.53312;
        fertiliserEmissions.EmissionsCore[CoreEmissions.DirectNON].Value = 1.823971;
        fertiliserEmissions.EmissionsCore[CoreEmissions.DirectN2N].Value = 13.67978;
        fertiliserEmissions.EmissionsCore[CoreEmissions.N2ONleached].Value = 0.804375;
        fertiliserEmissions.EmissionsCore[CoreEmissions.N2ONvolatalised].Value = 0.452999;
        fertiliserEmissions.EmissionsCore[CoreEmissions.LeachedNO3N].Value = 107.25;
        fertiliserEmissions.AdditionalOutputs[FertiliserType.UreaAmmoniumNitrate].Value = 650;

        fertiliserEmissions.RoundEmissions(4);

        mitigationMethods = new List<int>() { 1, 2, 3 };

        dataList.Add(
            new object[] {FertiliserType.UreaAmmoniumNitrate, new double[]{ 150, 0, 100, 50, 0, 100, 0, 50, 100, 0, 100, 0}, 812.5, 80, 863, false, mitigationMethods,
                fertiliserEmissions });

        fertiliserEmissions = new FertiliserEmissions();
        fertiliserEmissions.InitialiseFertiliserEmissions();
        fertiliserEmissions.EmissionsCore[CoreEmissions.DirectN2ON].Value = 122.8457;
        fertiliserEmissions.EmissionsCore[CoreEmissions.DirectNH3N].Value = 1.55664;
        fertiliserEmissions.EmissionsCore[CoreEmissions.DirectNON].Value = 49.13827;
        fertiliserEmissions.EmissionsCore[CoreEmissions.DirectN2N].Value = 368.5371;
        fertiliserEmissions.EmissionsCore[CoreEmissions.N2ONleached].Value = 1.035;
        fertiliserEmissions.EmissionsCore[CoreEmissions.N2ONvolatalised].Value = 0.709729;
        fertiliserEmissions.EmissionsCore[CoreEmissions.LeachedNO3N].Value = 138;
        fertiliserEmissions.AdditionalOutputs[FertiliserType.CalciumAmmoniumNitrate].Value = 460;

        fertiliserEmissions.RoundEmissions(4);

        mitigationMethods = new List<int>() { 4 };

        dataList.Add(
            new object[] {FertiliserType.CalciumAmmoniumNitrate, new double[]{ 0, 10, 100, 50, 0, 50, 0, 50, 100, 0, 100, 0}, 1150, 40, 6157, true, mitigationMethods,
                fertiliserEmissions });

        return dataList;
    }

    [Theory]
    [MemberData(nameof(MitigatedEmissionsFertiliser_TestGenerator))]
    public void MitigatedEmissionsFertiliser_AllOutputsCorrect(FertiliserType fertiliserType, double[] FertiliserApplications, double totalNapplied, double percentageFertiliserType, int gridSquare_ID, bool acidicSoil, List<int> mitigationMethods, FertiliserEmissions expectedResult)
    {

        FertiliserEmissions actualEmissions = FrameAgricultureApi.Components.FertiliserApplication.MitigatedEmissionsFertiliser(fertiliserType, FertiliserApplications, totalNapplied, percentageFertiliserType, gridSquare_ID, acidicSoil, mitigationMethods);
        actualEmissions.RoundEmissions(4);

        string actualEmissionsJson = JsonSerializer.Serialize(actualEmissions, new JsonSerializerOptions
        {
            NumberHandling = JsonNumberHandling.AllowNamedFloatingPointLiterals
        });
        string expectedResultJson = JsonSerializer.Serialize(expectedResult, new JsonSerializerOptions
        {
            NumberHandling = JsonNumberHandling.AllowNamedFloatingPointLiterals
        });

        Assert.Equal(expectedResultJson, actualEmissionsJson);

    }
}
