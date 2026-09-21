using FrameAgricultureApi.CustomErrorHandling;
using FrameAgricultureApi.Generic;
using System.Reflection;
using static FrameAgricultureApi.Enumerators.Enumerators;

namespace FrameAgricultureApi.Libraries.LookUps;

/// <summary>
/// Sheep Lookup class - singleton with all LUTs for sheep
/// </summary>
public class SheepLookup
{
    /// <summary>
    /// Private instance
    /// </summary>
    private static SheepLookup? _instance;
    /// <summary>
    /// public Instance
    /// </summary>
    public static SheepLookup Instance
    {
        get
        {
            _instance ??= new SheepLookup();
            return _instance;
        }
    }

    //Dictionary Delcaration
    private readonly Dictionary<Tuple<int, int>, double> FirstWinterManureFracLeach = []; //index = cell, soiltype (could use climate region, but would need to ignore entries as climate region can sapn multiple cells)
    private readonly Dictionary<Tuple<int, int, int, int>, SheepEnergyBalance> EnergyBalanceParameters = []; //INdexed by country, sheeptype, subtype, systemtype

    //Parameter Declaration
    private readonly double manureMethaneProducingCapacity = double.NaN;
    private readonly double emissionFactor_Ammonia_GrazedExcreta = double.NaN;
    private readonly double emissionFactor_Ammonia_HousedExcreta = double.NaN;
    private readonly double emissionFactor_Ammonia_SpreadManure = double.NaN;
    private readonly double emissionFactor_Ammonia_StoredManure = double.NaN;
    private readonly double emissionFactor_Nitrate_StoredManure = double.NaN;
    private readonly double emissionFactor_NitrousOxide_AtmosphericDeposition = double.NaN;
    private readonly double emissionFactor_NitrousOxide_GrazedDung = double.NaN;
    private readonly double emissionFactor_NitricOxide_GrazedDung = double.NaN;
    private readonly double emissionFactor_NitrousOxide_GrazedUrine = double.NaN;
    private readonly double emissionFactor_NitricOxide_GrazedUrine = double.NaN;
    private readonly double emissionFactor_NitrousOxide_HousedExcreta = double.NaN;
    private readonly double emissionFactor_NitrousOxide_LeachedNitrate = double.NaN;
    private readonly double emissionFactor_NitrousOxide_SpreadManure = double.NaN;
    private readonly double conversionFactor_Methane_GrazedExcreta = double.NaN;
    private readonly double conversionFactor_Methane_HousedExcreta = double.NaN;
    private readonly double uncertainty_EnergyBalanceInput = double.NaN;
    private readonly double uncertainty_EnergyBalance = double.NaN;
    private readonly double uncertainty_Coefficient_Methane_DryMatter = double.NaN;

