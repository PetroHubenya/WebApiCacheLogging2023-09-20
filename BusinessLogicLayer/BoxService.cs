using Interfaces;
using Models;

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

    }
}