using BlijvenLerenApi.UseCases.Shared;

namespace BlijvenLerenApi.UseCases.LearningResources
{
    public class GetLearningResourcesQueryEndpoint(GetLearningResourcesQueryHandler handler) : IEndpoint
    {
        public string GroupName => "LearningResources";
        public Action<RouteGroupBuilder> RegisterEndpoint =>
            (builder) => builder.MapGet("/", async () => await handler.Handle(new GetLearningResourcesQuery(), CancellationToken.None));
    }
}
