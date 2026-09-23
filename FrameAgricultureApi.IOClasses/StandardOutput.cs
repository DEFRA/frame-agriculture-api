namespace FrameAgricultureApi.Libraries.CoverCrops;

/// <summary>
/// The standard outputs class
/// </summary>
public class StandardOutput
{
    /// <summary>
    /// The list of emissions calculated
    /// </summary>
    public List<Emission> Emissions { get; set; }
    /// <summary>
    /// The source of emissions (e.g. crop type, animal type)
    /// </summary>
    public string Source { get; set; }
    /// <summary>
    /// The stage of emissions (e.g. residues, fertiliser type, graxing,housing, etc.)
    /// </summary>
    public string Stage { get; set; }
    /// <summary>
    /// The type from which the output is generated (e.g. housing type)
    /// </summary>
    public string MyType { get; set; }

    /// <summary>
    /// Constructor for standard outputs
    /// </summary>
    /// <param name="source">source of emissions</param>
    /// <param name="mytype">the emission type</param>
    /// <param name="stage">stage name for emissions</param>
    /// <param name="emissions">List of Emission objects for all emissions</param>
    public StandardOutput(string source, string mytype, string stage, List<Emission> emissions)
    {
        Emissions = new List<Emission>();
        Emissions.AddRange(emissions);
        Source = source;
        MyType = mytype;
        Stage = stage;
        Emissions = emissions;
    }
    /// <summary>
    /// Defualt "empty" constructor for standard output
    /// </summary>
    public StandardOutput()
    {
        Source = "No source defined";
        Stage = "No stage defined";
        MyType = "No type defined";
        Emissions = new List<Emission>();
    }
}
