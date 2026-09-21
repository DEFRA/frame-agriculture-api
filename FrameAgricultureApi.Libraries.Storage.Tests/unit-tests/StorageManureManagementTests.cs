using static FrameAgricultureApi.Enumerators.Enumerators;

namespace FrameAgricultureApi.Libraries.Storage.Tests;

public class StorageManureManagementTests
{
    [Theory]
    [InlineData(Sector.Pigs, 0, ManureStorageSystem.Slurry_AboveGroundTanks, OrganicMatterType.PigSlurry, 16.7028, 0)]
    [InlineData(Sector.Pigs, 0, ManureStorageSystem.Slurry_BelowGroundTanks, OrganicMatterType.PigSlurry, 16.7028, 0)]
    [InlineData(Sector.Pigs, 0, ManureStorageSystem.Slurry_EarthBankLagoon, OrganicMatterType.PigSlurry, 16.7028, 0)]
    [InlineData(Sector.Pigs, 0, ManureStorageSystem.Slurry_Bag, OrganicMatterType.PigSlurry, 11.2079, 0)]
    [InlineData(Sector.Pigs, 0, ManureStorageSystem.Slurry_AnaerobicDigestion, OrganicMatterType.PigSlurry, 14.54846, 0)]
    [InlineData(Sector.Pigs, 0, ManureStorageSystem.Slurry_SpreadDirect_NoStorage, OrganicMatterType.PigSlurry, 10.37276, 0)]
    [InlineData(Sector.Pigs, 0, ManureStorageSystem.FYM_Steading, OrganicMatterType.PigFYM, 10.37276, 0)]
    [InlineData(Sector.Pigs, 0, ManureStorageSystem.FYM_FieldHeap, OrganicMatterType.PigFYM, 16.7028, 0)]
    [InlineData(Sector.Pigs, 0, ManureStorageSystem.FYM_SpreadDirect_NoStorage, OrganicMatterType.PigFYM, 10.37276, 0)]
    [InlineData(Sector.Pigs, 0, ManureStorageSystem.FYM_AnaerobicDigestion, OrganicMatterType.PigFYM, 14.54846, 0)]
    [InlineData(Sector.MinorLivestock, 1, ManureStorageSystem.FYM_Steading, OrganicMatterType.MinorLivestockFYM, 16.7028, 0)]
    [InlineData(Sector.MinorLivestock, 1, ManureStorageSystem.FYM_FieldHeap, OrganicMatterType.MinorLivestockFYM, 16.7028, 0)]
    [InlineData(Sector.MinorLivestock, 1, ManureStorageSystem.FYM_SpreadDirect_NoStorage, OrganicMatterType.MinorLivestockFYM, 16.7028, 0)]
    [InlineData(Sector.MinorLivestock, 2, ManureStorageSystem.FYM_Steading, OrganicMatterType.MinorLivestockFYM, 16.7028, 0)]
    [InlineData(Sector.MinorLivestock, 2, ManureStorageSystem.FYM_FieldHeap, OrganicMatterType.MinorLivestockFYM, 16.7028, 0)]
    [InlineData(Sector.MinorLivestock, 2, ManureStorageSystem.FYM_SpreadDirect_NoStorage, OrganicMatterType.MinorLivestockFYM, 16.7028, 0)]
    [InlineData(Sector.MinorLivestock, 3, ManureStorageSystem.FYM_Steading, OrganicMatterType.MinorLivestockFYM, 16.7028, 0)]
    [InlineData(Sector.MinorLivestock, 3, ManureStorageSystem.FYM_FieldHeap, OrganicMatterType.MinorLivestockFYM, 16.7028, 0)]
    [InlineData(Sector.MinorLivestock, 3, ManureStorageSystem.FYM_SpreadDirect_NoStorage, OrganicMatterType.MinorLivestockFYM, 16.7028, 0)]
    [InlineData(Sector.MinorLivestock, 4, ManureStorageSystem.FYM_Steading, OrganicMatterType.MinorLivestockFYM, 16.7028, 0)]
    [InlineData(Sector.MinorLivestock, 4, ManureStorageSystem.FYM_FieldHeap, OrganicMatterType.MinorLivestockFYM, 16.7028, 0)]
    [InlineData(Sector.MinorLivestock, 4, ManureStorageSystem.FYM_SpreadDirect_NoStorage, OrganicMatterType.MinorLivestockFYM, 16.7028, 0)]
    [InlineData(Sector.Poultry, 1, ManureStorageSystem.FYM_Steading, OrganicMatterType.PoultryLitter, 0.3320, 0)]
    [InlineData(Sector.Poultry, 2, ManureStorageSystem.FYM_FieldHeap, OrganicMatterType.PoultryLayerManure, 0.6197, 0)]
    [InlineData(Sector.Poultry, 3, ManureStorageSystem.FYM_SpreadDirect_NoStorage, OrganicMatterType.PoultryLayerManure, 1.0360, 0)]
    [InlineData(Sector.Sheep, (int)SheepType.Ram, ManureStorageSystem.FYM_Steading, OrganicMatterType.SheepFYM, 0.097593813, 0.000487969)]
    [InlineData(Sector.Sheep, (int)SheepType.Ewe, ManureStorageSystem.FYM_Steading, OrganicMatterType.SheepFYM, 0.409564529, 0.002047823)]
    [InlineData(Sector.Sheep, (int)SheepType.Lamb, ManureStorageSystem.FYM_Steading, OrganicMatterType.SheepFYM, 0.311234554, 0.001556173)]
    public void N2ONEmission_AllOutputsCorrect(Sector sector, int animalType, ManureStorageSystem manureStorageSystem, OrganicMatterType organicMatterType, double totalN, double expectedResult)
    {
        var actualResult = StorageManureManagement.N2ONEmission(sector, animalType, manureStorageSystem, organicMatterType, totalN);
        Assert.Equal(expectedResult, actualResult, 4);
    }

