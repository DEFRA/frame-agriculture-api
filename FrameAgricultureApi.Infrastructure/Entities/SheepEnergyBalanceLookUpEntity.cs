using MongoDB.Bson.Serialization.Attributes;

namespace FrameAgricultureApi.Infrastructure.Entities;

/// <summary>
/// Lookup document from the Sheep_EnergyBalance collection.
/// </summary>
[BsonCollection("Sheep_EnergyBalance")]
public class SheepEnergyBalanceLookUpEntity : MongoDocument
{
    [BsonElement("COUNTRY")]
    public int Country { get; set; }

    [BsonElement("DIETTYPE")]
    public int DietType { get; set; }

    [BsonElement("FIELDAVAILABLENITROGEN")]
    public double FieldAvailableNitrogen { get; set; }

    [BsonElement("FIELDAVAILABLENITROGENCP100")]
    public double FieldAvailableNitrogenCp100 { get; set; }

    [BsonElement("FIELDAVAILABLENITROGENCP200")]
    public double FieldAvailableNitrogenCp200 { get; set; }

    [BsonElement("FIELDDAYS")]
    public double FieldDays { get; set; }

    [BsonElement("FIELDDUNGNITROGEN")]
    public double FieldDungNitrogen { get; set; }

    [BsonElement("FIELDDUNGNITROGENCP100")]
    public double FieldDungNitrogenCp100 { get; set; }

    [BsonElement("FIELDDUNGNITROGENCP200")]
    public double FieldDungNitrogenCp200 { get; set; }

    [BsonElement("FIELDENTERICMETHANE")]
    public double FieldEntericMethane { get; set; }

    [BsonElement("FIELDEXCRETAMASS")]
    public double FieldExcretaMass { get; set; }

    [BsonElement("FIELDEXCRETAVOLUME")]
    public double FieldExcretaVolume { get; set; }

    [BsonElement("FIELDGROSSENERGY")]
    public double FieldGrossEnergy { get; set; }

    [BsonElement("FIELDMETABOLISABLEENERGY")]
    public double FieldMetabolisableEnergy { get; set; }

    [BsonElement("FIELDNITROGENINTAKE")]
    public double FieldNitrogenIntake { get; set; }

    [BsonElement("FIELDNITROGENINTAKECP100")]
    public double FieldNitrogenIntakeCp100 { get; set; }

    [BsonElement("FIELDNITROGENINTAKECP200")]
    public double FieldNitrogenIntakeCp200 { get; set; }

    [BsonElement("FIELDRYMATTERINTAKE")]
    public double FieldDryMatterIntake { get; set; }

    [BsonElement("FIELDTOTALNITROGEN")]
    public double FieldTotalNitrogen { get; set; }

    [BsonElement("FIELDTOTALNITROGENCP100")]
    public double FieldTotalNitrogenCp100 { get; set; }

    [BsonElement("FIELDTOTALNITROGENCP200")]
    public double FieldTotalNitrogenCp200 { get; set; }

    [BsonElement("FIELDURINENITROGEN")]
    public double FieldUrineNitrogen { get; set; }

    [BsonElement("FIELDURINENITROGENCP100")]
    public double FieldUrineNitrogenCp100 { get; set; }

    [BsonElement("FIELDURINENITROGENCP200")]
    public double FieldUrineNitrogenCp200 { get; set; }

    [BsonElement("FIELDVOLATILESOLIDS")]
    public double FieldVolatileSolids { get; set; }

    [BsonElement("HOUSEAVAILABLENITROGEN")]
    public double HouseAvailableNitrogen { get; set; }

    [BsonElement("HOUSEDAYS")]
    public double HouseDays { get; set; }

    [BsonElement("HOUSEDRYMATTERINTAKE")]
    public double HouseDryMatterIntake { get; set; }

    [BsonElement("HOUSEDUNGNITROGEN")]
    public double HouseDungNitrogen { get; set; }

    [BsonElement("HOUSEENTERICMETHANE")]
    public double HouseEntericMethane { get; set; }

    [BsonElement("HOUSEEXCRETAMASS")]
    public double HouseExcretaMass { get; set; }

    [BsonElement("HOUSEEXCRETAVOLUME")]
    public double HouseExcretaVolume { get; set; }

    [BsonElement("HOUSEGROSSENERGY")]
    public double HouseGrossEnergy { get; set; }

    [BsonElement("HOUSEMANUREMASS")]
    public double HouseManureMass { get; set; }

    [BsonElement("HOUSEMANUREVOLUME")]
    public double HouseManureVolume { get; set; }

    [BsonElement("HOUSEMETABOLISABLEENERGY")]
    public double HouseMetabolisableEnergy { get; set; }

    [BsonElement("HOUSENITROGENINTAKE")]
    public double HouseNitrogenIntake { get; set; }

    [BsonElement("HOUSETOTALNITROGEN")]
    public double HouseTotalNitrogen { get; set; }

    [BsonElement("HOUSEURINENITROGEN")]
    public double HouseUrineNitrogen { get; set; }

    [BsonElement("HOUSEVOLATILESOLIDS")]
    public double HouseVolatileSolids { get; set; }

    [BsonElement("SHEEPTYPE")]
    public int SheepType { get; set; }

    [BsonElement("SUBTYPE")]
    public int SubType { get; set; }

    [BsonElement("SYSTEMTYPE")]
    public int SystemType { get; set; }

    [BsonElement("YEAR")]
    public int Year { get; set; }
}