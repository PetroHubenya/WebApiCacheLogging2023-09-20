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

        // Box CRUD.----------------------------------------------------------------

        // Create box.
        public async Task CreateBoxAsync(Box box)
        {
            await _boxCollection.InsertOneAsync(box);
        }

        // Get all boxes.
        public async Task<List<Box>> GetAllBoxesAsync()
        {
            return await _boxCollection.Find(new BsonDocument()).ToListAsync();
        }

        // Get box by id.
        public async Task<Box> GetBoxAsync(string id)
        {
            var filter = Builders<Box>.Filter.Eq(b => b.Id, id);

            return await _boxCollection.Find(filter).FirstOrDefaultAsync();
        }

        // Update box.
        public async Task<UpdateResult> UpdateBoxAsync(string id, Box newBox)
        {
            var filter = Builders<Box>.Filter.Eq(b => b.Id, id);

            var update = Builders<Box>.Update.Set(b => b.Name, newBox.Name);

            return await _boxCollection.UpdateOneAsync(filter, update);
        }

        // Delete box.
        public async Task<DeleteResult> DeleteBoxAsync(string id)
        {
            var filter = Builders<Box>.Filter.Eq(b => b.Id, id);

            return await _boxCollection.DeleteOneAsync(filter);
        }

        // Add sensor to the box.
        public async Task<ReplaceOneResult> AddSensorToBoxAsync(string boxId, string sensorId)
        {
            var boxFilter = Builders<Box>.Filter.Eq(b => b.Id, boxId);

            Box box = await _boxCollection.Find(boxFilter).FirstOrDefaultAsync();

            var sensorFilter = Builders<Sensor>.Filter.Eq(s => s.Id, sensorId);

            Sensor sensor = await _sensorCollection.Find(sensorFilter).FirstOrDefaultAsync();

            box.Sensors.Add(sensor);

            return await _boxCollection.ReplaceOneAsync(boxFilter, box);
        }

        // Sensor CRUD.----------------------------------------------------------------

        // Create sensor.
        public async Task CreateSensorAsync(Sensor sensor)
        {
            await _sensorCollection.InsertOneAsync(sensor);
        }

        // Get all sensors.
        public async Task<List<Sensor>> GetAllSensorsAsync()
        {
            return await _sensorCollection.Find(new BsonDocument()).ToListAsync();
        }

        // Get sensor by id.
        public async Task<Sensor> GetSensorAsync(string id)
        {
            var filter = Builders<Sensor>.Filter.Eq(s => s.Id, id);

            return await _sensorCollection.Find(filter).FirstOrDefaultAsync();
        }

        // Update sensor.
        public async Task<ReplaceOneResult> UpdateSensorAsync(string id, Sensor newSensor)
        {
            var filter = Builders<Sensor>.Filter.Eq(b => b.Id, id);

            return await _sensorCollection.ReplaceOneAsync(filter, newSensor);
        }

        // Delete sensor.
        public async Task<DeleteResult> DeleteSensorAsync(string id)
        {
            var filter = Builders<Sensor>.Filter.Eq(s => s.Id, id);

            return await _sensorCollection.DeleteOneAsync(filter);
        }
    }
}