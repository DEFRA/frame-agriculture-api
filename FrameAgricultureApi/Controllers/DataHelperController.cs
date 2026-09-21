using Asp.Versioning;
using FrameAgricultureApi.Components;
using FrameAgricultureApi.CustomErrorHandling;
using FrameAgricultureApi.DataTransferObjects;
using FrameAgricultureApi.Enumerators;
using FrameAgricultureApi.IOClasses;
using FrameAgricultureApi.Libraries.Enteric;
using FrameAgricultureApi.Libraries.Enteric.DefaultInputs;
using FrameAgricultureApi.Libraries.Enteric.DefaultInputs.UserInputs;
using FrameAgricultureApi.Libraries.Enteric.DTO;
using FrameAgricultureApi.Libraries.GrassResidues;
using FrameAgricultureApi.Libraries.LookUps;
using FrameAgricultureApi.Libraries.ValidInputChecks;
using Microsoft.AspNetCore.Mvc;
using System.Net.Mime;
using static FrameAgricultureApi.Enumerators.Enumerators;

namespace FrameAgricultureApi.Controllers;

/// <summary>
/// Data HYelper Controller
/// </summary>
/// <param name="logger">Logger for data helper controller</param>
[ApiController]
[Route("[controller]")]
[Route("api/[controller]")]
[Route("v{v:apiVersion}/[controller]")]
[ApiVersion(1.0)]
public class DataHelperController(ILogger<DataHelperController> logger) : ControllerBase
{
    /// <summary>
    /// Logger
    /// </summary>
    public ILogger<DataHelperController> Logger { get; } = logger;

    private readonly MitigationMethodLookup mitigationMethodLookup = MitigationMethodLookup.Instance;
    /// <summary>
    /// Endpoint to return a list of all mitigation methods.
    /// </summary>
    /// <returns>An object providing list of mitigaiton methods.</returns>
    [Produces(MediaTypeNames.Application.Json)]
    [ProducesResponseType(typeof(List<MitigationMethodDto>), StatusCodes.Status200OK)]
    [HttpGet("get-mitigation-methods")]
    public IActionResult GetMitigationMethods()
    {
        try
        {
            var result = mitigationMethodLookup.GetAllMitigationMethods();
            return Ok(result);
        }
        catch(Exception ex) { return BadRequest(ex.Message); }
    }
    /// <summary>
    /// Endpoint to return a list of the 10km grid squares in the UK with the easting and northing of their centroid.
    /// </summary>
    /// <returns>A list of objects that define the grid square code, the easting and northing of the centroid.</returns>
    [Produces(MediaTypeNames.Application.Json)]
    [ProducesResponseType(typeof(Dictionary<int, GridCoordinateDto>), StatusCodes.Status200OK)]
    [HttpGet("get-grid-locations")]
    public IActionResult GetGridLocations()
    {
        try
        {
            var result = PhysicalLookup.GetGridLocations();
            return Ok(result);
        }
        catch(Exception ex) { return BadRequest(ex.Message); }
    }
    /// <summary>
    /// Endpoint to return all enumerators and their values that are used in FrameAgricultureApi.
    /// </summary>
    /// <returns>A list of enumerator objects with the individual entries in the enuermator object specified as text name and integer value.</returns>
    [Produces(MediaTypeNames.Application.Json)]
    [ProducesResponseType(typeof(Dictionary<string, Dictionary<string, int>>), StatusCodes.Status200OK)]
    [HttpGet("get-enumerators")]
    public IActionResult GetEnumerators()
    {
        try
        {
            var result = EnumeratorLookup.GetEnumerators();
            return Ok(result);
        }
        catch(Exception ex) { return BadRequest(ex.Message); }
    }

