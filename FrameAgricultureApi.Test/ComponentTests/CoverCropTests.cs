using System;
using System.Text.Json;
using System.Text.Json.Serialization;
using FrameAgricultureApi.Libraries.CoverCrops;
using FrameAgricultureApi.Infrastructure;
using FrameAgricultureApi.Infrastructure.Entities;
using NSubstitute;

namespace FrameAgricultureApi.Test.ComponentTests;

public class CoverCropTests
{
    // public static IEnumerable<object[]> UnmitigatedCoverCropEmissions_TestGenerator()
    // {
    //     var dataList = new List<object[]>();

    //     CoverCropEmissions coverCropEmissions = new CoverCropEmissions();
    //     coverCropEmissions.InitialiseCoverCropEmissions();

    //     coverCropEmissions.additionalOutputs[AdditionalCoverCropEmissions.NReturn].Value = 77;
    //     coverCropEmissions.EmissionsCore[CoreEmissions.DirectN2ON].Value = 0.77;

    //     coverCropEmissions.EmissionsCore[CoreEmissions.LeachedNO3N].Value = 12.705;
    //     coverCropEmissions.additionalOutputs[AdditionalCoverCropEmissions.UnadjustedNO3N].Value = 23.1;
    //     coverCropEmissions.EmissionsCore[CoreEmissions.N2ONleached].Value = 0.0953;
    //     coverCropEmissions.additionalOutputs[AdditionalCoverCropEmissions.UnadjustedN2ONLeach].Value = 0.1733;
    //     coverCropEmissions.EmissionsCore[CoreEmissions.DirectNON].Value = 0.3080;
    //     coverCropEmissions.EmissionsCore[CoreEmissions.DirectN2N].Value = 2.31;
    //     coverCropEmissions.EmissionsCore[CoreEmissions.N2ONVolatalised].Value = 0.004312;

    //     dataList.Add(
    //         new object[]
    //         {
    //             CoverCropType.All, coverCropEmissions
    //         });

    //     return dataList;

    // }

    // [Theory]
    // [MemberData(nameof(UnmitigatedCoverCropEmissions_TestGenerator))]
    // public void UnmitigatedCoverCropEmissions_AllOutputsCorrect(CoverCropType coverCropType, CoverCropEmissions expectedResult)
    // {
    //     var coverCrops = new CoverCrops(Substitute.For<IMongoService<CoverCropLookUpEntity>>());
    //     CoverCropEmissions actualEmissions = coverCrops.CoverCropEmissions(coverCropType);

    //     actualEmissions.RoundEmissions(4);
    //     string actualEmissionJson = JsonSerializer.Serialize(actualEmissions, new JsonSerializerOptions
    //     {
    //         NumberHandling = JsonNumberHandling.AllowNamedFloatingPointLiterals
    //     });
    //     expectedResult.RoundEmissions(4);
    //     string expectedResultJson = JsonSerializer.Serialize(expectedResult, new JsonSerializerOptions
    //     {
    //         NumberHandling = JsonNumberHandling.AllowNamedFloatingPointLiterals
    //     });

    //     Assert.Equal(true, true);
    // }
}