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
    public class SensorService : ISensorService
    {
        private readonly IDataService _dataService;

        public SensorService(IDataService dataService)
        {
            _dataService = dataService;
        }

        // Create sensor.
        public async Task CreateSensorAsync(Sensor sensor)
        {
            try
            {
                if(sensor == null)
                {
                    throw new ArgumentNullException(nameof(sensor));
                }
                else
                {
                    await _dataService.CreateSensorAsync(sensor);
                }
            }
            catch
            {
                throw;
            }
        }

        // Get all sensors.
        public async Task<List<Sensor>> GetAllSensorsAsync()
        {
            try
            {
                var result = await _dataService.GetAllSensorsAsync();

                if (result == null)
                {
                    throw new Exception("The list of all sensors not found.");
                }
                else
                {
                    return result;
                }
            }
            catch
            {
                throw;
            }
        }

        // Get sensor by id.
        public async Task<Sensor> GetSensorAsync(string id)
        {
            try
            {
                if (string.IsNullOrEmpty(id))
                {
                    throw new ArgumentNullException(nameof(id));
                }
                else
                {
                    var result = await _dataService.GetSensorAsync(id);

                    if (result == null)
                    {
                        throw new Exception($"The sensor with the {id} Id is not found.");
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

        // Update sensor.
        public async Task UpdateSensorAsync(string id, Sensor newSensor)
        {
            try
            {
                if (string.IsNullOrEmpty(id))
                {
                    throw new ArgumentNullException(nameof(id));
                }
                else if (newSensor == null)
                {
                    throw new ArgumentNullException(nameof(newSensor));
                }
                else
                {
                    await _dataService.UpdateSensorAsync(id, newSensor);
                }
            }
            catch
            {
                throw;
            }
        }

        // Delete sensor.
        public async Task DeleteSensorAsync(string id)
        {
            try
            {
                if (string.IsNullOrEmpty(id))
                {
                    throw new ArgumentNullException(nameof(id));
                }
                else
                {
                    await _dataService.DeleteAllSensorDataBySensorIdAsync(id);

                    await _dataService.DeleteSensorAsync(id);
                }
            }
            catch
            {
                throw;
            }
        }
    }
}
