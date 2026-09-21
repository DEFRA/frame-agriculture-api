using FrameAgricultureApi.Libraries.LookUps;
using FrameAgricultureApi.Libraries.ManureMassVolume;
using System.Text.Json;
using System.Text.Json.Serialization;
using static FrameAgricultureApi.Enumerators.Enumerators;

namespace FrameAgricultureApi.Components.Tests;

public class ManureMassandVolumeTests
{

    #region Pigs, poultry and minor livestock
    public static IEnumerable<object[]> ManureMassVolume_PigPoultryMinorLivestock_Outdoor_TestGenerator()
    {
        var dataList = new List<object[]>();

        var expectedResult = new ManureMassVolumeOutput();
        expectedResult.InitialiseManureMassVolumeOutput();

        var sector = Sector.Poultry;
        var animalType = (int)PoultryType.GrowingPullets;
        var organicMatterType = OrganicMatterType.PoultryManureDigestate;
        var percentExcretionOutdoors = 30;

        expectedResult.ManureMassVolume[ManureMassVolumeOutputs.ExcretaVolumeIn].Value = 11.67363704;
        expectedResult.ManureMassVolume[ManureMassVolumeOutputs.ExcretaMassIn].Value = 12.02384615;
        expectedResult.ManureMassVolume[ManureMassVolumeOutputs.ManureVolumeOut].Value = 5.002987304;
        expectedResult.ManureMassVolume[ManureMassVolumeOutputs.ManureMassOut].Value = 5.153076923;

        dataList.Add(new object[]
        {
            sector, animalType, organicMatterType, percentExcretionOutdoors, expectedResult
        });

        expectedResult = new ManureMassVolumeOutput();
        expectedResult.InitialiseManureMassVolumeOutput();

        sector = Sector.Pigs;
        animalType = (int)PigType.Sows;
        organicMatterType = OrganicMatterType.PigFYM;
        percentExcretionOutdoors = 100;

        expectedResult.ManureMassVolume[ManureMassVolumeOutputs.ExcretaVolumeIn].Value = 3458.0005;
        expectedResult.ManureMassVolume[ManureMassVolumeOutputs.ExcretaMassIn].Value = 3402.327594;
        expectedResult.ManureMassVolume[ManureMassVolumeOutputs.ManureVolumeOut].Value = 3458.0005;
        expectedResult.ManureMassVolume[ManureMassVolumeOutputs.ManureMassOut].Value = 3402.327594;

        dataList.Add(new object[]
        {
            sector, animalType, organicMatterType, percentExcretionOutdoors, expectedResult
        });

        expectedResult = new ManureMassVolumeOutput();
        expectedResult.InitialiseManureMassVolumeOutput();

        sector = Sector.Pigs;
        animalType = (int)PigType.Gilts;
        organicMatterType = OrganicMatterType.PigFYM;
        percentExcretionOutdoors = 100;

        expectedResult.ManureMassVolume[ManureMassVolumeOutputs.ExcretaVolumeIn].Value = 1833.716624;
        expectedResult.ManureMassVolume[ManureMassVolumeOutputs.ExcretaMassIn].Value = 1804.194265;
        expectedResult.ManureMassVolume[ManureMassVolumeOutputs.ManureVolumeOut].Value = 1833.716624;
        expectedResult.ManureMassVolume[ManureMassVolumeOutputs.ManureMassOut].Value = 1804.194265;

        dataList.Add(new object[]
        {
            sector, animalType, organicMatterType, percentExcretionOutdoors, expectedResult
        });

        expectedResult = new ManureMassVolumeOutput();
        expectedResult.InitialiseManureMassVolumeOutput();

        sector = Sector.MinorLivestock;
        animalType = (int)MinorLivestockType.Goats;
        organicMatterType = OrganicMatterType.MinorLivestockFYM;
        percentExcretionOutdoors = 100;

        expectedResult.ManureMassVolume[ManureMassVolumeOutputs.ExcretaVolumeIn].Value = 2795.754;
        expectedResult.ManureMassVolume[ManureMassVolumeOutputs.ExcretaMassIn].Value = 2832.0715;
        expectedResult.ManureMassVolume[ManureMassVolumeOutputs.ManureVolumeOut].Value = 2795.754;
        expectedResult.ManureMassVolume[ManureMassVolumeOutputs.ManureMassOut].Value = 2832.0715;

        dataList.Add(new object[]
        {
            sector, animalType, organicMatterType, percentExcretionOutdoors, expectedResult
        });

        return dataList;

    }

