using FrameAgricultureApi.Libraries.CropBurning;
using System.Text.Json;
using System.Text.Json.Serialization;
using static FrameAgricultureApi.Enumerators.Enumerators;

namespace FrameAgricultureApi.Components.Tests;

public class CropBurningTests
{
    public static IEnumerable<object[]> CalculateCropBurningEmissions_TestGenerator()
    {
        var dataList = new List<object[]>();

        CropBurningEmissions cropBurningEmissions = new CropBurningEmissions();
        cropBurningEmissions.InitialiseCropBurningEmissions();

        cropBurningEmissions.CombustionEmissions[CombustionEmissions.BurningEfficiency].Value = 0.9;
        cropBurningEmissions.CombustionEmissions[CombustionEmissions.ResidueDryMatterBurned].Value = 5000;
        cropBurningEmissions.CombustionEmissions[CombustionEmissions.CombustionCO].Value = 0.30015;
        cropBurningEmissions.CombustionEmissions[CombustionEmissions.CombustionCH4].Value = 0.01215;
        cropBurningEmissions.CombustionEmissions[CombustionEmissions.CombustionN2ON].Value = 0.000200455;
        cropBurningEmissions.CombustionEmissions[CombustionEmissions.CombustionNH3N].Value = 0.008894;
        cropBurningEmissions.CombustionEmissions[CombustionEmissions.CombustionNMVOC].Value = 0.00225;
        cropBurningEmissions.CombustionEmissions[CombustionEmissions.CombustionNOxN].Value = 0.00315;
        cropBurningEmissions.CombustionEmissions[CombustionEmissions.CombustionPM10].Value = 0.02565;
        cropBurningEmissions.CombustionEmissions[CombustionEmissions.CombustionPM2_5].Value = 0.0243;
        cropBurningEmissions.CombustionEmissions[CombustionEmissions.CombustionSO2].Value = 0.00225;
        cropBurningEmissions.CombustionEmissions[CombustionEmissions.CombustionTSP].Value = 0.0261;

        cropBurningEmissions.RoundEmissions(5);

        dataList.Add(
            new object[] { 5000, CropType.Oats, cropBurningEmissions });

        cropBurningEmissions = new CropBurningEmissions();
        cropBurningEmissions.InitialiseCropBurningEmissions();
        cropBurningEmissions.CombustionEmissions[CombustionEmissions.BurningEfficiency].Value = 0.9;
        cropBurningEmissions.CombustionEmissions[CombustionEmissions.ResidueDryMatterBurned].Value = 5000;
        cropBurningEmissions.CombustionEmissions[CombustionEmissions.CombustionCO].Value = 0.44415;
        cropBurningEmissions.CombustionEmissions[CombustionEmissions.CombustionCH4].Value = 0.01215;
        cropBurningEmissions.CombustionEmissions[CombustionEmissions.CombustionN2ON].Value = 0.000200;
        cropBurningEmissions.CombustionEmissions[CombustionEmissions.CombustionNH3N].Value = 0.008894;
        cropBurningEmissions.CombustionEmissions[CombustionEmissions.CombustionNMVOC].Value = 0.05265;
        cropBurningEmissions.CombustionEmissions[CombustionEmissions.CombustionNOxN].Value = 0.003698;
        cropBurningEmissions.CombustionEmissions[CombustionEmissions.CombustionPM10].Value = 0.03465;
        cropBurningEmissions.CombustionEmissions[CombustionEmissions.CombustionPM2_5].Value = 0.0333;
        cropBurningEmissions.CombustionEmissions[CombustionEmissions.CombustionSO2].Value = 0.00045;
        cropBurningEmissions.CombustionEmissions[CombustionEmissions.CombustionTSP].Value = 0.0351;

        cropBurningEmissions.RoundEmissions(5);

        dataList.Add(
            new object[] { 5000, CropType.Spring_barley, cropBurningEmissions });

        cropBurningEmissions = new CropBurningEmissions();
        cropBurningEmissions.InitialiseCropBurningEmissions();
        cropBurningEmissions.CombustionEmissions[CombustionEmissions.BurningEfficiency].Value = 0.9;
        cropBurningEmissions.CombustionEmissions[CombustionEmissions.ResidueDryMatterBurned].Value = 7000;
        cropBurningEmissions.CombustionEmissions[CombustionEmissions.CombustionCO].Value = 0.62181;
        cropBurningEmissions.CombustionEmissions[CombustionEmissions.CombustionCH4].Value = 0.01701;
        cropBurningEmissions.CombustionEmissions[CombustionEmissions.CombustionN2ON].Value = 0.000280636;
        cropBurningEmissions.CombustionEmissions[CombustionEmissions.CombustionNH3N].Value = 0.012452;
        cropBurningEmissions.CombustionEmissions[CombustionEmissions.CombustionNMVOC].Value = 0.07371;
        cropBurningEmissions.CombustionEmissions[CombustionEmissions.CombustionNOxN].Value = 0.005177;
        cropBurningEmissions.CombustionEmissions[CombustionEmissions.CombustionPM10].Value = 0.04851;
        cropBurningEmissions.CombustionEmissions[CombustionEmissions.CombustionPM2_5].Value = 0.04662;
        cropBurningEmissions.CombustionEmissions[CombustionEmissions.CombustionSO2].Value = 0.00063;
        cropBurningEmissions.CombustionEmissions[CombustionEmissions.CombustionTSP].Value = 0.04914;

        cropBurningEmissions.RoundEmissions(5);

        dataList.Add(
            new object[] { 7000, CropType.Spring_barley_malting, cropBurningEmissions });

        cropBurningEmissions = new CropBurningEmissions();
        cropBurningEmissions.InitialiseCropBurningEmissions();
        cropBurningEmissions.CombustionEmissions[CombustionEmissions.BurningEfficiency].Value = 0.9;
        cropBurningEmissions.CombustionEmissions[CombustionEmissions.ResidueDryMatterBurned].Value = 100;
        cropBurningEmissions.CombustionEmissions[CombustionEmissions.CombustionCO].Value = 0;
        cropBurningEmissions.CombustionEmissions[CombustionEmissions.CombustionCH4].Value = 0;
        cropBurningEmissions.CombustionEmissions[CombustionEmissions.CombustionN2ON].Value = 0;
        cropBurningEmissions.CombustionEmissions[CombustionEmissions.CombustionNH3N].Value = 0;
        cropBurningEmissions.CombustionEmissions[CombustionEmissions.CombustionNMVOC].Value = 0;
        cropBurningEmissions.CombustionEmissions[CombustionEmissions.CombustionNOxN].Value = 0;
        cropBurningEmissions.CombustionEmissions[CombustionEmissions.CombustionPM10].Value = 0;
        cropBurningEmissions.CombustionEmissions[CombustionEmissions.CombustionPM2_5].Value = 0;
        cropBurningEmissions.CombustionEmissions[CombustionEmissions.CombustionSO2].Value = 0;
        cropBurningEmissions.CombustionEmissions[CombustionEmissions.CombustionTSP].Value = 0;

        cropBurningEmissions.RoundEmissions(5);

        dataList.Add(
            new object[] { 100, CropType.Minor_cereals, cropBurningEmissions });

        return dataList;
    }

    [Theory]
    [MemberData(nameof(CalculateCropBurningEmissions_TestGenerator))]
    public static void CalculateCropBurningEmissions(double residueDryMatterBurned, CropType cropType, CropBurningEmissions expectedResult)
    {
        CropBurningEmissions actualResult = CropBurning.CropBurningEmissions(residueDryMatterBurned, cropType);

        actualResult.RoundEmissions(5);

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
}
