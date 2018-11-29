using System;
using System.IO;
using System.Xml;
using System.Xml.Serialization;

namespace GenericConfigHandler.Packagers
{
    public class GenericConfigXmlPackager : IGenericConfigPackager
    {
        public T DeserializeSettings<T>(XmlNode node)
        {
            if (node == null)
            {
                throw new GenericConfigException("Cannot read settings");
            }

            string data = node.OuterXml;
            string section = node.Name;

            try
            {
                T settings = CreateInstance<T>(data, section);
                return settings;
            }
            catch (Exception exc)
            {
                throw new GenericConfigException(string.Format("Cannot deserialize read settings from section '{0}'", section), exc);
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