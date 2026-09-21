using FrameAgricultureApi.CustomErrorHandling;
using FrameAgricultureApi.Generic;
using System.Reflection;
using static FrameAgricultureApi.Enumerators.Enumerators;

namespace FrameAgricultureApi.Libraries.LookUps
{
    /// <summary>
    /// Lookup class for emission factors for excreate deposited outdoors
    /// </summary>
    public class OutdoorExcretionLookup
    {
        private static OutdoorExcretionLookup? _instance;

        /// <summary>
        /// Instance of singleton
        /// </summary>
        public static OutdoorExcretionLookup Instance
        {
            get
            {
                _instance ??= new OutdoorExcretionLookup();
                return _instance;
            }
        }

        //Dictionaries to store lookup values
        private readonly Dictionary<int, OutdoorExcretionData> _dairyData = []; //Urine N2ON, Dung N2ON, urine NH3N, dung NH3N, NON, N2N, fracleach, fraclech uncertainty, MCF
        private readonly Dictionary<int, OutdoorExcretionData> _beefData = []; //Urine N2ON, Dung N2ON, urine NH3N, dung NH3N, NON, N2N, fracleach, fraclech uncertainty, MCF
        private readonly Dictionary<int, OutdoorExcretionData> _pigData = []; //N2ON, NH3N, NON, N2N, fracleach, MCF
        private readonly Dictionary<int, OutdoorExcretionData> _poultryData = []; //N2ON, NH3N, NON, N2N, fracleach, MCF, Nitrogen Coefficient For Free Range Poultry
        private readonly Dictionary<int, OutdoorExcretionData> _minorLivestockData = []; //N2ON, NH3N, NON, N2N, fracleach, MCF
        private readonly Dictionary<int, OutdoorExcretionData> _sheepData = []; //Urine N2ON, Dung N2ON,  NH3N, NON, N2N, fracleach, MCF

        /// <summary>
        /// Outdoor Excretion Lookup initialisation
        /// </summary>
        /// <exception cref="Exception">Exception if loading of data fails</exception>
        public OutdoorExcretionLookup()
        {
            _dairyData.Clear();
            _beefData.Clear();
            _pigData.Clear();
            _poultryData.Clear();
            _minorLivestockData.Clear();
            _sheepData.Clear();

            //Data
            //Non-manure based organic mattermemoryStream.Close();

            MemoryStream memoryStream = HelperFunctions.GetLUT(Assembly.GetExecutingAssembly(), "FLEA_OutdoorExcretion_LUT.dat");
            try
            {
                using BinaryReader binaryReader = new(memoryStream);
                //Read in the manure lookup information
                while(binaryReader.BaseStream.Position < binaryReader.BaseStream.Length)
                {
                    int sector = binaryReader.ReadInt32();
                    int animal = binaryReader.ReadInt32();
                    OutdoorExcretionData thedata = new()
                    {
                        UrineN2ONEF = binaryReader.ReadDouble(),
                        DungN2ONEF = binaryReader.ReadDouble(),
                        ExcretaN2ONEF = binaryReader.ReadDouble(),
                        UrineNH3NEF = binaryReader.ReadDouble(),
                        DungNH3NEF = binaryReader.ReadDouble(),
                        ExcretaNH3NEF = binaryReader.ReadDouble(),
                        RatioNON = binaryReader.ReadDouble(),
                        RatioN2N = binaryReader.ReadDouble(),
                        FracLeach = binaryReader.ReadDouble(),
                        FracLeachUncertainty = binaryReader.ReadDouble(),
                        MethaneConversionFactor = binaryReader.ReadDouble(),
                        NitrogenCoefficientFreeRangePoultry = binaryReader.ReadDouble()
                    };

                    switch(sector)
                    {
                        case (int)Sector.Beef:
                            _beefData.Add(animal, thedata);
                            break;

                        case (int)Sector.Dairy:
                            _dairyData.Add(animal, thedata);
                            break;

                        case (int)Sector.Pigs:
                            _pigData.Add(animal, thedata);
                            break;

                        case (int)Sector.Poultry:
                            _poultryData.Add(animal, thedata);
                            break;

                        case (int)Sector.MinorLivestock:
                            _minorLivestockData.Add(animal, thedata);
                            break;

                        case (int)Sector.Sheep:
                            _sheepData.Add(animal, thedata);
                            break;

                    }
                }
            }
            catch(Exception excep)
            {
                throw new CustomAppException("Error loading Outdoor Excretion data to Singleton class: " + excep.Message);
            }
        }

