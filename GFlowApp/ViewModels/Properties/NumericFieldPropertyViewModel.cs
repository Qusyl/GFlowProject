using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using GFlowApp.Services;

namespace GFlowApp.ViewModels.Properties
{
    public partial class NumericFieldPropertyViewModel : PropertyFieldViewModel
    {
        [ObservableProperty]
        private double? _value;
        public NumericFieldPropertyViewModel(PropertySchema schema, JsonElement? element) : base(schema)
        {
            Value = element is { ValueKind: JsonValueKind.Number } e ? e.GetDouble() : 0;
        }

        public override JsonElement ToJsonElement()
        {
            return JsonSerializer.SerializeToElement(Value);
        }
    }
}