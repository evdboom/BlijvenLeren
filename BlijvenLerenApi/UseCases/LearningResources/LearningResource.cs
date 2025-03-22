using Azure;
using Azure.Data.Tables;
using BlijvenLerenApi.Contracts;

namespace BlijvenLerenApi.UseCases.LearningResources;

public class LearningResource : ITableEntity
{
    public string PartitionKey { get; set; } = default!;
    public string RowKey { get; set; } = default!;
    public DateTimeOffset? Timestamp { get; set; }
    public ETag ETag { get; set; }

    public string Title { get; set; } = default!;
    public string Description { get; set; } = default!;
    public ResourceType Type { get; set; }
    public string Url { get; set; } = default!;
    public Guid UserId { get; set; }
}
