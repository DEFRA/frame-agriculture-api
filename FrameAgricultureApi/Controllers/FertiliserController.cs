
using Asp.Versioning;
using FrameAgricultureApi.Components;
using FrameAgricultureApi.CustomErrorHandling;
using FrameAgricultureApi.IOClasses;
using FrameAgricultureApi.Libraries.Fertiliser;
using FrameAgricultureApi.Libraries.ValidInputChecks;
using Microsoft.AspNetCore.Mvc;
using System.Net.Mime;

namespace FrameAgricultureApi.Controllers;
/// <summary>
/// Fertiliser Controller
/// </summary>
/// <param name="logger">Logger for the fertiliser controller</param>
[ApiController]
[Route("[controller]")]
[Route("api/[controller]")]
[Route("v{v:apiVersion}/[controller]")]
[ApiVersion(1.0)]
public class FertiliserController(ILogger<FertiliserController> logger) : ControllerBase
{

    /// <summary>
    /// Logger
    /// </summary>
    public ILogger<FertiliserController> Logger { get; } = logger;

    /// <summary>
    /// Fertiliser Emissions Endpoint
    /// Calculates emissions from fertiliser applications by fertiliser type by month on a per hectare basis.
    ///
    /// The United Kingdom Greenhouse Gas and Ammonia Emissions Inventory for Agriculture methodology is used for the calculations.
    /// </summary>
    /// <param name="jsonInputObject">A Json Serialisation of the FertiliserInput object that contains: fertiliser type as integer,
    /// the total amount of nitrogen applied as inorganic fertiliser, the percentage of this total application that this fertiliser type represents,
    /// , the UK 10km grid square ID code as integer, a booloean indicating if the soil pH is less than 7 (i.e. non-alkaline or peaty soils),
    /// a double array of length 12, with the kg/ha of N of this fertiliser applied per month from January to December,
    /// an optional integer list (array) of mitigation methods applied.</param>
    /// <returns>Standard Output object in Json serialisation, where each Emission has a name (string), units (string) and a value (double).
    /// Further text strings provide information on the source of the emission and the stage within the source.</returns>
    [Consumes(MediaTypeNames.Application.Json)]
    [Produces(MediaTypeNames.Application.Json)]
    [ProducesResponseType(typeof(StandardOutput), StatusCodes.Status200OK)]
    [HttpPost("fertiliser-to-crop-emissions")]
    public IActionResult FertiliserEmissions([FromBody] FertiliserInput jsonInputObject)
    {
        try
        {
            StandardOutput standardOutput = new();
            if(jsonInputObject != null)
            {
                string errors = FertiliserValidityChecks.CheckValidInputs(jsonInputObject, out bool mitigated);
                if(errors.Equals(""))
                {
                    FertiliserEmissions fertiliserEmissions;
                    List<int> methods = jsonInputObject.MitigationMethods == null ? [] : [.. jsonInputObject.MitigationMethods];
                    if(mitigated)
                    {
                        ; //must be valid integer array as passed validity check
                        fertiliserEmissions = FertiliserApplication.MitigatedEmissionsFertiliser(jsonInputObject.FertiliserType, jsonInputObject.FertiliserApplications,
                        jsonInputObject.TotalNApplied, jsonInputObject.PercentageFertiliserType, jsonInputObject.GridSquare, jsonInputObject.AcidicSoil, methods);
                    }
                    else
                    {
                        fertiliserEmissions = FertiliserApplication.UnmitigatedEmissionsFertiliser(jsonInputObject.FertiliserType, jsonInputObject.FertiliserApplications,
                            jsonInputObject.TotalNApplied, jsonInputObject.PercentageFertiliserType, jsonInputObject.GridSquare, jsonInputObject.AcidicSoil);
                    }

                    standardOutput = new StandardOutput("Fertiliser Application", jsonInputObject.FertiliserType.ToString(), "Fertilising",
                        fertiliserEmissions.ExportEmissions());
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
    /// Fertiliser Emissions To Grass Endpoint
    /// Calculates emissions from fertiliser applications to Grass by fertiliser type by month on a per hectare basis.
    ///
    /// The United Kingdom Greenhouse Gas and Ammonia Emissions Inventory for Agriculture methodology is used for the calculations.
    /// </summary>
    /// <param name="jsonInputObject">A Json Serialisation of the GrassFertiliserInput object that contains: fertiliser type as fertiliser type enum integer,
    /// the total amount of nitrogen applied as inorganic fertiliser, the percentage of this total application that this fertiliser type represents,
    /// , the UK 10km grid square ID code as integer, the soil type, the grass type (temporary or permanent), the grass use type (cut,
    /// grazed, or cut and grazed), a boolean indicating if the grass was sown with clover, a boolean indicating if the grass receives managed manure,
    /// a booloean indicating if the soil pH is less than 7 (i.e. non-alkaline or peaty soils),
    /// a double array of length 12, with the kg/ha of N of this fertiliser applied per month from January to December,
    /// an optional integer list (array) of mitigation methods applied and an optional boolean to indicate if unmitigated emissions are required as well as mitigated emissions.</param>
    /// <returns>Standard Output object in Json serialisation, where each Emission has a name (string), units (string) and a value (double).
    /// Further text strings provide information on the source of the emission and the stage within the source.</returns>
    [Consumes(MediaTypeNames.Application.Json)]
    [Produces(MediaTypeNames.Application.Json)]
    [ProducesResponseType(typeof(StandardOutput), StatusCodes.Status200OK)]
    [HttpPost("fertiliser-to-grass-emissions")]
    public IActionResult FertiliserToGrassEmissions([FromBody] GrassFertiliserInput jsonInputObject)
    {
        try
        {
            StandardOutput standardOutput = new();
            if(jsonInputObject != null)
            {
                string errors = FertiliserValidityChecks.CheckValidInputs(jsonInputObject, out bool mitigated);
                if(errors.Equals(""))
                {
                    FertiliserEmissions fertiliserEmissions;
                    List<int> methods = jsonInputObject.MitigationMethods == null ? [] : [.. jsonInputObject.MitigationMethods];
                    if(mitigated)
                    {
                        ; //must be valid integer array as passed validity check
                        fertiliserEmissions = FertiliserApplicationToGrass.MitigatedEmissionsFertiliser(jsonInputObject.FertiliserType, jsonInputObject.FertiliserApplications,
                        jsonInputObject.TotalNApplied, jsonInputObject.PercentageFertiliserType, jsonInputObject.GridSquare, jsonInputObject.SoilType, jsonInputObject.GrassType,
                        jsonInputObject.GrassUseType, jsonInputObject.SownWithClover, jsonInputObject.ReceivesManagedManure, jsonInputObject.AcidicSoil, methods);
                    }
                    else
                    {
                        fertiliserEmissions = FertiliserApplicationToGrass.UnmitigatedEmissionsFertiliser(jsonInputObject.FertiliserType, jsonInputObject.FertiliserApplications,
                            jsonInputObject.TotalNApplied, jsonInputObject.PercentageFertiliserType, jsonInputObject.GridSquare, jsonInputObject.SoilType, jsonInputObject.GrassType,
                        jsonInputObject.GrassUseType, jsonInputObject.SownWithClover, jsonInputObject.ReceivesManagedManure, jsonInputObject.AcidicSoil);
                    }

                    standardOutput = new StandardOutput("Fertiliser Application To Grass", jsonInputObject.FertiliserType.ToString(), "Fertilising",
                        fertiliserEmissions.ExportEmissions());
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
