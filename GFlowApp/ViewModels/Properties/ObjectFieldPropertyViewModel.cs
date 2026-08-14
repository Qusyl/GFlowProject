using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using GFlowApp.Services;
using GFlowApp.Services.Schemas;

namespace GFlowApp.ViewModels.Properties
{
    public class ObjectFieldPropertyViewModel : PropertyFieldViewModel
    {
        public ObservableCollection<PropertyFieldViewModel> Fields { get; } = new();
        public ObjectFieldPropertyViewModel(PropertySchema schema, JsonElement? element, Dictionary<string, List<PropertyVariant>> references) : base(schema)
        {
            if (schema.Properties is null)
            {
                return;
            }
            foreach(var propertySchema in schema.Properties)
            {
                JsonElement? value = null;

                if (element is { ValueKind: JsonValueKind.Object } obj && obj.TryGetProperty(propertySchema.Name, out var property))
                {
                    value = property;
                }
                Fields.Add(PropertyFieldViewModelFactory.Create(propertySchema, value, references));
            }
           
        }

        public override JsonElement ToJsonElement()
        {
            var dictionary = Fields.ToDictionary(
                kvp => kvp.Schema.Name, kvp => kvp.ToJsonElement()
            );

            return JsonSerializer.SerializeToElement(dictionary);
        }

        public override void LoadFromJson(JsonElement element)
        {
            throw new NotImplementedException();
        }
    }
}