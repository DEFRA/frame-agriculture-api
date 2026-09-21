using FrameAgricultureApi.CustomErrorHandling;
using FrameAgricultureApi.Generic;
using System.Reflection;
using static FrameAgricultureApi.Enumerators.Enumerators;

namespace FrameAgricultureApi.Libraries.LookUps;

/// <summary>
/// Storage lookup class
/// </summary>
public class StorageLookup
{
    private static StorageLookup? _instance;

    /// <summary>
    /// Instance of singleton
    /// </summary>
    public static StorageLookup Instance
    {
        get
        {
            _instance ??= new StorageLookup();
            return _instance;
        }
    }

    //Define variables
    //private readonly Dictionary<Tuple<int,int,int,int>, StorageData> _StorageInfo = [];
    private readonly Dictionary<StorageKey, StorageData> _StorageInfo = [];

    private StorageLookup()
    {
        MemoryStream memoryStream = HelperFunctions.GetLUT(Assembly.GetExecutingAssembly(), "FLEA_ManureManagement_Storage_LUT.dat");
        try
        {
            using BinaryReader binaryReader = new(memoryStream);
            //Read in the Yard lookup information
            while(binaryReader.BaseStream.Position < binaryReader.BaseStream.Length)
            {
                int sectorID = binaryReader.ReadInt32();
                int animalType = binaryReader.ReadInt32();
                int storageType = binaryReader.ReadInt32();
                int manureType = binaryReader.ReadInt32();

                StorageKey theKey = new()
                {
                    Sector = (Sector)sectorID,
                    AnimalType = animalType,
                    ManureStorageSystem = (ManureStorageSystem)storageType,
                    OrganicMatterType = (OrganicMatterType)manureType
                };

                //Tuple<int, int, int, int> theKey = new Tuple<int, int, int, int>((int)sectorID, animalType, (int)storageType, (int)manureType);

                StorageData theData = new()
                {
                    EmissionFactorN2ON = binaryReader.ReadDouble(),
                    EmissionFactorNH3N = binaryReader.ReadDouble(),
                    MethaneConversionFactor = binaryReader.ReadDouble(),
                    MineralisationFactor = binaryReader.ReadDouble(),
                    RatioNON = binaryReader.ReadDouble(),
                    RatioN2N = binaryReader.ReadDouble(),
                    FracLeach = binaryReader.ReadDouble()
                };

                _StorageInfo.Add(theKey, theData);
            }
        }
        catch(Exception ex)
        {
            throw new Exception("Error loading housing information: " + ex.Message);
        }
    }

