
using Asp.Versioning;
using FrameAgricultureApi.Components;
using FrameAgricultureApi.CustomErrorHandling;
using FrameAgricultureApi.IOClasses;
using FrameAgricultureApi.Libraries.ValidInputChecks;
using FrameAgricultureApi.Libraries.YardExcretionLivestock;
using Microsoft.AspNetCore.Mvc;
using System.Net.Mime;
using static FrameAgricultureApi.Enumerators.Enumerators;

namespace FrameAgricultureApi.Controllers;

/// <summary>
/// The FLEA Yard Controller
/// </summary>
/// <param name="logger">Logger for the controller</param>
[ApiController]
[Route("[controller]")]
[Route("api/[controller]")]
[Route("v{v:apiVersion}/[controller]")]
[ApiVersion(1.0)]
public class ManureManagementYardController(ILogger<ManureManagementYardController> logger) : ControllerBase
{

    /// <summary>
    /// Logger
    /// </summary>
    public ILogger<ManureManagementYardController> Logger { get; } = logger;

    /// <summary>
    /// Calculates the emission from deposition of excreta on yards
    ///
    /// Note that this endpoint is for cattle only
    /// </summary>
    /// <param name="jsonInputObject">Yard Inputs object that contains the sector enumerator, animal type enumerator as an integer,
    /// total nitrogen in excreta deposited on yards (kg/head/unit time), total ammoniacal nitrogen in excreta deposited on yards(kg/head/unit time),
    /// volatile solids in excreta deposited om yards, a boolean indicating if the yard is a collecting yard and an optional list of mitigation method ids. </param>
    /// <returns>Standard Output object in JSON serialisation where each Emission has a name (string), units (string) and a value (double).
    /// Further text strings provide information on the source of the emission and the stage within the source.</returns>
    /// <exception cref="CustomAppException">Provides an exception with a message explainign the error that occurred.</exception>
    [Consumes(MediaTypeNames.Application.Json)]
    [Produces(MediaTypeNames.Application.Json)]
    [ProducesResponseType(typeof(StandardOutput), StatusCodes.Status200OK)]
    [HttpPost("manure-management-yard-emissions")]
    public IActionResult YardEmissions([FromBody] YardInputs jsonInputObject)
    {
        try
        {
            StandardOutput standardOutput = new();
            if(jsonInputObject != null)
            {
                string errors = YardValidityChecks.CheckValidYardInputs(jsonInputObject, out bool mitigated);
                if(errors.Equals(""))
                {
                    YardEmissions emissions;

                    if(mitigated)
                    {
                        List<int> methods = jsonInputObject.MitigationMethods == null ? [] : [.. jsonInputObject.MitigationMethods];

                        emissions = YardExcretion.MitigatedYardExcretionEmissions(jsonInputObject.Sector, jsonInputObject.AnimalType, jsonInputObject.TotalNitrogen,
                            jsonInputObject.TotalAmmoniacalNitrogen, jsonInputObject.VolatileSolids, jsonInputObject.CollectingYard, methods);

                    }
                    else
                    {
                        emissions = YardExcretion.UnmitigatedYardExcretionEmissions(jsonInputObject.Sector, jsonInputObject.AnimalType, jsonInputObject.TotalNitrogen,
                              jsonInputObject.TotalAmmoniacalNitrogen, jsonInputObject.VolatileSolids, jsonInputObject.CollectingYard);
                    }
                    string animal = "Not Set";
                    switch(jsonInputObject.Sector)
                    {

                        case Sector.Beef:
                            animal = ((BeefCattleType)jsonInputObject.AnimalType).ToString();
                            break;

                        case Sector.Dairy:
                            animal = ((DairyCattle)jsonInputObject.AnimalType).ToString();
                            break;

                    }

                    standardOutput = new StandardOutput(animal, "Yards", jsonInputObject.CollectingYard ? "Collecting Yard" : "Feeding Yard", emissions.ExportEmissions());

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
