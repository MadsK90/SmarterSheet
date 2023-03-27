namespace SmarterSheet.Sdk.Extensions.Templating.Tests.Integration.RowModels;

internal sealed class TestRowModel : SheetModelBase
{
    [ColumnName("Primary Column")]
    public string? Name { get; set; }

    [ColumnName("Favourite")]
    public bool Favourite { get; set; }
}