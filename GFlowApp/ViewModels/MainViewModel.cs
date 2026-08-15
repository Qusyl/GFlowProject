using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Threading.Tasks;
using Application.Dto;
using Avalonia.Media;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Domain.Graph;
using Domain.Nodes;
using Domain.Ports;
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

    public ObservableCollection<Edge> Edges { get;}

    public ObservableCollection<ConnectionViewModel> Connections { get; }

    private BlockViewModel? _selectedStartBlock;

    private BlockViewModel? _selectedEndBlock;

    private PortViewModel? _selectedStartPort;

    private PortViewModel? _selectedEndPort;

    [ObservableProperty]
    private string? selectedItem;

    public MainViewModel(IClientService clientService)
    {
        _serviceClient = clientService;
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
             BlockFactory.Create(block.type, _serviceClient),
             block.category.ToString()));
        }
        return blocksColl;
         
    }

    [RelayCommand]
    public async Task ExecuteWorkflow()
    {
        var nodes = new List<NodeExecution>();
        var edges = new List<Edge>();

        foreach (var block in Blocks)
        {
            var node = new NodeExecution(
                new Node(type: block.NodeType.ToString(),
                properties: block.LoadProperties()),

                new NodeDescriptor(DisplayName: block.NodeType.ToString(), NodeCategory: block.Category, new List<PortsDescriptor>())
            );
            nodes.Add(node);
        }
        var workflowDto = new WorkflowDto(nodes,edges );
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
        Blocks.Add(BlockFactory.Create(blockType, _serviceClient));
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