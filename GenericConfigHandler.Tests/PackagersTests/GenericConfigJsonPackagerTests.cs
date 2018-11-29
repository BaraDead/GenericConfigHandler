using System;
using GenericConfigHandler.Packagers;
using NUnit.Framework;

namespace GenericConfigHandler.Tests.PackagersTests
{
    [TestFixture]
    public class GenericConfigJsonPackagerTests
    {
        private IGenericConfigPackager _packager;

        [SetUp]
        public void Setup()
        {
            _packager = new GenericConfigJsonPackager();
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
        public void BooleanTest()
        {
            bool value = _packager.DeserializeSettings<bool>("True");

            Assert.AreEqual(true, value);
        }

        [Test]
        public void IntegerTest()
        {
            int value = _packager.DeserializeSettings<int>("1");

            Assert.AreEqual(1, value);
        }

        [Test]
        public void ByteTest()
        {
            byte value = _packager.DeserializeSettings<byte>("255");

            Assert.AreEqual(255, value);
        }

        [Test]
        public void ByteOverflowTest()
        {
            Assert.That(delegate { _packager.DeserializeSettings<byte>("256"); },
                Throws.TypeOf<GenericConfigException>(),
                "GenericConfigException must be thrown");
        }

        [Test]
        public void DecimalTest()
        {
            decimal value = _packager.DeserializeSettings<decimal>("45.8");

            Assert.AreEqual(45.8m, value);
        }

        [Test]
        public void DateTimeTest()
        {
            DateTime value = _packager.DeserializeSettings<DateTime>("2018-05-15 15:26:37.123");

            Assert.AreEqual(new DateTime(2018, 5, 15, 15, 26, 37, 123), value);
        }

        [Test]
        public void TimeSpanTest()
        {
            TimeSpan value = _packager.DeserializeSettings<TimeSpan>("13:35:37");

            Assert.AreEqual(new TimeSpan(13, 35, 37), value);
        }

        [Test]
        public void EnumTest()
        {
            En value = _packager.DeserializeSettings<En>("EnumValue1");

            Assert.AreEqual(En.EnumValue1, value);
        }

        [Test]
        public void EnumWithFlagsTest()
        {
            EnWithFlags value = _packager.DeserializeSettings<EnWithFlags>("EnumValue2,EnumValue3");

            Assert.AreEqual(EnWithFlags.EnumValue2 | EnWithFlags.EnumValue3, value);
        }

        [Test]
        public void ClassTest()
        {
            Outer value = _packager.DeserializeSettings<Outer>(Constants.ClassSettings);

            Assert.AreEqual(5, value.Integer);
            Assert.AreEqual("SomeString", value.String);
            Assert.AreEqual(6, value.InnerObject.Integer1);
            Assert.AreEqual("OtherString", value.InnerObject.String1);
            Assert.AreEqual(3, value.InnerObject.Array.Length);
            Assert.AreEqual(2, value.InnerObject.Array[0]);
            Assert.AreEqual(4, value.InnerObject.Array[1]);
            Assert.AreEqual(6, value.InnerObject.Array[2]);
        }

        [Test]
        public void ClassWithNullInnerObjectTest()
        {
            Outer value = _packager.DeserializeSettings<Outer>(Constants.Class1Settings);

            Assert.AreEqual(5, value.Integer);
            Assert.AreEqual("SomeString", value.String);
            Assert.IsNull(value.InnerObject);
        }
    }
}