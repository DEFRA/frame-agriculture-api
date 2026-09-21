using Asp.Versioning;
using FrameAgricultureApi.Components;
using FrameAgricultureApi.CustomErrorHandling;
using FrameAgricultureApi.IOClasses;
using FrameAgricultureApi.Libraries.Storage;
using FrameAgricultureApi.Libraries.ValidInputChecks;
using Microsoft.AspNetCore.Mvc;
using System.Net.Mime;
using static FrameAgricultureApi.Enumerators.Enumerators;

namespace FrameAgricultureApi.Controllers;
/// <summary>
/// COntroller containing endpoints for emissions from manure storage
/// </summary>
/// <param name="logger">Logger</param>
[ApiController]
[Route("[controller]")]
[Route("api/[controller]")]
[Route("v{v:apiVersion}/[controller]")]
[ApiVersion(1.0)]
public class ManureManagementStorageController(ILogger<ManureManagementStorageController> logger) : ControllerBase
{
    /// <summary>
    /// Logger
    /// </summary>
    public ILogger<ManureManagementStorageController> Logger { get; } = logger;

    /// <summary>
    /// Calculates the emission from storage of manure from all livestock, except sheep.
    ///
    /// </summary>
    /// <param name="jsonInputObject">Storage Inputs object that contains the sector enumerator, animal type enumerator as an integer,
    /// total nitrogen in manure entering storage (kg/head/unit time), total ammoniacal nitrogen in manure entering storage (kg/head/unit time),
    /// and volatile solids in manure entering storage</param>
    /// <returns>Standard Output object in JSON serialisation where each Emission has a name (string), units (string) and a value (double).
    /// Further text strings provide information on the source of the emission and the stage within the source.</returns>
    /// <exception cref="CustomAppException">Provides an exception with a message explainign the error that occurred</exception>
    [Consumes(MediaTypeNames.Application.Json)]
    [Produces(MediaTypeNames.Application.Json)]
    [ProducesResponseType(typeof(StandardOutput), StatusCodes.Status200OK)]
    [HttpPost("manure-management-storage-emissions-livestock")]
    public IActionResult StorageEmissions([FromBody] StorageInputs jsonInputObject)
    {
        try
        {
            StandardOutput standardOutput = new();
            if(jsonInputObject != null)
            {
                if(jsonInputObject.Sector.Equals(Sector.Sheep))
                {
                    throw new CustomAppException("You cannot use this endpoing for sheep, you need to use the StorageEmissions_Sheep endpoint.");
                }
                else
                {
                    string errors = StorageValidityChecks.CheckValidStorageInputs(jsonInputObject, out bool mitigated);
                    if(errors.Equals(""))
                    {
                        StorageEmissions emissions;

                        bool anaerobicDigestion = false;
                        if(jsonInputObject.ManureStorageSystem.Equals(ManureStorageSystem.FYM_AnaerobicDigestion) ||
                            jsonInputObject.ManureStorageSystem.Equals(ManureStorageSystem.Slurry_AnaerobicDigestion))
                        {
                            anaerobicDigestion = true;
                        }

                        if(mitigated)
                        {
                            List<int> methods = jsonInputObject.MitigationMethods == null ? [] : [.. jsonInputObject.MitigationMethods];

                            emissions = Storage.MitigatedstorageManureManagementEmissions(jsonInputObject.Sector, jsonInputObject.AnimalType,
                                jsonInputObject.OrganicMatterType, jsonInputObject.ManureStorageSystem, jsonInputObject.TotalNitrogen,
                                jsonInputObject.TotalAmmoniacalNitrogen, jsonInputObject.VolatileSolids, anaerobicDigestion, methods);

                        }
                        else
                        {
                            emissions = Storage.UnmitigatedstorageManureManagementEmissions(jsonInputObject.Sector, jsonInputObject.AnimalType,
                                jsonInputObject.OrganicMatterType, jsonInputObject.ManureStorageSystem, jsonInputObject.TotalNitrogen,
                                jsonInputObject.TotalAmmoniacalNitrogen, jsonInputObject.VolatileSolids, anaerobicDigestion);
                        }
                        string animal = "Not Set";
                        switch(jsonInputObject.Sector)
                        {
                            case Sector.MinorLivestock:
                                animal = ((MinorLivestockType)jsonInputObject.AnimalType).ToString();
                                break;

                            case Sector.Pigs:
                                animal = ((PigType)jsonInputObject.AnimalType).ToString();
                                break;

                            case Sector.Poultry:
                                animal = ((PoultryType)jsonInputObject.AnimalType).ToString();
                                break;
                            case Sector.Beef:
                                animal = ((BeefCattleType)jsonInputObject.AnimalType).ToString();
                                break;

                            case Sector.Dairy:
                                animal = ((DairyCattle)jsonInputObject.AnimalType).ToString();
                                break;

                        }

                        standardOutput = new StandardOutput(animal, "Storage", jsonInputObject.ManureStorageSystem.ToString(), emissions.ExportEmissions());

                    }
                    else
                    {
                        throw new CustomAppException("Invalid request data:\n" + errors);
                    }
                }
            }
            return Ok(standardOutput);
        }
        catch(Exception ex) { return BadRequest(ex.Message); }
    }

    /// <summary>
    /// Calculates the emission from storage of manure for sheep
    ///
    /// </summary>
    /// <param name="jsonInputObject">Storage Inputs object that contains the sector enumerator, animal type enumerator as an integer,
    /// and a sheep energy balance class object containing the required parameters for manure management emissions calculations</param>
    /// <returns>Standard Output object in JSON serialisation where each Emission has a name (string), units (string) and a value (double).
    /// Further text strings provide information on the source of the emission and the stage within the source.</returns>
    /// <exception cref="CustomAppException">Provides an exception with a message explainign the error that occurred.</exception>
    [Consumes(MediaTypeNames.Application.Json)]
    [Produces(MediaTypeNames.Application.Json)]
    [ProducesResponseType(typeof(StandardOutput), StatusCodes.Status200OK)]
    [HttpPost("manure-management-storage-emissions-sheep")]
    public IActionResult StorageEmissions_Sheep([FromBody] SheepStorageInputs jsonInputObject)
    {
        try
        {
            StandardOutput standardOutput = new();
            if(jsonInputObject != null)
            {
                string errors = StorageValidityChecks.CheckValidStorageInputs_Sheep(jsonInputObject, out bool mitigated);
                if(errors.Equals(""))
                {
                    StorageEmissions emissions;

                    if(mitigated)
                    {
                        List<int> methods = jsonInputObject.MitigationMethods == null ? [] : [.. jsonInputObject.MitigationMethods];

                        emissions = Storage.MitigatedstorageManureManagementEmissionsSheep(jsonInputObject.AnimalType, jsonInputObject.TotalNitrogen, jsonInputObject.TotalAmmoniacalNitrogen,
                            jsonInputObject.SheepEnergyBalance, methods);

                    }
                    else
                    {
                        emissions = Storage.UnmitigatedstorageManureManagementEmissionsSheep(jsonInputObject.AnimalType, jsonInputObject.TotalNitrogen, jsonInputObject.TotalAmmoniacalNitrogen,
                            jsonInputObject.SheepEnergyBalance);
                    }
                    string animal = ((SheepType)jsonInputObject.AnimalType).ToString();

                    standardOutput = new StandardOutput(animal, "Storage", ManureStorageSystem.FYM_FieldHeap.ToString(), emissions.ExportEmissions());

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
