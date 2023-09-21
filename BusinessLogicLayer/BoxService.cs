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

        // Create
        public async Task CreateBoxAsync(Box box)
        {
            await _dataService.CreateBoxAsync(box);
        }

    }
}