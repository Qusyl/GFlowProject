using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;

namespace Application.Executors.Action.Http
{
    public static class ContentTypeFactory
    {
        public static HttpContent? Create(HttpDefinition definition)
        {
            var data = definition.GetArgument<object?>(HttpRequestArgument.Body.ToString());
            if (data is null
             && definition.FormData is null
              && definition.Files is null)
            {
                return null;
            }
            return (definition.ContentType) switch
            {
                ContentType.Json => CreateJsonContentBody(data),

                ContentType.Xml => CreateXmlContentBody(data),

                ContentType.Binary => CreateBinaryContentBody(definition),

                ContentType.FormUrlEncoded => CreateFormUrlEncodedContentBody(definition),

                ContentType.MultipartForm => CreateMultipartFormContentBody(definition),
                _ => null
            };
        }

        private static HttpContent? CreateJsonContentBody(object? body)
        {
            if (body is null)
            {
                return new StringContent("{}", Encoding.UTF8, "application/json");
            }
            if (body is string dataJson && IsValidJson(dataJson))
            {
                return new StringContent(dataJson, Encoding.UTF8, "application/json");
            }

            var json = JsonSerializer.Serialize(body, new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                WriteIndented = true

            });

            return new StringContent(json, Encoding.UTF8, "application/json");
        }
        private static HttpContent? CreateXmlContentBody( object? body)
        {
            if (body is null)
            {
                return new StringContent("<root/>", Encoding.UTF8, "application/xml");
            }

            if (body is string stringXml)
            {
                return new StringContent(stringXml, Encoding.UTF8, "application/xml");
            }

            var xmlSeriallizer = new XmlSerializer(body.GetType());
            using var stringWriter = new StringWriter();
            using var xmlWriter = XmlWriter.Create(stringWriter, new XmlWriterSettings
            {
                Indent = true,
                OmitXmlDeclaration = false
            });

            xmlSeriallizer.Serialize(stringWriter, body);

            var xml = stringWriter.ToString();

            return new StringContent(xml, Encoding.UTF8, "application/xml");
        }

        private static HttpContent? CreateBinaryContentBody(object? body)
        {
            byte[] bytes;

            if (body is not null && body is byte[] byteArray)
            {
                bytes = byteArray;
            }
            else if (body is string base64String)
            {
                bytes = Convert.FromBase64String(base64String);
            }
            else if (body is Stream stream)
            {
                using var memoryStream = new MemoryStream();
                stream.CopyTo(memoryStream);
                bytes = memoryStream.ToArray();
            }
            else
            {
                var json = JsonSerializer.Serialize(body);
                bytes = Encoding.UTF8.GetBytes(json);
            }

            return new ByteArrayContent(bytes);
        }
        private static HttpContent? CreateFormUrlEncodedContentBody(object? body)
        {
            var formData = new List<KeyValuePair<string, string>>();
            switch (body)
            {
                case Dictionary<string, string> dict:
                    foreach (KeyValuePair<string, string> kvp in dict)
                    {
                        formData.Add(kvp);
                    }
                    break;
                case IEnumerable<KeyValuePair<string, string>> enumerable:
                    formData.AddRange(enumerable);
                    break;
                case string stringData:
                    var pairs = stringData.Split("&", StringSplitOptions.RemoveEmptyEntries);
                    foreach (var pair in pairs)
                    {
                        var parts = pair.Split("=", 2);
                        if (parts.Length == 2)
                        {
                            formData.Add(new KeyValuePair<string, string>(
                                Uri.UnescapeDataString(parts[0]),

                                Uri.UnescapeDataString(parts[1])
                            ));
                        }
                    }
                    break;
            }

            return new FormUrlEncodedContent(formData);
        }
        private static HttpContent? CreateMultipartFormContentBody(HttpDefinition definition)
        {
            var content = new MultipartFormDataContent();
            if (definition.FormData is not null)
            {
                foreach (var kvp in definition.FormData)
                {
                    content.Add(new StringContent(kvp.Value), kvp.Key);
                }
            }
            if (definition.Files is not null)
            {
                foreach (var file in definition.Files)
                {
                    byte[] fileBytes;
                    string fileName;
                    if (file.FileBytes is not null)
                    {
                        fileBytes = file.FileBytes;
                        fileName = file.FileName ?? "file.bin";
                    }
                    else if (!string.IsNullOrEmpty(file.FilePath) && System.IO.File.Exists(file.FilePath))
                    {
                        fileBytes = System.IO.File.ReadAllBytes(file.FilePath);
                        fileName = file.FileName ?? Path.GetFileName(file.FilePath);
                    }
                    else
                    {
                        continue;
                    }

                    var byteContent = new ByteArrayContent(fileBytes);
                    var paramName = file.ParameterName ?? "file";
                    content.Add(byteContent, paramName, fileName);
                }
            }
            return content;
        }
        private static bool IsValidJson(string JsonData)
        {
            try
            {
                JsonDocument.Parse(JsonData);
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}