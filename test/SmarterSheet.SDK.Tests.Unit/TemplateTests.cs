namespace SmarterSheet.SDK.Tests.Unit;

public sealed class TemplateTests : SheetClientTests
{
    #region Fields

    private readonly SheetClient _sut;
    private readonly Template[] _templates;

    #endregion

    #region Constants

    #endregion

    public TemplateTests(ITestOutputHelper output) : base(output)
    {
        _templates = CreateTestTemplates();
        _sut = CreateSheetClient();
    }

    #region Get

    [Fact]
    public async Task GetTemplates_ReturnTemplates_WhenSuccessful()
    {
        //Act
        var retrivedTemplates = await _sut.GetTemplates();

        //Assert
        retrivedTemplates.Should()
            .NotBeNull();

        var templates = retrivedTemplates.As<Template[]>();

        templates.Should()
            .BeEquivalentTo(_templates);
    }

    #endregion

    #region Setup

    protected override void RegisterResponses(MockHttpMessageHandler mockHttp)
    {
        MockGetTemplatesResponse(mockHttp, _templates);

        #region Local Functions

        static void MockGetTemplatesResponse(MockHttpMessageHandler mockHttp, Template[] templates)
        {
            var result = new IndexResult<Template>
            {
                PageNumber = 1,
                PageSize = 2,
                TotalPages = 1,
                TotalCount = 2,
                Data = templates
            };

            mockHttp.When(HttpMethod.Get, ApiRoutes.Templates.GetTemplates())
                .Respond(HttpStatusCode.OK, JsonContent.Create(result));
        }

        #endregion
    }

    private static Template[] CreateTestTemplates()
    {
        return new Template[]
        {
            new Template
            {
                Id = 1,
                Name = "TestTemplate 1"
            },
            new Template
            {
                Id = 2,
                Name = "TestTemplate 2"
            }
        };
    }

    #endregion
}