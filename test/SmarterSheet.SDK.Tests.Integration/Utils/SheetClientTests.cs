namespace SmarterSheet.SDK.Tests.Integration.Utils;

public abstract class SheetClientTests : IAsyncLifetime
{
    private readonly string _apiKey;

    protected SheetClientTests(ITestOutputHelper output, string apiKey)
    {
        _apiKey = apiKey;

        Log.Logger = new LoggerConfiguration()
            .WriteTo
            .TestOutput(output)
            .CreateLogger();
    }

    protected SheetClient CreateSheetClient()
    {
        var httpClient = new HttpClient
        {
            BaseAddress = new Uri(ApiRoutes.BASE)
        };

        return new SheetClient(httpClient, _apiKey);
    }

    public abstract Task DisposeAsync();
    public abstract Task InitializeAsync();
}