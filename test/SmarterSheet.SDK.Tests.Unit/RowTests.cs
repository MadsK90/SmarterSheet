namespace SmarterSheet.SDK.Tests.Unit;

public sealed class RowTests
{
    #region Fields
    private readonly SheetClient _sut;
    private readonly Row[] _rows;
    #endregion

    public RowTests()
    {
        _rows = CreateTestRows();
        _sut = new SheetClient(SetupMock(_rows));
    }

    #region Get

    [Fact]
    public async Task GetRow_ReturnRow_WhenRowExists()
    {
        //Arrange
        var row = _rows[0];

        //Act
        var retrivedRow = await _sut.GetRow(row.SheetId, row.Id);

        //Assert
        retrivedRow.Should()
            .NotBeNull();
        
        retrivedRow!.Id.Should()
            .Be(row.Id);

        retrivedRow.SheetId.Should()
            .Be(row.SheetId);

        retrivedRow.CreatedAt.Should()
            .Be(row.CreatedAt);

        retrivedRow.ModifiedAt.Should()
            .Be(row.ModifiedAt);

        retrivedRow.Cells.Should().NotBeNullOrEmpty();

        var cells = retrivedRow.Cells.As<Cell[]>();

        cells.Should()
            .ContainEquivalentOf(row.Cells![0]);
    }

    [Fact]
    public async Task GetRow_ReturnNull_WhenRowDoesntExists()
    {
        //Act
        var row = await _sut.GetRow(0, 0);

        //Assert
        row.Should().BeNull();
    }
    
    #endregion

    #region Add

    [Fact]
    public async Task AddRow_ReturnRow_WhenSuccessful()
    {
        //Arange
        var row = _rows[0];

        //Act
        var addedRow = await _sut.AddRow(row.SheetId, row);

        //Assert
        addedRow.Should()
         .NotBeNull();

        addedRow!.Id.Should()
            .Be(row.Id);

        addedRow.SheetId.Should()
            .Be(row.SheetId);

        addedRow.CreatedAt.Should()
            .Be(row.CreatedAt);

        addedRow.ModifiedAt.Should()
            .Be(row.ModifiedAt);

        addedRow.Cells.Should().NotBeNullOrEmpty();

        var cells = addedRow.Cells.As<Cell[]>();

        cells.Should()
            .ContainEquivalentOf(row.Cells![0]);
    }

    [Fact]
    public async Task AddRow_RowBuilder_ReturnRow_WhenSuccessful()
    {
        //Arrange
        var row = _rows[0];

        //Act
        var addedRow = await _sut.AddRow(row.SheetId, RowBuilder.DefaultAddRow(row));

        //Assert
        addedRow.Should()
         .NotBeNull();

        addedRow!.Id.Should()
            .Be(row.Id);

        addedRow.SheetId.Should()
            .Be(row.SheetId);

        addedRow.CreatedAt.Should()
            .Be(row.CreatedAt);

        addedRow.ModifiedAt.Should()
            .Be(row.ModifiedAt);

        addedRow.Cells.Should().NotBeNullOrEmpty();

        var cells = addedRow.Cells.As<Cell[]>();

        cells.Should()
            .ContainEquivalentOf(row.Cells![0]);
    }

    [Fact]
    public async Task AddRows_ReturnRows_WhenSuccessful()
    {
        //Act
        var addedRows = await _sut.AddRows(_rows[0].SheetId, _rows);

        //Assert
        addedRows.Should().NotBeNullOrEmpty();
        addedRows.Count().Should().Be(2);

        var addedRow = addedRows.FirstOrDefault();
        var row = _rows[0];
        
        addedRow.Should()
         .NotBeNull();

        addedRow!.Id.Should()
            .Be(row.Id);

        addedRow.SheetId.Should()
            .Be(row.SheetId);

        addedRow.CreatedAt.Should()
            .Be(row.CreatedAt);

        addedRow.ModifiedAt.Should()
        .Be(row.ModifiedAt);

        addedRow.Cells.Should().NotBeNullOrEmpty();
        

        var cells = addedRow.Cells.As<Cell[]>();
        cells.Should()
            .ContainEquivalentOf(row.Cells![0]);
    }

    [Fact]
    public async Task AddRows_RowBuilder_ReturnRows_WhenSuccessful()
    {
        //Act
        var addedRows = await _sut.AddRows(_rows[0].SheetId, RowBuilder.DefaultAddRows(_rows));

        //Assert
        addedRows.Should().NotBeNullOrEmpty();
        addedRows.Count().Should().Be(2);

        var addedRow = addedRows.FirstOrDefault();
        var row = _rows[0];

        addedRow.Should()
         .NotBeNull();

        addedRow!.Id.Should()
            .Be(row.Id);

        addedRow.SheetId.Should()
            .Be(row.SheetId);

        addedRow.CreatedAt.Should()
            .Be(row.CreatedAt);

        addedRow.ModifiedAt.Should()
        .Be(row.ModifiedAt);

        addedRow.Cells.Should().NotBeNullOrEmpty();


        var cells = addedRow.Cells.As<Cell[]>();
        cells.Should()
            .ContainEquivalentOf(row.Cells![0]);
    }
    
    #endregion

    #region Copy

    [Fact]
    public async Task CopyRow_ReturnsTrue_WhenSuccessful()
    {
        //Assign
        var row = _rows[0];

        //Act
        var copySuccessful = await _sut.CopyRowFromSheet(row.SheetId, row, 1);

        //Assert
        copySuccessful.Should().Be(true);
    }

    [Fact]
    public async Task CopyRows_ReturnsTrue_WhenSuccessful()
    {
        //Act
        var copySuccessful = await _sut.CopyRowsFromSheet(_rows[0].SheetId, _rows, 1);

        //Assert
        copySuccessful.Should().Be(true);
    }

