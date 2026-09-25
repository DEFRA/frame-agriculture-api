using System.Net.Mime;
using Asp.Versioning;
using FrameAgricultureApi.Libraries.CoverCrops;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FrameAgricultureApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CoverCropsController : ControllerBase
    {

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