using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using GFlowApp.Services;
using GFlowApp.Services.Schemas;

namespace GFlowApp.ViewModels.Properties
{
    public class DictionaryFieldPropertyViewModel : PropertyFieldViewModel
    {

        public ObservableCollection<DictionaryItemViewModel> Values { get; set; } = new();
        public DictionaryFieldPropertyViewModel(PropertySchema schema, JsonElement? element, Dictionary<string, List<PropertyVariant>> references) : base(schema)
        {
            if(element is not null && element is { ValueKind: JsonValueKind.Object} obj)
            {
                foreach(var property in obj.EnumerateObject())
                {
                    var field = PropertyFieldViewModelFactory.Create(schema.ElementSchema!, property.Value, references);
                    Values.Add(new DictionaryItemViewModel(property.Name, field));
                }
            }
        }

        public override JsonElement ToJsonElement()
        {
            var dictionary = Values.ToDictionary(
                key => key.Key,
                value => value.Value.ToJsonElement());
            return JsonSerializer.SerializeToElement(dictionary);
        }
    }
}