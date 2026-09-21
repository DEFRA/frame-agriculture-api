namespace FrameAgricultureApi.Libraries.Excreta.DTO;

public class ExcretalPoolsOutput
{
    public double VSExcretion { get; set; }
    public double VSExcretionGrazing { get; set; }
    public double VSExcretionYardCollecting { get; set; }
    public double VSExcretionYardFeeding { get; set; }
    public double VSExcretionHousing { get; set; }

    public double ExcretaMassGrazing { get; set; }
    public double ExcretaVolumeGrazing { get; set; }
    public double ExcretaMassHousing { get; set; }
    public double ExcretaVolumeHousing { get; set; }
    public double ExcretaMassFeedingYard { get; set; }
    public double ExcretaVolumeFeedingYard { get; set; }
    public double ExcretaMassCollectingYard { get; set; }
    public double ExcretaVolumeCollectingYard { get; set; }

    public double UrineMass { get; set; }
    public double FaecesMass { get; set; }

    public void RoundMembers(int decimalPlaces)
    {
        this.VSExcretion = Math.Round(VSExcretion, decimalPlaces);
        this.VSExcretionGrazing = Math.Round(VSExcretionGrazing, decimalPlaces);
        this.VSExcretionYardCollecting = Math.Round(VSExcretionYardCollecting, decimalPlaces);
        this.VSExcretionYardFeeding = Math.Round(VSExcretionYardFeeding, decimalPlaces);
        this.VSExcretionHousing = Math.Round(VSExcretionHousing, decimalPlaces);
        this.ExcretaMassGrazing = Math.Round(ExcretaMassGrazing, decimalPlaces);
        this.ExcretaVolumeGrazing = Math.Round(ExcretaVolumeGrazing, decimalPlaces);
        this.ExcretaMassHousing = Math.Round(ExcretaMassHousing, decimalPlaces);
        this.ExcretaVolumeHousing = Math.Round(ExcretaVolumeHousing, decimalPlaces);
        this.ExcretaMassFeedingYard = Math.Round(ExcretaMassFeedingYard, decimalPlaces);
        this.ExcretaVolumeFeedingYard = Math.Round(ExcretaVolumeFeedingYard, decimalPlaces);
        this.ExcretaMassCollectingYard = Math.Round(ExcretaMassCollectingYard, decimalPlaces);
        this.ExcretaVolumeCollectingYard = Math.Round(ExcretaVolumeCollectingYard, decimalPlaces);
        this.UrineMass = Math.Round(UrineMass, decimalPlaces);
        this.FaecesMass = Math.Round(FaecesMass, decimalPlaces);
    }
}