    private SheepLookup()
    {
        FirstWinterManureFracLeach.Clear();
        EnergyBalanceParameters.Clear();
        try
        {
            using MemoryStream ms = HelperFunctions.GetLUT(Assembly.GetExecutingAssembly(), "FLEA_Sheep_GeneralCoefficients_LUT.dat");
            using BinaryReader br = new(ms);

            while(br.BaseStream.Position < br.BaseStream.Length)
            {
                manureMethaneProducingCapacity = br.ReadDouble();
                emissionFactor_Ammonia_GrazedExcreta = br.ReadDouble();
                emissionFactor_Ammonia_HousedExcreta = br.ReadDouble();
                emissionFactor_Ammonia_SpreadManure = br.ReadDouble();
                emissionFactor_Ammonia_StoredManure = br.ReadDouble();
                emissionFactor_Nitrate_StoredManure = br.ReadDouble();
                emissionFactor_NitrousOxide_AtmosphericDeposition = br.ReadDouble();
                emissionFactor_NitrousOxide_GrazedDung = br.ReadDouble();
                emissionFactor_NitricOxide_GrazedDung = br.ReadDouble();
                emissionFactor_NitrousOxide_GrazedUrine = br.ReadDouble();
                emissionFactor_NitricOxide_GrazedUrine = br.ReadDouble();
                emissionFactor_NitrousOxide_HousedExcreta = br.ReadDouble();
                emissionFactor_NitrousOxide_LeachedNitrate = br.ReadDouble();
                emissionFactor_NitrousOxide_SpreadManure = br.ReadDouble();
                conversionFactor_Methane_GrazedExcreta = br.ReadDouble();
                conversionFactor_Methane_HousedExcreta = br.ReadDouble();
                uncertainty_EnergyBalanceInput = br.ReadDouble();
                uncertainty_EnergyBalance = br.ReadDouble();
                uncertainty_Coefficient_Methane_DryMatter = br.ReadDouble();
            }
        }
        catch(Exception excep)
        {
            throw new CustomAppException("Error loading general coefficients from file: " + excep.Message + ".");
        }

        try
        {
            using MemoryStream ms = HelperFunctions.GetLUT(Assembly.GetExecutingAssembly(), "FLEA_Sheep_FirstWinterManureFracLeach_LUT.dat");
            using BinaryReader br = new(ms);
            while(br.BaseStream.Position < br.BaseStream.Length)
            {
                int cell = br.ReadInt32();
                int climterefgion = br.ReadInt32();
                int soiltype = br.ReadInt32();

                double firstWinterManureFracLeach = br.ReadDouble();
                double AAR = br.ReadDouble();

                Tuple<int, int> key = new(cell, soiltype);
                if(!FirstWinterManureFracLeach.TryAdd(key, firstWinterManureFracLeach))
                {
                    throw new Exception("Unable to add value to First Winter Manure Frac Leach dictionary with key " + key.Item1.ToString() + " - " + key.Item2.ToString());
                }
            }
        }
        catch(Exception excep)
        {
            throw new CustomAppException("Error loading first winter manure frac leach from file: " + excep.Message + ".");
        }

        try
        {
            using MemoryStream ms = HelperFunctions.GetLUT(Assembly.GetExecutingAssembly(), "FLEA_Sheep_EnergyBalance_LUT.dat");
            using BinaryReader br = new(ms);
            while(br.BaseStream.Position < br.BaseStream.Length)
            {
                int year = br.ReadInt32();
                int country = br.ReadInt32();
                int diet = br.ReadInt32();
                int sheeptype = br.ReadInt32();
                int subtype = br.ReadInt32();
                int systemtype = br.ReadInt32();

                SheepEnergyBalance parameters = new(br.ReadDouble(), br.ReadDouble(), br.ReadDouble(), br.ReadDouble(), br.ReadDouble(),
                    br.ReadDouble(), br.ReadDouble(), br.ReadDouble(), br.ReadDouble(), br.ReadDouble(),
                    br.ReadDouble(), br.ReadDouble(), br.ReadDouble(), br.ReadDouble(), br.ReadDouble(),
                    br.ReadDouble(), br.ReadDouble(), br.ReadDouble(), br.ReadDouble(), br.ReadDouble(),
                    br.ReadDouble(), br.ReadDouble(), br.ReadDouble(), br.ReadDouble(), br.ReadDouble(),
                    br.ReadDouble(), br.ReadDouble(), br.ReadDouble(), br.ReadDouble(), br.ReadDouble(),
                    br.ReadDouble(), br.ReadDouble(), br.ReadDouble(), br.ReadDouble(), br.ReadDouble(),
                    br.ReadDouble(), br.ReadDouble(), br.ReadDouble());

                Tuple<int, int, int, int> key = new(country, sheeptype, subtype, systemtype);

                if(!EnergyBalanceParameters.TryAdd(key, parameters))
                {
                    throw new CustomAppException("Error adding energy balance parameters to dictionary for key: " + key.Item1 + "-" +
                        key.Item2 + "-" + key.Item3 + "-" + key.Item4 + ".");
                }

            }

        }
        catch(Exception excep)
        {
            throw new CustomAppException("Error loading sheep energy balance data from file:" + excep.Message + ".\nThe singleton has not been created.");
        }
    }
    /// <summary>
    /// Returns the Energy parameters for the specific combinatino of keying variables (country, sheep type, sheep subtype and system type)
    /// </summary>
    /// <param name="country">country enumerator as integer</param>
    /// <param name="sheeptype">sheep type enumerator as integer</param>
    /// <param name="subtype">sheep subtype enumerator as integer</param>
    /// <param name="systemtype">sheep system type enumerator as integer</param>
    /// <returns></returns>
    /// <exception cref="CustomAppException"></exception>
    public SheepEnergyBalance EnergyParameters(int country, int sheeptype, int subtype, int systemtype)
    {
        Tuple<int, int, int, int> key = new(country, sheeptype, subtype, systemtype);
        if(EnergyBalanceParameters.TryGetValue(key, out var parameters))
        {
            return parameters;
        }
        else
        {
            Country mycountry = (Country)key.Item1;
            SheepType mysheep = (SheepType)key.Item2;
            string mysubtype = mysheep.Equals(SheepType.Lamb) ? ((LambSubtype)key.Item3).ToString() : (mysheep.Equals(SheepType.Ewe) ?
                ((EweSubtype)key.Item3).ToString() : ((RamSubtype)key.Item3).ToString());
            SheepSystemType mysystemtype = (SheepSystemType)key.Item4;

            throw new CustomAppException("Unable to find energy balance parameters for " + mycountry.ToString() + " - " + mysheep.ToString() + " - " +
                    mysubtype + " - " + mysystemtype.ToString());
        }
    }
    /// <summary>
    /// Returns first winter manure frac leach for given 10km by 10km grid square and soil type
    /// </summary>
    /// <param name="cell">The UK 10km by 10 km grid cell ID</param>
    /// <param name="soiltype">Soil type enumerator as integer</param>
    /// <returns>The first winter manure frac leach</returns>
    /// <exception cref="CustomAppException">Exception if key does not exist</exception>
    public double SheepFirstWinterManureFracLeach(int cell, int soiltype)
    {
        Tuple<int, int> key = new(cell, soiltype);

        if(FirstWinterManureFracLeach.TryGetValue(key, out var parameters))
        {
            return parameters;
        }
        else
        {
            throw new CustomAppException("Unable to retrieve first winter manure frac leach for cell " + key.Item1.ToString() + " and soiltype " +
            key.Item2.ToString() + ".");
        }
    }
}

