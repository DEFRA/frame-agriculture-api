using FrameAgricultureApi.Core.Outputs;
using FrameAgricultureApi.Core.Enums;
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
        private readonly CoverCropInputLookup _coverCropInputLookup;

        public CoverCropsController(CoverCrops coverCrops, CoverCropInputLookup coverCropInputLookup)
        {
            _coverCrops = coverCrops;
            _coverCropInputLookup = coverCropInputLookup;
        }

        /// <summary>
        /// Calculates cover crop emissions using inputs and factors resolved from the lookup collections.
        /// </summary>
        /// <param name="coverCropType">The cover crop type; 0 represents all cover crops.</param>
        /// <returns>The cover crop type and calculated emissions.</returns>
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(typeof(CoverCropOutput), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
        [HttpGet("cover-crop-emissions/{coverCropType}")]
        [ApiVersion(0.0)]
        public IActionResult CoverCropEmissionsFromLookup([FromRoute] CoverCropType coverCropType)
        {
            if (!Enum.IsDefined(coverCropType))
            {
                return BadRequest($"Unsupported cover crop type '{coverCropType}'.");
            }

            try
            {
                var request = _coverCropInputLookup.CoverCropEmissionInputHelper(coverCropType);
                return Ok(new CoverCropOutput
                {
                    CoverCropType = coverCropType,
                    Emissions = _coverCrops.CoverCropEmissions(request)
                });
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(ex.Message);
            }
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
                    standardOutput.Emissions = _coverCrops.CoverCropEmissions(jsonInputObject);
                }
                return Ok(standardOutput);
            }
            catch (Exception ex) { return BadRequest(ex.Message); }
        }

    }
}