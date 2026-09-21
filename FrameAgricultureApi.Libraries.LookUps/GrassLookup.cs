using FrameAgricultureApi.CustomErrorHandling;
using FrameAgricultureApi.Generic;
using System.Reflection;
using static FrameAgricultureApi.Enumerators.Enumerators;

namespace FrameAgricultureApi.Libraries.LookUps;

/// <summary>
/// Grass lookup class
/// </summary>
public class GrassLookup
{
    /// <summary>
    /// private instance
    /// </summary>
    private static GrassLookup? _instance;

    /// <summary>
    /// public instance
    /// </summary>
    public static GrassLookup Instance
    {
        get
        {
            if(_instance == null)
            {
                _instance = new GrassLookup();
            }
            return _instance;
        }
    }

    //set arrays - indexed by soil climate region, soil texture, grass type, grass use, sown with clover, receieve managed manure
    private Dictionary<Tuple<int, int, int, int, int, int>, GrassEquationCoefficients> ResidualNitrogenCoefficients = [];
    private Dictionary<Tuple<int, int, int, int, int, int>, GrassEquationCoefficients> FracLeachCoefficients = [];
    private Dictionary<Tuple<int, int, int, int, int, int>, GrassEquationCoefficients> YieldDryMatterCoefficients = [];
    private Dictionary<Tuple<int, int, int, int, int, int>, GrassEquationCoefficients> AboveGroundDryMatterCoefficients = [];
    private Dictionary<Tuple<int, int, int, int, int, int>, GrassEquationCoefficients> BelowGroundDryMatterCoefficients = [];
    private Dictionary<Tuple<int, int, int, int, int, int>, GrassEquationCoefficients> NitrogenContentOfftakeCoefficients = [];
    private Dictionary<Tuple<int, int, int, int, int, int>, GrassEquationCoefficients> NitrogenfromCloverFixationCoefficients = [];

    //Set emission factors
    private double efDirectN2ON = double.NaN;
    private double efLeachedN2ON = double.NaN;
    private double efDepositinoN2ON = double.NaN;
    private double ratioNON = double.NaN;
    private double ratioN2N = double.NaN;

    private Dictionary<int, double> efNMVOC = [];
    private Dictionary<int, double> efPM2_5_Cultivation = [];
    private Dictionary<int, double> efPM2_5_Harvesting = [];
    private Dictionary<int, double> efPM2_5_Cleaning = [];
    private Dictionary<int, double> efPM2_5_Drying = [];
    private Dictionary<int, double> efPM10_Cultivation = [];
    private Dictionary<int, double> efPM10_Harvesting = [];
    private Dictionary<int, double> efPM10_Cleaning = [];
    private Dictionary<int, double> efPM10_Drying = [];