/// <summary>
/// Sheep energy balance parameter class
/// </summary>
public class SheepEnergyBalance
{
    /// <summary>
    /// Sheep energy balance class constructor
    /// </summary>
    public SheepEnergyBalance() { }

    /// <summary>
    /// Sheep energy balance consturctor
    /// </summary>
    /// <param name="fieldEntericMethane">Enteric methane produced in the field (kg)</param>
    /// <param name="fieldVolatileSolids">volatilse solids depositeded in the field (kg)</param>
    /// <param name="fieldTotalNitrogen">Total nitrogen deposited in the field (kg)</param>
    /// <param name="fieldAvailableNitrogen">Availabel nitrogen deposited in the field (kg)</param>
    /// <param name="fieldGrossEnergy">Gross energy intake (MJ)</param>
    /// <param name="fieldMetabolisableEnergy">Metabolisable energy intake (MJ)</param>
    /// <param name="fieldDryMatterIntake">Dry Matter intake (kg)</param>
    /// <param name="fieldDays">Days spent in field</param>
    /// <param name="fieldDungNitrogen">Nitrogen in dung deposited in the field</param>
    /// <param name="fieldUrineNitrogen">Nitrogn in urine deposited in the field</param>
    /// <param name="houseEntericMethane">Enteric methane emitted in the house (kg)</param>
    /// <param name="houseVolatileSolids">Volatilse solids deposited in the house (kg)</param>
    /// <param name="houseTotalNitrogen">Total nitrogen deposited in the house (kg)</param>
    /// <param name="houseAvailableNitrogen">Availabel nitrogen deposited in the house (kg)</param>
    /// <param name="houseGrossEnergy">Gross energy intake while housed (MJ)</param>
    /// <param name="houseMetabolisableEnergy">Metabolisable energy intake while housed (MJ)</param>
    /// <param name="houseDryMatterIntake">Dry matter intkae while housed (kg)</param>
    /// <param name="houseDays">Days in the hosue</param>
    /// <param name="houseDungNitrogen">Nitrogen in dung deposited in the house (kg)</param>
    /// <param name="houseUrineNitrogen">Nitrogen in urine deposited in the house (kg)</param>
    /// <param name="fieldDungNitrogenCP200">nitrogen in dung deposited in field on a CP200 diet (kg)</param>
    /// <param name="fieldUrineNitrogenCP200">nitrogen in urine deposited in field on a CP 200 diet (kg)</param>
    /// <param name="fieldDungNitrogenCP100">nitrogen in dung deposited in the field on a CP100 diet (kg)</param>
    /// <param name="fieldUrineNitrogenCP100">nitrogen in urine deposited in the field on a CP100 diet (kg)</param>
    /// <param name="fieldTotalNitrogenCP200">Total nitrogen deposited in the field on a CP200 diet (kg)</param>
    /// <param name="fieldAvailableNitrogenCP200">Availabel nitrogen deposited in the field on a CP100 diet</param>
    /// <param name="fieldTotalNitrogenCP100">Total nitrogne deposited in the field on a CP100 diet (kg)</param>
    /// <param name="fieldAvailableNitrogenCP100">Availabel nitrogen deposited in the field on a CP100 diet (kg)</param>
    /// <param name="fieldNitrogenIntake">Nitrogemn intake in the field (kg)</param>
    /// <param name="houseNitrogenIntake">Nitrogen intake in the house (kg)</param>
    /// <param name="fieldNitrogenIntakeCP100">Nitrogen intake in the field on a CP100 diet (kg)</param>
    /// <param name="fieldNitrogenIntakeCP200">Nitrogen intake in the field on a CP200 diet (kg)</param>
    /// <param name="fieldExcretaMass">Mass of excrreta deposited in the field (kg)</param>
    /// <param name="fieldExcretaVolume">Volume of excreta deposited in the field (l)</param>
    /// <param name="houseExcretaMass">Mass of excreta deposited in the house (kg)</param>
    /// <param name="houseExcretaVolume">Volume of excreta deposited in the house (l)</param>
    /// <param name="houseManureMass">Mass of manure leaving the house (kg)</param>
    /// <param name="houseManureVolume">Volume of manure leaving the house (l)</param>
    public SheepEnergyBalance(double fieldEntericMethane, double fieldVolatileSolids, double fieldTotalNitrogen, double fieldAvailableNitrogen, double fieldGrossEnergy, double fieldMetabolisableEnergy, double fieldDryMatterIntake, double fieldDays, double fieldDungNitrogen, double fieldUrineNitrogen, double houseEntericMethane, double houseVolatileSolids, double houseTotalNitrogen, double houseAvailableNitrogen, double houseGrossEnergy, double houseMetabolisableEnergy, double houseDryMatterIntake, double houseDays, double houseDungNitrogen, double houseUrineNitrogen, double fieldDungNitrogenCP200, double fieldUrineNitrogenCP200, double fieldDungNitrogenCP100, double fieldUrineNitrogenCP100, double fieldTotalNitrogenCP200, double fieldAvailableNitrogenCP200, double fieldTotalNitrogenCP100, double fieldAvailableNitrogenCP100, double fieldNitrogenIntake, double houseNitrogenIntake, double fieldNitrogenIntakeCP100, double fieldNitrogenIntakeCP200, double fieldExcretaMass, double fieldExcretaVolume, double houseExcretaMass, double houseExcretaVolume, double houseManureMass, double houseManureVolume)
    {
        FieldEntericMethane = fieldEntericMethane;
        FieldVolatileSolids = fieldVolatileSolids;
        FieldTotalNitrogen = fieldTotalNitrogen;
        FieldAvailableNitrogen = fieldAvailableNitrogen;
        FieldGrossEnergy = fieldGrossEnergy;
        FieldMetabolisableEnergy = fieldMetabolisableEnergy;
        FieldDryMatterIntake = fieldDryMatterIntake;
        FieldDays = fieldDays;
        FieldDungNitrogen = fieldDungNitrogen;
        FieldUrineNitrogen = fieldUrineNitrogen;
        HouseEntericMethane = houseEntericMethane;
        HouseVolatileSolids = houseVolatileSolids;
        HouseTotalNitrogen = houseTotalNitrogen;
        HouseAvailableNitrogen = houseAvailableNitrogen;
        HouseGrossEnergy = houseGrossEnergy;
        HouseMetabolisableEnergy = houseMetabolisableEnergy;
        HouseDryMatterIntake = houseDryMatterIntake;
        HouseDays = houseDays;
        HouseDungNitrogen = houseDungNitrogen;
        HouseUrineNitrogen = houseUrineNitrogen;
        FieldDungNitrogenCP200 = fieldDungNitrogenCP200;
        FieldUrineNitrogenCP200 = fieldUrineNitrogenCP200;
        FieldDungNitrogenCP100 = fieldDungNitrogenCP100;
        FieldUrineNitrogenCP100 = fieldUrineNitrogenCP100;
        FieldTotalNitrogenCP200 = fieldTotalNitrogenCP200;
        FieldAvailableNitrogenCP200 = fieldAvailableNitrogenCP200;
        FieldTotalNitrogenCP100 = fieldTotalNitrogenCP100;
        FieldAvailableNitrogenCP100 = fieldAvailableNitrogenCP100;
        FieldNitrogenIntake = fieldNitrogenIntake;
        HouseNitrogenIntake = houseNitrogenIntake;
        FieldNitrogenIntakeCP100 = fieldNitrogenIntakeCP100;
        FieldNitrogenIntakeCP200 = fieldNitrogenIntakeCP200;
        FieldExcretaMass = fieldExcretaMass;
        FieldExcretaVolume = fieldExcretaVolume;
        HouseExcretaMass = houseExcretaMass;
        HouseExcretaVolume = houseExcretaVolume;
        HouseManureMass = houseManureMass;
        HouseManureVolume = houseManureVolume;
    }

