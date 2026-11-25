using System;

namespace FrameAgricultureApi.Core.Outputs;

public class CoverCropInputLookupOutput
{
    public CoverCropInputsHelper CoverCropInputsHelper { get; set; }

}

public class CoverCropInputsHelper
{
    public InputHelper N_Return { get; set; }
}

public class CoverCropEmissionFactorsHelper
{
    public InputHelper
}

public class InputHelper
{
    public Unit Unit { get; set; }
    public decimal Value { get; set; }
}
