using static FrameAgricultureApi.Enumerators.Enumerators;

namespace FrameAgricultureApi.Libraries.Housing.Tests;

public class HousingManureManagementTests
{
    [Theory]
    [InlineData(Sector.Pigs, 1, ManureHousingSystem.Part_Slatted_Floor, OrganicMatterType.PigSlurry, 21.1, 0.0422)]
    [InlineData(Sector.Pigs, 1, ManureHousingSystem.Solid_Floor_with_Bedding, OrganicMatterType.PigFYM, 24.02, 0.4804)]
    [InlineData(Sector.Pigs, 1, ManureHousingSystem.Fully_Slatted_Floor, OrganicMatterType.PigSlurry, 21.1, 0.0422)]
    [InlineData(Sector.Pigs, 2, ManureHousingSystem.Solid_Floor_with_Bedding, OrganicMatterType.PigFYM, 13.6328, 0.2727)]
    [InlineData(Sector.Pigs, 2, ManureHousingSystem.Fully_Slatted_Floor, OrganicMatterType.PigSlurry, 11.68, 0.0234)]
    [InlineData(Sector.Pigs, 3, ManureHousingSystem.Part_Slatted_Floor, OrganicMatterType.PigSlurry, 18.9, 0.0378)]
    [InlineData(Sector.Pigs, 3, ManureHousingSystem.Solid_Floor_with_Bedding, OrganicMatterType.PigFYM, 21.2725, 0.4255)]
    [InlineData(Sector.Pigs, 4, ManureHousingSystem.Fully_Slatted_Floor, OrganicMatterType.PigSlurry, 13.1686, 0.0263)]
    [InlineData(Sector.Pigs, 4, ManureHousingSystem.Part_Slatted_Floor, OrganicMatterType.PigSlurry, 13.1686, 0.0263)]
    [InlineData(Sector.Pigs, 4, ManureHousingSystem.Solid_Floor_with_Bedding, OrganicMatterType.PigFYM, 14.2636, 0.2853)]
    [InlineData(Sector.Pigs, 5, ManureHousingSystem.Part_Slatted_Floor, OrganicMatterType.PigSlurry, 9.59871, 0.0192)]
    [InlineData(Sector.Pigs, 6, ManureHousingSystem.Solid_Floor_with_Bedding, OrganicMatterType.PigFYM, 4.3445, 0.0869)]
    [InlineData(Sector.MinorLivestock, 1, ManureHousingSystem.Solid_Floor_with_Bedding, OrganicMatterType.MinorLivestockFYM, 9.5315, 0.1906)]
    [InlineData(Sector.MinorLivestock, 2, ManureHousingSystem.Solid_Floor_with_Bedding, OrganicMatterType.MinorLivestockFYM, 30.6505, 0.6130)]
    [InlineData(Sector.MinorLivestock, 3, ManureHousingSystem.Solid_Floor_with_Bedding, OrganicMatterType.MinorLivestockFYM, 60.9500, 1.2190)]
    [InlineData(Sector.MinorLivestock, 4, ManureHousingSystem.Solid_Floor_with_Bedding, OrganicMatterType.MinorLivestockFYM, 139.950, 2.799)]
    [InlineData(Sector.Poultry, 1, ManureHousingSystem.Deep_Litter, OrganicMatterType.PoultryLitter, 0.3588 + 0.009125, 0.001839475)]
    [InlineData(Sector.Poultry, 2, ManureHousingSystem.Free_Range_Generic, OrganicMatterType.PoultryLayerManure, 0.5983, 0.002991443)]
    [InlineData(Sector.Poultry, 3, ManureHousingSystem.Free_Range_Generic, OrganicMatterType.PoultryLayerManure, 0.9698 + 0.009125, 0.00489479)]
    [InlineData(Sector.Poultry, 4, ManureHousingSystem.Free_Range_Generic, OrganicMatterType.PoultryLayerManure, 0.2554 + 0.009125, 0.001322858)]
    [InlineData(Sector.Poultry, 5, ManureHousingSystem.Free_Range_Generic, OrganicMatterType.PoultryLayerManure, 1.6038 + 0.097767857, 0.008507646)]
    [InlineData(Sector.Poultry, 6, ManureHousingSystem.Free_Range_Generic, OrganicMatterType.DuckFYM, 1.0797 + 0.148979592, 0.006143522)]
    [InlineData(Sector.Poultry, 7, ManureHousingSystem.Free_Range_Generic, OrganicMatterType.DuckFYM, 1.0797 + 0.148979592, 0.006143522)]
    [InlineData(Sector.Poultry, 8, ManureHousingSystem.Free_Range_Generic, OrganicMatterType.PoultryLayerManure, 1.0797 + 0.097767857, 0.005887463)]
    public void N2ONEmission_AllOutputsCorrect(Sector sector, int animalType, ManureHousingSystem manureHousingSystem, OrganicMatterType manureType, double totalN, double expectedResult)
    {
        var actualResult = HousingManureManagement.N2ONEmission(sector, animalType, manureHousingSystem, manureType, totalN);
        Assert.Equal(expectedResult, actualResult, 4);
    }

