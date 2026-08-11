using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using Avalonia.Media;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Domain.Nodes;
using GFlowApp.Services;

namespace GFlowApp.ViewModels;

public partial class MainViewModel : ViewModelBase
{
    private readonly IClientService _serviceClient;
    private BlockViewModel? _selectedBlock;

    [ObservableProperty]
    private bool _isConnectionMode;

    public ObservableCollection<BlockItem> ActionItems { get; }

    public ObservableCollection<BlockItem> LogicItems { get; }

    public ObservableCollection<BlockItem> TriggerItems { get; }

    public ObservableCollection<BlockViewModel> Blocks { get; }

    public ObservableCollection<ConnectionViewModel> Connections { get; }

    [ObservableProperty]
    private string? selectedItem;

    public MainViewModel(IClientService clientService)
    {
        _serviceClient = clientService;
        ActionItems = new ObservableCollection<BlockItem>
        {
            new BlockItem(new BlockViewModel("SqlAction", Brushes.Red, 500, 500, _serviceClient ), "Action"),
            new BlockItem(new BlockViewModel("HttpAction", Brushes.Red, 500, 500,_serviceClient ), "Action")
        };
        LogicItems = new ObservableCollection<BlockItem>
        {
            new BlockItem(new BlockViewModel("CompareLogic", Brushes.Yellow, 500, 500, _serviceClient), "Logic")
        };
        TriggerItems = new ObservableCollection<BlockItem>
        {
             new BlockItem(new BlockViewModel("ManualTrigger", Brushes.Red, 500, 500, _serviceClient), "Trigger"),
        };
        Blocks = new ObservableCollection<BlockViewModel>();

        Connections =
            new ObservableCollection<ConnectionViewModel>();

        SelectedItem = TriggerItems[0].Model.NodeType;
    }

    [RelayCommand]
    public void ToggleConnectionMode()
    {

        _isConnectionMode = !_isConnectionMode;
        if (!_isConnectionMode)
        {
            if (_selectedBlock != null)
                _selectedBlock.IsSelected = false;

            _selectedBlock = null;
        }

        System.Console.WriteLine(
            $"Connection mode: {IsConnectionMode}");
    }

    [RelayCommand]
    private void AddBlock(string blockType)
    {
        Console.WriteLine(
            $"Добавлен блок {blockType}");

        var random = new Random();

        var randX = random.Next(100, 700);
        var randY = random.Next(100, 500);

        var color = blockType switch
        {
            string type when type.Contains("Action")
                => Brushes.Red,

            string type when type.Contains("Logic")
                => Brushes.Yellow,

            string type when type.Contains("Trigger")
                => Brushes.Azure,

            _ => Brushes.Bisque
        };

        Blocks.Add(
            new BlockViewModel(
                blockType,
                color,
                randX,
                randY,
                 _serviceClient));
    }

    [RelayCommand]
    private void PortClicked(BlockViewModel block)
    {

      
            

        
        if (!IsConnectionMode)
        {
            Console.WriteLine(
                "Connection mode is OFF");

            return;
        }

  
        if (_selectedBlock == null)
        {
            _selectedBlock = block;
            block.IsSelected = true;

            
               

            return;
        }


        if (_selectedBlock == block)
        {
            block.IsSelected = false;

            _selectedBlock = null;

            Console.WriteLine(
                "Connection cancelled");

            return;
        }

     
        var connection =
            new ConnectionViewModel(
                _selectedBlock,
                block);

        Connections.Add(connection);


    
        
        _selectedBlock.IsSelected = false;

        _selectedBlock = null;

        IsConnectionMode = false;
    }
}