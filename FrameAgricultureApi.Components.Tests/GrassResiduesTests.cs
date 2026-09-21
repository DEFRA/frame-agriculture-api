using FrameAgricultureApi.Libraries.GrassResidues;
using System.Text.Json;
using System.Text.Json.Serialization;
using static FrameAgricultureApi.Enumerators.Enumerators;

namespace FrameAgricultureApi.Components.Tests;

public class GrassResiduesTests
{
    public static IEnumerable<object[]> UnmitigatedProductionGrassResidueEmissions_TestGenerator()
    {
        var dataList = new List<object[]>();

        var expectedResult = new GrassResidueEmissions();
        expectedResult.InitialiseGrassResidueEmissions();

        //expectedResult.EmissionsCore[CoreEmissions.DirectN2ON].Value = 1.2323;
        //expectedResult.EmissionsCore[CoreEmissions.DirectNON].Value = 0.1232;
        //expectedResult.EmissionsCore[CoreEmissions.DirectN2N].Value = 3.6969;
        //expectedResult.EmissionsCore[CoreEmissions.LeachedNO3N].Value = 5.2925;
        //expectedResult.EmissionsCore[CoreEmissions.N2ONleached].Value = 0.0397;

        expectedResult.EmissionsCore[CoreEmissions.DirectNH3N].Value = 0.593133583;
        expectedResult.EmissionsCore[CoreEmissions.N2ONvolatalised].Value = 0.0083087;

        expectedResult.AdditionalOutputs[AdditionalGrassResidueEmissions.DryMatterOfftake].Value = 14927.98721;
        expectedResult.AdditionalOutputs[AdditionalGrassResidueEmissions.AboveGroundResidueDryMatter].Value = 2124.38418;
        expectedResult.AdditionalOutputs[AdditionalGrassResidueEmissions.BelowGroundResidueDryMatter].Value = 6038.94374;
        expectedResult.AdditionalOutputs[AdditionalGrassResidueEmissions.NitrogeninGrassHarvested].Value = 328.7301294;
        expectedResult.AdditionalOutputs[AdditionalGrassResidueEmissions.NitrogenfromCloverfixation].Value = 133.0481297;

        var locationId = 219;
        var fertiliserRate = 183;
        var soilTextureType = SoilTextureType.Light_Sand;
        var grassType = GrassType.ImprovedTemporary;
        var grassUseType = GrassUseType.Cut;
        var sownWithClover = true;
        var receivesManagedManure = true;

        dataList.Add(
            new object[]
            {
                    locationId, fertiliserRate, soilTextureType, grassType, grassUseType, sownWithClover, receivesManagedManure, expectedResult
            });

        return dataList;

    }

