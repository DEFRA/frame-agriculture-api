namespace FrameAgricultureApi.DataTransferObjects;

/// <summary>
/// Class to export grid location ID
/// </summary>
public class GridLocationDto : GridCoordinateDto
{
    /// <summary>
    /// ID for 10km grid square
    /// </summary>
    public int GridSquareID { get; set; }
}
