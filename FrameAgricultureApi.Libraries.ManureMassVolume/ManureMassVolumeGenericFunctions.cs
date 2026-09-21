using FrameAgricultureApi.Generic;
using FrameAgricultureApi.Libraries.LookUps;
using static FrameAgricultureApi.Enumerators.Enumerators;

namespace FrameAgricultureApi.Libraries.ManureMassVolume;

public static class ManureMassVolumeGenericFunctions
{
    /// <summary>
    /// Manure Mass and Volume at stage for pigs
    /// </summary>
    /// <param name="component">Enumerator indicating component</param>
    /// <param name="organicMatterType">Ebnumerator for manure type</param>
    /// <param name="pig">Pig type enumerator</param>
    /// <param name="entering">Boolean indicating if entering or leaving stage</param>
    /// <param name="mass">Boolean indicating of doing mass (true) or volume (false)</param>
    /// <returns>Manure or excreta volume entering or leaving stage (litres)</returns>
    public static double ManureMassVolume_Pigs(Sector sector, ComponentType component, OrganicMatterType organicMatterType, PigType pig, bool entering, bool mass, bool indoor)
    {
        //Todo this disagrees with the spreadsheet
        double coefficient = ManureMassVolumeLookup.Instance.RetrieveCoefficient(sector, (int)pig, component, organicMatterType, entering, mass, indoor);
        //Todo this is different from spreadsheet
        double DMI = ManureMassVolumeLookup.Instance.RetrieveDMI_Pig(pig, indoor);
        double weighting = 1.0;

        //Manure digestate weighting - currently 1 so no need to implment as yet
        //if (!In && (organicMatterType.Equals(OrganicMatterType.DigestatePigFYM) || organicMatterType.Equals(OrganicMatterType.DigestatePigSlurry)))
        //{
        //    ManureMassVolumeLookup.Instance.RetrieveDigestateWeighting(organicMatterType, bool pig);
        //}

        return coefficient * (DMI / 365.0) * weighting * 365.0;
    }
    /// <summary>
    /// Manure Mass and Volume at stage for poultry
    /// </summary>
    /// <param name="component">Enumerator indicating component</param>
    /// <param name="organicMatterType">Ebnumerator for manure type</param>
    /// <param name="poultry">Poultry type enumerator</param>
    /// <param name="entering">Boolean indicating if entering or leaving stage</param>
    /// <param name="mass">Boolean indicating of doing mass (true) or volume (false)</param>
    /// <returns>Manure or excreta volume entering or leaving stage (litres)</returns>
    //Todo sector shouldn't be an input?
    public static double ManureMassVolume_Poultry(Sector sector, ComponentType component, OrganicMatterType organicMatterType, PoultryType poultry, bool entering, bool mass, bool indoor)
    {
        // Todo this is different in spreadsheet when poultrytype = growingPullets, component = Housing, organicMatterType = PoultryManureLayer, entering = false, mass = false, indoor = false
        double coefficient = ManureMassVolumeLookup.Instance.RetrieveCoefficient(sector, (int)poultry, component, organicMatterType, entering, mass, indoor);
        // Todo this is different in spreadsheet
        double DMI = ManureMassVolumeLookup.Instance.RetrieveDMI_Poultry(poultry);
        double weighting = 1.0;

        // Todo delete this comment ?
        //Manure digestate weighting - currently 1 so no need to implment as yet
        //if (!In && organicMatterType.Equals(OrganicMatterType.DigestatePoulryManure))
        //{
        //    ManureMassVolumeLookup.Instance.RetrieveDigestateWeighting(organicMatterType, bool poultry);
        //}

        // Todo
        return coefficient * (DMI / 365.0) * weighting * 365.0;
    }
    /// <summary>
    /// Manure Mass and Volume at stage for minr livestock
    /// </summary>
    /// <param name="component">Enumerator indicating component</param>
    /// <param name="organicMatterType">Enumerator for manure type</param>
    /// <param name="minrLivestockType">Minor livestock type enumerator</param>
    /// <param name="entering">Boolean indicating if entering or leaving stage</param>
    /// <param name="mass">Boolean indicating of doing mass (true) or volume (false)</param>
    /// <returns>Manure or excreta volume entering or leaving stage (litres)</returns>
    public static double ManureMassVolume_MinorLivestock(Sector sector, ComponentType component, OrganicMatterType organicMatterType, MinorLivestockType minorLivestockType, bool entering, bool mass, bool indoor)
    {
        double coefficient = ManureMassVolumeLookup.Instance.RetrieveCoefficient(sector, (int)minorLivestockType, component, organicMatterType, entering, mass, indoor);
        double DMI = ManureMassVolumeLookup.Instance.RetrieveDMI_MinorLivestock(minorLivestockType);
        double weighting = 1.0;

        //Manure digestate weighting - currently 1 so no need to implment as yet
        //if (!In && (organicMatterType.Equals(OrganicMatterType.DigestateMinorLivestockFYM) || organicMatterType.Equals(OrganicMatterType.DigestateMinorLivestockSlurry)))
        //{
        //    ManureMassVolumeLookup.Instance.RetrieveDigestateWeighting(organicMatterType, bool MinorLivestock);
        //}

        return coefficient * (DMI / 365.0) * weighting * 365.0;
    }
    /// <summary>
    /// Calculation of manure mass and volume leaving cattle housing
    /// </summary>
    /// <param name="urineMassIn">mass of urine entering housing (kg)</param>
    /// <param name="faecesMassIn">mass of faeces entering housing</param>
    /// <param name="urineVolumeIn">volume of urine entering housing</param>
    /// <param name="faecesVolumeIn">volume of faeces entering housing</param>
    /// <param name="fym">boolean indicating if fym (true) or not (false)</param>
    /// <returns>mass (kg) and volume (litres) or manure leaving housing (as double[2]) </returns>
    public static double[] ManureMassVolume_CattleHousing(double urineMassIn, double faecesMassIn, double urineVolumeIn,
        double faecesVolumeIn, bool fym)
    {
        double[] retmassvolume = new double[2];
        if(fym)
        {
            double strawaddition = (urineMassIn / ManureMassVolumeGenericEquationParameters.strawUrineAbsorbance);
            retmassvolume[0] = urineMassIn + faecesMassIn + strawaddition;
            retmassvolume[1] = (strawaddition / ManureMassVolumeGenericEquationParameters.bulkDensityCompressedStraw) * 1000.0;
        }
        else
        {
            retmassvolume[0] = urineMassIn + faecesMassIn;
            retmassvolume[1] = urineVolumeIn + faecesVolumeIn;
        }
        return retmassvolume;
    }
    /// <summary>
    /// Calculation of manure mass and volume leaving storage for cattle systems
    /// </summary>
    /// <param name="massIn">Mass of manure entering storage</param>
    /// <param name="volumeIn">Volume of manure entering storage</param>
    /// <param name="fym">boolean indicating if fym (true) or not (false)</param>
    /// <param name="anaerobicdigestion"> boolean indicating if anaerobic digestion (true) or not (false)</param>
    /// <returns>mass (kg) and volume (litres) or manure leaving storage (as double[2]) </returns>
    public static double[] ManureMassVolume_CattleStorage(double massIn, double volumeIn, bool fym, bool anaerobicdigestion)
    {
        double[] retmassvolume = new double[2];
        if(anaerobicdigestion)
        {
            retmassvolume[0] = massIn * ManureMassVolumeGenericEquationParameters.cattleAnaerobicDigestionMassLossAdjustment;
            retmassvolume[1] = volumeIn;
        }
        else
        {
            if(fym)
            {
                //Mass
                retmassvolume[0] = massIn * (1 - (ManureMassVolumeGenericEquationParameters.reductionFYMComposting * HelperFunctions.Percent_to_Proportion));
                //Volume
                retmassvolume[1] = volumeIn * (1 - (ManureMassVolumeGenericEquationParameters.reductionFYMComposting * HelperFunctions.Percent_to_Proportion));
            }
            else
            {
                retmassvolume[1] = volumeIn * ManureMassVolumeGenericEquationParameters.slurryDilutionRatio;
                retmassvolume[0] = massIn + (retmassvolume[1] - volumeIn);
            }
        }
        return retmassvolume;
    }

    //Sheep not included as taken directly fomr energy balance object provided as input to MMv calcualtions for sheep

}
