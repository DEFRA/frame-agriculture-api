namespace FrameAgricultureApi.Enumerators;

/// <summary>
/// Enumerator LookUp class
/// </summary>
public static class EnumeratorLookup
{
    /// <summary>
    /// Method to create list of all enumerators used by FLEA
    /// </summary>
    /// <returns>List of enumerator data transfer objects</returns>
    public static List<EnumeratorsDto> GetEnumerators()
    {
        var result = new List<EnumeratorsDto>();
        var nested = typeof(Enumerators).GetNestedTypes();

        int i = 0;
        foreach(var memberEnum in nested)
        {
            result.Add(new EnumeratorsDto() { EnumeratorName = memberEnum.Name, EnumeratorItems = [] });
            var values = Enum.GetValues(memberEnum);
            foreach(var value in values)
            {
                result[i].EnumeratorItems.Add(key: value.ToString() ?? "Enumerator parsing error", value: (int)value);
            }
            i++;
        }
        return result;
    }
}
/// <summary>
/// Enumerator data transfer object class
/// </summary>
public class EnumeratorsDto
{
    /// <summary>
    /// Name of enumerator type
    /// </summary>
    public string EnumeratorName { get; set; } = "";
    /// <summary>
    /// Dictionary of all items (name and id) in enumerastor type
    /// </summary>
    public Dictionary<string, int> EnumeratorItems { get; set; } = [];
}
