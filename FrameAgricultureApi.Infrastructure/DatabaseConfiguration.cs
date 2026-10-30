namespace FrameAgricultureApi.Infrastructure;

public class DatabaseConfiguration
{
    public required string ConnectionString { get; init; }

    public required string DatabaseName { get; init; }
}