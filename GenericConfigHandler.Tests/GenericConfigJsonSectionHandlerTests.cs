using System;
using NUnit.Framework;

namespace GenericConfigHandler.Tests
{
    [TestFixture]
    public class GenericConfigJsonSectionHandlerTests
    {
        private IGenericConfigSectionHandler _handler;

        [SetUp]
        public void Setup()
        {
            _handler = new GenericConfigSectionHandlerFactory().GetJsonHandler();
        }

        [Test]
        public void BooleanTest()
        {
            bool value = _handler.GetSettings<bool>("Boolean");

            Assert.AreEqual(true, value);
        }

        [Test]
        public void IntegerTest()
        {
            int value = _handler.GetSettings<int>("Integer");

            Assert.AreEqual(1, value);
        }

        [Test]
        public void ByteTest()
        {
            byte value = _handler.GetSettings<byte>("Byte");

            Assert.AreEqual(255, value);
        }

        [Test]
        public void SettingsNotExistTest()
        {
            Assert.That(delegate { _handler.GetSettings<byte>("NotExists"); },
                Throws.TypeOf<GenericConfigException>(),
                "GenericConfigException must be thrown");
        }

        [Test]
        public void ByteOverflowTest()
        {
            Assert.That(delegate { _handler.GetSettings<byte>("ByteOverflow"); },
                Throws.TypeOf<GenericConfigException>(),
                "GenericConfigException must be thrown");
        }

        [Test]
        public void DecimalTest()
        {
            decimal value = _handler.GetSettings<decimal>("Decimal");

            Assert.AreEqual(45.8m, value);
        }

        [Test]
        public void DateTimeTest()
        {
            DateTime value = _handler.GetSettings<DateTime>("DateTime");

            Assert.AreEqual(new DateTime(2018, 5, 15, 15, 26, 37, 123), value);
        }

        [Test]
        public void TimeSpanTest()
        {
            TimeSpan value = _handler.GetSettings<TimeSpan>("TimeSpan");

            Assert.AreEqual(new TimeSpan(13, 35, 37), value);
        }

        [Test]
        public void EnumTest()
        {
            En value = _handler.GetSettings<En>("Enum");

            Assert.AreEqual(En.EnumValue1, value);
        }

        [Test]
        public void EnumWithFlagsTest()
        {
            EnWithFlags value = _handler.GetSettings<EnWithFlags>("EnumWithFlags");

            Assert.AreEqual(EnWithFlags.EnumValue2 | EnWithFlags.EnumValue3, value);
        }

        [Test]
        public void ClassTest()
        {
            Outer value = _handler.GetSettings<Outer>("Class");

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
            Outer value = _handler.GetSettings<Outer>("Class1");

            Assert.AreEqual(5, value.Integer);
            Assert.AreEqual("SomeString", value.String);
            Assert.IsNull(value.InnerObject);
        }

        public enum En
        {
            None,
            EnumValue1,
            EnumValue2,
            EnumValue3
        }

        [Flags]
        public enum EnWithFlags
        {
            None = 0,
            EnumValue1 = 1,
            EnumValue2 = 1 << 1,
            EnumValue3 = 1 << 2
        }

        public class Outer
        {
            public int Integer { get; set; }
            public string String { get; set; }
            public Inner InnerObject { get; set; }
        }

        public class Inner
        {
            public int Integer1 { get; set; }
            public string String1 { get; set; }
            public int[] Array { get; set; }
        }
    }
}