using FrameAgricultureApi.CustomErrorHandling;
using FrameAgricultureApi.DataTransferObjects;
using FrameAgricultureApi.Generic;
using System.Reflection;
using static FrameAgricultureApi.Enumerators.Enumerators;

namespace FrameAgricultureApi.Libraries.LookUps;

/// <summary>
/// Singleton class to provide mitigation method information based on the method IDs
/// </summary>
public class MitigationMethodLookup
{
    /// <summary>
    /// Private Singleto instance of the class
    /// </summary>
    private static MitigationMethodLookup? _instance;

    private readonly Dictionary<OrganicMatterType, OrganicMatterSourceType> _organicMatterSources;
    private readonly Dictionary<OrganicMatterType, OrganicMatterState> _organicMatterStates;
    private readonly Dictionary<int, List<int>> MitigationConflicts;
    private readonly List<MitigationMethod> _mitigationMethods;
    /// <summary>
    /// Retrieve list of mitigaitn methods
    /// </summary>
    /// <returns>LIst of mitigation methods with the method IDs and component</returns>
    public List<MitigationMethodDto> GetAllMitigationMethods()
    {

        var mitigationMethodsDto = _mitigationMethods.DistinctBy(m => m.MethodID).Select(method => new MitigationMethodDto { MethodName = method.Methodname, MethodID = method.MethodID, ComponentName = ((ComponentType)method.Component).ToString(), ComponentID = method.Component }).ToList();
        return mitigationMethodsDto;
    }

    /// <summary>
    /// Publci Instance of the class - ensures only one instance of the calss exists
    /// </summary>
    public static MitigationMethodLookup Instance
    {
        get
        {
            _instance ??= new MitigationMethodLookup();
            return _instance;
        }
    }

