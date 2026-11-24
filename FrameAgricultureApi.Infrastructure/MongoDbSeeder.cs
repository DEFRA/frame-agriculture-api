using FrameAgricultureApi.Infrastructure.Entities;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Driver;

namespace FrameAgricultureApi.Infrastructure;

public class MongoDbSeeder(IMongoDatabase database)
{
    public async Task SeedAsync()
    {
        await DeleteCollectionDataAsync<BeefCattleLiveWeightParametersLookUpEntity>("BeefCattleLiveWeightParameters");
        await DeleteCollectionDataAsync<CattleManureSpreadingLookUpEntity>("CattleManureSpreading");
        await DeleteCollectionDataAsync<CoverCropLookUpEntity>("CoverCrops");
        await DeleteCollectionDataAsync<CropResidueLookUpEntity>("Crop_Residue");
        await DeleteCollectionDataAsync<DairyCattleLiveWeightLookUpEntity>("DairyCattleLiveWeight");
        await DeleteCollectionDataAsync<Enteric_CH4_LookUpEntity>("EntericMethane");
        await DeleteCollectionDataAsync<ExcretaLookUpEntity>("Excreta");
        await DeleteCollectionDataAsync<FertiliserMitigationLookUpEntity>("FertiliserMitigation");
        await DeleteCollectionDataAsync<FertiliserRatioEfsLookUpEntity>("Fertiliser_RatioEFs");
        await DeleteCollectionDataAsync<GrassResidueDefaultEmissionFactorsLookUpEntity>("GrassResidue_DefaultEmissionFactors");
        await DeleteCollectionDataAsync<HousingMitigationLookUpEntity>("HousingMitigation");
        await DeleteCollectionDataAsync<ManureManagementHousingLookUpEntity>("ManureManagement_Housing");
        await DeleteCollectionDataAsync<ManureManagementStorageLookUpEntity>("ManureManagement_Storage");
        await DeleteCollectionDataAsync<ManureManagementYardsLookUpEntity>("ManureManagement_Yards");
        await DeleteCollectionDataAsync<CH4_B0LookUpEntity>("MethaneB0");
        await DeleteCollectionDataAsync<MinorLivestockManureSpreadingLookUpEntity>("MinorLivestockManureSpreading");
        await DeleteCollectionDataAsync<OrganicMatterSpreadingLookUpEntity>("OrganicMatterSpreading");
        await DeleteCollectionDataAsync<OutdoorExcretionLookUpEntity>("OutdoorExcretion");
        await DeleteCollectionDataAsync<PigManureSpreadingLookUpEntity>("PigManureSpreading");
        await DeleteCollectionDataAsync<PoultryManureSpreadingLookUpEntity>("PoultryManureSpreading");
        await DeleteCollectionDataAsync<ResidueMitigationLookUpEntity>("ResidueMitigation");
        await DeleteCollectionDataAsync<SheepGeneralCoefficientsLookUpEntity>("Sheep_GeneralCoefficients");
        await DeleteCollectionDataAsync<SheepManureSpreadingLookUpEntity>("SheepManureSpreading");
        await DeleteCollectionDataAsync<SpreadingMitigationLookUpEntity>("SpreadingMitigation");
        await DeleteCollectionDataAsync<StorageMitigationLookUpEntity>("StorageMitigation");
        await DeleteCollectionDataAsync<YardMitigationLookUpEntity>("YardMitigation");

        await SeedCollectionAsync<BeefCattleLiveWeightParametersLookUpEntity>("BeefCattleLiveWeightParameters.json", "BeefCattleLiveWeightParameters");
        await SeedCollectionAsync<CattleManureSpreadingLookUpEntity>("CattleManureSpreading.json", "CattleManureSpreading");
        await SeedCollectionAsync<CoverCropLookUpEntity>("CoverCrops.json", "CoverCrops");
        await SeedCollectionAsync<CropResidueLookUpEntity>("CropResidue.json", "Crop_Residue");
        await SeedCollectionAsync<DairyCattleLiveWeightLookUpEntity>("DairyCattleLiveWeight.json", "DairyCattleLiveWeight");
        await SeedCollectionAsync<Enteric_CH4_LookUpEntity>("EntericMethane.json", "EntericMethane");
        await SeedCollectionAsync<ExcretaLookUpEntity>("Excreta.json", "Excreta");
        await SeedCollectionAsync<FertiliserMitigationLookUpEntity>("FertiliserMitigation.json", "FertiliserMitigation");
        await SeedCollectionAsync<FertiliserRatioEfsLookUpEntity>("FertiliserRatioEFs.json", "Fertiliser_RatioEFs");
        await SeedCollectionAsync<GrassResidueDefaultEmissionFactorsLookUpEntity>("GrassResidue_DefaultEmissionFactors.json", "GrassResidue_DefaultEmissionFactors");
        await SeedCollectionAsync<HousingMitigationLookUpEntity>("HousingMitigation.json", "HousingMitigation");
        await SeedCollectionAsync<ManureManagementHousingLookUpEntity>("ManureManagement_Housing.json", "ManureManagement_Housing");
        await SeedCollectionAsync<ManureManagementStorageLookUpEntity>("ManureManagement_Storage.json", "ManureManagement_Storage");
        await SeedCollectionAsync<ManureManagementYardsLookUpEntity>("ManureManagement_Yards.json", "ManureManagement_Yards");
        await SeedCollectionAsync<CH4_B0LookUpEntity>("MethaneB0.json", "MethaneB0");
        await SeedCollectionAsync<MinorLivestockManureSpreadingLookUpEntity>("MinorLivestockManureSpreading.json", "MinorLivestockManureSpreading");
        await SeedCollectionAsync<OrganicMatterSpreadingLookUpEntity>("OrganicMatterSpreading.json", "OrganicMatterSpreading");
        await SeedCollectionAsync<OutdoorExcretionLookUpEntity>("OutdoorExcretion.json", "OutdoorExcretion");
        await SeedCollectionAsync<PigManureSpreadingLookUpEntity>("PigManureSpreading.json", "PigManureSpreading");
        await SeedCollectionAsync<PoultryManureSpreadingLookUpEntity>("PoultryManureSpreading.json", "PoultryManureSpreading");
        await SeedCollectionAsync<ResidueMitigationLookUpEntity>("ResidueMitigation.json", "ResidueMitigation");
        await SeedCollectionAsync<SheepGeneralCoefficientsLookUpEntity>("Sheep_GeneralCoefficients.json", "Sheep_GeneralCoefficients");
        await SeedCollectionAsync<SheepManureSpreadingLookUpEntity>("SheepManureSpreading.json", "SheepManureSpreading");
        await SeedCollectionAsync<SpreadingMitigationLookUpEntity>("SpreadingMitigation.json", "SpreadingMitigation");
        await SeedCollectionAsync<StorageMitigationLookUpEntity>("StorageMitigation.json", "StorageMitigation");
        await SeedCollectionAsync<YardMitigationLookUpEntity>("YardMitigation.json", "YardMitigation");
    }