    [Theory]
    [MemberData(nameof(ManureMassVolume_PigPoultryMinorLivestock_Outdoor_TestGenerator))]
    public void ManureMassVolume_PigPoultryMinorLivestock_Outdoor_AllOutputsCorrect(Sector sector, int animaltype, OrganicMatterType organicMatterType, double percentExcretionOutdoors, ManureMassVolumeOutput expectedResult)
    {
        ManureMassVolumeOutput actualEmissions = ManureMassandVolume.ManureMassVolume_PigPoultryMinorLivestock_Outdoor(sector, animaltype, organicMatterType, percentExcretionOutdoors);

        actualEmissions.RoundEmissions(4);
        string actualEmissionJson = JsonSerializer.Serialize(actualEmissions, new JsonSerializerOptions
        {
            NumberHandling = JsonNumberHandling.AllowNamedFloatingPointLiterals
        });
        expectedResult.RoundEmissions(4);
        string expectedResultJson = JsonSerializer.Serialize(expectedResult, new JsonSerializerOptions
        {
            NumberHandling = JsonNumberHandling.AllowNamedFloatingPointLiterals
        });

        Assert.Equal(expectedResultJson, actualEmissionJson);
    }

    public static IEnumerable<object[]> ManureMassVolume_PigPoultryMinorLivestock_Housing_TestGenerator()
    {
        var dataList = new List<object[]>();

        var sector = Sector.MinorLivestock;
        var animalType = (int)MinorLivestockType.Goats;
        var organicMatterType = OrganicMatterType.MinorLivestockFYM;
        var percentExcretionIndoors = 100;

        var expectedResult = new ManureMassVolumeOutput();
        expectedResult.InitialiseManureMassVolumeOutput();

        expectedResult.ManureMassVolume[ManureMassVolumeOutputs.ExcretaVolumeIn].Value = 1743.532;
        expectedResult.ManureMassVolume[ManureMassVolumeOutputs.ExcretaMassIn].Value = 1729.224;
        expectedResult.ManureMassVolume[ManureMassVolumeOutputs.ManureVolumeOut].Value = 3110.7855;
        expectedResult.ManureMassVolume[ManureMassVolumeOutputs.ManureMassOut].Value = 1978.081;

        dataList.Add(new object[] {
            sector, animalType, organicMatterType, percentExcretionIndoors, expectedResult
        });

        sector = Sector.MinorLivestock;
        animalType = (int)MinorLivestockType.Deer;
        organicMatterType = OrganicMatterType.MinorLivestockFYM;
        percentExcretionIndoors = 100;

        expectedResult = new ManureMassVolumeOutput();
        expectedResult.InitialiseManureMassVolumeOutput();

        expectedResult.ManureMassVolume[ManureMassVolumeOutputs.ExcretaVolumeIn].Value = 1098.9785;
        expectedResult.ManureMassVolume[ManureMassVolumeOutputs.ExcretaMassIn].Value = 1029.154;
        expectedResult.ManureMassVolume[ManureMassVolumeOutputs.ManureVolumeOut].Value = 2249.349;
        expectedResult.ManureMassVolume[ManureMassVolumeOutputs.ManureMassOut].Value = 1209.099;

        dataList.Add(new object[] {
            sector, animalType, organicMatterType, percentExcretionIndoors, expectedResult
        });

        sector = Sector.Poultry;
        animalType = (int)PoultryType.GrowingPullets;
        organicMatterType = OrganicMatterType.PoultryLitter;
        percentExcretionIndoors = 70;

        expectedResult = new ManureMassVolumeOutput();
        expectedResult.InitialiseManureMassVolumeOutput();

        expectedResult.ManureMassVolume[ManureMassVolumeOutputs.ExcretaVolumeIn].Value = 11.67363704;
        expectedResult.ManureMassVolume[ManureMassVolumeOutputs.ExcretaMassIn].Value = 12.02384615;
        expectedResult.ManureMassVolume[ManureMassVolumeOutputs.ManureVolumeOut].Value = 8.943070245;
        expectedResult.ManureMassVolume[ManureMassVolumeOutputs.ManureMassOut].Value = 6.527099819;

        dataList.Add(new object[] {
            sector, animalType, organicMatterType, percentExcretionIndoors, expectedResult
        });

        sector = Sector.Poultry;
        animalType = (int)PoultryType.LayingHens;
        organicMatterType = OrganicMatterType.PoultryLayerManure;
        percentExcretionIndoors = 90;

        expectedResult = new ManureMassVolumeOutput();
        expectedResult.InitialiseManureMassVolumeOutput();

        expectedResult.ManureMassVolume[ManureMassVolumeOutputs.ExcretaVolumeIn].Value = 32.25952203;
        expectedResult.ManureMassVolume[ManureMassVolumeOutputs.ExcretaMassIn].Value = 33.22730769;
        expectedResult.ManureMassVolume[ManureMassVolumeOutputs.ManureVolumeOut].Value = 23.41854161;
        expectedResult.ManureMassVolume[ManureMassVolumeOutputs.ManureMassOut].Value = 24.68314287;

        dataList.Add(new object[] {
            sector, animalType, organicMatterType, percentExcretionIndoors, expectedResult
        });

        sector = Sector.Pigs;
        animalType = (int)PigType.Sows;
        organicMatterType = OrganicMatterType.PigSlurry;
        percentExcretionIndoors = 100;

        expectedResult = new ManureMassVolumeOutput();
        expectedResult.InitialiseManureMassVolumeOutput();

        expectedResult.ManureMassVolume[ManureMassVolumeOutputs.ExcretaVolumeIn].Value = 3152.174031;
        expectedResult.ManureMassVolume[ManureMassVolumeOutputs.ExcretaMassIn].Value = 3101.424852;
        expectedResult.ManureMassVolume[ManureMassVolumeOutputs.ManureVolumeOut].Value = 3152.174031;
        expectedResult.ManureMassVolume[ManureMassVolumeOutputs.ManureMassOut].Value = 3101.424852;

        dataList.Add(new object[] {
            sector, animalType, organicMatterType, percentExcretionIndoors, expectedResult
        });

        return dataList;
    }