    [Theory]
    [InlineData(Sector.Pigs, 1, ManureHousingSystem.Part_Slatted_Floor, OrganicMatterType.PigSlurry, 14.77, 4.2242)]
    [InlineData(Sector.Pigs, 1, ManureHousingSystem.Solid_Floor_with_Bedding, OrganicMatterType.PigFYM, 14.77, 4.948)]
    [InlineData(Sector.Pigs, 1, ManureHousingSystem.Fully_Slatted_Floor, OrganicMatterType.PigSlurry, 14.77, 4.2242)]
    [InlineData(Sector.Pigs, 2, ManureHousingSystem.Solid_Floor_with_Bedding, OrganicMatterType.PigFYM, 8.176, 2.5182)]
    [InlineData(Sector.Pigs, 2, ManureHousingSystem.Fully_Slatted_Floor, OrganicMatterType.PigSlurry, 8.176, 2.2484)]
    [InlineData(Sector.Pigs, 3, ManureHousingSystem.Part_Slatted_Floor, OrganicMatterType.PigSlurry, 13.23, 3.6383)]
    [InlineData(Sector.Pigs, 3, ManureHousingSystem.Solid_Floor_with_Bedding, OrganicMatterType.PigFYM, 13.23, 4.0748)]
    [InlineData(Sector.Pigs, 4, ManureHousingSystem.Fully_Slatted_Floor, OrganicMatterType.PigSlurry, 9.2180, 2.6364)]
    [InlineData(Sector.Pigs, 4, ManureHousingSystem.Part_Slatted_Floor, OrganicMatterType.PigSlurry, 9.2180, 2.6364)]
    [InlineData(Sector.Pigs, 4, ManureHousingSystem.Solid_Floor_with_Bedding, OrganicMatterType.PigFYM, 9.2180, 1.8067)]
    [InlineData(Sector.Pigs, 5, ManureHousingSystem.Part_Slatted_Floor, OrganicMatterType.PigSlurry, 6.7187, 1.9215)]
    [InlineData(Sector.Pigs, 6, ManureHousingSystem.Solid_Floor_with_Bedding, OrganicMatterType.PigFYM, 2.8239, 0.2090)]
    [InlineData(Sector.MinorLivestock, 1, ManureHousingSystem.Solid_Floor_with_Bedding, OrganicMatterType.MinorLivestockFYM, 5.040, 0.8467)]
    [InlineData(Sector.MinorLivestock, 2, ManureHousingSystem.Solid_Floor_with_Bedding, OrganicMatterType.MinorLivestockFYM, 17.580, 2.9534)]
    [InlineData(Sector.MinorLivestock, 3, ManureHousingSystem.Solid_Floor_with_Bedding, OrganicMatterType.MinorLivestockFYM, 30.0, 5.0400)]
    [InlineData(Sector.MinorLivestock, 4, ManureHousingSystem.Solid_Floor_with_Bedding, OrganicMatterType.MinorLivestockFYM, 77.4, 13.0032)]
    [InlineData(Sector.Poultry, 1, ManureHousingSystem.Free_Range_Generic, OrganicMatterType.PoultryLitter, 0.1758, 0.0237326)]
    [InlineData(Sector.Poultry, 2, ManureHousingSystem.Perchery, OrganicMatterType.PoultryLayerManure, 0.4653, 0.165659475)]
    [InlineData(Sector.Poultry, 2, ManureHousingSystem.Deep_Litter, OrganicMatterType.PoultryLayerManure, 0.4653, 0.165659475)]
    [InlineData(Sector.Poultry, 2, ManureHousingSystem.Cages_DeepPit, OrganicMatterType.PoultryLayerManure, 0.4653, 0.165659475)]
    [InlineData(Sector.Poultry, 2, ManureHousingSystem.Cages_BeltCleaned, OrganicMatterType.PoultryLayerManure, 0.4653, 0.067473663)]
    [InlineData(Sector.Poultry, 2, ManureHousingSystem.Enriched_Cages_BeltCleaned, OrganicMatterType.PoultryLayerManure, 0.4653, 0.041414869)]
    [InlineData(Sector.Poultry, 3, ManureHousingSystem.Free_Range_Generic, OrganicMatterType.PoultryLayerManure, 0.6789, 0.091649224)]
    [InlineData(Sector.Poultry, 4, ManureHousingSystem.Free_Range_Generic, OrganicMatterType.PoultryLayerManure, 0.1788, 0.017702455)]
    [InlineData(Sector.Poultry, 5, ManureHousingSystem.Free_Range_Generic, OrganicMatterType.PoultryLayerManure, 1.1226, 0.406393118)]
    [InlineData(Sector.Poultry, 6, ManureHousingSystem.Free_Range_Generic, OrganicMatterType.DuckFYM, 0.7558, 0.102033989)]
    [InlineData(Sector.Poultry, 7, ManureHousingSystem.Free_Range_Generic, OrganicMatterType.DuckFYM, 0.7558, 0.102033989)]
    [InlineData(Sector.Poultry, 8, ManureHousingSystem.Free_Range_Generic, OrganicMatterType.PoultryLayerManure, 0.7558, 0.102033989)]
    [InlineData(Sector.Sheep, 2, ManureHousingSystem.SheepHousing, OrganicMatterType.SheepFYM, 0.032557243, 0.009116028)]
    [InlineData(Sector.Sheep, 3, ManureHousingSystem.SheepHousing, OrganicMatterType.SheepFYM, 0.136308182, 0.038166291)]
    [InlineData(Sector.Sheep, (int)SheepType.Ewe, ManureHousingSystem.SheepHousing, OrganicMatterType.SheepFYM, 0.136024257, 0.038166291)]
    [InlineData(Sector.Sheep, (int)SheepType.Ewe, ManureHousingSystem.SheepHousing, OrganicMatterType.SheepFYM, 0.136024257, 0.038086792)]
    [InlineData(Sector.Sheep, (int)SheepType.Lamb, ManureHousingSystem.SheepHousing, OrganicMatterType.SheepFYM, 0.104013129, 0.029123676)]
    [InlineData(Sector.Sheep, (int)SheepType.Ram, ManureHousingSystem.SheepHousing, OrganicMatterType.SheepFYM, 0.051029174, 0.014288169)]
    [InlineData(Sector.Sheep, (int)SheepType.Ewe, ManureHousingSystem.SheepHousing, OrganicMatterType.SheepFYM, 0.312909297, 0.087614603)]
    [InlineData(Sector.Sheep, (int)SheepType.Ewe, ManureHousingSystem.SheepHousing, OrganicMatterType.SheepFYM, 0.312435085, 0.087481824)]
    [InlineData(Sector.Sheep, (int)SheepType.Lamb, ManureHousingSystem.SheepHousing, OrganicMatterType.SheepFYM, 0.253945378, 0.071104706)]
    [InlineData(Sector.Sheep, (int)SheepType.Ewe, ManureHousingSystem.SheepHousing, OrganicMatterType.SheepFYM, 0.247196742, 0.069328965)]
    public void NH3NEmission_AllOutputsCorrect(Sector sector, int animalType, ManureHousingSystem manureHousingSystem, OrganicMatterType manureType, double totalAmmoniacalNitrogen, double expectedResult)
    {
        var actualResult = HousingManureManagement.NH3NEmission(sector, animalType, manureHousingSystem, manureType, totalAmmoniacalNitrogen);
        Assert.Equal(expectedResult, actualResult, 3);
    }