    [Theory]
    [MemberData(nameof(UnmitigatedProductionGrassResidueEmissions_TestGenerator))]
    public void UnmitigatedGrassResidueEmissions_AllOutputsCorrect(int LocationID, double FertiliserRate, int soiltexturetype, int grassType, int grassUseType, bool sownWithClover, bool receivesManagedManure, GrassResidueEmissions expectedResult)
    {
        GeneticGainScalars geneticGainScalars = GrassGeneticGainScalarFunction.ReturnGeneticGainScalars((GrassType)grassType, FertiliserRate);
        GrassResidueEmissions actualEmissions = GrassProductionEmissions.UnmitigatedGrassProductionEmissions(LocationID, FertiliserRate, soiltexturetype, grassType, grassUseType, sownWithClover, receivesManagedManure, geneticGainScalars);

        actualEmissions.RoundEmissions(3);
        string actualEmissionJson = JsonSerializer.Serialize(actualEmissions, new JsonSerializerOptions
        {
            NumberHandling = JsonNumberHandling.AllowNamedFloatingPointLiterals
        });

        expectedResult.RoundEmissions(3);

        string expectedResultJson = JsonSerializer.Serialize(expectedResult, new JsonSerializerOptions
        {
            NumberHandling = JsonNumberHandling.AllowNamedFloatingPointLiterals
        });

        //Assert.Equal(expectedResultJson, actualEmissionJson);
        Assert.True(double.IsNaN(expectedResult.EmissionsCore[CoreEmissions.DirectN2ON].Value) && double.IsNaN(actualEmissions.EmissionsCore[CoreEmissions.DirectN2ON].Value));
        Assert.True(HelperFunctions.IsWithinPercentage(expectedResult.EmissionsCore[CoreEmissions.DirectNH3N].Value, actualEmissions.EmissionsCore[CoreEmissions.DirectNH3N].Value, 0.1));
        Assert.True(double.IsNaN(expectedResult.EmissionsCore[CoreEmissions.LeachedNO3N].Value) && double.IsNaN(actualEmissions.EmissionsCore[CoreEmissions.LeachedNO3N].Value));
        Assert.True(double.IsNaN(expectedResult.EmissionsCore[CoreEmissions.DirectN2N].Value) && double.IsNaN(actualEmissions.EmissionsCore[CoreEmissions.DirectN2N].Value));
        Assert.True(HelperFunctions.IsWithinPercentage(expectedResult.EmissionsCore[CoreEmissions.N2ONvolatalised].Value, actualEmissions.EmissionsCore[CoreEmissions.N2ONvolatalised].Value, 0.1));
        Assert.True(double.IsNaN(expectedResult.EmissionsCore[CoreEmissions.DirectNON].Value) && double.IsNaN(actualEmissions.EmissionsCore[CoreEmissions.DirectNON].Value));
        Assert.True(double.IsNaN(expectedResult.EmissionsCore[CoreEmissions.N2ONleached].Value) && double.IsNaN(actualEmissions.EmissionsCore[CoreEmissions.N2ONleached].Value));

        Assert.True(HelperFunctions.IsWithinPercentage(expectedResult.AdditionalOutputs[AdditionalGrassResidueEmissions.DryMatterOfftake].Value, actualEmissions.AdditionalOutputs[AdditionalGrassResidueEmissions.DryMatterOfftake].Value, 0.01));
        Assert.True(HelperFunctions.IsWithinPercentage(expectedResult.AdditionalOutputs[AdditionalGrassResidueEmissions.AboveGroundResidueDryMatter].Value, actualEmissions.AdditionalOutputs[AdditionalGrassResidueEmissions.AboveGroundResidueDryMatter].Value, 0.01));
        Assert.True(HelperFunctions.IsWithinPercentage(expectedResult.AdditionalOutputs[AdditionalGrassResidueEmissions.BelowGroundResidueDryMatter].Value, actualEmissions.AdditionalOutputs[AdditionalGrassResidueEmissions.BelowGroundResidueDryMatter].Value, 0.01));
        Assert.True(HelperFunctions.IsWithinPercentage(expectedResult.AdditionalOutputs[AdditionalGrassResidueEmissions.RenewalResidueNitrogen].Value, actualEmissions.AdditionalOutputs[AdditionalGrassResidueEmissions.RenewalResidueNitrogen].Value, 0.01));
        Assert.True(HelperFunctions.IsWithinPercentage(expectedResult.AdditionalOutputs[AdditionalGrassResidueEmissions.GrassFracLeach].Value, actualEmissions.AdditionalOutputs[AdditionalGrassResidueEmissions.GrassFracLeach].Value, 0.01));
    }

