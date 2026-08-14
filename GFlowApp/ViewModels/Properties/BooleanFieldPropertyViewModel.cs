using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using GFlowApp.Services;

namespace GFlowApp.ViewModels.Properties
{
    public partial class BooleanFieldPropertyViewModel : PropertyFieldViewModel
    {
        [ObservableProperty]
        private bool _value;
        public BooleanFieldPropertyViewModel(PropertySchema schema, JsonElement? element) : base(schema)
        {
            Value = element is { ValueKind: JsonValueKind.False or JsonValueKind.True } e && e.GetBoolean();
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