namespace SmarterSheet.SDK.Tests.Integration;

public sealed class RowTests : SheetClientTests
{
    private readonly SheetClient _sut;
    private Sheet? _testSheet;
    private readonly Dictionary<string, ulong> _testSheetColumns = new();

    public RowTests(ITestOutputHelper output) : base(output, "")
    {
        _sut = CreateSheetClient();
    }

    #region Get

    [Fact]
    public async Task GetRow_ReturnRow_WhenRowExists()
    {
        _testSheet.Should().NotBeNull();

        //Arrange
        var addedRow = await _sut.AddRow(_testSheet!.Id, new Row
        {
            Cells = new Cell[]
            {
                new Cell
                {
                    ColumnId = _testSheetColumns["Primary Column"],
                    Value = "Test",
                },
                new Cell
                {
                    ColumnId = _testSheetColumns["Favourite"],
                    Value = "true",
                    Strict = false
                }
            }
        });

        addedRow.Should().NotBeNull();

        //Act
        var retrivedRow = await _sut.GetRow(_testSheet!.Id, addedRow!.Id);

        //Assert
        retrivedRow.Should().NotBeNull();
        retrivedRow.Should().BeEquivalentTo(addedRow);
    }

    [Fact]
    public async Task GetRow_ReturnNull_WhenRowDoesntExists()
    {
        //Act
        var retrivedRow = await _sut.GetRow(_testSheet!.Id, 1);

        //Assert
        retrivedRow.Should().BeNull();
    }

    #endregion

    public override async Task DisposeAsync()
    {
        if (_testSheet == null)
            return;

        if (!await _sut.DeleteSheet(_testSheet.Id))
            throw new Exception("Failed to clean up");
    }

    public override async Task InitializeAsync()
    {
        _testSheet = await _sut.CreateSheet(new Sheet
        {
            Id = 1,
            Name = "TestSheet",
            Columns = new Column[]
                {
                    new Column { Title = "Primary Column", Primary = true, Type =  ColumnType.TextNumber},
                    new Column {Title = "Favourite", Type = ColumnType.CheckBox}
                }
        });

        if (_testSheet == null || _testSheet.Columns == null || !_testSheet.Columns.Any())
            throw new Exception("");

        var primaryColumn = _testSheet.Columns.FirstOrDefault(x => x.Title == "Primary Column") ?? throw new Exception("");
        var favouriteColumn = _testSheet.Columns.FirstOrDefault(x => x.Title == "Favourite") ?? throw new Exception("");

        _testSheetColumns.Add("Primary Column", primaryColumn.Id);
        _testSheetColumns.Add("Favourite", favouriteColumn.Id);
    }
}