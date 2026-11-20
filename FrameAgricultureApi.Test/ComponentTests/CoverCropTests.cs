using FrameAgricultureApi.Core.Outputs;
using System.Text.Json;
using System.Text.Json.Serialization;
using FrameAgricultureApi.Controllers;
using FrameAgricultureApi.Core.Enums;
using Microsoft.AspNetCore.Mvc;
using FrameAgricultureApi.ComponentLogic.CoverCropComponent;
using FrameAgricultureApi.Infrastructure;
using FrameAgricultureApi.Infrastructure.Entities;
using Microsoft.Extensions.Logging.Abstractions;
using MongoDB.Driver;
using NSubstitute;

namespace FrameAgricultureApi.Test.ComponentTests;

/// <summary>
/// Regression tests for cover crop emissions grouped by pollutant and pathway.
/// </summary>
public class CoverCropTests
{

}