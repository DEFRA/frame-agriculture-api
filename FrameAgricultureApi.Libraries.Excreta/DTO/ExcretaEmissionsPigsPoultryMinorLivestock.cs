using FrameAgricultureApi.Generic;

namespace FrameAgricultureApi.Libraries.Excreta.DTO;

public class ExcretaEmissionsPigsPoultryMinorLivestock
{
    public double Nitrogen { get; set; }
    public double EntericMethane { get; set; }
    public double TAN { get; set; }
    public double VolatileSolids { get; set; }

    public void RoundMembers(int decimalPlaces)
    {
        this.Nitrogen = Math.Round(Nitrogen, decimalPlaces);
        this.EntericMethane = Math.Round(EntericMethane, decimalPlaces);
        this.TAN = Math.Round(TAN, decimalPlaces);
        this.VolatileSolids = Math.Round(VolatileSolids, decimalPlaces);
    }
}

public class NitrogenEmission
{
    public List<Emission> Exreta { get; set; } = new();

    public NitrogenEmission(ExcretaEmissionsPigsPoultryMinorLivestock excretaEmissions)
    {
        Exreta = new();
        Exreta.Add(new Emission("Nitrogen", "", excretaEmissions.Nitrogen));
        Exreta.Add(new Emission("TAN", "", excretaEmissions.TAN));
    }
}

public enum NitrogenEmissionsPoultry
{
    Nitrogen = 0,
    TAN = 1
}

public class ExcretaEmissionsFreeRangePoultry
{
    public ExcretaEmissionsPigsPoultryMinorLivestock IndoorsEmissions { get; set; } = new ExcretaEmissionsPigsPoultryMinorLivestock();
    public ExcretaEmissionsPigsPoultryMinorLivestock OutdoorsEmissions { get; set; } = new ExcretaEmissionsPigsPoultryMinorLivestock();

    public void RoundMembers(int decimalPlaces)
    {
        IndoorsEmissions.RoundMembers(decimalPlaces);
        OutdoorsEmissions.RoundMembers(decimalPlaces);
    }
}

public class NitrogenEmissionFreeRangePoultry
{
    public List<Emission> Excreta { get; set; } = [];

    public NitrogenEmissionFreeRangePoultry(ExcretaEmissionsFreeRangePoultry excretaEmissions)
    {
        Excreta = new();
        Excreta.Add(new Emission("Indoor Nitrogen", "", excretaEmissions.IndoorsEmissions.Nitrogen));
        Excreta.Add(new Emission("Indoor TAN", "", excretaEmissions.IndoorsEmissions.TAN));
        Excreta.Add(new Emission("Outdoor Nitrogen", "", excretaEmissions.OutdoorsEmissions.Nitrogen));
        Excreta.Add(new Emission("Outdoor TAN", "", excretaEmissions.OutdoorsEmissions.TAN));

    }
};

public class VolatileEmissionFreeRangePoultry
{
    public List<Emission> Excreta { get; set; } = new();
    public VolatileEmissionFreeRangePoultry(ExcretaEmissionsFreeRangePoultry excretaEmissions)
    {
        Excreta = new();
        Excreta.Add(new Emission("Indoor Volatile Solids", "", excretaEmissions.IndoorsEmissions.VolatileSolids));
        Excreta.Add(new Emission("Outdoor Volatile Solids", "", excretaEmissions.OutdoorsEmissions.VolatileSolids));
    }
}

public enum NitrogenEmissionsFreeRangePoultry
{
    IndoorNitrogen = 0,
    IndoorTAN = 1,
    OutdoorNitrogen = 2,
    OutdoorTAN = 3,
}
