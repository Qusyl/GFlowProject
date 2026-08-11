using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GFlowApp.Services
{
    public class PropertySchema
    {
        public string Type { get; set; }
        public string Name { get; set; }
        public bool IsRequired { get; set; }
        
        public List<string>? EnumValues { get; set; }

        public List<PropertySchema> Properties { get; set; }

    }
}