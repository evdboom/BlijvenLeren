using System;

namespace BlijvenLeren.Contracts
{
    public class AdditionResponse
    {
        public string Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public Guid UserId { get; set; }
        public string UserName { get; set; }
        public DateTimeOffset? Timestamp { get; set; }
    }
}
