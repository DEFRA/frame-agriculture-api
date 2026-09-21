using Asp.Versioning;
using FrameAgricultureApi.IOClasses;
using FrameAgricultureApi.Libraries.Excreta;
using FrameAgricultureApi.Libraries.Excreta.DTO;
using FrameAgricultureApi.Libraries.Excreta.DTO.DefaultInputs.UserInputs;
using FrameAgricultureApi.Generic;
using Microsoft.AspNetCore.Mvc;
using System.Net.Mime;
using static FrameAgricultureApi.Enumerators.Enumerators;

namespace FrameAgricultureApi.Controllers;

[ApiController]
[Route("[controller]")]
[Route("api/[controller]")]
[Route("v{v:apiVersion}/[controller]")]
[ApiVersion(1.0)]
public class ExcretalProductionController : ControllerBase
{

    const string Stage = "Excretion";

    /// <summary>
    /// Calculates the total nitrogen excretion, nitrogen in urine and nitrogen in faeces for Dairy cattle.
    /// </summary>
    /// <param name="inputs"></param>
    /// <returns></returns>
    [Consumes(MediaTypeNames.Application.Json)]
    [Produces(MediaTypeNames.Application.Json)]
    [ProducesResponseType(typeof(StandardOutput), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [HttpPost("dairy-nitrogen-excretion")]
    public IActionResult DairyNitrogen([FromBody] DairyNitrogenInputsDto inputs)
    {
        var nitrogenOutputs = ExcretaFunctions.DairyNitrogen(inputs.NitrogenInputs, inputs.TimeScalar, inputs.CattleType);
        NitrogenAndEntericEmissions nitrogenEmission = new(nitrogenOutputs);
        StandardOutput response = new(Sector.Dairy.ToString(), inputs.CattleType.ToString(), Stage, nitrogenEmission.Excreta);
        return Ok(response);
    }

    /// <summary>
    /// Calculates the total nitrogen excretion, nitrogen in urine and nitrogen in faeces for Beef cattle.
    /// </summary>
    /// <param name="inputs"></param>
    /// <returns></returns>
    [Consumes(MediaTypeNames.Application.Json)]
    [Produces(MediaTypeNames.Application.Json)]
    [ProducesResponseType(typeof(StandardOutput), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [HttpPost("beef-nitrogen-excretion")]
    public IActionResult BeefNitrogen([FromBody] BeefNitrogenInputsDto inputs)
    {
        var nitrogenOutputs = ExcretaFunctions.BeefNitrogen(inputs.NitrogenInputs, inputs.TimeScalar, inputs.CattleType);
        var nitrogenEmissions = new NitrogenAndEntericEmissions(nitrogenOutputs);
        StandardOutput response = new(Sector.Beef.ToString(), inputs.CattleType.ToString(), Stage, nitrogenEmissions.Excreta);
        return Ok(response);
    }

    /// <summary>
    /// Calculates the initial nitrogen for pigs, poultry and minor livestock
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    [Consumes(MediaTypeNames.Application.Json)]
    [Produces(MediaTypeNames.Application.Json)]
    [ProducesResponseType(typeof(StandardOutput), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [HttpPost("default-pigs-poultry-minor-livestock-nitrogen")]
    public IActionResult InitialNitrogenPigsPoultryMinorLivestock([FromBody] ExcretaPigsPoultryMinorLivestockNitrogenKey input)
    {
        var initialNitrogen = ExcretaEquations.InitialNitrogen(input.Sector, input.AnimalType);
        Emission nitrogenEmissions = new("Nitrogen", "", initialNitrogen);
        StandardOutput response = new(input.Sector.ToString(), input.AnimalType.ToString(), Stage, new List<Emission>() { nitrogenEmissions });
        return Ok(response);
    }

    /// <summary>
    /// Calculates indoor nitrogen, indoor TAN, outdoor nitrogen and the outdoor TAN for free range poultry
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    [Consumes(MediaTypeNames.Application.Json)]
    [Produces(MediaTypeNames.Application.Json)]
    [ProducesResponseType(typeof(StandardOutput), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [HttpPost("nitrogen-emissions-free-range-poultry")]
    public IActionResult NitrogenEmissionFreeRangePoultry([FromBody] InitialNitrogenFreeRangePoultryUserInput input)
    {
        var excretaEmissions = ExcretaFunctions.CalculateExcretaEmissionFreeRangePoultry(input);
        NitrogenEmissionFreeRangePoultry nitrogenEmissions = new(excretaEmissions);
        StandardOutput response = new(Sector.Poultry.ToString(), input.PoultryType.ToString(), Stage, nitrogenEmissions.Excreta);
        return Ok(response);
    }

    /// <summary>
    /// Calculates the nitrogen and TAN for indoor poultry
    /// </summary>
    /// <param name="poultryType"></param>
    /// <returns></returns>
    [Consumes(MediaTypeNames.Application.Json)]
    [Produces(MediaTypeNames.Application.Json)]
    [ProducesResponseType(typeof(StandardOutput), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [HttpPost("nitrogen-emissions-indoor-poultry")]
    public IActionResult NitrogenEmissionIndoorPoultry([FromQuery] PoultryType poultryType)
    {
        var exretaEmissions = ExcretaFunctions.CalculateExcretaEmissionPigsPoultryMinorLivestock(Sector.Poultry, (int)poultryType);
        var nitrogenEmissions = new NitrogenEmission(exretaEmissions);
        StandardOutput response = new(Sector.Poultry.ToString(), poultryType.ToString(), Stage, nitrogenEmissions.Exreta);
        return Ok(response);
    }

    /// <summary>
    /// Calculates the nitrogen and TAN for minor livestock
    /// </summary>
    /// <param name="minorLivestockType"></param>
    /// <returns></returns>
    [Consumes(MediaTypeNames.Application.Json)]
    [Produces(MediaTypeNames.Application.Json)]
    [ProducesResponseType(typeof(StandardOutput), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [HttpPost("nitrogen-emissions-minor-livestock")]
    public IActionResult NitrogenEmissionMinorLivestock([FromQuery] MinorLivestockType minorLivestockType)
    {
        var excretaEmissions = ExcretaFunctions.CalculateExcretaEmissionPigsPoultryMinorLivestock(Sector.MinorLivestock, (int)minorLivestockType);
        var nitrogenEmissions = new NitrogenEmission(excretaEmissions);
        StandardOutput response = new(Sector.MinorLivestock.ToString(), minorLivestockType.ToString(), Stage, nitrogenEmissions.Exreta);
        return Ok(response);
    }

    /// <summary>
    /// Calculates the nitrogen and TAN for pigs
    /// </summary>
    /// <param name="pigType"></param>
    /// <returns></returns>
    [Consumes(MediaTypeNames.Application.Json)]
    [Produces(MediaTypeNames.Application.Json)]
    [ProducesResponseType(typeof(StandardOutput), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [HttpPost("nitrogen-emissions-pigs")]
    public IActionResult NitrogenEmissionPigs([FromQuery] PigType pigType)
    {
        var excretaEmissions = ExcretaFunctions.CalculateExcretaEmissionPigsPoultryMinorLivestock(Sector.Pigs, (int)pigType);
        var nitrogenEmissions = new NitrogenEmission(excretaEmissions);
        StandardOutput response = new(Sector.Pigs.ToString(), pigType.ToString(), Stage, nitrogenEmissions.Exreta);
        return Ok(response);
    }
}
