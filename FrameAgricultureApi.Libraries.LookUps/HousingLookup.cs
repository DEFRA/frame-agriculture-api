using FrameAgricultureApi.CustomErrorHandling;
using FrameAgricultureApi.Generic;
using System.Reflection;
using static FrameAgricultureApi.Enumerators.Enumerators;
using static FrameAgricultureApi.Libraries.LookUps.StorageLookup;

namespace FrameAgricultureApi.Libraries.LookUps;

/// <summary>
/// Housing lookup class
/// </summary>
public class HousingLookup
{

    private static HousingLookup? _instance;

    /// <summary>
    /// Instance of singleton
    /// </summary>
    public static HousingLookup Instance
    {
        get
        {
            _instance ??= new HousingLookup();
            return _instance;
        }
    }

    //Define variables
    private readonly Dictionary<HousingKey, HousingData> _HousingInfo = [];

    private HousingLookup()
    {
        MemoryStream memoryStream = HelperFunctions.GetLUT(Assembly.GetExecutingAssembly(), "FLEA_ManureManagement_Housing_LUT.dat");
        try
        {
            using BinaryReader binaryReader = new(memoryStream);
            //Read in the Yard lookup information
            while(binaryReader.BaseStream.Position < binaryReader.BaseStream.Length)
            {
                int sectorID = binaryReader.ReadInt32();
                int animalType = binaryReader.ReadInt32();
                int housingType = binaryReader.ReadInt32();
                int manureType = binaryReader.ReadInt32();

                HousingKey theKey = new()
                {
                    Sector = (Sector)sectorID,
                    AnimalType = animalType,
                    HousingSystem = (ManureHousingSystem)housingType,
                    OrganicMatterType = (OrganicMatterType)manureType
                };

                HousingData theData = new()
                {
                    EmissionFactorN2ON = binaryReader.ReadDouble(),
                    EmissionFactorNH3N = binaryReader.ReadDouble(),
                    MethaneConversionFactor = binaryReader.ReadDouble(),
                    ImmobilisationFactor = binaryReader.ReadDouble(),
                    RatioNON = binaryReader.ReadDouble(),
                    RatioN2N = binaryReader.ReadDouble()
                };

                _HousingInfo.Add(theKey, theData);
            }
        }
        catch(Exception ex)
        {
            throw new CustomAppException("Error loading housing information: " + ex.Message);
        }
    }

    /// <summary>
    /// Retrieve immobilisation factor
    /// </summary>
    /// <param name="sector">Sector enumerator</param>
    /// <param name="animalType">Animal type enumerator as integer</param>
    /// <param name="manureHousingSystem">Housing system enumerator</param>
    /// <param name="manureType">Manure type enumerator (organic matter source type)</param>
    /// <returns>Immobilisation factor as percentage</returns>
    public static double PercentImmobilisation(Sector sector, int animalType, ManureHousingSystem manureHousingSystem, OrganicMatterType manureType)
    {
        HousingKey key = new(sector, animalType, manureHousingSystem, manureType);
        if(Instance._HousingInfo.TryGetValue(key, out var myValue))
        {
            return myValue.ImmobilisationFactor;
        }
        else
        {
            throw new CustomAppException("No emission factors avaialble for " + manureType.ToString() + " in " + manureHousingSystem.ToString() + " (" +
                sector.ToString() + " with animal type " + animalType.ToString() + ").");
        }
    }
    /// <summary>
    /// Retrieve emission ratio for NO-N
    /// </summary>
    /// <param name="sector">Sector enumerator</param>
    /// <param name="animalType">Animal type enumerator as integer</param>
    /// <param name="manureHousingSystem">Housing system enumerator</param>
    /// <param name="manureType">Manure type enumerator (organic matter source type)</param>
    /// <returns>NON Emission Factor as ratio of Nitrouse Oxides (as N) Emission</returns>
    public static double RetrieveNONRatio(Sector sector, int animalType, ManureHousingSystem manureHousingSystem, OrganicMatterType manureType)
    {
        HousingKey key = new(sector, animalType, manureHousingSystem, manureType);
        if(Instance._HousingInfo.TryGetValue(key, out var myValue))
        {
            return myValue.RatioNON;
        }
        else
        {
            throw new CustomAppException("No emission factors avaialble for " + manureType.ToString() + " in " + manureHousingSystem.ToString() + " (" +
                sector.ToString() + " with animal type " + animalType.ToString() + ").");
        }
    }
    /// <summary>
    /// Retrieve emission ratio for N2-N
    /// </summary>
    /// <param name="sector">Sector enumerator</param>
    /// <param name="animalType">Animal type enumerator as integer</param>
    /// <param name="manureHousingSystem">Housing system enumerator</param>
    /// <param name="manureType">Manure type enumerator (organic matter source type)</param>
    /// <returns>N2N Emission Factor as ratio of Nitrouse Oxides (as N) Emission</returns>
    public static double RetrieveN2NRatio(Sector sector, int animalType, ManureHousingSystem manureHousingSystem, OrganicMatterType manureType)
    {
        HousingKey key = new(sector, animalType, manureHousingSystem, manureType);
        if(Instance._HousingInfo.TryGetValue(key, out var myValue))
        {
            return myValue.RatioN2N;
        }
        else
        {
            throw new CustomAppException("No emission factors avaialble for " + manureType.ToString() + " in " + manureHousingSystem.ToString() + " (" +
                sector.ToString() + " with animal type " + animalType.ToString() + ").");
        }
    }
    /// <summary>
    /// Retrieve emission factor for NH3-N
    /// </summary>
    /// <param name="sector">Sector enumerator</param>
    /// <param name="animalType">Animal type enumerator as integer</param>
    /// <param name="manureHousingSystem">Housing system enumerator</param>
    /// <param name="manureType">Manure type enumerator (organic matter type)</param>
    /// <returns>NH3N Emission Factor</returns>
    public static double RetrieveNH3NEmissionFactor(Sector sector, int animalType, ManureHousingSystem manureHousingSystem, OrganicMatterType manureType)
    {
        HousingKey key = new(sector, animalType, manureHousingSystem, manureType);
        if(Instance._HousingInfo.TryGetValue(key, out var myValue))
        {
            return myValue.EmissionFactorNH3N;
        }
        else
        {
            throw new CustomAppException("No emission factors avaialble for " + manureType.ToString() + " in " + manureHousingSystem.ToString() + " (" +
                sector.ToString() + " with animal type " + animalType.ToString() + ").");
        }
    }
    /// <summary>
    /// Retrieve emission factor for N2O-N
    /// </summary>
    /// <param name="sector">Sector enumerator</param>
    /// <param name="animalType">Animal type enumerator as integer</param>
    /// <param name="manureHousingSystem">Housing system enumerator</param>
    /// <param name="manureType">Manure type enumerator (organic matter type)</param>
    /// <returns>N2ON Emission Factor</returns>
    public static double RetrieveN2ONEmissionFactor(Sector sector, int animalType, ManureHousingSystem manureHousingSystem, OrganicMatterType manureType)
    {
        HousingKey key = new(sector, animalType, manureHousingSystem, manureType);
        if(Instance._HousingInfo.TryGetValue(key, out var myValue))
        {
            return myValue.EmissionFactorN2ON;
        }
        else
        {
            throw new CustomAppException("No emission factors avaialble for " + manureType.ToString() + " in " + manureHousingSystem.ToString() + " (" +
                sector.ToString() + " with animal type " + animalType.ToString() + ").");
        }
    }
    /// <summary>
    /// Method to retrieve the methane conversion factor
    /// </summary>
    /// <param name="sector">Sector enumerator</param>
    /// <param name="animalType">Animal type enumerator as integer</param>
    /// <param name="manureHousingSystem">Housing system enumerator</param>
    /// <param name="manureType">Manure type enumerator (organic matter  type)</param>
    /// <returns>Methane conversion factor</returns>
    public static double RetrieveMCF(Sector sector, int animalType, ManureHousingSystem manureHousingSystem, OrganicMatterType manureType)
    {

        HousingKey key = new(sector, animalType, manureHousingSystem, manureType);
        if(Instance._HousingInfo.TryGetValue(key, out var myValue))
        {
            return myValue.MethaneConversionFactor;
        }
        else
        {
            throw new CustomAppException("No emission factors avaialble for " + manureType.ToString() + " in " + manureHousingSystem.ToString() + " (" +
                sector.ToString() + " with animal type " + animalType.ToString() + ").");
        }
    }