    private GrassLookup()
        {
            try
            {
                //clear dictionaries
                ResidualNitrogenCoefficients.Clear();
                FracLeachCoefficients.Clear();
                YieldDryMatterCoefficients.Clear();
                AboveGroundDryMatterCoefficients.Clear();
                BelowGroundDryMatterCoefficients.Clear();
                NitrogenfromCloverFixationCoefficients.Clear();

                efNMVOC.Clear();
                efPM2_5_Cultivation.Clear();
                efPM2_5_Harvesting.Clear();
                efPM10_Cultivation.Clear();
                efPM10_Harvesting.Clear();
                efPM10_Cleaning.Clear();
                efPM10_Drying.Clear();
                efPM2_5_Cleaning.Clear();
                efPM2_5_Drying.Clear();

                using(MemoryStream ms = HelperFunctions.GetLUT(Assembly.GetExecutingAssembly(), "FLEA_GrassResidue_DefaultEmissionFactors_LUT.dat"))
                {
                    using BinaryReader br = new BinaryReader(ms);
                    efDirectN2ON = br.ReadDouble();
                    efLeachedN2ON = br.ReadDouble();
                    efDepositinoN2ON = br.ReadDouble();
                    ratioNON = br.ReadDouble();
                    ratioN2N = br.ReadDouble();
                }

            try
            {
                //Residual Nitrogen LUT

                using (MemoryStream ms = HelperFunctions.GetLUT(Assembly.GetExecutingAssembly(), "FLEA_GrassModel_ResidueN_LUT.dat"))
                {
                    using BinaryReader br = new BinaryReader(ms);

                    while (br.BaseStream.Position < br.BaseStream.Length)
                    {
                        int ClimateRegion = br.ReadInt32();
                        int SoilType = br.ReadInt32();
                        int GrassType = br.ReadInt32();
                        int GrassUseType = br.ReadInt32();
                        int sownwithClover = br.ReadInt32();
                        int receivesmanure = br.ReadInt32();
                        GrassEquationCoefficients data = new GrassEquationCoefficients(br.ReadDouble(), br.ReadDouble(), br.ReadDouble(), br.ReadDouble(), br.ReadDouble());
                        Tuple<int, int, int, int, int, int> key = new(ClimateRegion, SoilType, GrassType, GrassUseType, sownwithClover, receivesmanure);

                        if (!ResidualNitrogenCoefficients.TryAdd(key, data))
                        {
                            throw new CustomAppException("Error adding residue N parameters to dictionary for key: " + key.Item1 + "-" +
                                key.Item2 + "-" + key.Item3 + "-" + key.Item4 + "-" + key.Item5 + "-" + key.Item6 + ".");
                        }
                    }
                }

                //FracLeach

                using (MemoryStream ms = HelperFunctions.GetLUT(Assembly.GetExecutingAssembly(), "FLEA_GrassModel_FracLeach_LUT.dat"))
                {
                    using BinaryReader br = new BinaryReader(ms);
                    while (br.BaseStream.Position < br.BaseStream.Length)
                    {
                        int ClimateRegion = br.ReadInt32();
                        int SoilType = br.ReadInt32();
                        int GrassType = br.ReadInt32();
                        int GrassUseType = br.ReadInt32();
                        int sownwithClover = br.ReadInt32();
                        int receivesmanure = br.ReadInt32();
                        Tuple<int, int, int, int, int, int> key = new(ClimateRegion, SoilType, GrassType, GrassUseType, sownwithClover, receivesmanure);

                        GrassEquationCoefficients data = new GrassEquationCoefficients(br.ReadDouble(), br.ReadDouble(), br.ReadDouble(), br.ReadDouble(), br.ReadDouble());

                        if (!FracLeachCoefficients.TryAdd(key, data))
                        {
                            throw new CustomAppException("Error adding frac leach parameters to dictionary for key: " + key.Item1 + "-" +
                                key.Item2 + "-" + key.Item3 + "-" + key.Item4 + "-" + key.Item5 + "-" + key.Item6 + ".");
                        }
                    }
                }

                //Yield Dry Matter

                using (MemoryStream ms = HelperFunctions.GetLUT(Assembly.GetExecutingAssembly(), "FLEA_GrassModel_YieldDryMatter_LUT.dat"))
                {
                    using BinaryReader br = new BinaryReader(ms);
                    while (br.BaseStream.Position < br.BaseStream.Length)
                    {
                        int ClimateRegion = br.ReadInt32();
                        int SoilType = br.ReadInt32();
                        int GrassType = br.ReadInt32();
                        int GrassUseType = br.ReadInt32();
                        int sownwithClover = br.ReadInt32();
                        int receivesmanure = br.ReadInt32();
                        Tuple<int, int, int, int, int, int> key = new(ClimateRegion, SoilType, GrassType, GrassUseType, sownwithClover, receivesmanure);

                        GrassEquationCoefficients data = new GrassEquationCoefficients(br.ReadDouble(), br.ReadDouble(), br.ReadDouble(), br.ReadDouble(), br.ReadDouble());
                        if (!YieldDryMatterCoefficients.TryAdd(key, data))
                        {
                            throw new CustomAppException("Error adding yield dry matter parameters to dictionary for key: " + key.Item1 + "-" +
                                key.Item2 + "-" + key.Item3 + "-" + key.Item4 + "-" + key.Item5 + "-" + key.Item6 + ".");

                        }
                    }
                }

                //Above Groudn residue Dry Matter

                using (MemoryStream ms = HelperFunctions.GetLUT(Assembly.GetExecutingAssembly(), "FLEA_GrassModel_AboveGroundResidueDryMatter_LUT.dat"))
                {
                    using BinaryReader br = new BinaryReader(ms);
                    while (br.BaseStream.Position < br.BaseStream.Length)
                    {

                        int ClimateRegion = br.ReadInt32();
                        int SoilType = br.ReadInt32();
                        int GrassType = br.ReadInt32();
                        int GrassUseType = br.ReadInt32();
                        int sownwithClover = br.ReadInt32();
                        int receivesmanure = br.ReadInt32();
                        Tuple<int, int, int, int, int, int> key = new(ClimateRegion, SoilType, GrassType, GrassUseType, sownwithClover, receivesmanure);

                        GrassEquationCoefficients data = new GrassEquationCoefficients(br.ReadDouble(), br.ReadDouble(), br.ReadDouble(), br.ReadDouble(), br.ReadDouble());
                        if (!AboveGroundDryMatterCoefficients.TryAdd(key, data))
                        {
                            throw new CustomAppException("Error adding above ground dry matter parameters to dictionary for key: " + key.Item1 + "-" +
                                key.Item2 + "-" + key.Item3 + "-" + key.Item4 + "-" + key.Item5 + "-" + key.Item6 + ".");

                        }
                    }
                }

                //Below Groudn residue Dry Matter

                using (MemoryStream ms = HelperFunctions.GetLUT(Assembly.GetExecutingAssembly(), "FLEA_GrassModel_BelowGroundResidueDryMatter_LUT.dat"))
                {
                    using BinaryReader br = new BinaryReader(ms);
                    while (br.BaseStream.Position < br.BaseStream.Length)
                    {
                        int ClimateRegion = br.ReadInt32();
                        int SoilType = br.ReadInt32();
                        int GrassType = br.ReadInt32();
                        int GrassUseType = br.ReadInt32();
                        int sownwithClover = br.ReadInt32();
                        int receivesmanure = br.ReadInt32();
                        Tuple<int, int, int, int, int, int> key = new(ClimateRegion, SoilType, GrassType, GrassUseType, sownwithClover, receivesmanure);

                        GrassEquationCoefficients data = new GrassEquationCoefficients(br.ReadDouble(), br.ReadDouble(), br.ReadDouble(), br.ReadDouble(), br.ReadDouble());
                        if (!BelowGroundDryMatterCoefficients.TryAdd(key, data))
                        {
                            throw new CustomAppException("Error adding below ground dry matter parameters to dictionary for key: " + key.Item1 + "-" +
                                key.Item2 + "-" + key.Item3 + "-" + key.Item4 + "-" + key.Item5 + "-" + key.Item6 + ".");

                        }
                    }
                }

                //Nitrogen in grass cut or eaten

                using (MemoryStream ms = HelperFunctions.GetLUT(Assembly.GetExecutingAssembly(), "FLEA_GrassModel_NContent_offtake_LUT.dat"))
                {
                    using BinaryReader br = new BinaryReader(ms);
                    while (br.BaseStream.Position < br.BaseStream.Length)
                    {
                        int ClimateRegion = br.ReadInt32();
                        int SoilType = br.ReadInt32();
                        int GrassType = br.ReadInt32();
                        int GrassUseType = br.ReadInt32();
                        int sownwithClover = br.ReadInt32();
                        int receivesmanure = br.ReadInt32();
                        Tuple<int, int, int, int, int, int> key = new(ClimateRegion, SoilType, GrassType, GrassUseType, sownwithClover, receivesmanure);

                        GrassEquationCoefficients data = new GrassEquationCoefficients(br.ReadDouble(), br.ReadDouble(), br.ReadDouble(), br.ReadDouble(), br.ReadDouble());
                        if (!NitrogenContentOfftakeCoefficients.TryAdd(key, data))
                        {
                            throw new CustomAppException("Error adding N in grass cut or eaten parameters to dictionary for key: " + key.Item1 + "-" +
                                key.Item2 + "-" + key.Item3 + "-" + key.Item4 + "-" + key.Item5 + "-" + key.Item6 + ".");

                        }
                    }
                }

                //Nitrogen from Clover Fixation

                using (MemoryStream ms = HelperFunctions.GetLUT(Assembly.GetExecutingAssembly(), "FLEA_GrassModel_CloverNitrogenFixation_LUT.dat"))
                {
                    using BinaryReader br = new BinaryReader(ms);
                    while (br.BaseStream.Position < br.BaseStream.Length)
                    {
                        int ClimateRegion = br.ReadInt32();
                        int SoilType = br.ReadInt32();
                        int GrassType = br.ReadInt32();
                        int GrassUseType = br.ReadInt32();
                        int sownwithClover = br.ReadInt32();
                        int receivesmanure = br.ReadInt32();
                        Tuple<int, int, int, int, int, int> key = new(ClimateRegion, SoilType, GrassType, GrassUseType, sownwithClover, receivesmanure);

                        GrassEquationCoefficients data = new GrassEquationCoefficients(br.ReadDouble(), br.ReadDouble(), br.ReadDouble(), br.ReadDouble(), br.ReadDouble());
                        if (!NitrogenfromCloverFixationCoefficients.TryAdd(key, data))
                        {
                            throw new CustomAppException("Error adding N from clover fixation parameters to dictionary for key: " + key.Item1 + "-" +
                                key.Item2 + "-" + key.Item3 + "-" + key.Item4 + "-" + key.Item5 + "-" + key.Item6 + ".");

                        }
                    }
                }
            }
            catch(Exception ex)
            {
                throw new CustomAppException("Error loading grass model coefficients into Grass Lookup Singleton: " + ex.Message + ".");
            }

            try
            {
                //Non GHG Emission factors

                using MemoryStream ms = HelperFunctions.GetLUT(Assembly.GetExecutingAssembly(), "FLEA_Grass_NonGHG_LUT.dat");
                using BinaryReader br = new BinaryReader(ms);
                while(br.BaseStream.Position < br.BaseStream.Length)
                {
                    int grassType = br.ReadInt32();
                    efNMVOC.Add(grassType, br.ReadDouble());
                    efPM2_5_Cultivation.Add(grassType, br.ReadDouble());
                    efPM2_5_Harvesting.Add(grassType, br.ReadDouble());
                    efPM2_5_Cleaning.Add(grassType, br.ReadDouble());
                    efPM2_5_Drying.Add(grassType, br.ReadDouble());
                    efPM10_Cultivation.Add(grassType, br.ReadDouble());
                    efPM10_Harvesting.Add(grassType, br.ReadDouble());
                    efPM10_Cleaning.Add(grassType, br.ReadDouble());
                    efPM10_Drying.Add(grassType, br.ReadDouble());
                }
            }
            catch(Exception ex)
            {
                throw new CustomAppException("Error loading nonGHG emission factors: " + ex.Message + ".");
            }
        }
        catch(Exception excep)
        {
            throw new CustomAppException("Error loading grass coefficients data from file:" + excep.Message + ".\nThe singleton has not been created.");
        }
    }

