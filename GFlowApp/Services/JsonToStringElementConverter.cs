using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using Avalonia.Data.Converters;

namespace GFlowApp.Services
{
    public class JsonToStringElementConverter : IValueConverter
    {
        public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            return value is JsonElement e ? e.GetRawText() : "null";
        }

        public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            try
            {
                return JsonDocument.Parse(value as string ?? "null").RootElement;
            }catch(Exception ex)
            {
                return JsonSerializer.SerializeToElement(value as string);
            }
        }
    }
}