    private class HousingKey : IEquatable<HousingKey>
    {
        public Sector Sector { get; set; } = Sector.NotSet;
        public int AnimalType { get; set; } = 0;
        public ManureHousingSystem HousingSystem { get; set; } = ManureHousingSystem.NotSet;
        public OrganicMatterType OrganicMatterType { get; set; } = OrganicMatterType.NotSet;
        public HousingKey(Sector sector, int animalType, ManureHousingSystem housingSystem, OrganicMatterType organicMatterType)
        {
            Sector = sector;
            AnimalType = animalType;
            HousingSystem = housingSystem;
            OrganicMatterType = organicMatterType;
        }

        public HousingKey() { }

        public bool Equals(HousingKey? y)
        {
            if(y is null)
            {
                return false;
            }

            if(ReferenceEquals(this, y))
            {
                return true;
            }

            return Sector == y.Sector && AnimalType == y.AnimalType && HousingSystem == y.HousingSystem && OrganicMatterType == y.OrganicMatterType;
        }

        public override bool Equals(object? obj)
        {

            if(obj is null)
            {
                return false;
            }

            if(ReferenceEquals(this, obj))
            {
                return true;
            }

            if(obj.GetType() != typeof(StorageKey))
            {
                return false;
            }

            return Equals((StorageKey)obj);
        }

        public override int GetHashCode()
        {
            return (int)Sector ^ AnimalType ^ (int)HousingSystem ^ (int)OrganicMatterType;
        }
    }

    private class HousingData
    {
        public double EmissionFactorNH3N { get; set; } = double.NaN;
        public double EmissionFactorN2ON { get; set; } = double.NaN;
        public double MethaneConversionFactor { get; set; } = double.NaN;
        public double ImmobilisationFactor { get; set; } = double.NaN;
        public double RatioNON { get; set; } = double.NaN;
        public double RatioN2N { get; set; } = double.NaN;

        public HousingData(double emissionFactorNH3N, double emissionFactorN2ON, double methaneConversionFactor, double immobilisationFactor, double ratioNON, double ratioN2N)
        {
            EmissionFactorNH3N = emissionFactorNH3N;
            EmissionFactorN2ON = emissionFactorN2ON;
            MethaneConversionFactor = methaneConversionFactor;
            ImmobilisationFactor = immobilisationFactor;
            RatioNON = ratioNON;
            RatioN2N = ratioN2N;
        }

        public HousingData() { }
    }
}