    /// <summary>
    /// Constructor - reads information from LUT files and stores in appropriate dictionary
    /// </summary>
    private MitigationMethodLookup()
    {
        string errors = "";
        _mitigationMethods = [];
        _organicMatterSources = [];
        _organicMatterStates = [];
        MitigationConflicts = [];

        errors += LoadFertiliserMitigation();
        errors += LoadResidueMitigation();
        errors += LoadYardMitigation();
        errors += LoadHousingMitigation();
        errors += LoadStorageMitigation();
        errors += LoadSpreadingMitigation();
        errors += LoadOrganicMatterSourceState();
        errors += LoadMitigationConflicts();

        if(!errors.Equals(""))
            throw new CustomAppException(errors);

    }
    /// <summary>
    /// Read fertiliser mitigation data from file
    /// </summary>
    /// <returns>Error message as string</returns>
    private string LoadFertiliserMitigation()
    {
        MemoryStream memoryStream = HelperFunctions.GetLUT(Assembly.GetExecutingAssembly(), "FLEA_FertiliserMitigation.dat");
        try
        {
            using BinaryReader binaryReader = new(memoryStream);
            while(binaryReader.BaseStream.Position < binaryReader.BaseStream.Length)
            {
                int uID = binaryReader.ReadInt32();
                int methodID = binaryReader.ReadInt32();
                string name = binaryReader.ReadString();
                int component = binaryReader.ReadInt32();
                int enumSource = binaryReader.ReadInt32();
                double methane = binaryReader.ReadDouble();
                double ammonia = binaryReader.ReadDouble();
                double nitrousoxide = binaryReader.ReadDouble();
                double mineralisation = binaryReader.ReadDouble();
                double leaching = binaryReader.ReadDouble();

                MitigationMethod newFertiliserMitigation = new(uID, methodID,
                    name, component, enumSource, 0, 0, methane, ammonia, nitrousoxide, mineralisation, leaching);

                _mitigationMethods.Add(newFertiliserMitigation);

            }
            return "";
        }
        catch(Exception ex)
        {
            return "Error adding fertiliser mitigation data: " + ex.Message + ".\n";
        }
    }
    /// <summary>
    /// Load residue mitigation look up table
    /// </summary>
    /// <returns>erorr list as string</returns>
    private string LoadResidueMitigation()
    {
        MemoryStream memoryStream = HelperFunctions.GetLUT(Assembly.GetExecutingAssembly(), "FLEA_ResidueMitigation.dat");
        try
        {
            using BinaryReader binaryReader = new(memoryStream);
            while(binaryReader.BaseStream.Position < binaryReader.BaseStream.Length)
            {
                int uID = binaryReader.ReadInt32();
                int methodID = binaryReader.ReadInt32();
                string name = binaryReader.ReadString();
                int component = binaryReader.ReadInt32();
                double methane = binaryReader.ReadDouble();
                double ammonia = binaryReader.ReadDouble();
                double nitrousoxide = binaryReader.ReadDouble();
                double mineralisation = binaryReader.ReadDouble();
                double leaching = binaryReader.ReadDouble();

                MitigationMethod newResidueMitigation = new(uID, methodID,
                    name, component, 0, 0, 0, methane, ammonia, nitrousoxide, mineralisation, leaching);

                _mitigationMethods.Add(newResidueMitigation);
            }
            return "";
        }
        catch(Exception ex)
        {
            return "Error adding residue mitigation data: " + ex.Message + ".\n";
        }
    }
    /// <summary>
    /// Load Yard mitigation information
    /// </summary>
    /// <returns>errors as string</returns>
    private string LoadYardMitigation()
    {
        MemoryStream memoryStream = HelperFunctions.GetLUT(Assembly.GetExecutingAssembly(), "FLEA_YardMitigation.dat");
        try
        {
            using BinaryReader binaryReader = new(memoryStream);
            while(binaryReader.BaseStream.Position < binaryReader.BaseStream.Length)
            {
                int uID = binaryReader.ReadInt32();
                int methodID = binaryReader.ReadInt32();
                string name = binaryReader.ReadString();
                int component = binaryReader.ReadInt32();
                double methane = binaryReader.ReadDouble();
                double ammonia = binaryReader.ReadDouble();
                double nitrousoxide = binaryReader.ReadDouble();
                double mineralisation = binaryReader.ReadDouble();
                double leaching = binaryReader.ReadDouble();

                MitigationMethod newYardMitigation = new(uID, methodID,
                    name, component, 0, 0, 0, methane, ammonia, nitrousoxide, mineralisation, leaching);

                _mitigationMethods.Add(newYardMitigation);
            }
            return "";
        }
        catch(Exception ex)
        {
            return "Error adding yard mitigation data: " + ex.Message + ".\n";
        }
    }
    /// <summary>
    /// Load housing mitigation information
    /// </summary>
    /// <returns>errors as string</returns>
    private string LoadHousingMitigation()
    {
        MemoryStream memoryStream = HelperFunctions.GetLUT(Assembly.GetExecutingAssembly(), "FLEA_HousingMitigation.dat");
        try
        {
            using BinaryReader binaryReader = new(memoryStream);
            while(binaryReader.BaseStream.Position < binaryReader.BaseStream.Length)
            {
                int uID = binaryReader.ReadInt32();
                int methodID = binaryReader.ReadInt32();
                string name = binaryReader.ReadString();
                int enumComponent = binaryReader.ReadInt32();
                int enumOrganicMatterType = binaryReader.ReadInt32();
                double methane = binaryReader.ReadDouble();
                double ammonia = binaryReader.ReadDouble();
                double nitrousoxide = binaryReader.ReadDouble();
                double mineralisation = binaryReader.ReadDouble();
                double leaching = binaryReader.ReadDouble();

                MitigationMethod newHousingMitigation = new(uID, methodID, name, enumComponent, 0, 0, enumOrganicMatterType, methane, ammonia, nitrousoxide, mineralisation, leaching);
                _mitigationMethods.Add(newHousingMitigation);

            }
            return "";
        }
        catch(Exception ex)
        {
            return "Error adding housing mitigation data: " + ex.Message + ".\n";
        }
    }
    /// <summary>
    /// Load storage mitigation information
    /// </summary>
    /// <returns>errors as string</returns>
    private string LoadStorageMitigation()
    {
        MemoryStream memoryStream = HelperFunctions.GetLUT(Assembly.GetExecutingAssembly(), "FLEA_StorageMitigation.dat");
        try
        {
            using BinaryReader binaryReader = new(memoryStream);
            while(binaryReader.BaseStream.Position < binaryReader.BaseStream.Length)
            {
                int uID = binaryReader.ReadInt32();
                int methodID = binaryReader.ReadInt32();
                string name = binaryReader.ReadString();
                int enumComponent = binaryReader.ReadInt32();
                int enumState = binaryReader.ReadInt32();
                double methane = binaryReader.ReadDouble();
                double ammonia = binaryReader.ReadDouble();
                double nitrousoxide = binaryReader.ReadDouble();
                double mineralisation = binaryReader.ReadDouble();
                double leaching = binaryReader.ReadDouble();

                MitigationMethod newStorageMitigation = new(uID, methodID, name, enumComponent, 0, enumState, 0, methane, ammonia, nitrousoxide, mineralisation, leaching);
                _mitigationMethods.Add(newStorageMitigation);
            }
            return "";
        }
        catch(Exception ex)
        {
            return "Error adding storage mitigation data: " + ex.Message + ".\n";
        }
    }
    /// <summary>
    /// Load spreading mitigation
    /// </summary>
    /// <returns>error as string</returns>
    private string LoadSpreadingMitigation()
    {
        MemoryStream memoryStream = HelperFunctions.GetLUT(Assembly.GetExecutingAssembly(), "FLEA_SpreadingMitigation.dat");
        try
        {
            using BinaryReader binaryReader = new(memoryStream);
            while(binaryReader.BaseStream.Position < binaryReader.BaseStream.Length)
            {
                int uID = binaryReader.ReadInt32();
                int methodID = binaryReader.ReadInt32();
                string name = binaryReader.ReadString();
                string sector = binaryReader.ReadString();
                int enumComponent = binaryReader.ReadInt32();
                int enumSource = binaryReader.ReadInt32();
                int enumState = binaryReader.ReadInt32();
                int enumType = binaryReader.ReadInt32();
                double methane = binaryReader.ReadDouble();
                double ammonia = binaryReader.ReadDouble();
                double nitrousoxide = binaryReader.ReadDouble();
                double mineralisation = binaryReader.ReadDouble();
                double leaching = binaryReader.ReadDouble();

                MitigationMethod newspreadingMitigation = new(uID, methodID, name, enumComponent, enumSource, enumState, enumType, methane, ammonia, nitrousoxide, mineralisation, leaching);
                _mitigationMethods.Add(newspreadingMitigation);
            }
            return "";
        }
        catch(Exception ex)
        {
            return "Error adding spreading mitigation data: " + ex.Message + ".\n";
        }
    }
    /// <summary>
    /// Load organic matter source state information
    /// </summary>
    /// <returns>errors as string</returns>
    private string LoadOrganicMatterSourceState()
    {
        MemoryStream memoryStream = HelperFunctions.GetLUT(Assembly.GetExecutingAssembly(), "FLEA_OrganicMatterSourceState.dat");
        try
        {
            using BinaryReader binaryReader = new(memoryStream);
            while(binaryReader.BaseStream.Position < binaryReader.BaseStream.Length)
            {
                string _discard = binaryReader.ReadString();
                int enumOrganicMatterType = binaryReader.ReadInt32();
                _discard = binaryReader.ReadString();
                int enumOrganicMatterSource = binaryReader.ReadInt32();
                _discard = binaryReader.ReadString();
                int enumState = binaryReader.ReadInt32();

                _organicMatterSources.Add((OrganicMatterType)enumOrganicMatterType, (OrganicMatterSourceType)enumOrganicMatterSource);
                _organicMatterStates.Add((OrganicMatterType)enumOrganicMatterType, (OrganicMatterState)enumState);
            }
            return "";
        }
        catch(Exception ex)
        {
            return "Error adding Source state information: " + ex.Message + ".\n";
        }

    }
    /// <summary>
    /// Load Mitigation Conflicst information
    /// </summary>
    /// <returns>errors as string</returns>
    private string LoadMitigationConflicts()
    {
        MemoryStream memoryStream = HelperFunctions.GetLUT(Assembly.GetExecutingAssembly(), "FLEA_MitigationConflicts.dat");
        try
        {
            using BinaryReader binaryReader = new(memoryStream);
            while(binaryReader.BaseStream.Position < binaryReader.BaseStream.Length)
            {
                int key = binaryReader.ReadInt32();
                int count = binaryReader.ReadInt32();
                List<int> conflicts = [];
                if(count > 0)
                {
                    for(int i = 0; i < count; i++)
                    {
                        int conflict = binaryReader.ReadInt32();
                        conflicts.Add(conflict);
                    }
                }
                MitigationConflicts.Add(key, conflicts);
            }
            return "";
        }
        catch(Exception ex)
        {
            return "Error loading mitigation conflicts: " + ex.Message + ".\n";
        }
    }
    /// <summary>
    /// Produces list of conflicting methods based on ascendng traversal of methods
    /// For source, state and type - use 0 if not relevant
    /// </summary>
    /// <param name="methods">The list of method UIDs to be checked</param>
    /// <returns>List of Method UIDs to omit from mitigation calculation</returns>
    private List<int> DetermineConflicts(Dictionary<int, int> methods)
    {
        List<int> omit = [];
        List<int> conflicts = [];

        foreach(int methodID in methods.Keys)
        {
            //Add conflicts if not already present
            if(!conflicts.Contains(methodID))
            {
                //no conflict so add in any future conflicts
                foreach(int conflict in MitigationConflicts[methodID])
                {
                    if(!conflicts.Contains(conflict))
                    {
                        conflicts.Add(conflict);
                    }
                }
            }
            else
            {
                //conflicted so omit method UID
                omit.Add(methods[methodID]);
            }
        }
        return omit;
    }
    /// <summary>
    /// Method to return method ID from UID
    /// </summary>
    /// <param name="method"> method ID for mitigaiton method</param>
    /// <param name="component">component enumerator (integer)</param>
    /// <param name="sourcetype">sourcetype enumerator (integer)</param>
    /// <param name="sourcestate">source state enumerator (integer)</param>
    /// <param name="organicmattertype">organic matter type enumerator (integer)</param>
    /// <returns>mitigation method uID as integer, where uID links method to component, source, state and organicmatterype as relevant</returns>
    private int GetMethodUID(int method, int component, int sourcetype, int sourcestate, int organicmattertype)
    {
        int methodUID = 0;

        List<MitigationMethod> filtered = _mitigationMethods.FindAll(m => m.MethodID.Equals(method) && (m.Component.Equals(component) || m.Component.Equals((int)ComponentType.NotSet))
        && (m.Sourcetype.Equals(sourcetype) || m.Sourcetype.Equals((int)OrganicMatterSourceType.NotSet))
        && (m.Sourcestate.Equals(sourcestate) || m.Sourcestate.Equals((int)OrganicMatterState.NotSet))
        && (m.Organicmattertype.Equals(organicmattertype) || m.Organicmattertype.Equals((int)OrganicMatterSourceType.NotSet)));

        if(filtered.Count > 1)
        {
            throw new Exception("Non-unique mitigation methods for method ID " + method.ToString() + " at " + ((ComponentType)component).ToString() + ".");
        }
        else
        {
            if(filtered.Count == 0)
            { return methodUID; }
            else
            { return filtered[0].UID; }
        }
    }
    /// <summary>
    /// Create dictionary linking methodID to uID for relevant component, source, state and organic matter type
    /// </summary>
    /// <param name="methodList">the list of method IDs</param>
    /// <param name="component">component enumerator (integer)</param>
    /// <param name="sourcetype">sourcetype enumerator (integer)</param>
    /// <param name="sourcestate">source state enumerator (integer)</param>
    /// <param name="organicmattertype">organic matter type enumerator (integer)</param>
    /// <returns>Dictionary of uIDs indexed by methodID</returns>
    private Dictionary<int, int> GetMitigationMethodList(List<int> methodList, int component, int sourcetype, int sourcestate, int organicmattertype)
    {
        Dictionary<int, int> result = [];
        foreach(int methodID in methodList)
        {
            int uID = GetMethodUID(methodID, component, sourcetype, sourcestate, organicmattertype);
            if(!uID.Equals(0))
            {
                result.Add(methodID, uID);
            }
        }
        return result;
    }
    /// <summary>
    /// Calculate fertiliser mitigation mmultipliers
    /// </summary>
    /// <param name="methods">list of method UIDs</param>
    /// <param name="component">component enumerator (integer)</param>
    /// <param name="source">sourcetype enumerator (integer)</param>
    /// <param name="state">source state enumerator (integer)</param>
    /// <param name="organicmattertype">organic matter type enumerator (integer)</param>
    /// <param name="MitigationMultipliers">Mitigation Multipliers object</param>
    /// <param name="MitigationErrors">Out variable for returning errors encoutnered as a string</param>
    /// <returns>True if successful, false if errors</returns>
    public bool CalculateMitigation(List<int> methods, int component, int source, int state, int organicmattertype, out MitigationFactor MitigationMultipliers, out string MitigationErrors)
    {
        Dictionary<int, int> theMethods = GetMitigationMethodList(methods, component, source, state, organicmattertype);
        List<int> omit = DetermineConflicts(theMethods);
        List<string> errors = [];
        MitigationMultipliers = new MitigationFactor(true);
        foreach(int key in theMethods.Keys)
        {
            int method = theMethods[key];
            if(!omit.Contains(method))
            {
                MitigationMethod? item = _mitigationMethods.Find(m => m.UID.Equals(method));
                if(item != null)
                {
                    MitigationFactor reductionFactors = item.ReductionFactors;
                    MitigationMultipliers.CalculateMultiplier(reductionFactors);
                }
                else
                {
                    errors.Add("Method " + method.ToString() + " not found in lookup tables.\n");
                }
            }
            else
            {
                errors.Add("Conflict with method " + method.ToString() + ", this method has been omitted from the mitigation calcualtion.\n");
            }

        }

        if(errors.Count > 0)
        {
            MitigationErrors = "";
            foreach(string error in errors)
            {
                MitigationErrors += error;
            }
            return false;
        }
        else
        { MitigationErrors = string.Empty; return true; }
    }

