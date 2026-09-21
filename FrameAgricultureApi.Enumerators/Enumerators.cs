namespace FrameAgricultureApi.Enumerators;

/// <summary>
/// The Enumerators class
/// </summary>
public class Enumerators
{
    /// <summary>
    /// Enumerator for Countries
    /// </summary>
    public enum Country
    {
        /// <summary>
        /// England = 1
        /// </summary>
        England = 1,
        /// <summary>
        /// Wales = 2
        /// </summary>
        Wales = 2,
        /// <summary>
        /// Scotland = 3
        /// </summary>
        Scotland = 3,
        /// <summary>
        /// Northen Ireland = 4
        /// </summary>
        NorthernIreland = 4
    }
    /// <summary>
    /// Enumerator for months of the year.
    /// NOTE: indexing starts at 0 for January and runs to 11 for December
    /// </summary>
    public enum Month
    {
        /// <summary>
        /// January = 1
        /// </summary>
        January = 0,
        /// <summary>
        /// Feburary = 1
        /// </summary>
        February = 1,
        /// <summary>
        /// March = 2
        /// </summary>
        March = 2,
        /// <summary>
        /// April = 3
        /// </summary>
        April = 3,
        /// <summary>
        /// May = 4
        /// </summary>
        May = 4,
        /// <summary>
        /// June = 5
        /// </summary>
        June = 5,
        /// <summary>
        /// July = 6
        /// </summary>
        July = 6,
        /// <summary>
        /// August = 7
        /// </summary>
        August = 7,
        /// <summary>
        /// September = 8
        /// </summary>
        September = 8,
        /// <summary>
        /// October = 9
        /// </summary>
        October = 9,
        /// <summary>
        /// November = 10
        /// </summary>
        November = 10,
        /// <summary>
        /// December = 11
        /// </summary>
        December = 11,
        /// <summary>
        /// Not Set = 12
        /// </summary>
        NotSet = 12
    }
    /// <summary>
    /// Enumerator for Fertiliser type
    /// Indices run from 1 to 6 for Urea, Ammonium Nitrate, Urea Ammonium Nitrate, Calcium Ammonium Nitrate,
    /// Ammonium Sulphate or Diammonium Phosphate and Other Nitrogen Containing Fertilisers (including blends)
    /// </summary>
    public enum FertiliserType
    {
        /// <summary>
        /// UNfertilised = 0 [THIS IS NOT USED]
        /// </summary>
        Unfertilised = 0,
        /// <summary>
        /// Urea = 1
        /// </summary>
        Urea = 1,
        /// <summary>
        /// Ammonium nitrate = 2
        /// </summary>
        AmmoniumNitrate = 2,
        /// <summary>
        /// Urea ammonium nitrate = 3
        /// </summary>
        UreaAmmoniumNitrate = 3,
        /// <summary>
        /// Calcium ammonium nitrate = 4
        /// </summary>
        CalciumAmmoniumNitrate = 4,
        /// <summary>
        /// Ammonium sukphate or diammonium phosphate = 5
        /// </summary>
        AmmoniumSulphate_DiammoniumPhosphate = 5,
        /// <summary>
        /// All other nitrogen fertiliser (including blends) = 6
        /// </summary>
        OtherNitrogenincludingCompoundBlends = 6
    }
    /// <summary>
    /// Enumerator for crop type
    /// Indices run from 1 to 48, note that 23, 24, 42 + 43 are not used.
    /// </summary>
    public enum CropType
    {
        /// <summary>
        /// Oast = 1
        /// </summary>
        Oats = 1,
        /// <summary>
        /// Spring oats =2
        /// </summary>
        Spring_oats = 2,
        /// <summary>
        /// Winter oats =3
        /// </summary>
        Winter_oats = 3,
        /// <summary>
        /// Spring barley = 4
        /// </summary>
        Spring_barley = 4,
        /// <summary>
        /// Spring barley (malting) = 5
        /// </summary>
        Spring_barley_malting = 5,
        /// <summary>
        /// Spring barley (non-malting) = 6
        /// </summary>
        Spring_barley_nonmalting = 6,
        /// <summary>
        /// Winter barley = 7
        /// </summary>
        Winter_barley = 7,
        /// <summary>
        /// Winter barley (malting) = 8
        /// </summary>
        Winter_barley_malting = 8,
        /// <summary>
        /// Winter barley (non-malting) = 9
        /// </summary>
        Winter_barley_nonmalting = 9,
        /// <summary>
        /// Wheat = 10
        /// </summary>
        Wheat = 10,
        /// <summary>
        /// Milling wheat = 11
        /// </summary>
        Wheat_milling = 11,
        /// <summary>
        /// non-milling wheat = 12
        /// </summary>
        Wheat_nonmilling = 12,
        /// <summary>
        /// Minor cereals = 13
        /// </summary>
        Minor_cereals = 13,
        /// <summary>
        /// Oilseed rape = 14
        /// </summary>
        Oilseed_rape = 14,
        /// <summary>
        /// Spring oilseed rape = 15
        /// </summary>
        Spring_oilseed_rape = 15,
        /// <summary>
        /// Winter oilseed rape = 16
        /// </summary>
        Winter_oilseed_rape = 16,
        /// <summary>
        /// Linseed and flax = 17
        /// </summary>
        Linseed_and_Flax = 17,
        /// <summary>
        /// Linseed = 18
        /// </summary>
        Linseed = 18,
        /// <summary>
        /// Flas = 19
        /// </summary>
        Flax = 19,
        /// <summary>
        /// Field beasn (harvested dry) = 10
        /// </summary>
        Field_beans_harvesteddry = 20,
        /// <summary>
        /// Field peas (harvested dry) = 21
        /// </summary>
        Field_peas_harvesteddry = 21,
        /// <summary>
        /// Field beans and peas (combined, but not vining peas) = 22
        /// </summary>
        Field_beans_and_peas_combined_notViningpeas = 22,
        /// <summary>
        /// NOT USED = 23
        /// </summary>
        Void = 23,
        /// <summary>
        /// NOT USED = 24
        /// </summary>
        Void2 = 24,
        /// <summary>
        /// Potatoes = 25
        /// </summary>
        Potatoes = 25,
        /// <summary>
        /// Maincrop potatoes = 26
        /// </summary>
        Potatoes_maincrop = 26,
        /// <summary>
        /// Seed or early potatoes = 27
        /// </summary>
        Potatoes_seed_or_earlies = 27,
        /// <summary>
        /// Sugar beet = 28
        /// </summary>
        Sugar_beet = 28,
        /// <summary>
        /// Mazie = 29
        /// </summary>
        Maize = 29,
        /// <summary>
        /// Grain maize = 30
        /// </summary>
        Grain_maize = 30,
        /// <summary>
        /// Forage mazie = 31
        /// </summary>
        Forage_maize = 31,
        /// <summary>
        /// Rootcrops for stockfeed = 32
        /// </summary>
        Rootcrops_for_stockfeed = 32,
        /// <summary>
        /// Leady forage crops = 33
        /// </summary>
        Leafy_forage_crops = 33,
        /// <summary>
        /// Other fodder crops = 34
        /// </summary>
        Other_fodder_crops = 34,
        /// <summary>
        /// Vegetables (not differentiated) = 35
        /// </summary>
        Vegetables_notdifferentiated = 35,
        /// <summary>
        /// Vegetables (brassicas) = 36
        /// </summary>
        Vegetables_brassicas = 36,
        /// <summary>
        /// Vegetables (legumes) = 38
        /// </summary>
        Vegetables_legumes = 37,
        /// <summary>
        /// Vegetables (other non-legumes) = 38
        /// </summary>
        Vegetables_othernonlegumes = 38,
        /// <summary>
        /// Other horticultural crops = 39
        /// </summary>
        Other_horticultural_crops = 39,
        /// <summary>
        /// Soft fruit = 40
        /// </summary>
        Soft_Fruit = 40,
        /// <summary>
        /// Top fruit = 41
        /// </summary>
        Top_Fruit = 41,
        /// <summary>
        /// NOT USED = 42
        /// </summary>
        Void3 = 42,
        /// <summary>
        /// NOT USED = 43
        /// </summary>
        Void4 = 43,
        /// <summary>
        /// Miscanthus = 45
        /// </summary>
        Miscanthus = 44,
        /// <summary>
        /// Short rotation coppice willow = 45
        /// </summary>
        Willow_shortrotationcoppice = 45,
        /// <summary>
        /// Other field crops = 46
        /// </summary>
        Other_field_crops = 46,
        /// <summary>
        /// Wine grapes = 47
        /// </summary>
        Wine_grapes = 47,
        /// <summary>
        /// Mixed top and soft fruit = 48
        /// </summary>
        Fruit_mixed_top_and_soft_fruit = 48

    };
    /// <summary>
    /// Grass type enumerator
    /// </summary>
    public enum GrassType
    {
        /// <summary>
        /// NOT USED = 0
        /// </summary>
        NotSet = 0,
        /// <summary>
        /// Improved temporary grass = 1
        /// </summary>
        ImprovedTemporary = 1,
        /// <summary>
        /// IMproved permanent grass = 2
        /// </summary>
        ImprovedPermanent = 2,
        /// <summary>
        /// Unimproved sole rights grazing = 3
        /// </summary>
        UnimprovedSoleRights = 3,
        /// <summary>
        /// Unimproved common grazing = 4
        /// </summary>
        UnimprovedCommon = 4
    };
    /// <summary>
    /// Cover crop type enumerator
    /// </summary>
    public enum CoverCropType
    {
        /// <summary>
        /// No differentiation of cover crop type at present so use 0
        /// </summary>
        All = 0
    };