    #endregion

    #region Delete

    [Fact]
    public async Task DeleteRow_ReturnsTrue_WhenSuccessful()
    {
        //Arrange
        var row = _rows[0];

        //Act
        var deleteSuccessful = await _sut.DeleteRow(row.SheetId, row.Id);

        //Assert
        deleteSuccessful.Should().Be(true);
    }

    [Fact]
    public async Task DeleteRows_ReturnsTrue_WhenSuccessful()
    {
        //Act
        var deleteSuccessful = await _sut.DeleteRows(_rows[0].SheetId, _rows.Select(x => x.Id));

        //Assert
        deleteSuccessful.Should().Be(true);
    }

    #endregion Delete

    #region Update

    [Fact]
    public async Task UpdateRow_ReturnTrue_WhenSucessful()
    {
        //Arrange
        var row = _rows[0];

        //Act
        var updateSuccessful = await _sut.UpdateRow(row.SheetId, row);

        //Assert
        updateSuccessful.Should().Be(true);
    }

    [Fact]
    public async Task UpdateRow_RowBuilder_ReturnTrue_WhenSucessful()
    {
        //Arrange
        var row = _rows[0];

        //Act
        var updateSuccessful = await _sut.UpdateRow(row.SheetId, RowBuilder.DefaultUpdateRow(row));

        //Assert
        updateSuccessful.Should().Be(true);
    }


    [Fact]
    public async Task UpdateRows_ReturnTrue_WhenSucessful()
    {
        //Act
        var updateSuccessful = await _sut.UpdateRows(_rows[0].SheetId, _rows);

        //Assert
        updateSuccessful.Should().Be(true);
    }

    [Fact]
    public async Task UpdateRows__RowBuilder_ReturnTrue_WhenSucessful()
    {
        //Act
        var updateSuccessful = await _sut.UpdateRows(_rows[0].SheetId, RowBuilder.DefaultUpdateRows(_rows));

        //Assert
        updateSuccessful.Should().Be(true);
    }


    #endregion

    private static Row[] CreateTestRows()
    {
        return new Row[]
        {
            new Row
            {
                Id = 1000,
                SheetId = 1,
                ModifiedAt = DateTime.MinValue,
                CreatedAt = DateTime.MinValue,
                RowNumber = 1,
                Cells = new Cell[]
                {
                    new Cell
                    {
                        ColumnId = 1,
                        DisplayValue = "Test",
                        Value = "Test"
                    },
                    new Cell
                    {
                        ColumnId = 2,
                        DisplayValue = "Test Again",
                        Value = "Test Again"
                    },
                }
            },
            new Row
            {
                Id = 1001,
                SheetId = 1,
                ModifiedAt = DateTime.MinValue,
                CreatedAt = DateTime.MinValue,
                RowNumber = 2,
                Cells = new Cell[]
                {
                    new Cell
                    {
                        ColumnId = 1,
                        DisplayValue = "Test",
                        Value = "Test"
                    },
                    new Cell
                    {
                        ColumnId = 2,
                        DisplayValue = "Test Again",
                        Value = "Test Again"
                    },
                }
            }
        };
    }

    private static HttpClient SetupMock(Row[] rows)
    {
        var mockHttp = new MockHttpMessageHandler();

        MockGetRowResponse(mockHttp, rows[0]);
        MockAddRowResponse(mockHttp, rows);
        MockCopyRowResponse(mockHttp, rows);
        MockDeleteRowResponse(mockHttp, rows[0]);
        MockDeleteRowsResponse(mockHttp, rows);
        MockUpdateRowResponse(mockHttp, rows[0]);

        var httpCient = mockHttp.ToHttpClient();
        httpCient.BaseAddress = new Uri(ApiRoutes.Base);

        return httpCient;

        #region Local Functions

        static void MockGetRowResponse(MockHttpMessageHandler mockHttp, Row row)
        {
            mockHttp.When(ApiRoutes.Rows.GetRow(row.SheetId, row.Id))
                .Respond(HttpStatusCode.OK, JsonContent.Create(row));
        }

        static void MockAddRowResponse(MockHttpMessageHandler mockHttp, Row[] rows)
        {
            var result = new ResultObject<Row[]>
            {
                Message = "SUCCESS",
                ResultCode = 0,
                Result = rows
            };

            mockHttp.When(ApiRoutes.Rows.AddRow(rows[0].SheetId))
                .Respond(HttpStatusCode.OK, JsonContent.Create(result));
        }

        static void MockCopyRowResponse(MockHttpMessageHandler mockHttp, Row[] rows)
        {
            mockHttp.When(ApiRoutes.Rows.CopyRowFromSheet(rows[0].SheetId))
                .Respond(HttpStatusCode.OK);
        }

        static void MockDeleteRowResponse(MockHttpMessageHandler mockHttp, Row row)
        {
            mockHttp.When(ApiRoutes.Rows.DeleteRow(row.SheetId, row.Id))
                .Respond(HttpStatusCode.OK);
        }

        static void MockDeleteRowsResponse(MockHttpMessageHandler mockHttp, Row[] rows)
        {
            mockHttp.When(ApiRoutes.Rows.DeleteRows(rows[0].SheetId, rows.Select(x => x.Id)))
                 .Respond(HttpStatusCode.OK);
        }

        static void MockUpdateRowResponse(MockHttpMessageHandler mockHttp, Row row)
        {
            mockHttp.When(ApiRoutes.Rows.UpdateRow(row.SheetId))
                .Respond(HttpStatusCode.OK);
        }

        #endregion
    }
}