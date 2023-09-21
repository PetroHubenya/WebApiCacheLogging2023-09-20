using Interfaces;
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
    }
}
