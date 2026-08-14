using System;
using System.Collections.Generic;

using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using Application.Dto;
using Avalonia.Media;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Domain.Nodes;
using GFlowApp.Services;
using GFlowApp.ViewModels.Properties;

namespace GFlowApp.ViewModels
{
    public partial class BlockViewModel : ObservableObject
    {
        private readonly IClientService _clientService;
        [ObservableProperty]
        public partial int NodeId { get; set; }

        [ObservableProperty]

        public partial string NodeType { get; set; }

        public NodeCategory Category { get; }
        [ObservableProperty]
        private bool _isFlyoutOpen;



        [ObservableProperty]
        private Dictionary<string, JsonElement> _properties = new();

        public NodePropertiesViewModel PropertiesEditor { get; } = new();

        [ObservableProperty]
        public partial double X { get; set; }

        [ObservableProperty]
        public partial double Y { get; set; }

        [ObservableProperty]
        public partial IBrush Color { get; set; }

        [ObservableProperty]
        public partial bool IsSelected { get; set; } = false;
        public BlockViewModel(string nodeType,NodeCategory category ,IBrush color, double x, double y, IClientService clientService)
        {
            _clientService = clientService;
            Category = category;
            NodeType = nodeType;
            X = x;
            Y = y;
            Color = color;
        
        }
        public Dictionary<string, object?> LoadProperties()
        {
            var dictionary = new Dictionary<string, object?>();
            foreach (var property in Properties)
            {
                try
                {
                    var key = property.Key;
                    var value = JsonSerializer.Deserialize<object?>(property.Value);
                    dictionary.Add(key, value);

                }
                catch (Exception ex)
                {
                    System.Console.WriteLine($"Error while deserialize {ex.Message}");
                    throw;
                }
            }
            return dictionary;
        }
        public NodeExecutionDto ToDto()
        => new NodeExecutionDto(
         NodeType,
        Properties.ToDictionary(kvp => kvp.Key, kvp => (object?)kvp.Value));
[RelayCommand]
private void OpenFlyout()
{
    IsFlyoutOpen = true;
}


[RelayCommand]
private void CloseFlyout()
{
    IsFlyoutOpen = false;
}
        [RelayCommand]
        public async Task OpenNodeProperties()
        {
            IsFlyoutOpen = true;
            var schema = await _clientService.GetSchemaAsync(NodeType);

            if(schema is not null)
            {
                PropertiesEditor.LoadSchema(schema, Properties, OnPropertiesApplied);
            }
        }

        private void OnPropertiesApplied(Dictionary<string, JsonElement> properties)
        {
            Properties = properties;
            OnPropertyChanged(nameof(Properties));
            System.Console.WriteLine($"Блок {NodeType} обновлен!");
            IsFlyoutOpen = false;
        }
    }
}