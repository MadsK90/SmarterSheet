namespace SmarterSheet.Extensions.Templating;

public static class SheetClientExtensions
{
    private static readonly ColumnCache _cache = new(50);
    private static readonly TemplateResolver _templateResolver = new();

    public static async Task<Row?> AddRow<T>(this SheetClient client, ulong sheetId, T model) where T: SheetModelBase, new()
    {
        var columns = await client.GetSheetColumnsCached(sheetId);
        if (!columns.Any())
            return null;

        var row = _templateResolver.CreateRow(model, columns);
        if (row == null)
            return null;

        return await client.AddRow(sheetId, row);
    }

    public static async Task<IEnumerable<Row>> AddRows<T>(this SheetClient client, ulong sheetId, IEnumerable<T> models) where T: SheetModelBase, new()
    {
        var columns = await client.GetSheetColumnsCached(sheetId);
        if (!columns.Any())
            return Array.Empty<Row>();

        var rows = _templateResolver.CreateRows(models, columns);
        if (!rows.Any())
            return Array.Empty<Row>();

        return await client.AddRows(sheetId, rows);
    }

    public static async Task<T?> GetRow<T>(this SheetClient client, ulong sheetId, ulong rowId, 
        IEnumerable<RowInclusion>? inclusions = null) where T : SheetModelBase, new()
    {
        var inclusionList = new List<RowInclusion>();
        if(inclusions != null)
            inclusionList.AddRange(inclusions);

        if(!inclusionList.Contains(RowInclusion.Columns))
            inclusionList.Add(RowInclusion.Columns);

        var row = await client.GetRow(sheetId, rowId, inclusionList);
        if (row == null)
            return null;

        if (row.Columns == null)
            return null;

        return _templateResolver.ResolveModel<T>(row);
    }

    public static async Task<IEnumerable<T>> GetSheet<T>(this SheetClient client, ulong sheetId, 
        IEnumerable<SheetInclusion>? inclusions = null) where T : SheetModelBase, new()
    {
        var sheet = await client.GetSheet(sheetId, inclusions);
        if (sheet == null)
            return Array.Empty<T>();

        return _templateResolver.ResolveModels<T>(sheet);
    }

    private static async Task<Column[]> GetSheetColumnsCached(this SheetClient client, ulong sheetId)
    {
        if (_cache.TryGetColumns(sheetId, out var columns))
            return columns;

        columns = (await client.GetSheetColumns(sheetId)).ToArray();
        if(!columns.Any())
            return Array.Empty<Column>();

        _cache.Add(sheetId, columns);

        return columns;
    }
}