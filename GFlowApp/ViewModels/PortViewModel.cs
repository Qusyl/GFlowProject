using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using Domain.Ports;

namespace GFlowApp.ViewModels
{
    public partial class PortViewModel : ObservableObject
    {
     
        [ObservableProperty]
        private string _portName;

        

        [ObservableProperty]
        private PortsDirection _direction;

        [ObservableProperty]
        private bool _isConnected = false;

        public PortViewModel(string portName, PortsDirection direction)
        {
            PortName = portName;
            Direction = direction;
           
        }
    }
}