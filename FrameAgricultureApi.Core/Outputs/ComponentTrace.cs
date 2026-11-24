using System;

namespace FrameAgricultureApi.Core.Outputs;

public class ComponentTrace
{

    public string ComponentName { get; set; }
    public List<FunctionTrace> FunctionTraces { get; set; }
    public List<EquationTrace> EquationTraces { get; set; }
}