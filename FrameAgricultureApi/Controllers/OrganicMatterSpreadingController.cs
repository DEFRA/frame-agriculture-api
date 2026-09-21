using Asp.Versioning;
using FrameAgricultureApi.Components;
using FrameAgricultureApi.CustomErrorHandling;
using FrameAgricultureApi.IOClasses;
using FrameAgricultureApi.Libraries.OrganicMatterApplication;
using FrameAgricultureApi.Libraries.ValidInputChecks;
using Microsoft.AspNetCore.Mvc;
using System.Net.Mime;
using static FrameAgricultureApi.Enumerators.Enumerators;

namespace FrameAgricultureApi.Controllers;
/// <summary>
/// Controller containing endpoints for emissions from spreading of organic matter and manure
/// </summary>
[ApiController]
[Route("[controller]")]
[Route("api/[controller]")]
[Route("v{v:apiVersion}/[controller]")]
[ApiVersion(1.0)]
public class OrganicMatterSpreadingController(ILogger<OrganicMatterSpreadingController> logger) : ControllerBase
{
    /// <summary>
    /// Logger
    /// </summary>
    public ILogger<OrganicMatterSpreadingController> Logger { get; } = logger;

    /// <summary>
    /// Calculates the emissions from the application of non-manure based organic matter to land.
    ///
    /// The United Kingdom Greenhouse Gas and Ammonia Emissions Inventory for Agriculture methodology is used for the calculations.
    /// </summary>
    /// <param name="jsonInputObject">An organic matter spreading input object that contains the organic matter type enumerator as an integer, the quantity of organic matter applied per hectare in kg,
    /// an enum indicating if applied to arable land (2) or grass land (1) or not set (0). Optional parameters are available to provide the nitrogen content (kg per kg) and the percentage of N that is TAN</param>
    /// <returns>Standard Output object in JSON serialisation where each Emission has a name (string), units (string) and a value (double).
    /// Further text strings provide information on the source of the emission and the stage within the source.</returns>
    [Consumes(MediaTypeNames.Application.Json)]
    [Produces(MediaTypeNames.Application.Json)]
    [ProducesResponseType(typeof(StandardOutput), StatusCodes.Status200OK)]
    [HttpPost("manure-management-spreading-emissions-organicmatter")]
    public IActionResult OrganicMatterSpreadingEmissions([FromBody] OrganicMatterApplicationInputs jsonInputObject)
    {
        try
        {
            StandardOutput standardOutput = new();
            if(jsonInputObject != null)
            {
                if(jsonInputObject.LandAppliedTo.Equals(0))
                {
                    throw new CustomAppException("You must specify a land use to which the manure is applied.");
                }
                string errors = OrganicMatterApplicationValidityChecks.CheckValidInputs(jsonInputObject, out bool mitigated);
                if(errors.Equals(""))
                {
                    OrganicMatterSpreadingEmission emissions;
                    if(mitigated)
                    {

                        List<int> methods = jsonInputObject.MitigationMethods == null ? [] : [.. jsonInputObject.MitigationMethods];
                        emissions = OrganicMatterSpreading.MitigatedOrganicMatterSpreadingEmissions(jsonInputObject.OrganicMatterType, (double)jsonInputObject.QuantityApplied,
                                              jsonInputObject.LandAppliedTo, methods, jsonInputObject.Month,
                                              (jsonInputObject.ContentN == null ? double.NaN : (double)jsonInputObject.ContentN), (jsonInputObject.PercentTAN == null ? double.NaN : (double)jsonInputObject.PercentTAN));
                    }
                    else
                    {
                        emissions = OrganicMatterSpreading.UnmitigatedOrganicMatterSpreadingEmissions(jsonInputObject.OrganicMatterType, (double)jsonInputObject.QuantityApplied,
                        jsonInputObject.LandAppliedTo, jsonInputObject.Month,
                        (jsonInputObject.ContentN == null ? double.NaN : (double)jsonInputObject.ContentN), (jsonInputObject.PercentTAN == null ? double.NaN : (double)jsonInputObject.PercentTAN));
                    }

                    standardOutput = new StandardOutput("Organic Matter", jsonInputObject.OrganicMatterType.ToString(), "Organic Matter Spreading", emissions.ExportEmissions());

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
    /// Calculates the emissions from the application of livestock manures (excluding sheep) to land
    ///
    /// The United Kingdom Greenhouse Gas and Ammonia Emissions Inventory for Agriculture methodology is used for the calculations.
    /// </summary>
    /// <param name="jsonInputObject">A livestock manure application input object that contains the organic matter type enumerator as an integer,
    /// the land use applied to,  the total nitrogen content of manure applied (kg per hectare) and the total available nitrogen applied (kg per hectare).
    /// Mitigation methods, if used are provided as a list of IDs</param>
    /// <returns>Standard Output object in JSON serialisation where each Emission has a name (string), units (string) and a value (double).
    /// Further text strings provide information on the source of the emission and the stage within the source.</returns>
    [Consumes(MediaTypeNames.Application.Json)]
    [Produces(MediaTypeNames.Application.Json)]
    [ProducesResponseType(typeof(StandardOutput), StatusCodes.Status200OK)]
    [HttpPost("manure-management-spreading-emissions-livestockmanure")]
    public IActionResult LivestockManureSpreadingEmissions([FromBody] LivestockManureApplicationInputs jsonInputObject)
    {
        try
        {
            StandardOutput standardOutput = new();
            if(jsonInputObject != null)
            {

                if(jsonInputObject.LandAppliedTo.Equals(SpreadingLandUse.NotSet))
                {
                    throw new CustomAppException("You must specify a land use to which the manure is applied.");
                }
                string errors = OrganicMatterApplicationValidityChecks.CheckValidInputsLivestock(jsonInputObject, out bool mitigated);
                if(errors.Equals(""))
                {
                    OrganicMatterSpreadingEmission emissions;
                    if(mitigated)
                    {

                        List<int> methods = jsonInputObject.MitigationMethods == null ? [] : [.. jsonInputObject.MitigationMethods];
                        emissions = OrganicMatterSpreading.MitigatedManureSpreadingEmissionsLivestock(jsonInputObject.OrganicMatterType,
                                              jsonInputObject.LandAppliedTo, methods, (jsonInputObject.Month == null ? Month.NotSet : (Month)jsonInputObject.Month), jsonInputObject.TotalN, jsonInputObject.TAN);
                    }
                    else
                    {
                        emissions = OrganicMatterSpreading.UnmitigatedManureSpreadingEmissionsLivestock(jsonInputObject.OrganicMatterType,
                                              jsonInputObject.LandAppliedTo, (jsonInputObject.Month == null ? Month.NotSet : (Month)jsonInputObject.Month), jsonInputObject.TotalN, jsonInputObject.TAN);
                    }

                    standardOutput = new StandardOutput("Manure", jsonInputObject.OrganicMatterType.ToString(), "Livestock Manure Spreading", emissions.ExportEmissions());

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
    /// Calculates the emissions from the application of sheep manure to grass only.
    /// [It is assumed that sheep manure is not spread on arable crops.]
    ///
    /// The United Kingdom Greenhouse Gas and Ammonia Emissions Inventory for Agriculture methodology is used for the calculations.
    /// </summary>
    /// <param name="jsonInputObject">A livestock manure application input object that contains the organic matter type enumerator as an integer,
    /// the land use applied to,  the total nitrogen content of manure applied (kg per hectare) and the total available nitrogen applied (kg per hectare).
    /// Mitigation methods, if used are provided as a list of IDs</param>
    /// <returns>Standard Output object in JSON serialisation where each Emission has a name (string), units (string) and a value (double).
    /// Further text strings provide information on the source of the emission and the stage within the source.</returns>
    [Consumes(MediaTypeNames.Application.Json)]
    [Produces(MediaTypeNames.Application.Json)]
    [ProducesResponseType(typeof(StandardOutput), StatusCodes.Status200OK)]
    [HttpPost("manure-management-spreading-emissions-sheepmanure")]
    public IActionResult SheepManureSpreadingEmissions([FromBody] SheepManureApplicationInputs jsonInputObject)
    {
        try
        {
            StandardOutput standardOutput = new();
            if(jsonInputObject != null)
            {
                string errors = OrganicMatterApplicationValidityChecks.CheckValidInputsSheep(jsonInputObject, out bool mitigated);
                if(errors.Equals(""))
                {
                    OrganicMatterSpreadingEmission emissions;
                    if(mitigated)
                    {

                        List<int> methods = jsonInputObject.MitigationMethods == null ? [] : [.. jsonInputObject.MitigationMethods];
                        emissions = OrganicMatterSpreading.MitigatedManureSpreadingEmissionSheep(jsonInputObject.TotalN, jsonInputObject.TAN, methods, jsonInputObject.FristWinterManureFracLeach,
                            jsonInputObject.ManureFracLeachCoefficients, jsonInputObject.GrassFetiliserRate, (jsonInputObject.Month == null ? Month.NotSet : (Month)jsonInputObject.Month),
                            jsonInputObject.GeneticManuredFracLeachScalar ?? 1.0);
                    }
                    else
                    {
                        emissions = OrganicMatterSpreading.UnmitigatedManureSpreadingEmissionSheep(jsonInputObject.TotalN, jsonInputObject.TAN, jsonInputObject.FristWinterManureFracLeach,
                            jsonInputObject.ManureFracLeachCoefficients, jsonInputObject.GrassFetiliserRate, (jsonInputObject.Month == null ? Month.NotSet : (Month)jsonInputObject.Month),
                            jsonInputObject.GeneticManuredFracLeachScalar ?? 0.0);
                    }

                    standardOutput = new StandardOutput("Manure", OrganicMatterType.SheepFYM.ToString(), "Livestock Manure Spreading", emissions.ExportEmissions());

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
