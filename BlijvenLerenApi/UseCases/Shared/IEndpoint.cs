namespace BlijvenLerenApi.UseCases.Shared;

public interface IEndpoint
{
    public string GroupName { get; }
    public Action<RouteGroupBuilder> RegisterEndpoint { get; }
}
