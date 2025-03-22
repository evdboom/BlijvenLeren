using BlijvenLeren.Contracts;
using BlijvenLerenApi.UseCases.Shared;

namespace BlijvenLerenApi.UseCases.LearningResources
{
    public class AddLearningResourcesCommandEndpoint(AddLearningResourcesCommandHandler handler) : IEndpoint
    {
        public string GroupName => "LearningResources";
        public Action<RouteGroupBuilder> RegisterEndpoint =>
            (builder) => builder.MapPost("/", async (LearningRequest request) => await handler.Handle(new AddLearningResourceCommand(request), CancellationToken.None));
    }
}
