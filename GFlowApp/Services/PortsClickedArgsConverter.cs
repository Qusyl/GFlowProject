using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using Avalonia.Data.Converters;
using GFlowApp.ViewModels;

namespace GFlowApp.Services
{
    public class PortsClickedArgsConverter : IMultiValueConverter
    {
        public object? Convert(IList<object?> values, Type targetType, object? parameter, CultureInfo culture)
        {
            if(values.Count == 2 && values[0] is BlockViewModel block && values[1] is PortViewModel port)
            {
                return new PortClickedArgs {Block = block, Port =port };
            }
            return null;
        }
    }
}