    //Field
    /// <summary>
    /// Enteric methane produced in field (kg)
    /// </summary>
    public double FieldEntericMethane { get; set; }
    /// <summary>
    /// Volatile solids deposited in the field (kg)
    /// </summary>
    public double FieldVolatileSolids { get; set; }
    /// <summary>
    /// Total nitrogen deposited in the field (kg)
    /// </summary>
    public double FieldTotalNitrogen { get; set; }
    /// <summary>
    /// Availabel nitrogen deposited in the field (kg)
    /// </summary>
    public double FieldAvailableNitrogen { get; set; }
    /// <summary>
    /// Gross energy intake in the field (MJ)
    /// </summary>
    public double FieldGrossEnergy { get; set; }
    /// <summary>
    /// Metabolisable energy intkae in the field (MJ)
    /// </summary>
    public double FieldMetabolisableEnergy { get; set; }
    /// <summary>
    /// Dry matter intake in the field (kg)
    /// </summary>
    public double FieldDryMatterIntake { get; set; }
    /// <summary>
    /// Days in the field
    /// </summary>
    public double FieldDays { get; set; }
    /// <summary>
    /// Nitrogen in dung deposited in the field (kg)
    /// </summary>
    public double FieldDungNitrogen { get; set; }
    /// <summary>
    /// Nitrogen in urine deposited in the field
    /// </summary>
    public double FieldUrineNitrogen { get; set; }