        /// <summary>
        /// Method to retrieve the ammonia emission factor for urine
        /// </summary>
        /// <param name="sector">Sector enumerator - should be dairy, beef or sheep</param>
        /// <param name="urine">Boolean to indicate if EF for urine requires (true) if false, then Ef for dung or the generic EF</param>
        /// <param name="animal">Animal type enumerator as integer</param>
        /// <returns>Ammonia emission factor as percentage</returns>
        public static double RetrieveNH3NEF(Sector sector, bool urine, int animal = 1)
        {
            double retval = double.NaN;
            switch(sector)
            {
                case Sector.Beef:
                    if(urine)
                    { retval = Instance._beefData[animal].UrineNH3NEF; }
                    else
                    { retval = Instance._beefData[animal].DungNH3NEF; }
                    break;

                case Sector.Dairy:
                    if(urine)
                    { retval = Instance._dairyData[animal].UrineNH3NEF; }
                    else
                    { retval = Instance._beefData[animal].DungNH3NEF; }
                    break;

                case Sector.Pigs:
                    retval = Instance._pigData[animal].ExcretaNH3NEF;
                    break;

                case Sector.Poultry:
                    retval = Instance._poultryData[animal].ExcretaNH3NEF;
                    break;

                case Sector.MinorLivestock:
                    retval = Instance._minorLivestockData[animal].ExcretaNH3NEF;
                    break;

                case Sector.Sheep:
                    retval = Instance._sheepData[animal].ExcretaNH3NEF;
                    break;
            }

            return retval;
        }

