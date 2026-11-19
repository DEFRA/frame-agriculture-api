using FrameAgricultureApi.Core.Outputs;
using System.Net.Mime;
using Asp.Versioning;
using FrameAgricultureApi.ComponentLogic.CoverCropComponent;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FrameAgricultureApi.Controllers
{
    /// <summary>
    /// This is the cover crop controller summary from an XML comment
    /// </summary>
    [Route("api/cover-crops")]
    [ApiController]
    public class CoverCropsController : ControllerBase
    {
        private readonly CoverCrops _coverCrops;

        public CoverCropsController(CoverCrops coverCrops)
        {
            _coverCrops = coverCrops;
        }

        /// <summary>
        /// Calculates the emissions associated with the nitrogen returned to the soil from cover crops.
        /// </summary>
        /// <param name="jsonInputObject">A CoverCropInputs object that contains the crop type and cover crop type enumerators as integer values.</param>
        /// <returns>Cover crop output containing the crop types and nested emission values, units, pollutant and pathway metadata.</returns>
        /// <remarks>
        /// As cover crops reduce the fraction of nitrogen leached, the core calculations returns NO3-N leached and Indirect N2O-N from NO3-N leached emission with this reduction applied.
        /// To allow for the impact of the mitigation to be quantified, two additional outputs are included: the NO3-N leached and indirect N2O-N from NO3 leached emission without the reduction.
        /// 
        ///  The United Kingdom Greenhouse Gas and Ammonia Emissions Inventory for Agriculture methodology is used for the calculations.
        /// ![Calculation Diagram](/images/FLEA_CoverCrops_EquationWorkflows.jpg)
        /// </remarks>
        [Consumes(MediaTypeNames.Application.Json)]
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(typeof(CoverCropOutput), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(BadRequestResult), StatusCodes.Status400BadRequest)]
        [HttpPost("cover-crop-emissions")]
        [ApiVersion(0.0)]
        public IActionResult CoverCropEmissions([FromBody] CoverCropRequest jsonInputObject)
        {
            try
            {
                CoverCropOutput standardOutput = new();
                if (jsonInputObject != null)
                {
                    standardOutput.CoverCropType = jsonInputObject.CoverCropType;
                    standardOutput.Emissions = _coverCrops.CoverCropEmissions(jsonInputObject.CoverCropType);
                }
                return Ok(standardOutput);
            }
            catch (Exception ex) { return BadRequest(ex.Message); }
        }

    }
}