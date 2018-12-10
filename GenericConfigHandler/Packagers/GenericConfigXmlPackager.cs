using System;
using System.IO;
using System.Xml;
using System.Xml.Serialization;

namespace GenericConfigHandler.Packagers
{
    public class GenericConfigXmlPackager : IGenericConfigPackager
    {
        public T DeserializeSettings<T>(string content)
        {
            if (string.IsNullOrEmpty(content))
            {
                throw new GenericConfigException("Cannot read settings");
            }

            try
            {
                XmlDocument xmlDocument = new XmlDocument();
                xmlDocument.LoadXml(content);
                string section = xmlDocument.DocumentElement.Name;

                T settings = CreateInstance<T>(content, section);
                return settings;
            }
            catch (Exception exc)
            {
                throw new GenericConfigException("Cannot deserialize settings", exc);
            }
        }

        private T CreateInstance<T>(string data, string section)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(T), new XmlRootAttribute(section));

            using (StringReader stringReader = new StringReader(data))
            {
                T instance = (T)serializer.Deserialize(stringReader);
                return instance;
            }
        }
    }
}