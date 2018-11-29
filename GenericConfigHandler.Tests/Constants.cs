namespace GenericConfigHandler.Tests
{
    public class Constants
    {
        public const string ClassSettings = @"{
      ""Integer"" : 5,
      ""String"" : ""SomeString"",
      ""InnerObject"" :
      {
        ""Integer1"" : 6,
        ""String1"" : ""OtherString"",
        ""Array"" : [2, 4, 6]
      }
    }";

        public const string Class1Settings = @"{
      ""Integer"" : 5,
      ""String"" : ""SomeString""
    }";

        public const string ClassXmlSettings = @"
        <ClassXml Integer=""5"" String=""SomeString"">
            <InnerObject Integer1 = ""6"" String1=""OtherString"">
                <list>
                    <item>2</item>
                    <item>4</item>
                    <item>6</item>
                </list>
            </InnerObject>
        </ClassXml>";
    }
}