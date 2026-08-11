using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using GFlowApp.Services;

namespace GFlowApp.ViewModels.Properties
{
    public partial class EnumFieldPropertyViewModel : PropertyFieldViewModel
    {
        [ObservableProperty]
        private string? _selectedValue;

        public List<string> Options { get; }

        public EnumFieldPropertyViewModel(PropertySchema schema, JsonElement? element) : base(schema)
        {
            Options = schema.EnumValues ?? new();
            SelectedValue = element is { ValueKind: JsonValueKind.String } e ? e.GetString() : Options.FirstOrDefault();
        }

        public override JsonElement ToJsonElement()
        {
            return JsonSerializer.SerializeToElement(SelectedValue);
        }
    }
}