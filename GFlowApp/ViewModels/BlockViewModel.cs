using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text.Json;
using System.Threading.Tasks;
using Application.Dto;
using Avalonia.Controls;
using Avalonia.Media;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Domain.Nodes;
using Domain.Ports;
using GFlowApp.Services;
using GFlowApp.Services.Window;
using GFlowApp.ViewModels.Properties;
using GFlowApp.Views;

namespace GFlowApp.ViewModels
{
    public partial class BlockViewModel : ObservableObject
    {
        private readonly IClientService _clientService;

        private readonly IWindowService _parentWindow;
        [ObservableProperty]
        public partial int NodeId { get; set; }

        [ObservableProperty]

        public partial BlockTypes NodeType { get; set; }

        [ObservableProperty]
        private ObservableCollection<PortViewModel> _inputPorts = new();

        [ObservableProperty]
        private ObservableCollection<PortViewModel> _outputPorts = new();

        public NodeCategory Category { get; }
        [ObservableProperty]
        private bool _isFlyoutOpen;

        [ObservableProperty]
        private Dictionary<string, JsonElement> _properties = new();

        [ObservableProperty]
        private NodePropertiesViewModel _propertiesEditor  = new();

        [ObservableProperty]
        public partial double X { get; set; }

        [ObservableProperty]
        public partial double Y { get; set; }

        [ObservableProperty]
        public partial IBrush Color { get; set; }

        [ObservableProperty]
        public partial bool IsSelected { get; set; } = false;
        public BlockViewModel(int id,IWindowService parentWindow,BlockTypes nodeType, NodeCategory category, IBrush color, double x, double y, IClientService clientService, List<PortsDescriptor> ports)
        {
            _parentWindow = parentWindow;
            _clientService = clientService;
            NodeId = id;
            Category = category;
            NodeType = nodeType;
            X = x;
            Y = y;
            Color = color;
            InitializePorts(ports);
        }
        private void InitializePorts(List<PortsDescriptor> ports)
        {
            foreach (var port in ports)
            {
                if (port.PortDirection is PortsDirection.Input)
                {
                    _inputPorts.Add(new PortViewModel(port.PortName, PortsDirection.Input));
                }
                else
                {
                    _outputPorts.Add(new PortViewModel(port.PortName, PortsDirection.Output));
                }
            }
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
         NodeType.ToString(),
        Properties.ToDictionary(kvp => kvp.Key, kvp => (object?)kvp.Value));
[RelayCommand]
private void OpenFlyout()
{
    IsFlyoutOpen = true;
}



        [RelayCommand]
        public async Task OpenNodeProperties()
        {

            var schema = await _clientService.GetSchemaAsync(NodeType.ToString());
            if (schema is null)
            {
                return;
            }

            PropertiesEditor.LoadSchema(schema, Properties, OnPropertiesApplied);
            var dialog = new DialogWindow();

            dialog.DataContext = new PropertyDialogViewModel
            {
                BlockName = NodeType.ToString(),
                Fields = PropertiesEditor.Fields
            };

            var vm = (PropertyDialogViewModel)dialog.DataContext;
            vm.OnApply += () =>
            {
                PropertiesEditor.ApplyCommand.Execute(null);
                dialog.Close();
            };
            vm.OnDecline += () =>
            {
                dialog.Close();
            };
            var mWin = _parentWindow.GetMainWindow();
            await dialog.ShowDialog(mWin);
        }

        private void OnPropertiesApplied(Dictionary<string, JsonElement> properties)
        {
            Properties = properties;
            OnPropertyChanged(nameof(Properties));
            System.Console.WriteLine($"Блок {NodeType} обновлен!");
           
        }
    }
}