    /// <summary>
    /// Core emissions enumerator
    /// </summary>
    public enum CoreEmissions
    {
        /// <summary>
        /// Direct N2O-N emission (kg)
        /// </summary>
        DirectN2ON,
        /// <summary>
        /// NH3-N emission (kg)
        /// </summary>
        DirectNH3N,
        /// <summary>
        /// Direct NO-N emission (kg)
        /// </summary>
        DirectNON,
        /// <summary>
        /// Direct N2-N emission (kg)
        /// </summary>
        DirectN2N,
        /// <summary>
        /// No3-N leached (kg)
        /// </summary>
        LeachedNO3N,
        /// <summary>
        /// INdirect N2O-N from leached NO3-N (kg)
        /// </summary>
        N2ONleached,
        /// <summary>
        /// Indirect N2O-N volatalised from redepostion of NH3-N and NO-N (kg)
        /// </summary>
        N2ONvolatalised
    }

    /// <summary>
    /// Additional outputs from cover crop calcualtions enumerator
    /// </summary>
    public enum AdditionalCoverCropEmissions
    {
        /// <summary>
        /// Additional N return from cover crops (kg)
        /// </summary>
        Nreturn,
        /// <summary>
        /// Leached nitrate if cover crop frac leach reduction is not applied (kg)
        /// </summary>
        unadjustedNO3N,
        /// <summary>
        /// N2ON from leached nitrate without frac leach reduction applied (kg)
        /// </summary>
        unadjustedN2ONLeach
    }

    /// <summary>
    /// Additional outputs from grass residue calcualtions enumerator
    /// </summary>
    public enum AdditionalGrassResidueEmissions
    {
        /// <summary>
        /// Dry matter offtake (kg)
        /// </summary>
        DryMatterOfftake,
        /// <summary>
        /// Above ground residue dry matter (kg)
        /// </summary>
        AboveGroundResidueDryMatter,
        /// <summary>
        /// Below ground residue dry matter (kg)
        /// </summary>
        BelowGroundResidueDryMatter,

        //AboveGroundResidueDryMatterN,

        //BelowGroundResidueDryMatterN,

