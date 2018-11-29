using System.Xml.Serialization;

namespace GenericConfigHandler.Tests
{
    public class Inner
    {
        [XmlAttribute]
        public int Integer1 { get; set; }

        [XmlAttribute]
        public string String1 { get; set; }

        [XmlArray("list")]
        [XmlArrayItem("item")]
        public int[] Array { get; set; }
    }
}