    [Theory]
    [InlineData(Sector.Pigs, 0, ManureStorageSystem.Slurry_AboveGroundTanks, OrganicMatterType.PigSlurry, 11.2079, 1.457027)]
    [InlineData(Sector.Pigs, 0, ManureStorageSystem.Slurry_BelowGroundTanks, OrganicMatterType.PigSlurry, 11.2079, 0.784553)]
    [InlineData(Sector.Pigs, 0, ManureStorageSystem.Slurry_EarthBankLagoon, OrganicMatterType.PigSlurry, 11.2079, 5.828108)]
    [InlineData(Sector.Pigs, 0, ManureStorageSystem.Slurry_Bag, OrganicMatterType.PigSlurry, 11.2079, 0.0784553)]
    [InlineData(Sector.Pigs, 0, ManureStorageSystem.Slurry_AnaerobicDigestion, OrganicMatterType.PigSlurry, 14.54846, 1.8912998)]
    [InlineData(Sector.Pigs, 0, ManureStorageSystem.Slurry_SpreadDirect_NoStorage, OrganicMatterType.PigSlurry, 10.37276, 0)]
    [InlineData(Sector.Pigs, 0, ManureStorageSystem.FYM_Steading, OrganicMatterType.PigFYM, 10.37276, 3.2674194)]
    [InlineData(Sector.Pigs, 0, ManureStorageSystem.FYM_FieldHeap, OrganicMatterType.PigFYM, 10.37276, 3.2674194)]
    [InlineData(Sector.Pigs, 0, ManureStorageSystem.FYM_SpreadDirect_NoStorage, OrganicMatterType.PigFYM, 10.37276, 0)]
    [InlineData(Sector.Pigs, 0, ManureStorageSystem.FYM_AnaerobicDigestion, OrganicMatterType.PigFYM, 14.54846, 1.8912998)]
    [InlineData(Sector.MinorLivestock, 1, ManureStorageSystem.FYM_Steading, OrganicMatterType.MinorLivestockFYM, 10.37276, 0)]
    [InlineData(Sector.MinorLivestock, 1, ManureStorageSystem.FYM_FieldHeap, OrganicMatterType.MinorLivestockFYM, 10.37276, 2.7280359)]
    [InlineData(Sector.MinorLivestock, 1, ManureStorageSystem.FYM_SpreadDirect_NoStorage, OrganicMatterType.MinorLivestockFYM, 10.37276, 0)]
    [InlineData(Sector.MinorLivestock, 2, ManureStorageSystem.FYM_Steading, OrganicMatterType.MinorLivestockFYM, 10.37276, 0)]
    [InlineData(Sector.MinorLivestock, 2, ManureStorageSystem.FYM_FieldHeap, OrganicMatterType.MinorLivestockFYM, 10.37276, 2.7280359)]
    [InlineData(Sector.MinorLivestock, 2, ManureStorageSystem.FYM_SpreadDirect_NoStorage, OrganicMatterType.MinorLivestockFYM, 10.37276, 0)]
    [InlineData(Sector.MinorLivestock, 3, ManureStorageSystem.FYM_Steading, OrganicMatterType.MinorLivestockFYM, 10.37276, 0)]
    [InlineData(Sector.MinorLivestock, 3, ManureStorageSystem.FYM_FieldHeap, OrganicMatterType.MinorLivestockFYM, 10.37276, 2.7280359)]
    [InlineData(Sector.MinorLivestock, 3, ManureStorageSystem.FYM_SpreadDirect_NoStorage, OrganicMatterType.MinorLivestockFYM, 10.37276, 0)]
    [InlineData(Sector.MinorLivestock, 4, ManureStorageSystem.FYM_Steading, OrganicMatterType.MinorLivestockFYM, 10.37276, 0)]
    [InlineData(Sector.MinorLivestock, 4, ManureStorageSystem.FYM_FieldHeap, OrganicMatterType.MinorLivestockFYM, 10.37276, 2.7280359)]
    [InlineData(Sector.MinorLivestock, 4, ManureStorageSystem.FYM_SpreadDirect_NoStorage, OrganicMatterType.MinorLivestockFYM, 10.37276, 0)]
    [InlineData(Sector.MinorLivestock, 1, ManureStorageSystem.FYM_FieldHeap, OrganicMatterType.MinorLivestockFYM, 5.37276, 1.4130359)]
    [InlineData(Sector.Poultry, 1, ManureStorageSystem.FYM_Steading, OrganicMatterType.PoultryLitter, 0.2152, 0.020660336)]
    [InlineData(Sector.Sheep, (int)SheepType.Ram, ManureStorageSystem.FYM_Steading, OrganicMatterType.SheepFYM, 0.023441215, 0.00616504)]
    [InlineData(Sector.Sheep, (int)SheepType.Ewe, ManureStorageSystem.FYM_Steading, OrganicMatterType.SheepFYM, 0.098141891, 0.025811317)]
    [InlineData(Sector.Sheep, (int)SheepType.Lamb, ManureStorageSystem.FYM_Steading, OrganicMatterType.SheepFYM, 0.074889453, 0.019695926)]
    public void NH3NEmission_AllOutputsCorrect(Sector sector, int animalType, ManureStorageSystem manureStorageSystem, OrganicMatterType organicMatterType, double totalAmmoniacalNitrogen, double expectedResult)
    {
        var actualResult = StorageManureManagement.NH3NEmission(sector, animalType, manureStorageSystem, organicMatterType, totalAmmoniacalNitrogen);
        Assert.Equal(expectedResult, actualResult, 3);
    }

