using Interfaces;
using Models;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer
{
    public class SensorDataService : ISensorDataService
    {
        private readonly IDataService _dataService;

        public SensorDataService(IDataService dataService)
        {
            _dataService = dataService;
        }

        // Create SensorData for the specific sensor.
        public async Task CreateSensorDataAsync(SensorData sensorData)
        {
            if (sensorData == null)
            {
                throw new ArgumentNullException(nameof(sensorData));
            }
            else
            {
                if (sensorData.SensorId == null)
                {
                    throw new Exception("The received SensorData cannot be stored in the database, because it does not contain SensorId.");
                }
                else
                {
                    Sensor sensor = await _dataService.GetSensorAsync(sensorData.SensorId);

                    if (sensor == null)
                    {
                        throw new Exception($"The received SensorData contains {sensorData.SensorId} SensorId, that does not correspond to any sensor in the database.");
                    }
                    else
                    {
                        await _dataService.CreateSensorDataAsync(sensorData);
                    }
                }
            }
        }

        // Get SensorData of the specific sensor.
        public async Task<List<SensorData>> GetSensorsDataBySensorIdAsync(string sensorId)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(sensorId))
                {
                    throw new ArgumentNullException(nameof (sensorId));
                }
                else
                {
                    var result = await _dataService.GetSensorsDataBySensorIdAsync(sensorId);

                    if (result == null)
                    {
                        throw new Exception($"The list of SensorData, that correspond to the sensor with the {sensorId} Id, is not found.");
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

        // Get SensorData of the specific sensor with pagination.
        public async Task<(int totalPages, IReadOnlyList<SensorData> readOnlyList)> GetSensorsDataPaginationAsync(string sensorId,
                                                                                                                  int page,
                                                                                                                  int pageSize)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(sensorId))
                {
                    throw new ArgumentNullException(nameof (sensorId));
                }                
                else if (page <= 0)
                {
                    throw new ArgumentOutOfRangeException(nameof (page));
                }                
                else if (pageSize <= 0)
                {
                    throw new ArgumentOutOfRangeException(nameof (pageSize), "The page size must be greater than 0.");
                }
                else
                {
                    var (totalPages, readOnlyList) = await _dataService.GetSensorsDataPaginationAsync(sensorId, page, pageSize);

                    if (readOnlyList == null)
                    {
                        throw new Exception("The list of SensorData is not found.");
                    }
                    else
                    {
                        return (totalPages, readOnlyList);
                    }
                }
            }
            catch
            {
                throw;
            }
        }

        // Delete all SensorData of the specific sensor.
        public async Task<DeleteResult> DeleteAllSensorDataBySensorIdAsync(string sensorId)
        {
            try
            {
                if (string.IsNullOrEmpty(sensorId))
                {
                    throw new ArgumentNullException(nameof (sensorId));
                }
                else
                {
                    var result = await _dataService.DeleteAllSensorDataBySensorIdAsync(sensorId);

                    if (result == null)
                    {
                        throw new Exception("Could not delete SensorData.");
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
    }
}
