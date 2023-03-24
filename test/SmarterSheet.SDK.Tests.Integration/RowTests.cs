namespace SmarterSheet.SDK.Tests.Integration;

public sealed class RowTests : IAsyncLifetime
{
    private readonly SheetClient _sut;
    private Sheet? _testSheet;
    private readonly Dictionary<string, ulong> _testSheetColumns = new();

    public RowTests(ITestOutputHelper output)
    {
        var httpClient = new HttpClient
        {
            BaseAddress = new Uri(ApiRoutes.Base)
        };
        httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", "82XgDAzgQqF1PuXYy1pkTkccp2QQfRjGI9lrF");

        _sut = new SheetClient(httpClient);

        Log.Logger = new LoggerConfiguration()
            .WriteTo
            .TestOutput(output)
            .CreateLogger();
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

    //TODO: Figure out a better way of handling this, should only happen when Smartsheet is down but still.
    //Throw a meaningful exception
    public async Task InitializeAsync()
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

    public async Task DisposeAsync()
    {
        if (_testSheet == null)
            return;

        await _sut.DeleteSheet(_testSheet.Id);
    }
}