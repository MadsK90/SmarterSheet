namespace SmarterSheet.SDK.Tests.Unit;

public sealed class SheetTests : SheetClientTests
{
    #region Fields
    private readonly SheetClient _sut;
    private readonly Sheet _sheet;
    private readonly Sheet[] _sheets;
    #endregion

    #region Constants
    private const ulong SHEET_ID = 1;
    private const ulong FILTER_ID = 2;
    private const ulong FOLDER_ID = 3;
    private const ulong TEMPLATE_ID = 4;
    private const ulong WORKSPACE_ID = 5;
    private const string SHEET_FROM_TEMPLATE_NAME = "Sheet from Template";
    private const string COPIED_SHEET_NAME = "Test Copy";
    #endregion

    public SheetTests(ITestOutputHelper output) : base(output)
    {
        _sheet = CreateTestSheet();
        _sheets = CreateTestSheets();
        _sut = CreateSheetClient();
    }

    #region Get

    [Fact]
    public async Task GetSheet_ReturnSheet_WhenSheetExists()
    {
        //Act
        var retrivedSheet = await _sut.GetSheet(SHEET_ID);

        //Assert
        retrivedSheet.Should().NotBeNull();

        retrivedSheet!.Columns
            .Should()
            .NotBeNullOrEmpty();

        var columns = retrivedSheet.Columns.As<Column[]>();

        columns
            .Should()
            .ContainEquivalentOf(_sheet.Columns![0]);
    }

    [Fact]
    public async Task GetSheetWithFilter_ReturnSheet_WhenSheetExists()
    {
        //Arrange

        //Act
        var retrivedSheet = await _sut.GetSheet(SHEET_ID, FILTER_ID);

        //Assert
        retrivedSheet.Should().NotBeNull();

        retrivedSheet!.Columns
            .Should()
            .NotBeNullOrEmpty();

        var columns = retrivedSheet.Columns.As<Column[]>();

        columns
            .Should()
            .ContainEquivalentOf(_sheet.Columns![0]);
    }

    [Fact]
    public async Task GetSheets_ReturnSheets_WhenSheetSuccessful()
    {
        //Act
        var retrivedSheets = await _sut.GetSheets();

        //Assert
        retrivedSheets.Should()
            .NotBeNull()
            .And.HaveCount(2);

        var sheets = retrivedSheets.As<Sheet[]>();

        sheets.Should()
            .ContainEquivalentOf(_sheets[0]);
    }

    [Fact]
    public async Task GetSheetsInFolder_ReturnsSheets_WhenFolderHasSheets()
    {
        //Arrange

        //Act
        var retrivedSheets = await _sut.GetSheetsInFolder(FOLDER_ID);

        //Assert
        retrivedSheets.Should()
            .NotBeNull()
            .And.HaveCount(2);

        var sheets = retrivedSheets.As<Sheet[]>();

        sheets.Should()
            .ContainEquivalentOf(_sheets[0]);
    }

    #endregion

    #region Create

    [Fact]
    public async Task CreateSheet_ReturnSheet_WhenSuccessful()
    {
        //Act
        var createdSheet = await _sut.CreateSheet(_sheet);

        //Assert
        createdSheet.Should()
            .NotBeNull();

        createdSheet.Should()
            .BeEquivalentTo(_sheet);
    }

    [Fact]
    public async Task CreateSheetFromTemplate_ReturnSheet_WhenSuccessful()
    {
        //Act
        var createdSheet = await _sut.CreateSheet(SHEET_FROM_TEMPLATE_NAME, TEMPLATE_ID);

        //Assert
        createdSheet.Should().NotBeNull();
        createdSheet!.Name.Should()
            .Be(SHEET_FROM_TEMPLATE_NAME);
    }

    [Fact]
    public async Task CreateSheetInFolderFromTemplate_ReturnSheet_WhenSuccessful()
    {
        //Arrange

        //Act
        var createdSheet = await _sut.CreateSheet(SHEET_FROM_TEMPLATE_NAME, TEMPLATE_ID, FOLDER_ID);

        //Assert
        createdSheet.Should()
            .NotBeNull();

        createdSheet!.Name.Should()
            .Be(SHEET_FROM_TEMPLATE_NAME);

    }

    [Fact]
    public async Task CreateSheetInFolder_ReturnSheet_WhenSuccessful()
    {
        //Act
        var createdSheet = await _sut.CreateSheetInFolder(_sheet, FOLDER_ID);

        //Assert
        createdSheet.Should()
            .NotBeNull();

        createdSheet.Should()
            .BeEquivalentTo(_sheet);
    }

