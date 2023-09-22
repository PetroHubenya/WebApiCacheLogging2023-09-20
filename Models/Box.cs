using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.Text.Json.Serialization;

namespace Models
{
    public class Box
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }

        public string Name { get; set; } = null!;

        [BsonElement("items")]
        [JsonPropertyName("items")]
        public List<Sensor> Sensors { get; set; } = null!;
    }
}