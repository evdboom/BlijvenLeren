using Azure.Data.Tables;
using BlijvenLeren.Contracts;
using BlijvenLerenApi.UseCases.Shared;


namespace BlijvenLerenApi.UseCases.LearningResources
{
    public class GetLearningResourcesQueryHandler(TableServiceClient serviceClient, IHttpContextAccessor context) : IRequestHandler<GetLearningResourcesQuery, IEnumerable<LearningResponse>>
    {
        private const string TableName = "LearningResources";
        private readonly TableServiceClient _serviceClient = serviceClient;
        private readonly IHttpContextAccessor _context = context;

        public async Task<IEnumerable<LearningResponse>> Handle(GetLearningResourcesQuery request, CancellationToken cancellationToken)
        {
            if (_context.HttpContext?.User?.Identity?.IsAuthenticated != true)
            {
                throw new UnauthorizedAccessException();
            }

            var table = _serviceClient.GetTableClient(TableName);

            var users = new Dictionary<Guid, string>();

            var results = new List<LearningResponse>();
            await foreach(var resource in table.QueryAsync<LearningResource>(x => x.PartitionKey == "LearningResource", cancellationToken: cancellationToken))
            {
                if (!users.TryGetValue(resource.UserId, out string? resourceUser))
                {
                    var userTable = _serviceClient.GetTableClient("Users");
                    var user = await userTable.GetEntityAsync<TableUser>(resource.UserId.ToString(), "User", cancellationToken: cancellationToken);
                    resourceUser = user.Value.UserName;
                    users.Add(resource.UserId, resourceUser);
                }

                var response = new LearningResponse
                {
                    Id = resource.RowKey,
                    Title = resource.Title,
                    Description = resource.Description,
                    Type = resource.Type,
                    Url = resource.Url,
                    UserId = resource.UserId,
                    Timestamp = resource.Timestamp,
                    UserName = resourceUser,
                };


                await foreach (var addition in table.QueryAsync<LearningAddition>(x => x.PartitionKey == resource.RowKey && x.RowKey == "Addition", cancellationToken: cancellationToken))
                {
                    if (!users.TryGetValue(addition.UserId, out string? additionUser))
                    {
                        var userTable = _serviceClient.GetTableClient("Users");
                        var user = await userTable.GetEntityAsync<TableUser>(addition.UserId.ToString(), "User", cancellationToken: cancellationToken);
                        additionUser = user.Value.UserName;
                        users.Add(addition.UserId, additionUser);
                    }

                    response.Additions.Add(new AdditionResponse
                    {
                        Id = addition.RowKey,
                        Title = addition.Title,
                        Description = addition.Description,
                        UserId = addition.UserId,
                        UserName = additionUser,
                        Timestamp = addition.Timestamp,
                    });
                }
                results.Add(response);
            }

            return results;
        }
    }
}
