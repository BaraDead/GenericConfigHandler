using GenericConfigHandler.ConfigProviders;
using GenericConfigHandler.Packagers;

namespace GenericConfigHandler
{
    public class GenericConfigSectionHandlerFactory
    {
        /// <summary>
        /// Use this method to read settings from config file and deserialize it from json format
        /// </summary>
        /// <returns></returns>
        public IGenericConfigSectionHandler GetJsonHandler()
        {
            return new GenericConfigSectionHandler(new GenericConfigDefaultProvider(ConfigContentType.InnerText), new GenericConfigJsonPackager());
        }

        /// <summary>
        /// Use this method to read settings from config file and deserialize it from xml format
        /// </summary>
        /// <returns></returns>
        public IGenericConfigSectionHandler GetXmlHandler()
        {
            return new GenericConfigSectionHandler(new GenericConfigDefaultProvider(ConfigContentType.OuterXml), new GenericConfigXmlPackager());
        }

        /// <summary>
        /// Use this method to read settings from any source and deserialize it from custom format
        /// </summary>
        /// <param name="configProvider">Custom config reader</param>
        /// <param name="packager">Custom packager</param>
        /// <returns></returns>
        public IGenericConfigSectionHandler GetCustomHandler(IGenericConfigProvider configProvider, IGenericConfigPackager packager)
        {
            return new GenericConfigSectionHandler(configProvider, packager);
        }
    }
}