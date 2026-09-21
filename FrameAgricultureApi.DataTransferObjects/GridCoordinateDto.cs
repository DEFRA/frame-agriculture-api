namespace FrameAgricultureApi.DataTransferObjects;

/// <summary>
/// Coordinate data class for 10km squares
/// </summary>
public class GridCoordinateDto
{
    /// <summary>
    /// The Easting as British National Grid reference (for centroid of a 10km square) in metres east of origin
    /// </summary>
    public int Easting { get; set; }
    /// <summary>
    /// Northing as British National Grid reference (for centroid of a 10km square) in metres north of origin
    /// </summary>
    public int Northing { get; set; }
}
