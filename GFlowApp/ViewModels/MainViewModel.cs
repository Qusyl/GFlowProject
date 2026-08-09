using System;
using System.Collections.ObjectModel;
using System.Linq;
using Avalonia.Media;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace GFlowApp.ViewModels;

public partial class MainViewModel : ViewModelBase
{
    [ObservableProperty]
    public partial string Greeting { get; set; } = "Welcome to Avalonia!";

    public ObservableCollection<string> ActionItems { get; }

    public ObservableCollection<string> LogicItems { get; }
    
    public ObservableCollection<string> TriggerItems { get; }

    public ObservableCollection<BlockViewModel> Blocks { get; set; } 

    [ObservableProperty]
    public partial string? SelectedItem { get; set; }

    [RelayCommand]
    public void AddBlock(string blockType)
    {
        System.Console.WriteLine($"Добавлен блок {blockType}");
        var random = new Random();
    
        double randX = random.Next(500, 700);

        double randY = random.Next(500, 700);

        var color = blockType switch
        {
            string type when type.Contains("Action") => Brushes.Red,
            string type when type.Contains("Logic") => Brushes.Yellow,
            string type when type.Contains("Trigger") => Brushes.Azure,
            _ => Brushes.Bisque 
        };
        Blocks.Add(new BlockViewModel($"{blockType}",color,randX, randY));
    }

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
        Blocks = new();
        SelectedItem = TriggerItems[0];
    }
}
