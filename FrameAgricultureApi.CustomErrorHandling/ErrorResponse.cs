namespace FrameAgricultureApi.CustomErrorHandling;

/// <summary>
/// Error Response class
/// </summary>
public class ErrorResponse
{
    /// <summary>
    /// Success boolean
    /// </summary>
    public bool Success { get; set; } = false;
    /// <summary>
    /// Message for user about error response
    /// </summary>
    public string Message { get; set; } = "";
}
