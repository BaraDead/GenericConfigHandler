using System.Xml;

namespace GenericConfigHandler.ConfigProviders
{
    public interface IGenericConfigProvider
    {
        string ReadSettingsFromConfig(string section);
    }
}