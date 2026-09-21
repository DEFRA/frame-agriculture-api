using Asp.Versioning;
using FrameAgricultureApi.Components;
using FrameAgricultureApi.CustomErrorHandling;
using FrameAgricultureApi.IOClasses;
using FrameAgricultureApi.Libraries.OutdoorLivestock;
using FrameAgricultureApi.Libraries.ValidInputChecks;
using Microsoft.AspNetCore.Mvc;
using System.Net.Mime;
using static FrameAgricultureApi.Enumerators.Enumerators;

namespace FrameAgricultureApi.Controllers;
/// <summary>
/// Controller containing endpoints for emissions from excreat deposited at grazing
/// </summary>
[ApiController]
[Route("[controller]")]
[Route("api/[controller]")]
[Route("v{v:apiVersion}/[controller]")]
[ApiVersion(1.0)]
public class OutdoorLivestockController(ILogger<OutdoorLivestockController> logger) : ControllerBase
{
    /// <summary>
    /// Logger
    /// </summary>
    public ILogger<OutdoorLivestockController> Logger { get; } = logger;

    /// <summary>
    /// Calculates the emission from deposition of excreta outdoors by either pigs, poultry or minor livestock.
    ///
    /// Note that this endpoint should not be used for cattle or sheep
    /// </summary>
    /// <param name="jsonInputObject">Outdoor Excretion Inputs object that contains the sector enumerator, animal type enumerator as an integer,
    /// the total ammoniacal nitrogen in excreta deposited outdoors (kg/head/unit time), total nitrogen in excreta deposited outdoors (kg/head/unit time) and
    /// volatile solids in excreta deposited outdoors</param>
    /// <returns>Standard Output object in JSON serialisation where each Emission has a name (string), units (string) and a value (double).
    /// Further text strings provide information on the source of the emission and the stage within the source.</returns>
    /// <exception cref="CustomAppException">Provides an exception with a message explainign the error that occurred.</exception>
    [Consumes(MediaTypeNames.Application.Json)]
    [Produces(MediaTypeNames.Application.Json)]
    [ProducesResponseType(typeof(StandardOutput), StatusCodes.Status200OK)]
    [HttpPost("outdoor-excretion-emissions-otherlivestock")]
    public IActionResult OutdoorLivestockEmissions([FromBody] BaseOutdoorExcretaInputs jsonInputObject)
    {
        try
        {
            StandardOutput standardOutput = new();
            if(jsonInputObject != null)
            {
                string errors = OutdoorLivestockValidityChecks.CheckValidOutdoorLivestockInputs(jsonInputObject);
                if(errors.Equals(""))
                {
                    OutdoorLivestockEmission emissions;

                    emissions = OutdoorLivestock.OutdoorExcretaEmissions_PigsPoultryMinorLivestock(jsonInputObject.MySector, jsonInputObject.AnimalType, jsonInputObject.TotalAmmoniacalNitrogen,
                        jsonInputObject.TotalNitrogen, jsonInputObject.VolatileSolids);

                    string animal = "Not Set";
                    switch(jsonInputObject.MySector)
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
                    }
                    standardOutput = new StandardOutput(animal, "Outdoor Excretion", jsonInputObject.MySector.ToString(), emissions.ExportEmissions());

                }
                else
                {
                    throw new CustomAppException("Invalid request data:\n" + errors);
                }
            }
            return Ok(standardOutput);
        }
        catch(Exception ex) { return BadRequest(ex.Message); }
    }

    /// <summary>
    /// Calculates the emission from deposition of excreta outdoors by cattle.
    ///
    /// Note that this endpoint should only be used for cattle
    /// </summary>
    /// <param name="jsonInputObject">Outdoor Excretion Inputs object that contains the sector enumerator, animal type enumerator as an integer,
    /// the nitrogen in urine deposited outdoors (kg/head/unit time), nitrogen in dung deposited outdoors (kg/head/unit time) and
    /// volatile solids in excreta deposited outdoors</param>
    /// <returns>Standard Output object in JSON serialisation where each Emission has a name (string), units (string) and a value (double).
    /// Further text strings provide information on the source of the emission and the stage within the source.</returns>
    /// <exception cref="CustomAppException">Provides an exception with a message explainign the error that occurred.</exception>
    [Consumes(MediaTypeNames.Application.Json)]
    [Produces(MediaTypeNames.Application.Json)]
    [ProducesResponseType(typeof(StandardOutput), StatusCodes.Status200OK)]
    [HttpPost("outdoor-excretion-emissions-cattle")]
    public IActionResult OutdoorCattleEmissions([FromBody] CattleOutdoorExcretaInputs jsonInputObject)
    {
        try
        {
            StandardOutput standardOutput = new();
            if(jsonInputObject != null)
            {
                string errors = OutdoorLivestockValidityChecks.CheckValidOutdoorCattleInputs(jsonInputObject);
                if(errors.Equals(""))
                {
                    OutdoorLivestockEmission emissions;

                    emissions = OutdoorLivestock.OutdoorExcretaEmissions_Cattle(jsonInputObject.MySector, jsonInputObject.AnimalType, jsonInputObject.UrineNitrogen,
                        jsonInputObject.DungNitrogen, jsonInputObject.VolatileSolids);

                    string animal = "Not Set";
                    switch(jsonInputObject.MySector)
                    {
                        case Sector.Beef:
                            animal = ((BeefCattleType)jsonInputObject.AnimalType).ToString();
                            break;

                        case Sector.Dairy:
                            animal = ((DairyCattle)jsonInputObject.AnimalType).ToString();
                            break;
                    }
                    standardOutput = new StandardOutput(animal, "Outdoor Excretion", jsonInputObject.MySector.ToString(), emissions.ExportEmissions());

                }
                else
                {
                    throw new CustomAppException("Invalid request data:\n" + errors);
                }
            }
            return Ok(standardOutput);
        }
        catch(Exception ex) { return BadRequest(ex.Message); }
    }

    /// <summary>
    /// Calculates the emission from deposition of excreta outdoors by sheep.
    /// </summary>
    /// <param name="jsonInputObject">Outdoor Excretion Inputs object that contains the Sheep energy balance model outputs, sector enumerator, animal type enumerator as an integer,
    /// the rate of fertiliser applied to grass grazed, the appropriate grass fracleach coefficients and the appropriate grass model coefficients for the nitrogen content of the grass at grazing.</param>
    /// <returns>Standard Output object in JSON serialisation where each Emission has a name (string), units (string) and a value (double).
    /// Further text strings provide information on the source of the emission and the stage within the source.</returns>
    /// <exception cref="CustomAppException">Provides an exception with a message explainign the error that occurred.</exception>
    [Consumes(MediaTypeNames.Application.Json)]
    [Produces(MediaTypeNames.Application.Json)]
    [ProducesResponseType(typeof(StandardOutput), StatusCodes.Status200OK)]
    [HttpPost("outdoor-excretion-emissions-sheep")]
    public IActionResult OutdoorSheepEmissions([FromBody] SheepOutdoorExcretaInputs jsonInputObject)
    {
        try
        {
            StandardOutput standardOutput = new();
            if(jsonInputObject != null)
            {
                string errors = OutdoorLivestockValidityChecks.CheckValidOutdoorSheepInputs(jsonInputObject);
                if(errors.Equals(""))
                {
                    OutdoorLivestockEmission emissions;

                    emissions = OutdoorLivestock.OutdoorExcretaEmissions_Sheep(jsonInputObject.EnergyBalance, jsonInputObject.MySector, jsonInputObject.AnimalType,
                        jsonInputObject.FertiliserRate, jsonInputObject.FracLeachCoefficients, jsonInputObject.GrazingNitrogen, jsonInputObject.GeneticGrazedFracLeachScalar,
                        jsonInputObject.GeneticYieldDilutionScalar);

                    string animal = ((SheepType)jsonInputObject.AnimalType).ToString();

                    standardOutput = new StandardOutput(animal, "Outdoor Excretion", jsonInputObject.MySector.ToString(), emissions.ExportEmissions());

                }
                else
                {
                    throw new CustomAppException("Invalid request data:\n" + errors);
                }
            }
            return Ok(standardOutput);
        }
        catch(Exception ex) { return BadRequest(ex.Message); }
    }
}
