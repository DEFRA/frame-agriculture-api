using Asp.Versioning;
using FrameAgricultureApi.Components;
using FrameAgricultureApi.IOClasses;
using FrameAgricultureApi.Libraries.CoverCrops;
using Microsoft.AspNetCore.Mvc;
using System.Net.Mime;

namespace FrameAgricultureApi.Controllers;

/// <summary>
/// Controller containing endpoints for emissions from the use of cover crops
/// </summary>
/// <param name="logger">Logger</param>
[ApiController]
[Route("[controller]")]
[Route("api/[controller]")]
[Route("v{v:apiVersion}/[controller]")]
[ApiVersion(1.0)]
public class CoverCropsController(ILogger<CoverCropsController> logger) : ControllerBase
{

    /// <summary>
    /// Logger
    /// </summary>
    public ILogger<CoverCropsController> Logger { get; } = logger;

    /// <summary>
    /// Calculates the emissions associated with the nitrogen returned to the soil from cover crops.
    ///
    /// As cover crops reduce the fraction of nitrogen leached, the core calculations returns NO3-N leached and Indirect N2O-N from NO3-N leached emission with this reduction applied.
    /// To allow for the impact of the mitigation to be quantified, two additional outputs are included: the NO3-N leached and indirect N2O-N from NO3 leached emission without the reduction.
    ///
    ///  The United Kingdom Greenhouse Gas and Ammonia Emissions Inventory for Agriculture methodology is used for the calculations.
    /// </summary>
    /// <param name="jsonInputObject">A  cover crop input object that contains the crop type  and cover crop type enumerators as integer values.</param>
    /// <returns>Standard Output object in Json serialisation, where each Emission has a name (string), units (string) and a value (double).
    /// Further text strings provide information on the source of the emission and the stage within the source.</returns>
    [Consumes(MediaTypeNames.Application.Json)]
    [Produces(MediaTypeNames.Application.Json)]
    [ProducesResponseType(typeof(StandardOutput), StatusCodes.Status200OK)]
    [HttpPost("cover-crop-emissions")]
    [ApiVersion(1.0)]
    public IActionResult CoverCropEmissions([FromBody] CoverCropInputs jsonInputObject)
    {
        try
        {
            StandardOutput standardOutput = new();
            if(jsonInputObject != null)
            {
                CoverCropEmissions emissions = CoverCrops.CoverCropEmissions(jsonInputObject.CoverCropType);
                standardOutput = new StandardOutput("Cover Crops", jsonInputObject.CropType.ToString(), "Crop Production", emissions.ExportEmissions());
            }
            return Ok(standardOutput);
        }
        catch(Exception ex) { return BadRequest(ex.Message); }
    }
}