    [Theory]
    [InlineData(Sector.Pigs, 0, ManureStorageSystem.FYM_AnaerobicDigestion, OrganicMatterType.PigFYM, 0, 0)]
    [InlineData(Sector.MinorLivestock, 1, ManureStorageSystem.FYM_Steading, OrganicMatterType.MinorLivestockFYM, 0, 0)]
    [InlineData(Sector.MinorLivestock, 1, ManureStorageSystem.FYM_FieldHeap, OrganicMatterType.MinorLivestockFYM, 0, 0)]
    [InlineData(Sector.MinorLivestock, 1, ManureStorageSystem.FYM_SpreadDirect_NoStorage, OrganicMatterType.MinorLivestockFYM, 0, 0)]
    [InlineData(Sector.MinorLivestock, 2, ManureStorageSystem.FYM_Steading, OrganicMatterType.MinorLivestockFYM, 0, 0)]
    [InlineData(Sector.MinorLivestock, 2, ManureStorageSystem.FYM_FieldHeap, OrganicMatterType.MinorLivestockFYM, 0, 0)]
    [InlineData(Sector.MinorLivestock, 2, ManureStorageSystem.FYM_SpreadDirect_NoStorage, OrganicMatterType.MinorLivestockFYM, 0, 0)]
    [InlineData(Sector.MinorLivestock, 3, ManureStorageSystem.FYM_Steading, OrganicMatterType.MinorLivestockFYM, 0, 0)]
    [InlineData(Sector.MinorLivestock, 3, ManureStorageSystem.FYM_FieldHeap, OrganicMatterType.MinorLivestockFYM, 0, 0)]
    [InlineData(Sector.MinorLivestock, 3, ManureStorageSystem.FYM_SpreadDirect_NoStorage, OrganicMatterType.MinorLivestockFYM, 0, 0)]
    [InlineData(Sector.MinorLivestock, 4, ManureStorageSystem.FYM_Steading, OrganicMatterType.MinorLivestockFYM, 0, 0)]
    [InlineData(Sector.MinorLivestock, 4, ManureStorageSystem.FYM_FieldHeap, OrganicMatterType.MinorLivestockFYM, 0, 0)]
    [InlineData(Sector.MinorLivestock, 4, ManureStorageSystem.FYM_SpreadDirect_NoStorage, OrganicMatterType.MinorLivestockFYM, 0, 0)]
    [InlineData(Sector.Sheep, (int)SheepType.Ewe, ManureStorageSystem.FYM_Steading, OrganicMatterType.SheepFYM, 0.002047823, 0.000204782)]
    [InlineData(Sector.Sheep, (int)SheepType.Ram, ManureStorageSystem.FYM_Steading, OrganicMatterType.SheepFYM, 0.000487969, 4.87969E-05)]
    [InlineData(Sector.Sheep, (int)SheepType.Lamb, ManureStorageSystem.FYM_Steading, OrganicMatterType.SheepFYM, 0.001556173, 0.000155617)]
    public void NONEmission_AllOutputsCorrect(Sector sector, int animalType, ManureStorageSystem manureStorageSystem, OrganicMatterType organicMatterType, double totalN2ON, double expectedResult)
    {
        var actualResult = StorageManureManagement.NONEmission(sector, animalType, manureStorageSystem, organicMatterType, totalN2ON);
        Assert.Equal(expectedResult, actualResult, 3);
    }

