using FrameAgricultureApi.Generic;
using static FrameAgricultureApi.Enumerators.Enumerators;

namespace FrameAgricultureApi.Libraries.ManureMassVolume;

public class ManureMassVolumeOutput
{
    public Dictionary<ManureMassVolumeOutputs, Emission> ManureMassVolume { get; set; } = [];

    public void InitialiseManureMassVolumeOutput()
    {
        ManureMassVolume = [];
        ManureMassVolume.Clear();
        ManureMassVolume.Add(ManureMassVolumeOutputs.ExcretaVolumeIn, new Emission("Excreta volume entering stage", "litres", double.NaN));
        ManureMassVolume.Add(ManureMassVolumeOutputs.ManureVolumeIn, new Emission("Manure volume entering stage", "litres", double.NaN));
        ManureMassVolume.Add(ManureMassVolumeOutputs.UrineVolumeIn, new Emission("Urine volume entering stage", "litres", double.NaN));
        ManureMassVolume.Add(ManureMassVolumeOutputs.DungVolumeIn, new Emission("Dung volume entering stage", "litres", double.NaN));
        ManureMassVolume.Add(ManureMassVolumeOutputs.ManureVolumeOut, new Emission("Manure volume leaving stage", "litres", double.NaN));
        ManureMassVolume.Add(ManureMassVolumeOutputs.ExcretaMassIn, new Emission("Excreta mass entering stage", "kg", double.NaN));
        ManureMassVolume.Add(ManureMassVolumeOutputs.ManureMassIn, new Emission("Manure mass entering stage", "kg", double.NaN));
        ManureMassVolume.Add(ManureMassVolumeOutputs.UrineMassIn, new Emission("Urine mass entering stage", "kg", double.NaN));
        ManureMassVolume.Add(ManureMassVolumeOutputs.DungMassIn, new Emission("Dung mass entering stage", "kg", double.NaN));
        ManureMassVolume.Add(ManureMassVolumeOutputs.ManureMassOut, new Emission("Manure mass leaving stage", "kg", double.NaN));
    }

    public List<Emission> ExportEmissions()
    {
        List<Emission> retList = [];

        foreach(ManureMassVolumeOutputs key in ManureMassVolume.Keys)
        {
            if(!double.IsNaN(ManureMassVolume[key].Value))
            {
                retList.Add(ManureMassVolume[key]);
            }
        }
        return retList;
    }

    public void RoundEmissions(int decimalPlaces)
    {

        foreach(ManureMassVolumeOutputs key in ManureMassVolume.Keys)
        {
            if(!double.IsNaN(ManureMassVolume[key].Value))
            {
                ManureMassVolume[key].Value = Math.Round(ManureMassVolume[key].Value, decimalPlaces);
            }
        }
    }
}
