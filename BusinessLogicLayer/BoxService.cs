using Interfaces;

namespace BusinessLogicLayer
{
    public class BoxService : IBoxService
    {
        private readonly IDataService _dataService;

        public BoxService(IDataService dataService)
        {
            _dataService = dataService;
        }

    }
}