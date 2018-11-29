using System;
using System.Configuration;
using System.Xml;

namespace GenericConfigHandler.ConfigProviders
{
    public class GenericConfigDefaultProvider : IGenericConfigProvider
    {
        private readonly ConfigContentType _contentType;

        public GenericConfigDefaultProvider(ConfigContentType contentType)
        {
            _contentType = contentType;
        }

        public string ReadSettingsFromConfig(string section)
        {
            try
            {
                XmlNode node = (XmlNode)ConfigurationManager.GetSection(section);

                return _contentType == ConfigContentType.InnerText
                    ? node.InnerText.Trim()
                    : node.OuterXml;

            }
            catch (Exception exc)
            {
                throw new GenericConfigException(string.Format("Cannot read settings from section '{0}'", section), exc);
            }
        }
    }
}