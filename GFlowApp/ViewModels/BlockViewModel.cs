using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Avalonia.Media;
using CommunityToolkit.Mvvm.ComponentModel;

namespace GFlowApp.ViewModels
{
    public partial class BlockViewModel : ObservableObject
    {
        [ObservableProperty]
        public partial string Name { get; set; }

        [ObservableProperty]
        public partial double X { get; set; }

        [ObservableProperty]
        public partial double Y { get; set; }

        [ObservableProperty]
        public partial IBrush Color { get; set; }

        public BlockViewModel(string name,IBrush color, double x, double y)
        {
            Name = name;
            X = x;
            Y = y;
            Color = color;
        }

    }
}