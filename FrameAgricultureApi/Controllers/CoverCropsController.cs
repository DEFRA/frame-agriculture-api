using System.Net.Mime;
using Asp.Versioning;
using FrameAgricultureApi.Libraries.CoverCrops;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FrameAgricultureApi.Controllers
{
    /// <summary>
    /// This is the cover crop controller summary from an XML comment
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class CoverCropsController : ControllerBase
    {

        /// <summary>
        /// Calculates the emissions associated with the nitrogen returned to the soil from cover crops.
        /// </summary>
        /// <param name="jsonInputObject">A CoverCropInputs object that contains the crop type and cover crop type enumerators as integer values.</param>
        /// <returns>Standard Output object in Json serialisation, where each Emission has a name (string), units (string) and a value (double).
        /// Further text strings provide information on the source of the emission and the stage within the source.</returns>
        /// <remarks>
        /// As cover crops reduce the fraction of nitrogen leached, the core calculations returns NO3-N leached and Indirect N2O-N from NO3-N leached emission with this reduction applied.
        /// To allow for the impact of the mitigation to be quantified, two additional outputs are included: the NO3-N leached and indirect N2O-N from NO3 leached emission without the reduction.
        /// 
        ///  The United Kingdom Greenhouse Gas and Ammonia Emissions Inventory for Agriculture methodology is used for the calculations.
        /// ![Calculation Diagram](http://localhost:8085/images/FLEA_CoverCrops_EquationWorkflows.jpg)
        /// </remarks>
        [Consumes(MediaTypeNames.Application.Json)]
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(typeof(StandardOutput), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(BadRequestResult), StatusCodes.Status400BadRequest)]
        [HttpPost("cover-crop-emissions")]
        [ApiVersion(0.0)]
        public IActionResult CoverCropEmissions([FromBody] CoverCropRequest jsonInputObject)
        {
            try
            {
                StandardOutput standardOutput = new();
                if (jsonInputObject != null)
                {
                    CoverCropEmissions emissions = CoverCrops.CoverCropEmissions(jsonInputObject.CoverCropType);
                    standardOutput = new StandardOutput("Cover Crops", jsonInputObject.CropType.ToString(), "Crop Production", emissions.ExportEmissions());
                }
                return Ok(standardOutput);
            }
            catch (Exception ex) { return BadRequest(ex.Message); }
        }

    }
}