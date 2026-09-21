using FrameAgricultureApi.CustomErrorHandling;
using FrameAgricultureApi.Generic;
using System.Reflection;
using static FrameAgricultureApi.Enumerators.Enumerators;

namespace FrameAgricultureApi.Libraries.LookUps;

/// <summary>
/// Manure mass anbd volume lookup singleton
/// </summary>
public class ManureMassVolumeLookup
{
    private static ManureMassVolumeLookup? _instance;

    /// <summary>
    /// Instance of singleton
    /// </summary>
    public static ManureMassVolumeLookup Instance
    {
        get
        {
            _instance ??= new ManureMassVolumeLookup();
            return _instance;
        }
    }

    //Define variables
    private Dictionary<int, MMV_DMI_information> OutdoorPigs_DMI = [];
    private Dictionary<int, MMV_DMI_information> IndoorPigs_DMI = [];
    private Dictionary<int, MMV_DMI_information> Poultry_DMI = [];
    private Dictionary<int, MMV_DMI_information> MinorLivestock_DMI = [];

    private Dictionary<int, MMV_Chain_Information> Pigs_Chain = [];
    private Dictionary<int, MMV_Chain_Information> Poultry_Chain = [];
    private Dictionary<int, MMV_Chain_Information> MinorLivestock_Chain = [];

    private ManureMassVolumeLookup()
    {
        OutdoorPigs_DMI = [];
        IndoorPigs_DMI = [];
        Poultry_DMI = [];
        MinorLivestock_DMI = [];

        MemoryStream memoryStream = HelperFunctions.GetLUT(Assembly.GetExecutingAssembly(), "FLEA_MMV_DMI_LUT.dat");
        try
        {
            using BinaryReader binaryReader = new(memoryStream);
            //Read in the Yard lookup information
            while(binaryReader.BaseStream.Position < binaryReader.BaseStream.Length)
            {
                Sector sector = (Sector)binaryReader.ReadInt32();
                int location = binaryReader.ReadInt32();
                int animalType = binaryReader.ReadInt32();

                MMV_DMI_information info = new()
                {
                    DryMatterIntake = binaryReader.ReadDouble(),
                    MassCoefficient = binaryReader.ReadDouble(),
                    VolumeCoefficient = binaryReader.ReadDouble()
                };

                if(sector.Equals(Sector.Pigs))
                {
                    if(location == 1)
                    {
                        OutdoorPigs_DMI.Add(animalType, info);
                    }
                    else
                    {
                        IndoorPigs_DMI.Add(animalType, info);
                    }
                }
                else if(sector.Equals(Sector.Poultry))
                {
                    Poultry_DMI.Add(animalType, info);
                }
                else
                {
                    MinorLivestock_DMI.Add(animalType, info);
                }
                ;

            }
        }
        catch(Exception ex)
        {
            throw new CustomAppException("Error loading MMV DMI information: " + ex.Message);
        }
        memoryStream.Close();
        memoryStream.Dispose();

        MemoryStream theMemoryStream = HelperFunctions.GetLUT(Assembly.GetExecutingAssembly(), "FLEA_MMV_MMChain_LUT.dat");
        try
        {
            using BinaryReader binaryReader = new(theMemoryStream);
            //Read in the Yard lookup information
            while(binaryReader.BaseStream.Position < binaryReader.BaseStream.Length)
            {
                Sector sector = (Sector)binaryReader.ReadInt32();
                int animalType = binaryReader.ReadInt32();
                int organicMatterType = binaryReader.ReadInt32();

                MMV_Chain_Information info = new()
                {
                    HousingMassIn = binaryReader.ReadDouble(),
                    HousingVolumeIn = binaryReader.ReadDouble(),
                    HousingMassOut = binaryReader.ReadDouble(),
                    HousingVolumeOut = binaryReader.ReadDouble(),
                    StorageMassIn = binaryReader.ReadDouble(),
                    StorageVolumeIn = binaryReader.ReadDouble(),
                    StorageMassOut = binaryReader.ReadDouble(),
                    StorageVolumeOut = binaryReader.ReadDouble(),
                    SpreadingMassIn = binaryReader.ReadDouble(),
                    SpreadingVolumeIn = binaryReader.ReadDouble(),
                    SpreadingMassOut = binaryReader.ReadDouble(),
                    SpreadingVolumeOut = binaryReader.ReadDouble()
                };

                if(sector.Equals(Sector.Pigs))
                {

                    Pigs_Chain.Add(organicMatterType, info);

                }
                else if(sector.Equals(Sector.Poultry))
                {
                    Poultry_Chain.Add(organicMatterType, info);
                }
                else
                {
                    MinorLivestock_Chain.Add(animalType, info);
                }
                ;

            }
        }
        catch(Exception ex)
        {
            throw new CustomAppException("Error loading MMV chain information: " + ex.Message);
        }
    }

