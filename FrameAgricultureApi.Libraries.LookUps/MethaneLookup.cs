using FrameAgricultureApi.CustomErrorHandling;
using FrameAgricultureApi.Generic;
using System.Reflection;

namespace FrameAgricultureApi.Libraries.LookUps;

/// <summary>
/// Methane lookup class
/// </summary>
public class MethaneLookup
{
    private static MethaneLookup? _instance;

    /// <summary>
    /// Instance of singleton
    /// </summary>
    public static MethaneLookup Instance
    {
        get
        {
            _instance ??= new MethaneLookup();
            return _instance;
        }
    }

    private readonly Dictionary<Tuple<int, int>, double> _B0 = [];
    /// <summary>
    /// Methane B0 constructor
    /// </summary>
    /// <exception cref="Exception">Errors encountered loading B0 LUT</exception>
    private MethaneLookup()
    {
        MemoryStream memoryStream = HelperFunctions.GetLUT(Assembly.GetExecutingAssembly(), "FLEA_MethaneB0_LUT.dat");
        try
        {
            using BinaryReader binaryReader = new(memoryStream);
            //Read in the Methane lookup information
            while(binaryReader.BaseStream.Position < binaryReader.BaseStream.Length)
            {
                int Sector = binaryReader.ReadInt32();
                int animalType = binaryReader.ReadInt32();
                double B0 = binaryReader.ReadDouble();

                _B0.Add(new Tuple<int, int>(Sector, animalType), B0);

            }
        }
        catch(Exception ex)
        {
            throw new CustomAppException("Error loading methane B0 information: " + ex.Message);
        }

    }

    /// <summary>
    /// Retrieve B0 parameter (methane producgin capacity)
    /// </summary>
    /// <param name="sector">Sector enumerator</param>
    /// <param name="animalType">Animal type enumerator as integer (as sector specific)</param>
    /// <returns>Methane producing capacity (cubic metres per kg volatile solids)</returns>
    public static double RetrieveB0(Enumerators.Enumerators.Sector sector, int animalType)
    {
        return Instance._B0[new Tuple<int, int>((int)sector, animalType)];
    }
}