    [Theory]
    [InlineData(Sector.Pigs, 1, ManureHousingSystem.Part_Slatted_Floor, OrganicMatterType.PigSlurry, 0.0422, 0.0042)]
    [InlineData(Sector.Pigs, 1, ManureHousingSystem.Solid_Floor_with_Bedding, OrganicMatterType.PigFYM, 0.4804, 0.0480)]
    [InlineData(Sector.Pigs, 1, ManureHousingSystem.Fully_Slatted_Floor, OrganicMatterType.PigSlurry, 0.0422, 0.0042)]
    [InlineData(Sector.Pigs, 2, ManureHousingSystem.Solid_Floor_with_Bedding, OrganicMatterType.PigFYM, 0.2727, 0.0273)]
    [InlineData(Sector.Pigs, 2, ManureHousingSystem.Fully_Slatted_Floor, OrganicMatterType.PigSlurry, 0.0234, 0.0023)]
    [InlineData(Sector.Pigs, 3, ManureHousingSystem.Part_Slatted_Floor, OrganicMatterType.PigSlurry, 0.0378, 0.0038)]
    [InlineData(Sector.Pigs, 3, ManureHousingSystem.Solid_Floor_with_Bedding, OrganicMatterType.PigFYM, 0.42545, 0.042545)]
    [InlineData(Sector.Pigs, 4, ManureHousingSystem.Fully_Slatted_Floor, OrganicMatterType.PigSlurry, 0.0263, 0.0026)]
    [InlineData(Sector.Pigs, 4, ManureHousingSystem.Part_Slatted_Floor, OrganicMatterType.PigSlurry, 0.0263, 0.0026)]
    [InlineData(Sector.Pigs, 4, ManureHousingSystem.Solid_Floor_with_Bedding, OrganicMatterType.PigFYM, 0.2853, 0.028527)]
    [InlineData(Sector.Pigs, 5, ManureHousingSystem.Part_Slatted_Floor, OrganicMatterType.PigSlurry, 0.0192, 0.0019)]
    [InlineData(Sector.Pigs, 6, ManureHousingSystem.Solid_Floor_with_Bedding, OrganicMatterType.PigFYM, 0.0869, 0.0087)]
    [InlineData(Sector.MinorLivestock, 1, ManureHousingSystem.Solid_Floor_with_Bedding, OrganicMatterType.MinorLivestockFYM, 0.1906, 0.0191)]
    [InlineData(Sector.MinorLivestock, 2, ManureHousingSystem.Solid_Floor_with_Bedding, OrganicMatterType.MinorLivestockFYM, 0.6130, 0.0613)]
    [InlineData(Sector.MinorLivestock, 3, ManureHousingSystem.Solid_Floor_with_Bedding, OrganicMatterType.MinorLivestockFYM, 1.2190, 0.1219)]
    [InlineData(Sector.MinorLivestock, 4, ManureHousingSystem.Solid_Floor_with_Bedding, OrganicMatterType.MinorLivestockFYM, 2.7990, 0.2799)]
    [InlineData(Sector.Poultry, 1, ManureHousingSystem.Deep_Litter, OrganicMatterType.PoultryLitter, 0.001839475, 0.000183948)]
    [InlineData(Sector.Poultry, 2, ManureHousingSystem.Perchery, OrganicMatterType.PoultryLayerManure, 0.003323826, 0.000332383)]
    [InlineData(Sector.Poultry, 2, ManureHousingSystem.Deep_Litter, OrganicMatterType.PoultryLayerManure, 0.003323826, 0.000332383)]
    [InlineData(Sector.Poultry, 2, ManureHousingSystem.Cages_DeepPit, OrganicMatterType.PoultryLayerManure, 0.003323826, 0.000332383)]
    [InlineData(Sector.Poultry, 2, ManureHousingSystem.Cages_BeltCleaned, OrganicMatterType.PoultryLayerManure, 0.003323826, 0.000332383)]
    [InlineData(Sector.Poultry, 2, ManureHousingSystem.Enriched_Cages_BeltCleaned, OrganicMatterType.PoultryLayerManure, 0.003323826, 0.000332383)]
    [InlineData(Sector.Poultry, 3, ManureHousingSystem.Deep_Litter, OrganicMatterType.PoultryLayerManure, 0.00575053, 0.000575053)]
    [InlineData(Sector.Poultry, 4, ManureHousingSystem.Free_Range_Generic, OrganicMatterType.PoultryLayerManure, 0.001322858, 0.000132286)]
    [InlineData(Sector.Poultry, 5, ManureHousingSystem.Free_Range_Generic, OrganicMatterType.PoultryLayerManure, 0.008507646, 0.000850765)]
    [InlineData(Sector.Poultry, 6, ManureHousingSystem.Free_Range_Generic, OrganicMatterType.DuckFYM, 0.006143522, 0.000614352)]
    [InlineData(Sector.Poultry, 7, ManureHousingSystem.Free_Range_Generic, OrganicMatterType.DuckFYM, 0.006143522, 0.000614352)]
    [InlineData(Sector.Poultry, 8, ManureHousingSystem.Free_Range_Generic, OrganicMatterType.PoultryLayerManure, 0.005887463, 0.000588746)]
    public void NONEmission_AllOutputsCorrect(Sector sector, int animalType, ManureHousingSystem manureHousingSystem, OrganicMatterType manureType, double totalN2ON, double expectedResult)
    {
        var actualResult = HousingManureManagement.NONEmission(sector, animalType, manureHousingSystem, manureType, totalN2ON);
        Assert.Equal(expectedResult, actualResult, 4);
    }