    [Theory]
    [MemberData(nameof(ManureMassVolume_PigPoultryMinorLivestock_Housing_TestGenerator))]
    public void ManureMassVolume_PigPoultryMinorLivestock_Housing_AllOutputsCorrect(Sector sector, int animaltype, OrganicMatterType organicMatterType, double percentExcretionIndoors, ManureMassVolumeOutput expectedResult)
    {
        ManureMassVolumeOutput actualEmissions = ManureMassandVolume.ManureMassVolume_PigPoultryMinorLivestock_Housing(sector, animaltype, organicMatterType, percentExcretionIndoors);

        actualEmissions.RoundEmissions(4);
        string actualEmissionJson = JsonSerializer.Serialize(actualEmissions, new JsonSerializerOptions
        {
            NumberHandling = JsonNumberHandling.AllowNamedFloatingPointLiterals
        });
        expectedResult.RoundEmissions(4);
        string expectedResultJson = JsonSerializer.Serialize(expectedResult, new JsonSerializerOptions
        {
            NumberHandling = JsonNumberHandling.AllowNamedFloatingPointLiterals
        });

        Assert.Equal(expectedResultJson, actualEmissionJson);
    }

    public static IEnumerable<object[]> ManureMassVolume_PigPoultryMinorLivestock_Storage_TestGenerator()
    {
        var dataList = new List<object[]>();

        var sector = Sector.MinorLivestock;
        var animalType = (int)MinorLivestockType.Goats;
        var organicMatterType = OrganicMatterType.MinorLivestockFYM;
        var percentExcretionIndoors = 100.0;

        var expectedResult = new ManureMassVolumeOutput();
        expectedResult.InitialiseManureMassVolumeOutput();

        expectedResult.ManureMassVolume[ManureMassVolumeOutputs.ManureMassIn].Value = 1978.081;
        expectedResult.ManureMassVolume[ManureMassVolumeOutputs.ManureVolumeIn].Value = 3110.7855;
        expectedResult.ManureMassVolume[ManureMassVolumeOutputs.ManureVolumeOut].Value = 2239.786;
        expectedResult.ManureMassVolume[ManureMassVolumeOutputs.ManureMassOut].Value = 1424.23;

        dataList.Add(new object[]
        {
            sector, animalType, percentExcretionIndoors, organicMatterType, expectedResult
        });

        sector = Sector.MinorLivestock;
        animalType = (int)MinorLivestockType.Deer;
        organicMatterType = OrganicMatterType.MinorLivestockFYM;
        percentExcretionIndoors = 100.0;

        expectedResult = new ManureMassVolumeOutput();
        expectedResult.InitialiseManureMassVolumeOutput();

        expectedResult.ManureMassVolume[ManureMassVolumeOutputs.ManureMassIn].Value = 1209.099;
        expectedResult.ManureMassVolume[ManureMassVolumeOutputs.ManureVolumeIn].Value = 2249.349;
        expectedResult.ManureMassVolume[ManureMassVolumeOutputs.ManureVolumeOut].Value = 1619.5415;
        expectedResult.ManureMassVolume[ManureMassVolumeOutputs.ManureMassOut].Value = 870.5615;

        dataList.Add(new object[]
        {
            sector, animalType, percentExcretionIndoors, organicMatterType, expectedResult
        });

        sector = Sector.Poultry;
        animalType = (int)PoultryType.GrowingPullets;
        organicMatterType = OrganicMatterType.PoultryLitter;
        percentExcretionIndoors = 70.0;

        expectedResult = new ManureMassVolumeOutput();
        expectedResult.InitialiseManureMassVolumeOutput();

        expectedResult.ManureMassVolume[ManureMassVolumeOutputs.ManureMassIn].Value = 6.527099817;
        expectedResult.ManureMassVolume[ManureMassVolumeOutputs.ManureVolumeIn].Value = 8.943070243;
        expectedResult.ManureMassVolume[ManureMassVolumeOutputs.ManureVolumeOut].Value = 6.439010575;
        expectedResult.ManureMassVolume[ManureMassVolumeOutputs.ManureMassOut].Value = 4.699511868;

        dataList.Add(new object[]
        {
            sector, animalType, percentExcretionIndoors,organicMatterType, expectedResult
        });

        sector = Sector.Pigs;
        animalType = (int)PigType.Sows;
        organicMatterType = OrganicMatterType.PigFYM;
        percentExcretionIndoors = 100.0;

        expectedResult = new ManureMassVolumeOutput();
        expectedResult.InitialiseManureMassVolumeOutput();

        expectedResult.ManureMassVolume[ManureMassVolumeOutputs.ManureMassIn].Value = 4319.215995;
        expectedResult.ManureMassVolume[ManureMassVolumeOutputs.ManureVolumeIn].Value = 15222.38929;
        expectedResult.ManureMassVolume[ManureMassVolumeOutputs.ManureVolumeOut].Value = 10960.12029;
        expectedResult.ManureMassVolume[ManureMassVolumeOutputs.ManureMassOut].Value = 3109.835517;

        dataList.Add(new object[]
        {
            sector, animalType, percentExcretionIndoors, organicMatterType, expectedResult
        });

        return dataList;
    }

