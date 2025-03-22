using Azure;
using Azure.Data.Tables;

namespace BlijvenLerenApi.UseCases.Shared
{
    public class TableUser : ITableEntity
    {
        public string PartitionKey { get; set; } = default!;
        public string RowKey { get; set; } = default!;
        public DateTimeOffset? Timestamp { get; set; }
        public ETag ETag { get; set; }
        public string UserName { get; set; } = default!;
        public string Email { get; set; } = default!;
    }
}
