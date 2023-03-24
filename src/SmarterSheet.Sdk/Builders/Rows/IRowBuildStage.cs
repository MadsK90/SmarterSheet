namespace SmarterSheet.Sdk.Builders.Rows;

public interface IRowBuildStage
{
    public AddRow BuildAddRow(Row row);

    public IEnumerable<AddRow> BuildAddRows(IEnumerable<Row> rows);

    public UpdateRow BuildUpdateRow(Row row);

    public IEnumerable<UpdateRow> BuildUpdateRows(IEnumerable<Row> rows);
}