    [Theory]
    [MemberData(nameof(ManureMassVolume_PigPoultryMinorLivestock_Storage_TestGenerator))]
    public void ManureMassVolume_PigPoultryMinorLivestock_Storage_AllOutputsCorrect(Sector sector, int animaltype, double percentExcretionIndoors, OrganicMatterType organicMatterType, ManureMassVolumeOutput expectedResult)
    {
        ManureMassVolumeOutput actualEmissions = ManureMassandVolume.ManureMassVolume_PigPoultryMinorLivestock_Storage(sector, animaltype, organicMatterType, percentExcretionIndoors);

        actualEmissions.RoundEmissions(4);
        string actualEmissionJson = JsonSerializer.Serialize(actualEmissions, new JsonSerializerOptions
        {
            NumberHandling = JsonNumberHandling.AllowNamedFloatingPointLiterals
        });
        expectedResult.RoundEmissions(4);

        string expectedResultJson = JsonSerializer.Serialize(expectedResult, new JsonSerializerOptions
        {
            NumberHandling = JsonNumberHandling.AllowNamedFloatingPointLiterals
        });

        Assert.Equal(expectedResultJson, actualEmissionJson);
    }

    public static IEnumerable<object[]> ManureMassVolume_PigPoultryMinorLivestock_Spreading_TestGenerator()
    {
        var dataList = new List<object[]>();

        var sector = Sector.Pigs;
        var animalType = (int)PigType.Sows;
        var organicMatterType = OrganicMatterType.PigSlurry;
        var percentindoor = 100;

        var expectedResult = new ManureMassVolumeOutput();
        expectedResult.InitialiseManureMassVolumeOutput();

        expectedResult.ManureMassVolume[ManureMassVolumeOutputs.ManureVolumeIn].Value = 4786.155228;
        expectedResult.ManureMassVolume[ManureMassVolumeOutputs.ManureMassIn].Value = 4735.406049;
        expectedResult.ManureMassVolume[ManureMassVolumeOutputs.ManureVolumeOut].Value = 4786.155228;
        expectedResult.ManureMassVolume[ManureMassVolumeOutputs.ManureMassOut].Value = 4735.406049;

        dataList.Add(new object[]
        {
            sector, animalType, percentindoor, organicMatterType, expectedResult
        });

        sector = Sector.MinorLivestock;
        animalType = (int)MinorLivestockType.Goats;
        organicMatterType = OrganicMatterType.MinorLivestockFYM;
        percentindoor = 100;

        expectedResult = new ManureMassVolumeOutput();
        expectedResult.InitialiseManureMassVolumeOutput();

        expectedResult.ManureMassVolume[ManureMassVolumeOutputs.ManureVolumeIn].Value = 2239.786;
        expectedResult.ManureMassVolume[ManureMassVolumeOutputs.ManureMassIn].Value = 1424.23;
        expectedResult.ManureMassVolume[ManureMassVolumeOutputs.ManureVolumeOut].Value = 2239.786;
        expectedResult.ManureMassVolume[ManureMassVolumeOutputs.ManureMassOut].Value = 1424.23;

        dataList.Add(new object[]
        {
            sector, animalType, percentindoor, organicMatterType, expectedResult
        });

        sector = Sector.Poultry;
        animalType = (int)PoultryType.GrowingPullets;
        organicMatterType = OrganicMatterType.PoultryLitter;
        percentindoor = 70;

        expectedResult = new ManureMassVolumeOutput();
        expectedResult.InitialiseManureMassVolumeOutput();

        expectedResult.ManureMassVolume[ManureMassVolumeOutputs.ManureVolumeIn].Value = 6.439010575;
        expectedResult.ManureMassVolume[ManureMassVolumeOutputs.ManureMassIn].Value = 4.699511868;
        expectedResult.ManureMassVolume[ManureMassVolumeOutputs.ManureVolumeOut].Value = 6.439010575;
        expectedResult.ManureMassVolume[ManureMassVolumeOutputs.ManureMassOut].Value = 4.699511868;

        dataList.Add(new object[]
        {
            sector, animalType, percentindoor, organicMatterType, expectedResult
        });

        return dataList;

    }

