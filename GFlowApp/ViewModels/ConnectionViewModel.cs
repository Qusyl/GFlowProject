using Avalonia;
using CommunityToolkit.Mvvm.ComponentModel;

namespace GFlowApp.ViewModels;

public partial class ConnectionViewModel : ObservableObject
{
    [ObservableProperty]
    private BlockViewModel _startBlock = null!;

    [ObservableProperty]
    private BlockViewModel _endBlock = null!;

    [ObservableProperty]
    private PortViewModel _startPort = null!;
    [ObservableProperty]
    private PortViewModel _endPort = null!;

    public Point StartPoint =>
        new(
            StartBlock.X + 75,
            StartBlock.Y + 150);

    public Point EndPoint =>
        new(
            EndBlock.X + 75,
            EndBlock.Y + 150);

    public ConnectionViewModel(
        BlockViewModel startBlock,
        BlockViewModel endBlock,
        PortViewModel startPort,
        PortViewModel endPort)
    {
        StartBlock = startBlock;
        EndBlock = endBlock;

        StartPort = startPort;
        EndPort = endPort;

        StartBlock.PropertyChanged += Block_PropertyChanged;
        EndBlock.PropertyChanged += Block_PropertyChanged;
        
    }

    private void Block_PropertyChanged(
        object? sender,
        System.ComponentModel.PropertyChangedEventArgs e)
    {
        if (e.PropertyName is nameof(BlockViewModel.X)
            or nameof(BlockViewModel.Y))
        {
            OnPropertyChanged(nameof(StartPoint));
            OnPropertyChanged(nameof(EndPoint));
        }
    }
}