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
            try
            {
                if (value is JsonElement element)
                {
                    if (element.ValueKind == JsonValueKind.String)
                    {
                        return element.GetString() ?? string.Empty;
                    }

                    return element.GetRawText();
                }
                return value?.ToString() ?? string.Empty;
            }catch(Exception ex)
            {
                System.Console.WriteLine($"Ошибка - {ex.Message}");
                throw;
            }
            
        }

        public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            if (value is string str)
            {
                try
                {
                    using var doc = JsonDocument.Parse(str);
                    return doc.RootElement.Clone();
                }
                catch
                {
                  
                     try
            {
                var escaped = JsonSerializer.Serialize(str);
                using var doc = JsonDocument.Parse(escaped);
                return doc.RootElement.Clone();
            }
            catch
            {
                return null;
            }
                }
            }
         
            return null;
        }
    }
}