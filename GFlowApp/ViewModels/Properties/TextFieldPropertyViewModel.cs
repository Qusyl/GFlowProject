using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using GFlowApp.Services;

namespace GFlowApp.ViewModels.Properties
{
    public partial class TextFieldPropertyViewModel : PropertyFieldViewModel
    {
        [ObservableProperty]
        private string? _value;

        public TextFieldPropertyViewModel(PropertySchema schema, JsonElement? element) : base(schema)
        {
            Value = element is { ValueKind: JsonValueKind.String } e ? e.GetString() : element?.ToString();
        }

        public override void LoadFromJson(JsonElement element)
        {
            throw new NotImplementedException();
        }

        public override JsonElement ToJsonElement()
        {
            return JsonSerializer.SerializeToElement(Value);
        }
    }
}