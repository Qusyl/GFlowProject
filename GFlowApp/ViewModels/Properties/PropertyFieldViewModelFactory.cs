using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using GFlowApp.Services;
using GFlowApp.Services.Schemas;

namespace GFlowApp.ViewModels.Properties
{
    public static class PropertyFieldViewModelFactory 
    {
        public static PropertyFieldViewModel Create(PropertySchema schema, JsonElement? element, Dictionary<string, List<PropertyVariant>> references)
        {
            return schema.Type switch
            {
                "string" => new TextFieldPropertyViewModel(schema, element),

                "number" => new NumericFieldPropertyViewModel(schema, element),

                "boolean" => new BooleanFieldPropertyViewModel(schema, element),

                "enum" => new EnumFieldPropertyViewModel(schema, element),

                "object" => new ObjectFieldPropertyViewModel(schema, element, references),

                "array" => new ArrayFieldPropertyViewModel(schema, element, references),

                "dictionary" => new DictionaryFieldPropertyViewModel(schema, element, references),

                "any" => new AnyFieldPropertyViewModel(schema, element),

                "ref" => new ReferenceFieldPropertyViewModel(schema, element, references),

                "polymorphic" => new PolymorphicFieldPropertyViewModel(schema, element, references),

                _ => throw new NotSupportedException(
                    $"Unknown property type: {schema.Type}")
            };
        } 
    }
}