        /// <summary>
        /// Nitrogen content of residues for grass renewal
        /// </summary>
        RenewalResidueNitrogen,
        /// <summary>
        /// The percentage of nitrogen leached
        /// </summary>
        GrassFracLeach,
        /// <summary>
        /// Nitrogen in grass harvested (kg)
        /// </summary>
        NitrogeninGrassHarvested,
        /// <summary>
        /// Nitrogen fixed by clover (kg)
        /// </summary>
        NitrogenfromCloverfixation

    }
    /// <summary>
    /// Additional outputs from residue calcualtions enumerator
    /// </summary>
    public enum AdditionalCropResidueEmissions
    {
        /// <summary>
        /// Dry matter offtake (kg/ha)
        /// </summary>
        DryMatterOfftake,
        /// <summary>
        /// Above ground residue dry matter (kg/ha)
        /// </summary>
        AboveGroundResidueDryMatter,
        /// <summary>
        /// Below ground residue dry matter (kg/ha)
        /// </summary>
        BelowGroundResidueDryMatter,
        /// <summary>
        /// N in above ground residue dry matter (kg/ha)
        /// </summary>
        AboveGroundResidueDryMatterN,
        /// <summary>
        /// N in below ground residue dry matter (kg/ha)
        /// </summary>
        BelowGroundResidueDryMatterN,
        /// <summary>
        /// The nitrogen in any crop residues that are removed from the field afte harvesting (kg/ha)
        /// </summary>
        NitrogeninStrawRemoved,
        /// <summary>
        /// Nitrogen in the harvested yield (kg/ha)
        /// </summary>
        NitrogeninHarvestedYield

    }

    /// <summary>
    /// Crop residue burning emissions enumerator
    /// </summary>
    public enum CombustionEmissions
    {
        /// <summary>
        /// residue dry matter burned (kg)
        /// </summary>
        ResidueDryMatterBurned,
        /// <summary>
        /// Burning efficiency (%)
        /// </summary>
        BurningEfficiency,
        /// <summary>
        /// CO emission frrom crop residue burning (kg)
        /// </summary>
        CombustionCO,
        /// <summary>
        /// N2O-N emission frrom crop residue burning (kg)
        /// </summary>
        CombustionN2ON,
        /// <summary>
        /// NH3-N emission frrom crop residue burning (kg)
        /// </summary>
        CombustionNH3N,
        /// <summary>
        /// NOx-N emission frrom crop residue burning (kg)
        /// </summary>
        CombustionNOxN,
        /// <summary>
        /// SO2 emission frrom crop residue burning (kg)
        /// </summary>
        CombustionSO2,
        /// <summary>
        /// CH4 emission frrom crop residue burning (kg)
        /// </summary>
        CombustionCH4,
        /// <summary>
        /// NMVOC emission frrom crop residue burning (kg)
        /// </summary>
        CombustionNMVOC,
        /// <summary>
        /// Particulate matter (2.5 micron) emission frrom crop residue burning (kg)
        /// </summary>
        CombustionPM2_5,
        /// <summary>
        /// Particulaute matter (10 micron) emission frrom crop residue burning (kg)
        /// </summary>
        CombustionPM10,
        /// <summary>
        /// Total solid particulate matter emission frrom crop residue burning (kg)
        /// </summary>
        CombustionTSP
    }

    /// <summary>
    /// Non-GHG emissions enumerator
    /// </summary>
    public enum NonGHG_Emissions
    {
        /// <summary>
        /// NMVOC emission (kg)
        /// </summary>
        NMVOC,
        /// <summary>
        /// Particulate matter (2.5 micron) emission (kg)
        /// </summary>
        PM2_5,
        /// <summary>
        /// Particulate matter (10 micron) emission (kg)
        /// </summary>
        PM10,
        /// <summary>
        /// Total solid particulates  emission (kg)
        /// </summary>
        TSP
    }
    /// <summary>
    /// Additional non-GHG emissions enumerator
    /// </summary>
    public enum NonGHGEmissions_AdditionalCropGrass
    {
        /// <summary>
        /// PM 2.5 particulaate matter emissions from harvesting
        /// </summary>
        PM2_5_Harvesting,
        /// <summary>
        /// PM 2.5 particulaate matter emissions from cultivation
        /// </summary>
        PM2_5_Cultivation,
        /// <summary>
        /// PM 2.5 particulaate matter emissions from drying
        /// </summary>
        PM2_5_Drying,
        /// <summary>
        /// PM 2.5 particulaate matter emissions from cleaning
        /// </summary>
        PM2_5_Cleaning,
        /// <summary>
        /// PM 10 particulaate matter emissions from harvesting
        /// </summary>
        PM10_Harvesting,
        /// <summary>
        /// PM 10 particulaate matter emissions from cultivation
        /// </summary>
        PM10_Cultivation,
        /// <summary>
        /// PM 10 particulaate matter emissions from drying
        /// </summary>
        PM10_Drying,
        /// <summary>
        /// PM 10 particulaate matter emissions from cleaning
        /// </summary>
        PM10_Cleaning,
        /// <summary>
        /// NMVOC from silage feed
        /// </summary>
        NMVOC_SilageFeed,
        /// <summary>
        /// NMVOC from Silage storage
        /// </summary>
        NMVOC_SilageStore,
        /// <summary>
        /// NMVOC from housing
        /// </summary>
        NMVOC_Housing,
        /// <summary>
        /// NMVOC at grazing
        /// </summary>
        NMVOC_Grazing,
        /// <summary>
        /// NMVOC fomr manure storage
        /// </summary>
        NMVOC_Storage,
        /// <summary>
        /// NMVOC from manure spreading
        /// </summary>
        NMVOC_Spreading
    }
    /// <summary>
    /// Additional nonGHG emissions for livestock
    /// </summary>
    public enum NonGHGEmissions_AdditionalLivestock
    {
        /// <summary>
        /// NMVOC from silage storage
        /// </summary>
        NMVOC_SilageStore,
        /// <summary>
        /// NMVOC from silage feed
        /// </summary>
        NMVOC_SilageFeed,
        /// <summary>
        /// NMVOC form housing
        /// </summary>
        NMVOC_Housing,
        /// <summary>
        /// NMVOC from storage
        /// </summary>
        NMVOC_Storage,
        /// <summary>
        /// NMVOC form spreading
        /// </summary>
        NMVOC_Spreading,
        /// <summary>
        /// NMVOC form grazing
        /// </summary>
        NMVOC_Grazing,
        /// <summary>
        /// PM2.5 from manure management
        /// </summary>
        PM2_5_ManureManagement,
        /// <summary>
        /// PM10 form manure management
        /// </summary>
        PM10_ManureManagement,
        /// <summary>
        /// TSP from manure management
        /// </summary>
        TSP_ManureManagement
    }