        /// <summary>
        /// Method to retrieve the nitrous oxide emission factor for urine
        /// </summary>
        /// <param name="sector">Sector enumerator - should be dairy, beef or sheep</param>
        /// <param name="urine">Boolean to indicate if EF for urine requires (true) if false, then Ef for dung or the generic EF</param>
        /// <param name="animal">Animal type enumerator as integer</param>
        /// <returns>Nitrous oxide emission factor as percentage</returns>
        public static double RetrieveN2ONEF(Sector sector, bool urine, int animal = 1)
        {
            double retval = double.NaN;
            switch(sector)
            {
                case Sector.Beef:
                    if(urine)
                    { retval = Instance._beefData[animal].UrineN2ONEF; }
                    else
                    { retval = Instance._beefData[animal].DungN2ONEF; }
                    break;

                case Sector.Dairy:
                    if(urine)
                    { retval = Instance._dairyData[animal].UrineN2ONEF; }
                    else
                    { retval = Instance._beefData[animal].DungN2ONEF; }
                    break;

                case Sector.Pigs:
                    retval = Instance._pigData[animal].ExcretaN2ONEF;
                    break;

                case Sector.Poultry:
                    retval = Instance._poultryData[animal].ExcretaN2ONEF;
                    break;

                case Sector.MinorLivestock:
                    retval = Instance._minorLivestockData[animal].ExcretaN2ONEF;
                    break;

                case Sector.Sheep:
                    if(urine)
                    { retval = Instance._sheepData[animal].UrineN2ONEF; }
                    else
                    { retval = Instance._sheepData[animal].DungN2ONEF; }
                    break;
            }

            return retval;
        }
        /// <summary>
        /// Method to retrieve the fracleach uncertainty used for the variabel fracleach in the sheep sector
        /// </summary>
        /// <returns>frac leach uncertainty</returns>
        public static double RetrieveFracLeachUncertainty(Sector sector, int animal = 1)
        {
            double retval = double.NaN;
            switch(sector)
            {
                case Sector.Beef:
                    retval = Instance._beefData[animal].FracLeachUncertainty;
                    break;

                case Sector.Dairy:
                    retval = Instance._beefData[animal].FracLeachUncertainty;
                    break;

                case Sector.Pigs:
                    retval = Instance._pigData[animal].FracLeachUncertainty;
                    break;

                case Sector.Poultry:
                    retval = Instance._poultryData[animal].FracLeachUncertainty;
                    break;

                case Sector.MinorLivestock:
                    retval = Instance._minorLivestockData[animal].FracLeachUncertainty;
                    break;

                case Sector.Sheep:
                    retval = Instance._sheepData[animal].FracLeachUncertainty;
                    break;
            }

            return retval;
        }
        /// <summary>
        /// Method to retrieve the fracleach for outdoor excreta (pigs, poultry and minor livestock)
        /// </summary>
        /// <param name="sector">Sector enumerator - should be pig, poultry or minor livestock</param>
        /// <param name="animal">Animal type enumerator as integer</param>
        /// <returns>frac leach as a percentage</returns>
        public static double RetrieveOutdoorFracLeach(Sector sector, int animal = 1)
        {
            double retval = double.NaN;
            switch(sector)
            {
                case Sector.Beef:
                    retval = Instance._beefData[animal].FracLeach;
                    break;

                case Sector.Dairy:
                    retval = Instance._beefData[animal].FracLeach;
                    break;

                case Sector.Pigs:
                    retval = Instance._pigData[animal].FracLeach;
                    break;

                case Sector.Poultry:
                    retval = Instance._poultryData[animal].FracLeach;
                    break;

                case Sector.MinorLivestock:
                    retval = Instance._minorLivestockData[animal].FracLeach;
                    break;

                    //No sheep sector as this shoudl use the variable frac leach method
            }

            return retval;
        }
        /// <summary>
        /// Method to retrieve the NON to N2ON ratio
        /// </summary>
        /// <param name="sector">Sector enumerator</param>
        /// <param name="animal">Animal type enumerator as integer</param>
        /// <returns>Ratio of NON to N2ON</returns>
        public static double RetrieveNitricOxideRatio(Sector sector, int animal = 1)
        {
            double retval = double.NaN;
            switch(sector)
            {
                case Sector.Beef:
                    retval = Instance._beefData[animal].RatioNON;
                    break;

                case Sector.Dairy:
                    retval = Instance._beefData[animal].RatioNON;
                    break;

                case Sector.Pigs:
                    retval = Instance._pigData[animal].RatioNON;
                    break;

                case Sector.Poultry:
                    retval = Instance._poultryData[animal].RatioNON;
                    break;

                case Sector.MinorLivestock:
                    retval = Instance._minorLivestockData[animal].RatioNON;
                    break;

                case Sector.Sheep:
                    retval = Instance._sheepData[animal].RatioNON;
                    break;
            }

            return retval;
        }
        /// <summary>
        /// Method to retrieve the N2N to N2ON ratio
        /// </summary>
        /// <param name="sector">Sector enumerator</param>
        /// <param name="animal">Animal type enumerator as integer</param>
        /// <returns>Ratio of N2N to N2ON</returns>
        public static double RetrieveDinitrogenRatio(Sector sector, int animal = 1)
        {
            double retval = double.NaN;
            switch(sector)
            {
                case Sector.Beef:
                    retval = Instance._beefData[animal].RatioN2N;
                    break;

                case Sector.Dairy:
                    retval = Instance._beefData[animal].RatioN2N;
                    break;

                case Sector.Pigs:
                    retval = Instance._pigData[animal].RatioN2N;
                    break;

                case Sector.Poultry:
                    retval = Instance._poultryData[animal].RatioN2N;
                    break;

                case Sector.MinorLivestock:
                    retval = Instance._minorLivestockData[animal].RatioN2N;
                    break;

                case Sector.Sheep:
                    retval = Instance._sheepData[animal].RatioN2N;
                    break;
            }

            return retval;
        }
        /// <summary>
        /// Method to retrieve the methane conversion factor for excreta deposited outdoors
        /// </summary>
        /// <param name="sector">Sector enumerator</param>
        /// <param name="animal">Animal type enumerator as integer</param>
        /// <returns>Methane conversion factor for outdoor excreta</returns>
        public static double RetrieveMethaneConversionFactor(Sector sector, int animal)
        {
            double retval = double.NaN;
            switch(sector)
            {
                case Sector.Beef:
                    retval = Instance._beefData[animal].MethaneConversionFactor;
                    break;

                case Sector.Dairy:
                    retval = Instance._beefData[animal].MethaneConversionFactor;
                    break;

                case Sector.Pigs:
                    retval = Instance._pigData[animal].MethaneConversionFactor;
                    break;

                case Sector.Poultry:
                    retval = Instance._poultryData[animal].MethaneConversionFactor;
                    break;

                case Sector.MinorLivestock:
                    retval = Instance._minorLivestockData[animal].MethaneConversionFactor;
                    break;

                case Sector.Sheep:
                    retval = Instance._sheepData[animal].MethaneConversionFactor;
                    break;
            }

            return retval;
        }
        /// <summary>
        /// Method to retrieve the methane conversion factor for excreta deposited outdoors
        /// </summary>
        /// <param name="sector">Sector enumerator</param>
        /// <param name="animal">Animal type enumerator as integer</param>
        /// <param name="isIndoors">true if animal is indoors</param>
        /// <returns>Methane conversion factor for outdoor excreta</returns>
        public static double RetrieveNitrogenCoefficientFreeRangePoultry(Sector sector, int animal, bool isIndoors)
        {
            double retval = double.NaN;

            if(sector == Sector.Poultry)
            {
                if(!isIndoors)
                    retval = Instance._poultryData[animal].NitrogenCoefficientFreeRangePoultry;
                else
                    retval = 1.0;
            }

            return retval;
        }
    }
}
/// <summary>
/// Outdoor Excretion data class
/// </summary>
internal class OutdoorExcretionData
{
    public double UrineN2ONEF { get; set; } = double.NaN;
    public double DungN2ONEF { get; set; } = double.NaN;
    public double ExcretaN2ONEF { get; set; } = double.NaN;
    public double UrineNH3NEF { get; set; } = double.NaN;
    public double DungNH3NEF { get; set; } = double.NaN;
    public double ExcretaNH3NEF { get; set; } = double.NaN;
    public double RatioNON { get; set; } = double.NaN;
    public double RatioN2N { get; set; } = double.NaN;
    public double FracLeach { get; set; } = double.NaN;
    public double FracLeachUncertainty { get; set; } = double.NaN;
    public double MethaneConversionFactor { get; set; } = double.NaN;
    public double NitrogenCoefficientFreeRangePoultry { get; set; } = double.NaN;

}

