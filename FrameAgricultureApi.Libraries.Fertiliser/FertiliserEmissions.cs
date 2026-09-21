using FrameAgricultureApi.Generic;
using static FrameAgricultureApi.Enumerators.Enumerators;

namespace FrameAgricultureApi.Libraries.Fertiliser;

/// <summary>
/// <header>Fertiliser emissions class</header>
/// Contains the standard emissions dictionary plus a dictionary of additional outputs.
/// </summary>
public class FertiliserEmissions : BaseEmissions
{
    /// <summary>
    /// Dictionary of additional outputs as name, units and value
    /// Additional outputs are N applied by fertiliser type
    /// </summary>
    public Dictionary<FertiliserType, Emission> AdditionalOutputs { get; set; } = [];
    /// <summary>
    /// Initialisation function to initialise the class
    /// </summary>
    public void InitialiseFertiliserEmissions()
    {
        InitialiseBaseEmissions();
        AdditionalOutputs = new Dictionary<FertiliserType, Emission>();
        AdditionalOutputs.Clear();
        AdditionalOutputs.Add(FertiliserType.Urea, new Emission("Urea N applied", "kg", double.NaN));
        AdditionalOutputs.Add(FertiliserType.AmmoniumNitrate, new Emission("Ammonium NitrateN applied", "kg", double.NaN));
        AdditionalOutputs.Add(FertiliserType.CalciumAmmoniumNitrate, new Emission("Calcium Ammonium Nitrate N applied", "kg", double.NaN));
        AdditionalOutputs.Add(FertiliserType.UreaAmmoniumNitrate, new Emission("Urea Ammonium Nitrate N applied", "kg", double.NaN));
        AdditionalOutputs.Add(FertiliserType.AmmoniumSulphate_DiammoniumPhosphate, new Emission("Ammonium Sulphate Diammonium Phosphate N applied", "kg", double.NaN));
        AdditionalOutputs.Add(FertiliserType.OtherNitrogenincludingCompoundBlends, new Emission("Other Nitrogen Fertilisers N applied", "kg", double.NaN));
    }
    /// <summary>
    /// Export emissions function
    /// </summary>
    /// <returns>List of Emission objects, combining standard emissions and additional outputs together.</returns>
    public List<Emission> ExportEmissions()
    {

        List<Emission> retList = new List<Emission>();

        foreach(CoreEmissions key in EmissionsCore.Keys)
        {
            if(!double.IsNaN(EmissionsCore[key].Value))
            {
                retList.Add(EmissionsCore[key]);
            }
        }

        foreach(FertiliserType key in AdditionalOutputs.Keys)
        {
            if(!double.IsNaN(AdditionalOutputs[key].Value))
            {
                retList.Add(AdditionalOutputs[key]);
            }
        }
        return retList;
    }
    /// <summary>
    /// Function to round emissions to specified number of decimal places
    /// </summary>
    /// <param name="decimalPlaces">The number of decimal places to round the emissions to</param>
    public void RoundEmissions(int decimalPlaces)
    {
        base.RoundCoreEmissions(decimalPlaces);

        foreach(FertiliserType key in AdditionalOutputs.Keys)
        {
            if(!double.IsNaN(AdditionalOutputs[key].Value))
            {
                AdditionalOutputs[key].Value = Math.Round(AdditionalOutputs[key].Value, decimalPlaces);
            }
        }
    }
}
