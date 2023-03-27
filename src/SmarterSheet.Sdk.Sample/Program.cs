SetupLog();

var client = new SheetClient(CreateHttpClient());

var response = await client.CreateSheet(new Sheet
{
    Name = "TestSheet",
    Columns = new Column[]
                {
                    new Column { Title = "Primary Column", Primary = true, Type =  ColumnType.TextNumber},
                    new Column {Title = "Favourite", Type = ColumnType.CheckBox}
                }
});

Console.WriteLine("");

static HttpClient CreateHttpClient()
{
    var httpClient = new HttpClient
    {
        BaseAddress = new Uri("https://api.smartsheet.com")
    };
    httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", "APIKEY");

    return httpClient;
}

static void SetupLog()
{
    Log.Logger = new LoggerConfiguration()
        .MinimumLevel.Debug()
        .WriteTo.Console()
        .CreateLogger();

    Log.Information("Test");
}