    /// <summary>
    /// Data helper function to return the energy balance object for a required combination of country,
    /// sheep type, sheep sub-type and sheep system. It uses the results from an offline sheep model with
    /// a number of underpinning assumptions about diet and the time spent in housing
    /// </summary>
    /// <param name="jsonInputObject">A Json serialisation of the SheepEnergyBalanceKey object that contains the
    /// following enumerators as integers: country, sheep type, sheep sub-type and sheep system type</param>
    /// <returns>A sheep energy balance output object</returns>
    /// <exception cref="CustomAppException">Errors when checking validity of inputs</exception>
    [Consumes(MediaTypeNames.Application.Json)]
    [Produces(MediaTypeNames.Application.Json)]
    [ProducesResponseType(typeof(SheepEnergyBalance), StatusCodes.Status200OK)]
    [HttpPost("datahelper-sheep-energy-balance")]
    public IActionResult SheepEnergyBalance([FromBody] SheepEnergyBalanceKey jsonInputObject)
    {
        try
        {
            SheepEnergyBalance returnobject = new();

            if(jsonInputObject != null)
            {
                string errors = DataHelperValidityChecks.CheckValidSheepEnergyBalanceKey(jsonInputObject);

                if(errors.Equals(""))
                {
                    returnobject = DataHelpers.SheepEnergyBalanceOutputs(jsonInputObject.Country, jsonInputObject.SheepType,
                        jsonInputObject.SheepSubType, jsonInputObject.SheepSystemType);
                }
                else
                {
                    throw new CustomAppException("Invalid request data:\n" + errors);
                }
            }
            return Ok(returnobject);
        }
        catch(Exception ex) { return BadRequest(ex.Message); }
    }

    /// <summary>
    /// Data helper function to return the first winter fracleach object for a required combination of 10km UK grid square and
    /// soil texture type</summary>
    /// <param name="jsonInputObject">A Json serialisation of the FirstWinterManureFracLEachKey object that contains the
    /// following: UK 10km grid square ID as integer and soil texture type enumerator as integer</param>
    /// <returns>The first winter manure farc leach percentage as double</returns>
    /// <exception cref="CustomAppException">Errors when checking validity of inputs</exception>
    [Consumes(MediaTypeNames.Application.Json)]
    [Produces(MediaTypeNames.Application.Json)]
    [ProducesResponseType(typeof(double), StatusCodes.Status200OK)]
    [HttpPost("datahelper-first-winter-manure-frac-leach")]
    public IActionResult FirstWinterManureFracLeach([FromBody] FirstWinterManureFracLeachKey jsonInputObject)
    {
        try
        {
            double returnobject = new();

            if(jsonInputObject != null)
            {
                string errors = DataHelperValidityChecks.CheckValidFirstWinterManureFracLeachKey(jsonInputObject);

                if(errors.Equals(""))
                {
                    returnobject = DataHelpers.FirstWinterManureFracLeach(jsonInputObject.CellID, jsonInputObject.SoilType);
                }
                else
                {
                    throw new CustomAppException("Invalid request data:\n" + errors);
                }
            }
            return Ok(returnobject);
        }
        catch(Exception ex) { return BadRequest(ex.Message); }
    }

