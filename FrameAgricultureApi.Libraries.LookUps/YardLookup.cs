using FrameAgricultureApi.CustomErrorHandling;
using FrameAgricultureApi.Generic;
using System.Reflection;
using static FrameAgricultureApi.Enumerators.Enumerators;

namespace FrameAgricultureApi.Libraries.LookUps;

/// <summary>
/// Manure Management LookUp Singleton class
/// </summary>
public class YardLookup
{
    private static YardLookup? _instance;

    /// <summary>
    /// Instance of singleton
    /// </summary>
    public static YardLookup Instance
    {
        get
        {
            _instance ??= new YardLookup();
            return _instance;
        }
    }

    //Define variables
    private readonly Dictionary<YardKey, YardData> _YardInfo = new Dictionary<YardKey, YardData>();

    /// <summary>
    /// COnstructor - reads data from LUT file
    /// </summary>
    /// <exception cref="Exception">Errors occuring in reading LUT file</exception>
    public YardLookup()
    {
        MemoryStream memoryStream = HelperFunctions.GetLUT(Assembly.GetExecutingAssembly(), "FLEA_ManureManagement_Yards_LUT.dat");
        try
        {
            using BinaryReader binaryReader = new(memoryStream);
            //Read in the Yard lookup information
            while(binaryReader.BaseStream.Position < binaryReader.BaseStream.Length)
            {
                int sectorID = binaryReader.ReadInt32();
                int animalType = binaryReader.ReadInt32();
                string yardtype = binaryReader.ReadString();
                bool collecting = yardtype.ToLower().Equals("collecting");
                YardKey theKey = new YardKey();
                theKey.Sector = (Sector)sectorID;
                theKey.AnimalType = animalType;
                theKey.CollectingYard = collecting;

                YardData theData = new YardData();
                theData.ScrapingEfficiency = binaryReader.ReadDouble();
                theData.EmissionFactorN2ON = binaryReader.ReadDouble();
                theData.EmissionFactorNH3N = binaryReader.ReadDouble();
                theData.RatioNON = binaryReader.ReadDouble();
                theData.RatioN2N = binaryReader.ReadDouble();
                theData.MethaneConversionFactor = binaryReader.ReadDouble();

                _YardInfo.Add(theKey, theData);
            }
        }
        catch(Exception ex)
        {
            throw new CustomAppException("Error loading yard information: " + ex.Message);
        }
    }

