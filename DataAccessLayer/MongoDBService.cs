using Interfaces;
using Microsoft.Extensions.Options;
using Models;
using MongoDB.Bson;
using MongoDB.Driver;
using System.Linq;

namespace DataAccessLayer
{
    public class MongoDBService : IDataService
    {
        private readonly IMongoCollection<Box> _boxCollection;

        private readonly IMongoCollection<Sensor> _sensorCollection;

        private readonly IMongoCollection<SensorData> _sensorDataCollection;

        public MongoDBService(IOptions<MongoDBSettings> mongoDBSettings)
        {
            MongoClient client = new MongoClient(mongoDBSettings.Value.ConnectionURL);
            IMongoDatabase database = client.GetDatabase(mongoDBSettings.Value.DatabaseName);
            _boxCollection = database.GetCollection<Box>(mongoDBSettings.Value.CollectionNameBoxes);
            _sensorCollection = database.GetCollection<Sensor>(mongoDBSettings.Value.CollectionNameSensors);
            _sensorDataCollection = database.GetCollection<SensorData>(mongoDBSettings.Value.CollectionNameSensorsData);
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

            box.SensorIds.Add(sensorId);

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

        // SensorData Crud. ----------------------------------------------------------------

        // Create SensorData for the specific sensor.
        public async Task CreateSensorDataAsync(SensorData sensorData)
        {
            await _sensorDataCollection.InsertOneAsync(sensorData);
        }

        // Get all SensorData.
        public async Task<List<SensorData>> GetAllSensorDataAsync()
        {
            return await _sensorDataCollection.Find(new BsonDocument()).ToListAsync();
        }

        // Get SensorData of the specific sensor.
        public async Task<List<SensorData>> GetSensorsDataBySensorIdAsync(string sensorId)
        {
            var filter = Builders<SensorData>.Filter.Eq(sd => sd.SensorId, sensorId);

            return await _sensorDataCollection.Find(filter).ToListAsync();
        }

        // Get SensorData of the specific sensor with pagination.
        public SensorDataPagination GetSensorsDataPaginationAsync(string sensorId, int rows, int page)
        {
            var queryableCollection = _sensorDataCollection.AsQueryable();

            List<SensorData> sensorDataList = queryableCollection.Where(sd => sd.SensorId == sensorId)
                                                                 .OrderByDescending(sd => sd.Timestamp)
                                                                 .Select(sd => new SensorData
                                                                 {
                                                                     Id = sd.Id,
                                                                     SensorId = sd.SensorId,
                                                                     Value = sd.Value,
                                                                     Timestamp = sd.Timestamp
                                                                 })
                                                                 .Skip((page - 1) * rows)
                                                                 .Take(rows)
                                                                 .ToList();

            int totalSensorDataCount = queryableCollection.Where(sd => sd.SensorId == sensorId)
                                                          .Count();

            var pages = (int)Math.Ceiling((double)totalSensorDataCount / rows);

            var result = new SensorDataPagination
            {
                SensorDataList = sensorDataList,
                NumberOfPages = pages
            };

            return result;
        }

        // Delete all SensorData of the specific sensor.
        public async Task<DeleteResult> DeleteAllSensorDataBySensorIdAsync(string sensorId)
        {
            var filter = Builders<SensorData>.Filter.Eq(sd => sd.SensorId, sensorId);

            return await _sensorDataCollection.DeleteManyAsync(filter);
        }
    }
}