    [Fact]
    public async Task CreateSheetInWorkspace_ReturnSheet_WhenSuccessful()
    {
        //Act
        var createdSheet = await _sut.CreateSheetInWorkspace(_sheet, WORKSPACE_ID);

        //Assert
        createdSheet.Should()
            .NotBeNull();

        createdSheet.Should()
            .BeEquivalentTo(_sheet);
    }

    #endregion

    #region Copy

    [Fact]
    public async Task CopySheet_ReturnSheet_WhenSucessful()
    {
        //Act
        var copiedSheet = await _sut.CopySheet(_sheet.Id, FOLDER_ID, COPIED_SHEET_NAME);

        //Assert
        copiedSheet.Should()
            .NotBeNull();

        copiedSheet!.Name.Should()
            .Be(COPIED_SHEET_NAME);
    }


    #endregion

    #region Delete

    [Fact]
    public async Task DeleteSheet_ReturnTrue_WhenSUccessful()
    {
        //Act
        var deletedSheet = await _sut.DeleteSheet(_sheet.Id);

        //Assert
        deletedSheet.Should().BeTrue();
    }

    #endregion

    #region Move

    [Fact]
    public async Task MoveSheet_ReturnTrue_WhenSuccessful()
    {
        //Act
        var moveSuccessful = await _sut.MoveSheet(_sheet.Id, FOLDER_ID);

        //Assert
        moveSuccessful.Should()
            .BeTrue();
    }

    #endregion

    #region Rename

    [Fact]
    public async Task RenameSheet_ReturnTrue_WhenSuccessful()
    {
        //Act
        var renameSuccesful = await _sut.RenameSheet(_sheet.Id, "");

        //Assert
        renameSuccesful.Should()
            .BeTrue();
    }

    #endregion

    #region Setup

