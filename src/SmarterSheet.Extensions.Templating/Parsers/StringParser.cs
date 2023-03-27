using System.Reflection;

namespace SmarterSheet.Extensions.Templating.Parsers;

[ValueParser(typeof(string))]
public sealed class StringParser : IValueParser
{
    public bool Parse<T>(in string input, Property property, ref T model) where T : SheetModelBase
    {
        property.PropertyInfo.SetValue(model, input);

        return true;
    }

    public bool Parse<T>(in T model, Property property, in ulong columnId, out Cell? cell) where T : SheetModelBase
    {
        var obj = property.PropertyInfo.GetValue(model);
        if (obj == null || obj.ToString() == "")
        {
            cell = null;
            return true;
        }

        if (property.IsFormula)
        {
            cell = new Cell { ColumnId = columnId, Formula = obj.ToString() };
            return true;
        }

        cell = new Cell { ColumnId = columnId, Value = obj.ToString() };
        return true;
    }
}