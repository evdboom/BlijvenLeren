using BlijvenLerenApi.Contracts;
using System;

namespace BlijvenLeren.Contracts
{    
    public class LearningRequest
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public ResourceType Type { get; set; }
        public string Url { get; set; }
    }
}
