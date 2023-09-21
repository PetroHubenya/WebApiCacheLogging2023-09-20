using Interfaces;
using Microsoft.Extensions.Options;
using Models;
using MongoDB.Bson;
using MongoDB.Driver;

namespace DataAccessLayer
{
    public class MongoDBService : IDataService
    {
        private readonly IMongoCollection<Box> _boxCollection;

        private readonly IMongoCollection<Sensor> _sensorCollection;

        public MongoDBService(IOptions<MongoDBSettings> mongoDBSettings)
        {
            MongoClient client = new MongoClient(mongoDBSettings.Value.ConnectionURL);
            IMongoDatabase database = client.GetDatabase(mongoDBSettings.Value.DatabaseName);
            _boxCollection = database.GetCollection<Box>(mongoDBSettings.Value.CollectionNameBoxes);
            _sensorCollection = database.GetCollection<Sensor>(mongoDBSettings.Value.CollectionNameSensors);
        }

        //----------------------------------------------------------------
        // Box CRUD

        // Create Box
        public async Task CreateBoxAsync(Box box)
        {
            await _boxCollection.InsertOneAsync(box);
        }

        // Get Box
        public async Task<List<Box>> GetAllBoxesAsync()
        {
            return await _boxCollection.Find(new BsonDocument()).ToListAsync();
        }




        //----------------------------------------------------------------
        // Sensor CRUD



    }
}