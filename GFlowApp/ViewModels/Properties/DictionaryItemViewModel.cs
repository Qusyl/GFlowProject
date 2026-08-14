using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace GFlowApp.ViewModels.Properties
{
    public partial class DictionaryItemViewModel : ObservableObject
    {
        private readonly DictionaryFieldPropertyViewModel _parent;
        private string _key;
        private PropertyFieldViewModel _value;

        public string Key
        {
            get => _key;
            set => SetProperty(ref _key, value);
        }
        public PropertyFieldViewModel Value
        {
            get => _value;
            set => SetProperty(ref _value, value);
        }

        public DictionaryItemViewModel(string key, PropertyFieldViewModel value, DictionaryFieldPropertyViewModel parent)
        {
            _key = key;
            _value = value;
            _parent = parent;
        }
    [RelayCommand]

        public void Remove()
        {
            _parent.RemoveItemCommand.Execute(this);
        }
    }
}