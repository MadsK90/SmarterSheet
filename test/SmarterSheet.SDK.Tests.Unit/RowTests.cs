namespace SmarterSheet.SDK.Tests.Unit;

public sealed class RowTests : SheetClientTests
{
    #region Fields
    private readonly SheetClient _sut;
    private readonly Row[] _rows;
    private readonly Row _row;
    #endregion

    #region Constants
    
    private const ulong TO_SHEET_ID = 3000;
    
    #endregion

    public RowTests(ITestOutputHelper output) : base(output)
    {
        _rows = CreateTestRows();
        _row = CreateTestRow();
        _sut = CreateSheetClient();
    }

    #region Get

    [Fact]
    public async Task GetRow_ReturnRow_WhenRowExists()
    {
        //Act
        var retrivedRow = await _sut.GetRow(_row.SheetId, _row.Id);

        //Assert
        retrivedRow.Should()
            .NotBeNull();
        
        retrivedRow!.Id.Should()
            .Be(_row.Id);

        retrivedRow.SheetId.Should()
            .Be(_row.SheetId);

        retrivedRow.CreatedAt.Should()
            .Be(_row.CreatedAt);

        retrivedRow.ModifiedAt.Should()
            .Be(_row.ModifiedAt);

        retrivedRow.Cells.Should().NotBeNullOrEmpty();

        var cells = retrivedRow.Cells.As<Cell[]>();

        cells.Should()
            .ContainEquivalentOf(_row.Cells![0]);
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
        //Act
        var addedRow = await _sut.AddRow(_row.SheetId, _row);

        //Assert
        addedRow.Should()
         .NotBeNull();

        addedRow!.Id.Should()
            .Be(_row.Id);

        addedRow.SheetId.Should()
            .Be(_row.SheetId);

        addedRow.CreatedAt.Should()
            .Be(_row.CreatedAt);

        addedRow.ModifiedAt.Should()
            .Be(_row.ModifiedAt);

        addedRow.Cells.Should().NotBeNullOrEmpty();

        var cells = addedRow.Cells.As<Cell[]>();

        cells.Should()
            .ContainEquivalentOf(_row.Cells![0]);
    }

    [Fact]
    public async Task AddRow_RowBuilder_ReturnRow_WhenSuccessful()
    {
        //Act
        var addedRow = await _sut.AddRow(_row.SheetId, RowBuilder.DefaultAddRow(_row));

        //Assert
        addedRow.Should()
         .NotBeNull();

        addedRow!.Id.Should()
            .Be(_row.Id);

        addedRow.SheetId.Should()
            .Be(_row.SheetId);

        addedRow.CreatedAt.Should()
            .Be(_row.CreatedAt);

        addedRow.ModifiedAt.Should()
            .Be(_row.ModifiedAt);

        addedRow.Cells.Should().NotBeNullOrEmpty();

        var cells = addedRow.Cells.As<Cell[]>();

        cells.Should()
            .ContainEquivalentOf(_row.Cells![0]);
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
        //Act
        var copySuccessful = await _sut.CopyRowFromSheet(_row.SheetId, _row, TO_SHEET_ID);

        //Assert
        copySuccessful.Should().Be(true);
    }

    [Fact]
    public async Task CopyRows_ReturnsTrue_WhenSuccessful()
    {
        //Act
        var copySuccessful = await _sut.CopyRowsFromSheet(_rows[0].SheetId, _rows, TO_SHEET_ID);

        //Assert
        copySuccessful.Should().Be(true);
    }

    #endregion

    #region Delete

    [Fact]
    public async Task DeleteRow_ReturnsTrue_WhenSuccessful()
    {
        //Act
        var deleteSuccessful = await _sut.DeleteRow(_row.SheetId, _row.Id);

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

    #region Setup

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

    private static Row CreateTestRow()
    {
        return new Row
        {
            Id = 1000,
            SheetId = 2,
            ModifiedAt = DateTime.MinValue,
            CreatedAt = DateTime.MinValue,
            RowNumber = 1,
            Cells = new Cell[]
               {
                    new Cell
                    {
                        ColumnId = 1,
                        DisplayValue = "Sigle Row Test",
                        Value = "Sigle Row Test"
                    },
                    new Cell
                    {
                        ColumnId = 2,
                        DisplayValue = "Sigle Row Test Again",
                        Value = "Sigle Row Test Again"
                    },
               }
        };
    }

    protected override void RegisterResponses(MockHttpMessageHandler mockHttp)
    {
        MockGetRowResponse(mockHttp, _row);
        MockAddRowResponse(mockHttp, _row);
        MockAddRowsResponse(mockHttp, _rows);
        MockCopyRowsResponse(mockHttp, _rows);
        MockCopyRowResponse(mockHttp, _row);
        MockDeleteRowResponse(mockHttp, _row);
        MockDeleteRowsResponse(mockHttp, _rows);
        MockUpdateRowResponse(mockHttp, _row);

        #region Local Functions

        static void MockGetRowResponse(MockHttpMessageHandler mockHttp, Row row)
        {
            mockHttp.When(HttpMethod.Get, ApiRoutes.Rows.GetRow(row.SheetId, row.Id))
                .Respond(HttpStatusCode.OK, JsonContent.Create(row));
        }

        static void MockAddRowResponse(MockHttpMessageHandler mockHttp, Row row)
        {
            mockHttp.When(HttpMethod.Post, ApiRoutes.Rows.AddRow(row.SheetId))
                .Respond(HttpStatusCode.OK, CreateSuccessfulResult(row));
        }

        static void MockAddRowsResponse(MockHttpMessageHandler mockHttp, Row[] rows)
        {
            mockHttp.When(HttpMethod.Post, ApiRoutes.Rows.AddRows(rows[0].SheetId))
                .Respond(HttpStatusCode.OK, CreateSuccessfulResult(rows));
        }

        static void MockCopyRowsResponse(MockHttpMessageHandler mockHttp, Row[] rows)
        {
            mockHttp.When(HttpMethod.Post, ApiRoutes.Rows.CopyRowFromSheet(rows[0].SheetId))
                .Respond(HttpStatusCode.OK);
        }

        static void MockCopyRowResponse(MockHttpMessageHandler mockHttp, Row row)
        {
            mockHttp.When(HttpMethod.Post, ApiRoutes.Rows.CopyRowFromSheet(row.SheetId))
                .Respond(HttpStatusCode.OK);
        }

        static void MockDeleteRowResponse(MockHttpMessageHandler mockHttp, Row row)
        {
            mockHttp.When(HttpMethod.Delete, ApiRoutes.Rows.DeleteRow(row.SheetId, row.Id))
                .Respond(HttpStatusCode.OK);
        }

        static void MockDeleteRowsResponse(MockHttpMessageHandler mockHttp, Row[] rows)
        {
            mockHttp.When(HttpMethod.Delete, ApiRoutes.Rows.DeleteRows(rows[0].SheetId, rows.Select(x => x.Id)))
                 .Respond(HttpStatusCode.OK);
        }

        static void MockUpdateRowResponse(MockHttpMessageHandler mockHttp, Row row)
        {
            mockHttp.When(HttpMethod.Put, ApiRoutes.Rows.UpdateRow(row.SheetId))
                .Respond(HttpStatusCode.OK);
        }

        #endregion

    }

    #endregion
}