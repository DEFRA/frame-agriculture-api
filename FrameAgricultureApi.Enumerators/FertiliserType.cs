using System;

namespace FrameAgricultureApi.Enumerators;

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