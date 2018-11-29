using System;

namespace GenericConfigHandler.Tests
{
    [Flags]
    public enum EnWithFlags
    {
        None = 0,
        EnumValue1 = 1,
        EnumValue2 = 1 << 1,
        EnumValue3 = 1 << 2
    }
}