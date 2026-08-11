using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using GFlowApp.Services;
using Npgsql.Internal.Postgres;

namespace GFlowApp.ViewModels.Properties
{
    public partial class NodePropertiesViewModel : ObservableObject
    {

        public ObservableCollection<PropertyFieldViewModel> Fields { get; } = new();

        private NodeTypeSchema? _schema;
        private Action<Dictionary<string, JsonElement>>? _onApply;

        public void LoadSchema(
            NodeTypeSchema? schema,
            Dictionary<string, JsonElement> currentValues,
            Action<Dictionary<string, JsonElement>>? onApply
        )
        {
            _schema = schema;
            _onApply = onApply;
            Fields.Clear();
            if (schema is not null)
            {
                foreach (var prop in schema.Properties)
                {
                    currentValues.TryGetValue(prop.Name, out var element);
                    var hasValue = currentValues.ContainsKey(prop.Name);
                    Fields.Add(PropertyFieldViewModelFactory.Create(prop, hasValue ? element : default));
                }
            }

        }

        [RelayCommand]
        private void Apply()
        {
            var result = Fields.ToDictionary(f => f.Schema.Name, f => f.ToJsonElement());
            _onApply?.Invoke(result);
        }
    }
}