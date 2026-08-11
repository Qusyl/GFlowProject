using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using GFlowApp.Services;

namespace GFlowApp.ViewModels.Properties
{
    public static class PropertyFieldViewModelFactory 
    {
        public static PropertyFieldViewModel Create(PropertySchema schema, JsonElement? element)
        {
            return schema.Type switch
            {
                "string" => new TextFieldPropertyViewModel(schema, element),
                "number" => new NumericFieldPropertyViewModel(schema, element),
                "boolean" => new BooleanFieldPropertyViewModel(schema, element),
                "enum" => new EnumFieldPropertyViewModel(schema, element),
                _ => new TextFieldPropertyViewModel(schema, element) 
            };
        } 
    }
}