    /// <summary>
    /// Retrieve emission ratio for NO-N
    /// </summary>
    /// <param name="sector">Sector enumerator (shoudl be only beef or dairy)</param>
    /// <param name="animalType">Animal type enumerator as integer</param>
    /// <param name="collectingYard">optional boolean to distinguish yard types (collecting yard = true, feeding yard = false)</param>
    /// <returns>NON Emission Factor as ratio of Nitrouse Oxides (as N) Emission</returns>
    public double RetrieveNONRatio(Sector sector, int animalType, bool collectingYard = false)
    {
        if(sector.Equals(Sector.Dairy) || sector.Equals(Sector.Beef))
        {
            YardKey key = new YardKey(sector, animalType, collectingYard);
            return Instance._YardInfo[key].RatioNON;
        }
        else
        {
            return double.NaN;
        }
    }
    /// <summary>
    /// Retrieve emission ratio for N2-N
    /// </summary>
    /// <param name="sector">Sector enumerator (shoudl be only beef or dairy)</param>
    /// <param name="animalType">Animal type enumerator as integer</param>
    /// <param name="collectingYard">optional boolean to distinguish yard types (collecting yard = true, feeding yard = false)</param>
    /// <returns>N2N Emission Factor as ratio of Nitrouse Oxides (as N) Emission</returns>
    public double RetrieveN2NRatio(Sector sector, int animalType, bool collectingYard = false)
    {
        if(sector.Equals(Sector.Dairy) || sector.Equals(Sector.Beef))
        {
            YardKey key = new YardKey(sector, animalType, collectingYard);
            return Instance._YardInfo[key].RatioN2N;
        }
        else
        {
            return double.NaN;
        }
    }
    /// <summary>
    /// Retrieve emission factor for N2O-N
    /// </summary>
    /// <param name="sector">Sector enumerator (shoudl be only beef or dairy)</param>
    /// <param name="animalType">Animal type enumerator as integer</param>
    /// <param name="collectingYard">optional boolean to distinguish yard types (collecting yard = true, feeding yard = false)</param>
    /// <returns>NON Emission Factor as ratio of Nitrouse Oxides (as N) Emission</returns>
    public double RetrieveN2ONEmissionFactor(Sector sector, int animalType, bool collectingYard = false)
    {
        if(sector.Equals(Sector.Dairy) || sector.Equals(Sector.Beef))
        {
            YardKey key = new YardKey(sector, animalType, collectingYard);
            return Instance._YardInfo[key].EmissionFactorN2ON;
        }
        else
        {
            return double.NaN;
        }
    }
    /// <summary>
    /// Retrieve emission factor for NH3-N
    /// </summary>
    /// <param name="sector">Sector enumerator (shoudl be only beef or dairy)</param>
    /// <param name="animalType">Animal type enumerator as integer</param>
    /// <param name="collectingYard">boolean indicating if collecting yard (true) or feeding yard (false)</param>
    /// <returns>NON Emission Factor as ratio of Nitrouse Oxides (as N) Emission</returns>
    public double RetrieveNH3NEmissionFactor(Sector sector, int animalType, bool collectingYard = false)
    {
        if(sector.Equals(Sector.Dairy) || sector.Equals(Sector.Beef))
        {
            YardKey key = new YardKey(sector, animalType, collectingYard);
            return Instance._YardInfo[key].EmissionFactorNH3N;
        }
        else
        {
            return double.NaN;
        }
    }
    /// <summary>
    /// Retrieve Yard scraping efficieny
    /// </summary>
    /// <param name="sector">Sector enumerator (shoudl be only beef or dairy)</param>
    /// <param name="animalType">Animal type enumerator as integer</param>
    /// <param name="collectingYard">Boolean indicating if collecting yard (true) or feeding yard (false)</param>
    /// <returns>Scraping efficiency</returns>
    public double RetrieveYardScrapingEfficiency(Sector sector, int animalType, bool collectingYard = false)
    {
        if(sector.Equals(Sector.Dairy) || sector.Equals(Sector.Beef))
        {
            YardKey key = new YardKey(sector, animalType, collectingYard);
            return Instance._YardInfo[key].ScrapingEfficiency;
        }
        else
        {
            return double.NaN;
        }
    }
    /// <summary>
    /// Method to retrieve the methane conversion factor
    /// </summary>
    /// <param name="sector">Sector enumerator (shoudl be only beef or dairy)</param>
    /// <param name="animalType">Animal type enumerator as integer</param>
    /// <param name="collectingYard">optional boolean to distinguish yard types (collecting yard = true, feeding yard = false)</param>
    /// <returns>Methaen conversion factor</returns>
    public double RetrieveMCF(Sector sector, int animalType, bool collectingYard = false)
    {
        if(sector.Equals(Sector.Dairy) || sector.Equals(Sector.Beef))
        {
            YardKey key = new YardKey(sector, animalType, collectingYard);
            return Instance._YardInfo[key].MethaneConversionFactor;
        }
        else
        {
            return double.NaN;
        }
    }

    private class YardKey : IEquatable<YardKey>
    {
        public Sector Sector { get; set; } = Sector.NotSet;
        public int AnimalType { get; set; } = 0;
        public bool CollectingYard { get; set; } = false;

        public YardKey(Sector sector, int animalType, bool collectingYard)
        {
            Sector = sector;
            AnimalType = animalType;
            CollectingYard = collectingYard;
        }

        public YardKey() { }

        public bool Equals(YardKey? y)
        {
            return Sector == y.Sector && AnimalType == y.AnimalType && CollectingYard == y.CollectingYard;
        }

        public override int GetHashCode()
        {
            return (int)Sector ^ AnimalType ^ (CollectingYard ? 1 : 0);
        }

    }

    private class YardData
    {
        public double ScrapingEfficiency { get; set; } = double.NaN;
        public double EmissionFactorN2ON { get; set; } = double.NaN;
        public double EmissionFactorNH3N { get; set; } = double.NaN;
        public double RatioNON { get; set; } = double.NaN;
        public double RatioN2N { get; set; } = double.NaN;
        public double MethaneConversionFactor { get; set; } = double.NaN;

        public YardData(double scrapingEfficiency, double emissionFactorN2ON, double emissionFactorNH3N, double ratioNON, double ratioN2N, double methaneConversionFactor)
        {
            ScrapingEfficiency = scrapingEfficiency;
            EmissionFactorN2ON = emissionFactorN2ON;
            EmissionFactorNH3N = emissionFactorNH3N;
            RatioNON = ratioNON;
            RatioN2N = ratioN2N;
            MethaneConversionFactor = methaneConversionFactor;
        }

        public YardData() { }
    }
}
