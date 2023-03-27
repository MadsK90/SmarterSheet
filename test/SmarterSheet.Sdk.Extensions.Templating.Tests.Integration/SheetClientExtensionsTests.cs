namespace SmarterSheet.Sdk.Extensions.Templating.Tests.Integration;

public sealed class SheetClientExtensionsTests : IAsyncLifetime
{
    #region Fields

    private readonly SheetClient _sut;
    private readonly TestRowModel _testRowModel;
    private readonly TestRowModel[] _testRowModels;
    private Sheet _testSheet;

    #endregion

    #region Constants

    #endregion


    //TODO: Add apikey as Github secrets for automated testing
    public SheetClientExtensionsTests(ITestOutputHelper output)
    {
        var httpClient = new HttpClient
        {
            BaseAddress = new Uri(ApiRoutes.BASE)
        };
        httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", "APIKEY");

        SetupLogging(output);
        _testRowModel = CreateTestRow();
        _testRowModels = CreateTestRows();        

        _testSheet = new Sheet();
        _sut = new SheetClient(httpClient);
    }

    #region Add

    [Fact]
    public async Task AddRowAsModel_ReturnRow_WhenSuccessful()
    {
        //Act
        var addedRow = await _sut.AddRow(_testSheet.Id, _testRowModel);

        //Assert
        addedRow.Should()
            .NotBeNull();
    }

    [Fact]
    public async Task AddRowsAsModel_ReturnRows_WhenSuccessful()
    {
        //Act
        var addedRows = await _sut.AddRows(_testSheet.Id, _testRowModels);

        //Assert
        addedRows.Should()
            .NotBeNullOrEmpty();
    }

    #endregion

    #region Get

    [Fact]
    public async Task GetRowAsModel_ReturnModel_WhenExists()
    {
        //Arrange
        var addedRow = await _sut.AddRow(_testSheet.Id, _testRowModel);

        addedRow.Should()
            .NotBeNull();

        //Act
        var retrivedRow = await _sut.GetRow<TestRowModel>(_testSheet.Id, addedRow!.Id);

        //Assert
        retrivedRow.Should()
            .NotBeNull();

        retrivedRow.Should()
            .BeEquivalentTo(_testRowModel);
    }

    [Fact]
    public async Task GetSheetAsModel_ReturnModels_WhenExists()
    {
        //Arrange
        var addedRows = await _sut.AddRows(_testSheet.Id, _testRowModels);

        addedRows.Should()
            .NotBeNullOrEmpty();

        //Act
        var retrivedRows = await _sut.GetSheet<TestRowModel>(_testSheet.Id);

        //Assert
        retrivedRows.Should()
            .NotBeNull();

        retrivedRows.Should()
            .BeEquivalentTo(_testRowModels);
    }

    #endregion

    #region Setup & Teardown

    //TODO: Figure out a better way of handling this, should only happen when Smartsheet is down but still.
    //Throw a meaningful exception
    public async Task InitializeAsync()
    {
        var testSheet = await _sut.CreateSheet(new Sheet
        {
            Id = 1,
            Name = "TestSheet",
            Columns = new Column[]
            {
                new Column { Title = "Primary Column", Primary = true, Type =  ColumnType.TextNumber},
                new Column {Title = "Favourite", Type = ColumnType.CheckBox}
            }
        });

        if (testSheet == null || testSheet.Columns == null || !testSheet.Columns.Any())
            throw new Exception("See log from SmarterSheet");

        _testSheet = testSheet;
    }

    public async Task DisposeAsync()
    {
        if (_testSheet == null)
            return;

        if (!await _sut.DeleteSheet(_testSheet.Id))
            throw new Exception("Failed to clean up");
    }

    private static TestRowModel CreateTestRow()
    {
        return new TestRowModel
        {
            Name = "Single TestRow",
            Favourite = true,
            RowNumber = 1,
        };
    }

    private static TestRowModel[] CreateTestRows()
    {
        return new TestRowModel[]
        {
            new TestRowModel
            {
                Name = "TestRow 1",
                Favourite = false,
                RowNumber = 1,
            },
            new TestRowModel
            {
                Name = "TestRow 2",
                Favourite = true,
                RowNumber = 2
            }
        };
    }

    private static void SetupLogging(ITestOutputHelper output)
    {
        Log.Logger = new LoggerConfiguration()
            .WriteTo
            .TestOutput(output)
            .CreateLogger();
    }

    #endregion
}