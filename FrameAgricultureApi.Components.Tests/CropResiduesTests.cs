using FrameAgricultureApi.Libraries.CropResidues;
using System.Text.Json;
using System.Text.Json.Serialization;
using static FrameAgricultureApi.Enumerators.Enumerators;

namespace FrameAgricultureApi.Components.Tests;

public class CropResiduesTests
{
    public static IEnumerable<object[]> UnmitigatedCropResidueEmissions_HarvestIndex_TestGenerator()
    {
        var dataList = new List<object[]>();

        CropResidueEmissions cropResidueEmissions = new CropResidueEmissions();
        cropResidueEmissions.InitialiseCropResidueEmissions();

        cropResidueEmissions.EmissionsCore[CoreEmissions.DirectN2ON].Value = 0.5444;
        cropResidueEmissions.EmissionsCore[CoreEmissions.LeachedNO3N].Value = 16.3323;
        cropResidueEmissions.EmissionsCore[CoreEmissions.N2ONleached].Value = 0.1225;
        cropResidueEmissions.EmissionsCore[CoreEmissions.DirectNON].Value = 0.2178;
        cropResidueEmissions.EmissionsCore[CoreEmissions.DirectN2N].Value = 1.6332;
        cropResidueEmissions.EmissionsCore[CoreEmissions.N2ONvolatalised].Value = 0.003049;
        cropResidueEmissions.EmissionsCore[CoreEmissions.DirectNH3N].Value = 0.0;

        cropResidueEmissions.AdditionalOutputs[AdditionalCropResidueEmissions.DryMatterOfftake].Value = 5094.140349441;
        cropResidueEmissions.AdditionalOutputs[AdditionalCropResidueEmissions.AboveGroundResidueDryMatter].Value = 5980.0778015;
        cropResidueEmissions.AdditionalOutputs[AdditionalCropResidueEmissions.AboveGroundResidueDryMatterN].Value = 32.2924;
        cropResidueEmissions.AdditionalOutputs[AdditionalCropResidueEmissions.BelowGroundResidueDryMatter].Value = 2768.5545;
        cropResidueEmissions.AdditionalOutputs[AdditionalCropResidueEmissions.BelowGroundResidueDryMatterN].Value = 22.1484;
        cropResidueEmissions.AdditionalOutputs[AdditionalCropResidueEmissions.NitrogeninHarvestedYield].Value = 89.1475;
        cropResidueEmissions.AdditionalOutputs[AdditionalCropResidueEmissions.NitrogeninStrawRemoved].Value = 0.0;

        cropResidueEmissions.RoundEmissions(4);

        dataList.Add(
            new object[] { 5923.419011, true, CropType.Oats, 5094.140349 / 5923.419011 * 100, 0.46, cropResidueEmissions });

        cropResidueEmissions = new CropResidueEmissions();
        cropResidueEmissions.InitialiseCropResidueEmissions();

        cropResidueEmissions.EmissionsCore[CoreEmissions.DirectN2ON].Value = 0.3840;
        cropResidueEmissions.EmissionsCore[CoreEmissions.LeachedNO3N].Value = 11.5206;
        cropResidueEmissions.EmissionsCore[CoreEmissions.N2ONleached].Value = 0.0864;
        cropResidueEmissions.EmissionsCore[CoreEmissions.DirectNON].Value = 0.1536;
        cropResidueEmissions.EmissionsCore[CoreEmissions.DirectN2N].Value = 1.1521;
        cropResidueEmissions.EmissionsCore[CoreEmissions.N2ONvolatalised].Value = 0.002151;
        cropResidueEmissions.EmissionsCore[CoreEmissions.DirectNH3N].Value = 0.0;

        cropResidueEmissions.AdditionalOutputs[AdditionalCropResidueEmissions.DryMatterOfftake].Value = 5108.4000;
        cropResidueEmissions.AdditionalOutputs[AdditionalCropResidueEmissions.AboveGroundResidueDryMatter].Value = 2998.4087;
        cropResidueEmissions.AdditionalOutputs[AdditionalCropResidueEmissions.AboveGroundResidueDryMatterN].Value = 16.1914;
        cropResidueEmissions.AdditionalOutputs[AdditionalCropResidueEmissions.BelowGroundResidueDryMatter].Value = 2776.3043;
        cropResidueEmissions.AdditionalOutputs[AdditionalCropResidueEmissions.BelowGroundResidueDryMatterN].Value = 22.2104;
        cropResidueEmissions.AdditionalOutputs[AdditionalCropResidueEmissions.NitrogeninHarvestedYield].Value = 89.3970;
        cropResidueEmissions.AdditionalOutputs[AdditionalCropResidueEmissions.NitrogeninStrawRemoved].Value = 16.1914
            ;

        cropResidueEmissions.RoundEmissions(4);

        dataList.Add(
            new object[] { 5940.0, false, CropType.Spring_oats, 5108.4 / 5940.0 * 100.0, 0.46, cropResidueEmissions });

        cropResidueEmissions = new CropResidueEmissions();
        cropResidueEmissions.InitialiseCropResidueEmissions();

        cropResidueEmissions.EmissionsCore[CoreEmissions.DirectN2ON].Value = 0.5585;
        cropResidueEmissions.EmissionsCore[CoreEmissions.LeachedNO3N].Value = 16.755242;
        cropResidueEmissions.EmissionsCore[CoreEmissions.N2ONleached].Value = 0.1257;
        cropResidueEmissions.EmissionsCore[CoreEmissions.DirectNON].Value = 0.2234;
        cropResidueEmissions.EmissionsCore[CoreEmissions.DirectN2N].Value = 1.6755;
        cropResidueEmissions.EmissionsCore[CoreEmissions.N2ONvolatalised].Value = 0.00312765;
        cropResidueEmissions.EmissionsCore[CoreEmissions.DirectNH3N].Value = 0.0;

        cropResidueEmissions.AdditionalOutputs[AdditionalCropResidueEmissions.DryMatterOfftake].Value = 7714.2;
        cropResidueEmissions.AdditionalOutputs[AdditionalCropResidueEmissions.AboveGroundResidueDryMatter].Value = 3857.1;
        cropResidueEmissions.AdditionalOutputs[AdditionalCropResidueEmissions.AboveGroundResidueDryMatterN].Value = 23.9140;
        cropResidueEmissions.AdditionalOutputs[AdditionalCropResidueEmissions.BelowGroundResidueDryMatter].Value = 3548.532;
        cropResidueEmissions.AdditionalOutputs[AdditionalCropResidueEmissions.BelowGroundResidueDryMatterN].Value = 31.9368;
        cropResidueEmissions.AdditionalOutputs[AdditionalCropResidueEmissions.NitrogeninHarvestedYield].Value = 143.0534855;
        cropResidueEmissions.AdditionalOutputs[AdditionalCropResidueEmissions.NitrogeninStrawRemoved].Value = 23.914020;

        cropResidueEmissions.RoundEmissions(4);

        dataList.Add(
            new object[] { 8970.0, false, CropType.Wheat, 7714.2 / 8970.0 * 100.0, 0.5, cropResidueEmissions });

        //need to add a non-cereal crop where the ammonia emissions occurs - e.g. potatoes or sugar beet

        return dataList;
    }

