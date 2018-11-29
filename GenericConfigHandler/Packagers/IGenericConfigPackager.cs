using System.Xml;

namespace GenericConfigHandler.Packagers
{
    public interface IGenericConfigPackager
    {
        T DeserializeSettings<T>(XmlNode node);
    }
}