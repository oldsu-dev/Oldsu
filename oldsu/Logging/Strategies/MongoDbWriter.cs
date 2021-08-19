using System;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Driver;

namespace Oldsu.Logging.Strategies
{
    public class MongoDbWriter : ILoggerWriterStrategy
    {
        private MongoClient _mongoClient;
        private IMongoCollection<BsonDocument> _mongoCollection;

        public MongoDbWriter(string connectionString)
        {
            // todo move this to a seperate object
            _mongoClient = new MongoClient(
                connectionString
            );
            
            var database = _mongoClient.GetDatabase("oldsu");
            
            _mongoCollection = database.GetCollection<BsonDocument>("logging");
        }

        private BsonDocument GetBsonDocument<T>(string urgency, string message, Exception? exception, object? dump, int lineNumber, string caller)
        {
            return new BsonDocument
            {
                { "date", DateTime.Now },
                { "urgency", urgency},
                { "message", message },
                { "exception", exception?.ToString() ?? (BsonValue)BsonNull.Value },
                { "type", typeof(T).ToString() },
                { "line_number", lineNumber },
                { "caller", caller },
                { "dump", dump?.ToBsonDocument() ?? (BsonValue)BsonNull.Value }
            };
        }

        public async Task LogInfo<T>(string message, Exception? exception, object? dump, int lineNumber, string caller)
        {
            var bsonDoc = GetBsonDocument<T>("info", message, exception, dump, lineNumber, caller);
            
            await _mongoCollection.InsertOneAsync(bsonDoc);
        }

        public async Task LogCritical<T>(string message, Exception? exception, object? dump, int lineNumber, string caller)
        {
            var bsonDoc = GetBsonDocument<T>("critical", message, exception, dump, lineNumber, caller);
            
            await _mongoCollection.InsertOneAsync(bsonDoc);
        }

        public async Task LogFatal<T>(string message, Exception? exception, object? dump, int lineNumber, string caller)
        {
            var bsonDoc = GetBsonDocument<T>("Fatal", message, exception, dump, lineNumber, caller);
            
            await _mongoCollection.InsertOneAsync(bsonDoc);
        }
    }
}