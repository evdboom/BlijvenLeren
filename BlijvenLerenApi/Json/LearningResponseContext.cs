using BlijvenLeren.Contracts;
using System.Text.Json.Serialization;

namespace BlijvenLerenApi.Json;

[JsonSerializable(typeof(LearningResponse))]
[JsonSerializable(typeof(IEnumerable<LearningResponse>))]
public partial class LearningResponseContext : JsonSerializerContext
{
}
