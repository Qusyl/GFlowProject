using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using Avalonia.Media;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace GFlowApp.ViewModels;

public partial class MainViewModel : ViewModelBase
{
    private BlockViewModel? _selectedBlock;

    [ObservableProperty]
    private bool _isConnectionMode;

    public ObservableCollection<string> ActionItems { get; }

    public ObservableCollection<string> LogicItems { get; }

    public ObservableCollection<string> TriggerItems { get; }

    public ObservableCollection<BlockViewModel> Blocks { get; }

    public ObservableCollection<ConnectionViewModel> Connections { get; }

    [ObservableProperty]
    private string? selectedItem;

    public MainViewModel()
    {
        ActionItems = new ObservableCollection<string>
        {
            "SqlAction",
            "HttpAction"
        };

        LogicItems = new ObservableCollection<string>
        {
            "CompareLogic"
        };

        TriggerItems = new ObservableCollection<string>
        {
            "ManualTrigger"
        };

        Blocks = new ObservableCollection<BlockViewModel>();

        Connections =
            new ObservableCollection<ConnectionViewModel>();

        SelectedItem = TriggerItems[0];
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
                randY));
    }

    [RelayCommand]
    private void PortClicked(BlockViewModel block)
    {

        Console.WriteLine(
            $"Port clicked: {block.Name}");

        
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

            Console.WriteLine(
                $"Start block: {block.Name}");

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

        Console.WriteLine(
            $"Connection: {_selectedBlock.Name} -> {block.Name}");

        
        _selectedBlock.IsSelected = false;

        _selectedBlock = null;

        IsConnectionMode = false;
    }
}