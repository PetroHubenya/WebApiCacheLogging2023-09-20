﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class MongoDBSettings
    {
        public string ConnectionURL { get; set; } = null!;

        public string DatabaseName { get; set; } = null!;

        public string CollectionNameBoxes { get; set; } = null!;

        public string CollectionNameSensors { get; set; } = null!;

        public string CollectionNameSensorsData { get; set; } = null!;
    }
}