    [Theory]
    [InlineData(Sector.Pigs, 1, ManureHousingSystem.Fully_Slatted_Floor, OrganicMatterType.PigSlurry, 0.0422, 0.1266)]
    [InlineData(Sector.Pigs, 1, ManureHousingSystem.Part_Slatted_Floor, OrganicMatterType.PigSlurry, 0.0422, 0.1266)]
    [InlineData(Sector.Pigs, 1, ManureHousingSystem.Solid_Floor_with_Bedding, OrganicMatterType.PigFYM, 0.4804, 1.4412)]
    [InlineData(Sector.Pigs, 2, ManureHousingSystem.Fully_Slatted_Floor, OrganicMatterType.PigSlurry, 0.0234, 0.0701)]
    [InlineData(Sector.Pigs, 2, ManureHousingSystem.Part_Slatted_Floor, OrganicMatterType.PigSlurry, 0.0234, 0.0701)]
    [InlineData(Sector.Pigs, 2, ManureHousingSystem.Solid_Floor_with_Bedding, OrganicMatterType.PigFYM, 0.2727, 0.8180)]
    [InlineData(Sector.Pigs, 3, ManureHousingSystem.Fully_Slatted_Floor, OrganicMatterType.PigSlurry, 0.0378, 0.1134)]
    [InlineData(Sector.Pigs, 3, ManureHousingSystem.Part_Slatted_Floor, OrganicMatterType.PigSlurry, 0.0378, 0.1134)]
    [InlineData(Sector.Pigs, 3, ManureHousingSystem.Solid_Floor_with_Bedding, OrganicMatterType.PigFYM, 0.4255, 1.2764)]
    [InlineData(Sector.Pigs, 4, ManureHousingSystem.Fully_Slatted_Floor, OrganicMatterType.PigSlurry, 0.0263, 0.0790)]
    [InlineData(Sector.Pigs, 4, ManureHousingSystem.Part_Slatted_Floor, OrganicMatterType.PigSlurry, 0.0263, 0.0790)]
    [InlineData(Sector.Pigs, 4, ManureHousingSystem.Solid_Floor_with_Bedding, OrganicMatterType.PigFYM, 0.2853, 0.8558)]
    [InlineData(Sector.Pigs, 5, ManureHousingSystem.Fully_Slatted_Floor, OrganicMatterType.PigSlurry, 0.0192, 0.0576)]
    [InlineData(Sector.Pigs, 5, ManureHousingSystem.Part_Slatted_Floor, OrganicMatterType.PigSlurry, 0.0192, 0.0576)]
    [InlineData(Sector.Pigs, 5, ManureHousingSystem.Solid_Floor_with_Bedding, OrganicMatterType.PigFYM, 0.2091, 0.6274)]
    [InlineData(Sector.Pigs, 6, ManureHousingSystem.Fully_Slatted_Floor, OrganicMatterType.PigSlurry, 0.0081, 0.0242)]
    [InlineData(Sector.Pigs, 6, ManureHousingSystem.Part_Slatted_Floor, OrganicMatterType.PigSlurry, 0.0081, 0.0242)]
    [InlineData(Sector.Pigs, 6, ManureHousingSystem.Solid_Floor_with_Bedding, OrganicMatterType.PigFYM, 0.0869, 0.2607)]
    [InlineData(Sector.MinorLivestock, 1, ManureHousingSystem.Solid_Floor_with_Bedding, OrganicMatterType.MinorLivestockFYM, 0.1906, 0.5719)]
    [InlineData(Sector.MinorLivestock, 2, ManureHousingSystem.Solid_Floor_with_Bedding, OrganicMatterType.MinorLivestockFYM, 0.6130, 1.8390)]
    [InlineData(Sector.MinorLivestock, 3, ManureHousingSystem.Solid_Floor_with_Bedding, OrganicMatterType.MinorLivestockFYM, 1.2190, 3.6570)]
    [InlineData(Sector.MinorLivestock, 4, ManureHousingSystem.Solid_Floor_with_Bedding, OrganicMatterType.MinorLivestockFYM, 2.7990, 8.3970)]
    public void N2NEmission_AllOuputsCorrect(Sector sector, int animalType, ManureHousingSystem manureHousingSystem, OrganicMatterType manureType, double totalN2ON, double expectedResult)
    {
        var actualResult = HousingManureManagement.N2NEmission(sector, animalType, manureHousingSystem, manureType, totalN2ON);
        Assert.Equal(expectedResult, actualResult, 3);
    }