    [Theory]
    [MemberData(nameof(ManureMassVolume_PigPoultryMinorLivestock_Spreading_TestGenerator))]
    public void ManureMassVolume_PigPoultryMinorLivestock_Spreading_AllOutputsCorrect(Sector sector, int animaltype, double percentindoor, OrganicMatterType organicMatterType, ManureMassVolumeOutput expectedResult)
    {
        ManureMassVolumeOutput actualEmissions = ManureMassandVolume.ManureMassVolume_PigPoultryMinorLivestock_Spreading(sector, animaltype, organicMatterType, percentindoor);

        actualEmissions.RoundEmissions(4);
        string actualEmissionJson = JsonSerializer.Serialize(actualEmissions, new JsonSerializerOptions
        {
            NumberHandling = JsonNumberHandling.AllowNamedFloatingPointLiterals
        });
        expectedResult.RoundEmissions(4);
        string expectedResultJson = JsonSerializer.Serialize(expectedResult, new JsonSerializerOptions
        {
            NumberHandling = JsonNumberHandling.AllowNamedFloatingPointLiterals
        });

        Assert.Equal(expectedResultJson, actualEmissionJson);
    }

    #endregion

    public static IEnumerable<object[]> ManureMassVolume_Cattle_Grazing_TestGenerator()
    {
        var dataList = new List<object[]>();

        var dryMatterIntake = 164.5467447; // kg per month
        var dryMatterDigestibility = 71.70271;
        var drymattercontent = 29.8;
        var percentTimeGrazing = 100;

        ManureMassVolumeOutput expectedResult = new();
        expectedResult.InitialiseManureMassVolumeOutput();

        expectedResult.ManureMassVolume[ManureMassVolumeOutputs.UrineVolumeIn].Value = 226.0340355;
        expectedResult.ManureMassVolume[ManureMassVolumeOutputs.UrineMassIn].Value = 232.8150565;
        expectedResult.ManureMassVolume[ManureMassVolumeOutputs.DungVolumeIn].Value = 291.0141845;
        expectedResult.ManureMassVolume[ManureMassVolumeOutputs.DungMassIn].Value = 278.8159852;
        expectedResult.ManureMassVolume[ManureMassVolumeOutputs.ExcretaVolumeIn].Value = 517.04822;
        expectedResult.ManureMassVolume[ManureMassVolumeOutputs.ExcretaMassIn].Value = 511.6310417;
        expectedResult.ManureMassVolume[ManureMassVolumeOutputs.ManureVolumeOut].Value = 517.04822;
        expectedResult.ManureMassVolume[ManureMassVolumeOutputs.ManureMassOut].Value = 511.6310417;

        dataList.Add(new object[]{
                        dryMatterIntake, dryMatterDigestibility, drymattercontent, percentTimeGrazing, expectedResult
        });

        return dataList;

    }

    [Theory]
    [MemberData(nameof(ManureMassVolume_Cattle_Grazing_TestGenerator))]
    public void ManureMassVolume_Cattle_Grazing_AllOutputsCorrect(double dryMatterIntake, double dryMatterDigestibility, double drymattercontent, double percentTimeGrazing, ManureMassVolumeOutput expectedResult)
    {
        var actualEmissions = ManureMassandVolume.ManureMassVolume_Cattle_Grazing(dryMatterIntake, dryMatterDigestibility, drymattercontent, percentTimeGrazing);

        actualEmissions.RoundEmissions(4);
        string actualEmissionJson = JsonSerializer.Serialize(actualEmissions, new JsonSerializerOptions
        {
            NumberHandling = JsonNumberHandling.AllowNamedFloatingPointLiterals
        });
        expectedResult.RoundEmissions(4);
        string expectedResultJson = JsonSerializer.Serialize(expectedResult, new JsonSerializerOptions
        {
            NumberHandling = JsonNumberHandling.AllowNamedFloatingPointLiterals
        });

        Assert.Equal(expectedResultJson, actualEmissionJson);
    }

