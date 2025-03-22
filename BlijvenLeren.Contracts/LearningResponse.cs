using BlijvenLerenApi.Contracts;
using System;
using System.Collections.Generic;

namespace BlijvenLeren.Contracts
{
    public class LearningResponse
    {
        public LearningResponse() 
        {
            Additions = new List<AdditionResponse>();
        }

        public string Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public ResourceType Type { get; set; }
        public string Url { get; set; }
        public Guid UserId { get; set; }
        public string UserName { get; set; }
        public DateTimeOffset? Timestamp { get; set; }

        public ICollection<AdditionResponse> Additions { get; set; }
    }
}