    /// <summary>
    /// Retrieve the source of organice matter base don organic matter type
    /// </summary>
    /// <param name="type">Organic matter type enumerator</param>
    /// <returns>organice matter source enumerator</returns>
    public OrganicMatterSourceType RetrieveSource(OrganicMatterType type)
    {
        return _organicMatterSources[type];
    }
    /// <summary>
    /// Retrieve organice matter state based on type
    /// </summary>
    /// <param name="type">Organice matter type as enumerator</param>
    /// <returns>Organice matter state enumerator</returns>
    public OrganicMatterState RetrieveState(OrganicMatterType type)
    {
        return _organicMatterStates[type];
    }

}
///// <summary>
///// Fertiliser mitigation method class - stores reductions for fertiliser mitigation methods
///// </summary>
//public class FertiliserMitigation
//{
//    /// <summary>
//    /// The unique identifieer for the method, component, source, etc.
//    /// </summary>
//    public int UID { get; set; }
//    /// <summary>
//    /// The (non-unique) ID of the method
//    /// </summary>
//    public int methodID { get; set; }
//    /// <summary>
//    /// The name of the mitigaiton measure
//    /// </summary>
//    public string name { get; set; }
//    /// <summary>
//    /// The component type for the the mitigaiton method.
//    /// </summary>
//    public ComponentType component { get; set; }
//    /// <summary>
//    /// The fertiliser type for which the method applies
//    /// </summary>
//    public FertiliserType fertiliser { get; set; }
//    /// <summary>
//    /// Percemtage reductions in emissions resulting from the use of the mitigation method
//    /// </summary>
//    public MitigationFactor MitigationFactors { get; set; } = new MitigationFactor(false);
//    /// <summary>
//    /// COnstructor for Fertiliser Mitigation object
//    /// </summary>
//    /// <param name="iD">Unique identifier</param>
//    /// <param name="method">Method identifier</param>
//    /// <param name="methodname">Method name</param>
//    /// <param name="enumComponent">Component enumerator</param>
//    /// <param name="enumSource">Enum for fertiliser type source (as int)</param>
//    /// <param name="methane">Methane reduction (%)</param>
//    /// <param name="ammonia">Ammonia reduction (%)</param>
//    /// <param name="nitrousOxide">Nitrous Oxide reduction (%)</param>
//    /// <param name="mineralisation">Mineralisation reduction (%)</param>
//    /// <param name="leaching">Leaching reduction (%)</param>
//    public FertiliserMitigation(int iD, int method, string methodname, int enumComponent, int enumSource,
//        double methane, double ammonia, double nitrousOxide, double mineralisation, double leaching)
//    {
//        UID = iD;
//        methodID = method;
//        name = methodname;
//        component = (ComponentType)enumComponent;
//        fertiliser = (FertiliserType)enumSource;
//        MitigationFactors.factorMethane = methane;
//        MitigationFactors.factorAmmonia = ammonia;
//        MitigationFactors.factorNitrousOxide = nitrousOxide;
//        MitigationFactors.factorMineralisation = mineralisation;
//        MitigationFactors.factorLeaching = leaching;
//    }
//}
///// <summary>
///// Residue mitigation method class - stores reductions for residue mitigation methods
///// </summary>
//public class ResidueMitigation
//{
//    /// <summary>
//    /// The name of the mitigation method
//    /// </summary>
//    private string name;
//    /// <summary>
//    /// Constructor for residue mitigation method
//    /// </summary>
//    /// <param name="uID">Unique identifier</param>
//    /// <param name="methodID">Method identifier</param>
//    /// <param name="name">Method name</param>
//    /// <param name="enumComponent">Component enumerator</param>
//    /// <param name="methane">Methane reduction (%)</param>
//    /// <param name="ammonia">Ammonia reduction (%)</param>
//    /// <param name="nitrousoxide">Nitrous oxide reduction (%)</param>
//    /// <param name="mineralisation">Mineralisation reduction (%)</param>
//    /// <param name="leaching">Leaching reduction (%)</param>
//    public ResidueMitigation(int uID, int methodID, string name, int enumComponent, double methane, double ammonia, double nitrousoxide, double mineralisation, double leaching)
//    {
//        UID = uID;
//        this.methodID = methodID;
//        this.name = name;
//        component = (ComponentType)enumComponent;
//        MitigationFactors.factorMethane = methane;
//        MitigationFactors.factorAmmonia = ammonia;
//        MitigationFactors.factorNitrousOxide = nitrousoxide;
//        MitigationFactors.factorMineralisation = mineralisation;
//        MitigationFactors.factorLeaching = leaching;
//    }

