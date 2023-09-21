using Interfaces;
using Microsoft.Extensions.Options;
using Models;
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


        //----------------------------------------------------------------
        // Sensor CRUD



    }
}