using System;

namespace FrameAgricultureApi.Libraries.CoverCrops;

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