    private async Task DeleteCollectionDataAsync<TDocument>(string collectionName)
        where TDocument : MongoDocument
    {
        var collection = database.GetCollection<TDocument>(collectionName);
        await collection.DeleteManyAsync(Builders<TDocument>.Filter.Empty);
    }

    private async Task SeedCollectionAsync<TDocument>(string fileName, string collectionName)
        where TDocument : MongoDocument
    {
        using var stream = OpenSeedData(fileName);
        using var reader = new StreamReader(stream);
        var documents = BsonSerializer.Deserialize<BsonArray>(await reader.ReadToEndAsync());
        var classMap = BsonClassMap.LookupClassMap(typeof(TDocument));
        var collection = database.GetCollection<TDocument>(collectionName);

        foreach (var document in documents)
        {
            var mappedDocument = new BsonDocument();
            foreach (var element in document.AsBsonDocument)
            {
                var memberMap = classMap.AllMemberMaps.FirstOrDefault(member =>
                    member.ElementName == element.Name || member.MemberName == element.Name)
                    ?? throw new InvalidOperationException($"Unknown field '{element.Name}' in {fileName}.");
                mappedDocument.Add(memberMap.ElementName, element.Value);
            }

            var entity = BsonSerializer.Deserialize<TDocument>(mappedDocument);
            await collection.InsertOneAsync(entity);
        }
    }

    private static Stream OpenSeedData(string fileName)
    {
        return typeof(MongoDbSeeder).Assembly.GetManifestResourceStream(
            $"FrameAgricultureApi.Infrastructure.SeedData.{fileName}")
            ?? throw new InvalidOperationException($"Seed data resource '{fileName}' was not found.");
    }
}