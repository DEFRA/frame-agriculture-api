namespace FrameAgricultureApi.Components.Tests;

public class HelperFunctions
{
    public static Boolean IsWithinPercentage(double expectedValue, double actualValue, double percentage)
    {
        if(Double.IsNaN(expectedValue) && Double.IsNaN(actualValue))
            return true;

        return Math.Abs(expectedValue - actualValue) < (percentage * expectedValue);
    }
}