//    /// <summary>
//    /// The unique identifieer for the method, component, source, etc.
//    /// </summary>
//    public int UID { get; set; }
//    /// <summary>
//    /// The (non-unique) ID of the method
//    /// </summary>
//    public int methodID { get; set; }
//    /// <summary>
//    /// The component type for the the mitigaiton method.
//    /// </summary>
//    public ComponentType component { get; set; }
//    /// <summary>
//    /// Percemtage reductions in emissions resulting from the use of the mitigation method
//    /// </summary>
//    public MitigationFactor MitigationFactors { get; set; } = new MitigationFactor(false);
//}
///// <summary>
///// Yard mitigation method class - stores reductions for yard mitigation methods
///// </summary>
//public class YardMitigation
//{
//    /// <summary>
//    /// The name of the mitigation method
//    /// </summary>
//    private string name;

//    /// <summary>
//    /// Constructor for residue mitigation method
//    /// </summary>
//    /// <param name="uID">Unique identifier</param>
//    /// <param name="methodID">Method identifier</param>
//    /// <param name="name">Method name</param>
//    /// <param name="enumComponent">Component enumerator</param>
//    /// <param name="methane">Methane reduction (%)</param>
//    /// <param name="ammonia">Ammonia reduction (%)</param>
//    /// <param name="nitrousoxide">Nitrous oxide reduction (%)</param>
//    /// <param name="mineralisation">Mineralisation reduction (%)</param>
//    /// <param name="leaching">Leaching reduction (%)</param>
//    public YardMitigation(int uID, int methodID, string name, int enumComponent, double methane, double ammonia, double nitrousoxide, double mineralisation, double leaching)
//    {
//        UID = uID;
//        this.methodID = methodID;
//        this.name = name;
//        component = (ComponentType)enumComponent;
//        MitigationFactors.factorMethane = methane;
//        MitigationFactors.factorAmmonia = ammonia;
//        MitigationFactors.factorNitrousOxide = nitrousoxide;
//        MitigationFactors.factorMineralisation = mineralisation;
//        MitigationFactors.factorLeaching = leaching;
//    }