    /// <summary>
    /// Method to retrieve the DMI for pigs
    /// </summary>
    /// <param name="indoor">Boolean indicating if indoor pigs</param>
    /// <param name="pig">Enumerator of pig type</param>
    /// <returns>Dry matter intake for pig (kg)</returns>
    /// <exception cref="NotImplementedException"></exception>
    public double RetrieveDMI_Pig(PigType pig, bool indoor)
    {
        if(indoor)
        {
            return IndoorPigs_DMI[(int)pig].DryMatterIntake;
        }
        else
        {
            return OutdoorPigs_DMI[(int)pig].DryMatterIntake;
        }
    }
    /// <summary>
    /// Method to retrieve the cofefficient
    /// </summary>
    /// <param name="sector">Sector enumerator</param>
    /// <param name="animalType">Animal type</param>
    /// <param name="component">Enumerator indicating stage</param>
    /// <param name="organicMatterType">Enumerator of organic matter type</param>
    /// <param name="entering">boolean indicating if entering(true) or leaving (false) the stage</param>
    /// <param name="mass">boolean indicating if need value for mass (true) or volume (false)</param>
    /// <param name="indoor"></param>
    /// <returns>Coefficient</returns>
    /// <exception cref="NotImplementedException"></exception>
    public double RetrieveCoefficient(Sector sector, int animalType, ComponentType component, OrganicMatterType organicMatterType, bool entering, bool mass, bool indoor = false)
    {
        switch(sector)
        {
            case Sector.Pigs:
                if(component.Equals(ComponentType.Grazing))
                {
                    if(mass)
                    {
                        return OutdoorPigs_DMI[animalType].MassCoefficient;
                    }
                    else
                    {
                        return OutdoorPigs_DMI[animalType].VolumeCoefficient;
                    }
                }
                else
                {
                    switch(component)
                    {
                        case ComponentType.Housing:
                            if(mass && entering)
                            {
                                return Pigs_Chain[(int)organicMatterType].HousingMassIn;
                            }
                            else if(!mass && entering)
                            {
                                return Pigs_Chain[(int)organicMatterType].HousingVolumeIn;
                            }
                            else if(mass && !entering)
                            {
                                return Pigs_Chain[(int)organicMatterType].HousingMassOut;
                            }
                            else
                            {
                                return Pigs_Chain[(int)organicMatterType].HousingVolumeOut;
                            }

                        case ComponentType.Storage:
                            if(mass && entering)
                            {
                                return Pigs_Chain[(int)organicMatterType].StorageMassIn;
                            }
                            else if(!mass && entering)
                            {
                                return Pigs_Chain[(int)organicMatterType].StorageVolumeIn;
                            }
                            else if(mass && !entering)
                            {
                                return Pigs_Chain[(int)organicMatterType].StorageMassOut;
                            }
                            else
                            {
                                return Pigs_Chain[(int)organicMatterType].StorageVolumeOut;
                            }

                        case ComponentType.Spreading:
                            if(mass && entering)
                            {
                                return Pigs_Chain[(int)organicMatterType].SpreadingMassIn;
                            }
                            else if(!mass && entering)
                            {
                                return Pigs_Chain[(int)organicMatterType].SpreadingVolumeIn;
                            }
                            else if(mass && !entering)
                            {
                                return Pigs_Chain[(int)organicMatterType].SpreadingMassOut;
                            }
                            else
                            {
                                return Pigs_Chain[(int)organicMatterType].SpreadingVolumeOut;
                            }

                        default:
                            return double.NaN;

                    }
                }

            case Sector.Poultry:
                if(component.Equals(ComponentType.Grazing))
                {
                    if(mass)
                    {
                        return Poultry_DMI[animalType].MassCoefficient;
                    }
                    else
                    {
                        return Poultry_DMI[animalType].VolumeCoefficient;
                    }
                }
                else
                {
                    switch(component)
                    {
                        case ComponentType.Housing:
                            if(mass && entering)
                            {
                                return Poultry_Chain[(int)organicMatterType].HousingMassIn;
                            }
                            else if(!mass && entering)
                            {
                                return Poultry_Chain[(int)organicMatterType].HousingVolumeIn;
                            }
                            else if(mass && !entering)
                            {
                                return Poultry_Chain[(int)organicMatterType].HousingMassOut;
                            }
                            else
                            {
                                return Poultry_Chain[(int)organicMatterType].HousingVolumeOut;
                            }

                        case ComponentType.Storage:
                            if(mass && entering)
                            {
                                return Poultry_Chain[(int)organicMatterType].StorageMassIn;
                            }
                            else if(!mass && entering)
                            {
                                return Poultry_Chain[(int)organicMatterType].StorageVolumeIn;
                            }
                            else if(mass && !entering)
                            {
                                return Poultry_Chain[(int)organicMatterType].StorageMassOut;
                            }
                            else
                            {
                                return Poultry_Chain[(int)organicMatterType].StorageVolumeOut;
                            }

                        case ComponentType.Spreading:
                            if(mass && entering)
                            {
                                return Poultry_Chain[(int)organicMatterType].SpreadingMassIn;
                            }
                            else if(!mass && entering)
                            {
                                return Poultry_Chain[(int)organicMatterType].SpreadingVolumeIn;
                            }
                            else if(mass && !entering)
                            {
                                return Poultry_Chain[(int)organicMatterType].SpreadingMassOut;
                            }
                            else
                            {
                                return Poultry_Chain[(int)organicMatterType].SpreadingVolumeOut;
                            }

                        default:
                            return double.NaN;
                    }
                }

            case Sector.MinorLivestock:
                if(component.Equals(ComponentType.Grazing))
                {
                    if(mass)
                    {
                        return MinorLivestock_DMI[animalType].MassCoefficient;
                    }
                    else
                    {
                        return MinorLivestock_DMI[animalType].VolumeCoefficient;
                    }
                }
                else
                {
                    switch(component)
                    {
                        //Note that for minor livestock - the index is animal type and NOT organic matter type
                        case ComponentType.Housing:
                            if(mass && entering)
                            {
                                return MinorLivestock_Chain[animalType].HousingMassIn;
                            }
                            else if(!mass && entering)
                            {
                                return MinorLivestock_Chain[animalType].HousingVolumeIn;
                            }
                            else if(mass && !entering)
                            {
                                return MinorLivestock_Chain[animalType].HousingMassOut;
                            }
                            else
                            {
                                return MinorLivestock_Chain[animalType].HousingVolumeOut;
                            }

                        case ComponentType.Storage:
                            if(mass && entering)
                            {
                                return MinorLivestock_Chain[animalType].StorageMassIn;
                            }
                            else if(!mass && entering)
                            {
                                return MinorLivestock_Chain[animalType].StorageVolumeIn;
                            }
                            else if(mass && !entering)
                            {
                                return MinorLivestock_Chain[animalType].StorageMassOut;
                            }
                            else
                            {
                                return MinorLivestock_Chain[animalType].StorageVolumeOut;
                            }

                        case ComponentType.Spreading:
                            if(mass && entering)
                            {
                                return MinorLivestock_Chain[animalType].SpreadingMassIn;
                            }
                            else if(!mass && entering)
                            {
                                return MinorLivestock_Chain[animalType].SpreadingVolumeIn;
                            }
                            else if(mass && !entering)
                            {
                                return MinorLivestock_Chain[animalType].SpreadingMassOut;
                            }
                            else
                            {
                                return MinorLivestock_Chain[animalType].SpreadingVolumeOut;
                            }

                        default:
                            return double.NaN;

                    }
                }

            default:
                return double.NaN;

        }
    }
    /// <summary>
    /// Method to retrieve multipler for digestates
    /// </summary>
    /// <param name="sector">Sector enumerator</param>
    /// <exception cref="NotImplementedException"></exception>
    public double RetrieveDigestateWeighting(Sector sector)
    {
        return 1.0;
    }
    /// <summary>
    /// Method to retrieve the DMI for poultry
    /// </summary>
    /// <param name="poultry">Enumerator of poultry type</param>
    /// <returns>Dry matter intake for poultry (kg)</returns>
    /// <exception cref="NotImplementedException"></exception>
    public double RetrieveDMI_Poultry(PoultryType poultry)
    {
        return Poultry_DMI[(int)poultry].DryMatterIntake;
    }
    /// <summary>
    /// Method to retrieve the DMI for minor livestock
    /// </summary>
    /// <param name="minorLivestockType">Enumerator of minor livestock type</param>
    /// <returns>Dry matter intake for minor livestock (kg)</returns>
    /// <exception cref="NotImplementedException"></exception>
    public double RetrieveDMI_MinorLivestock(MinorLivestockType minorLivestockType)
    {
        return MinorLivestock_DMI[(int)minorLivestockType].DryMatterIntake;
    }
}

