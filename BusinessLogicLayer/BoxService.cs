﻿using Interfaces;
using Models;
using MongoDB.Driver;

namespace BusinessLogicLayer
{
    public class BoxService : IBoxService
    {
        private readonly IDataService _dataService;

        public BoxService(IDataService dataService)
        {
            _dataService = dataService;
        }

        // Create box
        public async Task CreateBoxAsync(Box box)
        {
            await _dataService.CreateBoxAsync(box);
        }

        // Get all boxes
        public async Task<List<Box>> GetAllBoxesAsync()
        {
            return await _dataService.GetAllBoxesAsync();
        }

        // Get box by id.
        public async Task<Box> GetBoxAsync(string id)
        {
            return await _dataService.GetBoxAsync(id);
        }

        // Update box.
        public async Task UpdateBoxAsync(Box newBox)
        {
            await _dataService.UpdateBoxAsync(newBox);
        }

        // Delete box.
        public async Task DeleteBoxAsync(string id)
        {
            await _dataService.DeleteBoxAsync(id);
        }

        // Add sensor to the box.
        public async Task AddSensorToBoxAsync(string boxId, string sensorId)
        {
            await _dataService.AddSensorToBoxAsync(boxId, sensorId);
        }
    }
}