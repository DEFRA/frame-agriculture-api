using FrameAgricultureApi.Libraries.NonGHG;
using System.Text.Json;
using System.Text.Json.Serialization;
using static FrameAgricultureApi.Enumerators.Enumerators;

namespace FrameAgricultureApi.Components.Tests;

public class NonGHGCropGrassTests
{
    public static IEnumerable<object[]> NonGHG_Crop_TestGenerator()
    {
        var dataList = new List<object[]>();

        NonGHGCropGrassEmissions nonGHGCropGrassEmissions = new NonGHGCropGrassEmissions();
        nonGHGCropGrassEmissions.InitialiseNonGHGCropGrassEmissions();

        nonGHGCropGrassEmissions.nonGHGEmissions[NonGHG_Emissions.NMVOC].Value = 0.86;
        nonGHGCropGrassEmissions.nonGHGEmissions[NonGHG_Emissions.PM2_5].Value = 0.2505;
        nonGHGCropGrassEmissions.nonGHGEmissions[NonGHG_Emissions.PM10].Value = 1.78;
        nonGHGCropGrassEmissions.nonGHGEmissions[NonGHG_Emissions.TSP].Value = double.NaN;

        nonGHGCropGrassEmissions.AdditionalOutputs[NonGHGEmissions_AdditionalCropGrass.PM2_5_Cultivation].Value = 0.015;
        nonGHGCropGrassEmissions.AdditionalOutputs[NonGHGEmissions_AdditionalCropGrass.PM2_5_Harvesting].Value = 0.025;
        nonGHGCropGrassEmissions.AdditionalOutputs[NonGHGEmissions_AdditionalCropGrass.PM2_5_Cleaning].Value = 0.0125;
        nonGHGCropGrassEmissions.AdditionalOutputs[NonGHGEmissions_AdditionalCropGrass.PM2_5_Drying].Value = 0.198;

        nonGHGCropGrassEmissions.AdditionalOutputs[NonGHGEmissions_AdditionalCropGrass.PM10_Cultivation].Value = 0.25;
        nonGHGCropGrassEmissions.AdditionalOutputs[NonGHGEmissions_AdditionalCropGrass.PM10_Harvesting].Value = 0.62;
        nonGHGCropGrassEmissions.AdditionalOutputs[NonGHGEmissions_AdditionalCropGrass.PM10_Cleaning].Value = 0.25;
        nonGHGCropGrassEmissions.AdditionalOutputs[NonGHGEmissions_AdditionalCropGrass.PM10_Drying].Value = 0.66;

        dataList.Add(
            new object[] { CropType.Oats, nonGHGCropGrassEmissions }
            );

        nonGHGCropGrassEmissions = new NonGHGCropGrassEmissions();
        nonGHGCropGrassEmissions.InitialiseNonGHGCropGrassEmissions();

        nonGHGCropGrassEmissions.nonGHGEmissions[NonGHG_Emissions.NMVOC].Value = 0.86;
        nonGHGCropGrassEmissions.nonGHGEmissions[NonGHG_Emissions.PM2_5].Value = 0.168;
        nonGHGCropGrassEmissions.nonGHGEmissions[NonGHG_Emissions.PM10].Value = 1.25;
        nonGHGCropGrassEmissions.nonGHGEmissions[NonGHG_Emissions.TSP].Value = double.NaN;

        nonGHGCropGrassEmissions.AdditionalOutputs[NonGHGEmissions_AdditionalCropGrass.PM2_5_Cultivation].Value = 0.015;
        nonGHGCropGrassEmissions.AdditionalOutputs[NonGHGEmissions_AdditionalCropGrass.PM2_5_Harvesting].Value = 0.016;
        nonGHGCropGrassEmissions.AdditionalOutputs[NonGHGEmissions_AdditionalCropGrass.PM2_5_Cleaning].Value = 0.008;
        nonGHGCropGrassEmissions.AdditionalOutputs[NonGHGEmissions_AdditionalCropGrass.PM2_5_Drying].Value = 0.129;

        nonGHGCropGrassEmissions.AdditionalOutputs[NonGHGEmissions_AdditionalCropGrass.PM10_Cultivation].Value = 0.25;
        nonGHGCropGrassEmissions.AdditionalOutputs[NonGHGEmissions_AdditionalCropGrass.PM10_Harvesting].Value = 0.41;
        nonGHGCropGrassEmissions.AdditionalOutputs[NonGHGEmissions_AdditionalCropGrass.PM10_Cleaning].Value = 0.16;
        nonGHGCropGrassEmissions.AdditionalOutputs[NonGHGEmissions_AdditionalCropGrass.PM10_Drying].Value = 0.43;

        dataList.Add(
            new object[] { CropType.Spring_barley, nonGHGCropGrassEmissions }
            );

        nonGHGCropGrassEmissions = new NonGHGCropGrassEmissions();
        nonGHGCropGrassEmissions.InitialiseNonGHGCropGrassEmissions();

        nonGHGCropGrassEmissions.nonGHGEmissions[NonGHG_Emissions.NMVOC].Value = 0.32;
        nonGHGCropGrassEmissions.nonGHGEmissions[NonGHG_Emissions.PM2_5].Value = 0.212;
        nonGHGCropGrassEmissions.nonGHGEmissions[NonGHG_Emissions.PM10].Value = 1.49;
        nonGHGCropGrassEmissions.nonGHGEmissions[NonGHG_Emissions.TSP].Value = double.NaN;

        nonGHGCropGrassEmissions.AdditionalOutputs[NonGHGEmissions_AdditionalCropGrass.PM2_5_Cultivation].Value = 0.015;
        nonGHGCropGrassEmissions.AdditionalOutputs[NonGHGEmissions_AdditionalCropGrass.PM2_5_Harvesting].Value = 0.02;
        nonGHGCropGrassEmissions.AdditionalOutputs[NonGHGEmissions_AdditionalCropGrass.PM2_5_Cleaning].Value = 0.009;
        nonGHGCropGrassEmissions.AdditionalOutputs[NonGHGEmissions_AdditionalCropGrass.PM2_5_Drying].Value = 0.168;

        nonGHGCropGrassEmissions.AdditionalOutputs[NonGHGEmissions_AdditionalCropGrass.PM10_Cultivation].Value = 0.25;
        nonGHGCropGrassEmissions.AdditionalOutputs[NonGHGEmissions_AdditionalCropGrass.PM10_Harvesting].Value = 0.49;
        nonGHGCropGrassEmissions.AdditionalOutputs[NonGHGEmissions_AdditionalCropGrass.PM10_Cleaning].Value = 0.19;
        nonGHGCropGrassEmissions.AdditionalOutputs[NonGHGEmissions_AdditionalCropGrass.PM10_Drying].Value = 0.56;

        dataList.Add(
            new object[] { CropType.Wheat, nonGHGCropGrassEmissions }
            );

        nonGHGCropGrassEmissions = new NonGHGCropGrassEmissions();
        nonGHGCropGrassEmissions.InitialiseNonGHGCropGrassEmissions();

        nonGHGCropGrassEmissions.nonGHGEmissions[NonGHG_Emissions.NMVOC].Value = 1.03;
        nonGHGCropGrassEmissions.nonGHGEmissions[NonGHG_Emissions.PM2_5].Value = 0.149;
        nonGHGCropGrassEmissions.nonGHGEmissions[NonGHG_Emissions.PM10].Value = 1.15;
        nonGHGCropGrassEmissions.nonGHGEmissions[NonGHG_Emissions.TSP].Value = double.NaN;

        nonGHGCropGrassEmissions.AdditionalOutputs[NonGHGEmissions_AdditionalCropGrass.PM2_5_Cultivation].Value = 0.015;
        nonGHGCropGrassEmissions.AdditionalOutputs[NonGHGEmissions_AdditionalCropGrass.PM2_5_Harvesting].Value = 0.015;
        nonGHGCropGrassEmissions.AdditionalOutputs[NonGHGEmissions_AdditionalCropGrass.PM2_5_Cleaning].Value = 0.008;
        nonGHGCropGrassEmissions.AdditionalOutputs[NonGHGEmissions_AdditionalCropGrass.PM2_5_Drying].Value = 0.111;

        nonGHGCropGrassEmissions.AdditionalOutputs[NonGHGEmissions_AdditionalCropGrass.PM10_Cultivation].Value = 0.25;
        nonGHGCropGrassEmissions.AdditionalOutputs[NonGHGEmissions_AdditionalCropGrass.PM10_Harvesting].Value = 0.37;
        nonGHGCropGrassEmissions.AdditionalOutputs[NonGHGEmissions_AdditionalCropGrass.PM10_Cleaning].Value = 0.16;
        nonGHGCropGrassEmissions.AdditionalOutputs[NonGHGEmissions_AdditionalCropGrass.PM10_Drying].Value = 0.37;

        dataList.Add(
            new object[] { CropType.Minor_cereals, nonGHGCropGrassEmissions }
            );

        nonGHGCropGrassEmissions = new NonGHGCropGrassEmissions();
        nonGHGCropGrassEmissions.InitialiseNonGHGCropGrassEmissions();

        nonGHGCropGrassEmissions.nonGHGEmissions[NonGHG_Emissions.NMVOC].Value = 1.34;
        nonGHGCropGrassEmissions.nonGHGEmissions[NonGHG_Emissions.PM2_5].Value = 0.015;
        nonGHGCropGrassEmissions.nonGHGEmissions[NonGHG_Emissions.PM10].Value = 0.25;
        nonGHGCropGrassEmissions.nonGHGEmissions[NonGHG_Emissions.TSP].Value = double.NaN;

        nonGHGCropGrassEmissions.AdditionalOutputs[NonGHGEmissions_AdditionalCropGrass.PM2_5_Cultivation].Value = 0.015;
        nonGHGCropGrassEmissions.AdditionalOutputs[NonGHGEmissions_AdditionalCropGrass.PM2_5_Harvesting].Value = 0.0;
        nonGHGCropGrassEmissions.AdditionalOutputs[NonGHGEmissions_AdditionalCropGrass.PM2_5_Cleaning].Value = 0.0;
        nonGHGCropGrassEmissions.AdditionalOutputs[NonGHGEmissions_AdditionalCropGrass.PM2_5_Drying].Value = 0.0;

        nonGHGCropGrassEmissions.AdditionalOutputs[NonGHGEmissions_AdditionalCropGrass.PM10_Cultivation].Value = 0.25;
        nonGHGCropGrassEmissions.AdditionalOutputs[NonGHGEmissions_AdditionalCropGrass.PM10_Harvesting].Value = 0.0;
        nonGHGCropGrassEmissions.AdditionalOutputs[NonGHGEmissions_AdditionalCropGrass.PM10_Cleaning].Value = 0.0;
        nonGHGCropGrassEmissions.AdditionalOutputs[NonGHGEmissions_AdditionalCropGrass.PM10_Drying].Value = 0.0;

        dataList.Add(
            new object[] { CropType.Oilseed_rape, nonGHGCropGrassEmissions }
            );

        nonGHGCropGrassEmissions = new NonGHGCropGrassEmissions();
        nonGHGCropGrassEmissions.InitialiseNonGHGCropGrassEmissions();

        nonGHGCropGrassEmissions.nonGHGEmissions[NonGHG_Emissions.NMVOC].Value = 0.86;
        nonGHGCropGrassEmissions.nonGHGEmissions[NonGHG_Emissions.PM2_5].Value = 0.015;
        nonGHGCropGrassEmissions.nonGHGEmissions[NonGHG_Emissions.PM10].Value = 0.25;
        nonGHGCropGrassEmissions.nonGHGEmissions[NonGHG_Emissions.TSP].Value = double.NaN;

        nonGHGCropGrassEmissions.AdditionalOutputs[NonGHGEmissions_AdditionalCropGrass.PM2_5_Cultivation].Value = 0.015;
        nonGHGCropGrassEmissions.AdditionalOutputs[NonGHGEmissions_AdditionalCropGrass.PM2_5_Harvesting].Value = 0.0;
        nonGHGCropGrassEmissions.AdditionalOutputs[NonGHGEmissions_AdditionalCropGrass.PM2_5_Cleaning].Value = 0.0;
        nonGHGCropGrassEmissions.AdditionalOutputs[NonGHGEmissions_AdditionalCropGrass.PM2_5_Drying].Value = 0.0;

        nonGHGCropGrassEmissions.AdditionalOutputs[NonGHGEmissions_AdditionalCropGrass.PM10_Cultivation].Value = 0.25;
        nonGHGCropGrassEmissions.AdditionalOutputs[NonGHGEmissions_AdditionalCropGrass.PM10_Harvesting].Value = 0.0;
        nonGHGCropGrassEmissions.AdditionalOutputs[NonGHGEmissions_AdditionalCropGrass.PM10_Cleaning].Value = 0.0;
        nonGHGCropGrassEmissions.AdditionalOutputs[NonGHGEmissions_AdditionalCropGrass.PM10_Drying].Value = 0.0;

        dataList.Add(
            new object[] { CropType.Linseed_and_Flax, nonGHGCropGrassEmissions }
            );

        return dataList;
    }