    [Theory]
    [InlineData(Sector.Pigs, 0, ManureStorageSystem.Slurry_AboveGroundTanks, OrganicMatterType.PigSlurry, 0, 0)]
    [InlineData(Sector.Pigs, 0, ManureStorageSystem.FYM_Steading, OrganicMatterType.PigFYM, 0, 0)]
    [InlineData(Sector.Pigs, 0, ManureStorageSystem.Slurry_BelowGroundTanks, OrganicMatterType.PigSlurry, 0, 0)]
    [InlineData(Sector.Pigs, 0, ManureStorageSystem.Slurry_EarthBankLagoon, OrganicMatterType.PigSlurry, 0, 0)]
    [InlineData(Sector.Pigs, 0, ManureStorageSystem.Slurry_Bag, OrganicMatterType.PigSlurry, 0, 0)]
    [InlineData(Sector.Pigs, 0, ManureStorageSystem.Slurry_AnaerobicDigestion, OrganicMatterType.PigSlurry, 0, 0)]
    [InlineData(Sector.Pigs, 0, ManureStorageSystem.Slurry_SpreadDirect_NoStorage, OrganicMatterType.PigSlurry, 0, 0)]
    [InlineData(Sector.Pigs, 0, ManureStorageSystem.FYM_FieldHeap, OrganicMatterType.PigFYM, 0, 0)]
    [InlineData(Sector.Pigs, 0, ManureStorageSystem.FYM_SpreadDirect_NoStorage, OrganicMatterType.PigFYM, 0, 0)]
    [InlineData(Sector.Pigs, 0, ManureStorageSystem.FYM_AnaerobicDigestion, OrganicMatterType.PigFYM, 0, 0)]
    [InlineData(Sector.MinorLivestock, 1, ManureStorageSystem.FYM_Steading, OrganicMatterType.MinorLivestockFYM, 0, 0)]
    [InlineData(Sector.MinorLivestock, 1, ManureStorageSystem.FYM_FieldHeap, OrganicMatterType.MinorLivestockFYM, 0, 0)]
    [InlineData(Sector.MinorLivestock, 1, ManureStorageSystem.FYM_SpreadDirect_NoStorage, OrganicMatterType.MinorLivestockFYM, 0, 0)]
    [InlineData(Sector.MinorLivestock, 2, ManureStorageSystem.FYM_Steading, OrganicMatterType.MinorLivestockFYM, 0, 0)]
    [InlineData(Sector.MinorLivestock, 2, ManureStorageSystem.FYM_FieldHeap, OrganicMatterType.MinorLivestockFYM, 0, 0)]
    [InlineData(Sector.MinorLivestock, 2, ManureStorageSystem.FYM_SpreadDirect_NoStorage, OrganicMatterType.MinorLivestockFYM, 0, 0)]
    [InlineData(Sector.MinorLivestock, 3, ManureStorageSystem.FYM_Steading, OrganicMatterType.MinorLivestockFYM, 0, 0)]
    [InlineData(Sector.MinorLivestock, 3, ManureStorageSystem.FYM_FieldHeap, OrganicMatterType.MinorLivestockFYM, 0, 0)]
    [InlineData(Sector.MinorLivestock, 3, ManureStorageSystem.FYM_SpreadDirect_NoStorage, OrganicMatterType.MinorLivestockFYM, 0, 0)]
    [InlineData(Sector.MinorLivestock, 4, ManureStorageSystem.FYM_Steading, OrganicMatterType.MinorLivestockFYM, 0, 0)]
    [InlineData(Sector.MinorLivestock, 4, ManureStorageSystem.FYM_FieldHeap, OrganicMatterType.MinorLivestockFYM, 0, 0)]
    [InlineData(Sector.MinorLivestock, 4, ManureStorageSystem.FYM_SpreadDirect_NoStorage, OrganicMatterType.MinorLivestockFYM, 0, 0)]
    [InlineData(Sector.Sheep, (int)SheepType.Ram, ManureStorageSystem.FYM_Steading, OrganicMatterType.SheepFYM, 0.000487969, 0.001463907)]
    [InlineData(Sector.Sheep, (int)SheepType.Ewe, ManureStorageSystem.FYM_Steading, OrganicMatterType.SheepFYM, 0.002047823, 0.006143468)]
    [InlineData(Sector.Sheep, (int)SheepType.Lamb, ManureStorageSystem.FYM_Steading, OrganicMatterType.SheepFYM, 0.001556173, 0.004668518)]
    public void N2NEmission_AllOutputsCorrect(Sector sector, int animalType, ManureStorageSystem manureStorageSystem, OrganicMatterType organicMatterType, double totalN2ON, double expectedResult)
    {
        var actualResult = StorageManureManagement.N2NEmission(sector, animalType, manureStorageSystem, organicMatterType, totalN2ON);
        Assert.Equal(expectedResult, actualResult, 3);
    }

