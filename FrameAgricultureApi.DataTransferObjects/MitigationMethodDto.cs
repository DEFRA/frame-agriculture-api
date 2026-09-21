namespace FrameAgricultureApi.DataTransferObjects;

/// <summary>
/// Class for producing list o Mitigation methods for passing to other tools
/// </summary>
public class MitigationMethodDto
{
    /// <summary>
    /// Name of miotigation method
    /// </summary>
    public string MethodName { get; set; } = "";
    /// <summary>
    /// ID of mitigation method
    /// </summary>
    public int MethodID { get; set; }
    /// <summary>
    /// Name of component to which mitigation method applies
    /// </summary>
    public string ComponentName { get; set; } = "";
    /// <summary>
    /// ID of component to which mitigaiotn method applies
    /// </summary>
    public int ComponentID { get; set; }
}
