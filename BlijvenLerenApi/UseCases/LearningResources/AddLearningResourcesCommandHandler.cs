using Azure;
using Azure.Data.Tables;
using BlijvenLeren.Contracts;
using BlijvenLerenApi.UseCases.Shared;
using System.Security.Claims;


namespace BlijvenLerenApi.UseCases.LearningResources
{
    public class AddLearningResourcesCommandHandler(TableServiceClient serviceClient, IHttpContextAccessor context, ITimeProvider timeProvider) : IRequestHandler<AddLearningResourceCommand, LearningResponse>
    {
        private const string TableName = "LearningResources";
        private readonly TableServiceClient _serviceClient = serviceClient;
        private readonly IHttpContextAccessor _context = context;
        private readonly ITimeProvider _timeProvider = timeProvider;

        public async Task<LearningResponse> Handle(AddLearningResourceCommand request, CancellationToken cancellationToken)
        {
            if (_context.HttpContext?.User?.Identity?.IsAuthenticated != true)
            {
                throw new UnauthorizedAccessException();
            }
            if (!_context.HttpContext.User.HasClaim("UserType", "Full"))
            {
                throw new UnauthorizedAccessException();
            }

            var table = _serviceClient.GetTableClient(TableName);

            var resource = new LearningResource
            {
                PartitionKey = "LearningResource",
                RowKey = Guid.NewGuid().ToString(),
                Title = request.Title,
                Description = request.Description,
                Type = request.Type,
                Url = request.Url,
                UserId = Guid.Parse(_context.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier)!),
                ETag = ETag.All,
                Timestamp = _timeProvider.UtcNowOffset 
            };

            var response = await table.UpsertEntityAsync(resource, cancellationToken: cancellationToken);
            if (response.IsError)
            {
                throw new InvalidOperationException("Failed to add learning resource");
            }

            return new LearningResponse
            {
                Id = resource.RowKey,
                Title = resource.Title,
                Description = resource.Description,
                Type = resource.Type,
                Url = resource.Url,
                UserId = resource.UserId,
                Timestamp = resource.Timestamp,
                UserName = _context.HttpContext.User.FindFirstValue(ClaimTypes.Name)!,
            };
        }
    }
}
