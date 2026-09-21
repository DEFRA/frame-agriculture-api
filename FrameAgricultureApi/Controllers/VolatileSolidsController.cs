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
public class VolatileSolidsController : ControllerBase
{

    const string Stage = "Excretion";

    /// <summary>
    /// Calculates the volatile solids of Dairy cattle.
    /// </summary>
    /// <param name="inputs"></param>
    /// <returns></returns>
    [Consumes(MediaTypeNames.Application.Json)]
    [Produces(MediaTypeNames.Application.Json)]
    [ProducesResponseType(typeof(StandardOutput), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [HttpPost("dairy-volatile-solids-excretion")]
    public IActionResult DairyVolatileSolids([FromBody] VolatileSolidsInputsDto inputs)
    {
        var volatileSolids = ExcretaFunctions.DairyVolatileSolids(inputs.VolatileSolidsInputs, inputs.TimeScalar);
        var volatileSolidsEmition = new Emission("Volatile Solids", "", volatileSolids);
        StandardOutput response = new(Sector.Dairy.ToString(), "", Stage, new List<Emission>() { volatileSolidsEmition });
        return Ok(response);
    }

    /// <summary>
    /// Calculates the volatile solids of Beef cattle.
    /// </summary>
    /// <param name="inputs"></param>
    /// <returns></returns>
    [Consumes(MediaTypeNames.Application.Json)]
    [Produces(MediaTypeNames.Application.Json)]
    [ProducesResponseType(typeof(StandardOutput), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [HttpPost("beef-volatile-solids-excretion")]
    public IActionResult BeefVolatileSolids([FromBody] VolatileSolidsInputsDto inputs)
    {
        var volatileSolids = ExcretaFunctions.BeefVolatileSolids(inputs.VolatileSolidsInputs, inputs.TimeScalar);
        Emission volatileSolidsEmission = new("Volatile Solids", "", volatileSolids);
        StandardOutput response = new(Sector.Beef.ToString(), "", Stage, new List<Emission>() { volatileSolidsEmission });
        return Ok(response);
    }

    /// <summary>
    /// Calculates the volatile solids of free range poultry
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    [Consumes(MediaTypeNames.Application.Json)]
    [Produces(MediaTypeNames.Application.Json)]
    [ProducesResponseType(typeof(StandardOutput), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [HttpPost("volatile-solids-emissions-free-range-poultry")]
    public IActionResult VolatileSolidsEmissionsFreeRangePoultry([FromBody] InitialNitrogenFreeRangePoultryUserInput input)
    {
        var excretaEmissions = ExcretaFunctions.CalculateExcretaEmissionFreeRangePoultry(input);
        var volatileSolidsEmissions = new VolatileEmissionFreeRangePoultry(excretaEmissions);
        StandardOutput response = new(Sector.Poultry.ToString(), input.PoultryType.ToString(), Stage, volatileSolidsEmissions.Excreta);
        return Ok(response);
    }

    /// <summary>
    /// Calculates the volatile solids of indoor poultry
    /// </summary>
    /// <param name="poultryType"></param>
    /// <returns></returns>
    [Consumes(MediaTypeNames.Application.Json)]
    [Produces(MediaTypeNames.Application.Json)]
    [ProducesResponseType(typeof(StandardOutput), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [HttpPost("volatile-solids-emissions-indoor-poultry")]
    public IActionResult VolatileSolidsEmissionIndoorPoultry([FromQuery] PoultryType poultryType)
    {
        var excreta = ExcretaFunctions.CalculateExcretaEmissionPigsPoultryMinorLivestock(Sector.Poultry, (int)poultryType);
        Emission volatileSolidsEmission = new Emission("Volatile Solids", "", excreta.VolatileSolids);
        StandardOutput response = new(Sector.Poultry.ToString(), poultryType.ToString(), Stage, new List<Emission>() { volatileSolidsEmission });
        return Ok(response);
    }

    /// <summary>
    /// Calculates the volatile solids of minor livestock
    /// </summary>
    /// <param name="minorLivestockType"></param>
    /// <returns></returns>
    [Consumes(MediaTypeNames.Application.Json)]
    [Produces(MediaTypeNames.Application.Json)]
    [ProducesResponseType(typeof(StandardOutput), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [HttpPost("volatile-solids-emissions-minor-livestock")]
    public IActionResult VolatileSolidsEmissionMinorLivestock([FromQuery] MinorLivestockType minorLivestockType)
    {
        var excreta = ExcretaFunctions.CalculateExcretaEmissionPigsPoultryMinorLivestock(Sector.MinorLivestock, (int)minorLivestockType);
        Emission volatileSolidsEmission = new Emission("Volatile Solids", "", excreta.VolatileSolids);
        StandardOutput response = new(Sector.MinorLivestock.ToString(), minorLivestockType.ToString(), Stage, new List<Emission>() { volatileSolidsEmission });
        return Ok(response);
    }

    /// <summary>
    /// Calculates the volatile solids of pigs
    /// </summary>
    /// <param name="pigType"></param>
    /// <returns></returns>
    [Consumes(MediaTypeNames.Application.Json)]
    [Produces(MediaTypeNames.Application.Json)]
    [ProducesResponseType(typeof(StandardOutput), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [HttpPost("volatile-solids-emissions-pigs")]
    public IActionResult VolatileSolidsEmissionPigs([FromQuery] PigType pigType)
    {
        var excreta = ExcretaFunctions.CalculateExcretaEmissionPigsPoultryMinorLivestock(Sector.Pigs, (int)pigType);
        Emission volatileSolidsEmission = new Emission("Volatile Solids", "", excreta.VolatileSolids);
        StandardOutput response = new(Sector.Pigs.ToString(), pigType.ToString(), Stage, new List<Emission> { volatileSolidsEmission });
        return Ok(response);
    }
}