    /// <summary>
    /// Function to retrieve mineralisation percent
    /// </summary>
    /// <param name="sector">Sector enumerator</param>
    /// <param name="animalType">animal type enumerator as integer</param>
    /// <param name="manureStorageSystem">Manure Storage System enumerator</param>
    /// <param name="organicMatterType">manure type enumerator (Organic matter type)</param>
    /// <returns></returns>
    public static double RetrieveStorageMineralisationFactor(Sector sector, int animalType, ManureStorageSystem manureStorageSystem, OrganicMatterType organicMatterType)
    {
        StorageKey key = new(sector, animalType, manureStorageSystem, organicMatterType);
        if(Instance._StorageInfo.TryGetValue(key, out var myValue))
        {
            return myValue.MineralisationFactor;
        }
        else
        {
            throw new CustomAppException("No emission factors avaialble for " + organicMatterType.ToString() + " in " + manureStorageSystem.ToString() + " (" +
                sector.ToString() + " with animal type " + animalType.ToString() + ").");
        }
    }
    /// <summary>
    /// Retrieve emission ratio for NO-N
    /// </summary>
    /// <param name="sector">Sector enumerator</param>
    /// <param name="animalType">animal type enumerator as integer</param>
    /// <param name="manureStorageSystem">Manure Storage System enumerator</param>
    /// <param name="organicMatterType">manure type enumerator (Organic matter type)</param>
    /// <returns>NON Emission Factor as ratio of Nitrouse Oxides (as N) Emission</returns>
    public static double RetrieveStorageNONRatio(Sector sector, int animalType, ManureStorageSystem manureStorageSystem, OrganicMatterType organicMatterType)
    {
        StorageKey key = new(sector, animalType, manureStorageSystem, organicMatterType);
        if(Instance._StorageInfo.TryGetValue(key, out var myValue))
        {
            return myValue.RatioNON;
        }
        else
        {
            throw new CustomAppException("No emission factors avaialble for " + organicMatterType.ToString() + " in " + manureStorageSystem.ToString() + " (" +
                sector.ToString() + " with animal type " + animalType.ToString() + ").");
        }
    }
    /// <summary>
    /// Retrieve emission ratio for N2-N
    /// </summary>
    /// <param name="sector">Sector enumerator</param>
    /// <param name="animalType">animal type enumerator as integer</param>
    /// <param name="manureStorageSystem">Manure Storage System enumerator</param>
    /// <param name="organicMatterType">manure type enumerator (Organic matter type)</param>
    /// <returns>N2N Emission Factor as ratio of Nitrouse Oxides (as N) Emission</returns>
    public static double RetrieveStorageN2NRatio(Sector sector, int animalType, ManureStorageSystem manureStorageSystem, OrganicMatterType organicMatterType)
    {
        StorageKey key = new(sector, animalType, manureStorageSystem, organicMatterType);
        if(Instance._StorageInfo.TryGetValue(key, out var myValue))
        {
            return myValue.RatioN2N;
        }
        else
        {
            throw new CustomAppException("No emission factors avaialble for " + organicMatterType.ToString() + " in " + manureStorageSystem.ToString() + " (" +
                sector.ToString() + " with animal type " + animalType.ToString() + ").");
        }
    }
    /// <summary>
    /// Retrieve emission factor for N2O-N
    /// </summary>
    /// <param name="sector">Sector enumerator</param>
    /// <param name="animalType">animal type enumerator as integer</param>
    /// <param name="manureStorageSystem">Manure Storage System enumerator</param>
    /// <param name="organicMatterType">manure type enumerator (Organic matter type)</param>
    /// <returns>NON Emission Factor as ratio of Nitrouse Oxides (as N) Emission</returns>
    public static double RetrieveStorageN2ONEmissionFactor(Sector sector, int animalType, ManureStorageSystem manureStorageSystem, OrganicMatterType organicMatterType)
    {
        StorageKey key = new(sector, animalType, manureStorageSystem, organicMatterType);
        if(Instance._StorageInfo.TryGetValue(key, out var myValue))
        {
            return myValue.EmissionFactorN2ON;
        }
        else
        {
            throw new CustomAppException("No emission factors avaialble for " + organicMatterType.ToString() + " in " + manureStorageSystem.ToString() + " (" +
                sector.ToString() + " with animal type " + animalType.ToString() + ").");
        }
    }

    /// <summary>
    /// Method to retrieve frac leach coefficient
    /// </summary>
    /// <param name="sector">Sector enumerator</param>
    /// <param name="animalType">animal type enumerator as integer</param>
    /// <param name="manureStorageSystem">Manure Storage System enumerator</param>
    /// <param name="organicMatterType">manure type enumerator (Organic matter type)</param>
    /// <returns>Frac Leach coefficient</returns>
    public static double RetrieveFracLeach(Sector sector, int animalType, ManureStorageSystem manureStorageSystem, OrganicMatterType organicMatterType)
    {
        StorageKey key = new(sector, animalType, manureStorageSystem, organicMatterType);
        if(Instance._StorageInfo.TryGetValue(key, out var myValue))
        {
            return myValue.FracLeach;
        }
        else
        {
            throw new CustomAppException("No emission factors avaialble for " + organicMatterType.ToString() + " in " + manureStorageSystem.ToString() + " (" +
                sector.ToString() + " with animal type " + animalType.ToString() + ").");
        }
    }

    /// <summary>
    /// Retrieve NH3-N emission factor
    /// </summary>
    /// <param name="sector">Sector enumerator</param>
    /// <param name="animalType">animal type enumerator as integer</param>
    /// <param name="manureStorageSystem">Manure Storage System enumerator</param>
    /// <param name="organicMatterType">manure type enumerator (Organic matter type)</param>
    /// <returns>Nh3N emission factor</returns>
    public static double RetrieveStorageNH3NEmissionFactor(Sector sector, int animalType, ManureStorageSystem manureStorageSystem, OrganicMatterType organicMatterType)
    {
        StorageKey key = new(sector, animalType, manureStorageSystem, organicMatterType);
        if(Instance._StorageInfo.TryGetValue(key, out var myValue))
        {
            return myValue.EmissionFactorNH3N;
        }
        else
        {
            throw new CustomAppException("No emission factors avaialble for " + organicMatterType.ToString() + " in " + manureStorageSystem.ToString() + " (" +
                sector.ToString() + " with animal type " + animalType.ToString() + ").");
        }
    }