    public static IEnumerable<object[]> GeneticGainScalar_TestGenerator()
    {
        var dataList = new List<object[]>();

        GeneticGainScalars expectedScalars = new GeneticGainScalars();
        expectedScalars.GeneticAllFracLeachScalar = 0.9604;
        expectedScalars.GeneticYieldScalar = 1.0935282;
        expectedScalars.GeneticYieldDilutionScalar = 0.9572356;
        expectedScalars.GeneticNitrogenUptakeScalar = 1.0467641;

        dataList.Add(new object[] { GrassType.ImprovedTemporary, 183.00, expectedScalars });

        expectedScalars = new GeneticGainScalars();
        expectedScalars.GeneticAllFracLeachScalar = 0.9802000;
        expectedScalars.GeneticYieldScalar = 1.04821221;
        expectedScalars.GeneticYieldDilutionScalar = 0.98930691;
        expectedScalars.GeneticNitrogenUptakeScalar = 1.02410611;

        dataList.Add(new object[] { GrassType.ImprovedPermanent, 183.00, expectedScalars });

        return dataList;
    }

    [Theory]
    [MemberData(nameof(GeneticGainScalar_TestGenerator))]
    public void GeneticGainScalars_OutputCorrect(GrassType grassType, double fertiliserRate, GeneticGainScalars expectedScalars)
    {
        var actualScalars = new GeneticGainScalars();
        actualScalars = GrassGeneticGainScalarFunction.ReturnGeneticGainScalars(grassType, fertiliserRate);
        string actualEmissionJson = JsonSerializer.Serialize(actualScalars, new JsonSerializerOptions
        {
            NumberHandling = JsonNumberHandling.AllowNamedFloatingPointLiterals
        });

        string expectedResultJson = JsonSerializer.Serialize(expectedScalars, new JsonSerializerOptions
        {
            NumberHandling = JsonNumberHandling.AllowNamedFloatingPointLiterals
        });

        Assert.True(HelperFunctions.IsWithinPercentage(expectedScalars.GeneticYieldScalar, actualScalars.GeneticYieldScalar, 1.0));
        Assert.True(HelperFunctions.IsWithinPercentage(expectedScalars.GeneticYieldScalar, actualScalars.GeneticYieldDilutionScalar, 1.0));
        Assert.True(HelperFunctions.IsWithinPercentage(expectedScalars.GeneticYieldScalar, actualScalars.GeneticAllFracLeachScalar, 1.0));
        Assert.True(HelperFunctions.IsWithinPercentage(expectedScalars.GeneticYieldScalar, actualScalars.GeneticNitrogenUptakeScalar, 1.0));

    }
    public static IEnumerable<object[]> UnmitigatedRenewalGrassResidueEmissions_TestGenerator()
    {
        var dataList = new List<object[]>();

        var expectedResult = new GrassResidueEmissions();
        expectedResult.InitialiseGrassResidueEmissions();

        expectedResult.EmissionsCore[CoreEmissions.DirectN2ON].Value = 1.2899;
        expectedResult.EmissionsCore[CoreEmissions.DirectNON].Value = 0.1290;
        expectedResult.EmissionsCore[CoreEmissions.DirectN2N].Value = 3.8698;
        ;
        expectedResult.EmissionsCore[CoreEmissions.LeachedNO3N].Value = 5.3206;
        expectedResult.EmissionsCore[CoreEmissions.N2ONleached].Value = 0.0399;
        expectedResult.EmissionsCore[CoreEmissions.DirectNH3N].Value = 0.0;
        expectedResult.EmissionsCore[CoreEmissions.N2ONvolatalised].Value = 0.0018059;

        expectedResult.AdditionalOutputs[AdditionalGrassResidueEmissions.RenewalResidueNitrogen].Value = 128.9923;
        expectedResult.AdditionalOutputs[AdditionalGrassResidueEmissions.GrassFracLeach].Value = 4.1248;

        var locationId = 219;
        var fertiliserRate = 183;
        var soilTextureType = SoilTextureType.Light_Sand;
        var grassType = GrassType.ImprovedTemporary;
        var grassUseType = GrassUseType.Cut;
        var sownWithClover = true;
        var receivesManagedManure = true;

        dataList.Add(
            new object[]
            {
                    locationId, fertiliserRate, soilTextureType, grassType, grassUseType, sownWithClover, receivesManagedManure, expectedResult
            });

        return dataList;

    }

