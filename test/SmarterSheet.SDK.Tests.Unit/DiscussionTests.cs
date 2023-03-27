namespace SmarterSheet.SDK.Tests.Unit;

public sealed class DiscussionTests : SheetClientTests
{
    #region Fields

    private readonly SheetClient _sut;
    private readonly Discussion _discussion;
    private readonly Discussion[] _discussions;

    #endregion

    #region Constants

    private const ulong DISCUSSION_ID = 1;
    private const ulong SHEET_ID = 2;
    private const ulong ROW_ID = 3;

    #endregion

    public DiscussionTests(ITestOutputHelper output) : base(output)
    {
        _discussion = CreateTestDiscussion();
        _discussions = CreateTestDiscussions();
        _sut = CreateSheetClient();
    }

    #region Get

    [Fact]
    public async Task GetDiscussion_ReturnDiscussion_WhenExists()
    {
        //Act
        var retrivedDiscussion = await _sut.GetDiscussion(SHEET_ID, DISCUSSION_ID);

        //Assert
        retrivedDiscussion.Should()
            .NotBeNull();

        retrivedDiscussion.Should()
            .BeEquivalentTo(_discussion);
    }

    [Fact]
    public async Task GetDiscussions_ReturnDiscussions_WhenExists()
    {
        //Act
        var retrivedDiscussions = await _sut.GetDiscussions(SHEET_ID);

        //Assert
        retrivedDiscussions.Should()
            .NotBeNull();

        var discussions = retrivedDiscussions.As<Discussion[]>();

        discussions.Should()
            .BeEquivalentTo(_discussions);
    }

    [Fact]
    public async Task GetDiscussions_FromRow_ReturnDiscussions_WhenExists()
    {
        //Act
        var retrivedDiscussions = await _sut.GetDiscussions(SHEET_ID, ROW_ID);

        //Assert
        retrivedDiscussions.Should()
            .NotBeNull();

        var discussions = retrivedDiscussions.As<Discussion[]>();

        discussions.Should()
            .BeEquivalentTo(_discussions);
    }

    #endregion

    #region Add

    [Fact]
    public async Task CreateDiscussion_ReturnDiscussion_WhenSuccessful()
    {
        //Act
        var createdDiscussion = await _sut.CreateDiscussion(SHEET_ID, "");

        //Assert
        createdDiscussion.Should()
            .NotBeNull();

        createdDiscussion.Should()
            .BeEquivalentTo(_discussion);
    }

    [Fact]
    public async Task CreateDiscussion_InRow_ReturnDiscussion_WhenSuccessful()
    {
        //Act
        var createdDiscussion = await _sut.CreateDiscussion(SHEET_ID, ROW_ID, "");

        //Assert
        createdDiscussion.Should()
            .NotBeNull();

        createdDiscussion.Should()
            .BeEquivalentTo(_discussion);
    }

    #endregion

    #region Delete

    [Fact]
    public async Task DeleteDiscussion_ReturnTrue_WhenSuccessful()
    {
        //Act
        var discussionDeleted = await _sut.DeleteDiscussion(SHEET_ID, DISCUSSION_ID);

        //Assert
        discussionDeleted.Should()
            .BeTrue();
    }

    #endregion

    #region Setup

    protected override void RegisterResponses(MockHttpMessageHandler mockHttp)
    {
        MockGetDiscussionResponse(mockHttp, _discussion);
        MockGetDiscussionsResponse(mockHttp, _discussions);
        MockGetDiscussionsFromRowResponse(mockHttp, _discussions);
        MockCreateDiscussionResponse(mockHttp, _discussion);
        MockCreateDiscussionOnRowResponse(mockHttp, _discussion);
        MockDeleteDiscussionResponse(mockHttp);
        
        #region Local Functions

        static void MockGetDiscussionResponse(MockHttpMessageHandler mockHttp, Discussion discussion)
        {
            mockHttp.When(HttpMethod.Get, ApiRoutes.Discussions.GetDiscussion(SHEET_ID, discussion.Id))
                .Respond(HttpStatusCode.OK, CreateSuccessfulResult(discussion));
        }

        static void MockGetDiscussionsResponse(MockHttpMessageHandler mockHttp, Discussion[] discussions)
        {
            var result = new IndexResult<Discussion>
            {
                PageNumber = 1,
                PageSize = 2,
                TotalPages = 1,
                TotalCount = 2,
                Data = discussions
            };

            mockHttp.When(HttpMethod.Get, ApiRoutes.Discussions.GetDiscussions(SHEET_ID))
                .Respond(HttpStatusCode.OK, JsonContent.Create(result));
        }

        static void MockGetDiscussionsFromRowResponse(MockHttpMessageHandler mockHttp, Discussion[] discussions)
        {
            var result = new IndexResult<Discussion>
            {
                PageNumber = 1,
                PageSize = 2,
                TotalPages = 1,
                TotalCount = 2,
                Data = discussions
            };

            mockHttp.When(HttpMethod.Get, ApiRoutes.Discussions.GetDiscussions(SHEET_ID, ROW_ID))
                .Respond(HttpStatusCode.OK, JsonContent.Create(result));
        }

        static void MockCreateDiscussionResponse(MockHttpMessageHandler mockHttp, Discussion discussion)
        {
            mockHttp.When(HttpMethod.Post, ApiRoutes.Discussions.CreateDiscussion(SHEET_ID))
                .Respond(HttpStatusCode.OK, CreateSuccessfulResult(discussion));
        }

        static void MockCreateDiscussionOnRowResponse(MockHttpMessageHandler mockHttp, Discussion discussion)
        {
            mockHttp.When(HttpMethod.Post, ApiRoutes.Discussions.CreateDiscussion(SHEET_ID, ROW_ID))
                .Respond(HttpStatusCode.OK, CreateSuccessfulResult(discussion));
        }

        static void MockDeleteDiscussionResponse(MockHttpMessageHandler mockHttp)
        {
            mockHttp.When(HttpMethod.Delete, ApiRoutes.Discussions.DeleteDiscussion(SHEET_ID, DISCUSSION_ID))
                .Respond(HttpStatusCode.OK, CreateSucessfulEmptyresult());
        }

        #endregion
    }

    private static Discussion CreateTestDiscussion()
    {
        return new Discussion
        {
            Id = 1,
            Title = "Single TestDiscussion"
        };
    }

    private static Discussion[] CreateTestDiscussions()
    {
        return new Discussion[]
        {
            new Discussion
            {
                Id = 2,
                Title = "TestDiscussion 2"
            },
            new Discussion
            {
                Id= 3,
                Title = "TestDiscussion 3"
            }
        };
    }

    #endregion
}