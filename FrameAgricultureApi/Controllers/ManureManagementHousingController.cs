using Asp.Versioning;
using FrameAgricultureApi.Components;
using FrameAgricultureApi.CustomErrorHandling;
using FrameAgricultureApi.IOClasses;
using FrameAgricultureApi.Libraries.Housing;
using FrameAgricultureApi.Libraries.ValidInputChecks;
using Microsoft.AspNetCore.Mvc;
using System.Net.Mime;
using static FrameAgricultureApi.Enumerators.Enumerators;

namespace FrameAgricultureApi.Controllers;
/// <summary>
/// The FLEA Housing Controller
/// </summary>
/// <param name="logger">Logger for the controller</param>
[ApiController]
[Route("[controller]")]
[Route("api/[controller]")]
[Route("v{v:apiVersion}/[controller]")]
[ApiVersion(1.0)]
public class ManureManagementHousingController(ILogger<ManureManagementHousingController> logger) : ControllerBase
{
    /// <summary>
    /// Logger
    /// </summary>
    public ILogger<ManureManagementHousingController> Logger { get; } = logger;

    /// <summary>
    /// Calculates the emission from deposition of excreta at housing for all livestock except sheep.
    /// </summary>
    /// <param name="jsonInputObject">Housing Inputs object that contains the sector enumerator, animal type enumerator as an integer,
    /// the organic matter type enumerator, total nitrogen in excreta deposited at housing (kg/head/unit time), total ammoniacal nitrogen in excreta
    /// deposited at housing (kg/head/unit time), volatile solids in excreta deposited at housing, and optionally the nitrogen in urine deposited in housing (kg/head/unit time)
    /// and the nitrogen in dung deposited in housing (kg/head/unit time)) and  the nitrogen in bedding.</param>
    /// <returns>Standard Output object in JSON serialisation where each Emission has a name (string), units (string) and a value (double).
    /// Further text strings provide information on the source of the emission and the stage within the source.</returns>
    /// <exception cref="CustomAppException">Provides an exception with a message explainign the error that occurred</exception>
    [Consumes(MediaTypeNames.Application.Json)]
    [Produces(MediaTypeNames.Application.Json)]
    [ProducesResponseType(typeof(StandardOutput), StatusCodes.Status200OK)]
    [HttpPost("manure-management-housing-emissions-livestock")]
    public IActionResult HousingEmissions([FromBody] HousingInputs jsonInputObject)
    {
        try
        {
            StandardOutput standardOutput = new();
            if(jsonInputObject != null)
            {
                string errors = HousingValidityChecks.CheckValidHousingInputs(jsonInputObject, out bool mitigated);
                if(errors.Equals(""))
                {
                    HousingEmissions emissions;

                    double beddingN = jsonInputObject.BeddingNitrogen ?? 0.0;

                    if(mitigated)
                    {
                        List<int> methods = jsonInputObject.MitigationMethods == null ? [] : [.. jsonInputObject.MitigationMethods];

                        emissions = Housing.MitigatedHousingManureManagementEmissions(jsonInputObject.Sector, jsonInputObject.AnimalType, jsonInputObject.ManureHousingSystem,
                            jsonInputObject.OrganicMatterType, jsonInputObject.TotalNitrogen, jsonInputObject.TotalAmmoniacalNitrogen, jsonInputObject.VolatileSolids, methods, beddingN);

                    }
                    else
                    {
                        emissions = Housing.UnmitigatedHousingManureManagementEmissions(jsonInputObject.Sector, jsonInputObject.AnimalType, jsonInputObject.ManureHousingSystem,
                            jsonInputObject.OrganicMatterType, jsonInputObject.TotalNitrogen, jsonInputObject.TotalAmmoniacalNitrogen, jsonInputObject.VolatileSolids, beddingN);

                    }
                    string animal = "Not Set";
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

                        case Sector.Beef:
                            animal = ((BeefCattleType)jsonInputObject.AnimalType).ToString();
                            break;

                        case Sector.Dairy:
                            animal = ((DairyCattle)jsonInputObject.AnimalType).ToString();
                            break;

                    }

                    standardOutput = new StandardOutput(animal, "Housing", jsonInputObject.ManureHousingSystem.ToString(), emissions.ExportEmissions());

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
    /// Calculates the emission from deposition of excreta at housing for sheep only.
    /// </summary>
    /// <param name="jsonInputObject">Sheep Housing Inputs object that contains the sector enumerator, animal type enumerator as an integer,
    /// and a sheep energy balance object that contains all the other parameters needed by the sheep housing calculations</param>
    /// <returns>Standard Output object in JSON serialisation where each Emission has a name (string), units (string) and a value (double).
    /// Further text strings provide information on the source of the emission and the stage within the source.</returns>
    /// <exception cref="CustomAppException">Provides an exception with a message explainign the error that occurred.</exception>
    [Consumes(MediaTypeNames.Application.Json)]
    [Produces(MediaTypeNames.Application.Json)]
    [ProducesResponseType(typeof(StandardOutput), StatusCodes.Status200OK)]
    [HttpPost("manure-management-housing-emissions-sheep")]
    public IActionResult HousingEmissions([FromBody] SheepHousingInputs jsonInputObject)
    {
        try
        {
            StandardOutput standardOutput = new();
            if(jsonInputObject != null)
            {
                string errors = HousingValidityChecks.CheckValidSheepHousingInputs(jsonInputObject, out bool mitigated);
                if(errors.Equals(""))
                {
                    HousingEmissions emissions;

                    if(mitigated)
                    {
                        List<int> methods = jsonInputObject.MitigationMethods == null ? [] : [.. jsonInputObject.MitigationMethods];

                        emissions = Housing.MitigatedHousingManureManagementEmissions_Sheep(jsonInputObject.Sector, jsonInputObject.AnimalType, jsonInputObject.SheepEnergyBalance, methods);

                    }
                    else
                    {

                        emissions = Housing.UnmitigatedHousingManureManagementEmissions_Sheep(jsonInputObject.Sector, jsonInputObject.AnimalType, jsonInputObject.SheepEnergyBalance);

                    }
                    string animal = ((SheepType)jsonInputObject.AnimalType).ToString();

                    standardOutput = new StandardOutput(animal, "Housing", ManureHousingSystem.SheepHousing.ToString(), emissions.ExportEmissions());
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
