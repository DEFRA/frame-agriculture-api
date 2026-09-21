using Asp.Versioning;
using FrameAgricultureApi.Components;
using FrameAgricultureApi.CustomErrorHandling;
using FrameAgricultureApi.IOClasses;
using FrameAgricultureApi.Libraries.ManureMassVolume;
using FrameAgricultureApi.Libraries.ValidInputChecks;
using Microsoft.AspNetCore.Mvc;
using System.Net.Mime;
using static FrameAgricultureApi.Enumerators.Enumerators;

namespace FrameAgricultureApi.Controllers;
/// <summary>
/// Manure mass and volume controller
/// </summary>
[ApiController]
[Route("[controller]")]
[Route("api/[controller]")]
[Route("v{v:apiVersion}/[controller]")]
[ApiVersion(1.0)]
[ApiExplorerSettings(IgnoreApi = true)]
public class ManureMassAndVolumeController : ControllerBase
{
    /// <summary>
    /// Calculates the manure mass and volume at all stages of the manure management chain for cattle.
    /// </summary>
    /// <param name="jsonInputObject">Manure mass and volume input object containing the sector enumerator, the animal type enumerator,
    /// the organic matter type enumerator, as array containing the percentage time spent on grazing, yards and housing (summing to 100),
    /// the dry matter intake and digestibility of the diet and a boolean indicating if the manure goes to anaerobic digestion.</param>
    /// <returns>Standard Output object in JSON serialisation where each Emission has a name (string), units (string) and a value (double).
    /// Further text strings provide information on the source of the emission and the stage within the source.</returns>
    /// <exception cref="CustomAppException">Provides an exception with a message explainign the error that occurred</exception>
    [Consumes(MediaTypeNames.Application.Json)]
    [Produces(MediaTypeNames.Application.Json)]
    [ProducesResponseType(typeof(ManureMassVolumeStandardOutput), StatusCodes.Status200OK)]
    [HttpPost("manure-mass-volume-cattle")]
    public IActionResult ManureMassVolume_Cattle([FromBody] ManureMassVolumeCattleInputs jsonInputObject)
    {
        try
        {
            ManureMassVolumeStandardOutput output = new();

            if(jsonInputObject != null)
            {
                string errors = ManureMassVolumeValidityChecks.CheckValidCattleMMVInputs(jsonInputObject);
                if(errors.Equals(""))
                {
                    List<StandardOutput> outputList = [];
                    ManureMassVolumeOutput mmv = new();
                    double grazingtime = (double)jsonInputObject.TimeInLocation.TimeGrazing;
                    double yardtime = (double)jsonInputObject.TimeInLocation.TimeYards;
                    double housingtime = (double)jsonInputObject.TimeInLocation.TimeHousing;
                    bool fym = jsonInputObject.OrganicMatterType.Equals(OrganicMatterType.CattleFYM);
                    string animal = string.Empty;
                    switch(jsonInputObject.Sector)
                    {
                        case Sector.Beef:
                            animal = ((BeefCattleType)jsonInputObject.AnimalType).ToString();
                            break;

                        case Sector.Dairy:
                            animal = ((DairyCattle)jsonInputObject.AnimalType).ToString();
                            break;

                    }

                    mmv = (ManureMassandVolume.ManureMassVolume_Cattle_Grazing(jsonInputObject.CattleDryMatterIntake, jsonInputObject.CattleDryMatterDigestibility,
                        jsonInputObject.CattleDryMatterContent, grazingtime));
                    outputList.Add(new StandardOutput("Manure mass and volume", jsonInputObject.OrganicMatterType.ToString(), "Grazing", mmv.ExportEmissions()));

                    mmv = new();
                    mmv = (ManureMassandVolume.ManureMassVolume_Cattle_Housing(jsonInputObject.CattleDryMatterIntake, jsonInputObject.CattleDryMatterDigestibility,
                        jsonInputObject.CattleDryMatterContent, housingtime, fym, out double massOutHousing, out double volumeOutHousing));
                    outputList.Add(new StandardOutput("Manure mass and volume", jsonInputObject.OrganicMatterType.ToString(), "Housing", mmv.ExportEmissions()));

                    //Note assume that system is either FYM or slurry and not a mixture
                    double massInStorage = 0.0;
                    double volumeInStorage = 0.0;

                    if(fym)
                    {
                        massInStorage = massOutHousing;
                        volumeInStorage = volumeOutHousing;
                    }
                    else
                    {
                        mmv = new();
                        mmv = (ManureMassandVolume.ManureMassVolume_Cattle_Yards(jsonInputObject.CattleDryMatterIntake, jsonInputObject.CattleDryMatterDigestibility, jsonInputObject.CattleDryMatterContent,
                            yardtime, out double massOutYrds,
                           out double volumeOutYards));
                        outputList.Add(new StandardOutput("Manure mass and volume", jsonInputObject.OrganicMatterType.ToString(), "Yards", mmv.ExportEmissions()));
                        massInStorage = massOutYrds + massOutHousing;
                        volumeInStorage = volumeOutYards + volumeOutHousing;
                    }

                    mmv = new();
                    mmv = (ManureMassandVolume.ManureMassVolume_Cattle_Storage(massInStorage, volumeInStorage, fym, jsonInputObject.AnaerobicDigestionAtStorage,
                        out double massSpread, out double volumeSpread));
                    outputList.Add(new StandardOutput("Manure mass and volume", jsonInputObject.OrganicMatterType.ToString(), "Storage", mmv.ExportEmissions()));

                    mmv = new();
                    mmv = (ManureMassandVolume.ManureMassVolume_CattleSheep_Spreading(massSpread, volumeSpread));
                    outputList.Add(new StandardOutput("Manure mass and volume", jsonInputObject.OrganicMatterType.ToString(), "Spreading", mmv.ExportEmissions()));

                    output = new ManureMassVolumeStandardOutput(outputList, jsonInputObject.Sector.ToString(), animal, "ManureMassVolume");

                }
                else
                {
                    throw new CustomAppException("Invalid request data:\n" + errors);
                }
            }
            return Ok(output);
        }
        catch(Exception ex) { return BadRequest(ex.Message); }
    }
    /// <summary>
    /// Calculates the manure mass and volume at all stages of the manure management chain for sheep.
    /// </summary>
    /// <param name="jsonInputObject">Sheep Manure mass and volume input object containing the animal type enumerator,
    /// and the sheep energy balance object containing a suite of parameters including the initial excreta mass and volume.</param>
    /// <returns>Standard Output object in JSON serialisation where each Emission has a name (string), units (string) and a value (double).
    /// Further text strings provide information on the source of the emission and the stage within the source.</returns>
    /// <exception cref="CustomAppException">Provides an exception with a message explainign the error that occurred</exception>
    [Consumes(MediaTypeNames.Application.Json)]
    [Produces(MediaTypeNames.Application.Json)]
    [ProducesResponseType(typeof(ManureMassVolumeStandardOutput), StatusCodes.Status200OK)]
    [HttpPost("manure-mass-volume-sheep")]
    public IActionResult ManureMassVolume_Sheep([FromBody] ManureMassVolumeSheepInputs jsonInputObject)
    {
        try
        {
            ManureMassVolumeStandardOutput output = new();

            if(jsonInputObject != null)
            {
                string errors = ManureMassVolumeValidityChecks.CheckValidSheepMMVInputs(jsonInputObject.AnimalType, jsonInputObject.SheepEnergyBalance);
                if(errors.Equals(""))
                {
                    List<StandardOutput> outputList = [];
                    ManureMassVolumeOutput mmv = new();

                    mmv = (ManureMassandVolume.ManureMassVolume_Sheep_Grazing(jsonInputObject.SheepEnergyBalance));
                    outputList.Add(new StandardOutput("Manure mass and volume", "FYM", "Grazing", mmv.ExportEmissions()));

                    mmv = new();
                    mmv = (ManureMassandVolume.ManureMassVolume_Sheep_Housing(jsonInputObject.SheepEnergyBalance, out double massToStorage, out double volumeToStorage));
                    outputList.Add(new StandardOutput("Manure mass and volume", "FYM", "Housing", mmv.ExportEmissions()));

                    mmv = new();
                    mmv = (ManureMassandVolume.ManureMassVolume_Sheep_Storage(massToStorage, volumeToStorage, out double massSpread, out double volumeSpread));
                    outputList.Add(new StandardOutput("Manure mass and volume", "FYM", "Storage", mmv.ExportEmissions()));

                    mmv = new();
                    mmv = (ManureMassandVolume.ManureMassVolume_Sheep_Spreading(massSpread, volumeSpread));
                    outputList.Add(new StandardOutput("Manure mass and volume", "FYM", "Spreading", mmv.ExportEmissions()));

                    string animal = ((SheepType)jsonInputObject.AnimalType).ToString();

                    output = new ManureMassVolumeStandardOutput(outputList, Sector.Sheep.ToString(), animal, "ManureMassVolume");

                }
                else
                {
                    throw new CustomAppException("Invalid request data:\n" + errors);
                }
            }
            return Ok(output);
        }
        catch(Exception ex) { return BadRequest(ex.Message); }
    }
    /// <summary>
    /// Calculates the manure mass and volume at all stages of the manure management chain for pigs, poultry and minor livestock.
    /// </summary>
    /// <param name="jsonInputObject">Manure mass and volume input object containing the sector enumerator, the animal type enumerator,
    /// the manure type enumerator and an optional parameter indicating the proportion of excreta deposited outdoors.</param>
    /// <returns>Standard Output object in JSON serialisation where each Emission has a name (string), units (string) and a value (double).
    /// Further text strings provide information on the source of the emission and the stage within the source.</returns>
    /// <exception cref="CustomAppException">Provides an exception with a message explainign the error that occurred</exception>
    [Consumes(MediaTypeNames.Application.Json)]
    [Produces(MediaTypeNames.Application.Json)]
    [ProducesResponseType(typeof(ManureMassVolumeStandardOutput), StatusCodes.Status200OK)]
    [HttpPost("manure-mass-volume-otherlivestock")]
    public IActionResult ManureMassVolume_PigsPoultryMinorLivestock([FromBody] ManureMassVolumeInputs jsonInputObject)
    {
        try
        {
            ManureMassVolumeStandardOutput output = new();

            if(jsonInputObject != null)
            {
                string errors = ManureMassVolumeValidityChecks.CheckValidMMVInputs(jsonInputObject);
                if(errors.Equals(""))
                {
                    List<StandardOutput> outputList = [];
                    ManureMassVolumeOutput mmv = new();

                    double outdoorpercent = jsonInputObject.ExcretaOutdoorPercentage ?? 100.0;
                    bool fym = true;
                    string animal = string.Empty;
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

                        case Sector.Sheep:
                            animal = ((SheepType)jsonInputObject.AnimalType).ToString();
                            break;
                        case Sector.Beef:
                            animal = ((BeefCattleType)jsonInputObject.AnimalType).ToString();
                            break;

                        case Sector.Dairy:
                            animal = ((DairyCattle)jsonInputObject.AnimalType).ToString();
                            break;

                    }
                    mmv = (ManureMassandVolume.ManureMassVolume_PigPoultryMinorLivestock_Outdoor(jsonInputObject.Sector, jsonInputObject.AnimalType, jsonInputObject.OrganicMatterType, outdoorpercent));
                    outputList.Add(new StandardOutput("Manure mass and volume", jsonInputObject.OrganicMatterType.ToString(), "Grazing", mmv.ExportEmissions()));

                    mmv = new();
                    mmv = (ManureMassandVolume.ManureMassVolume_PigPoultryMinorLivestock_Housing(jsonInputObject.Sector, jsonInputObject.AnimalType, jsonInputObject.OrganicMatterType, (100.0 - outdoorpercent)));
                    outputList.Add(new StandardOutput("Manure mass and volume", jsonInputObject.OrganicMatterType.ToString(), "Housing", mmv.ExportEmissions()));

                    mmv = new();
                    mmv = (ManureMassandVolume.ManureMassVolume_PigPoultryMinorLivestock_Storage(jsonInputObject.Sector, jsonInputObject.AnimalType, jsonInputObject.OrganicMatterType, (100.0 - outdoorpercent)));
                    outputList.Add(new StandardOutput("Manure mass and volume", jsonInputObject.OrganicMatterType.ToString(), "Storage", mmv.ExportEmissions()));

                    mmv = new();
                    mmv = (ManureMassandVolume.ManureMassVolume_PigPoultryMinorLivestock_Spreading(jsonInputObject.Sector, jsonInputObject.AnimalType, jsonInputObject.OrganicMatterType, (100.0 - outdoorpercent)));
                    outputList.Add(new StandardOutput("Manure mass and volume", jsonInputObject.OrganicMatterType.ToString(), "Spreading", mmv.ExportEmissions()));

                    output = new ManureMassVolumeStandardOutput(outputList, jsonInputObject.Sector.ToString(), animal, "ManureMassVolume");

                }
                else
                {
                    throw new CustomAppException("Invalid request data:\n" + errors);
                }
            }
            return Ok(output);
        }
        catch(Exception ex) { return BadRequest(ex.Message); }
    }
}