//    /// <summary>
//    /// The unique identifieer for the method, component, source, etc.
//    /// </summary>
//    public int UID { get; set; }
//    /// <summary>
//    /// The (non-unique) ID of the method
//    /// </summary>
//    public int methodID { get; set; }
//    /// <summary>
//    /// The component type for the the mitigaiton method.
//    /// </summary>
//    public ComponentType component { get; set; }
//    /// <summary>
//    /// Percemtage reductions in emissions resulting from the use of the mitigation method
//    /// </summary>
//    public MitigationFactor MitigationFactors { get; set; } = new MitigationFactor(false);
//}
///// <summary>
///// Housing mitigation method class - stores reductions for housing mitigation methods
///// </summary>
//public class HousingMitigation
//{
//    /// <summary>
//    /// The method name
//    /// </summary>
//    public string name;
//    /// <summary>
//    /// The unique identifieer for the method, component, source, etc.
//    /// </summary>
//    public int UID { get; set; }
//    /// <summary>
//    /// The (non-unique) ID of the method
//    /// </summary>
//    public int methodID { get; set; }
//    /// <summary>
//    /// The component type for the the mitigaiton method.
//    /// </summary>
//    public ComponentType component { get; set; }
//    /// <summary>
//    /// The source animal from whih the manures derive
//    /// </summary>
//    public OrganicMatterSourceType animalSource { get; set; }
//    /// <summary>
//    /// Percemtage reductions in emissions resulting from the use of the mitigation method
//    /// </summary>
//    public MitigationFactor MitigationFactors { get; set; } = new MitigationFactor(false);

