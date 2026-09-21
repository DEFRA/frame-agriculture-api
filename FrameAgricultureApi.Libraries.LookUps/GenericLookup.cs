using FrameAgricultureApi.CustomErrorHandling;
using FrameAgricultureApi.Generic;
using System.Reflection;

namespace FrameAgricultureApi.Libraries.LookUps;

/// <summary>
/// The lookupt for generic paramters
/// </summary>
public class GenericLookup
{
    private static GenericLookup? _instance;

    /// <summary>
    /// The publci Instance for the Generic Lookup singleton class
    /// </summary>
    public static GenericLookup Instance
    {
        get
        {
            if(_instance == null)
            {
                _instance = new GenericLookup();
            }
            return _instance;
        }
    }

    //define paramwters
    private readonly double fracLeachArable;
    private readonly double fracLeachGrass;
    private readonly double iPCCEF1;
    private readonly double iPCCEF4;
    private readonly double iPCCEF5;

    /// <summary>
    /// Initialises GenericLookup Singleton class and loads data from LUT file
    /// </summary>
    /// <exception cref="CustomAppException"></exception>
    private GenericLookup()
    {
        //Data
        MemoryStream memoryStream = HelperFunctions.GetLUT(Assembly.GetExecutingAssembly(), "FLEA_Generic_LUT.dat");
        try
        {
            using BinaryReader binaryReader = new(memoryStream);
            fracLeachArable = binaryReader.ReadDouble();
            fracLeachGrass = binaryReader.ReadDouble();
            iPCCEF1 = binaryReader.ReadDouble();
            iPCCEF4 = binaryReader.ReadDouble();
            iPCCEF5 = binaryReader.ReadDouble();
        }
        catch(Exception ex)
        {
            throw new CustomAppException("Error loading generic LUT information: " + ex.Message);
        }
    }
    /// <summary>
    /// Gets Frac Leach default for arable crops
    /// </summary>
    /// <returns>Frac Leach in arable crops as a percentage</returns>
    public static double FracLeachArable()
    {
        return Instance.fracLeachArable;
    }
    /// <summary>
    /// Gets Frac Leach default for grass crops
    /// </summary>
    /// <returns>Frac Leach for grass crops as a percentage</returns>
    public static double FracLeachGrass()
    {
        return Instance.fracLeachGrass;
    }
    /// <summary>
    /// Returns IPCC Emission Factor 1
    /// </summary>
    /// <returns>IPCC EF 1</returns>
    public static double IPCCEF1()
    {
        return Instance.iPCCEF1;
    }
    /// <summary>
    /// Returns IPCC Emission Factor 4
    /// </summary>
    /// <returns>IPCC EF 4</returns>
    public static double IPCCEF4()
    {
        return Instance.iPCCEF4;
    }
    /// <summary>
    /// Returns IPCC Emission Factor 5
    /// </summary>
    /// <returns>IPCC EF 5</returns>
    public static double IPCCEF5()
    {
        return Instance.iPCCEF5;
    }
}
