# SmarterSheet Draft

The goal of this project is making it easier to work with Smartheet from an API.

So instead of having to manually setup sheets like the following:

```c#
var sheet = await sheetClient.CreateSheet(new Sheet
{
    Id = 1,
    Name = "TestSheet",
    Columns = new Column[]
        {
            new Column { Title = "Primary Column", Primary = true, Type =  ColumnType.TextNumber},
            new Column {Title = "Favourite", Type = ColumnType.CheckBox}
        }
});

var primaryColumn = sheet.Columns.FirstOrDefault(x => x.Title == "Primary Column") ?? throw new Exception("");
var favouriteColumn = sheet.Columns.FirstOrDefault(x => x.Title == "Favourite") ?? throw new Exception("");

var row = await sheetClient.AddRow(sheet.Id, new Row
{
  Cells = new Cell[]
  {
    new Cell
    {
        ColumnId = primaryColumn.Id,
        Value = "Test",
    },
    new Cell
    {
        ColumnId = favouriteColumn.Id,
        Value = "true",
        Strict = false
    }
  }
});
```

You can instead setup a SheetModelBase much like a Json model
```c#
  [ColumnName("Primary Column")]
  public string? Name { get; set; }

  [ColumnName("Favourite")]
  public bool Favourite { get; set; }
```

And then just use that to add/get ect data to a sheet:
```c#
var model = new TestModelRow
{
  Name = "Test",
  Favourite = true
};

var row = await sheetClient.AddRow(sheet.Id, model);
```

