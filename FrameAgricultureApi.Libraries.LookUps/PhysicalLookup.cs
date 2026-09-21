using FrameAgricultureApi.DataTransferObjects;
using FrameAgricultureApi.Generic;
using System.Reflection;
using static FrameAgricultureApi.Enumerators.Enumerators;

namespace FrameAgricultureApi.Libraries.LookUps;

/// <summary>
/// Physical information lookup class (singleton)
/// </summary>
public class PhysicalLookup
{
    /// <summary>
    /// private instance
    /// </summary>
    private static PhysicalLookup? _instance;

    /// <summary>
    /// public instance
    /// </summary>
    public static PhysicalLookup Instance
    {
        get
        {
            if(_instance == null)
            {
                _instance = new PhysicalLookup();
            }
            return _instance;
        }
    }

    //set arrays
    private Dictionary<int, double> summerRainfall = new Dictionary<int, double>();
    private Dictionary<int, double> annualRainfall = new Dictionary<int, double>();
    private Dictionary<int, double> annualTemperature = new Dictionary<int, double>();
    private Dictionary<int, double[]> monthlyTemperature = new Dictionary<int, double[]>();
    private Dictionary<int, double[]> monthlyRainfall = new Dictionary<int, double[]>();
    private Dictionary<int, double[]> rainfallEventLag = new Dictionary<int, double[]>();
    private List<int> gridCells = new List<int>();
    private Dictionary<int, GridCoordinateDto> GridLocations = new Dictionary<int, GridCoordinateDto>();
    private Dictionary<int, int> cellClimateRegion = new Dictionary<int, int>();
    private List<int> climateRegions = [];

