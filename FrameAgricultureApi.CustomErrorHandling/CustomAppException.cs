using System.Globalization;

namespace FrameAgricultureApi.CustomErrorHandling;

/// <summary>
/// Custom App Exception class
/// </summary>
public class CustomAppException : Exception
{
    /// <summary>
    /// Constructor
    /// </summary>
    public CustomAppException() : base() { }

    /// <summary>
    /// Overloaded Constructor
    /// </summary>
    /// <param name="message">Error message</param>
    public CustomAppException(string message) : base(message) { }

    /// <summary>
    /// Overlodaed Constructor
    /// </summary>
    /// <param name="message">error message</param>
    /// <param name="args">arguments list</param>
    public CustomAppException(string message, params object[] args)
        : base(string.Format(CultureInfo.CurrentCulture, message, args))
    {
    }
    /// <summary>
    /// Overloaded Constructor
    /// </summary>
    /// <param name="message">error message</param>
    /// <param name="key">key indicating source of error</param>
    /// <param name="args">arguments list</param>
    public CustomAppException(string message, string key, params object[] args)
        : base(string.Format(CultureInfo.CurrentCulture, message, key, args))
    {
    }
}
