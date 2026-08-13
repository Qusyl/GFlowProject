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

        public AnyFieldPropertyViewModel(PropertySchema schema, JsonElement? element) : base(schema)
        {
            Value = element;
        }

        public override JsonElement ToJsonElement()
        {
            return Value ?? JsonSerializer.SerializeToElement<object?>(null);
        }
    }
}