    public static IEnumerable<object[]> ManureMassVolume_Cattle_Housing_TestGenerator()
    {
        var dataList = new List<object[]>();

        //Beef Heifer for breeding, Housed winter, out summer + yards, August
        var dryMatterIntake = 140.8228708;
        var dryMatterDigestibility = 79.55026;
        var drymattercontent = 18.0;
        var percentTimeHousing = 9.5;
        var fym = true;
        var masstoStorage = 0.0;
        var volumetoStorage = 0.0;
        ManureMassVolumeOutput expectedResult = new();
        expectedResult.InitialiseManureMassVolumeOutput();

        expectedResult.ManureMassVolume[ManureMassVolumeOutputs.UrineVolumeIn].Value = 18.45624654;
        expectedResult.ManureMassVolume[ManureMassVolumeOutputs.UrineMassIn].Value = 19.00993393;
        expectedResult.ManureMassVolume[ManureMassVolumeOutputs.DungVolumeIn].Value = 17.09875961;
        expectedResult.ManureMassVolume[ManureMassVolumeOutputs.DungMassIn].Value = 16.38204514;
        expectedResult.ManureMassVolume[ManureMassVolumeOutputs.ExcretaMassIn].Value = 35.39197907;
        expectedResult.ManureMassVolume[ManureMassVolumeOutputs.ExcretaVolumeIn].Value = 35.55500615;
        expectedResult.ManureMassVolume[ManureMassVolumeOutputs.ManureMassOut].Value = 44.89694604;
        expectedResult.ManureMassVolume[ManureMassVolumeOutputs.ManureVolumeOut].Value = 118.8120871;

        dataList.Add(new object[]
        {
            dryMatterIntake, dryMatterDigestibility, drymattercontent, percentTimeHousing, fym, masstoStorage, volumetoStorage, expectedResult
        });

        return dataList;

    }

    [Theory]
    [MemberData(nameof(ManureMassVolume_Cattle_Housing_TestGenerator))]
    public void ManureMassVolume_Cattle_Housing_AllOutputsCorrect(double dryMatterIntake, double dryMatterDigestibility, double drymattercontent, double percentTimeHousing, bool fym,
            out double masstoStorage, out double volumetoStorage, ManureMassVolumeOutput expectedResult)
    {
        var actualEmissions = ManureMassandVolume.ManureMassVolume_Cattle_Housing(dryMatterIntake, dryMatterDigestibility, drymattercontent, percentTimeHousing, fym,
            out masstoStorage, out volumetoStorage);

        actualEmissions.RoundEmissions(4);
        string actualEmissionJson = JsonSerializer.Serialize(actualEmissions, new JsonSerializerOptions
        {
            NumberHandling = JsonNumberHandling.AllowNamedFloatingPointLiterals
        });
        expectedResult.RoundEmissions(4);
        string expectedResultJson = JsonSerializer.Serialize(expectedResult, new JsonSerializerOptions
        {
            NumberHandling = JsonNumberHandling.AllowNamedFloatingPointLiterals
        });

        Assert.Equal(expectedResultJson, actualEmissionJson);

    }

    public static IEnumerable<object[]> ManureMassVolume_Cattle_Yards_TestGenerator()
    {
        var dataList = new List<object[]>();

        //Beef Heifer for breeding, Housed winter, out summer + yards, August
        var dryMatterIntake = 140.8228708;
        var dryMatterDigestibility = 79.55026;
        var drymattercontent = 18.0;
        var percentTimeYards = 6.3;
        var massOutYards = 0.0;
        var volumeOutYards = 0.0;

        ManureMassVolumeOutput expectedResult = new();
        expectedResult.InitialiseManureMassVolumeOutput();

        expectedResult.ManureMassVolume[ManureMassVolumeOutputs.UrineVolumeIn].Value = 12.2394056;
        expectedResult.ManureMassVolume[ManureMassVolumeOutputs.UrineMassIn].Value = 12.60658776;
        expectedResult.ManureMassVolume[ManureMassVolumeOutputs.DungVolumeIn].Value = 11.33917743;
        expectedResult.ManureMassVolume[ManureMassVolumeOutputs.DungMassIn].Value = 10.86388257;
        expectedResult.ManureMassVolume[ManureMassVolumeOutputs.ExcretaVolumeIn].Value = 23.57858303;
        expectedResult.ManureMassVolume[ManureMassVolumeOutputs.ExcretaMassIn].Value = 23.4704733;
        expectedResult.ManureMassVolume[ManureMassVolumeOutputs.ManureMassOut].Value = 23.4704733;
        expectedResult.ManureMassVolume[ManureMassVolumeOutputs.ManureVolumeOut].Value = 23.57858303;

        dataList.Add(new object[]
        {
            dryMatterIntake, dryMatterDigestibility, drymattercontent, percentTimeYards, massOutYards, volumeOutYards, expectedResult
        });

        return dataList;

    }