    private PhysicalLookup()
    {
        //clear dictionaries
        summerRainfall.Clear();
        annualRainfall.Clear();
        annualTemperature.Clear();
        monthlyTemperature.Clear();
        monthlyRainfall.Clear();
        rainfallEventLag.Clear();
        gridCells.Clear();
        climateRegions.Clear();

        //Fertiliser data
        MemoryStream memoryStream = HelperFunctions.GetLUT(Assembly.GetExecutingAssembly(), "FLEA_Physical_LUT.dat");
        using(BinaryReader binaryReader = new BinaryReader(memoryStream))
        {
            int cellcount = binaryReader.ReadInt32();

            for(int i = 0; i < cellcount; i++)
            {
                int cell = binaryReader.ReadInt32();
                gridCells.Add(cell);
                GridCoordinateDto coordinate = new GridCoordinateDto() { Easting = binaryReader.ReadInt32(), Northing = binaryReader.ReadInt32() };
                GridLocations.Add(cell, coordinate);
                //summer rainfall
                summerRainfall.Add(cell, binaryReader.ReadDouble());
                //annualrainfall
                annualRainfall.Add(cell, binaryReader.ReadDouble());
                //annual temperature
                annualTemperature.Add(cell, binaryReader.ReadDouble());
                //monthly Temperature
                monthlyTemperature.Add(cell, [binaryReader.ReadDouble(),
                    binaryReader.ReadDouble(),
                    binaryReader.ReadDouble(),
                    binaryReader.ReadDouble(),
                    binaryReader.ReadDouble(),
                    binaryReader.ReadDouble(),
                    binaryReader.ReadDouble(),
                    binaryReader.ReadDouble(),
                    binaryReader.ReadDouble(),
                    binaryReader.ReadDouble(),
                    binaryReader.ReadDouble(),
                    binaryReader.ReadDouble()]);
                //monthly Rainfall
                monthlyRainfall.Add(cell, [binaryReader.ReadDouble(),
                    binaryReader.ReadDouble(),
                    binaryReader.ReadDouble(),
                    binaryReader.ReadDouble(),
                    binaryReader.ReadDouble(),
                    binaryReader.ReadDouble(),
                    binaryReader.ReadDouble(),
                    binaryReader.ReadDouble(),
                    binaryReader.ReadDouble(),
                    binaryReader.ReadDouble(),
                    binaryReader.ReadDouble(),
                    binaryReader.ReadDouble()]);
                //event lag
                rainfallEventLag.Add(cell, [binaryReader.ReadDouble(),
                    binaryReader.ReadDouble(),
                    binaryReader.ReadDouble(),
                    binaryReader.ReadDouble(),
                    binaryReader.ReadDouble(),
                    binaryReader.ReadDouble()]);

            }
        }

        //Climateregion by cell
        memoryStream.Close();
        memoryStream = new MemoryStream();
        memoryStream = HelperFunctions.GetLUT(Assembly.GetExecutingAssembly(), "FLEA_Generic_CLIMATEREGION_CELL_LUT.dat");
        using(BinaryReader binaryReader = new BinaryReader(memoryStream))
        {
            while(binaryReader.BaseStream.Position < binaryReader.BaseStream.Length)
            {
                int cell = binaryReader.ReadInt32();
                int climateRegion = binaryReader.ReadInt32();

                cellClimateRegion.Add(cell, climateRegion);
                if(!climateRegions.Contains(climateRegion))
                {
                    climateRegions.Add(climateRegion);
                }
            }
        }

    }
    /// <summary>
    /// Method to return the Easting and Northings for the centroid of each 10km grid cell in the UK as a dictionary.
    /// </summary>
    /// <returns>Dictionary indexed by cell ID of centroid easting and norhtings</returns>
    public static Dictionary<int, GridCoordinateDto> GridEastingNorthing()
    {
        return Instance.GridLocations;
    }
    /// <summary>
    /// Method to retrieve UK GHG AEIA grid information (ID, centorid easting and northing)
    /// </summary>
    /// <returns>a List of gtid locatin objects</returns>
    public static List<GridLocationDto> GetGridLocations()
    {
        List<GridLocationDto> gridLocations = new List<GridLocationDto>();

        foreach(int gridSquareID in Instance.GridLocations.Keys)
        {
            gridLocations.Add(new GridLocationDto() { GridSquareID = gridSquareID, Easting = Instance.GridLocations[gridSquareID].Easting, Northing = Instance.GridLocations[gridSquareID].Northing });
        }

        return gridLocations;
    }
    /// <summary>
    /// Method to chekc if grid square is valid
    /// </summary>
    /// <param name="gridID">UK 10km grid square ID</param>
    /// <returns>true if valid, false otherwise</returns>
    public static bool IsValidGridSquare(int gridID)
    {
        if(Instance.gridCells.Contains(gridID))
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    public static bool IsValidClimateRegion(int climateRegion)
    {
        if(Instance.climateRegions.Contains(climateRegion))
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    /// <summary>
    /// Method to retrieve monthly rainfall for a grid square
    /// </summary>
    /// <param name="month">month enumerator</param>
    /// <param name="locationID">UK 10km grid square ID</param>
    /// <returns>monthly average temperature for the grid square</returns>
    public static double RetrieveMonthlyRainfall(Month month, int locationID)
    {
        return Instance.monthlyRainfall[locationID][(int)month];
    }
    /// <summary>
    /// Retrieve annual average temperaturefor a grid square
    /// </summary>
    /// <param name="locationID">UK 10 km grid square</param>
    /// <returns>Annual average temperature for grid square</returns>
    public static double RetrieveAnnualTemperature(int locationID)
    {
        return Instance.annualTemperature[locationID];
    }
    /// <summary>
    /// Retireve probability fo rainfall event occuring in 1-6 days after application based on location
    /// </summary>
    /// <param name="locationID">UK 10 km grid square ID</param>
    /// <returns>Array of probabilities that rainfall event occurs within 6 days of application</returns>
    public static double[] RetrieveRainfallEventProbability(int locationID)
    {
        return Instance.rainfallEventLag[locationID];
    }
    /// <summary>
    /// Retrieve monthly average temperature for a given location and given month
    /// </summary>
    /// <param name="month">month enumerator</param>
    /// <param name="locationID">UK 10 km grid sqaure ID</param>
    /// <returns>Average monthly temperature for the month at the location specified</returns>
    public static double RetrieveMonthlyTemperature(Month month, int locationID)
    { return Instance.monthlyTemperature[locationID][(int)month]; }
    /// <summary>
    /// Retrieve annual average rainfall for a location
    /// </summary>
    /// <param name="locationID">UK 10 km grid square ID</param>
    /// <returns>Annual average rianfall for the 10km gri dsqaure specified</returns>
    public static double RetrieveAnnualAverageRainfall(int locationID)
    {
        return Instance.annualRainfall[locationID];
    }
    /// <summary>
    /// Retrieve climate region for cell base don 10km grid square ID
    /// </summary>
    /// <param name="locationID">the ID of the 10km grid square</param>
    /// <returns>Climateregion ID as integer</returns>
    public static int ClimateRegion(int locationID)
    {
        return Instance.cellClimateRegion[locationID];
    }
}
