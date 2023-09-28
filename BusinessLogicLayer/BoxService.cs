using Interfaces;
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
            try
            {
                if (box == null)
                {
                    throw new ArgumentNullException(nameof(box));
                }
                else
                {
                    await _dataService.CreateBoxAsync(box);
                }
            }
            catch
            {
                throw;
            }
        }

        // Get all boxes
        public async Task<List<Box>> GetAllBoxesAsync()
        {
            try
            {
                var result = await _dataService.GetAllBoxesAsync();

                if (result == null)
                {
                    throw new Exception("The list of boxes is not found.");
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

        // Get box by id.
        public async Task<Box> GetBoxAsync(string id)
        {
            try
            {
                if (string.IsNullOrEmpty(id))
                {
                    throw new ArgumentNullException(nameof(id));
                }
                else
                {
                    var result = await _dataService.GetBoxAsync(id);

                    if (result == null)
                    {
                        throw new Exception($"The box with the {id} Id is not found.");
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

        // Update box.
        public async Task UpdateBoxAsync(string id, Box newBox)
        {
            try
            {
                if (string.IsNullOrEmpty(id))
                {
                    throw new ArgumentNullException(nameof(id));
                }
                else if (newBox == null)
                {
                    throw new ArgumentNullException(nameof(newBox));
                }
                else
                {
                    await _dataService.UpdateBoxAsync(id, newBox);
                }
            }
            catch
            {
                throw;
            }
        }

        // Delete box.
        public async Task DeleteBoxAsync(string id)
        {
            try
            {
                if (string.IsNullOrEmpty(id))
                {
                    throw new ArgumentNullException(nameof(id));
                }
                else
                {
                    // Delete all the SensorData for the correspondent sensors.

                    var box = await _dataService.GetBoxAsync(id);

                    if (box == null)
                    {
                        throw new Exception($"The box with the {id} Id is not found.");
                    }
                    else
                    {
                        foreach (string sensorId in box.SensorIds)
                        {
                            await _dataService.DeleteAllSensorDataBySensorIdAsync(sensorId);
                        }

                        // Delete the box.

                        await _dataService.DeleteBoxAsync(id);
                    }
                }
            }
            catch
            {
                throw;
            }
        }

        // Add sensor to the box.
        public async Task AddSensorToBoxAsync(string boxId, string sensorId)
        {
            try
            {
                if (string.IsNullOrEmpty(boxId))
                {
                    throw new ArgumentNullException(nameof(boxId));
                }
                else if (sensorId == null)
                {
                    throw new ArgumentNullException(nameof(sensorId));
                }
                else
                {
                    await _dataService.AddSensorToBoxAsync(boxId, sensorId);
                }
            }
            catch
            {
                throw;
            }
        }
    }
}