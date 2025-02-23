using MongoDB.Bson.Serialization.Serializers;
using MongoDB.Bson.Serialization;
using MongoDB.Bson;

namespace Api.Extensions
{
    public static class MongoDbExtension
    {
        public static IServiceCollection AddMongoService(this IServiceCollection services)
        {
            // Register GUID serializer
            BsonSerializer.RegisterSerializer(new GuidSerializer(BsonType.String));

            // Return the modified service collection
            return services;
        }
    }
}