    /// <summary>
    /// Data helper function to return the grass coefficients object for a required combination of climate region,
    /// soil type, grass use type, grass type, whether the grass is sown with white clover and whether the grass
    /// receives managed manure for a given grass coefficient.
    /// The functions use the outputs from an offline grass model to provide coefficients for a fourth order
    /// polynomial that predicts the required output based on the rate of application of synthetic fertiliser.
    /// This table is extended from that used in the national inventory to include all possible combinations of
    /// the keying variables.
    /// </summary>
    /// <param name="jsonInputObject">A Json serialisation of the GrassCoefficientKey object that countains the
    /// climate region, soil type, grass use type and grass type as integers along with whether the grass is sown
    /// with white clover and whether the grass receives managed manure as booleans and the grass coefficient
    /// required type enumerator as an integer</param>
    /// <returns>A grass coefficients object</returns>
    /// <exception cref="CustomAppException">Errors when chekign validity of inputs</exception>
    [Consumes(MediaTypeNames.Application.Json)]
    [Produces(MediaTypeNames.Application.Json)]
    [ProducesResponseType(typeof(GrassEquationCoefficients), StatusCodes.Status200OK)]
    [HttpPost("datahelper-grass-coefficients")]
    public IActionResult GrassCoefficients([FromBody] GrassCoefficientKey jsonInputObject)
    {
        try
        {
            GrassEquationCoefficients returnobject = new();

            if(jsonInputObject != null)
            {
                string errors = DataHelperValidityChecks.CheckValidGrassCoefficientKey(jsonInputObject);

                if(errors.Equals(""))
                {
                    switch(jsonInputObject.GrassCoefficientRequired)
                    {
                        case (int)GrassEquations.BelowGroundDryMatter:
                            returnobject = DataHelpers.GrassModelCoefficients_BelowGroundDryMatter(jsonInputObject.ClimateRegion,
                                jsonInputObject.SoilTextureType, jsonInputObject.GrassType, jsonInputObject.GrassUseType,
                                jsonInputObject.WhiteCloverSown, jsonInputObject.RecievesManagedManure);
                            break;

                        case (int)GrassEquations.AboveGroundDryMatter:
                            returnobject = DataHelpers.GrassModelCoefficients_AboveGroundDryMatter(jsonInputObject.ClimateRegion,
                                jsonInputObject.SoilTextureType, jsonInputObject.GrassType, jsonInputObject.GrassUseType,
                                jsonInputObject.WhiteCloverSown, jsonInputObject.RecievesManagedManure);
                            break;

                        case (int)GrassEquations.YieldDryMatter:
                            returnobject = DataHelpers.GrassModelCoefficients_YieldDryMatter(jsonInputObject.ClimateRegion,
                                jsonInputObject.SoilTextureType, jsonInputObject.GrassType, jsonInputObject.GrassUseType,
                                jsonInputObject.WhiteCloverSown, jsonInputObject.RecievesManagedManure);
                            break;

                        case (int)GrassEquations.ResidualNitrogen:
                            returnobject = DataHelpers.GrassModelCoefficients_ResidualNitrogen(jsonInputObject.ClimateRegion,
                                jsonInputObject.SoilTextureType, jsonInputObject.GrassType, jsonInputObject.GrassUseType,
                                jsonInputObject.WhiteCloverSown, jsonInputObject.RecievesManagedManure);
                            break;

                        case (int)GrassEquations.FracLeach:
                            returnobject = DataHelpers.GrassModelCoefficients_FracLeach(jsonInputObject.ClimateRegion,
                                jsonInputObject.SoilTextureType, jsonInputObject.GrassType, jsonInputObject.GrassUseType,
                                jsonInputObject.WhiteCloverSown, jsonInputObject.RecievesManagedManure);
                            break;

                        case (int)GrassEquations.NitrogeninGrassHarvestedorEaten:
                                returnobject = DataHelpers.GrassModelCoefficients_GrassNitrogenConsumedbyLivestockorCut(jsonInputObject.ClimateRegion,
                                    jsonInputObject.SoilTextureType, jsonInputObject.GrassType, jsonInputObject.GrassUseType,
                                    jsonInputObject.WhiteCloverSown, jsonInputObject.RecievesManagedManure);
                                break;
                        case (int)GrassEquations.NitrogenfromCloverFixation:
                            returnobject = DataHelpers.GrassModelCoefficients_NitrogenfromCloverFixation(jsonInputObject.ClimateRegion,
                                jsonInputObject.SoilTextureType, jsonInputObject.GrassType, jsonInputObject.GrassUseType,
                                jsonInputObject.WhiteCloverSown, jsonInputObject.RecievesManagedManure);
                            break;
                    }
                    }
                    else
                    {
                        throw new CustomAppException("Invalid request data:\n" + errors);
                    }
                }
                return Ok(returnobject);
            }
            catch (Exception ex) { return BadRequest(ex.Message); }
        }