    [Theory]
    [MemberData(nameof(NonGHG_Crop_TestGenerator))]
    public void NonGHG_Crop_AllOutputsCorrect(CropType cropType, NonGHGCropGrassEmissions expectedResult)
    {
        NonGHGCropGrassEmissions actualEmissions = NonGHGCropGrassLivestock.NonGHG_Crop(cropType);

        foreach(NonGHG_Emissions key in actualEmissions.nonGHGEmissions.Keys)
        {
            if(!double.IsNaN(actualEmissions.nonGHGEmissions[key].Value))
            {
                actualEmissions.nonGHGEmissions[key].Value = Math.Round(actualEmissions.nonGHGEmissions[key].Value, 4);
            }
        }

        string actualEmissionsJson = JsonSerializer.Serialize(actualEmissions, new JsonSerializerOptions
        {
            NumberHandling = JsonNumberHandling.AllowNamedFloatingPointLiterals,
            IncludeFields = true
        });
        string expectedResultJson = JsonSerializer.Serialize(expectedResult, new JsonSerializerOptions
        {
            NumberHandling = JsonNumberHandling.AllowNamedFloatingPointLiterals,
            IncludeFields = true
        });

        Assert.Equal(expectedResultJson, actualEmissionsJson);
    }

    public static IEnumerable<object[]> NonGHG_Grass_TestGenerator()
    {
        List<object[]> dataList = new();

        GrassType grassType = GrassType.ImprovedTemporary;

        NonGHGCropGrassEmissions expectedOutput = new();
        expectedOutput.InitialiseNonGHGCropGrassEmissions();

        expectedOutput.nonGHGEmissions[NonGHG_Emissions.NMVOC].Value = 0.4100;
        expectedOutput.nonGHGEmissions[NonGHG_Emissions.PM2_5].Value = 0.0250;
        expectedOutput.nonGHGEmissions[NonGHG_Emissions.PM10].Value = 0.5000;

        dataList.Add(new object[]
        {
            grassType, expectedOutput
        });

        grassType = GrassType.ImprovedPermanent;

        expectedOutput = new();
        expectedOutput.InitialiseNonGHGCropGrassEmissions();

        expectedOutput.nonGHGEmissions[NonGHG_Emissions.NMVOC].Value = 0.4100;
        expectedOutput.nonGHGEmissions[NonGHG_Emissions.PM2_5].Value = 0.0250;
        expectedOutput.nonGHGEmissions[NonGHG_Emissions.PM10].Value = 0.5000;

        dataList.Add(new object[]
        {
            grassType, expectedOutput
        });

        grassType = GrassType.UnimprovedSoleRights;

        expectedOutput = new();
        expectedOutput.InitialiseNonGHGCropGrassEmissions();

        expectedOutput.nonGHGEmissions[NonGHG_Emissions.NMVOC].Value = 0.4100;
        expectedOutput.nonGHGEmissions[NonGHG_Emissions.PM2_5].Value = 0.0100;
        expectedOutput.nonGHGEmissions[NonGHG_Emissions.PM10].Value = 0.2500;

        dataList.Add(new object[]
        {
            grassType, expectedOutput
        });

        grassType = GrassType.UnimprovedCommon;

        expectedOutput = new();
        expectedOutput.InitialiseNonGHGCropGrassEmissions();

        expectedOutput.nonGHGEmissions[NonGHG_Emissions.NMVOC].Value = 0.4100;
        expectedOutput.nonGHGEmissions[NonGHG_Emissions.PM2_5].Value = 0.0100;
        expectedOutput.nonGHGEmissions[NonGHG_Emissions.PM10].Value = 0.2500;

        dataList.Add(new object[]
        {
            grassType, expectedOutput
        });

        return dataList;

    }

