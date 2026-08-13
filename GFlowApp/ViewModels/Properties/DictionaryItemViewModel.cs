using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GFlowApp.ViewModels.Properties
{
    public class DictionaryItemViewModel
    {
        public string Key { get; set; }

        public PropertyFieldViewModel Value { get; set; }

        public DictionaryItemViewModel(string key, PropertyFieldViewModel value)
        {
            Key = key;
            Value = value;
        }
    }
}