using System.Xml;

namespace GenericConfigHandler.ConfigProviders
{
    public interface IGenericConfigProvider
    {
        XmlNode ReadSettingsFromConfig(string section);
    }
}