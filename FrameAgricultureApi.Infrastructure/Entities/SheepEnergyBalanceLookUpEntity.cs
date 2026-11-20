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
    public double FieldAvailable_N { get; set; }

    [BsonElement("FIELDAVAILABLENITROGENCP100")]
    public double FieldAvailable_N_Cp100 { get; set; }

    [BsonElement("FIELDAVAILABLENITROGENCP200")]
    public double FieldAvailable_N_Cp200 { get; set; }

    [BsonElement("FIELDDAYS")]
    public double FieldDays { get; set; }

    [BsonElement("FIELDDUNGNITROGEN")]
    public double FieldDung_N { get; set; }

    [BsonElement("FIELDDUNGNITROGENCP100")]
    public double FieldDung_N_Cp100 { get; set; }

    [BsonElement("FIELDDUNGNITROGENCP200")]
    public double FieldDung_N_Cp200 { get; set; }

    [BsonElement("FIELDENTERICMETHANE")]
    public double FieldEnteric_CH4 { get; set; }

    [BsonElement("FIELDEXCRETAMASS")]
    public double FieldExcretaMass { get; set; }

    [BsonElement("FIELDEXCRETAVOLUME")]
    public double FieldExcretaVolume { get; set; }

    [BsonElement("FIELDGROSSENERGY")]
    public double FieldGrossEnergy { get; set; }

    [BsonElement("FIELDMETABOLISABLEENERGY")]
    public double FieldMetabolisableEnergy { get; set; }

    [BsonElement("FIELDNITROGENINTAKE")]
    public double Field_N_Intake { get; set; }

    [BsonElement("FIELDNITROGENINTAKECP100")]
    public double Field_N_IntakeCp100 { get; set; }

    [BsonElement("FIELDNITROGENINTAKECP200")]
    public double Field_N_IntakeCp200 { get; set; }

    [BsonElement("FIELDRYMATTERINTAKE")]
    public double FieldDryMatterIntake { get; set; }

    [BsonElement("FIELDTOTALNITROGEN")]
    public double FieldTotal_N { get; set; }

    [BsonElement("FIELDTOTALNITROGENCP100")]
    public double FieldTotal_N_Cp100 { get; set; }

    [BsonElement("FIELDTOTALNITROGENCP200")]
    public double FieldTotal_N_Cp200 { get; set; }

    [BsonElement("FIELDURINENITROGEN")]
    public double FieldUrine_N { get; set; }

    [BsonElement("FIELDURINENITROGENCP100")]
    public double FieldUrine_N_Cp100 { get; set; }

    [BsonElement("FIELDURINENITROGENCP200")]
    public double FieldUrine_N_Cp200 { get; set; }

    [BsonElement("FIELDVOLATILESOLIDS")]
    public double FieldVolatileSolids { get; set; }

    [BsonElement("HOUSEAVAILABLENITROGEN")]
    public double HouseAvailable_N { get; set; }

    [BsonElement("HOUSEDAYS")]
    public double HouseDays { get; set; }

    [BsonElement("HOUSEDRYMATTERINTAKE")]
    public double HouseDryMatterIntake { get; set; }

    [BsonElement("HOUSEDUNGNITROGEN")]
    public double HouseDung_N { get; set; }

    [BsonElement("HOUSEENTERICMETHANE")]
    public double HouseEnteric_CH4 { get; set; }

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
    public double House_N_Intake { get; set; }

    [BsonElement("HOUSETOTALNITROGEN")]
    public double HouseTotal_N { get; set; }

    [BsonElement("HOUSEURINENITROGEN")]
    public double HouseUrine_N { get; set; }

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