using System;
using System.Collections.Generic;

using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using Application.Dto;
using Avalonia.Media;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

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

        public Dictionary<string, JsonElement> Properties { get; set; } = new();

        public NodePropertiesViewModel PropertiesEditor { get; } = new();

        [ObservableProperty]
        public partial double X { get; set; }

        [ObservableProperty]
        public partial double Y { get; set; }

        [ObservableProperty]
        public partial IBrush Color { get; set; }

        [ObservableProperty]
        public partial bool IsSelected { get; set; } = false;
        public BlockViewModel(string nodeType, IBrush color, double x, double y, IClientService clientService)
        {
            _clientService = clientService;
            NodeType = nodeType;
            X = x;
            Y = y;
            Color = color;
        }
        
        public NodeExecutionDto ToDto()
        => new NodeExecutionDto(
         NodeType,
        Properties.ToDictionary(kvp => kvp.Key, kvp => (object?)kvp.Value));

        [RelayCommand]
        public async Task OpenNodeProperties()
        {
            var schema = await _clientService.GetSchemaAsync(NodeType);

            if(schema is not null)
            {
                PropertiesEditor.LoadSchema(schema, Properties, update => Properties = update );
            }
        }
    }
}