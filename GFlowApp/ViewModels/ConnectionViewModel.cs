using Avalonia;
using CommunityToolkit.Mvvm.ComponentModel;

namespace GFlowApp.ViewModels;

public partial class ConnectionViewModel : ObservableObject
{
    [ObservableProperty]
    private BlockViewModel startBlock = null!;

    [ObservableProperty]
    private BlockViewModel endBlock = null!;

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
        BlockViewModel endBlock)
    {
        StartBlock = startBlock;
        EndBlock = endBlock;

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