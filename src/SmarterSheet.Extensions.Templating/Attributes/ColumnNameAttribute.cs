namespace SmarterSheet.Extensions.Templating.Attributes;

[AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
public sealed class ColumnNameAttribute : Attribute
{
    public string Name { get; }

    public bool Optional { get; set; }

    public bool Formula { get; set; }

    public ColumnNameAttribute(string name, bool optional = false, bool formula = false)
    {
        Name = name.ToLowerInvariant();
        Optional = optional;
        Formula = formula;
    }
}