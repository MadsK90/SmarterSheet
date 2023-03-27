namespace SmarterSheet.Extensions.Templating.Utils;

internal class ColumnCache
{
    private readonly Column[][] _cache;
    private readonly ulong[] _sheetIds;
    private readonly int _max;
    private int _index;

    public ColumnCache(int size)
    {
        _cache = new Column[size][];
        _sheetIds = new ulong[size];
    }

    public void Add(ulong sheetId, Column[] columns)
    {
        if (_index == _max)
            _index = 0;

        _cache[_index] = columns;
        _sheetIds[_index] = sheetId;

        ++_index;
    }

    public bool TryGetColumns(ulong sheetId, out Column[] columns)
    {
        for (var i = 0; i < _max; ++i)
        {
            if (_sheetIds[i] == sheetId)
            {
                columns = _cache[i];
                return true;
            }
        }

        columns = Array.Empty<Column>();
        return false;
    }
}