    //House
    /// <summary>
    /// Enteric methane emission from housed sheep (kg)
    /// </summary>
    public double HouseEntericMethane { get; set; }
    /// <summary>
    /// Volatile solids deposited in the house (kg)
    /// </summary>
    public double HouseVolatileSolids { get; set; }
    /// <summary>
    /// Total nitrogen deposited in the house (kg)
    /// </summary>
    public double HouseTotalNitrogen { get; set; }
    /// <summary>
    /// Available nitrogen deposited in the house (kg)
    /// </summary>
    public double HouseAvailableNitrogen { get; set; }
    /// <summary>
    /// Gross energy intake in the house (MJ)
    /// </summary>
    public double HouseGrossEnergy { get; set; }
    /// <summary>
    /// Metabolisable energy intake in the house (MJ)
    /// </summary>
    public double HouseMetabolisableEnergy { get; set; }
    /// <summary>
    /// Dry matter intake in the house (kg)
    /// </summary>
    public double HouseDryMatterIntake { get; set; }
    /// <summary>
    /// Days in the house
    /// </summary>
    public double HouseDays { get; set; }
    /// <summary>
    /// Nitrogen in dung deposited in the house (kg)
    /// </summary>
    public double HouseDungNitrogen { get; set; }
    /// <summary>
    /// Nitrogen in urine deposited in the house )kg)
    /// </summary>
    public double HouseUrineNitrogen { get; set; }