    /// <summary>
    /// Emission factor for direct nitrous oxide
    /// </summary>
    /// <returns>Emission factor as percentage</returns>
    public double DirectN2ONEmissionFactor()
    {
        return efDirectN2ON;
    }
    /// <summary>
    /// Emission factor for nitrous oxide from leached nitrogen
    /// </summary>
    /// <returns>Emission factor as percentage</returns>
    public double LeachedN2ONEmissionFactor()
    {
        return efLeachedN2ON;
    }
    /// <summary>
    /// Emission factor for direct nitrous oxide form redeposited nitrogen
    /// </summary>
    /// <returns>Emission factor as percentage</returns>
    public double DepositionN2ONEmissinFactor()
    {
        return efDepositinoN2ON;
    }
    /// <summary>
    /// Ratio of NON to N2ON
    /// </summary>
    /// <returns>Ratio as double</returns>
    public double RationNON()
    {
        return ratioNON;
    }
    /// <summary>
    /// Ratio of N2N to N2ON
    /// </summary>
    /// <returns>ratio as double</returns>
    public double RatioN2N()
    {
        return ratioN2N;
    }
    /// <summary>
    /// NMVOC emission facotr
    /// </summary>
    /// <returns>kg NMVOC per hectare</returns>
    public double EF_NMVOC(int grassType)
    {
        return efNMVOC[grassType];
    }
    /// <summary>
    /// PM 2.5 from cultivation emission factor
    /// </summary>
    /// <returns>kg PM2.5 per hectare</returns>
    public double EF_PM2_5_Cultivation(int grassType)
    {
        return efPM2_5_Cultivation[grassType];
    }
    /// <summary>
    /// PM 2.5 from Harvesting emission factor
    /// </summary>
    /// <returns>kg PM2.5 per hectare</returns>
    public double EF_PM2_5_Harvesting(int grassType)
    {
        return efPM2_5_Harvesting[grassType];
    }
    /// <summary>
    /// PM 2.5 from cleaning emission factor
    /// </summary>
    /// <returns>kg PM2.5 per hectare</returns>
    public double EF_PM2_5_Cleaning(int grassType)
    {
        return efPM2_5_Cleaning[grassType];
    }
    /// <summary>
    /// PM 2.5 from drying emission factor
    /// </summary>
    /// <returns>kg PM2.5 per hectare</returns>
    public double EF_PM2_5_Drying(int grassType)
    {
        return efPM2_5_Drying[grassType];
    }
    /// <summary>
    /// PM 10 from cultivation emission factor
    /// </summary>
    /// <returns>kg PM10 per hectare</returns>
    public double EF_PM10_Cultivation(int grassType)
    {
        return efPM10_Cultivation[grassType];
    }
    /// <summary>
    /// PM 10 from Harvesting emission factor
    /// </summary>
    /// <returns>kg PM10 per hectare</returns>
    public double EF_PM10_Harvesting(int grassType)
    {
        return efPM10_Harvesting[grassType];
    }
    /// <summary>
    /// PM 10 from Cleaning emission factor
    /// </summary>
    /// <returns>kg PM10 per hectare</returns>
    public double EF_PM10_Cleaning(int grassType)
    {
        return efPM10_Cleaning[grassType];
    }
    /// <summary>
    /// PM 10 from Drying emission factor
    /// </summary>
    /// <returns>kg PM10 per hectare</returns>
    public double EF_PM10_Drying(int grassType)
    {
        return efPM10_Drying[grassType];
    }
    /// <summary>
    /// Retrieve climate region
    /// </summary>
    /// <param name="locationID">ID of the 10 km grid squre (integer)</param>
    /// <returns>CLimate region ID (integer)</returns>
    public int ClimateRegion(int locationID)
    {
        return PhysicalLookup.ClimateRegion(locationID);
    }
    /// <summary>
    /// Return polynomial equation coefficients for calcualtion of grass esidue emissions based
    /// </summary>
    /// <param name="climateRegion">clmimate region ID</param>
    /// <param name="soiltexturetype">soil texture type enumerator as integer</param>
    /// <param name="grassType">grass type enumerator as integer</param>
    /// <param name="grassUseType">grasss use type enumerator as integer</param>
    /// <param name="sownWithClover">boolean indicating if sown with clover</param>
    /// <param name="receivesManagedManure">boolean indicating if receives managed manure inputs</param>
    /// <returns></returns>
    public Dictionary<int, GrassEquationCoefficients> GrassModelCoefficients(int climateRegion, int soiltexturetype, int grassType, int grassUseType, bool sownWithClover, bool receivesManagedManure)
    {
        Dictionary<int, GrassEquationCoefficients> retdict = new Dictionary<int, GrassEquationCoefficients>();

        //define clover and manure indices for LUT data retireval
        int clover = sownWithClover ? 1 : 2;
        int manure = receivesManagedManure ? 1 : 2;

        //define required key
        Tuple<int, int, int, int, int, int> thekey = new(climateRegion, soiltexturetype, grassType, grassUseType, clover, manure);

        //Residue Nitrogen
        retdict.Add((int)GrassEquations.ResidualNitrogen, Instance.ResidualNitrogenCoefficients[thekey]);

        //FracLeach
        retdict.Add((int)GrassEquations.FracLeach, Instance.FracLeachCoefficients[thekey]);

        //Yield Dry Matter
        retdict.Add((int)GrassEquations.YieldDryMatter, Instance.YieldDryMatterCoefficients[thekey]);

        //Above Ground Residue Dry Matter
        retdict.Add((int)GrassEquations.AboveGroundDryMatter, Instance.AboveGroundDryMatterCoefficients[thekey]);

        //Below Ground Residue Dry Matter
        retdict.Add((int)GrassEquations.BelowGroundDryMatter, Instance.BelowGroundDryMatterCoefficients[thekey]);

        //Nitrogen in Grass Consumed by Livestock
        retdict.Add((int)GrassEquations.NitrogeninGrassHarvestedorEaten, Instance.NitrogenContentOfftakeCoefficients[thekey]);

        //Nitrogen from Clover fixation
        retdict.Add((int)GrassEquations.NitrogenfromCloverFixation, Instance.NitrogenfromCloverFixationCoefficients[thekey]);

        return retdict;
    }
    /// <summary>
    /// Return frac leach coefficients
    /// </summary>
    /// <param name="climateRegion">Climate region enumerator as integer</param>
    /// <param name="soiltexturetype">soil type enumerator as integer</param>
    /// <param name="grassType">grass type integer as enumerator</param>
    /// <param name="grassUseType">grass use type enumerator as integer</param>
    /// <param name="sownWithClover">boolean indicating if grass is sown with clover</param>
    /// <param name="receivesManagedManure">boolean indicating if grass ecieves managed manure applications</param>
    /// <returns>Grass coefficient object with relevant coefficies for fourht order polynomial</returns>
    public GrassEquationCoefficients GrassFracLeachCoefficients(int climateRegion, int soiltexturetype, int grassType, int grassUseType, bool sownWithClover, bool receivesManagedManure)
    {

        //define clover and manure indices for LUT data retireval
        int clover = sownWithClover ? 1 : 2;
        int manure = receivesManagedManure ? 1 : 2;

        //define required key
        Tuple<int, int, int, int, int, int> thekey = new(climateRegion, soiltexturetype, grassType, grassUseType, clover, manure);
        if(FracLeachCoefficients.TryGetValue(thekey, out var coefficients))
        {
            return coefficients;
        }
        else
        {
            throw new CustomAppException("unable to find  key: " + thekey.Item1.ToString() + " : " + thekey.Item2.ToString() + " : " +
                thekey.Item3.ToString() + " : " + thekey.Item4.ToString() + " : " + thekey.Item5.ToString() + " : " +
                thekey.Item6.ToString() + " in Nitrogen in grass consumed by livetock lookup table.");
        }
    }
    /// <summary>
    /// Return residual nitrogen coefficients
    /// </summary>
    /// <param name="climateRegion">Climate region enumerator as integer</param>
    /// <param name="soiltexturetype">soil type enumerator as integer</param>
    /// <param name="grassType">grass type integer as enumerator</param>
    /// <param name="grassUseType">grass use type enumerator as integer</param>
    /// <param name="sownWithClover">boolean indicating if grass is sown with clover</param>
    /// <param name="receivesManagedManure">boolean indicating if grass ecieves managed manure applications</param>
    /// <returns>Grass coefficient object with relevant coefficies for fourht order polynomial</returns>
    public GrassEquationCoefficients ResidualNitrogen(int climateRegion, int soiltexturetype, int grassType, int grassUseType, bool sownWithClover, bool receivesManagedManure)
    {

        //define clover and manure indices for LUT data retireval
        int clover = sownWithClover ? 1 : 2;
        int manure = receivesManagedManure ? 1 : 2;

            //define required key
            Tuple<int, int, int, int, int, int> thekey = new(climateRegion, soiltexturetype, grassType, grassUseType, clover, manure);
            if (ResidualNitrogenCoefficients.TryGetValue(thekey, out var coefficients))
            {
                return coefficients;
            }
            else
            {
                throw new CustomAppException("unable to find  key: " + thekey.Item1.ToString() + " : " + thekey.Item2.ToString() + " : " +
                    thekey.Item3.ToString() + " : " + thekey.Item4.ToString() + " : " + thekey.Item5.ToString() + " : " +
                    thekey.Item6.ToString() + " in Nitrogen in grass consumed by livetock lookup table.");
            }
        }
        /// <summary>
        /// Return coefficients for grass nitrogen content when consumed by livestock or cut
        /// </summary>
        /// <param name="climateRegion">Climate region enumerator as integer</param>
        /// <param name="soiltexturetype">soil type enumerator as integer</param>
        /// <param name="grassType">grass type integer as enumerator</param>
        /// <param name="grassUseType">grass use type enumerator as integer</param>
        /// <param name="sownWithClover">boolean indicating if grass is sown with clover</param>
        /// <param name="receivesManagedManure">boolean indicating if grass ecieves managed manure applications</param>
        /// <returns>Grass coefficient object with relevant coefficies for fourht order polynomial</returns>
        public GrassEquationCoefficients NitrogeninGrassCutorEaten(int climateRegion, int soiltexturetype, int grassType, int grassUseType, bool sownWithClover, bool receivesManagedManure)
        {

        //define clover and manure indices for LUT data retireval
        int clover = sownWithClover ? 1 : 2;
        int manure = receivesManagedManure ? 1 : 2;

            //define required key
            Tuple<int, int, int, int, int, int> thekey = new(climateRegion, soiltexturetype, grassType, grassUseType, clover, manure);
            if (NitrogenContentOfftakeCoefficients.TryGetValue(thekey, out var coefficients))
            {
                return coefficients;
            }
            else
            {
                throw new CustomAppException("unable to find  key: " + thekey.Item1.ToString() + " : " +  thekey.Item2.ToString() + " : " +
                    thekey.Item3.ToString() + " : " + thekey.Item4.ToString() + " : " + thekey.Item5.ToString() + " : " +
                    thekey.Item6.ToString() + " in Nitrogen in grass consumed by livetock lookup table.");
            }
        }
        /// <summary>
        /// Return Yield dry matter coefficients
        /// </summary>
        /// <param name="climateRegion">Climate region enumerator as integer</param>
        /// <param name="soiltexturetype">soil type enumerator as integer</param>
        /// <param name="grassType">grass type integer as enumerator</param>
        /// <param name="grassUseType">grass use type enumerator as integer</param>
        /// <param name="sownWithClover">boolean indicating if grass is sown with clover</param>
        /// <param name="receivesManagedManure">boolean indicating if grass ecieves managed manure applications</param>
        /// <returns>Grass coefficient object with relevant coefficies for fourht order polynomial</returns>
        public GrassEquationCoefficients YieldDryMatter(int climateRegion, int soiltexturetype, int grassType, int grassUseType, bool sownWithClover, bool receivesManagedManure)
        {

        //define clover and manure indices for LUT data retireval
        int clover = sownWithClover ? 1 : 2;
        int manure = receivesManagedManure ? 1 : 2;

        //define required key
        Tuple<int, int, int, int, int, int> thekey = new(climateRegion, soiltexturetype, grassType, grassUseType, clover, manure);
        if(YieldDryMatterCoefficients.TryGetValue(thekey, out var coefficients))
        {
            return coefficients;
        }
        else
        {
            throw new CustomAppException("unable to find  key: " + thekey.Item1.ToString() + " : " + thekey.Item2.ToString() + " : " +
                thekey.Item3.ToString() + " : " + thekey.Item4.ToString() + " : " + thekey.Item5.ToString() + " : " +
                thekey.Item6.ToString() + " in Nitrogen in grass yield dry matter lookup table.");
        }
    }
    /// <summary>
    /// Return Above ground dry matter coefficients
    /// </summary>
    /// <param name="climateRegion">Climate region enumerator as integer</param>
    /// <param name="soiltexturetype">soil type enumerator as integer</param>
    /// <param name="grassType">grass type integer as enumerator</param>
    /// <param name="grassUseType">grass use type enumerator as integer</param>
    /// <param name="sownWithClover">boolean indicating if grass is sown with clover</param>
    /// <param name="receivesManagedManure">boolean indicating if grass ecieves managed manure applications</param>
    /// <returns>Grass coefficient object with relevant coefficies for fourht order polynomial</returns>
    public GrassEquationCoefficients AboveGroundDryMatter(int climateRegion, int soiltexturetype, int grassType, int grassUseType, bool sownWithClover, bool receivesManagedManure)
    {

        //define clover and manure indices for LUT data retireval
        int clover = sownWithClover ? 1 : 2;
        int manure = receivesManagedManure ? 1 : 2;

        //define required key
        Tuple<int, int, int, int, int, int> thekey = new(climateRegion, soiltexturetype, grassType, grassUseType, clover, manure);
        if(AboveGroundDryMatterCoefficients.TryGetValue(thekey, out var coefficients))
        {
            return coefficients;
        }
        else
        {
            throw new CustomAppException("unable to find  key: " + thekey.Item1.ToString() + " : " + thekey.Item2.ToString() + " : " +
                thekey.Item3.ToString() + " : " + thekey.Item4.ToString() + " : " + thekey.Item5.ToString() + " : " +
                thekey.Item6.ToString() + " in Nitrogen in grass above ground dry matter lookup table.");
        }
    }
    /// <summary>
    /// Return Above ground dry matter coefficients
    /// </summary>
    /// <param name="climateRegion">Climate region enumerator as integer</param>
    /// <param name="soiltexturetype">soil type enumerator as integer</param>
    /// <param name="grassType">grass type integer as enumerator</param>
    /// <param name="grassUseType">grass use type enumerator as integer</param>
    /// <param name="sownWithClover">boolean indicating if grass is sown with clover</param>
    /// <param name="receivesManagedManure">boolean indicating if grass ecieves managed manure applications</param>
    /// <returns>Grass coefficient object with relevant coefficies for fourht order polynomial</returns>
    public GrassEquationCoefficients BelowGroundDryMatter(int climateRegion, int soiltexturetype, int grassType, int grassUseType, bool sownWithClover, bool receivesManagedManure)
    {

        //define clover and manure indices for LUT data retireval
        int clover = sownWithClover ? 1 : 2;
        int manure = receivesManagedManure ? 1 : 2;

            //define required key
            Tuple<int, int, int, int, int, int> thekey = new(climateRegion, soiltexturetype, grassType, grassUseType, clover, manure);
            if (BelowGroundDryMatterCoefficients.TryGetValue(thekey, out var coefficients))
            {
                return coefficients;
            }
            else
            {
                throw new CustomAppException("unable to find  key: " + thekey.Item1.ToString() + " : " + thekey.Item2.ToString() + " : " +
                    thekey.Item3.ToString() + " : " + thekey.Item4.ToString() + " : " + thekey.Item5.ToString() + " : " +
                    thekey.Item6.ToString() + " in Nitrogen in grass below ground dry matter lookup table.");
            }
        }

