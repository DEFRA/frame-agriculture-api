using System;

namespace FrameAgricultureApi.Libraries.CoverCrops;

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

