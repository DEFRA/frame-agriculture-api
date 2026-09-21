using FrameAgricultureApi.Generic;
using static FrameAgricultureApi.Enumerators.Enumerators;

namespace FrameAgricultureApi.Libraries.Excreta;

public class ExcretaEmissions
{
    public Dictionary<ExcretaOutputs, Emission> ExcretaOutputEmissions { get; set; } = [];

    public void InitialiseExcretaEmissions()
    {
        ExcretaOutputEmissions.Clear();
        ExcretaOutputEmissions.Add(ExcretaOutputs.ExcretaTotalN, new Emission("Nitrogen content of faeces", "kg/head/place/year", double.NaN));
        ExcretaOutputEmissions.Add(ExcretaOutputs.ExcretaTAN, new Emission("Total Ammoniacal Nitrogen content of faeces", "kg", double.NaN));
        ExcretaOutputEmissions.Add(ExcretaOutputs.ExcretaOrganicN, new Emission("Organic Nitrogen", "kg", double.NaN));
    }

    public List<Emission> ExportEmissions()
    {
        List<Emission> emissions = new List<Emission>();

        foreach(ExcretaOutputs key in ExcretaOutputEmissions.Keys)
        {
            if(!double.IsNaN(ExcretaOutputEmissions[key].Value))
            {
                emissions.Add(ExcretaOutputEmissions[key]);
            }
        }

        return emissions;
    }

    public void RoundEmissions(int decimalPlaces)
    {
        foreach(ExcretaOutputs key in ExcretaOutputEmissions.Keys)
        {
            if(!double.IsNaN(ExcretaOutputEmissions[key].Value))
            {
                ExcretaOutputEmissions[key].Value = Math.Round(ExcretaOutputEmissions[key].Value, decimalPlaces);
            }
        }
    }
}
