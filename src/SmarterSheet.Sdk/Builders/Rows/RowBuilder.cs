namespace SmarterSheet.Sdk.Builders.Rows;

public sealed class RowBuilder : IRowHierarchySelectionStage, IRowPositionStage, IRowBuildStage
{
    private ulong _parentId;
    private ulong _siblingId;
    private bool _toTop;
    private bool _toBottom;

    private RowBuilder() {}

    public static IRowHierarchySelectionStage Create()
    {
        return new RowBuilder();
    }

    #region Defaults

    public static AddRow DefaultAddRow(Row row)
    {
        var addRow = new AddRow();

        if (row.Cells != null)
            addRow.Cells = row.Cells;

        addRow.ToBottom = true;

        return addRow;
    }

    public static IEnumerable<AddRow> DefaultAddRows(IEnumerable<Row> rows)
    {
        var addRows = new List<AddRow>(rows.Count());

        foreach (var row in rows)
        {
            addRows.Add(DefaultAddRow(row));
        }

        return addRows;
    }

    public static UpdateRow DefaultUpdateRow(Row row)
    {
        var updateRow = new UpdateRow();

        if(row.Cells != null)
            updateRow.Cells = row.Cells;

        return updateRow;
    }
    
    public static IEnumerable<UpdateRow> DefaultUpdateRows(IEnumerable<Row> rows)
    {
        var updateRows = new List<UpdateRow>();

        foreach(var row in rows)
        {
            updateRows.Add(DefaultUpdateRow(row));
        }

        return updateRows;
    }

    #endregion

    #region AddRows

    public AddRow BuildAddRow(Row row)
    {
        var addRow = new AddRow();

        if (row.Cells != null)
            addRow.Cells = row.Cells;

        if (_siblingId != 0)
        {
            addRow.SiblingId = _siblingId;

            if (_toTop)
                addRow.Above = true;

            return addRow;
        }

        if (_parentId != 0)
            addRow.ParentId = _parentId;

        if (_toBottom)
            addRow.ToBottom = _toBottom;

        if (_toTop)
            addRow.ToTop = _toTop;

        return addRow;
    }

    public IEnumerable<AddRow> BuildAddRows(IEnumerable<Row> rows)
    {
        var addRows = new List<AddRow>(rows.Count());

        foreach (var row in rows)
        {
            addRows.Add(BuildAddRow(row));
        }

        return addRows;
    }

    #endregion

    #region Update Rows

    public UpdateRow BuildUpdateRow(Row row)
    {
        return new UpdateRow
        {
            Id = row.Id,
            ParentId = row.ParentId,
            Cells = row.Cells,
            ToBottom = default,
            ToTop = default
        };
    }

    public IEnumerable<UpdateRow> BuildUpdateRows(IEnumerable<Row> rows)
    {
        var updateRows = new List<UpdateRow>(rows.Count());

        foreach(var row in rows)
        {
            updateRows.Add(new UpdateRow
            {
                Id = row.Id,
                ParentId = row.ParentId,
                Cells = row.Cells,
                ToBottom = default,
                ToTop = default
            });
        }

        return updateRows;
    }

    #endregion

    public IRowPositionStage WithParent(ulong parentId)
    {
        _parentId = parentId;
        return this;
    }

    public IRowPositionStage WithSibling(ulong siblingId)
    {
        _siblingId = siblingId;
        return this;
    }

    public IRowBuildStage ToBottom()
    {
        _toBottom = true;
        return this;
    }

    public IRowBuildStage ToTop()
    {
        _toTop = true;
        return this;
    }
}