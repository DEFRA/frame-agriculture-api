using FrameAgricultureApi.Generic;

namespace FrameAgricultureApi.Libraries.ManureManagement;

public static class ManureManagementGenericEquations
{
    /// <summary>
    /// Equation to calculate the percentage of urine in excreta from cattle
    /// </summary>
    /// <param name="intercepts">Intercepts for calcualtion of urine N and total N in cattle</param>
    /// <param name="gradients">Gradients for equations to calcualte urine N and total N in cattle</param>
    /// <param name="intakeNitrogen">The nitrogen intake in kg per month</param>
    /// <returns>percent of excreta that is urine</returns>
    public static double CattlePercentUrine(double[] intercepts, double[] gradients, double intakeNitrogen)
    {
        double urine = GenericEquations.SimpleLinear(intercepts[1], gradients[1], intakeNitrogen);
        double total = GenericEquations.SimpleLinear(intercepts[0], gradients[0], intakeNitrogen);
        return (urine / total) * 100.0;
    }
    /// <summary>
    /// Equation to calculate emission from urine and dung - can be applied to multiple emissions
    /// </summary>
    /// <param name="urine">Nitrogen in urine exreted (kg) per unit time</param>
    /// <param name="dung">Nitrogen in dung excreted (kg) oper unti time</param>
    /// <param name="emissionFactorUrine">Emission factor for urine as a percentage</param>
    /// <param name="emissionFactorDung">emission factor fo dung as a percentage</param>
    /// <returns></returns>
    public static double Emission_UrineDung(double urine, double dung, double emissionFactorUrine, double emissionFactorDung)
    {
        double urineEmission = GenericEquations.Emission_PercentageEF(urine, emissionFactorUrine);
        double dungEmission = GenericEquations.Emission_PercentageEF(dung, emissionFactorDung);
        return (urineEmission + dungEmission);
    }
    /// <summary>
    /// Method to calculate TAN from sheep urine and dung
    /// </summary>
    /// <param name="nitrogenUrine">nitrogen in urine deposited outdoors (kg)</param>
    /// <param name="nitrogenDung">nitrogen in dung deposited outdoors (kg)</param>
    /// <returns>Total available nitrogen (kg) from sheep urine and dung deposited outdoors</returns>
    public static double SheepTAN(double nitrogenUrine, double nitrogenDung)
    {
        return ((nitrogenUrine * ManureManagementParameters.sheepUrineTAN) + (nitrogenDung * ManureManagementParameters.sheepDungTAN));
    }

    /// <summary>
    /// Calculates frac leach based on fertilisation rate of grass
    /// </summary>
    /// <param name="fertiliserRate">Rate of all fertiliser applied (total kg N per hectare)</param>
    /// <param name="multipliers">multipliers for calcualtion from power 1 to power 4</param>
    /// <param name="uncertainty">uncertinaty value</param>
    /// <param name="fracLeach">the calculated fracLeach as a proportion</param>
    /// <returns>True if succesful calcaultion, false otherwise</returns>
    public static double VariableFracLeachEmissionFactor(double fertiliserRate, double[] multipliers, double uncertainty)
    {
        double temp = (multipliers[3] * Math.Pow(fertiliserRate, 4)) + (multipliers[2] * Math.Pow(fertiliserRate, 3)) + (multipliers[1] * Math.Pow(fertiliserRate, 2)) + (multipliers[0] * fertiliserRate) + uncertainty;
        double fracLeach = Math.Max(0, Math.Min(100, temp));
        return fracLeach;
    }
}
