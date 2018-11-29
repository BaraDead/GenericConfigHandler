using GenericConfigHandler.ConfigProviders;
using NUnit.Framework;

namespace GenericConfigHandler.Tests.ProvidersTests
{
    public class GenericConfigDefaultProviderTests
    {
        [Test]
        public void XmlDataTest()
        {
            GenericConfigDefaultProvider configProvider = new GenericConfigDefaultProvider(ConfigContentType.OuterXml);

            const string expected = @"<ClassXml Integer=""5"" String=""SomeString""><InnerObject Integer1=""6"" String1=""OtherString""><list><item>2</item><item>4</item><item>6</item></list></InnerObject></ClassXml>";
            string settings = configProvider.ReadSettingsFromConfig("ClassXml");

            Assert.AreEqual(expected, settings);
        }

        [Test]
        public void CDataTest()
        {
            GenericConfigDefaultProvider configProvider = new GenericConfigDefaultProvider(ConfigContentType.InnerText);

            string settings = configProvider.ReadSettingsFromConfig("Class");

            Assert.AreEqual(Constants.ClassSettings, settings);
        }

        [Test]
        public void PlainTest()
        {
            GenericConfigDefaultProvider configProvider = new GenericConfigDefaultProvider(ConfigContentType.InnerText);

            string settings = configProvider.ReadSettingsFromConfig("Class1");

            Assert.AreEqual(Constants.Class1Settings, settings);
        }

        [Test]
        public void IncorrectSectionTest()
        {
            Assert.That(() =>
                {
                    GenericConfigDefaultProvider configProvider =
                        new GenericConfigDefaultProvider(ConfigContentType.OuterXml);
                    configProvider.ReadSettingsFromConfig("Incorrect");
                },
                Throws.TypeOf<GenericConfigException>(),
                "GenericConfigException must be thrown");
        }
    }
}