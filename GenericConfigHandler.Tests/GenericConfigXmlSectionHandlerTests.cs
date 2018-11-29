using NUnit.Framework;

namespace GenericConfigHandler.Tests
{
    public class GenericConfigXmlSectionHandlerTests
    {
        private IGenericConfigSectionHandler _handler;

        [SetUp]
        public void Setup()
        {
            _handler = new GenericConfigSectionHandlerFactory().GetXmlHandler();
        }

        [Test]
        public void ClassTest()
        {
            Outer value = _handler.GetSettings<Outer>("ClassXml");

            Assert.AreEqual(5, value.Integer);
            Assert.AreEqual("SomeString", value.String);
            Assert.AreEqual(6, value.InnerObject.Integer1);
            Assert.AreEqual("OtherString", value.InnerObject.String1);
            Assert.AreEqual(3, value.InnerObject.Array.Length);
            Assert.AreEqual(2, value.InnerObject.Array[0]);
            Assert.AreEqual(4, value.InnerObject.Array[1]);
            Assert.AreEqual(6, value.InnerObject.Array[2]);
        }
    }
}