    [Theory]
    [MemberData(nameof(NonGHG_Grass_TestGenerator))]
    public void NonGHG_Grass_AllOutputsCorrect(GrassType grassType, NonGHGCropGrassEmissions expectedResult)
    {
        NonGHGCropGrassEmissions actualEmissions = NonGHGCropGrassLivestock.NonGHG_Grass(grassType);

        foreach(NonGHG_Emissions key in actualEmissions.nonGHGEmissions.Keys)
        {
            if(!double.IsNaN(actualEmissions.nonGHGEmissions[key].Value))
            {
                actualEmissions.nonGHGEmissions[key].Value = Math.Round(actualEmissions.nonGHGEmissions[key].Value, 4);
            }
        }

        string actualEmissionsJson = JsonSerializer.Serialize(actualEmissions, new JsonSerializerOptions
        {
            NumberHandling = JsonNumberHandling.AllowNamedFloatingPointLiterals,
            IncludeFields = true
        });
        string expectedResultJson = JsonSerializer.Serialize(expectedResult, new JsonSerializerOptions
        {
            NumberHandling = JsonNumberHandling.AllowNamedFloatingPointLiterals,
            IncludeFields = true
        });

        Assert.Equal(expectedResultJson, actualEmissionsJson);
    }

    public static IEnumerable<object[]> NonGHG_DairyBeef_TestGenerator()
    {
        List<object[]> dataList = new();

        var sector = Sector.Beef;
        var animalType = (int)BeefCattleType.Heifersforbreeding;
        var monthlyGrossEnergyIntake = (230.0 * 365.0) / 12.0;
        var percentTimeHousingYards = 50.0;
        var housingAmmonia = 150.0;
        var storageAmmonia = 175.0;
        var spreadingAmmonia = 200.0;

        NonGHGLivestockEmissions expectedResult = new();
        expectedResult.InitialiseNonGHGLivestockEmissions();

        expectedResult.AdditionalOutputs[NonGHGEmissions_AdditionalLivestock.NMVOC_Grazing].Value = 0.024135625;
        expectedResult.AdditionalOutputs[NonGHGEmissions_AdditionalLivestock.NMVOC_Housing].Value = 0.123476458;
        expectedResult.AdditionalOutputs[NonGHGEmissions_AdditionalLivestock.NMVOC_SilageFeed].Value = 0.350141458;
        expectedResult.AdditionalOutputs[NonGHGEmissions_AdditionalLivestock.NMVOC_SilageStore].Value = 0.087535365;
        expectedResult.AdditionalOutputs[NonGHGEmissions_AdditionalLivestock.NMVOC_Storage].Value = 0.144055868;
        expectedResult.AdditionalOutputs[NonGHGEmissions_AdditionalLivestock.NMVOC_Spreading].Value = 0.164635278;

        expectedResult.nonGHGEmissions[NonGHG_Emissions.NMVOC].Value = 0.024135625 + 0.123476458 + 0.350141458 + 0.087535365
            + 0.144055868 + 0.164635278;

        expectedResult.nonGHGEmissions[NonGHG_Emissions.PM2_5].Value = 0.0075;
        expectedResult.nonGHGEmissions[NonGHG_Emissions.PM10].Value = 0.01125;
        expectedResult.nonGHGEmissions[NonGHG_Emissions.TSP].Value = 0.024583;

        dataList.Add(new object[]
        {
           sector, animalType, monthlyGrossEnergyIntake, percentTimeHousingYards, housingAmmonia, storageAmmonia, spreadingAmmonia, expectedResult
        });

        sector = Sector.Beef;
        animalType = (int)BeefCattleType.Cows;
        monthlyGrossEnergyIntake = (230.0 * 365.0) / 12.0;
        percentTimeHousingYards = 50.0;
        housingAmmonia = 150.0;
        storageAmmonia = 175.0;
        spreadingAmmonia = 200.0;

        expectedResult = new();
        expectedResult.InitialiseNonGHGLivestockEmissions();

        expectedResult.AdditionalOutputs[NonGHGEmissions_AdditionalLivestock.NMVOC_Grazing].Value = 0.024135625;
        expectedResult.AdditionalOutputs[NonGHGEmissions_AdditionalLivestock.NMVOC_Housing].Value = 0.123476458;
        expectedResult.AdditionalOutputs[NonGHGEmissions_AdditionalLivestock.NMVOC_SilageFeed].Value = 0.350141458;
        expectedResult.AdditionalOutputs[NonGHGEmissions_AdditionalLivestock.NMVOC_SilageStore].Value = 0.087535365;
        expectedResult.AdditionalOutputs[NonGHGEmissions_AdditionalLivestock.NMVOC_Storage].Value = 0.144055868;
        expectedResult.AdditionalOutputs[NonGHGEmissions_AdditionalLivestock.NMVOC_Spreading].Value = 0.164635278;

        expectedResult.nonGHGEmissions[NonGHG_Emissions.NMVOC].Value = 0.024135625 + 0.123476458 + 0.350141458 + 0.087535365
            + 0.144055868 + 0.164635278;

        expectedResult.nonGHGEmissions[NonGHG_Emissions.PM2_5].Value = 0.0075;
        expectedResult.nonGHGEmissions[NonGHG_Emissions.PM10].Value = 0.01125;
        expectedResult.nonGHGEmissions[NonGHG_Emissions.TSP].Value = 0.024583;

        dataList.Add(new object[]
        {
           sector, animalType, monthlyGrossEnergyIntake, percentTimeHousingYards, housingAmmonia, storageAmmonia, spreadingAmmonia, expectedResult
        });

        return dataList;
    }

