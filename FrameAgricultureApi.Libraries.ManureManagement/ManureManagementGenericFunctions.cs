using FrameAgricultureApi.CustomErrorHandling;
using static FrameAgricultureApi.Enumerators.Enumerators;

namespace FrameAgricultureApi.Libraries.ManureManagement;

public static class ManureManagementGenericFunctions
{
    /// <summary>
    /// Set the cattle total N and TAN from urine and dung
    /// </summary>
    /// <param name="urine">urine N deposited in housing (kg/head)</param>
    /// <param name="dung">dung N deposited in housing (kg/head)</param>
    /// <param name="totalAmmoniacalNitrogen">Output - TAN in excreta depozited in housing (kg/head)</param>
    /// <param name="totalNitrogen">Output - total N in excreat deposited in housing (kg/head)</param>
    public static void SetCattleN(double urine, double dung, out double totalAmmoniacalNitrogen, out double totalNitrogen)
    {
        totalAmmoniacalNitrogen = urine;
        totalNitrogen = dung;
    }
    /// <summary>
    /// Calculates the percent of excreta as urine for cattle
    /// </summary>
    /// <param name="intakeNitrogen">Nitroge intkae in kg/head</param>
    /// <param name="sector">the sector (beef or dairy)</param>
    /// <param name="DairyAnimalType">Dairy animal type (DC1 to DC4)</param>
    /// <returns>The percentage of excreta that is urine</returns>
    /// <exception cref="Exception">Exception if dairy aniimal not specified</exception>
    public static double CattlePercentNitrogenExcretionasUrine(double intakeNitrogen, Sector sector, DairyCattle DairyAnimalType = DairyCattle.NotSet)
    {
        if(sector.Equals(Sector.Beef))
        {
            double[] intercepts = new double[2];
            double[] gradients = new double[2];
            intercepts[0] = ManureManagementParameters.excretionNitrogenTotal_Intercept_Beef;
            gradients[0] = ManureManagementParameters.excretionNitrogenTotal_Slope_Beef;
            intercepts[1] = ManureManagementParameters.excretionNitrogenUrine_Intercept_Beef;
            gradients[1] = ManureManagementParameters.excretionNitrogenUrine_Slope_Beef;
            return ManureManagementGenericEquations.CattlePercentUrine(intercepts, gradients, intakeNitrogen);
        }
        else
        {
            if(DairyAnimalType == DairyCattle.NotSet)
            { throw new CustomAppException("You need to specify a dairy animal type to calculation the percentage of urine."); }
            else if(DairyAnimalType.Equals(DairyCattle.DC4_DairyCows))
            {
                double[] intercepts = new double[2];
                double[] gradients = new double[2];
                intercepts[0] = ManureManagementParameters.excretionNitrogenTotal_Intercept_DairyCow;
                gradients[0] = ManureManagementParameters.excretionNitrogenTotal_Slope_DairyCow;
                intercepts[1] = ManureManagementParameters.excretionNitrogenUrine_Intercept_DairyCow;
                gradients[1] = ManureManagementParameters.excretionNitrogenUrine_Slope_DairyCow;
                return ManureManagementGenericEquations.CattlePercentUrine(intercepts, gradients, intakeNitrogen);
            }
            else
            {
                double[] intercepts = new double[2];
                double[] gradients = new double[2];
                intercepts[0] = ManureManagementParameters.excretionNitrogenTotal_Intercept_DairyOther;
                gradients[0] = ManureManagementParameters.excretionNitrogenTotal_Slope_DairyOther;
                intercepts[1] = ManureManagementParameters.excretionNitrogenUrine_Intercept_DairyOther;
                gradients[1] = ManureManagementParameters.excretionNitrogenUrine_Slope_DairyOther;
                return ManureManagementGenericEquations.CattlePercentUrine(intercepts, gradients, intakeNitrogen);
            }
        }
    }
}
