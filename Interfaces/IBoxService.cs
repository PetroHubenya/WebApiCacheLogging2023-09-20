﻿using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interfaces
{
    public interface IBoxService
    {
        Task CreateBoxAsync(Box box);

        Task<List<Box>> GetAllBoxesAsync();
    }
}
