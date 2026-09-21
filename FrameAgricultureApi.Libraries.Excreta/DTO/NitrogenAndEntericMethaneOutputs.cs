using FrameAgricultureApi.Generic;

namespace FrameAgricultureApi.Libraries.Excreta.DTO;

public class NitrogenAndEntericMethaneOutputs
{
    public double EntericMethane { get; set; }
    public NitrogenOutputs NitrogenOutputs { get; set; } = new NitrogenOutputs();
    public double TotalGEIntake { get; set; }
    public double TotalDMIIntake { get; set; }
    public double TotalMERequirement { get; set; }
    public double TotalVolatileSolidsExcretion { get; set; }

    public void RoundMembers(int decimalPlaces)
    {
        this.NitrogenOutputs.RoundMembers(decimalPlaces);
        this.EntericMethane = Math.Round(EntericMethane, decimalPlaces);
        this.TotalGEIntake = Math.Round(TotalGEIntake, decimalPlaces);
        this.TotalDMIIntake = Math.Round(TotalDMIIntake, decimalPlaces);
        this.TotalMERequirement = Math.Round(TotalMERequirement, decimalPlaces);
        this.TotalVolatileSolidsExcretion = Math.Round(TotalVolatileSolidsExcretion, decimalPlaces);
    }
}

public class NitrogenOutputs
{
    public double TotalNitrogenIntake { get; set; }
    public double TotalNitrogenExcretion { get; set; }
    public double TotalNitrogenExcretionAsUrine { get; set; }
    public double TotalNitrogenExcretionAsFaeces { get; set; }

    public void RoundMembers(int decimalPlaces)
    {
        this.TotalNitrogenIntake = Math.Round(TotalNitrogenIntake, decimalPlaces);
        this.TotalNitrogenExcretion = Math.Round(TotalNitrogenExcretion, decimalPlaces);
        this.TotalNitrogenExcretionAsUrine = Math.Round(TotalNitrogenExcretionAsUrine, decimalPlaces);
        this.TotalNitrogenExcretionAsFaeces = Math.Round(TotalNitrogenExcretionAsFaeces, decimalPlaces);
    }
}

public class NitrogenAndEntericEmissions
{
    public List<Emission> Excreta { get; set; } = [];

    public NitrogenAndEntericEmissions(NitrogenOutputs nitrogenOutputs)
    {
        Excreta = new();
        Excreta.Add(new Emission("Total Nitrogen Intake", "", nitrogenOutputs.TotalNitrogenIntake));
        Excreta.Add(new Emission("Total Nitrogen Excretion", "", nitrogenOutputs.TotalNitrogenExcretion));
        Excreta.Add(new Emission("Total Nitrogen Excretion as Urine", "", nitrogenOutputs.TotalNitrogenExcretionAsUrine));
        Excreta.Add(new Emission("Total Nitrogen Excretion as Faeces", "", nitrogenOutputs.TotalNitrogenExcretionAsFaeces));

    }
}
