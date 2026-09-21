using Asp.Versioning;
using FrameAgricultureApi.IOClasses;
using FrameAgricultureApi.Libraries.Excreta;
using FrameAgricultureApi.Libraries.Excreta.DTO;
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
public class EntericMethaneController : ControllerBase
{
    const string Stage = "Enteric Fermentation";

    /// <summary>
    /// Calculates methane for dairy cattle
    /// </summary>
    /// <param name="inputs"></param>
    /// <returns></returns>
    [Consumes(MediaTypeNames.Application.Json)]
    [Produces(MediaTypeNames.Application.Json)]
    [ProducesResponseType(typeof(StandardOutput), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [HttpPost("dairy-enteric-methane-excretion")]
    public IActionResult DairyEntericMethane([FromBody] DairyEntericMethaneInputsDto inputs)
    {
        var methane = ExcretaFunctions.DairyEntericMethane(inputs.CattleType, inputs.DailyDMIIntake);
        Emission methaneEmission = new("Methane", "", methane);
        StandardOutput response = new(Sector.Dairy.ToString(), inputs.CattleType.ToString(), Stage, new List<Emission>() { methaneEmission });
        return Ok(response);
    }

    /// <summary>
    /// Calculates methane for beef cattle
    /// </summary>
    /// <param name="inputs"></param>
    /// <returns></returns>
    [Consumes(MediaTypeNames.Application.Json)]
    [Produces(MediaTypeNames.Application.Json)]
    [ProducesResponseType(typeof(StandardOutput), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [HttpPost("beef-enteric-methane-excretion")]
    public IActionResult BeefEntericMethane([FromBody] BeefEntericMethaneInputsDto inputs)
    {
        var methane = ExcretaFunctions.BeefEntericMethane(inputs.BeefCattleType, inputs.DailyDMIIntake);
        Emission methaneEmission = new("Methane", "", methane);
        StandardOutput response = new(Sector.Beef.ToString(), inputs.BeefCattleType.ToString(), Stage, new List<Emission>() { methaneEmission });
        return Ok(response);
    }

    /// <summary>
    /// Calculates enteric methane for minor livestock
    /// </summary>
    /// <param name="minorLivestockType"></param>
    /// <returns></returns>
    [Consumes(MediaTypeNames.Application.Json)]
    [Produces(MediaTypeNames.Application.Json)]
    [ProducesResponseType(typeof(StandardOutput), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [HttpPost("enteric-methane-emissions-minor-livestock")]
    public IActionResult EntericMethaneEmissionMinorLivestock([FromQuery] MinorLivestockType minorLivestockType)
    {
        var excretaEmissions = ExcretaFunctions.CalculateExcretaEmissionPigsPoultryMinorLivestock(Sector.MinorLivestock, (int)minorLivestockType);
        Emission entericMethane = new("Enteric Methane", "", excretaEmissions.EntericMethane);
        StandardOutput response = new(Sector.MinorLivestock.ToString(), minorLivestockType.ToString(), Stage, new List<Emission>() { entericMethane });
        return Ok(response);
    }

    /// <summary>
    /// Calculates enteric methane for pigs
    /// </summary>
    /// <param name="pigType"></param>
    /// <returns></returns>
    [Consumes(MediaTypeNames.Application.Json)]
    [Produces(MediaTypeNames.Application.Json)]
    [ProducesResponseType(typeof(StandardOutput), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [HttpPost("enteric-methane-emissions-pigs")]
    public IActionResult EntericMethaneEmissionPigs([FromQuery] PigType pigType)
    {
        var excretaEmissions = ExcretaFunctions.CalculateExcretaEmissionPigsPoultryMinorLivestock(Sector.Pigs, (int)pigType);
        Emission entericMethane = new("Enteric Methane", "", excretaEmissions.EntericMethane);
        StandardOutput response = new(Sector.Pigs.ToString(), pigType.ToString(), Stage, new List<Emission>() { entericMethane });
        return Ok(response);
    }
}
