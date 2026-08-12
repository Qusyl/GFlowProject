using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GFlowApp.Services.Schemas;

namespace GFlowApp.Services
{
    public class PropertySchema
    {
        public string Type { get; set; }
        public string? Ref { get; set; }
        public string Name { get; set; }
        public bool IsRequired { get; set; }
        
        public List<string>? EnumValues { get; set; }

        public string? Descriminator { get; set; }
        public PropertySchema? ElementSchema { get; set; }
        public List<PropertyVariant>? Variants { get; set; }
        public List<PropertySchema> Properties { get; set; }

    }
}