    /// <summary>
    /// Organic matter types - a combination of manure types and imported non-manure based organic matter types
    /// </summary>
    public enum OrganicMatterType
    {
        /// <summary>
        /// Not set value of 0 - used for error trapping
        /// </summary>
        NotSet = 0,
        /// <summary>
        /// slurry manure (liquids) - potentially will be split into solid and liquid fractions in a future update
        /// </summary>
        CattleSlurry = 1,
        /// <summary>
        /// Farmyard manure
        /// </summary>
        CattleFYM = 2,
        /// <summary>
        /// Digestate from slurry
        /// </summary>
        DigestateCattleSlurry = 3,
        /// <summary>
        /// DIgestate from farmyard manure
        /// </summary>
        DigestateCattleFYM = 4,
        /// <summary>
        /// slurry manure (liquids) - potentially will be split into solid and liquid fractions in a future update
        /// </summary>
        PigSlurry = 5,
        /// <summary>
        /// Farmyard manure
        /// </summary>
        PigFYM = 6,
        /// <summary>
        /// Digestate from slurry
        /// </summary>
        DigestatePigSlurry = 7,
        /// <summary>
        /// DIgestate from farmyard manure
        /// </summary>
        DigestatePigFYM = 8,
        /// <summary>
        /// Poultry layer manure
        /// </summary>
        PoultryLayerManure = 9,
        /// <summary>
        /// Poultry litter
        /// </summary>
        PoultryLitter = 10,
        /// <summary>
        /// Duck farmyard manure
        /// </summary>
        DuckFYM = 11,
        /// <summary>
        /// Digestate from poultry manure
        /// </summary>
        PoultryManureDigestate = 12,
        /// <summary>
        /// Minor Livestock manure (always FYM)
        /// </summary>
        MinorLivestockFYM = 13,
        /// <summary>
        /// Sheep manure (always FYM)
        /// </summary>
        SheepFYM = 14,
        /// <summary>
        /// The liquid portion of sewage sludge
        /// </summary>
        SewageSludgeLiquid = 15,
        /// <summary>
        /// The solid portion of sewage sludge
        /// </summary>
        SewageSludgeCake = 16,
        /// <summary>
        /// Digestae from food waste
        /// </summary>
        DigestateFoodbased = 17,
        /// <summary>
        /// Digestate from crops
        /// </summary>
        DigestateCropbased = 18,
        /// <summary>
        /// Digestate from other organic sources
        /// </summary>
        DigestateOtherOrganicResidue = 19,
        /// <summary>
        /// Compost
        /// </summary>
        Compost = 20
    }
    /// <summary>
    /// The source of the organic matter
    /// </summary>
    public enum OrganicMatterSourceType
    {
        /// <summary>
        /// Not set - no source
        /// </summary>
        NotSet = 0,
        /// <summary>
        /// Organic matter of cattle origin
        /// </summary>
        Cattle = 1,
        /// <summary>
        /// Organic matter of pig origin
        /// </summary>
        Pig = 2,
        /// <summary>
        /// Organic matter of poultry origin
        /// </summary>
        Poultry = 3,
        /// <summary>
        /// Organic matter of minor livestock origin
        /// </summary>
        MinorLivestock = 4,
        /// <summary>
        /// Organic matter of sheep origin
        /// </summary>
        Sheep = 5,
        /// <summary>
        /// Organic matter not of livestock origin
        /// </summary>
        NonLivestock = 6

    }
    /// <summary>
    /// State of organic matter
    /// </summary>
    public enum OrganicMatterState
    {
        /// <summary>
        /// No state provided
        /// </summary>
        NotSet = 0,
        /// <summary>
        /// Liquid organic matter (e.g. slurry and digestates)
        /// </summary>
        Liquid = 1,
        /// <summary>
        /// Solid organic matter (e.g. FYM, poultry manure)
        /// </summary>
        Solid = 2
    }
    /// <summary>
    /// The land use to which  organic matter is spread
    /// </summary>
    public enum SpreadingLandUse
    {
        /// <summary>
        /// A not set value for error trapping
        /// </summary>
        NotSet = 0,
        /// <summary>
        /// All grassland types
        /// </summary>
        Grassland = 1,
        /// <summary>
        /// All crop types
        /// </summary>
        Arable = 2
    }

    /// <summary>
    /// Additional outputs for spreading of organic matter
    /// </summary>
    public enum AdditionalOrganicMatterSpreadingOutputs
    {
        /// <summary>
        /// NotSet value for error trapping
        /// </summary>
        NotSet = 0,
        /// <summary>
        /// Total N in organic matter applied (per unit area)
        /// </summary>
        NTotalNApplied = 1,
        /// <summary>
        /// TAN in organic matter applied (per unit area)
        /// </summary>
        TANApplied = 2,
        /// <summary>
        /// Organic N in organic matter applied (per unit area)
        /// </summary>
        OrganicNApplied = 3
    }

    /// <summary>
    /// FLEA Component types
    /// </summary>
    public enum ComponentType
    {
        /// <summary>
        /// Not Set value
        /// </summary>
        NotSet = 0,
        /// <summary>
        /// Fertiliser component
        /// </summary>
        Fertiliser = 1,
        /// <summary>
        /// Residues component
        /// </summary>
        Residues = 2,
        /// <summary>
        /// Yard component (for cattle only)
        /// </summary>
        Yards = 3,
        /// <summary>
        /// Housing component - for livestock only
        /// </summary>
        Housing = 4,
        /// <summary>
        /// Storage component - for manures only
        /// </summary>
        Storage = 5,
        /// <summary>
        /// Spreading component - for manures and other organic matter
        /// </summary>
        Spreading = 6,
        /// <summary>
        /// Grazing component - for livestock only
        /// </summary>
        Grazing = 7

    }

