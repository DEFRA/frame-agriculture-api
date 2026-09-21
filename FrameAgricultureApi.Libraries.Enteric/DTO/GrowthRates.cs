namespace FrameAgricultureApi.Libraries.Enteric.DTO;

public class GrowthRates
{
    public double GrowthRateFirstMonthToFirstYear { get; set; }
    public double GrowthRateConceptionToCalving { get; set; }
    public double GrowthRateCalvingToDeath { get; set; }
    public double GrowthRateBirthToFirstYear { get; set; }
    public double GrowthRateFirstYearToFirstConception { get; set; }

    public void RoundMembers(int decimalPlaces)
    {
        this.GrowthRateFirstMonthToFirstYear = Math.Round(GrowthRateFirstMonthToFirstYear, decimalPlaces);
        this.GrowthRateConceptionToCalving = Math.Round(GrowthRateConceptionToCalving, decimalPlaces);
        this.GrowthRateCalvingToDeath = Math.Round(GrowthRateCalvingToDeath, decimalPlaces);
        this.GrowthRateBirthToFirstYear = Math.Round(GrowthRateBirthToFirstYear, decimalPlaces);
        this.GrowthRateFirstYearToFirstConception = Math.Round(GrowthRateFirstYearToFirstConception, decimalPlaces);
    }
}
