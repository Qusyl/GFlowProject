using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using GFlowApp.Services;

namespace GFlowApp.ViewModels.Properties
{
    public partial class AnyFieldPropertyViewModel : PropertyFieldViewModel
    {
        [ObservableProperty]
        private JsonElement? _value;

        [ObservableProperty]
        private string? _textValue;

        public AnyFieldPropertyViewModel(PropertySchema schema, JsonElement? element) : base(schema)
        {
            Value = element;
        }

        public override void LoadFromJson(JsonElement element)
        {
            if(element.ValueKind == JsonValueKind.String)
            {
                TextValue = element.GetString();
            }else if(element.ValueKind == JsonValueKind.Null)
            {
                TextValue = null;
            }
            else
            {
                TextValue = element.GetRawText();
            }
        }

        public override JsonElement ToJsonElement()
        {
            return Value ?? JsonSerializer.SerializeToElement<object?>(null);
        }
    }
}