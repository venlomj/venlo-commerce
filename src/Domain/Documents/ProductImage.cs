using Domain.Attributes;
using Domain.Settings;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace Domain.Documents
{
    [BsonCollection("pictures")]
    public class ProductImage : Document
    {
        [BsonElement("name")]
        public string Name { get; set; }

        [BsonElement("image_data")]
        public required byte[] ImageData { get; set; }

        [BsonElement("image_identifier")]
        public Guid ImageIdentifier { get; set; } = Guid.NewGuid();

        [BsonElement("product_id")]
        public Guid ProductId { get; set; }  // Reference to ProductId in SQL

        [BsonElement("created_by")]
        internal string CreatedBy { get; set; } = "VenloCommerce";

        [BsonElement("created_at")]
        internal DateTime CreatedAtUtc { get; set; } = DateTime.UtcNow;

        [BsonElement("modified_by")]
        internal string ModifiedBy { get; set; } = string.Empty;

        [BsonElement("modified_at")]
        internal DateTime ModifiedAtUtc { get; set; } = DateTime.UtcNow;
    }
}