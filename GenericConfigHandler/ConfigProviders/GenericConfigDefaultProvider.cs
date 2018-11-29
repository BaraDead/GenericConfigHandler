using System;
using System.Configuration;
using System.Xml;

namespace GenericConfigHandler.ConfigProviders
{
    public class GenericConfigDefaultProvider : IGenericConfigProvider
    {
        public XmlNode ReadSettingsFromConfig(string section)
        {
            try
            {
                XmlNode node = (XmlNode)ConfigurationManager.GetSection(section);
                return node;
            }
            catch (Exception exc)
            {
                throw new GenericConfigException(string.Format("Cannot read settings from section '{0}'", section), exc);
            }
        }
    }
}