//    /// <summary>
//    /// COnstructor for Fertiliser Mitigation object
//    /// </summary>
//    /// <param name="iD">Unique identifier</param>
//    /// <param name="method">Method identifier</param>
//    /// <param name="methodname">Method name</param>
//    /// <param name="enumComponent">Component enumerator</param>
//    /// <param name="enumSource">Enum for fertiliser type source (as int)</param>
//    /// <param name="methane">Methane reduction (%)</param>
//    /// <param name="ammonia">Ammonia reduction (%)</param>
//    /// <param name="nitrousOxide">Nitrous Oxide reduction (%)</param>
//    /// <param name="mineralisation">Mineralisation reduction (%)</param>
//    /// <param name="leaching">Leaching reduction (%)</param>
//    public HousingMitigation(int iD, int method, string methodname, int enumComponent, int enumSource,
//        double methane, double ammonia, double nitrousOxide, double mineralisation, double leaching)
//    {
//        UID = iD;
//        methodID = method;
//        name = methodname;
//        component = (ComponentType)enumComponent;
//        animalSource = (OrganicMatterSourceType)enumSource;
//        MitigationFactors.factorMethane = methane;
//        MitigationFactors.factorAmmonia = ammonia;
//        MitigationFactors.factorNitrousOxide = nitrousOxide;
//        MitigationFactors.factorMineralisation = mineralisation;
//        MitigationFactors.factorLeaching = leaching;
//    }

//}
///// <summary>
///// Storage mitigation method class - stores reductions for storage mitigation methods
///// </summary>
//public class StorageMitigation
//{
//    /// <summary>
//    /// Method name
//    /// </summary>
//    public string name;
//    /// <summary>
//    /// The unique identifieer for the method, component, source, etc.
//    /// </summary>
//    public int UID { get; set; }
//    /// <summary>
//    /// The (non-unique) ID of the method
//    /// </summary>
//    public int methodID { get; set; }
//    /// <summary>
//    /// The component type for the the mitigaiton method.
//    /// </summary>
//    public ComponentType component { get; set; }
//    /// <summary>
//    /// The organic matter state (liquid or solid)
//    /// </summary>
//    public OrganicMatterState organicMatterState { get; set; }
//    /// <summary>
//    /// Percemtage reductions in emissions resulting from the use of the mitigation method
//    /// </summary>
//    public MitigationFactor MitigationFactors { get; set; } = new MitigationFactor(false);

