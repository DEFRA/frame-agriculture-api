namespace FrameAgricultureApi.IOClasses;

public class ManureMassVolumeStandardOutput
{

    public List<StandardOutput> ManureMassVolumes { get; set; }
    public string Sector { get; set; }
    public string Stage { get; set; }
    public string Animal { get; set; }

    public ManureMassVolumeStandardOutput(List<StandardOutput> manureMassVolumes, string sector, string stage, string animal)
    {
        ManureMassVolumes = manureMassVolumes;
        Sector = sector;
        Stage = stage;
        Animal = animal;
    }

    public ManureMassVolumeStandardOutput()
    {
        ManureMassVolumes = [];
        Sector = string.Empty;
        Stage = string.Empty;
        Animal = string.Empty;
    }
}
