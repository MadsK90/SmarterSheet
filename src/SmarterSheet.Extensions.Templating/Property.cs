namespace SmarterSheet.Extensions.Templating;

public readonly struct Property
{
    public readonly PropertyInfo PropertyInfo { get; init; }
    public readonly bool IsFormula { get; init; }
}