//    /// <summary>
//    /// COnstructor for Fertiliser Mitigation object
//    /// </summary>
//    /// <param name="iD">Unique identifier</param>
//    /// <param name="method">Method identifier</param>
//    /// <param name="methodname">Method name</param>
//    /// <param name="enumComponent">Component ewnumerator</param>
//    /// <param name="enumState">Enum for organic matter state(as int)</param>
//    /// <param name="methane">Methane reduction (%)</param>
//    /// <param name="ammonia">Ammonia reduction (%)</param>
//    /// <param name="nitrousOxide">Nitrous Oxide reduction (%)</param>
//    /// <param name="mineralisation">Mineralisation reduction (%)</param>
//    /// <param name="leaching">Leaching reduction (%)</param>
//    public StorageMitigation(int iD, int method, string methodname, int enumComponent, int enumState,
//        double methane, double ammonia, double nitrousOxide, double mineralisation, double leaching)
//    {
//        UID = iD;
//        methodID = method;
//        name = methodname;
//        component = (ComponentType)enumComponent;
//        organicMatterState = (OrganicMatterState)enumState;
//        MitigationFactors.factorMethane = methane;
//        MitigationFactors.factorAmmonia = ammonia;
//        MitigationFactors.factorNitrousOxide = nitrousOxide;
//        MitigationFactors.factorMineralisation = mineralisation;
//        MitigationFactors.factorLeaching = leaching;
//    }

//}
///// <summary>
///// Mitigation method class - stores method and reductoin factors
///// </summary>
//public class SpreadingMitigation
//{
//    /// <summary>
//    /// The unique identifieer for the method, component, source, etc.
//    /// </summary>
//    public int UID { get; set; }
//    /// <summary>
//    /// The (non-unique) ID of the method
//    /// </summary>
//    public int methodID { get; set; }
//    /// <summary>
//    /// Sector name
//    /// </summary>
//    public string sector { get; set; }
//    /// <summary>
//    /// The component type for the the mitigaiton method.
//    /// </summary>
//    public ComponentType Component {  get; set; }
//    /// <summary>
//    /// The organic matter type for which the method applies
//    /// </summary>
//    public OrganicMatterType Type{ get; set; }
//    /// <summary>
//    /// The origin of the origanic matter (livestock type or non-livestock)
//    /// </summary>
//    public OrganicMatterSourceType Source { get; set; }
//    /// <summary>
//    /// The state of the organic matter to which the method applies (solid or liquid)
//    /// </summary>
//    public OrganicMatterState State { get; set; }
//    /// <summary>
//    /// Percemtage reductions in emissions resulting from the use of the mitigation method
//    /// </summary>
//    public MitigationFactor MitigationFactors { get; set; } = new MitigationFactor(false);
//    /// <summary>
//    /// Method name
//    /// </summary>
//    public string name;
//    /// <summary>
//    /// Constructor for spreading mitigation
//    /// </summary>
//    /// <param name="iD">unique identifieer</param>
//    /// <param name="method">method identifier</param>
//    /// <param name="methodname">method name</param>
//    /// <param name="mysector">sector name</param>
//    /// <param name="enumComponent">component enumerator</param>
//    /// <param name="enumSource">animal source enumerator</param>
//    /// <param name="enumState">organic matter state enumerator</param>
//    /// <param name="enumType">organic matter type enumerator</param>
//    /// <param name="methane">methane reduction (%)</param>
//    /// <param name="ammonia">ammonia reduction (%)</param>
//    /// <param name="nitrousOxide">nitrous oxide reduction (%)</param>
//    /// <param name="mineralisation">mineralisation redution (%)</param>
//    /// <param name="leaching">leaching reduction (%)</param>
//    public SpreadingMitigation(int iD, int method, string methodname, string mysector, int enumComponent, int enumSource,
//        int enumState, int enumType, double methane, double ammonia, double nitrousOxide, double mineralisation,
//        double leaching)
//    {
//        UID = iD;
//        methodID = method;
//        name = methodname;
//        sector = mysector;
//        Component = (ComponentType)enumComponent;
//        Source =(OrganicMatterSourceType)enumSource;
//        State =(OrganicMatterState)enumState;
//        Type= (OrganicMatterType)enumType;
//        MitigationFactors.factorMethane = methane;
//        MitigationFactors.factorAmmonia = ammonia;
//        MitigationFactors.factorNitrousOxide = nitrousOxide;
//        MitigationFactors.factorMineralisation = mineralisation;
//        MitigationFactors.factorLeaching = leaching;
//    }
//}
/// <summary>
/// Class storing the percentage reductions in emissions
/// </summary>
public class MitigationFactor
{
    /// <summary>
    /// Percemtage reduction in methane emissions resulting from the use of the mitigation method
    /// </summary>
    public double FactorMethane { get; set; }
    /// <summary>
    /// Percentage factor in ammonia emissions resulting from the use of the mitigation method
    /// </summary>
    public double FactorAmmonia { get; set; }
    /// <summary>
    /// Percentage factor in nitrous oxide emissions from the use of the mitigation method
    /// </summary>
    public double FactorNitrousOxide { get; set; }
    /// <summary>
    /// Percentage factor in mineralisatoin from the use of the mitigation method
    /// </summary>
    public double FactorMineralisation { get; set; }
    /// <summary>
    /// Percentage factor in leaching from the use of the mitigation method
    /// </summary>
    public double FactorLeaching { get; set; }
    /// <summary>
    /// Consructor to initialise as multiplier or factors
    /// </summary>
    /// <param name="multiplier">boolean indicating if multiplier (true) or factors (false)</param>
    public MitigationFactor(bool multiplier)
    {
        if(multiplier)
        {
            FactorAmmonia = 1.0;
            FactorNitrousOxide = 1.0;
            FactorMineralisation = 1.0;
            FactorLeaching = 1.0;
            FactorMethane = 1.0;
        }
        else
        {
            FactorAmmonia = 0.0;
            FactorNitrousOxide = 0.0;
            FactorMineralisation = 0.0;
            FactorLeaching = 0.0;
            FactorMethane = 0.0;
        }
    }
    /// <summary>
    /// Calculate emission multiplier resulting from mitigation implementation
    /// </summary>
    /// <param name="Methodfactors">Mitigation multiplier object</param>
    public void CalculateMultiplier(MitigationFactor Methodfactors)
    {
        FactorMethane *= (100.0 - Methodfactors.FactorMethane) * HelperFunctions.Percent_to_Proportion;
        FactorNitrousOxide *= (100.0 - Methodfactors.FactorNitrousOxide) * HelperFunctions.Percent_to_Proportion;
        FactorAmmonia *= (100.0 - Methodfactors.FactorAmmonia) * HelperFunctions.Percent_to_Proportion;
        FactorMineralisation *= (100.0 - Methodfactors.FactorMineralisation) * HelperFunctions.Percent_to_Proportion;
        FactorLeaching *= (100.0 - Methodfactors.FactorLeaching) * HelperFunctions.Percent_to_Proportion;
    }
    /// <summary>
    /// Method to round emissions to a fixed number of decimal places
    /// </summary>
    /// <param name="decimalPlaces">The number of decimal places to round to</param>
    public void RoundFactors(int decimalPlaces)
    {

        this.FactorAmmonia = Math.Round(this.FactorAmmonia, decimalPlaces);
        this.FactorNitrousOxide = Math.Round(this.FactorNitrousOxide, decimalPlaces);
        this.FactorMineralisation = Math.Round(this.FactorMineralisation, decimalPlaces);
        this.FactorLeaching = Math.Round(this.FactorLeaching, decimalPlaces);
        this.FactorMethane = Math.Round(this.FactorMethane, decimalPlaces);
    }

}
/// <summary>
/// Mitigation Method class
/// </summary>
public class MitigationMethod
{
    /// <summary>
    /// Unique ID for combination of method, component, source, state and organic matter type
    /// </summary>
    public int UID { get; set; }
    /// <summary>
    /// The unqiue method ID
    /// </summary>
    public int MethodID { get; set; }
    /// <summary>
    /// Method name - text description
    /// </summary>
    public string Methodname { get; set; }
    /// <summary>
    /// Component enumerator
    /// </summary>
    public int Component { get; set; }
    /// <summary>
    /// Source enumerator (type is depednent on component)
    /// </summary>
    public int Sourcetype { get; set; }
    /// <summary>
    /// source state (for organic matter only) - zero if not organic matter
    /// </summary>
    public int Sourcestate { get; set; }
    /// <summary>
    /// organic matter type - zero if not organic matter
    /// </summary>
    public int Organicmattertype { get; set; }

