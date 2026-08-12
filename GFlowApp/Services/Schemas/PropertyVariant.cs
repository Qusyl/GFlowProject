using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GFlowApp.Services.Schemas
{
    public class PropertyVariant
    {
        public string Name { get; set; }

        public List<PropertySchema> Properties { get; set; } = new();
    }
}