/// <summary>
/// Manure mass and volume dry matter intake information
/// </summary>
public class MMV_DMI_information
{
    /// <summary>
    /// Dry matter intake (kg dry matter per unit time [year])
    /// </summary>
    public double DryMatterIntake { get; set; } = double.NaN;
    /// <summary>
    /// The mass coefficient (kg/kg DM)
    /// </summary>
    public double MassCoefficient { get; set; } = double.NaN;
    /// <summary>
    /// The volume coefficient (l/kg DM)
    /// </summary>
    public double VolumeCoefficient { get; set; } = double.NaN;

}
/// <summary>
/// Manure mass and volume information for the manure management chain
/// </summary>
public class MMV_Chain_Information
{
    /// <summary>
    /// Mass of excreta entering housing (kg)
    /// </summary>
    public double HousingMassIn { get; set; } = double.NaN;
    /// <summary>
    /// Mass of manure leaving housing (kg)
    /// </summary>
    public double HousingMassOut { get; set; } = double.NaN;
    /// <summary>
    /// Volume of excreta entering housing (litres)
    /// </summary>
    public double HousingVolumeIn { get; set; } = double.NaN;
    /// <summary>
    /// Volume of manure leaving housing (litres)
    /// </summary>
    public double HousingVolumeOut { get; set; } = double.NaN;
    /// <summary>
    /// Mass of manure entering storage (kg)
    /// </summary>
    public double StorageMassIn { get; set; } = double.NaN;
    /// <summary>
    /// Mass of manure leaving storage (kg)
    /// </summary>
    public double StorageMassOut { get; set; } = double.NaN;
    /// <summary>
    /// Volume of manure entering storage (litres)
    /// </summary>
    public double StorageVolumeIn { get; set; } = double.NaN;
    /// <summary>
    /// Volume of manure leaving storage (litres)
    /// </summary>
    public double StorageVolumeOut { get; set; } = double.NaN;
    /// <summary>
    /// Mass of manure entering spreading (kg)
    /// </summary>
    public double SpreadingMassIn { get; set; } = double.NaN;
    /// <summary>
    /// Mass of manure leaving spreading (kg)
    /// </summary>
    public double SpreadingMassOut { get; set; } = double.NaN;
    /// <summary>
    /// Volume of manure entering spreading (litres)
    /// </summary>
    public double SpreadingVolumeIn { get; set; } = double.NaN;
    /// <summary>
    /// Volume of manure leaving spreading (litres)
    /// </summary>
    public double SpreadingVolumeOut { get; set; } = double.NaN;
}
