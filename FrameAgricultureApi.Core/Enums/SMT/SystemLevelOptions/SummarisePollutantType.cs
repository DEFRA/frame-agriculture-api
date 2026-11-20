namespace FrameAgricultureApi.Core.Enums;

/// <summary>
/// Pollutant Type.
/// </summary>
public enum SummarisePollutantType
{
    /// <summary>
    /// All.
    /// </summary>
    All = 0,
    /// <summary>
    /// Ammonia.
    /// </summary>
    NH3 = 1,
    /// <summary>
    /// Nitrous Oxide.
    /// </summary>
    N2O = 2,
    /// <summary>
    /// Nitric Oxide.
    /// </summary>
    NO = 3,
    /// <summary>
    /// Dinitrogen.
    /// </summary>
    N2 = 4,
    /// <summary>
    /// Nitrate.
    /// </summary>
    NO3 = 5,
    /// <summary>
    /// Methane.
    /// </summary>
    CH4 = 6,
    /// <summary>
    /// Non Methane Volatile Organic Compound.
    /// </summary>
    NMVOC = 7,
    /// <summary>
    /// Particulate Matter2500 Nano Met.
    /// </summary>
    ParticulateMatter2500NanoMet = 8,
    /// <summary>
    /// Particulate Matter10000 Nano Met.
    /// </summary>
    ParticulateMatter10000NanoMet = 9,
    /// <summary>
    /// Total Suspended Particles.
    /// </summary>
    TotalSuspendedParticles = 10,
    N = 12,
    NO3_N = 13,
    /// <summary>
    /// Nitrogen supplied as urea (CH4N2O).
    /// </summary>
    CH4N2O_N = 14,
    /// <summary>
    /// Nitrogen supplied as ammonium nitrate (NH4NO3).
    /// </summary>
    NH4NO3_N = 15,
    /// <summary>
    /// Nitrogen supplied as calcium ammonium nitrate (CAN).
    /// </summary>
    CAN_N = 16,
    /// <summary>
    /// Nitrogen supplied as urea ammonium nitrate (UAN).
    /// </summary>
    UAN_N = 17,
    /// <summary>
    /// Nitrogen supplied as ammonium sulphate or diammonium phosphate (AS/DAP).
    /// </summary>
    AS_DAP_N = 18,
    /// <summary>
    /// Nitrogen supplied as other fertilisers, including compound blends.
    /// </summary>
    OtherFertilisers_N = 19,
}