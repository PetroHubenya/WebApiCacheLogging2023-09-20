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
                    throw new ArgumentNullException(nameof(sensorData.SensorId));
                }
                else
                {
                    Sensor sensor = await _dataService.GetSensorAsync(sensorData.SensorId);

                    if (sensor == null)
                    {
                        throw new ArgumentNullException(nameof(sensor));
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
            return await _dataService.GetSensorsDataBySensorIdAsync(sensorId);
        }

        // Get SensorData of the specific sensor with pagination.
        public async Task<(int totalPages, IReadOnlyList<SensorData> readOnlyList)> GetSensorsDataPaginationAsync(string sensorId,
                                                                                                                  int page,
                                                                                                                  int pageSize)
        {
            return await _dataService.GetSensorsDataPaginationAsync(sensorId, page, pageSize);
        }

        // Delete all SensorData of the specific sensor.
        public async Task<DeleteResult> DeleteAllSensorDataBySensorIdAsync(string sensorId)
        {
            return await _dataService.DeleteAllSensorDataBySensorIdAsync(sensorId);
        }
    }
}