    [Theory]
    [MemberData(nameof(ManureMassVolume_Cattle_Yards_TestGenerator))]
    public void ManureMassVolume_Cattle_Yards_AllOutputsCorrect(double dryMatterIntake, double dryMatterDigestibility, double drymattercontent, double percentTimeYards, out double massOutYards, out double volumeOutYards, ManureMassVolumeOutput expectedResult)
    {
        var actualEmissions = ManureMassandVolume.ManureMassVolume_Cattle_Yards(dryMatterIntake, dryMatterDigestibility, drymattercontent, percentTimeYards, out massOutYards, out volumeOutYards);

        actualEmissions.RoundEmissions(4);
        string actualEmissionJson = JsonSerializer.Serialize(actualEmissions, new JsonSerializerOptions
        {
            NumberHandling = JsonNumberHandling.AllowNamedFloatingPointLiterals
        });
        expectedResult.RoundEmissions(4);
        string expectedResultJson = JsonSerializer.Serialize(expectedResult, new JsonSerializerOptions
        {
            NumberHandling = JsonNumberHandling.AllowNamedFloatingPointLiterals
        });

        Assert.Equal(expectedResultJson, actualEmissionJson);
    }

    public static IEnumerable<object[]> ManureMassVolume_Cattle_Storage_TestGenerator()
    {
        var dataList = new List<object[]>();

        var maureMassIn = 125;
        var manureVolumeIn = 312.5;
        var fym = true;
        var anaerobicDigestion = true;
        var massToSpreading = 106.25;
        var volumeToSpreading = 312.5;

        ManureMassVolumeOutput expectedResult = new();
        expectedResult.InitialiseManureMassVolumeOutput();

        expectedResult.ManureMassVolume[ManureMassVolumeOutputs.ManureMassIn].Value = 125;
        expectedResult.ManureMassVolume[ManureMassVolumeOutputs.ManureVolumeIn].Value = 312.5;
        expectedResult.ManureMassVolume[ManureMassVolumeOutputs.ManureMassOut].Value = 106.25;
        expectedResult.ManureMassVolume[ManureMassVolumeOutputs.ManureVolumeOut].Value = 312.5;

        dataList.Add(new object[]
        {
            maureMassIn, manureVolumeIn, fym, anaerobicDigestion, massToSpreading, volumeToSpreading, expectedResult
        });

        return dataList;

    }

    [Theory]
    [MemberData(nameof(ManureMassVolume_Cattle_Storage_TestGenerator))]
    public void ManureMassVolume_Cattle_Storage_AllOutputsCorrect(double manureMassIn, double manureVolumeIn, bool fym, bool anaerobicDigestion,
            out double massToSpreading, out double volumeToSpreading, ManureMassVolumeOutput expectedResult)
    {
        var actualEmissions = ManureMassandVolume.ManureMassVolume_Cattle_Storage(manureMassIn, manureVolumeIn, fym, anaerobicDigestion,
            out massToSpreading, out volumeToSpreading);

        actualEmissions.RoundEmissions(4);
        string actualEmissionJson = JsonSerializer.Serialize(actualEmissions, new JsonSerializerOptions
        {
            NumberHandling = JsonNumberHandling.AllowNamedFloatingPointLiterals
        });
        expectedResult.RoundEmissions(4);
        string expectedResultJson = JsonSerializer.Serialize(expectedResult, new JsonSerializerOptions
        {
            NumberHandling = JsonNumberHandling.AllowNamedFloatingPointLiterals
        });

        Assert.Equal(expectedResultJson, actualEmissionJson);
    }

    public static IEnumerable<object[]> ManureMassVolume_CattleSheep_Spreading_TestGenerator()
    {
        var dataList = new List<object[]>();

        var manureMassIn = 106.25;
        var manureVolumeIn = 312.5;

        ManureMassVolumeOutput expectedResult = new();
        expectedResult.InitialiseManureMassVolumeOutput();

        expectedResult.ManureMassVolume[ManureMassVolumeOutputs.ManureMassIn].Value = 106.25;
        expectedResult.ManureMassVolume[ManureMassVolumeOutputs.ManureMassOut].Value = 106.25;
        expectedResult.ManureMassVolume[ManureMassVolumeOutputs.ManureVolumeIn].Value = 312.5;
        expectedResult.ManureMassVolume[ManureMassVolumeOutputs.ManureVolumeOut].Value = 312.5;

        dataList.Add(new object[] {
            manureMassIn, manureVolumeIn, expectedResult
        });

        return dataList;
    }

