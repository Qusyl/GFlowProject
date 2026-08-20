using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Avalonia.Controls;
using Avalonia.Media;
using Domain.Nodes;
using Domain.Ports;
using GFlowApp.Services.Window;
using GFlowApp.ViewModels;

namespace GFlowApp.Services
{
    public static class BlockFactory
    {
        private static int _id = 0;
        public static BlockViewModel Create(IWindowService parentWindow,BlockTypes blockType, IClientService clientService)
        {
            var random = new Random();
            var randX = random.Next(100, 700);
            var randY = random.Next(100, 500);

            (IImmutableSolidColorBrush color, NodeCategory category) = SelectColorAndCategory(blockType);
            List<PortsDescriptor> ports = BuildPorts(blockType);
            return new BlockViewModel(_id++,parentWindow,blockType, category, color, randX, randY, clientService, ports);
        }
        private static (IImmutableSolidColorBrush color, NodeCategory category) SelectColorAndCategory(BlockTypes blockType)
        {
            return blockType switch
            {
                BlockTypes b when b.ToString().Contains("Action") => (Brushes.Red, NodeCategory.Action),
                BlockTypes b when b.ToString().Contains("Logic") => (Brushes.Yellow, NodeCategory.Logic),
                BlockTypes b when b.ToString().Contains("Trigger") => (Brushes.Aquamarine, NodeCategory.Trigger),
                _ => throw new NotSupportedException("Not supported block type")
            };
        }
        private static List<PortsDescriptor> BuildPorts(BlockTypes blockType)
        {
            return blockType switch
            {
                BlockTypes.SqlAction => new List<PortsDescriptor>
                {
                    new PortsDescriptor("Input", PortsDirection.Input),
                new PortsDescriptor("QueryResult", PortsDirection.Output),
                },
                BlockTypes.HttpAction => new List<PortsDescriptor>
                {
                    new PortsDescriptor("Input", PortsDirection.Input),
                new PortsDescriptor("HttpResponse", PortsDirection.Output)
                },
                BlockTypes.CompareLogic => new List<PortsDescriptor>
                {
                    new PortsDescriptor("Input", PortsDirection.Input),
                new PortsDescriptor("True", PortsDirection.Output),
                 new PortsDescriptor("False", PortsDirection.Output),
                },
                BlockTypes.ManualTrigger => new List<PortsDescriptor>
                {
                     new PortsDescriptor("Invoke", PortsDirection.Output),
                },
            };
        }
       
    }
}