    /// <summary>
    /// Returns default values for Dairy Cows.
    /// </summary>
    /// <param name="dairyLiveweightKey"></param>
    /// <returns></returns>
    [Consumes(MediaTypeNames.Application.Json)]
    [Produces(MediaTypeNames.Application.Json)]
    [ProducesResponseType(typeof(DairyKeyPerformanceIndicators), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [HttpPost("default-dairy-key-performance-indicators")]
    public IActionResult DefaultDairyKeyPerformanceIndicators([FromBody] DairyLiveweightKey dairyLiveweightKey)
    {
        var response = InputHelperFunctions.DairyKeyPerformanceIndicators(dairyLiveweightKey);
        return Ok(response);
    }

    /// <summary>
    /// Returns default values for dairy cows foraged diet
    /// </summary>
    /// <param name="dairyForageKey"></param>
    /// <returns></returns>
    [Consumes(MediaTypeNames.Application.Json)]
    [Produces(MediaTypeNames.Application.Json)]
    [ProducesResponseType(typeof(DefaultForageInputs), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [HttpPost("default-dairy-forage-inputs")]
    public IActionResult DefaultDairyForageInputs([FromBody] DairyForageKey dairyForageKey)
    {
        var response = InputHelperFunctions.GetDefaultDairyForageDietInputs(dairyForageKey);
        return Ok(response);
    }

    /// <summary>
    /// Returns default values for dairy cows concentrate diet
    /// </summary>
    /// <param name="dairyConcentrateKey"></param>
    /// <returns></returns>
    [Consumes(MediaTypeNames.Application.Json)]
    [Produces(MediaTypeNames.Application.Json)]
    [ProducesResponseType(typeof(DefaultDairyConcentrateInputs), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [HttpPost("default-dairy-concentrate-inputs")]
    public IActionResult DefaultDairyConcentrateInputs([FromBody] DairyConcentrateKey dairyConcentrateKey)
    {
        var response = InputHelperFunctions.GetDefaultDairyConcentrateDietInputs(dairyConcentrateKey);
        return Ok(response);
    }

    /// <summary>
    /// Returns default values for dairy cows milk nutrition.
    /// </summary>
    /// <param name="dairyMilkKey"></param>
    /// <returns></returns>
    [Consumes(MediaTypeNames.Application.Json)]
    [Produces(MediaTypeNames.Application.Json)]
    [ProducesResponseType(typeof(DefaultDairyMilkInput), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [HttpPost("default-dairy-milk-inputs")]
    public IActionResult DefaultDairyMilkInputs([FromBody] DairyMilkKey dairyMilkKey)
    {
        var response = InputHelperFunctions.GetDefaultDairyMilkInputs(dairyMilkKey);
        return Ok(response);
    }

    /// <summary>
    /// Returns default values for beef cows.
    /// </summary>
    /// <param name="key"></param>
    /// <returns></returns>
    [Consumes(MediaTypeNames.Application.Json)]
    [Produces(MediaTypeNames.Application.Json)]
    [ProducesResponseType(typeof(BeefKeyPerformanceIndicators), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [HttpPost("default-beef-key-performance-indicators")]
    public IActionResult DefaultBeefKeyPerformanceIndicators([FromBody] BeefDefaultKey key)
    {
        var response = InputHelperFunctions.BeefKeyPerformanceIndicators(key);
        return Ok(response);
    }

    /// <summary>
    /// Returns the breakdown of diet.
    /// </summary>
    /// <param name="dietInputs"></param>
    /// <returns></returns>
    [Consumes(MediaTypeNames.Application.Json)]
    [Produces(MediaTypeNames.Application.Json)]
    [ProducesResponseType(typeof(DietOutputs), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [HttpPost("diet-information")]
    public IActionResult DietInformation([FromBody] DietInputs dietInputs)
    {
        var response = EntericFunctions.CalculateDietInformation(dietInputs);

        return Ok(response);
    }

    /// <summary>
    /// Returns a breakdown of liveweights for dairy cows.
    /// </summary>
    /// <param name="liveweightInputs"></param>
    /// <returns></returns>
    [Consumes(MediaTypeNames.Application.Json)]
    [Produces(MediaTypeNames.Application.Json)]
    [ProducesResponseType(typeof(LiveWeightOutputsDairy), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [HttpPost("dairy-liveweights")]
    public IActionResult LiveweightDairy([FromBody] DairyLiveWeightInputs liveweightInputs)
    {
        var response = EntericFunctions.LiveweightsDairy(liveweightInputs);

        return Ok(response);
    }

    /// <summary>
    /// Returns a breakdown of liveweights for beef cows.
    /// </summary>
    /// <param name="liveweightInputs"></param>
    /// <returns></returns>
    [Consumes(MediaTypeNames.Application.Json)]
    [Produces(MediaTypeNames.Application.Json)]
    [ProducesResponseType(typeof(LiveWeightOutputsBeef), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [HttpPost("beef-liveweights")]
    public IActionResult LiveweightBeef([FromBody] BeefLiveWeightInputs liveweightInputs)
    {
        var response = EntericFunctions.LiveweightsBeef(liveweightInputs);

        return Ok(response);
    }

    /// <summary>
    /// Returns a daily breakdown of energy balance for beef cows.
    /// </summary>
    /// <param name="energyBalanceInputs"></param>
    /// <returns></returns>
    [Consumes(MediaTypeNames.Application.Json)]
    [Produces(MediaTypeNames.Application.Json)]
    [ProducesResponseType(typeof(EnergyBalanceOutputsBeef), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [HttpPost("beef-energy-and-nitrogen-balance")]
    public IActionResult BeefDailyEnergyAndNitrogenBalance([FromBody] BeefEnergyBalanceInputs energyBalanceInputs)
    {
        var response = EntericFunctions.EnergyAndNitrogenBalanceBeef(energyBalanceInputs);

        return Ok(response);
    }

    /// <summary>
    /// Returns a daily breakdown of energy balance for dairy cows.
    /// </summary>
    /// <param name="energyBalanceInputs"></param>
    /// <returns></returns>
    [Consumes(MediaTypeNames.Application.Json)]
    [Produces(MediaTypeNames.Application.Json)]
    [ProducesResponseType(typeof(EnergyBalanceOutputsDairy), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [HttpPost("dairy-energy-and-nitrogen-balance")]
    public IActionResult DairyDailyEnergyAndNitrogenBalance([FromBody] DairyEnergyBalanceInputs energyBalanceInputs)
    {
        var response = EntericFunctions.EnergyAndNitrogenBalanceDairy(energyBalanceInputs);

        return Ok(response);
    }

    /// <summary>
    /// Returns liveweight gain for a specified time period for dairy cattle
    /// </summary>
    /// <param name="CattleType">Dairy cattle type as DairyCattle Enumerator</param>
    /// <param name="LiveweightData">Dairy Livweweight Outputs object</param>
    /// <param name="TimePeriodsPerYear">The number of time periods in a year (as double)</param>
    /// <returns>Liveweight gain (kg/time period)</returns>
    [Consumes(MediaTypeNames.Application.Json)]
    [Produces(MediaTypeNames.Application.Json)]
    [ProducesResponseType(typeof(EnergyBalanceOutputsDairy), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [HttpPost("dairy-liveweight-gain")]
    public IActionResult DairyLiveweightGain([FromBody] DairyLiveweightGainInputs Inputs)
    {
        double response = EntericFunctions.LiveWeightGainDairy(Inputs.CattleType, Inputs.LiveweightData, Inputs.TimePeriodsPerYear);

        return Ok(response);
    }

    /// <summary>
    /// Returns liveweight gain for a specified time period for beef cattle
    /// </summary>
    /// <param name="LiveweightData">Beef Liveweight Outputs object</param>
    /// <param name="TimePeriodsPerYear">The number of time periods in a year (as double)</param>
    /// <returns>Liveweight gain (kg/time period)</returns>
    [Consumes(MediaTypeNames.Application.Json)]
    [Produces(MediaTypeNames.Application.Json)]
    [ProducesResponseType(typeof(EnergyBalanceOutputsDairy), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [HttpPost("beef-liveweight-gain")]
    public IActionResult BeefLiveweightGain([FromBody] BeefLiveweightGainInputs Inputs)
    {
        double response = EntericFunctions.LiveweightGainBeef(Inputs.LiveweightData, Inputs.TimePeriodsPerYear);

        return Ok(response);
    }

    /// <summary>
    /// Data helper function to return the geneticGainScalars for grass (used in the grass and sheep sectors),
    /// </summary>
    /// <param name="jsonInputObject">A Json serialisation of the GrassProductionandRenewalInput object that contains the
    /// following values: Nitrogen applied (kg/ha), Location (10km grid square ID), Soil type, grass type, sown with clover boolean,
    /// Recieves managed manure (boolean)
    /// </param>
    /// <returns>A sheep energy balance output object</returns>
    /// <exception cref="CustomAppException">Errors when checking validity of inputs</exception>
    [Consumes(MediaTypeNames.Application.Json)]
    [Produces(MediaTypeNames.Application.Json)]
    [ProducesResponseType(typeof(SheepEnergyBalance), StatusCodes.Status200OK)]
    [HttpPost("datahelper-grass-genetic-gain-scalars")]
    public IActionResult GeneticGainScalars([FromBody] GrassGeneticGainInputs jsonInputObject)
    {
        try
        {
            GeneticGainScalars returnobject = new();

            if(jsonInputObject != null)
            {
                string errors = DataHelperValidityChecks.CheckValidGrassGeneticGainInputs(jsonInputObject);

                if(errors.Equals(""))
                {
                    returnobject = GrassGeneticGainScalarFunction.ReturnGeneticGainScalars((GrassType)jsonInputObject.GrassType, jsonInputObject.FertiliserRate);
                }
                else
                {
                    throw new CustomAppException("Invalid request data:\n" + errors);
                }
            }
            return Ok(returnobject);
        }
        catch(Exception ex) { return BadRequest(ex.Message); }
    }
}
