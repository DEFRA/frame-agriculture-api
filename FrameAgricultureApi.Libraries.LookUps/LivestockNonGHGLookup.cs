using FrameAgricultureApi.CustomErrorHandling;
using FrameAgricultureApi.Generic;
using System.Reflection;
using static FrameAgricultureApi.Enumerators.Enumerators;

namespace FrameAgricultureApi.Libraries.LookUps
{
    /// <summary>
    /// Lookup for livestock non GHG emissions
    /// </summary>
    public class LivestockNonGHGLookup
    {
        private static LivestockNonGHGLookup? _instance;

        /// <summary>
        /// Instance of lookup singleton
        /// </summary>
        public static LivestockNonGHGLookup Instance
        {
            get
            {
                if(_instance == null)
                {
                    _instance = new LivestockNonGHGLookup();
                }
                return _instance;
            }
        }

        private static Dictionary<Tuple<int, int>, LivestockNonGHGFactors> LivestockNonGHGInformation = new Dictionary<Tuple<int, int>, LivestockNonGHGFactors>();

        private LivestockNonGHGLookup()
        {
            LivestockNonGHGInformation = new Dictionary<Tuple<int, int>, LivestockNonGHGFactors>();
            LivestockNonGHGInformation.Clear();

            MemoryStream memoryStream = HelperFunctions.GetLUT(Assembly.GetExecutingAssembly(), "FLEA_Livestock_NonGHG_LUT.dat");
            try
            {
                using BinaryReader binaryReader = new(memoryStream);
                while(binaryReader.BaseStream.Position < binaryReader.BaseStream.Length)
                {
                    int item1 = binaryReader.ReadInt32();
                    int item2 = binaryReader.ReadInt32();
                    Tuple<int, int> thekey = new Tuple<int, int>(item1, item2);

                    LivestockNonGHGFactors thefactors = new LivestockNonGHGFactors();
                    thefactors.ef_NMVOC_Silage = binaryReader.ReadDouble();
                    thefactors.ef_NMVOC_Housing = binaryReader.ReadDouble();
                    thefactors.ef_NMVOC_Grazing = binaryReader.ReadDouble();
                    thefactors.fractionSilage = binaryReader.ReadDouble();
                    thefactors.fractionSilageStore = binaryReader.ReadDouble();
                    thefactors.ef_PM25 = binaryReader.ReadDouble();
                    thefactors.ef_PM10 = binaryReader.ReadDouble();
                    thefactors.ef_TSP = binaryReader.ReadDouble();

                    LivestockNonGHGInformation.Add(thekey, thefactors);

                }
            }
            catch(Exception ex)
            {
                throw new CustomAppException("Error loading livestock non-ghg lookup information: " + ex.Message);
            }
        }
        /// <summary>
        /// Retrieve the emission factor for NMVOC form silage
        /// </summary>
        /// <param name="sector">Sector enumerator</param>
        /// <param name="animalType">Animal type enumerator (as integer)</param>
        /// <returns>emission factor for NMVOC silage</returns>
        /// <exception cref="Exception">Error if no entry found in dictionary</exception>
        public double RetrieveNMVOCSilage(int sector, int animalType)
        {
            Sector mysector = (Sector)sector;

            Tuple<int, int> key = new Tuple<int, int>(sector, animalType);
            if(LivestockNonGHGInformation.ContainsKey(key))
            {
                return LivestockNonGHGInformation[key].ef_NMVOC_Silage;
            }
            else
            {
                throw new CustomAppException("No corresponsding emission factor (NMVOC silage) was found for animal type " + animalType.ToString() + " in the " + mysector.ToString() + " sector.");
            }
        }
        /// <summary>
        /// Retrieve the emission factor for NMVOC at housing
        /// </summary>
        /// <param name="sector">Sector enumerator</param>
        /// <param name="animalType">Animal type enumerator (as integer)</param>
        /// <returns>NMVOC emission factor for housing</returns>
        /// <exception cref="Exception">Error if no entry found in dictionary</exception>
        public double RetrieveNMVOCHousing(int sector, int animalType)
        {
            Sector mysector = (Sector)sector;

            Tuple<int, int> key = new Tuple<int, int>(sector, animalType);
            if(LivestockNonGHGInformation.ContainsKey(key))
            {
                return LivestockNonGHGInformation[key].ef_NMVOC_Housing;
            }
            else
            {
                throw new CustomAppException("No corresponsding emission factor (NMVOC housing) was found for animal type " + animalType.ToString() + " in the " + mysector.ToString() + " sector.");
            }
        }
        /// <summary>
        /// Retrieve NMVOC emission factor for  grazing
        /// </summary>
        /// <param name="sector">Sector enumerator</param>
        /// <param name="animalType">Animal type enumerator (as integer)</param>
        /// <returns>NMVOC emission factor for grazing</returns>
        /// <exception cref="Exception">Error if no entry found in dictionary</exception>
        public double RetrieveNMVOCGrazing(int sector, int animalType)
        {
            Sector mysector = (Sector)sector;

            Tuple<int, int> key = new Tuple<int, int>(sector, animalType);
            if(LivestockNonGHGInformation.ContainsKey(key))
            {
                return LivestockNonGHGInformation[key].ef_NMVOC_Grazing;
            }
            else
            {
                throw new CustomAppException("No corresponsding emission factor (NMVOC grazing) was found for animal type " + animalType.ToString() + " in the " + mysector.ToString() + " sector.");
            }
        }
        /// <summary>
        /// Retrieve silage fraction emission multiplier
        /// </summary>
        /// <param name="sector">Sector enumerator</param>
        /// <param name="animalType">Animal type enumerator (as integer)</param>
        /// <returns>fraction of silage emissions</returns>
        /// <exception cref="Exception">Error if no entry found in dictionary</exception>
        public double RetrieveFractionSilage(int sector, int animalType)
        {
            Sector mysector = (Sector)sector;

            Tuple<int, int> key = new Tuple<int, int>(sector, animalType);
            if(LivestockNonGHGInformation.ContainsKey(key))
            {
                return LivestockNonGHGInformation[key].fractionSilage;
            }
            else
            {
                throw new CustomAppException("No corresponsding silage fraction was found for animal type " + animalType.ToString() + " in the " + mysector.ToString() + " sector.");
            }
        }
        /// <summary>
        /// Retrieve silage store fraction emission multiplier
        /// </summary>
        /// <param name="sector">Sector enumerator</param>
        /// <param name="animalType">Animal type enumerator (as integer)</param>
        /// <returns>fraction of silage store emissions</returns>
        /// <exception cref="Exception">Error if no entry found in dictionary</exception>
        public double RetrieveFractionSilageStore(int sector, int animalType)
        {
            Sector mysector = (Sector)sector;

            Tuple<int, int> key = new Tuple<int, int>(sector, animalType);
            if(LivestockNonGHGInformation.ContainsKey(key))
            {
                return LivestockNonGHGInformation[key].fractionSilageStore;
            }
            else
            {
                throw new CustomAppException("No corresponsding silage store fraction was found for animal type " + animalType.ToString() + " in the " + mysector.ToString() + " sector.");
            }
        }
        /// <summary>
        /// Retrieve PM2.5 emission factor
        /// </summary>
        /// <param name="sector">Sector enumerator</param>
        /// <param name="animalType">Animal type enumerator (as integer)</param>
        /// <returns>PM2.5 emission factor</returns>
        /// <exception cref="Exception">Error if no entry found in dictionary</exception>
        public double RetrievePM25(int sector, int animalType)
        {
            Sector mysector = (Sector)sector;

            Tuple<int, int> key = new Tuple<int, int>(sector, animalType);
            if(LivestockNonGHGInformation.ContainsKey(key))
            {
                return LivestockNonGHGInformation[key].ef_PM25;
            }
            else
            {
                throw new CustomAppException("No corresponsding PM2.5 emission factor was found for animal type " + animalType.ToString() + " in the " + mysector.ToString() + " sector.");
            }
        }
        /// <summary>
        /// Retrieve PM10 emission factor
        /// </summary>
        /// <param name="sector">Sector enumerator</param>
        /// <param name="animalType">Animal type enumerator (as integer)</param>
        /// <returns>PM10 emission factor</returns>
        /// <exception cref="Exception">Error if no entry found in dictionary</exception>
        public double RetrievePM10(int sector, int animalType)
        {
            Sector mysector = (Sector)sector;

            Tuple<int, int> key = new Tuple<int, int>(sector, animalType);
            if(LivestockNonGHGInformation.ContainsKey(key))
            {
                return LivestockNonGHGInformation[key].ef_PM10;
            }
            else
            {
                throw new CustomAppException("No corresponsding PM10 emission factor was found for animal type " + animalType.ToString() + " in the " + mysector.ToString() + " sector.");
            }
        }
        /// <summary>
        /// Retrieve TSP emission factor
        /// </summary>
        /// <param name="sector">Sector enumerator</param>
        /// <param name="animalType">Animal type enumerator (as integer)</param>
        /// <returns>TSP emission factor</returns>
        /// <exception cref="Exception">Error if no entry found in dictionary</exception>
        public double RetrieveTSP(int sector, int animalType)
        {
            Sector mysector = (Sector)sector;

            Tuple<int, int> key = new Tuple<int, int>(sector, animalType);
            if(LivestockNonGHGInformation.ContainsKey(key))
            {
                return LivestockNonGHGInformation[key].ef_TSP;
            }
            else
            {
                throw new CustomAppException("No corresponsding TSP emission factor was found for animal type " + animalType.ToString() + " in the " + mysector.ToString() + " sector.");
            }
        }
    }
}

/// <summary>
/// Liveston non GHG emission factors and multiplier class
/// </summary>
public class LivestockNonGHGFactors
{
    /// <summary>
    /// NMVOC silage
    /// </summary>
    public double ef_NMVOC_Silage { get; set; }
    /// <summary>
    /// NMVOC housing
    /// </summary>
    public double ef_NMVOC_Housing { get; set; }
    /// <summary>
    /// NMVOC grazing
    /// </summary>
    public double ef_NMVOC_Grazing { get; set; }
    /// <summary>
    /// Fraction silage
    /// </summary>
    public double fractionSilage { get; set; }
    /// <summary>
    /// fraction silage store
    /// </summary>
    public double fractionSilageStore { get; set; }
    /// <summary>
    /// PM2.5
    /// </summary>
    public double ef_PM25 { get; set; }
    /// <summary>
    /// PM10
    /// </summary>
    public double ef_PM10 { get; set; }
    /// <summary>
    /// TSP
    /// </summary>
    public double ef_TSP { get; set; }
}
