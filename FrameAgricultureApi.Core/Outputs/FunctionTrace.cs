using System;

namespace FrameAgricultureApi.Core.Outputs;

public class FunctionTrace
{
    public string FunctionName { get; set; }
    public List<EquationTrace> EquationTraces { get; set; }
}