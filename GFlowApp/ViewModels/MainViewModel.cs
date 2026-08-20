using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using Application.Dto;
using Avalonia.Controls;
using Avalonia.Media;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Domain.Graph;
using Domain.Nodes;
using Domain.Ports;
using GFlowApp.Services;
using GFlowApp.Services.Window;

namespace GFlowApp.ViewModels;

public partial class MainViewModel : ViewModelBase
{
    private readonly IClientService _serviceClient;

    private readonly IWindowService _windowService;

       private BlockViewModel? _selectedBlock;

    [ObservableProperty]
    private bool _isConnectionMode;
    public ObservableCollection<BlockItem> ActionItems { get; }

    public ObservableCollection<BlockItem> LogicItems { get; }

    public ObservableCollection<BlockItem> TriggerItems { get; }

    public ObservableCollection<BlockViewModel> Blocks { get; }

    public ObservableCollection<Edge> Edges { get; }
    
    public ObservableCollection<NodeExecution> PreparedNodes { get; }

    public ObservableCollection<ConnectionViewModel> Connections { get; }

    private BlockViewModel? _selectedStartBlock;

    private BlockViewModel? _selectedEndBlock;

    private PortViewModel? _selectedStartPort;

    private PortViewModel? _selectedEndPort;

    [ObservableProperty]
    private string? selectedItem;

    public MainViewModel(IClientService clientService, IWindowService windowService)
    {
        _serviceClient = clientService;
        _windowService = windowService;
        ActionItems = InitializeBlockComponent(new List<(BlockTypes type, NodeCategory category)>
        {
            (BlockTypes.SqlAction, NodeCategory.Action),
             (BlockTypes.HttpAction, NodeCategory.Action)
        });
        LogicItems = InitializeBlockComponent(new List<(BlockTypes type, NodeCategory category)>
        {
            (BlockTypes.CompareLogic, NodeCategory.Logic),
        });

        TriggerItems = InitializeBlockComponent(new List<(BlockTypes type, NodeCategory category)>
        {
            (BlockTypes.ManualTrigger, NodeCategory.Trigger),
        });
        Blocks = new ObservableCollection<BlockViewModel>();
        Edges = new();
        PreparedNodes = new();

        Connections =
            new ObservableCollection<ConnectionViewModel>();

        SelectedItem = TriggerItems[0].Model.NodeType.ToString();

    }
    private ObservableCollection<BlockItem> InitializeBlockComponent(List<(BlockTypes type, NodeCategory category)> blocks)
    {
        var blocksColl = new ObservableCollection<BlockItem>();
        foreach (var block in blocks)
        {
            blocksColl.Add(
             new BlockItem(
             BlockFactory.Create(_windowService,block.type, _serviceClient),
             block.category.ToString()));
        }
        return blocksColl;
         
    }
private NodeExecution BuildNodeExecution(BlockViewModel block)
{
    var ports = new List<PortsDescriptor>();
    foreach (var input in block.InputPorts)
        ports.Add(new PortsDescriptor(input.PortName, input.Direction));
    foreach (var output in block.OutputPorts)
        ports.Add(new PortsDescriptor(output.PortName, output.Direction));

    var node = new Node(block.NodeId, block.NodeType.ToString(), block.LoadProperties()); // ← актуальный вызов, прямо перед отправкой
    var descriptor = new NodeDescriptor(block.NodeType.ToString(), block.Category, ports);

    return new NodeExecution(node, descriptor);
}
    [RelayCommand]
    public async Task ExecuteWorkflow()
    {
     var preparedNodes = Blocks.Select(BuildNodeExecution).ToList();
     var workflowDto = new WorkflowDto(preparedNodes, Edges);
        

        var result = await _serviceClient.ExecuteAsync(workflowDto);
        System.Console.WriteLine("Результат получен!");

        if(result is null)
        {
            System.Console.WriteLine("Result is empty");
        }
        else if(result!.Exceptions is not null)
        {
            int index = 1;
            foreach(var error in result!.Exceptions!)
            {
                    System.Console.WriteLine($"[{index}] {error.Message}");
            }
        }
    }

    public void AddEdge(PortsDescriptor From, PortsDescriptor To, int NodeIdFrom, int NodeIdTo)
    {
        Edges.Add(new Edge(NodeIdFrom, NodeIdTo, From.PortName, To.PortName));
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
    private void AddBlock(BlockTypes blockType)
    {
        Console.WriteLine(
            $"Добавлен блок {blockType}");
        var createdNode = BlockFactory.Create(_windowService, blockType, _serviceClient);
        Blocks.Add(createdNode);
    }

    [RelayCommand]
    public void PortClicked(PortClickedArgs args)
    {

        if (!IsConnectionMode)
        {
            ToggleConnectionMode();
            System.Console.WriteLine($"Connection mode is {IsConnectionMode}");
           
        }
        var block = args.Block;
        var port = args.Port;
       
        System.Console.WriteLine($"Нажат порт {port.PortName} блока {block.NodeType} {block.NodeId}");
        if (_selectedStartBlock is null)
        {
            if (port.Direction != PortsDirection.Output)
            {
                return;

            }
            
                    _selectedStartBlock = block;
                    _selectedStartPort = port;
                    block.IsSelected = true;
                    port.IsConnected = true;
                    return;
           
        }
        if (_selectedStartBlock == block)
        {
            _selectedStartBlock = null;
            _selectedStartPort = null;
            block.IsSelected = false;
            port.IsConnected = false;
            return;
        }
        if (port.Direction != PortsDirection.Input)
        {
            return;
        }
        _selectedEndBlock = block;
        _selectedEndPort = port;
        var connection = new ConnectionViewModel(
            _selectedStartBlock,

            _selectedEndBlock,

            _selectedStartPort!,

            _selectedEndPort
        );
        Connections.Add(connection);
        AddEdge(
        new PortsDescriptor(_selectedStartPort!.PortName, _selectedStartPort.Direction),
        new PortsDescriptor(_selectedEndPort.PortName, _selectedEndPort.Direction),
         _selectedStartBlock.NodeId, _selectedEndBlock.NodeId);

        _selectedStartBlock.IsSelected = false;
        _selectedStartPort.IsConnected = false;

        _selectedStartBlock = null;

        _selectedStartPort = null;

        IsConnectionMode = false;
        }
        
    }