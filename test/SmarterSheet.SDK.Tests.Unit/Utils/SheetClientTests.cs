namespace SmarterSheet.SDK.Tests.Unit.Utils;

public abstract class SheetClientTests
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
        var mockHttp = new MockHttpMessageHandler();

        RegisterResponses(mockHttp);

        var httpClient = mockHttp.ToHttpClient();
        httpClient.BaseAddress = new Uri(ApiRoutes.BASE);

        return new SheetClient(httpClient, _apiKey);
    }

    protected static JsonContent CreateSuccessfulResult<T>(T data)
    {
        return JsonContent.Create(new ResultObject<T>
        {
            Message = "SUCCESS",
            ResultCode = 0,
            Result = data
        });
    }

    protected static JsonContent CreateSucessfulEmptyresult()
    {
        return JsonContent.Create(new EmptyResultObject
        {
            Message = "SUCCESS",
            ResultCode = 0,
        });
    }

    protected abstract void RegisterResponses(MockHttpMessageHandler mockHttp);
}