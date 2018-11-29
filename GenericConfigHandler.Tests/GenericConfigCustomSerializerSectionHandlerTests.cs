using System.Xml;
using GenericConfigHandler.ConfigProviders;
using GenericConfigHandler.Packagers;
using NUnit.Framework;
using Rhino.Mocks;
using Rhino.Mocks.Constraints;

namespace GenericConfigHandler.Tests
{
    [TestFixture]
    public class GenericConfigCustomSerializerSectionHandlerTests
    {
        private MockRepository _mockRepository;

        [SetUp]
        public void Setup()
        {
            _mockRepository = new MockRepository();
        }

        [Test]
        public void SuccessTest()
        {
            IGenericConfigPackager packager = MockRepository.GenerateMock<IGenericConfigPackager>();
            IGenericConfigProvider configProvider = MockRepository.GenerateMock<IGenericConfigProvider>();

            IGenericConfigSectionHandler handler = new GenericConfigSectionHandlerFactory().GetCustomHandler(configProvider, packager);

            XmlDocument document = new XmlDocument();
            document.LoadXml("<node>chunk</node>");

            XmlNode node = null;

            using (_mockRepository.Ordered())
            {
                configProvider
                    .Expect(reader => reader.ReadSettingsFromConfig("node"))
                    .Return(document);

                packager
                    .Expect(configSerializer => configSerializer.DeserializeSettings<byte>(null))
                    .IgnoreArguments()
                    .WhenCalled(invocation =>
                    {
                        node = (XmlNode)invocation.Arguments[0];
                    })
                    .Return(253);
            }

            byte value;
            using (_mockRepository.Playback())
            {
                value = handler.GetSettings<byte>("node");
            }

            configProvider.VerifyAllExpectations();
            packager.VerifyAllExpectations();

            Assert.AreEqual("<node>chunk</node>", node.OuterXml);
            Assert.AreEqual(253, value, "Deserialized value incorrect");
        }
    }
}