using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text.Json;
using System.Text.Json.Nodes;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using GFlowApp.Services;
using GFlowApp.Services.Schemas;

namespace GFlowApp.ViewModels.Properties
{
    public partial class DictionaryFieldPropertyViewModel : PropertyFieldViewModel
    {
        private readonly Dictionary<string, List<PropertyVariant>> _references;
        private readonly PropertySchema _elementSchema;
        public ObservableCollection<DictionaryItemViewModel> Values { get; set; } = new();
        public DictionaryFieldPropertyViewModel(PropertySchema schema, JsonElement? element, Dictionary<string, List<PropertyVariant>> references) : base(schema)
        {
            _references = references;
            _elementSchema = schema.ElementSchema!;
           if(element.HasValue && element.Value.ValueKind == JsonValueKind.Object)
            {
                foreach(var property in element.Value.EnumerateObject())
                {
                    var field = PropertyFieldViewModelFactory.Create(_elementSchema, property.Value, _references);
                    Values.Add(new DictionaryItemViewModel(property.Name, field, this));
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

        [RelayCommand]
        public void AddItem()
        {
            var schema = Schema.ElementSchema!;
            var field = PropertyFieldViewModelFactory.Create(schema, null, _references);
            Values.Add(new DictionaryItemViewModel($"[{Values.Count + 1}]", field,this));
        }

        [RelayCommand]
        public void RemoveItem(DictionaryItemViewModel model)
        {
            Values.Remove(model);
        }

        public override void LoadFromJson(JsonElement element)
        {
            Values.Clear();
            if (element.ValueKind != JsonValueKind.Object)
            {
                return;
            }
            
            foreach(var property in element.EnumerateObject())
            {
                var field = PropertyFieldViewModelFactory.Create(Schema.ElementSchema!, element, _references);

                Values.Add(new DictionaryItemViewModel(property.Name, field, this));
            }
        }
    }
}