    [Theory]
    [InlineData(Sector.Pigs, 1, ManureStorageSystem.Slurry_AboveGroundTanks, OrganicMatterType.PigSlurry, 167.9, 8.6057145)]
    [InlineData(Sector.Pigs, 1, ManureStorageSystem.Slurry_BelowGroundTanks, OrganicMatterType.PigSlurry, 167.9, 8.6057145)]
    [InlineData(Sector.Pigs, 1, ManureStorageSystem.Slurry_EarthBankLagoon, OrganicMatterType.PigSlurry, 167.9, 8.6057145)]
    [InlineData(Sector.Pigs, 1, ManureStorageSystem.Slurry_Bag, OrganicMatterType.PigSlurry, 167.9, 2.024874)]
    [InlineData(Sector.Pigs, 1, ManureStorageSystem.Slurry_AnaerobicDigestion, OrganicMatterType.PigSlurry, 167.9, 2.024874)]
    [InlineData(Sector.Pigs, 1, ManureStorageSystem.Slurry_SpreadDirect_NoStorage, OrganicMatterType.PigSlurry, 167.9, 0.05062185)]
    [InlineData(Sector.Pigs, 1, ManureStorageSystem.FYM_Steading, OrganicMatterType.PigFYM, 167.9, 8.6057145)]
    [InlineData(Sector.Pigs, 1, ManureStorageSystem.FYM_FieldHeap, OrganicMatterType.PigFYM, 167.9, 8.6057145)]
    [InlineData(Sector.Pigs, 1, ManureStorageSystem.FYM_SpreadDirect_NoStorage, OrganicMatterType.PigFYM, 167.9, 0.05062185)]
    [InlineData(Sector.Pigs, 1, ManureStorageSystem.FYM_AnaerobicDigestion, OrganicMatterType.PigFYM, 167.9, 2.024874)]
    [InlineData(Sector.Pigs, 2, ManureStorageSystem.FYM_SpreadDirect_NoStorage, OrganicMatterType.PigFYM, 167.9, 0.05062185)]
    [InlineData(Sector.Pigs, 2, ManureStorageSystem.FYM_AnaerobicDigestion, OrganicMatterType.PigFYM, 167.9, 2.024874)]
    [InlineData(Sector.Pigs, 3, ManureStorageSystem.FYM_SpreadDirect_NoStorage, OrganicMatterType.PigFYM, 167.9, 0.05062185)]
    [InlineData(Sector.Pigs, 3, ManureStorageSystem.FYM_AnaerobicDigestion, OrganicMatterType.PigFYM, 167.9, 2.024874)]
    [InlineData(Sector.Pigs, 4, ManureStorageSystem.Slurry_SpreadDirect_NoStorage, OrganicMatterType.PigSlurry, 109.5, 0.03301425)]
    [InlineData(Sector.Pigs, 4, ManureStorageSystem.Slurry_Bag, OrganicMatterType.PigSlurry, 109.5, 1.32057)]
    [InlineData(Sector.Pigs, 5, ManureStorageSystem.FYM_Steading, OrganicMatterType.PigFYM, 109.5, 5.6124225)]
    [InlineData(Sector.MinorLivestock, 1, ManureStorageSystem.FYM_Steading, OrganicMatterType.MinorLivestockFYM, 109.5, 0)]
    [InlineData(Sector.MinorLivestock, 1, ManureStorageSystem.FYM_FieldHeap, OrganicMatterType.MinorLivestockFYM, 109.5, 0)]
    [InlineData(Sector.MinorLivestock, 1, ManureStorageSystem.FYM_SpreadDirect_NoStorage, OrganicMatterType.MinorLivestockFYM, 777.45, 0)]
    [InlineData(Sector.MinorLivestock, 2, ManureStorageSystem.FYM_Steading, OrganicMatterType.MinorLivestockFYM, 777.45, 0)]
    [InlineData(Sector.MinorLivestock, 2, ManureStorageSystem.FYM_FieldHeap, OrganicMatterType.MinorLivestockFYM, 109.5, 0)]
    [InlineData(Sector.MinorLivestock, 2, ManureStorageSystem.FYM_SpreadDirect_NoStorage, OrganicMatterType.MinorLivestockFYM, 109.5, 0)]
    [InlineData(Sector.MinorLivestock, 3, ManureStorageSystem.FYM_Steading, OrganicMatterType.MinorLivestockFYM, 109.5, 0)]
    [InlineData(Sector.MinorLivestock, 3, ManureStorageSystem.FYM_FieldHeap, OrganicMatterType.MinorLivestockFYM, 109.5, 0)]
    [InlineData(Sector.MinorLivestock, 3, ManureStorageSystem.FYM_SpreadDirect_NoStorage, OrganicMatterType.MinorLivestockFYM, 777.45, 0)]
    [InlineData(Sector.MinorLivestock, 4, ManureStorageSystem.FYM_Steading, OrganicMatterType.MinorLivestockFYM, 777.45, 0)]
    [InlineData(Sector.MinorLivestock, 4, ManureStorageSystem.FYM_FieldHeap, OrganicMatterType.MinorLivestockFYM, 777.45, 0)]
    [InlineData(Sector.MinorLivestock, 4, ManureStorageSystem.FYM_SpreadDirect_NoStorage, OrganicMatterType.MinorLivestockFYM, 109.5, 0)]
    public void CH4Emission_AllOutputsCorrect(Sector sector, int animalType, ManureStorageSystem manureStorageSystem, OrganicMatterType organicMatterType, double volatileSolids, double expectedResult)
    {
        var actualResult = StorageManureManagement.CH4Emission(sector, animalType, manureStorageSystem, organicMatterType, volatileSolids);
        Assert.Equal(expectedResult, actualResult, 3);
    }
}
