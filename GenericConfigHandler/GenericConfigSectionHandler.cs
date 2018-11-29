using System.Configuration;
using System.Xml;
using GenericConfigHandler.ConfigProviders;
using GenericConfigHandler.Packagers;

namespace GenericConfigHandler
{
    internal class GenericConfigSectionHandler : IConfigurationSectionHandler, IGenericConfigSectionHandler
    {
        private readonly IGenericConfigProvider _configProvider;
        private readonly IGenericConfigPackager _packager;

        private GenericConfigSectionHandler()
        {}

        public GenericConfigSectionHandler(IGenericConfigProvider configProvider, IGenericConfigPackager packager)
        {
            _configProvider = configProvider;
            _packager = packager;
        }

        public T GetSettings<T>(string section)
        {
            string content = _configProvider.ReadSettingsFromConfig(section);

            return _packager.DeserializeSettings<T>(content);
        }

        public object Create(object parent, object configContext, XmlNode section)
        {
            return section;
        }
    }
}