using System.Reflection;

namespace SmarterSheet.Extensions.Templating.Parsers;

[ValueParser(typeof(bool))]
public sealed class BoolParser : IValueParser
{
    public bool Parse<T>(in string input, Property property, ref T model) where T : SheetModelBase
    {
        if (!bool.TryParse(input, out var boolValue))
            return false;

        property.PropertyInfo.SetValue(model, boolValue);

        return true;
    }

    public bool Parse<T>(in T model, Property property, in ulong columnId, out Cell? cell) where T : SheetModelBase
    {
        var obj = property.PropertyInfo.GetValue(model);
        if (obj == null)
        {
            cell = null;
            return false;
        }

        var str = obj.ToString();
        if(string.IsNullOrEmpty(str))
        {
            cell = null;
            return false;
        }

        cell = new Cell { ColumnId = columnId, Value = str.ToLowerInvariant(), Strict = false };
        return true;
    }
}