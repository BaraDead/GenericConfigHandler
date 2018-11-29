using System;
using System.IO;
using System.Reflection;
using System.Xml;
using Newtonsoft.Json;

namespace GenericConfigHandler.Packagers
{
    public class GenericConfigJsonPackager : IGenericConfigPackager
    {
        public T DeserializeSettings<T>(XmlNode node)
        {
            if (node == null)
            {
                throw new GenericConfigException("Cannot read settings");
            }

            string data = node.InnerText.Trim();
            string section = node.Name;

            try
            {
                T settings = CreateInstance<T>(data);
                return settings;
            }
            catch (Exception exc)
            {
                throw new GenericConfigException(string.Format("Cannot deserialize read settings from section '{0}'", section), exc);
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