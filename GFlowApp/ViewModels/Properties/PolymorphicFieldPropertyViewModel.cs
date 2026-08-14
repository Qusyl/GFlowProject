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
    public partial class PolymorphicFieldPropertyViewModel : PropertyFieldViewModel
    {
        public ObservableCollection<PropertyFieldViewModel> Fields { get; set; } = new();
        public ObservableCollection<PropertyVariant> Variants { get; }

        private JsonElement? _initialElement;
        [ObservableProperty]
        private PropertyVariant? _selectedVariant;

        public PropertyFieldViewModel? Value { get; private set; }

        private readonly Dictionary<string, List<PropertyVariant>> _references;
 
        public PolymorphicFieldPropertyViewModel(PropertySchema schema, JsonElement? element, Dictionary<string,List<PropertyVariant>>? references) : base(schema)
        {
            _initialElement = element;
            _references = references ?? new();
            Variants = new ObservableCollection<PropertyVariant>(
                    schema.Variants ?? new()
            );
            if (element is { } json)
            {
                SelectedVariant = FindVariant(json);
            
            }
            
            if(SelectedVariant is not null)
            {
                BuildFields(SelectedVariant, element);
            }
        }

        public override JsonElement ToJsonElement()
{
    var dictionary = Fields.ToDictionary(
        field => field.Schema.Name,
        field => field.ToJsonElement()
    );

    if (!string.IsNullOrWhiteSpace(Schema.Descriminator))
    {
        dictionary[Schema.Descriminator] =
            JsonSerializer.SerializeToElement(SelectedVariant?.Name);
    }

    return JsonSerializer.SerializeToElement(dictionary);
}
        private void BuildFields(PropertyVariant variant, JsonElement? element)
        {
            Fields.Clear();
            foreach (var property in variant.Properties)
            {
                JsonElement? currentElement = null;
                if (element is { } json && json.ValueKind is JsonValueKind.Object && json.TryGetProperty(property.Name, out var value))
                {
                    currentElement = value;
                }

                Fields.Add(
                    PropertyFieldViewModelFactory.Create(
                    property, currentElement, _references)
            );
            } }
        partial void OnSelectedVariantChanged(PropertyVariant? value)
        {
            if (value is null)
            {
                Fields.Clear();
                return;
            }

            var elementForBuild = (_initialElement is { } json && FindVariant(json) == value) ? _initialElement : null;
            BuildFields(value, elementForBuild);
        }
        private PropertyVariant? FindVariant(JsonElement element)
        {
            if (element.ValueKind != JsonValueKind.Object)
            {
                return null;
            }
            var descrminator = Schema.Descriminator;
            if (string.IsNullOrWhiteSpace(descrminator))
            {
                return null;
            }
            if (!element.TryGetProperty(descrminator, out var descrElement))
            {
                return null;
            }
            if (descrElement is not { ValueKind: JsonValueKind.String })
            {
                return null;
            }

            var value = descrElement.GetString();

            return Variants
            .FirstOrDefault(v => string.Equals(v.Name, value, StringComparison.OrdinalIgnoreCase));
        }

        public override void LoadFromJson(JsonElement element)
        {
            throw new NotImplementedException();
        }
    }
}