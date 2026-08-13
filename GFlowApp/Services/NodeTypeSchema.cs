using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GFlowApp.Services.Schemas;

namespace GFlowApp.Services
{
    public class NodeTypeSchema { 
        public List<PropertySchema> Properties { get; set; }
        public Dictionary<string, List<PropertyVariant>> References { get; set; } = new();
    };
}