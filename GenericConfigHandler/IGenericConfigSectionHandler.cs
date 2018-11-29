namespace GenericConfigHandler
{
    public interface IGenericConfigSectionHandler
    {
        T GetSettings<T>(string section);
    }
}