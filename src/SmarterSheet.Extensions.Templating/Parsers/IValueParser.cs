namespace SmarterSheet.Extensions.Templating.Parsers;

public interface IValueParser
{
    bool Parse<T>(in string input, Property property, ref T model) where T : SheetModelBase;

    bool Parse<T>(in T model, Property property, in ulong columnId, out Cell? cell) where T : SheetModelBase;
}