    protected override void RegisterResponses(MockHttpMessageHandler mockHttp)
    {
        MockGetSheetResponse(mockHttp, _sheet);
        MockGetSheetWithFilterResponse(mockHttp, _sheet);
        MockDeleteSheetResponse(mockHttp, _sheet.Id);
        MockGetSheetsResponse(mockHttp, _sheets);
        MockGetFolderResponse(mockHttp, _sheets);
        MockCreateSheetFromTemplateResponse(mockHttp);
        MockCreateSheetInFolderFromTemplate(mockHttp);
        MockCreateSheetResponse(mockHttp, _sheet);
        MockCreateSheetInFolderResponse(mockHttp, _sheet);
        MockCreateSheetInWorkspaceResponse(mockHttp, _sheet);
        MockCopySheetResponse(mockHttp, _sheet);
        MockMoveSheetResponse(mockHttp, _sheet);
        MockRenameSheetResponse(mockHttp, _sheet);

        #region Local Functions

        static void MockCreateSheetResponse(MockHttpMessageHandler mockHttp, Sheet sheet)
        {
            mockHttp.When(HttpMethod.Post, ApiRoutes.Sheets.CreateSheet())
                .Respond(HttpStatusCode.OK, CreateSuccessfulResult(sheet));
        }

        static void MockDeleteSheetResponse(MockHttpMessageHandler mockHttp, ulong sheetId)
        {            
            mockHttp.When(HttpMethod.Delete, ApiRoutes.Sheets.DeleteSheet(sheetId))
                .Respond(HttpStatusCode.OK, CreateSucessfulEmptyresult());
        }

        static void MockGetSheetResponse(MockHttpMessageHandler mockHttp, Sheet sheet)
        {
            mockHttp.When(HttpMethod.Get, ApiRoutes.Sheets.GetSheet(sheet.Id))
                .Respond(HttpStatusCode.OK, CreateSuccessfulResult(sheet));
        }

        static void MockGetSheetWithFilterResponse(MockHttpMessageHandler mockHttp, Sheet sheet)
        {
            mockHttp.When(HttpMethod.Get, ApiRoutes.Sheets.GetSheet(sheet.Id, FILTER_ID))
                .Respond(HttpStatusCode.OK, CreateSuccessfulResult(sheet));
        }

        static void MockGetSheetsResponse(MockHttpMessageHandler mockHttp, Sheet[] sheets)
        {
            var result = new IndexResult<Sheet>
            {
                PageNumber = 1,
                PageSize = 2,
                TotalPages = 1,
                TotalCount = 2,
                Data = sheets
            };

            mockHttp.When(HttpMethod.Get, ApiRoutes.Sheets.GetSheets())
                .Respond(HttpStatusCode.OK, JsonContent.Create(result));
        }

        static void MockGetFolderResponse(MockHttpMessageHandler mockHttp, Sheet[] sheets)
        {
            var folder = new Folder
            {
                Id = FOLDER_ID,
                Name = "Test Folder",
                Sheets = sheets
            };

            mockHttp.When(HttpMethod.Get, ApiRoutes.Folders.GetFolder(FOLDER_ID))
                .Respond(HttpStatusCode.OK, JsonContent.Create(folder));
        }

        static void MockCreateSheetFromTemplateResponse(MockHttpMessageHandler mockHttp)
        {
            var sheet = new Sheet
            {
                Name = SHEET_FROM_TEMPLATE_NAME
            };

            mockHttp.When(HttpMethod.Post, ApiRoutes.Sheets.CreateSheetFromTemplate())
                .WithPartialContent(SHEET_FROM_TEMPLATE_NAME)
                .Respond(HttpStatusCode.OK, CreateSuccessfulResult(sheet));
        }

        static void MockCreateSheetInFolderFromTemplate(MockHttpMessageHandler mockHttp)
        {
            var sheet = new Sheet
            {
                Name = SHEET_FROM_TEMPLATE_NAME
            };


            mockHttp.When(HttpMethod.Post, ApiRoutes.Sheets.CreateSheetFromTemplateInFolder(FOLDER_ID))
                .WithPartialContent(SHEET_FROM_TEMPLATE_NAME)
                .Respond(HttpStatusCode.OK, CreateSuccessfulResult(sheet));
        }

        static void MockCreateSheetInFolderResponse(MockHttpMessageHandler mockHttp, Sheet sheet)
        {
            mockHttp.When(HttpMethod.Post, ApiRoutes.Sheets.CreateSheetInFolder(FOLDER_ID))
                .WithPartialContent(sheet.Name)
                .Respond(HttpStatusCode.OK, CreateSuccessfulResult(sheet));
        }

        static void MockCreateSheetInWorkspaceResponse(MockHttpMessageHandler mockHttp, Sheet sheet)
        {
            mockHttp.When(HttpMethod.Post, ApiRoutes.Sheets.CreateSheetInWorkspace(WORKSPACE_ID))
                .WithPartialContent(sheet.Name)
                .Respond(HttpStatusCode.OK, CreateSuccessfulResult(sheet));
        }

        static void MockCopySheetResponse(MockHttpMessageHandler mockHttp, Sheet sheet)
        {
            var copiedSheet = new Sheet
            {
                Name = COPIED_SHEET_NAME,
                Id = sheet.Id,
                Columns = sheet.Columns
            };

            mockHttp.When(HttpMethod.Post, ApiRoutes.Sheets.CopySheet(sheet.Id))
                .Respond(HttpStatusCode.OK, CreateSuccessfulResult(copiedSheet));
        }

        static void MockMoveSheetResponse(MockHttpMessageHandler mockHttp, Sheet sheet)
        {
            mockHttp.When(HttpMethod.Post, ApiRoutes.Sheets.MoveSheet(sheet.Id))
                .Respond(HttpStatusCode.OK, CreateSuccessfulResult(sheet));
        }

        static void MockRenameSheetResponse(MockHttpMessageHandler mockHttp, Sheet sheet)
        {
            mockHttp.When(HttpMethod.Put, ApiRoutes.Sheets.RenameSheet(sheet.Id))
                .Respond(HttpStatusCode.OK, CreateSuccessfulResult(sheet));
        }

        #endregion

    }

    private static Sheet CreateTestSheet()
    {
        return new Sheet
        {
            Id = 1,
            Name = "Single TestSheet",
            Columns = new Column[]
            {
                new Column { Title = "Primary Column", Primary = true, Type =  ColumnType.TextNumber},
                new Column {Title = "Favourite", Type = ColumnType.CheckBox}
            }
        };
    }

    private static Sheet[] CreateTestSheets()
    {
        return new Sheet[]
        {
            new Sheet
            {
                Id = 2,
                Name = "TestSheet 1",
                Columns = new Column[]
                {
                    new Column { Title = "Primary Column", Primary = true, Type =  ColumnType.TextNumber},
                    new Column {Title = "Favourite", Type = ColumnType.CheckBox}
                }
            },
            new Sheet
            {
                Id = 3,
                Name = "TestSheet 2",
                Columns = new Column[]
                {
                    new Column { Title = "Primary Column", Primary = true, Type =  ColumnType.TextNumber},
                    new Column {Title = "Num", Type = ColumnType.TextNumber}
                }
            }
        };
    }

    #endregion
}