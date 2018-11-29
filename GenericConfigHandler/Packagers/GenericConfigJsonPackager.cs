using System;
using System.IO;
using System.Reflection;
using Newtonsoft.Json;

namespace GenericConfigHandler.Packagers
{
    public class GenericConfigJsonPackager : IGenericConfigPackager
    {
        public T DeserializeSettings<T>(string content)
        {
            if (string.IsNullOrWhiteSpace(content))
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
            Type type = typeof(T);

            if (type.IsEnum)
            {
                return (T)Enum.Parse(type, data);
            }

            MethodInfo parseMethod = type.GetMethod("Parse", new Type[] { typeof(string) });
            if (parseMethod != null)
            {
                return (T)parseMethod.Invoke(null, new object[] { data });
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