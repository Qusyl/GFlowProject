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
    public partial class ArrayFieldPropertyViewModel : PropertyFieldViewModel
    {
        public ObservableCollection<PropertyFieldViewModel> Values { get; set; } = new();
        public ArrayFieldPropertyViewModel(PropertySchema schema, JsonElement? element, Dictionary<string, List<PropertyVariant>> references ) : base(schema)
        {
            if(element is not null && element is { ValueKind: JsonValueKind.Array} arr )
            {
                foreach(var item in arr.EnumerateArray())
                {
                    Values.Add(
                    PropertyFieldViewModelFactory.Create(schema.ElementSchema!, item,references)
                    );
                }
            }
            
        }

        public override JsonElement ToJsonElement()
        {
            var values = Values
            .Select(x => x.ToJsonElement())
            .ToList();

            return JsonSerializer.SerializeToElement(values);
        }

        public override void LoadFromJson(JsonElement element)
        {
            throw new NotImplementedException();
        }
    }
}