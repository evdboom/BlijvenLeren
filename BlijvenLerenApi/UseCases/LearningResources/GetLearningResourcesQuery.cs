using BlijvenLeren.Contracts;
using BlijvenLerenApi.UseCases.Shared;

namespace BlijvenLerenApi.UseCases.LearningResources;

public class GetLearningResourcesQuery : IRequest<IEnumerable<LearningResponse>>
{
}
