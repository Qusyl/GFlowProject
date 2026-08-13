using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using GFlowApp.Services;
using GFlowApp.Services.Schemas;

namespace GFlowApp.ViewModels.Properties
{
    public partial class ReferenceFieldPropertyViewModel : PropertyFieldViewModel
    {
        public PropertyFieldViewModel? Value { get; set; }
        public ReferenceFieldPropertyViewModel(PropertySchema schema,JsonElement? element ,Dictionary<string,List<PropertyVariant>> variants) : base(schema)
        {
            var schemaRef = schema.Ref;
            if(schemaRef is not null )
            {
                if(variants.TryGetValue(schemaRef, out var propertyVariants))
                {
                    Value = new PolymorphicFieldPropertyViewModel(schema, element, variants);
                }
            }
        }

        public override JsonElement ToJsonElement()
        {
            return Value?.ToJsonElement() ?? JsonSerializer.SerializeToElement<object?>(null);
        }
    }
}