    /// <summary>
    /// FLEA sector names (for UK GHG AEIA mapping)
    /// </summary>
    public enum Sector
    {
        /// <summary>
        /// No sector set
        /// </summary>
        NotSet = 0,
        /// <summary>
        /// Arable crops
        /// </summary>
        Arable = 1,
        /// <summary>
        /// Grassland
        /// </summary>
        Grass = 2,
        /// <summary>
        /// Beef production
        /// </summary>
        Beef = 3,
        /// <summary>
        /// Dairy production
        /// </summary>
        Dairy = 4,
        /// <summary>
        /// Pig production
        /// </summary>
        Pigs = 5,
        /// <summary>
        /// Poultry production
        /// </summary>
        Poultry = 6,
        /// <summary>
        /// Minor Livestock production
        /// </summary>
        MinorLivestock = 7,
        /// <summary>
        /// Application of non-manure based organic materials (e.g. sewage sludge, food digestate)
        /// </summary>
        OtherOrganices = 8,
        /// <summary>
        /// Sheep
        /// </summary>
        Sheep = 9
    }
    /// <summary>
    /// Sheep type enumerator
    /// </summary>
    public enum SheepType
    {
        /// <summary>
        /// Default not set
        /// </summary>
        NotSet = 0,
        /// <summary>
        /// Lamb
        /// </summary>
        Lamb = 1, // Lamb = 1 in main inventory - renumber and change all LUTs when have time
        /// <summary>/// Ram
        /// </summary>
        Ram = 2, // Ram = 2 in main inventory - renumber and change all LUTs when have time
        /// <summary>
        /// Ewe
        /// </summary>
        Ewe = 3
    }
    /// <summary>
    /// Minor Livestock Types
    /// </summary>
    public enum MinorLivestockType
    {
        /// <summary>
        /// Default not set
        /// </summary>
        NotSet = 0,
        /// <summary>
        /// Goats
        /// </summary>
        Goats = 1,
        /// <summary>
        /// Deer
        /// </summary>
        Deer = 2,
        /// <summary>
        /// Horses for agricultural use
        /// </summary>
        AgriculturalHorses = 3,
        /// <summary>
        /// Horses for racing, eventing, showjumping, etc
        /// </summary>
        ProfessionalHorses = 4,
        /// <summary>
        /// Horses for domestic use
        /// </summary>
        DomesticHorses = 5
    };
    /// <summary>
    /// Poultry types
    /// </summary>
    public enum PoultryType
    {
        /// <summary>
        /// default not set
        /// </summary>
        NotSet = 0,
        /// <summary>
        /// Growing pullets
        /// </summary>
        GrowingPullets = 1,
        /// <summary>
        /// Laying hens
        /// </summary>
        LayingHens = 2,
        /// <summary>
        /// Breeding flock
        /// </summary>
        BreedingFlock = 3,
        /// <summary>
        /// Broilers
        /// </summary>
        Broilers = 4,
        /// <summary>
        /// Turkeys
        /// </summary>
        Turkeys = 5,
        /// <summary>
        /// Ducks
        /// </summary>
        Ducks = 6,
        /// <summary>
        /// Geese
        /// </summary>
        Geese = 7,
        /// <summary>
        /// All other poultry
        /// </summary>
        OtherPoultry = 8
    };
    /// <summary>
    /// Pig type
    /// </summary>
    public enum PigType
    {
        /// <summary>
        /// Defualt not set
        /// </summary>
        NotSet = 0,
        /// <summary>
        /// Sows (Inventory pig type 1)
        /// </summary>
        Sows = 1,
        /// <summary>
        /// Gilts (Inventory pig type 2)
        /// </summary>
        Gilts = 2,
        /// <summary>
        /// Boars (Inventory pig type 3)
        /// </summary>
        Boars = 3,
        /// <summary>
        /// Fattening pigs greater than 80kg (Inventory pig type 4)
        /// </summary>
        Fattening_GT80kg = 4,
        /// <summary>
        /// Fattening pigs 20-80kg (Inventory pig type 5)
        /// </summary>
        Fattening_20to80kg = 5,
        /// <summary>
        /// Fattnening pigs less than 20kg (Inventory pig type 6)
        /// </summary>
        Fattening_LT20kg = 6
    };
    /// <summary>
    /// Beef cattle type enumerator
    /// </summary>
    public enum BeefCattleType
    {
        /// <summary>
        /// Default not set
        /// </summary>
        NotSet = 0,
        /// <summary>
        /// Heifers for breeding
        /// </summary>
        Heifersforbreeding = 1,
        /// <summary>
        /// Females for slaughter
        /// </summary>
        Beeffemalesforslaughter = 2,
        /// <summary>
        /// Bulls for breeding
        /// </summary>
        Bullsforbreeding = 3,
        /// <summary>
        /// Cereal fed bull
        /// </summary>
        Cerealfedbull = 4,
        /// <summary>
        /// Steers
        /// </summary>
        Steers = 5,
        /// <summary>
        /// Beef cows
        /// </summary>
        Cows = 6
    };
    /// <summary>
    /// Beed breeds
    /// </summary>
    public enum BeefCattleBreed
    {
        /// <summary>
        /// Default not set
        /// </summary>
        NotSet = 0,
        /// <summary>
        /// Continental
        /// </summary>
        Continental = 4,
        /// <summary>
        /// Lowland
        /// </summary>
        Lowland = 5,
        /// <summary>
        /// Upland
        /// </summary>
        Upland = 6,
        /// <summary>
        /// Dairy
        /// </summary>
        Dairy = 7
    };
    /// <summary>
    /// Cattle age class enumerator
    /// </summary>
    public enum BeefCattleAge
    {
        /// <summary>
        /// defualt not set
        /// </summary>
        notset = 0,
        /// <summary>
        /// 0-3 months
        /// </summary>
        m0to3 = 1,
        /// <summary>
        /// 3-6 months
        /// </summary>
        m3to6 = 2,
        /// <summary>
        /// 6-9 months
        /// </summary>
        m6to9 = 3,
        /// <summary>
        /// 9-12 months
        /// </summary>
        m9to12 = 4,
        /// <summary>
        /// 12-15 months
        /// </summary>
        m12to15 = 5,
        /// <summary>
        /// 15-18 months
        /// </summary>
        m15to18 = 6,
        /// <summary>
        /// 18-21 months
        /// </summary>
        m18to21 = 7,
        /// <summary>
        /// 21-24 months
        /// </summary>
        m21to24 = 8,
        /// <summary>
        /// 24-27 months
        /// </summary>
        m24to27 = 9,
        /// <summary>
        /// 27 to 30 months
        /// </summary>
        m27to30 = 10,
        /// <summary>
        /// 30-33 months
        /// </summary>
        m30to33 = 11,
        /// <summary>
        /// 33-36 months
        /// </summary>
        m33to36 = 12,
        /// <summary>
        /// 36 - 48 months
        /// </summary>
        m36to48 = 13,
        /// <summary>
        /// 48 - 60 months
        /// </summary>
        m48to60 = 14,
        /// <summary>
        /// 60 - 240 months
        /// </summary>
        m60to240 = 15,
        /// <summary>
        /// 240 - 300 months
        /// </summary>
        m240to300 = 16,
        /// <summary>
        /// 0-y6 months JAS
        /// </summary>
        m0to6 = 17,
        /// <summary>
        /// 6-12 months JAS
        /// </summary>
        m6to12 = 18,
        /// <summary>
        /// 12-24 months JAS
        /// </summary>
        m12to24 = 19,
        /// <summary>
        /// 24-300 months JAS
        /// </summary>
        m24to300 = 20
    };
    /// <summary>
    /// Dairy cattle enumerators (matches UK GHG AEIA definitions)
    /// </summary>
    public enum DairyCattle
    {
        /// <summary>
        /// Base not set value
        /// </summary>
        NotSet = 0,
        /// <summary>
        /// Female calves in dairy system (NB male calves are automatically the beef system)
        /// </summary>
        DC1_DairyCalvesFemale = 1,
        /// <summary>
        /// Dairy replacements
        /// </summary>
        DC2_DairyReplacementsFemale = 2,
        /// <summary>
        /// Heifers in calf
        /// </summary>
        DC3_DairyInCalfHeifers = 3,
        /// <summary>
        /// Cows
        /// </summary>
        DC4_DairyCows = 4
    }
    /// <summary>
    /// Size of dairy animal breed
    /// </summary>
    public enum DairyBreedSize
    {
        /// <summary>
        /// Default not set value
        /// </summary>
        NotSet = 0,
        /// <summary>
        /// Large breeds
        /// </summary>
        Large = 1,
        /// <summary>
        /// Mediume breeds
        /// </summary>
        Medium = 2,
        /// <summary>
        /// Small breeds
        /// </summary>
        Small = 3
    }
    /// <summary>
    /// Stages of growth used for dairy cattle weight and growth rate calculations
    /// </summary>
    public enum DairyCattleStages
    {
        /// <summary>
        /// Birth
        /// </summary>
        Birth = 0,
        /// <summary>
        /// One month
        /// </summary>
        FirstMonth = 1,
        /// <summary>
        /// One Year
        /// </summary>
        FirstYear = 2,
        /// <summary>
        /// Concpetion (first)
        /// </summary>
        FirstConception = 3,
        /// <summary>
        /// Calving (First)
        /// </summary>
        FirstCalving = 4,
        /// <summary>
        /// Death
        /// </summary>
        Death = 5
    }
    /// <summary>
    /// Manure storage system enumerator
    /// </summary>
    public enum ManureStorageSystem
    {
        /// <summary>
        /// Default not set
        /// </summary>
        NotSet = 0,
        /// <summary>
        /// Slurry in above groudn tanks
        /// </summary>
        Slurry_AboveGroundTanks = 1,
        /// <summary>
        /// Slurry in below ground tanks
        /// </summary>
        Slurry_BelowGroundTanks = 2,
        /// <summary>
        /// Slurry in laggon with earth banks
        /// </summary>
        Slurry_EarthBankLagoon = 3,
        /// <summary>
        /// Slurry in a bag
        /// </summary>
        Slurry_Bag = 4,
        /// <summary>
        /// Slurry in weeping wall storage
        /// </summary>
        Slurry_WeepingWall = 5,
        /// <summary>
        /// Anaerobic digestion of slurry
        /// </summary>
        Slurry_AnaerobicDigestion = 6,
        /// <summary>
        /// Slurry not stored
        /// </summary>
        Slurry_SpreadDirect_NoStorage = 7,
        /// <summary>
        /// FYM store on steading
        /// </summary>
        FYM_Steading = 8,
        /// <summary>
        /// FYM stored as a field heap
        /// </summary>
        FYM_FieldHeap = 9,
        /// <summary>
        /// FYM not stored
        /// </summary>
        FYM_SpreadDirect_NoStorage = 10,
        /// <summary>
        /// Anaerobic digestion of FYM
        /// </summary>
        FYM_AnaerobicDigestion = 11,
        /// <summary>
        /// Incineration (pultry manure only)
        /// </summary>
        Incineration = 12
    };
    /// <summary>
    /// Additional Outputs for Emissions from Grazing or Outdoor Livestock
    /// </summary>
    public enum AdditionalOutdoorExcretaOutputs
    {
        /// <summary>
        /// Not set - default
        /// </summary>
        NotSet = 0,
        /// <summary>
        /// Nitrogen in urine
        /// </summary>
        UrineNitrogen = 1,
        /// <summary>
        /// Nitrogen in dung
        /// </summary>
        DungNitrogen = 2,
        /// <summary>
        /// Total nitrogen in excreta deposited
        /// </summary>
        ExcretaTotalN = 3,
        /// <summary>
        /// TAN in  excreta deposited
        /// </summary>
        ExcretaTAN = 4,
        /// <summary>
        /// Organic N in excreta deposited
        /// </summary>
        ExcretaOrganicN = 5
    }
    /// <summary>
    /// Additional outputs form calcualtion of emissions from excreat deposited on yards
    /// </summary>
    public enum AdditionalYardExcretaOutputs
    {
        /// <summary>
        /// Not set - default
        /// </summary>
        NotSet = 0,
        /// <summary>
        /// Nitrogen in urine
        /// </summary>
        UrineNitrogen = 1,
        /// <summary>
        /// Nitrogen in dung
        /// </summary>
        DungNitrogen = 2,
        /// <summary>
        /// Total nitrogen in excreta deposited
        /// </summary>
        ExcretaTotalN = 3,
        /// <summary>
        /// TAN in  excreta deposited
        /// </summary>
        ExcretaTAN = 4,
        /// <summary>
        /// Organic N in excreta deposited
        /// </summary>
        ExcretaOrganicN = 5,
        /// <summary>
        /// Total N leaving yards(kg per head)
        /// </summary>
        TotalNout = 6,
        /// <summary>
        /// TAN leaving yards(kg per head)
        /// </summary>
        TANout = 7,
        /// <summary>
        /// Organic N leaving yards (kg per head)
        /// </summary>
        OrganicNout = 8
    }
    /// <summary>
    /// Additional outputs for housing manure management
    /// </summary>
    public enum AdditionalHousingManureManagementOutputs
    {
        /// <summary>
        /// NotSet value for error trapping
        /// </summary>
        NotSet = 0,
        /// <summary>
        /// Total N entering housing (kg per head)
        /// </summary>
        TotalNin = 1,
        /// <summary>
        /// TAN entering housing (kg per head)
        /// </summary>
        TANin = 2,
        /// <summary>
        /// Organic N entering housing (kg per head)
        /// </summary>
        OrganicNin = 3,
        /// <summary>
        /// Total N leaving housing (kg per head)
        /// </summary>
        TotalNout = 4,
        /// <summary>
        /// TAN leaving housing (kg per head)
        /// </summary>
        TANout = 5,
        /// <summary>
        /// Organic N leaving housing (kg per head)
        /// </summary>
        OrganicNout = 6,
        /// <summary>
        /// Nitrogen in bedding (kg per head)
        /// </summary>
        NinBedding = 7,
        /// <summary>
        /// N immobilised (kg per head)
        /// </summary>
        NImmobilised = 8,
        /// <summary>
        /// N in urine entering housing (kg per head)
        /// </summary>
        Urinein = 9,
        /// <summary>
        /// N in dung entering housing (kg per head)
        /// </summary>
        Dungin = 10

        }
        /// <summary>
        /// Additional Outputs associated with manure storage (including anaerobic digestion)
        /// </summary>
        public enum AdditionalStorageManureManagementOutputs
        {
            /// <summary>
            /// NotSet value for error trapping
            /// </summary>
            NotSet = 0,
            /// <summary>
            /// Total N entering housing (kg per head)
            /// </summary>
            TotalNin = 1,
            /// <summary>
            /// TAN entering housing (kg per head)
            /// </summary>
            TANin = 2,
            /// <summary>
            /// Organic N entering housing (kg per head)
            /// </summary>
            OrganicNin = 3,
            /// <summary>
            /// Total N leaving housing (kg per head)
            /// </summary>
            TotalNout = 4,
            /// <summary>
            /// TAN leaving housing (kg per head)
            /// </summary>
            TANout = 5,
            /// <summary>
            /// Organic N leaving housing (kg per head)
            /// </summary>
            OrganicNout = 6,
            /// <summary>
            /// N mineralised (kg per head)
            /// </summary>
            NMineralised = 7
        }
        /// <summary>
        /// Livestock emissions enumerator
        ///
        ///
        /// </summary>
        public enum LivestockEmissions
        {
            /// <summary>
            /// Not set value as defualt
            /// </summary>
            NotSet = 0,
            /// <summary>
            /// Enteric methane
            /// </summary>
            EntericMethane = 1,
            /// <summary>
            /// Methane emission from excreta deposited outdoors
            /// </summary>
            OutdoorExcretionMethane = 2,
            /// <summary>
            /// Methane from manure management
            /// </summary>
            ManureManagementMethane = 3
        }
        /// <summary>
        /// Enumerator for Manure Mass and Volume Outputs
        /// </summary>
        public enum ManureMassVolumeOutputs
        {
            /// <summary>
            /// Default - not set
            /// </summary>
            NotSet = 0,
            /// <summary>
            /// Mass of uring entering stage
            /// </summary>
            UrineMassIn = 1,
            /// <summary>
            /// Mass of dung entering stage
            /// </summary>
            DungMassIn = 2,
            /// <summary>
            /// Mass of excreta entering stage
            /// </summary>
            ExcretaMassIn = 3,
            /// <summary>
            /// Mass of manure entering stage
            /// </summary>
            ManureMassIn = 4,
            /// <summary>
            /// Mass of manure leaving stage
            /// </summary>
            ManureMassOut = 5,
            /// <summary>
            /// Volume of uring entering stage
            /// </summary>
            UrineVolumeIn = 6,
            /// <summary>
            /// Volume of dung entering stage
            /// </summary>
            DungVolumeIn = 7,
            /// <summary>
            /// Volume of excreta entering stage
            /// </summary>
            ExcretaVolumeIn = 8,
            /// <summary>
            /// Volume of manure entering stage
            /// </summary>
            ManureVolumeIn = 9,
            /// <summary>
            /// Volume of manure leaving stage
            /// </summary>
            ManureVolumeOut = 10
        }
        /// <summary>
        /// Manure Housing systems
        /// </summary>
        public enum ManureHousingSystem
        {
            /// <summary>
            /// Default not set
            /// </summary>
            NotSet = 0,
            /// <summary>
            /// Solid floor cubicles (cattle systems only)
            /// </summary>
            Cattle_Cubicle_Solid_Floor = 1,
            /// <summary>
            /// Slatted floor cubicles (Cattle only)
            /// </summary>
            Cattle_Cubicle_Slatted_Floor = 2,
            /// <summary>
            /// Loose slatted floor (cattle only)
            /// </summary>
            Cattle_Loose_Slatted_Floor = 3,
            /// <summary>
            /// Solid floor with bedding (all livestock)
            /// </summary>
            Solid_Floor_with_Bedding = 4,
            /// <summary>
            /// Fully slatted floor (pigs only)
            /// </summary>
            Fully_Slatted_Floor = 5,
            /// <summary>
            /// Part slatted floor (pigs only)
            /// </summary>
            Part_Slatted_Floor = 6,
            /// <summary>
            /// Free range generic (poultry only)
            /// </summary>
            Free_Range_Generic = 7,
            /// <summary>
            /// Single tier free range (poultry only)
            /// </summary>
            Free_Range_SingleTier = 8,
            /// <summary>
            /// Multi tier free range (poultry only)
            /// </summary>
            Free_Range_MultiTier = 9,
            /// <summary>
            /// Perchery (poultry only)
            /// </summary>
            Perchery = 10,
            /// <summary>
            /// Deep litter (poultry only)
            /// </summary>
            Deep_Litter = 11,
            /// <summary>
            /// Deep pit cages (poultry only)
            /// </summary>
            Cages_DeepPit = 12,
            /// <summary>
            /// Belt cleaned cages (poultry only)
            /// </summary>
            Cages_BeltCleaned = 13,
            /// <summary>
            /// Enriched belt cleaned cages (poultry only)
            /// </summary>
            Enriched_Cages_BeltCleaned = 14,
            /// <summary>
            /// Sheep Housing - assumed shed with straw?
            /// </summary>
            SheepHousing = 15
        }
        /// <summary>
        /// Outputs calculated by excreta formula
        /// </summary>
        public enum ExcretaOutputs
        {
            /// <summary>
            /// Not set - default
            /// </summary>
            NotSet = 0,
            /// <summary>
            /// Total nitrogen in excreta deposited
            /// </summary>
            ExcretaTotalN = 1,
            /// <summary>
            /// TAN in  excreta deposited
            /// </summary>
            ExcretaTAN = 2,
            /// <summary>
            /// Organic N in excreta deposited
            /// </summary>
            ExcretaOrganicN = 3,
        }
        /// <summary>
        /// Cattle mamagement regime (housing based)
        /// </summary>
        public enum ManagementRegime
        {
            /// <summary>
            /// Housed all year
            /// </summary>
            HousedAllYear = 1,
            /// <summary>
            /// Housed winter and part-housed summer
            /// </summary>
            HousedWinterPartHousedSummer = 2,
            /// <summary>
            /// Housed winter, out summer
            /// </summary>
            HousedWinterOutSummer = 3,
            /// <summary>
            /// Extended Grazing
            /// </summary>
            ExtendedGrazing = 4,
            /// <summary>
            /// Housed all year with access to yards
            /// </summary>
            HousedAllYearAndYards = 5,
            /// <summary>
            /// Housed winter and part housed summer with access to yards
            /// </summary>
            HousedWinterPartHousedSummerAndYards = 6,
            /// <summary>
            /// House winter and out summer with access to yards
            /// </summary>
            HousedWinterOutSummerAndYards = 7,
            /// <summary>
            /// Extended grazing with access to yards
            /// </summary>
            ExtendedGrazingAndYards = 8
        }
        /// <summary>
        /// Grass equation types
        /// </summary>
        public enum GrassEquations
        {
            /// <summary>
            /// Nitrogen in grass residues at renewal
            /// </summary>
            ResidualNitrogen = 1,
            /// <summary>
            /// The grass frac leach
            /// </summary>
            FracLeach = 2,
            /// <summary>
            /// Dry matter yield
            /// </summary>
            YieldDryMatter = 3,
            /// <summary>
            /// Above ground residue dry matter
            /// </summary>
            AboveGroundDryMatter = 4,
            /// <summary>
            /// Below ground residue dry matter
            /// </summary>
            BelowGroundDryMatter = 5,
            /// <summary>
            /// The nitrogen removed in harvested grass
            /// </summary>
            NitrogeninGrassHarvestedorEaten = 6,
            /// <summary>
            /// Nitrogen added due to clover fixation
            /// </summary>
            NitrogenfromCloverFixation = 7
        }
        /// <summary>
        /// Soil texture type
        /// </summary>
        public enum SoilTextureType
        {
            /// <summary>
            /// light sandy soil
            /// </summary>
            Light_Sand = 1,
            /// <summary>
            /// Shallow soil
            /// </summary>
            Shallow = 2,
            /// <summary>
            /// Medium Soil
            /// </summary>
            Medium = 3,
            /// <summary>
            /// Deep clay soil
            /// </summary>
            Deep_Clay = 4,
            /// <summary>
            /// Deep fertile silty soil
            /// </summary>
            Deep_Fertile_Silty = 5,
            /// <summary>
            /// Organic soil
            /// </summary>
            Organic = 6,
            /// <summary>
            /// Peaty soil
            /// </summary>
            Peaty = 7
        }
        /// <summary>
        /// Grass use type
        /// </summary>
        public enum GrassUseType
        {
            /// <summary>
            /// Cut for hay or silage
            /// </summary>
            Cut = 1,
            /// <summary>
            /// Grazed by livestock
            /// </summary>
            Grazed = 2,
            /// <summary>
            /// Both cut for hay/silage and grzed by livestock
            /// </summary>
            Cut_and_Grazed = 3
        }
        /// <summary>
        /// Subtypes used for Ewes
        /// </summary>
        public enum EweSubtype
        {
            /// <summary>
            /// Ewe lamb - produced lamb on 1st birthday
            /// </summary>
            EweLamb = 1,
            /// <summary>
            /// Conventional Ewe - produces lamb on 2nd birthday
            /// </summary>
            Ewe = 2
        }
        /// <summary>
        /// subtypes used for rams
        /// </summary>
        public enum RamSubtype
        {
            /// <summary>
            /// Rams have no subtype
            /// </summary>
            Ram = 1
        }
        /// <summary>
        /// subtypes used for lambs
        /// </summary>
        public enum LambSubtype
        {
            /// <summary>
            /// Lmabs that ae destined for finishing
            /// </summary>
            FinishingLamb = 1,
            /// <summary>
            /// AStore lambs that are sold and so leave the farm
            /// </summary>
            StoreLamb_Sold = 2,
            /// <summary>
            /// Lamnb intended to replace a ewe (conventional first lamb at 2 years)
            /// </summary>
            ReplacementEwe = 3,
            /// <summary>
            /// Lamb intended to replace a ewe lamb (first lamb at 1 year)
            /// </summary>
            ReplacementEweLamb = 4,
            /// <summary>
            /// Lamnbs that are reared as store lambs nbut do not leave the farm or as keep lambs
            /// </summary>
            StoreLamb_Keep = 5

    }
    /// <summary>
    /// Sheep system type
    /// </summary>
    public enum SheepSystemType
    {
        /// <summary>
        /// Hill sheep
        /// </summary>
        Hill = 1,
        /// <summary>
        /// Lowland sheep
        /// </summary>
        Lowland = 2,
        /// <summary>
        /// Upland sheep
        /// </summary>
        Upland = 3
    }
}
