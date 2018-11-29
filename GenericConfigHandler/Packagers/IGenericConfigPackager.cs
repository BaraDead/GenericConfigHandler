using System.Xml;

namespace GenericConfigHandler.Packagers
{
    public interface IGenericConfigPackager
    {
        T DeserializeSettings<T>(string node);
    }
}