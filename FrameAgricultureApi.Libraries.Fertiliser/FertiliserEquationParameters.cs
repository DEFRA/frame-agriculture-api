namespace FrameAgricultureApi.Libraries.Fertiliser;

/// <summary>
/// Fertiliser Equation Parameters class
/// </summary>
public static class FertiliserEquationParameters
{
    /// <summary>
    /// The fracLeach coefficient
    /// </summary>
    public static readonly double fracLeach;

    //N2ON from urea fertiliser
    /// <summary>
    /// Parameter 1 of the equation to calculate N2O-N emissions from urea fertilisers
    /// </summary>
    public static readonly double n2ON_Urea_Parameter_1 = 0.8404;               //Constant
    /// <summary>
    /// Parameter 2 of the equation to calculate N2O-N emissions from urea fertilisers
    /// </summary>
    public static readonly double n2ON_Urea_Parameter_2 = 0.001518;             //Rate multiplier
    /// <summary>
    /// Parameter 3 of the equation to calculate N2O-N emissions from urea fertilisers
    /// </summary>
    public static readonly double n2ON_Urea_Parameter_3 = 1.63;                //subtractor
    /// <summary>
    /// log normal transofrmation scalar for calculatino of N2O-N emissions from urea fertilisers
    /// </summary>
    public static readonly double n20N_Urea_lognormaltransformScalar = 1.01107; //Scalar

    //N2ON from other fertilisers
    /// <summary>
    /// Parameter 1 of the equation to calcualte N2O-N emissions from non-ureafertilisers
    /// </summary>
    public static readonly double n2ON_Other_Parameter_1 = 0.570;               //Constant
    /// <summary>
    /// Parameter 2 of the equation to calcualte N2O-N emissions from non-ureafertilisers
    /// </summary>
    public static readonly double n2ON_Other_Parameter_2 = 0.3962;              //Rainfall multiplier
    /// <summary>
    /// Maximum rainfall used in the equation to calcualte N2O-N emissions from non-ureafertilisers
    /// </summary>
    public static readonly double n2ON_Other_Parameter_MaxRainfall = 1300.0;    //maximum rainfall
    /// <summary>
    ///Divisor for rianfall used in the equation to calcualte N2O-N emissions from non-ureafertilisers
    /// </summary>
    public static readonly double n2ON_Other_Parameter_Divisor = 1000.0;        //Rainfall divisor
    /// <summary>
    /// Parameter 3 of the equation to calcualte N2O-N emissions from non-ureafertilisers
    /// </summary>
    public static readonly double n2ON_Other_Parameter_3 = 0.00019420;           //Fertiliser rate multiplier
    /// <summary>
    /// Parameter 4 of the equation to calcualte N2O-N emissions from non-ureafertilisers
    /// </summary>
    public static readonly double n2ON_Other_Parameter_4 = 0.0032480;           //Rainfall by Fertiliser rate multiplier
    /// <summary>
    /// Parameter 5 of the equation to calcualte N2O-N emissions from non-ureafertilisers
    /// </summary>
    public static readonly double n2ON_Other_Paramter_5 = 1.63;                 //Subtractor
    /// <summary>
    /// log normal transformation scalar for the equation to calcualte N2O-N emissions from non-ureafertilisers
    /// </summary>
    public static readonly double n20N_Other_lognormaltransformScalar = 1.0197; //Scalar

    //base ammonia EF
    /// <summary>
    /// Base ammonia emission factor for urea
    /// </summary>
    public static readonly double baseAmmoniaEF_Urea = 45.0;
    /// <summary>
    /// base ammonia emission factor for urea ammonium nitrate
    /// </summary>
    public static readonly double baseAmmoniaEF_UAN = 23.0;
    /// <summary>
    /// base ammonia emission factor for other N fertiliser
    /// </summary>
    public static readonly double baseAmmoniaED_Other = 1.8;

    //Ammonia EF fertiliser rate modification
    /// <summary>
    /// Parameter 1 for equation to modify ammonia emission factor by rate of fertiliser applied
    /// </summary>
    public static readonly double fertiliserRateAmmoniaEFModifier_1 = 0.62;
    /// <summary>
    /// Parameter 2 for equation to modify ammonia emission factor by rate of fertiliser applied
    /// </summary>
    public static readonly double fertiliserRateAmmoniaEFModifier_2 = 1.0;
    /// <summary>
    /// Parameter 3 for equation to modify ammonia emission factor by rate of fertiliser applied
    /// </summary>
    public static readonly double fertiliserRateAmmoniaEFModifier_3_Multiplier = 0.0032;
    /// <summary>
    /// Parameter 4 for equation to modify ammonia emission factor by rate of fertiliser applied
    /// </summary>
    public static readonly double fertiliserRateAmmoniaEFModifier_3_Constant = 0.5238;

    //Ammmonia EF rainfall event modification
    /// <summary>
    /// Multipliers for ammnia emission factor base on probability of a rianfall event in the 6 days following application
    /// </summary>
    public static readonly double[] rainfallEventMultipliers = { 0.3, 0.5, 0.7, 0.8, 0.9, 1.0 };

    //Ammonia EF temperature modifier
    /// <summary>
    /// Fixed parameters for modifying ammonia emission factor due to temperature
    /// </summary>
    public static readonly double[] AmmoniaEF_Temperature_FixedParameters = { 0.0, 1.0, 0.5 };
    /// <summary>
    /// Multiplier for ammonia emission factore when considering devation of temperature from average
    /// </summary>
    public static readonly double AmmoniaEF_TemperatureDifferrenceMultiplier = 0.1386;
    /// <summary>
    /// Average UK air teperature constant
    /// </summary>
    public static readonly double constantUKAverageAirTemperature = 8.9;

}