    /// <summary>
    /// Return Nitrogen from clover fixationcoefficients
    /// </summary>
    /// <param name="climateRegion">Climate region enumerator as integer</param>
    /// <param name="soiltexturetype">soil type enumerator as integer</param>
    /// <param name="grassType">grass type integer as enumerator</param>
    /// <param name="grassUseType">grass use type enumerator as integer</param>
    /// <param name="sownWithClover">boolean indicating if grass is sown with clover</param>
    /// <param name="receivesManagedManure">boolean indicating if grass ecieves managed manure applications</param>
    /// <returns>Grass coefficient object with relevant coefficietss for fourth order polynomial</returns>
    public GrassEquationCoefficients NitrogenFromCloverFixation(int climateRegion, int soiltexturetype, int grassType, int grassUseType, bool sownWithClover, bool receivesManagedManure)
    {

        //define clover and manure indices for LUT data retireval
        int clover = sownWithClover ? 1 : 2;
        int manure = receivesManagedManure ? 1 : 2;

        //define required key
        Tuple<int, int, int, int, int, int> thekey = new(climateRegion, soiltexturetype, grassType, grassUseType, clover, manure);
        if(NitrogenfromCloverFixationCoefficients.TryGetValue(thekey, out var coefficients))
        {
            return coefficients;
        }
        else
        {
            throw new CustomAppException("unable to find  key: " + thekey.Item1.ToString() + " : " + thekey.Item2.ToString() + " : " +
                thekey.Item3.ToString() + " : " + thekey.Item4.ToString() + " : " + thekey.Item5.ToString() + " : " +
                thekey.Item6.ToString() + " in Nitrogen in grass harvestedlookup table.");
        }
    }
}
/// <summary>
/// Grass polynomial equation coefficients object
/// </summary>
public class GrassEquationCoefficients
{
    /// <summary>
    /// The constant
    /// </summary>
    public double C { get; set; }
    /// <summary>
    /// Multiplier for X
    /// </summary>
    public double Coeff_X { get; set; }
    /// <summary>
    /// Multiplier for X squared
    /// </summary>
    public double Coeff_X2 { get; set; }
    /// <summary>
    /// Multiplier for X cubed
    /// </summary>
    public double Coeff_X3 { get; set; }
    /// <summary>
    /// Multiplier for the fourth power of X
    /// </summary>
    public double Coeff_X4 { get; set; }
    /// <summary>
    /// Base constructor
    /// </summary>
    public GrassEquationCoefficients()
    {
        C = double.NaN;
        Coeff_X = double.NaN;
        Coeff_X2 = double.NaN;
        Coeff_X3 = double.NaN;
        Coeff_X4 = double.NaN;
    }
    /// <summary>
    /// COnstructor
    /// </summary>
    /// <param name="constant">Constant</param>
    /// <param name="coeff_X">Mmultiplier for the x value</param>
    /// <param name="coeff_X2">Multiplier for square of x value</param>
    /// <param name="coeff_X3">Multiplier fo cube of X value</param>
    /// <param name="coeff_X4">Multiplier for fourht power of X value</param>
    public GrassEquationCoefficients(double constant, double coeff_X, double coeff_X2, double coeff_X3, double coeff_X4)
    {
        C = constant;
        Coeff_X = coeff_X;
        Coeff_X2 = coeff_X2;
        Coeff_X3 = coeff_X3;
        Coeff_X4 = coeff_X4;
    }
}
