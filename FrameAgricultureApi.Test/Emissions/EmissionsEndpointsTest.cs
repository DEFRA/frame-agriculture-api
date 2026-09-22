using System.Net;
using System.Net.Http.Json;
using System.Text.Json;
using FrameAgricultureApi.Controllers;
using Microsoft.AspNetCore.Mvc.Testing;

namespace FrameAgricultureApi.Test.Emissions;

public class EmissionsEndpointsTest
{
    [Theory]
    [InlineData("/api/CoverCrops/cover-crop-emissions")]
    [InlineData("/CoverCrops/cover-crop-emissions")]
    [InlineData("/v1/CoverCrops/cover-crop-emissions")]
    public async Task Cover_crop_routes_return_reference_emissions(string route)
    {
        await using var factory = new WebApplicationFactory<CoverCropsController>();
        using var client = factory.CreateClient();
        var token = TestContext.Current.CancellationToken;
        var response = await client.PostAsJsonAsync(route, new { cropType = 10, coverCropType = 0 }, token);
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        var result = await response.Content.ReadFromJsonAsync<JsonElement>(token);
        Assert.Equal("Wheat", result.GetProperty("myType").GetString());
        var emissions = result.GetProperty("emissions").EnumerateArray().ToArray();
        Assert.Equal(9, emissions.Length);
        var direct = Assert.Single(emissions, e => e.GetProperty("name").GetString() == "Direct N2O-N");
        Assert.Equal(0.77, direct.GetProperty("value").GetDouble(), 8);
    }

    [Theory]
    [InlineData("get-enumerators")]
    [InlineData("get-grid-locations")]
    [InlineData("get-mitigation-methods")]
    public async Task Data_helpers_load_embedded_lookup_data(string endpoint)
    {
        await using var factory = new WebApplicationFactory<CoverCropsController>();
        using var client = factory.CreateClient();
        var response = await client.GetAsync($"/v1/DataHelper/{endpoint}", TestContext.Current.CancellationToken);
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        var json = await response.Content.ReadFromJsonAsync<JsonElement>(TestContext.Current.CancellationToken);
        Assert.True(json.ValueKind is JsonValueKind.Array or JsonValueKind.Object);
        Assert.NotEqual("[]", json.GetRawText());
        Assert.NotEqual("{}", json.GetRawText());
    }

    [Fact]
    public async Task Malformed_cover_crop_returns_bad_request()
    {
        await using var factory = new WebApplicationFactory<CoverCropsController>();
        using var client = factory.CreateClient();
        var response = await client.PostAsJsonAsync("/v1/CoverCrops/cover-crop-emissions",
            new { cropType = 10, coverCropType = "invalid" }, TestContext.Current.CancellationToken);
        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
    }
}