    [Theory]
    [MemberData(nameof(NonGHG_DairyBeef_TestGenerator))]
    public void NonGHG_DairyBeef_AllOutputsCorrect(Sector sector, int animalType, double MonthlyGrossEnergyIntake, double PercentTimeHousingYards, double housingAmmonia, double storageAmmonia, double spreadingAmmonia, NonGHGLivestockEmissions expectedResult)
    {
        NonGHGLivestockEmissions actualEmissions = NonGHGCropGrassLivestock.NonGHG_DairyBeef(sector, animalType, MonthlyGrossEnergyIntake, PercentTimeHousingYards, housingAmmonia, storageAmmonia, spreadingAmmonia);

        actualEmissions.RoundEmissions(4);

        string actualEmissionsJson = JsonSerializer.Serialize(actualEmissions, new JsonSerializerOptions
        {
            NumberHandling = JsonNumberHandling.AllowNamedFloatingPointLiterals,
            IncludeFields = true
        });

        expectedResult.RoundEmissions(4);

        string expectedResultJson = JsonSerializer.Serialize(expectedResult, new JsonSerializerOptions
        {
            NumberHandling = JsonNumberHandling.AllowNamedFloatingPointLiterals,
            IncludeFields = true
        });

        //            Assert.Equal(expectedResultJson, actualEmissionsJson);

        Assert.True(HelperFunctions.IsWithinPercentage(expectedResult.nonGHGEmissions[NonGHG_Emissions.NMVOC].Value, actualEmissions.nonGHGEmissions[NonGHG_Emissions.NMVOC].Value, 0.1));
        Assert.True(HelperFunctions.IsWithinPercentage(expectedResult.nonGHGEmissions[NonGHG_Emissions.PM2_5].Value, actualEmissions.nonGHGEmissions[NonGHG_Emissions.PM2_5].Value, 0.1));
        Assert.True(HelperFunctions.IsWithinPercentage(expectedResult.nonGHGEmissions[NonGHG_Emissions.PM10].Value, actualEmissions.nonGHGEmissions[NonGHG_Emissions.PM10].Value, 0.1));
        Assert.True(HelperFunctions.IsWithinPercentage(expectedResult.nonGHGEmissions[NonGHG_Emissions.TSP].Value, actualEmissions.nonGHGEmissions[NonGHG_Emissions.TSP].Value, 0.1));

        Assert.True(HelperFunctions.IsWithinPercentage(expectedResult.AdditionalOutputs[NonGHGEmissions_AdditionalLivestock.NMVOC_Grazing].Value, actualEmissions.AdditionalOutputs[NonGHGEmissions_AdditionalLivestock.NMVOC_Grazing].Value, 0.01));
        Assert.True(HelperFunctions.IsWithinPercentage(expectedResult.AdditionalOutputs[NonGHGEmissions_AdditionalLivestock.NMVOC_Housing].Value, actualEmissions.AdditionalOutputs[NonGHGEmissions_AdditionalLivestock.NMVOC_Housing].Value, 0.01));
        Assert.True(HelperFunctions.IsWithinPercentage(expectedResult.AdditionalOutputs[NonGHGEmissions_AdditionalLivestock.NMVOC_Storage].Value, actualEmissions.AdditionalOutputs[NonGHGEmissions_AdditionalLivestock.NMVOC_Storage].Value, 0.01));
        Assert.True(HelperFunctions.IsWithinPercentage(expectedResult.AdditionalOutputs[NonGHGEmissions_AdditionalLivestock.NMVOC_Spreading].Value, actualEmissions.AdditionalOutputs[NonGHGEmissions_AdditionalLivestock.NMVOC_Spreading].Value, 0.01));
        Assert.True(HelperFunctions.IsWithinPercentage(expectedResult.AdditionalOutputs[NonGHGEmissions_AdditionalLivestock.NMVOC_SilageFeed].Value, actualEmissions.AdditionalOutputs[NonGHGEmissions_AdditionalLivestock.NMVOC_SilageFeed].Value, 0.01));
        Assert.True(HelperFunctions.IsWithinPercentage(expectedResult.AdditionalOutputs[NonGHGEmissions_AdditionalLivestock.NMVOC_SilageStore].Value, actualEmissions.AdditionalOutputs[NonGHGEmissions_AdditionalLivestock.NMVOC_SilageStore].Value, 0.01));
    }

