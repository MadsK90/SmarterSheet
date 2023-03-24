namespace SmarterSheet.SDK.Tests.Unit;

public sealed class SheetTests
{
    private readonly SheetClient _sut;
    private readonly Sheet _sheet;

    public SheetTests()
    {
        _sheet = CreateTestSheet();
        _sut = new SheetClient(SetupMock(_sheet));
    }

    #region Create

    [Fact]
    public async Task CreateSheet_ReturnSheet_WhenSuccessful()
    {
        //Act
        var createdSheet = await _sut.CreateSheet(_sheet);

        //Assert
        createdSheet.Should().NotBeNull();
        createdSheet.Should().BeEquivalentTo(_sheet);
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

    private static HttpClient SetupMock(Sheet sheet)
    {
        var mockHttp = new MockHttpMessageHandler();

        MockCreateSheetResponse(mockHttp, sheet);
        MockDeleteSheetResponse(mockHttp, sheet.Id);

        var httpCient = mockHttp.ToHttpClient();
        httpCient.BaseAddress = new Uri(ApiRoutes.Base);

        return httpCient;

        #region Local Functions

        static void MockCreateSheetResponse(MockHttpMessageHandler mockHttp, Sheet sheet)
        {
            var result = new ResultObject<Sheet>
            {
                Message = "SUCCESS",
                ResultCode = 0,
                Result = sheet
            };

            mockHttp.When(ApiRoutes.Sheets.CreateSheet())
                .Respond(HttpStatusCode.OK, JsonContent.Create(result));
        }

        static void MockDeleteSheetResponse(MockHttpMessageHandler mockHttp, ulong sheetId)
        {
            var result = new EmptyResultObject
            {
                Message = "SUCCESS",
                ResultCode = 0,
            };

            mockHttp.When(ApiRoutes.Sheets.DeleteSheet(sheetId))
                .Respond(HttpStatusCode.OK, JsonContent.Create(result));
        }

        #endregion
    }

    private static Sheet CreateTestSheet()
    {
        return new Sheet
        {
            Id = 1,
            Name = "TestSheet",
            Columns = new Column[]
                {
                    new Column { Title = "Primary Column", Primary = true, Type =  ColumnType.TextNumber},
                    new Column {Title = "Favourite", Type = ColumnType.CheckBox}
                }
        };
    }
}