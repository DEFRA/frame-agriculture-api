using FrameAgricultureApi.CustomErrorHandling;
using FrameAgricultureApi.Generic;
using System.Reflection;

//Todo this could be merged with methane Lookup?
namespace FrameAgricultureApi.Libraries.LookUps;

public class EntericMethaneLookup
{
    private static EntericMethaneLookup? _instance;

    /// <summary>
    /// Instance of singleton
    /// </summary>
    public static EntericMethaneLookup Instance
    {
        get
        {
            _instance ??= new EntericMethaneLookup();
            return _instance;
        }
    }

    private readonly Dictionary<Tuple<int, int>, double> _CH4 = [];

    private EntericMethaneLookup()
    {
        MemoryStream memoryStream = HelperFunctions.GetLUT(Assembly.GetExecutingAssembly(), "FLEA_EntericMethane_LUT.dat");
        try
        {
            using BinaryReader binaryReader = new(memoryStream);
            //Read in the Methane lookup information
            while(binaryReader.BaseStream.Position < binaryReader.BaseStream.Length)
            {
                int Sector = binaryReader.ReadInt32();
                int animalType = binaryReader.ReadInt32();
                double B0 = binaryReader.ReadDouble();

                _CH4.Add(new Tuple<int, int>(Sector, animalType), B0);

            }
        }
        catch(Exception ex)
        {
            throw new CustomAppException("Error loading methane CH4 information: " + ex.Message);
        }
    }

    /// <summary>
    ///
    /// </summary>
    /// <param name="sector">Sector enumerator</param>
    /// <param name="animalType">Animal type enumerator as integer (as sector specific)</param>
    /// <returns>Methane producing capacity (cubic metres per kg volatile solids)</returns>
    public static double RetrieveCH4(Enumerators.Enumerators.Sector sector, int animalType)
    {
        return Instance._CH4[new Tuple<int, int>((int)sector, animalType)];
    }
}

