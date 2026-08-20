using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using GFlowApp.ViewModels.Properties;

namespace GFlowApp.ViewModels
{
    public partial class PropertyDialogViewModel : ObservableObject
    {
        [ObservableProperty]
        private string? _blockName;

        [ObservableProperty]
        private ObservableCollection<PropertyFieldViewModel> _fields = new();

        public event Action OnApply;

        public event Action OnDecline;

        [RelayCommand]
        public void Apply()
        {
            OnApply?.Invoke();
        }

        [RelayCommand]
        public void Decline()
        {
            OnDecline?.Invoke();
        }
    }
}