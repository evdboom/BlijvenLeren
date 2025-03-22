using BlijvenLeren.Contracts;
using BlijvenLerenApi.Contracts;
using BlijvenLerenApi.UseCases.Shared;

namespace BlijvenLerenApi.UseCases.LearningResources;

public class AddLearningResourceCommand(LearningRequest request) : IRequest<LearningResponse>
{
    public string Title { get; set; } = request.Title;
    public string Description { get; set; } = request.Description;
    public ResourceType Type { get; set; } = request.Type;
    public string Url { get; set; } = request.Url;
}