    public static IEnumerable<object[]> NonGHG_Livestock_notCattle_TestGenerator()
    {
        List<object[]> dataList = new();

        var sector = Sector.Pigs;
        var animalType = (int)PigType.Sows;
        var volatileSolids = 100.0;
        //Todo this isn't in the spread sheet?
        var HouseDay = 365.0;
        var housingAmmonia = 50.0;
        var storageAmmonia = 25.0;
        var spreadingAmmonia = 75.0;

        NonGHGLivestockEmissions expectedResult = new();
        expectedResult.InitialiseNonGHGLivestockEmissions();

        expectedResult.AdditionalOutputs[NonGHGEmissions_AdditionalLivestock.NMVOC_Grazing].Value = 0.0;
        expectedResult.AdditionalOutputs[NonGHGEmissions_AdditionalLivestock.NMVOC_Housing].Value = 0.7042;
        expectedResult.AdditionalOutputs[NonGHGEmissions_AdditionalLivestock.NMVOC_SilageFeed].Value = 0.0;
        expectedResult.AdditionalOutputs[NonGHGEmissions_AdditionalLivestock.NMVOC_SilageStore].Value = 0.0;
        expectedResult.AdditionalOutputs[NonGHGEmissions_AdditionalLivestock.NMVOC_Storage].Value = 0.3521;
        expectedResult.AdditionalOutputs[NonGHGEmissions_AdditionalLivestock.NMVOC_Spreading].Value = 1.0563;

        expectedResult.nonGHGEmissions[NonGHG_Emissions.NMVOC].Value = 2.1126;

        expectedResult.nonGHGEmissions[NonGHG_Emissions.PM2_5].Value = 0.01;
        expectedResult.nonGHGEmissions[NonGHG_Emissions.PM10].Value = 0.17;
        expectedResult.nonGHGEmissions[NonGHG_Emissions.TSP].Value = 0.62;

        dataList.Add(new object[]
        {
            sector, animalType, volatileSolids, HouseDay, housingAmmonia, storageAmmonia, spreadingAmmonia, expectedResult
        });

        sector = Sector.MinorLivestock;
        animalType = (int)MinorLivestockType.Goats;
        volatileSolids = 100;

        HouseDay = 180;
        housingAmmonia = 50;
        storageAmmonia = 25;
        spreadingAmmonia = 75;

        expectedResult = new();
        expectedResult.InitialiseNonGHGLivestockEmissions();

        expectedResult.AdditionalOutputs[NonGHGEmissions_AdditionalLivestock.NMVOC_Grazing].Value = 0.0011911;
        expectedResult.AdditionalOutputs[NonGHGEmissions_AdditionalLivestock.NMVOC_Housing].Value = 0.079594521;
        expectedResult.AdditionalOutputs[NonGHGEmissions_AdditionalLivestock.NMVOC_SilageFeed].Value = 0.265315068;
        expectedResult.AdditionalOutputs[NonGHGEmissions_AdditionalLivestock.NMVOC_SilageStore].Value = 0.066328767;
        expectedResult.AdditionalOutputs[NonGHGEmissions_AdditionalLivestock.NMVOC_Storage].Value = 0.03979726;
        expectedResult.AdditionalOutputs[NonGHGEmissions_AdditionalLivestock.NMVOC_Spreading].Value = 0.119391781;

        expectedResult.nonGHGEmissions[NonGHG_Emissions.NMVOC].Value = 0.5716184932;

        expectedResult.nonGHGEmissions[NonGHG_Emissions.PM2_5].Value = 0.009863014;
        expectedResult.nonGHGEmissions[NonGHG_Emissions.PM10].Value = 0.029589041;
        expectedResult.nonGHGEmissions[NonGHG_Emissions.TSP].Value = 0.069041096;

        dataList.Add(new object[]
        {
            sector, animalType, volatileSolids, HouseDay, housingAmmonia, storageAmmonia, spreadingAmmonia, expectedResult
        });

        return dataList;

    }

