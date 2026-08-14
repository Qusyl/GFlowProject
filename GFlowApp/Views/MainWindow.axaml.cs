using Avalonia;
using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Interactivity;
using Avalonia.Media;
using GFlowApp.ViewModels;
using System;
using System.Diagnostics;


namespace GFlowApp.Views;

public partial class MainWindow : Window
{
    private bool _isDragging;
    private Point _dragStartPointerPos;
    private Point _dragStartBlockPos;
    private BlockViewModel? _draggedBlock;
    public MainWindow()
    {
        InitializeComponent();
       
    }
 
    

    private void Node_PointerPressed(object? sender, PointerPressedEventArgs args)
    {
           Console.WriteLine($"PointerPressed: sender={sender?.GetType().Name}, DataContext={((sender as Border)?.DataContext)?.GetType().Name}");
        if (sender is not Border border || border.DataContext is not BlockViewModel block)
        {
            System.Console.WriteLine($"Конец захвата");
            return;
        }
        if (!args.GetCurrentPoint(border).Properties.IsLeftButtonPressed)
        {
            return;
        }
        _isDragging = true;
        _draggedBlock = block;
        _dragStartPointerPos = args.GetPosition(this);
        _dragStartBlockPos = new Point(block.X, block.Y);
        args.Pointer.Capture(border);
        args.Handled = true;
    }
    private void Node_PointerMoved(object? sender, PointerEventArgs args)
    {
        System.Console.WriteLine($"Началась движуха, block is null - {_draggedBlock is null}");
        if(_draggedBlock is null || sender is not Border border)
        {
            return;
        }
        if (args.Pointer.Captured != border)
        {
            return;
        }

        var currentPos = args.GetPosition(this);
        var delta = currentPos - _dragStartPointerPos;
        _draggedBlock.X = _dragStartBlockPos.X + delta.X;
        _draggedBlock.Y = _dragStartBlockPos.Y + delta.Y;

    }
    private void Node_PointerReleased(object? sender, PointerReleasedEventArgs args)
    {
        System.Console.WriteLine("Свобода указателю");
        _isDragging = false;
        _draggedBlock = null;
        if (sender is Border border)
        {
            args.Pointer.Capture(null);
        }
    }

    private void Node_PointerCaptureLost(object? sender, PointerCaptureLostEventArgs args)
    {
        _draggedBlock = null;
    }
    
}