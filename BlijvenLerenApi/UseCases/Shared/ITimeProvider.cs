namespace BlijvenLerenApi.UseCases.Shared;

public interface ITimeProvider
{
    DateTime Now { get; }
    DateTime UtcNow { get; }
    DateOnly Today { get; }
    DateTimeOffset NowOffset { get; }
    DateTimeOffset UtcNowOffset { get; }
}