    [Theory]
    [InlineData(Sector.Pigs, 1, ManureHousingSystem.Fully_Slatted_Floor, OrganicMatterType.PigSlurry, 167.9, 0)]
    [InlineData(Sector.MinorLivestock, 1, ManureHousingSystem.Solid_Floor_with_Bedding, OrganicMatterType.MinorLivestockFYM, 109.5, 0.39)]
    [InlineData(Sector.MinorLivestock, 2, ManureHousingSystem.Solid_Floor_with_Bedding, OrganicMatterType.MinorLivestockFYM, 109.5, 0.22)]
    [InlineData(Sector.MinorLivestock, 3, ManureHousingSystem.Solid_Floor_with_Bedding, OrganicMatterType.MinorLivestockFYM, 777.45, 0.41)]
    [InlineData(Sector.MinorLivestock, 4, ManureHousingSystem.Solid_Floor_with_Bedding, OrganicMatterType.MinorLivestockFYM, 777.45, 0.41)]
    [InlineData(Sector.Poultry, 1, ManureHousingSystem.Free_Range_Generic, OrganicMatterType.PoultryLitter, 2.555, 0)]
    [InlineData(Sector.Poultry, 2, ManureHousingSystem.Free_Range_Generic, OrganicMatterType.PoultryLayerManure, 7.3, 0)]
    [InlineData(Sector.Poultry, 3, ManureHousingSystem.Free_Range_Generic, OrganicMatterType.PoultryLayerManure, 3.65, 0)]
    [InlineData(Sector.Poultry, 4, ManureHousingSystem.Free_Range_Generic, OrganicMatterType.PoultryLayerManure, 7.3, 0)]
    [InlineData(Sector.Poultry, 5, ManureHousingSystem.Free_Range_Generic, OrganicMatterType.PoultryLayerManure, 25.55, 0)]
    [InlineData(Sector.Poultry, 6, ManureHousingSystem.Free_Range_Generic, OrganicMatterType.DuckFYM, 7.3, 0)]
    [InlineData(Sector.Poultry, 7, ManureHousingSystem.Free_Range_Generic, OrganicMatterType.DuckFYM, 7.3, 0)]
    [InlineData(Sector.Poultry, 8, ManureHousingSystem.Free_Range_Generic, OrganicMatterType.PoultryLayerManure, 3.65, 0)]
    public void CH4Emission_AllOutputsCorrect(Sector sector, int animalType, ManureHousingSystem manureHousingSystem, OrganicMatterType manureType, double volatileSolids, double expectedResult)
    {
        var actualResult = HousingManureManagement.CH4Emission(sector, animalType, manureHousingSystem, manureType, volatileSolids);
        Assert.Equal(expectedResult, actualResult, 3);
    }
}
