namespace SmarterSheet.Extensions.Templating.Attributes;

[AttributeUsage(AttributeTargets.Class)]
public class ValueParserAttribute : Attribute
{
    public Type ValueType { get; }

    public ValueParserAttribute(Type parserType)
    {
        ValueType = parserType;
    }
}