    [Theory]
    [MemberData(nameof(UnmitigatedCropResidueEmissions_HarvestIndex_TestGenerator))]
    public void UnmitigatedCropResidueEmissions_AllOutputsCorrect(double yieldFreshWeight, bool incorporated,
        CropType cropType, double cropDryMatterContent, double cropHarvestIndex, CropResidueEmissions expectedResult)
    {
        var actualResult = CropResidues.UnmitigatedCropResidueEmissions_HarvestIndex(yieldFreshWeight, incorporated, cropType, cropDryMatterContent, cropHarvestIndex);
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

    public static IEnumerable<object[]> MitigatedCropResidueEmissions_HarvestIndex_TestGenerator()
    {
        var dataList = new List<object[]>();

        CropResidueEmissions cropResidueEmissions = new CropResidueEmissions();
        cropResidueEmissions.InitialiseCropResidueEmissions();

        cropResidueEmissions.EmissionsCore[CoreEmissions.DirectN2ON].Value = 0.5444;
        cropResidueEmissions.EmissionsCore[CoreEmissions.LeachedNO3N].Value = 8.982741;
        cropResidueEmissions.EmissionsCore[CoreEmissions.N2ONleached].Value = 0.0674;
        cropResidueEmissions.EmissionsCore[CoreEmissions.DirectNON].Value = 0.2178;
        cropResidueEmissions.EmissionsCore[CoreEmissions.DirectN2N].Value = 1.6332;
        cropResidueEmissions.EmissionsCore[CoreEmissions.N2ONvolatalised].Value = 0.0030;
        cropResidueEmissions.EmissionsCore[CoreEmissions.DirectNH3N].Value = 0.0;

        cropResidueEmissions.AdditionalOutputs[AdditionalCropResidueEmissions.DryMatterOfftake].Value = 5094.140349441;
        cropResidueEmissions.AdditionalOutputs[AdditionalCropResidueEmissions.AboveGroundResidueDryMatter].Value = 5980.0778015;
        cropResidueEmissions.AdditionalOutputs[AdditionalCropResidueEmissions.AboveGroundResidueDryMatterN].Value = 32.2924;
        cropResidueEmissions.AdditionalOutputs[AdditionalCropResidueEmissions.BelowGroundResidueDryMatter].Value = 2768.5545;
        cropResidueEmissions.AdditionalOutputs[AdditionalCropResidueEmissions.BelowGroundResidueDryMatterN].Value = 22.1484;
        cropResidueEmissions.AdditionalOutputs[AdditionalCropResidueEmissions.NitrogeninHarvestedYield].Value = 89.1475;
        cropResidueEmissions.AdditionalOutputs[AdditionalCropResidueEmissions.NitrogeninStrawRemoved].Value = 0.0;

        cropResidueEmissions.RoundEmissions(4);

        var mitigationMethods = new List<int>() { 5 };

        dataList.Add(
            new object[] { 5923.419011, true, CropType.Oats, mitigationMethods, 5094.140349 / 5923.419011 * 100, 0.46, cropResidueEmissions });

        return dataList;
    }

    [Theory]
    [MemberData(nameof(MitigatedCropResidueEmissions_HarvestIndex_TestGenerator))]
    public void MitigatedCropResidueEmissions_AllOutputsCorrect(double yieldFreshWeight, bool incorporated,
        CropType cropType, List<int> mitigationMethods, double cropDryMatterContent, double cropHarvestIndex, CropResidueEmissions expectedResult)
    {
        var actualResult = CropResidues.MitigatedCropResidueEmissions_HarvestIndex(yieldFreshWeight, incorporated, cropType, mitigationMethods, cropDryMatterContent, cropHarvestIndex);
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

    public static IEnumerable<object[]> UnmitigatedCropResidueEmissions_IPCC_TestGenerator()
    {
        var dataList = new List<object[]>();

        CropResidueEmissions cropResidueEmissions = new CropResidueEmissions();
        cropResidueEmissions.InitialiseCropResidueEmissions();

        cropResidueEmissions.EmissionsCore[CoreEmissions.DirectN2ON].Value = 0.4979901;
        cropResidueEmissions.EmissionsCore[CoreEmissions.LeachedNO3N].Value = 14.9397;
        cropResidueEmissions.EmissionsCore[CoreEmissions.N2ONleached].Value = 0.112048;
        cropResidueEmissions.EmissionsCore[CoreEmissions.DirectNON].Value = 0.1992;
        cropResidueEmissions.EmissionsCore[CoreEmissions.DirectN2N].Value = 1.4939702;
        cropResidueEmissions.EmissionsCore[CoreEmissions.N2ONvolatalised].Value = 0.002789;
        cropResidueEmissions.EmissionsCore[CoreEmissions.DirectNH3N].Value = 0.0;

        cropResidueEmissions.AdditionalOutputs[AdditionalCropResidueEmissions.DryMatterOfftake].Value = 3397.000000000;
        cropResidueEmissions.AdditionalOutputs[AdditionalCropResidueEmissions.AboveGroundResidueDryMatter].Value = 4688.6100;
        cropResidueEmissions.AdditionalOutputs[AdditionalCropResidueEmissions.AboveGroundResidueDryMatterN].Value = 37.508880;
        cropResidueEmissions.AdditionalOutputs[AdditionalCropResidueEmissions.BelowGroundResidueDryMatter].Value = 1536.2659;
        cropResidueEmissions.AdditionalOutputs[AdditionalCropResidueEmissions.BelowGroundResidueDryMatterN].Value = 12.2901272;
        cropResidueEmissions.AdditionalOutputs[AdditionalCropResidueEmissions.NitrogeninHarvestedYield].Value = 161.3575;
        cropResidueEmissions.AdditionalOutputs[AdditionalCropResidueEmissions.NitrogeninStrawRemoved].Value = 0.0;

        cropResidueEmissions.RoundEmissions(4);

        dataList.Add(
            new object[] { 3950.000000000, false, CropType.Field_beans_harvesteddry, 219, 3397.0 / 3950.0 * 100, 1.13, 0.85, cropResidueEmissions });

        return dataList;
    }

    [Theory]
    [MemberData(nameof(UnmitigatedCropResidueEmissions_IPCC_TestGenerator))]
    public void UnmitigatedCropResidueEmissions_IPCC_AllOutputsCorrect(double yieldFreshWeight, bool incorporated,
        CropType cropType, int locationID, double cropDryMatterContent, double slopeIPCC, double interceptIPCC, CropResidueEmissions expectedResult)
    {
        var actualResult = CropResidues.UnmitigatedCropResidueEmissions_IPCC(yieldFreshWeight, incorporated, cropType, cropDryMatterContent, slopeIPCC, interceptIPCC);
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

    public static IEnumerable<object[]> MitigatedCropResidueEmissions_IPCC_TestGenerator()
    {
        var dataList = new List<object[]>();

        CropResidueEmissions cropResidueEmissions = new CropResidueEmissions();
        cropResidueEmissions.InitialiseCropResidueEmissions();

        cropResidueEmissions.EmissionsCore[CoreEmissions.DirectN2ON].Value = 0.4979901;
        cropResidueEmissions.EmissionsCore[CoreEmissions.LeachedNO3N].Value = 8.21684;
        cropResidueEmissions.EmissionsCore[CoreEmissions.N2ONleached].Value = 0.06163;
        cropResidueEmissions.EmissionsCore[CoreEmissions.DirectNON].Value = 0.1992;
        cropResidueEmissions.EmissionsCore[CoreEmissions.DirectN2N].Value = 1.4939702;
        cropResidueEmissions.EmissionsCore[CoreEmissions.N2ONvolatalised].Value = 0.00278874;
        cropResidueEmissions.EmissionsCore[CoreEmissions.DirectNH3N].Value = 0.0;

        cropResidueEmissions.AdditionalOutputs[AdditionalCropResidueEmissions.DryMatterOfftake].Value = 3397.000000000;
        cropResidueEmissions.AdditionalOutputs[AdditionalCropResidueEmissions.AboveGroundResidueDryMatter].Value = 4688.6100;
        cropResidueEmissions.AdditionalOutputs[AdditionalCropResidueEmissions.AboveGroundResidueDryMatterN].Value = 37.508880;
        cropResidueEmissions.AdditionalOutputs[AdditionalCropResidueEmissions.BelowGroundResidueDryMatter].Value = 1536.2659;
        cropResidueEmissions.AdditionalOutputs[AdditionalCropResidueEmissions.BelowGroundResidueDryMatterN].Value = 12.2901272;
        cropResidueEmissions.AdditionalOutputs[AdditionalCropResidueEmissions.NitrogeninHarvestedYield].Value = 161.3575;
        cropResidueEmissions.AdditionalOutputs[AdditionalCropResidueEmissions.NitrogeninStrawRemoved].Value = 0.0;

        cropResidueEmissions.RoundEmissions(4);

        var mitigationMethods = new List<int> { 5 };

        dataList.Add(
            new object[] { 3950.000000000, true, CropType.Field_beans_harvesteddry, 219, mitigationMethods, 3397.0 / 3950.0 * 100, 1.13, 0.85, cropResidueEmissions });

        cropResidueEmissions = new CropResidueEmissions();
        cropResidueEmissions.InitialiseCropResidueEmissions();

        cropResidueEmissions.EmissionsCore[CoreEmissions.DirectN2ON].Value = 0.62281;
        cropResidueEmissions.EmissionsCore[CoreEmissions.LeachedNO3N].Value = 10.27631;
        cropResidueEmissions.EmissionsCore[CoreEmissions.N2ONleached].Value = 0.07707;
        cropResidueEmissions.EmissionsCore[CoreEmissions.DirectNON].Value = 0.24912;
        cropResidueEmissions.EmissionsCore[CoreEmissions.DirectN2N].Value = 1.86842;
        cropResidueEmissions.EmissionsCore[CoreEmissions.N2ONvolatalised].Value = 0.01222;
        cropResidueEmissions.EmissionsCore[CoreEmissions.DirectNH3N].Value = 0.62364;

        cropResidueEmissions.AdditionalOutputs[AdditionalCropResidueEmissions.DryMatterOfftake].Value = 7866.000000000;
        cropResidueEmissions.AdditionalOutputs[AdditionalCropResidueEmissions.AboveGroundResidueDryMatter].Value = 1846.6000;
        cropResidueEmissions.AdditionalOutputs[AdditionalCropResidueEmissions.AboveGroundResidueDryMatterN].Value = 35.0854;
        cropResidueEmissions.AdditionalOutputs[AdditionalCropResidueEmissions.BelowGroundResidueDryMatter].Value = 1942.5200;
        cropResidueEmissions.AdditionalOutputs[AdditionalCropResidueEmissions.BelowGroundResidueDryMatterN].Value = 27.1953;
        cropResidueEmissions.AdditionalOutputs[AdditionalCropResidueEmissions.NitrogeninHarvestedYield].Value = 98.3250;
        cropResidueEmissions.AdditionalOutputs[AdditionalCropResidueEmissions.NitrogeninStrawRemoved].Value = 0.0;

        cropResidueEmissions.RoundEmissions(4);

        mitigationMethods = new List<int> { 5 };

        dataList.Add(
            new object[] { 39330.0000, false, CropType.Potatoes, 219, mitigationMethods, 7866.0 / 39330.0 * 100, 0.1, 1.06, cropResidueEmissions });

        return dataList;
    }

    [Theory]
    [MemberData(nameof(MitigatedCropResidueEmissions_IPCC_TestGenerator))]
    public void MitigatedCropResidueEmissions_IPCC_AllOutputsCorrect(double yieldFreshWeight, bool incorporated,
        CropType cropType, int locationID, List<int> mitigationMethods, double cropDryMatterContent, double slopeIPCC, double interceptIPCC, CropResidueEmissions expectedResult)
    {
        var actualResult = CropResidues.MitigatedCropResidueEmissions_IPCC(yieldFreshWeight, incorporated, cropType, mitigationMethods, cropDryMatterContent, slopeIPCC, interceptIPCC);
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
}