    //Set CP contents
    /// <summary>
    /// Nitrogen in dung deposited in the field when fed a CP200 diet (kg)
    /// </summary>
    public double FieldDungNitrogenCP200 { get; set; }
    /// <summary>
    /// Nitrogen in urine deposited in the field when fed a CP200 diet (kg)
    /// </summary>
    public double FieldUrineNitrogenCP200 { get; set; }
    /// <summary>
    /// Nitrogen in dung deposited in the field when fed a CP100 diet (kg)
    /// </summary>
    public double FieldDungNitrogenCP100 { get; set; }
    /// <summary>
    /// Nitrogen in urine deposited in the field when fed a CP100 diet (kg)
    /// </summary>
    public double FieldUrineNitrogenCP100 { get; set; }
    /// <summary>
    /// Total nitrogen deposite din the field when fed a CP200 diet (kg)
    /// </summary>
    public double FieldTotalNitrogenCP200 { get; set; }
    /// <summary>
    /// Availabel nitrogen deposited in the field when fed a CP200 diet (kg)
    /// </summary>
    public double FieldAvailableNitrogenCP200 { get; set; }
    /// <summary>
    /// Total nitrogen deposited in the field when fed a CP100 diet (kg)
    /// </summary>
    public double FieldTotalNitrogenCP100 { get; set; }
    /// <summary>
    /// Availabel nitrogen deposited in the field when fed a CP100 diet (kg)
    /// </summary>
    public double FieldAvailableNitrogenCP100 { get; set; }
    /// <summary>
    /// Nitrogen intake in the field (kg)
    /// </summary>
    public double FieldNitrogenIntake { get; set; }
    /// <summary>
    /// Nitrogen intkae in the house (kg)
    /// </summary>
    public double HouseNitrogenIntake { get; set; }
    /// <summary>
    /// Nitrogen intake in the field when fed a CP100 diet (kg)
    /// </summary>
    public double FieldNitrogenIntakeCP100 { get; set; }
    /// <summary>
    /// Nitrogen intkae in the field when fed a CP200 diet (kg)
    /// </summary>
    public double FieldNitrogenIntakeCP200 { get; set; }

    //Excreta & Manure - mass and volume
    /// <summary>
    /// Mass of excreta deposited in the field (kg)
    /// </summary>
    public double FieldExcretaMass { get; set; }
    /// <summary>
    /// Volume of excreta deposited in the field (l)
    /// </summary>
    public double FieldExcretaVolume { get; set; }
    /// <summary>
    /// Mass of excreta deposited in the house (kg)
    /// </summary>
    public double HouseExcretaMass { get; set; }
    /// <summary>
    /// Volume of excreta deposited in the house (l)
    /// </summary>
    public double HouseExcretaVolume { get; set; }
    /// <summary>
    /// Mass of manure leaving the house (kg)
    /// </summary>
    public double HouseManureMass { get; set; }
    /// <summary>
    /// Volume of manure leaving the housee (l)
    /// </summary>
    public double HouseManureVolume { get; set; }

}