    /// <summary>
    /// Mitigation method reduction factors for emissions
    /// </summary>
    public MitigationFactor ReductionFactors { get; set; } = new(false);

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="iD">unique ID for combination of method, component, source, state and organic metter type</param>
    /// <param name="method">method ID</param>
    /// <param name="namemethod">Mehtod name</param>
    /// <param name="enumComponent">Component enumerator</param>
    /// <param name="enumsource">Source enumerator</param>
    /// <param name="enumstate">State enumerator</param>
    /// <param name="enumorganicmattertype">Organic matter type enumerator</param>
    /// <param name="methane">methane reduction factor (%)</param>
    /// <param name="ammonia">ammonia reduction factor (%)</param>
    /// <param name="nitrousOxide">nitrous oxide reduction factor (%)</param>
    /// <param name="mineralisation">mineralisation reduction factor (%)</param>
    /// <param name="leaching">leaching reduction factor (%)</param>
    public MitigationMethod(int iD, int method, string namemethod, int enumComponent, int enumsource, int enumstate, int enumorganicmattertype,
        double methane, double ammonia, double nitrousOxide, double mineralisation, double leaching)
    {
        UID = iD;
        MethodID = method;
        Methodname = namemethod;
        Component = enumComponent;
        Sourcetype = enumsource;
        Sourcestate = enumstate;
        Organicmattertype = enumorganicmattertype;
        ReductionFactors.FactorMethane = methane;
        ReductionFactors.FactorAmmonia = ammonia;
        ReductionFactors.FactorNitrousOxide = nitrousOxide;
        ReductionFactors.FactorMineralisation = mineralisation;
        ReductionFactors.FactorLeaching = leaching;
    }
}
