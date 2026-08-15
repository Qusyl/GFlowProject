using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Ports;
using GFlowApp.ViewModels;

namespace GFlowApp.Services
{
    public class PortClickedArgs
    {
        public BlockViewModel Block { get; set; }

        public PortViewModel Port { get; set; }

   
    }
}