    [Theory]
    [MemberData(nameof(NonGHG_Livestock_notCattle_TestGenerator))]
    public static void NonGHG_Livestock_notCattle_AllOutputsCorrect(Sector sector, int animalType, double VolatileSolids, double HouseDays, double housingAmmonia, double storageAmmonia, double spreadingAmmonia, NonGHGLivestockEmissions expectedResult)
    {
        NonGHGLivestockEmissions actualEmissions = NonGHGCropGrassLivestock.NonGHG_Livestock_notCattle(sector, animalType, VolatileSolids, HouseDays, housingAmmonia, storageAmmonia, spreadingAmmonia);

        actualEmissions.RoundEmissions(4);

        string actualEmissionsJson = JsonSerializer.Serialize(actualEmissions, new JsonSerializerOptions
        {
            NumberHandling = JsonNumberHandling.AllowNamedFloatingPointLiterals,
            IncludeFields = true
        });

        expectedResult.RoundEmissions(4);

        string expectedResultJson = JsonSerializer.Serialize(expectedResult, new JsonSerializerOptions
        {
            NumberHandling = JsonNumberHandling.AllowNamedFloatingPointLiterals,
            IncludeFields = true
        });

        Assert.Equal(expectedResultJson, actualEmissionsJson);
    }
}
