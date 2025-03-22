using BlijvenLeren.Contracts;
using System.Text.Json.Serialization;

namespace BlijvenLerenApi.Json;

[JsonSerializable(typeof(LearningRequest))]
public partial class LearningRequestContext : JsonSerializerContext
{
}
