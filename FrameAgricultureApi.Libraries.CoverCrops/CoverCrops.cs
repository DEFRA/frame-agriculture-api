using System;
using FrameAgricultureApi.Generic;

namespace FrameAgricultureApi.Libraries.CoverCrops;


/// <summary>
/// The Cover Crops Component class
/// </summary>
public class CoverCrops
{
    /// <summary>
    /// Calculates emissions from cover crops
    /// </summary>
    /// <param name="coverCropType">Enumerator indicating the cover crop type</param>
    /// <returns>An object of type CoverCropEmissions</returns>
    public static CoverCropEmissions CoverCropEmissions(CoverCropType coverCropType)
    {
        CoverCropEmissions myEmissions = new();
        myEmissions.InitialiseCoverCropEmissions();

        double additionalNReturn = 10;//CropLookup.RetrieveAdditionalNReturn_CoverCrop(coverCropType);
        myEmissions.additionalOutputs[AdditionalCoverCropEmissions.Nreturn].Value = additionalNReturn;

        myEmissions.EmissionsCore[CoreEmissions.DirectN2ON].Value = GenericEquations.Emission_PercentageEF(additionalNReturn, 10); // GenericEquations.Emission_PercentageEF(additionalNReturn, CropLookup.RetrieveIPCCEmissionFactor1());
        double adjustedFracLeach = 10;// CropLookup.RetrieveFracLeach() * ((100 - CropLookup.RetrievedCoverCropFracLeachReduction()) * HelperFunctions.Percent_to_Proportion);
        myEmissions.EmissionsCore[CoreEmissions.LeachedNO3N].Value = GenericEquations.Emission_PercentageEF(additionalNReturn, adjustedFracLeach);
        myEmissions.additionalOutputs[AdditionalCoverCropEmissions.unadjustedNO3N].Value = GenericEquations.Emission_PercentageEF(additionalNReturn, 10);// GenericEquations.Emission_PercentageEF(additionalNReturn, CropLookup.RetrieveFracLeach());
        myEmissions.EmissionsCore[CoreEmissions.N2ONleached].Value = GenericEquations.Emission_PercentageEF(myEmissions.EmissionsCore[CoreEmissions.LeachedNO3N].Value, 10); // GenericEquations.Emission_PercentageEF(myEmissions.EmissionsCore[CoreEmissions.LeachedNO3N].Value, CropLookup.RetrieveIPCCEmissionFactor5());
        myEmissions.additionalOutputs[AdditionalCoverCropEmissions.unadjustedN2ONLeach].Value = GenericEquations.Emission_PercentageEF(myEmissions.additionalOutputs[AdditionalCoverCropEmissions.unadjustedNO3N].Value, 10); //GenericEquations.Emission_PercentageEF(myEmissions.additionalOutputs[AdditionalCoverCropEmissions.unadjustedNO3N].Value, CropLookup.RetrieveIPCCEmissionFactor5());
        myEmissions.EmissionsCore[CoreEmissions.DirectNON].Value = GenericEquations.Emission_ProportionEF(myEmissions.EmissionsCore[CoreEmissions.DirectN2ON].Value, 10); //GenericEquations.Emission_ProportionEF(myEmissions.EmissionsCore[CoreEmissions.DirectN2ON].Value, CropLookup.RetrieveRatioNON());
        myEmissions.EmissionsCore[CoreEmissions.DirectN2N].Value = GenericEquations.Emission_ProportionEF(myEmissions.EmissionsCore[CoreEmissions.DirectN2ON].Value, 10); //GenericEquations.Emission_ProportionEF(myEmissions.EmissionsCore[CoreEmissions.DirectN2ON].Value, CropLookup.RetrieveRatioN2N());
        myEmissions.EmissionsCore[CoreEmissions.N2ONvolatalised].Value = GenericEquations.Emission_PercentageEF(myEmissions.EmissionsCore[CoreEmissions.DirectNON].Value, 10); //GenericEquations.Emission_PercentageEF(myEmissions.EmissionsCore[CoreEmissions.DirectNON].Value, CropLookup.RetrieveIPCCEmissionFactor4());

        return myEmissions;
    }
}