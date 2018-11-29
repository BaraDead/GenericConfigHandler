using GenericConfigHandler.Packagers;
using NUnit.Framework;

namespace GenericConfigHandler.Tests.PackagersTests
{
    [TestFixture]
    public class GenericConfigXmlPackagerTests
    {
        private IGenericConfigPackager _packager;

        [SetUp]
        public void Setup()
        {
            _packager = new GenericConfigXmlPackager();
        }

        [Test]
        public void ParseErrorTest()
        {
            Assert.That(() => _packager.DeserializeSettings<byte>("NotExists"),
                Throws.TypeOf<GenericConfigException>(),
                "GenericConfigException must be thrown");
        }

        [Test]
        public void SettingsNotExistTest()
        {
            Assert.That(() => _packager.DeserializeSettings<byte>(""),
                Throws.TypeOf<GenericConfigException>(),
                "GenericConfigException must be thrown");
        }

        [Test]
        public void ClassTest()
        {
            Outer value = _packager.DeserializeSettings<Outer>(Constants.ClassXmlSettings);

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