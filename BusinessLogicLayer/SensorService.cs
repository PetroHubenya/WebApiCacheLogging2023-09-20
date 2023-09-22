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
            await _dataService.CreateSensorAsync(sensor);
        }

        // Get all sensors.
        public async Task<List<Sensor>> GetAllSensorsAsync()
        {
            return await _dataService.GetAllSensorsAsync();
        }

        // Get sensor by id.
        public async Task<Sensor> GetSensorAsync(string id)
        {
            return await _dataService.GetSensorAsync(id);
        }

        // Update sensor.
        public async Task UpdateSensorAsync(string id, Sensor newSensor)
        {
            await _dataService.UpdateSensorAsync(id, newSensor);
        }

        // Delete sensor.
        public async Task DeleteSensorAsync(string id)
        {
            await _dataService.DeleteSensorAsync(id);
        }
    }
}