    [Theory]
    [MemberData(nameof(UnmitigatedRenewalGrassResidueEmissions_TestGenerator))]
    public void UnmitigatedGrassRenewalEmissions_AllOutputsCorrect(int locationID, double fertiliserrate, int soiltexturetype, int grassType, int grassUseType, bool sownWithClover, bool receivesManagedManure, GrassResidueEmissions expectedResult)
    {
        GeneticGainScalars geneticGainScalars = GrassGeneticGainScalarFunction.ReturnGeneticGainScalars((GrassType)grassType, fertiliserrate);
        GrassResidueEmissions actualEmissions = GrassProductionEmissions.UnmitigatedGrassReseededOrPloughedRenewalEmissions(locationID, fertiliserrate, soiltexturetype, grassType, grassUseType, sownWithClover, receivesManagedManure, geneticGainScalars);

        actualEmissions.RoundEmissions(3);
        string actualEmissionJson = JsonSerializer.Serialize(actualEmissions, new JsonSerializerOptions
        {
            NumberHandling = JsonNumberHandling.AllowNamedFloatingPointLiterals
        });

        expectedResult.RoundEmissions(3);

        string expectedResultJson = JsonSerializer.Serialize(expectedResult, new JsonSerializerOptions
        {
            NumberHandling = JsonNumberHandling.AllowNamedFloatingPointLiterals
        });

        //Assert.Equal(expectedResultJson, actualEmissionJson);
        Assert.True(HelperFunctions.IsWithinPercentage(expectedResult.EmissionsCore[CoreEmissions.DirectN2ON].Value,actualEmissions.EmissionsCore[CoreEmissions.DirectN2ON].Value,0.1));
        Assert.True(HelperFunctions.IsWithinPercentage(expectedResult.EmissionsCore[CoreEmissions.DirectNH3N].Value, actualEmissions.EmissionsCore[CoreEmissions.DirectNH3N].Value, 0.1));
        Assert.True(HelperFunctions.IsWithinPercentage(expectedResult.EmissionsCore[CoreEmissions.LeachedNO3N].Value,actualEmissions.EmissionsCore[CoreEmissions.LeachedNO3N].Value,0.1));
        Assert.True(HelperFunctions.IsWithinPercentage(expectedResult.EmissionsCore[CoreEmissions.DirectN2N].Value, actualEmissions.EmissionsCore[CoreEmissions.DirectN2N].Value, 0.1));
        Assert.True(HelperFunctions.IsWithinPercentage(expectedResult.EmissionsCore[CoreEmissions.N2ONvolatalised].Value, actualEmissions.EmissionsCore[CoreEmissions.N2ONvolatalised].Value, 0.1));
        Assert.True(HelperFunctions.IsWithinPercentage(expectedResult.EmissionsCore[CoreEmissions.DirectNON].Value, actualEmissions.EmissionsCore[CoreEmissions.DirectNON].Value, 0.1));
        Assert.True(HelperFunctions.IsWithinPercentage(expectedResult.EmissionsCore[CoreEmissions.N2ONleached].Value, actualEmissions.EmissionsCore[CoreEmissions.N2ONleached].Value, 0.1));

        Assert.True(HelperFunctions.IsWithinPercentage(expectedResult.AdditionalOutputs[AdditionalGrassResidueEmissions.RenewalResidueNitrogen].Value, actualEmissions.AdditionalOutputs[AdditionalGrassResidueEmissions.RenewalResidueNitrogen].Value, 0.01));
        Assert.True(HelperFunctions.IsWithinPercentage(expectedResult.AdditionalOutputs[AdditionalGrassResidueEmissions.GrassFracLeach].Value, actualEmissions.AdditionalOutputs[AdditionalGrassResidueEmissions.GrassFracLeach].Value, 0.01));
    }
}
