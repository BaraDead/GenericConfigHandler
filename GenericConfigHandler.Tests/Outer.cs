using System.Xml.Serialization;

namespace GenericConfigHandler.Tests
{
    public class Outer
    {
        [XmlAttribute]
        public int Integer { get; set; }

        [XmlAttribute]
        public string String { get; set; }

        public Inner InnerObject { get; set; }
    }
}