    [Theory]
    [MemberData(nameof(ManureMassVolume_CattleSheep_Spreading_TestGenerator))]
    public void ManureMassVolume_CattleSheep_Spreading_AllOutputsCorrect(double manureMassIn, double manureVolumeIn, ManureMassVolumeOutput expectedResult)
    {
        var actualEmissions = ManureMassandVolume.ManureMassVolume_CattleSheep_Spreading(manureMassIn, manureVolumeIn);

        actualEmissions.RoundEmissions(4);
        string actualEmissionJson = JsonSerializer.Serialize(actualEmissions, new JsonSerializerOptions
        {
            NumberHandling = JsonNumberHandling.AllowNamedFloatingPointLiterals
        });
        expectedResult.RoundEmissions(4);
        string expectedResultJson = JsonSerializer.Serialize(expectedResult, new JsonSerializerOptions
        {
            NumberHandling = JsonNumberHandling.AllowNamedFloatingPointLiterals
        });

        Assert.Equal(expectedResultJson, actualEmissionJson);
    }

    public static IEnumerable<object[]> ManureMassVolume_Sheep_Grazing_TestGenerator()
    {
        var dataList = new List<object[]>();

        SheepEnergyBalance sheepEnergyBalance = new()
        {
            FieldEntericMethane = 6.827447259,
            FieldVolatileSolids = 137.0920614,
            FieldTotalNitrogen = 8.164032536,
            FieldAvailableNitrogen = 4.336841577,
            FieldGrossEnergy = 7488.366586,
            FieldMetabolisableEnergy = 4324.480082,
            FieldDryMatterIntake = 400.7941338,
            FieldDays = 360,
            FieldDungNitrogen = 3.717980112,
            FieldUrineNitrogen = 4.446052424,
            HouseEntericMethane = 0.095689575,
            HouseVolatileSolids = 2.196151093,
            HouseTotalNitrogen = 0.106709841,
            HouseAvailableNitrogen = 0.032557243,
            HouseGrossEnergy = 105.0147233,
            HouseMetabolisableEnergy = 55.21276458,
            HouseDryMatterIntake = 5.636256082,
            HouseDays = 5,
            HouseDungNitrogen = 0.048893501,
            HouseUrineNitrogen = 0.050446436,

            FieldDungNitrogenCP200 = 4.050711348,
            FieldUrineNitrogenCP200 = 7.045596623,
            FieldDungNitrogenCP100 = 3.234258436,
            FieldUrineNitrogenCP100 = 2.97492381,
            FieldTotalNitrogenCP200 = 11.09630797,
            FieldAvailableNitrogenCP200 = 6.596363831,
            FieldTotalNitrogenCP100 = 6.209182246,
            FieldAvailableNitrogenCP100 = 3.013824004,
            FieldNitrogenIntake = 8.631024169,
            HouseNitrogenIntake = 0.1057542,
            FieldNitrogenIntakeCP100 = 6.676173879,
            FieldNitrogenIntakeCP200 = 11.5632996,
            FieldExcretaMass = 1103.306293,
            FieldExcretaVolume = 1171.555705,
            HouseExcretaMass = 8.802569406,
            HouseExcretaVolume = 10.25542182,
            HouseManureMass = 10.51650072,
            HouseManureVolume = 21.42414139
        };

        ManureMassVolumeOutput expectedResult = new();
        expectedResult.InitialiseManureMassVolumeOutput();
        expectedResult.ManureMassVolume[ManureMassVolumeOutputs.ExcretaVolumeIn].Value = 1171.555705;
        expectedResult.ManureMassVolume[ManureMassVolumeOutputs.ExcretaMassIn].Value = 1103.306293;
        expectedResult.ManureMassVolume[ManureMassVolumeOutputs.ManureVolumeOut].Value = 1171.555705;
        expectedResult.ManureMassVolume[ManureMassVolumeOutputs.ManureMassOut].Value = 1103.306293;

        dataList.Add(new object[]
        {
            sheepEnergyBalance, expectedResult
        });

        return dataList;
    }

    [Theory]
    [MemberData(nameof(ManureMassVolume_Sheep_Grazing_TestGenerator))]
    public void ManureMassVolume_Sheep_Grazing_AllOutputsCorrect(SheepEnergyBalance sheepEnergyBalance, ManureMassVolumeOutput expectedResult)
    {
        var actualEmissions = ManureMassandVolume.ManureMassVolume_Sheep_Grazing(sheepEnergyBalance);

        actualEmissions.RoundEmissions(4);
        string actualEmissionJson = JsonSerializer.Serialize(actualEmissions, new JsonSerializerOptions
        {
            NumberHandling = JsonNumberHandling.AllowNamedFloatingPointLiterals
        });
        expectedResult.RoundEmissions(4);
        string expectedResultJson = JsonSerializer.Serialize(expectedResult, new JsonSerializerOptions
        {
            NumberHandling = JsonNumberHandling.AllowNamedFloatingPointLiterals
        });

        Assert.Equal(expectedResultJson, actualEmissionJson);
    }
}
