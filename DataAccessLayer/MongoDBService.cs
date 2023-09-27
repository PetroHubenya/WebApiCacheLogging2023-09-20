using Interfaces;
using Microsoft.Extensions.Options;
using Models;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Linq;
using System.Threading.Tasks;

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
            try
            {
                if (box == null)
                {
                    throw new ArgumentNullException(nameof(box));
                }
                else
                {
                    await _boxCollection.InsertOneAsync(box);
                }
            }
            catch (Exception)
            {
                throw;
            }
            
        }

        // Get all boxes.
        public async Task<List<Box>> GetAllBoxesAsync()
        {
            try
            {
                var result = await _boxCollection.Find(new BsonDocument()).ToListAsync();

                if (result == null)
                {
                    throw new Exception("The list of all boxes is not found.");
                }
                else
                {
                    return result;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        // Get box by id.
        public async Task<Box> GetBoxAsync(string id)
        {
            try
            {
                if (id == null)
                {
                    throw new ArgumentNullException(nameof(id));
                }
                else
                {
                    var filter = Builders<Box>.Filter.Eq(b => b.Id, id);

                    var result = await _boxCollection.Find(filter).FirstOrDefaultAsync();

                    if (result == null)
                    {
                        throw new Exception($"The Box with the {id} Id is not found.");
                    }
                    else
                    {
                        return result;
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        // Update box.
        public async Task<UpdateResult> UpdateBoxAsync(string id, Box newBox)
        {
            try
            {
                if (id == null)
                {
                    throw new ArgumentNullException(nameof(id));
                }
                else if (newBox == null)
                {
                    throw new ArgumentNullException(nameof(newBox));
                }
                else
                {
                    var filter = Builders<Box>.Filter.Eq(b => b.Id, id);

                    var update = Builders<Box>.Update.Set(b => b.Name, newBox.Name);

                    var result = await _boxCollection.UpdateOneAsync(filter, update);

                    if (result == null)
                    {
                        throw new Exception($"The Box with the {id} Id is not updated.");
                    }
                    else
                    {
                        return result;
                    }
                }
            }
            catch
            {
                throw;
            }
        }

        // Delete box.
        public async Task<DeleteResult> DeleteBoxAsync(string id)
        {
            try
            {
                if (string.IsNullOrEmpty(id))
                {
                    throw new ArgumentNullException(nameof(id));
                }
                else
                {
                    var filter = Builders<Box>.Filter.Eq(b => b.Id, id);

                    var result = await _boxCollection.DeleteOneAsync(filter);

                    if (result == null)
                    {
                        throw new Exception($"The Box with the {id} Id is not deleted.");
                    }
                    else
                    {
                        return result;
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        // Add sensor to the box.
        public async Task<ReplaceOneResult> AddSensorToBoxAsync(string boxId, string sensorId)
        {
            try
            {
                if (string.IsNullOrEmpty(boxId))
                {
                    throw new ArgumentNullException(nameof(boxId));
                }
                else if (string.IsNullOrEmpty(sensorId))
                {
                    throw new ArgumentNullException(nameof(sensorId));
                }
                else
                {
                    var boxFilter = Builders<Box>.Filter.Eq(b => b.Id, boxId);

                    Box box = await _boxCollection.Find(boxFilter).FirstOrDefaultAsync();

                    if (box == null)
                    {
                        throw new Exception($"The Box with {boxId} Id is not found.");
                    }
                    else
                    {
                        box.SensorIds.Add(sensorId);

                        var result = await _boxCollection.ReplaceOneAsync(boxFilter, box);

                        if (result == null)
                        {
                            throw new Exception($"The Sensor with the {sensorId} Id is not added to the Box with {boxId} Id.");
                        }
                        else
                        {
                            return result;
                        }
                    }
                }
            }
            catch
            {
                throw;
            }
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

        // SensorData CRUD. ----------------------------------------------------------------

        // Create SensorData for the specific sensor.
        public async Task CreateSensorDataAsync(SensorData sensorData)
        {
            // Add validation to be sure, that the sensor exist in sensors collection.

            await _sensorDataCollection.InsertOneAsync(sensorData);
        }

        // Get SensorData of the specific sensor.
        public async Task<List<SensorData>> GetSensorsDataBySensorIdAsync(string sensorId)
        {
            var filter = Builders<SensorData>.Filter.Eq(sd => sd.SensorId, sensorId);

            return await _sensorDataCollection.Find(filter).ToListAsync();
        }

        // Get SensorData of the specific sensor with pagination.
        public async Task<(int totalPages, IReadOnlyList<SensorData> readOnlyList)> GetSensorsDataPaginationAsync(string sensorId,
                                                                                                                  int page,
                                                                                                                  int pageSize)
        {
            var countFacet = AggregateFacet.Create("count", PipelineDefinition<SensorData, AggregateCountResult>
                                           .Create(new[]
                                           {
                                               PipelineStageDefinitionBuilder.Count<SensorData>()
                                           }));

            var dataFacet = AggregateFacet.Create("data", PipelineDefinition<SensorData, SensorData>
                                          .Create(new[]
                                          {
                                              PipelineStageDefinitionBuilder.Sort(Builders<SensorData>.Sort.Ascending(sd => sd.SensorId == sensorId)),
                                              PipelineStageDefinitionBuilder.Skip<SensorData>((page - 1) * pageSize),
                                              PipelineStageDefinitionBuilder.Limit<SensorData>(pageSize),
                                          }));

            var filter = Builders<SensorData>.Filter.Empty;

            var aggregation = await _sensorDataCollection.Aggregate()
                                                         .Match(filter)
                                                         .Facet(countFacet, dataFacet)
                                                         .ToListAsync();

            var count = aggregation.First()
                                   .Facets.First(x => x.Name == "count")
                                   .Output<AggregateCountResult>()
                                   ?.FirstOrDefault()
                                   ?.Count ?? 0;

            var totalPages = (int)count / pageSize;

            var data = aggregation.First()
                                  .Facets.First(x => x.Name == "data")
                                  .Output<SensorData>();

            return (totalPages, data);
        }


        // Delete all SensorData of the specific sensor.
        public async Task<DeleteResult> DeleteAllSensorDataBySensorIdAsync(string sensorId)
        {
            var filter = Builders<SensorData>.Filter.Eq(sd => sd.SensorId, sensorId);

            return await _sensorDataCollection.DeleteManyAsync(filter);
        }
    }
}