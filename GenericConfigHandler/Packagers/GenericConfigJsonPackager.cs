using System;
using System.ComponentModel;
using System.IO;
using Newtonsoft.Json;

namespace GenericConfigHandler.Packagers
{
    public class GenericConfigJsonPackager : IGenericConfigPackager
    {
        public T DeserializeSettings<T>(string content)
        {
            if (string.IsNullOrEmpty(content))
            {
                throw new GenericConfigException("Cannot read settings");
            }

            try
            {
                T settings = CreateInstance<T>(content);
                return settings;
            }
            catch (Exception exc)
            {
                throw new GenericConfigException("Cannot deserialize settings", exc);
            }
        }

        private T CreateInstance<T>(string data)
        {
            TypeConverter converter = TypeDescriptor.GetConverter(typeof(T));
            if (converter.CanConvertFrom(typeof(string)))
            {
                return (T)converter.ConvertFromString(data);
            }

            return GetClassValue<T>(data);
        }

        private static T GetClassValue<T>(string data)
        {
            JsonSerializer serializer = JsonSerializer.Create();
            using (TextReader textReader = new StringReader(data))
            {
                using (JsonReader reader = new JsonTextReader(textReader))
                {
                    T settings = serializer.Deserialize<T>(reader);
                    return settings;
                }
            }
        }
    }
}