using GenericConfigHandler.ConfigProviders;
using GenericConfigHandler.Packagers;
using NUnit.Framework;
using Rhino.Mocks;

namespace GenericConfigHandler.Tests.HandlerTests
{
    [TestFixture]
    public class CustomHandlerTests
    {
        private MockRepository _mockRepository;
        private IGenericConfigProvider _configProvider;
        private IGenericConfigPackager _packager;
        private IGenericConfigSectionHandler _handler;

        [SetUp]
        public void Setup()
        {
            _mockRepository = new MockRepository();

            _configProvider = MockRepository.GenerateMock<IGenericConfigProvider>();
            _packager = MockRepository.GenerateMock<IGenericConfigPackager>();

            _handler = new GenericConfigSectionHandlerFactory().GetCustomHandler(_configProvider, _packager);
        }

        [Test]
        public void SuccessTest()
        {
            using (_mockRepository.Ordered())
            {
                _configProvider
                    .Expect(reader => reader.ReadSettingsFromConfig("SettingsNode"))
                    .Return("SettingsValue");

                _packager
                    .Expect(configSerializer => configSerializer.DeserializeSettings<byte>("SettingsValue"))
                    .Return(253);
            }

            byte value;
            using (_mockRepository.Playback())
            {
                value = _handler.GetSettings<byte>("SettingsNode");
            }

            _configProvider.VerifyAllExpectations();
            _packager.VerifyAllExpectations();

            Assert.AreEqual(253, value, "Deserialized value incorrect");
        }

        [Test]
        public void ProviderFailedTest()
        {
            using (_mockRepository.Ordered())
            {
                _configProvider
                    .Expect(reader => reader.ReadSettingsFromConfig("SettingsNode"))
                    .Throw(new GenericConfigException("Provider failed"));
            }

            using (_mockRepository.Playback())
            {
                Assert.That(() => _handler.GetSettings<byte>("SettingsNode"),
                    Throws.TypeOf<GenericConfigException>(),
                    "GenericConfigException must be thrown");
            }

            _configProvider.VerifyAllExpectations();
            _packager.VerifyAllExpectations();
        }

        [Test]
        public void PackagerFailedTest()
        {
            using (_mockRepository.Ordered())
            {
                _configProvider
                    .Expect(reader => reader.ReadSettingsFromConfig("SettingsNode"))
                    .Return("SettingsValue");

                _packager
                    .Expect(configSerializer => configSerializer.DeserializeSettings<byte>("SettingsValue"))
                    .Throw(new GenericConfigException("Packager failed"));
            }

            using (_mockRepository.Playback())
            {
                Assert.That(() => _handler.GetSettings<byte>("SettingsNode"),
                    Throws.TypeOf<GenericConfigException>(),
                    "GenericConfigException must be thrown");
            }

            _configProvider.VerifyAllExpectations();
            _packager.VerifyAllExpectations();
        }

    }
}