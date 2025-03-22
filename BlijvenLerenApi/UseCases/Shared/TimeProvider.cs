
namespace BlijvenLerenApi.UseCases.Shared;

public class TimeProvider : ITimeProvider
{
    public DateTime Now => DateTime.Now;

    public DateTime UtcNow => DateTime.UtcNow;

    public DateOnly Today => DateOnly.FromDateTime(DateTime.Today);

    public DateTimeOffset NowOffset => DateTimeOffset.Now;

    public DateTimeOffset UtcNowOffset => DateTimeOffset.UtcNow;
}