    /// <summary>
    /// Retrieve methane conversion factor
    /// </summary>
    /// <param name="sector">Sector enumerator</param>
    /// <param name="animalType">animal type enumerator as integer</param>
    /// <param name="manureStorageSystem">Manure Storage System enumerator</param>
    /// <param name="organicMatterType">manure type enumerator (Organic matter type)</param>
    /// <returns>Methane conversion factor</returns>
    public static double RetrieveStorageMethaneConversionFactor(Sector sector, int animalType, ManureStorageSystem manureStorageSystem, OrganicMatterType organicMatterType)
    {
        StorageKey key = new(sector, animalType, manureStorageSystem, organicMatterType);
        if(Instance._StorageInfo.TryGetValue(key, out var myValue))
        {
            return myValue.MethaneConversionFactor;
        }
        else
        {
            throw new CustomAppException("No emission factors avaialble for " + organicMatterType.ToString() + " in " + manureStorageSystem.ToString() + " (" +
                sector.ToString() + " with animal type " + animalType.ToString() + ").");
        }
    }
    /// <summary>
    /// Storage Key class
    /// </summary>
    public class StorageKey : IEquatable<StorageKey>
    {
        /// <summary>
        /// Sector enumerator
        /// </summary>
        public Sector Sector { get; set; } = Sector.NotSet;
        /// <summary>
        /// Animal type enumerator as integer
        /// </summary>
        public int AnimalType { get; set; } = 0;
        /// <summary>
        /// Manure storage system enumerator
        /// </summary>
        public ManureStorageSystem ManureStorageSystem { get; set; } = ManureStorageSystem.NotSet;
        /// <summary>
        /// Organic matter type enumerator
        /// </summary>
        public OrganicMatterType OrganicMatterType { get; set; } = OrganicMatterType.NotSet;
        /// <summary>
        /// Storage Key constructor
        /// </summary>
        /// <param name="sector">Sector enumerator</param>
        /// <param name="animalType">Animal Tyope enumerator as integer</param>
        /// <param name="manureStorageSystem">Manure Storage System enumerator</param>
        /// <param name="manureType">Organic matter type enumerator</param>

        public StorageKey(Sector sector, int animalType, ManureStorageSystem manureStorageSystem, OrganicMatterType manureType)
        {
            Sector = sector;
            AnimalType = animalType;
            ManureStorageSystem = manureStorageSystem;
            OrganicMatterType = manureType;
        }
        /// <summary>
        /// EMpty constructor
        /// </summary>
        public StorageKey() { }

        /// <summary>
        /// Comparer for Storage key
        /// </summary>
        /// <param name="y">Storage Key object</param>
        /// <returns>true if the same as this StorageKey, fasle otherwise</returns>
        public bool Equals(StorageKey? y)
        {
            if(y is null)
                return false;
            if(ReferenceEquals(this, y))
                return true;
            return Sector == y.Sector && AnimalType == y.AnimalType && ManureStorageSystem == y.ManureStorageSystem && OrganicMatterType == y.OrganicMatterType;
        }
        /// <summary>
        /// Override of default equality comparer
        /// </summary>
        /// <param name="obj">Object object</param>
        /// <returns>True if object is the same as this StorageKey, false otherwise</returns>
        public override bool Equals(object? obj)
        {

            if(obj is null)
                return false;
            if(ReferenceEquals(this, obj))
                return true;
            if(obj.GetType() != typeof(StorageKey))
                return false;
            return Equals((StorageKey)obj);
        }
        /// <summary>
        /// Get hash code override
        /// </summary>
        /// <returns>hash code as integer</returns>
        public override int GetHashCode()
        {
            return (int)Sector ^ AnimalType ^ (int)ManureStorageSystem ^ (int)OrganicMatterType;
        }

    }

    /// <summary>
    /// Storage Data class
    /// </summary>
    private class StorageData
    {
        public double EmissionFactorNH3N { get; set; } = double.NaN;
        public double EmissionFactorN2ON { get; set; } = double.NaN;
        public double MethaneConversionFactor { get; set; } = double.NaN;
        public double MineralisationFactor { get; set; } = double.NaN;
        public double RatioNON { get; set; } = double.NaN;
        public double RatioN2N { get; set; } = double.NaN;
        public double FracLeach { get; set; } = double.NaN;

        public StorageData(double emissionFactorNH3N, double emissionFactorN2ON, double methaneConversionFactor, double mineralisationFactor, double ratioNON, double ratioN2N, double fracLeach)
        {
            EmissionFactorNH3N = emissionFactorNH3N;
            EmissionFactorN2ON = emissionFactorN2ON;
            MethaneConversionFactor = methaneConversionFactor;
            MineralisationFactor = mineralisationFactor;
            RatioNON = ratioNON;
            RatioN2N = ratioN2N;
            FracLeach = fracLeach;
        }

        public StorageData()
        {
        }
    }
}
