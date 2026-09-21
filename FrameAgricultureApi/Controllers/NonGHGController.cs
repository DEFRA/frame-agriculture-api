
using Asp.Versioning;
using FrameAgricultureApi.Components;
using FrameAgricultureApi.IOClasses;
using FrameAgricultureApi.Libraries.NonGHG;
using Microsoft.AspNetCore.Mvc;
using System.Net.Mime;
using static FrameAgricultureApi.Enumerators.Enumerators;

namespace FrameAgricultureApi.Controllers;

/// <summary>
/// The FLEA API Gateway Controller
/// </summary>
/// <param name="logger">Logger for the gateway</param>
[ApiController]
[Route("[controller]")]
[Route("api/[controller]")]
[Route("v{v:apiVersion}/[controller]")]
[ApiVersion(1.0)]
[ApiExplorerSettings(IgnoreApi = true)]
public class NonGHGController(ILogger<NonGHGController> logger) : ControllerBase
{
    /// <summary>
    /// Logger
    /// </summary>
    public ILogger<NonGHGController> Logger { get; } = logger;

    ///<summary>
    /// Calculates non-GHG emissions from crop production.
    ///
    /// The non-GHG emissions include emissions of NMVOCs and particulate matter (2.5 micron and 10 micron).
    /// Where relevant these are provided for key activities such as cultivation, harvesting, cleaning and drying.
    ///
    /// The United Kingdom Greenhouse Gas and Ammonia Emissions Inventory for Agriculture methodology is used for the calculations.
    ///
    /// </summary>
    /// <param name="jsonInputObject">A  crop non-GHG input object that contains the crop type enumerator as an integer value.</param>
    /// <returns>Standard Output object in Json serialisation, where each Emission has a name (string), units (string) and a value (double).
    /// Further text strings provide information on the source of the emission and the stage within the source.</returns>
    [Consumes(MediaTypeNames.Application.Json)]
    [Produces(MediaTypeNames.Application.Json)]
    [ProducesResponseType(typeof(StandardOutput), StatusCodes.Status200OK)]
    [HttpPost("non-ghg-emissions-crop")]
    public IActionResult NonGHGEmissions([FromBody] NonGHGInputsCrop jsonInputObject)
    {
        try
        {
            StandardOutput standardOutput = new();
            if(jsonInputObject != null)
            {
                NonGHGCropGrassEmissions emissions = NonGHGCropGrassLivestock.NonGHG_Crop(jsonInputObject.CropType);
                standardOutput = new StandardOutput("Crop Operations", jsonInputObject.CropType.ToString(), "Crop Production", emissions.ExportEmissions());
            }

            return Ok(standardOutput);
        }
        catch(Exception ex) { return BadRequest(ex.Message); }
    }

    ///<summary>
    /// Calculates non-GHG emissions from livestock (non-cattle) production.
    ///
    /// The United Kingdom Greenhouse Gas and Ammonia Emissions Inventory for Agriculture methodology is used for the calculations.
    /// Livestock non-GHG emissions includes emissions from silage storage and feed as well as housing, storage and spreading
    /// </summary>
    /// <param name="jsonInputObject">A  crop non-GHG input object that contains the crop type enumerator as an integer value.</param>
    /// <returns>Standard Output object in Json serialisation, where each Emission has a name (string), units (string) and a value (double).
    /// Further text strings provide information on the source of the emission and the stage within the source.</returns>
    [Consumes(MediaTypeNames.Application.Json)]
    [Produces(MediaTypeNames.Application.Json)]
    [ProducesResponseType(typeof(StandardOutput), StatusCodes.Status200OK)]
    [HttpPost("non-ghg-emissions-otherlivestock")]
    public IActionResult NonGHGEmissionsLivestock([FromBody] NonGHGInputsLivestock_NonCattle jsonInputObject)
    {
        try
        {
            StandardOutput standardOutput = new();
            if(jsonInputObject != null)
            {

                NonGHGLivestockEmissions emissions = NonGHGCropGrassLivestock.NonGHG_Livestock_notCattle(jsonInputObject.Sector, jsonInputObject.AnimalType, jsonInputObject.VolatileSolidsExcretion, jsonInputObject.HouseDays,
                    jsonInputObject.AmmoniaHousing, jsonInputObject.AmmoniaStorage, jsonInputObject.AmmoniaSpreading);
                string animal = string.Empty;

                switch(jsonInputObject.Sector)
                {
                    case Sector.MinorLivestock:
                        animal = ((MinorLivestockType)jsonInputObject.AnimalType).ToString();
                        break;

                    case Sector.Pigs:
                        animal = ((PigType)jsonInputObject.AnimalType).ToString();
                        break;

                    case Sector.Poultry:
                        animal = ((PoultryType)jsonInputObject.AnimalType).ToString();
                        break;

                    case Sector.Sheep:
                        animal = ((SheepType)jsonInputObject.AnimalType).ToString();
                        break;

                }
                standardOutput = new StandardOutput(animal, jsonInputObject.Sector.ToString(), "Non-GHG EMissions", emissions.ExportEmissions());
            }

            return Ok(standardOutput);
        }
        catch(Exception ex) { return BadRequest(ex.Message); }
    }

    ///<summary>
    /// Calculates non-GHG emissions from cattle production.
    ///
    /// The United Kingdom Greenhouse Gas and Ammonia Emissions Inventory for Agriculture methodology is used for the calculations.
    /// Livestock non-GHG emissions includes emissions from silage storage and feed as well as housing, storage and spreading
    /// </summary>
    /// <param name="jsonInputObject">A  crop non-GHG input object that contains the crop type enumerator as an integer value.</param>
    /// <returns>Standard Output object in Json serialisation, where each Emission has a name (string), units (string) and a value (double).
    /// Further text strings provide information on the source of the emission and the stage within the source.</returns>
    [Consumes(MediaTypeNames.Application.Json)]
    [Produces(MediaTypeNames.Application.Json)]
    [ProducesResponseType(typeof(StandardOutput), StatusCodes.Status200OK)]
    [HttpPost("non-ghg-emissions-cattle")]
    public IActionResult NonGHGEmissionsDairyBeef([FromBody] NonGHGInputsLivestock_Cattle jsonInputObject)
    {
        try
        {
            StandardOutput standardOutput = new();
            if(jsonInputObject != null)
            {

                NonGHGLivestockEmissions emissions = NonGHGCropGrassLivestock.NonGHG_DairyBeef(jsonInputObject.Sector, jsonInputObject.AnimalType, jsonInputObject.MonthlyGrossEnergyIntake, jsonInputObject.PercentTimeHousingYards,
                    jsonInputObject.MonthlyAmmoniaHousing, jsonInputObject.MonthlyAmmoniaStorage, jsonInputObject.MonthlyAmmoniaSpreading);
                string animal = string.Empty;

                switch(jsonInputObject.Sector)
                {
                    case Sector.Dairy:
                        animal = ((DairyCattle)jsonInputObject.AnimalType).ToString();
                        break;

                    case Sector.Beef:
                        animal = ((BeefCattleType)jsonInputObject.AnimalType).ToString();
                        break;

                }
                standardOutput = new StandardOutput(animal, jsonInputObject.Sector.ToString(), "Non-GHG EMissions", emissions.ExportEmissions());
            }

            return Ok(standardOutput);
        }
        catch(Exception ex) { return BadRequest(ex.Message); }
    }
}
