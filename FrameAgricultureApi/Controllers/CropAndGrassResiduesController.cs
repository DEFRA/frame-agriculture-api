using Asp.Versioning;
using FrameAgricultureApi.Components;
using FrameAgricultureApi.CustomErrorHandling;
using FrameAgricultureApi.IOClasses;
using FrameAgricultureApi.Libraries.CropResidues;
using FrameAgricultureApi.Libraries.GrassResidues;
using FrameAgricultureApi.Libraries.ValidInputChecks;
using Microsoft.AspNetCore.Mvc;
using System.Net.Mime;

namespace FrameAgricultureApi.Controllers;

/// <summary>
/// Controller containgin endpoints for calculatino of emissions from residue decomposition in crops and grass
/// </summary>
/// <param name="logger">Logger</param>
[ApiController]
[Route("[controller]")]
[Route("api/[controller]")]
[Route("v{v:apiVersion}/[controller]")]
[ApiVersion(1.0)]
public class CropAndGrassResiduesController(ILogger<CropAndGrassResiduesController> logger) : ControllerBase
{

    /// <summary>
    /// Logger
    /// </summary>
    public ILogger<CropAndGrassResiduesController> Logger { get; } = logger;

    /// <summary>
    /// Calculates emissions from crop residues by crop type on a per hectare basis using the IPCC method.
    ///
    /// The United Kingdom Greenhouse Gas and Ammonia Emissions Inventory for Agriculture methodology is used for the calculations.
    /// </summary>
    /// <param name="jsonInputObject">A crop residue inputs (IPCC) object serialised to Json</param>
    /// <returns>Standard Output object in Json serialisation, where each Emission has a name (string), units (string) and a value (double).
    /// Further text strings provide information on the source of the emission and the stage within the source.</returns>
    [Consumes(MediaTypeNames.Application.Json)]
    [Produces(MediaTypeNames.Application.Json)]
    [ProducesResponseType(typeof(StandardOutput), StatusCodes.Status200OK)]
    [HttpPost("crop-residue-emissions-ipcc")]
    public IActionResult CropResidueEmissions([FromBody] CropResidueInputs_IPCC jsonInputObject)
    {
        try
        {
            StandardOutput standardOutput = new();
            if(jsonInputObject != null)
            {
                string errors = CropResidueValidityChecks.CheckValidInputs(jsonInputObject, out bool mitigated);
                if(errors.Equals(""))
                {
                    CropResidueEmissions residueEmissions;

                    if(mitigated)
                    {
                        List<int> methods = jsonInputObject.MitigationMethods == null ? [] : [.. jsonInputObject.MitigationMethods];
                        residueEmissions = CropResidues.MitigatedCropResidueEmissions_IPCC(jsonInputObject.YieldFreshWeight,
                            jsonInputObject.Incorporated, jsonInputObject.CropType, methods,
                            jsonInputObject.CropDryMatterContent, jsonInputObject.SlopeIPCC, jsonInputObject.InterceptIPCC);
                    }
                    else
                    {
                        residueEmissions = CropResidues.UnmitigatedCropResidueEmissions_IPCC(jsonInputObject.YieldFreshWeight,
                            jsonInputObject.Incorporated, jsonInputObject.CropType,
                            jsonInputObject.CropDryMatterContent, jsonInputObject.SlopeIPCC, jsonInputObject.InterceptIPCC);
                        ;
                    }

                    standardOutput = new StandardOutput("Crop Residues", jsonInputObject.CropType.ToString(), "Residues",
                    residueEmissions.ExportEmissions());
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
    /// Grass emissions from renewal via reseeding or ploughing
    /// Calculates emissions from grass renewal on a per hectare basis.
    ///
    /// NOTE: This should only be used for the area of grass that is being reseeded or ploughed for renewal in the current year.
    /// The method is not applicable to grass that is not being renewed or reseeded.
    ///
    /// The United Kingdom Greenhouse Gas and Ammonia Emissions Inventory for Agriculture methodology is used for the calculations.
    /// </summary>
    /// <param name="jsonInputObject">A Json Serialisation of the GrassProductionandRenewalInput object that contains:
    /// the total amount of N applied as inorganic fertiliser, the UK 10km grid square ID code as integer, the soil type as integer, the grass type as integer, the grass use type (cut,
    /// grazed, or cut and grazed) as integer, a boolean indicating if the grass was sown with clover, a boolean indicating if the grass receives managed manure and
    /// an optional integer list (array) of mitigation methods applied.</param>
    /// <returns>Standard Output object in Json serialisation, where each Emission has a name (string), units (string) and a value (double).
    /// Further text strings provide information on the source of the emission and the stage within the source.</returns>
    [Consumes(MediaTypeNames.Application.Json)]
    [Produces(MediaTypeNames.Application.Json)]
    [ProducesResponseType(typeof(StandardOutput), StatusCodes.Status200OK)]
    [HttpPost("grass-renewal-residue-emissions")]
    public IActionResult GrassRenewal([FromBody] GrassProductionandRenewalInput jsonInputObject)
    {
        try
        {
            StandardOutput standardOutput = new();
            if(jsonInputObject != null)
            {
                string errors = GrassResidueValidityChecks.CheckValidInputs(jsonInputObject, out bool mitigated);
                if(errors.Equals(""))
                {
                    GrassResidueEmissions grassEmissions;
                    List<int> methods = jsonInputObject.MitigationMethods == null ? [] : [.. jsonInputObject.MitigationMethods];
                    GeneticGainScalars geneticGainScalars = jsonInputObject.GeneticGainScalars ?? throw new CustomAppException("Genetic gain scalar object is invalid.\n" +
                        "Please provide a valid genetic gain scalar object.");
                    if(mitigated)
                    {
                        //must be valid integer array as passed validity check
                        grassEmissions = GrassProductionEmissions.MitigatedGrassReseededOrPloughedRenewalEmissions(jsonInputObject.GridSquare,
                        jsonInputObject.TotalNApplied, jsonInputObject.SoilType, jsonInputObject.GrassType,
                        jsonInputObject.GrassUseType, jsonInputObject.SownWithClover, jsonInputObject.ReceivesManagedManure, geneticGainScalars, methods);
                    }
                    else
                    {
                        grassEmissions = GrassProductionEmissions.UnmitigatedGrassReseededOrPloughedRenewalEmissions(jsonInputObject.GridSquare,
                       jsonInputObject.TotalNApplied, jsonInputObject.SoilType, jsonInputObject.GrassType,
                       jsonInputObject.GrassUseType, jsonInputObject.SownWithClover, jsonInputObject.ReceivesManagedManure, geneticGainScalars);

                    }

                    standardOutput = new StandardOutput("Grass Renewal", jsonInputObject.GrassType.ToString(), "Grass Residue (Renewal)",
                        grassEmissions.ExportEmissions());
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
    /// Grass emissions from grass production
    /// Calculates emissions from grass production on a per hectare basis.
    /// This endpoint should be called separately for grass that is cut and grass that is grazed
    ///
    /// NOTE: This method does not include emissions from renewal - the renewal endpoint should be called in addition to this one if renewal takes place
    ///
    /// The United Kingdom Greenhouse Gas and Ammonia Emissions Inventory for Agriculture methodology is used for the calculations.
    /// </summary>
    /// <param name="jsonInputObject">A Json Serialisation of the GrassProductionandRenewalInput object that contains:
    /// the total amount of N applied as inorganic fertiliser, the UK 10km grid square ID code as integer, the soil type as integer, the grass type as integer, the grass use type (cut,
    /// grazed, or cut and grazed) as integer, a boolean indicating if the grass was sown with clover, a boolean indicating if the grass receives managed manure and
    /// an optional integer list (array) of mitigation methods applied.</param>
    /// <returns>Standard Output object in Json serialisation, where each Emission has a name (string), units (string) and a value (double).
    /// Further text strings provide information on the source of the emission and the stage within the source.</returns>
    [Consumes(MediaTypeNames.Application.Json)]
    [Produces(MediaTypeNames.Application.Json)]
    [ProducesResponseType(typeof(StandardOutput), StatusCodes.Status200OK)]
    [HttpPost("grass-production-emissions")]
    public IActionResult GrassProduction([FromBody] GrassProductionandRenewalInput jsonInputObject)
    {
        try
        {
            StandardOutput standardOutput = new();
            if(jsonInputObject != null)
            {
                string errors = GrassResidueValidityChecks.CheckValidInputs(jsonInputObject, out bool mitigated);
                if(errors.Equals(""))
                {
                    GrassResidueEmissions grassEmissions;
                    List<int> methods = jsonInputObject.MitigationMethods == null ? [] : [.. jsonInputObject.MitigationMethods];
                    GeneticGainScalars geneticGainScalars = jsonInputObject.GeneticGainScalars ?? throw new CustomAppException("Genetic gain scalar object is invalid.\n" +
                       "Please provide a valid genetic gain scalar object.");
                    if(mitigated)
                    {
                        //must be valid integer array as passed validity check
                        grassEmissions = GrassProductionEmissions.MitigatedGrassProductionEmissions(jsonInputObject.GridSquare,
                        jsonInputObject.TotalNApplied, jsonInputObject.SoilType, jsonInputObject.GrassType,
                        jsonInputObject.GrassUseType, jsonInputObject.SownWithClover, jsonInputObject.ReceivesManagedManure, geneticGainScalars, methods);
                    }
                    else
                    {
                        grassEmissions = GrassProductionEmissions.UnmitigatedGrassProductionEmissions(jsonInputObject.GridSquare,
                       jsonInputObject.TotalNApplied, jsonInputObject.SoilType, jsonInputObject.GrassType,
                       jsonInputObject.GrassUseType, jsonInputObject.SownWithClover, jsonInputObject.ReceivesManagedManure, geneticGainScalars);

                    }

                    standardOutput = new StandardOutput("Grass Production", jsonInputObject.GrassType.ToString(), "Grass Production",
                        grassEmissions.ExportEmissions());
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
    /// Calculates emissions from crop residues by crop type on a per hectare basis using the harvest index method.
    ///
    /// The United Kingdom Greenhouse Gas and Ammonia Emissions Inventory for Agriculture methodology is used for the calculations.
    /// </summary>
    /// <param name="jsonInputObject">A crop residue inputs (harvest index) object serialised to Json</param>
    /// <returns>Standard Output object in Json serialisation, where each Emission has a name (string), units (string) and a value (double).
    /// Further text strings provide information on the source of the emission and the stage within the source.</returns>
    [Consumes(MediaTypeNames.Application.Json)]
    [Produces(MediaTypeNames.Application.Json)]
    [ProducesResponseType(typeof(StandardOutput), StatusCodes.Status200OK)]
    [HttpPost("crop-residue-emissions-harvestindex")]
    public IActionResult CropResidueEmissions_HarvestIndex([FromBody] CropResidueInputs_HarvestIndex jsonInputObject)
    {
        try
        {
            StandardOutput standardOutput = new();
            if(jsonInputObject != null)
            {
                string errors = CropResidueValidityChecks.CheckValidInputs_HarvestIndex(jsonInputObject, out bool mitigated);
                if(errors.Equals(""))
                {
                    CropResidueEmissions residueEmissions;

                    if(mitigated)
                    {
                        List<int> methods = jsonInputObject.MitigationMethods == null ? [] : [.. jsonInputObject.MitigationMethods];
                        residueEmissions = CropResidues.MitigatedCropResidueEmissions_HarvestIndex(jsonInputObject.YieldFreshWeight,
                            jsonInputObject.Incorporated, jsonInputObject.CropType, methods,
                            jsonInputObject.CropDryMatterContent, jsonInputObject.CropHarvestIndex);
                    }
                    else
                    {
                        residueEmissions = CropResidues.UnmitigatedCropResidueEmissions_HarvestIndex(jsonInputObject.YieldFreshWeight,
                            jsonInputObject.Incorporated, jsonInputObject.CropType,
                            jsonInputObject.CropDryMatterContent, jsonInputObject.CropHarvestIndex);
                    }

                    standardOutput = new StandardOutput("Crop Residues", jsonInputObject.CropType.ToString(), "Residues",
                    residueEmissions.ExportEmissions());
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
