namespace SmarterSheet.SDK.Tests.Unit;

public sealed class AttachmentTests : SheetClientTests
{
    #region Fields

    private readonly SheetClient _sut;
    private readonly Attachment _attachment;
    private readonly Attachment[] _attachments;

    #endregion

    #region Constants

    private const ulong ATTACHMENT_ID = 1;
    private const ulong SHEET_ID = 2;
    private const ulong ROW_ID = 3;

    #endregion

    public AttachmentTests(ITestOutputHelper output) : base(output)
    {
        _attachment = CreateTestAttachment();
        _attachments = CreateTestAttachments();
        _sut = CreateSheetClient();
    }

    #region Get

    [Fact]
    public async Task GetAttachment_ReturnAttachment_WhenAttachmentExists()
    {
        //Act
        var retrivedAttachment = await _sut.GetAttachment(SHEET_ID, ATTACHMENT_ID);

        //Assert
        retrivedAttachment.Should()
            .NotBeNull();

        retrivedAttachment.Should()
            .BeEquivalentTo(_attachment);
    }

    [Fact]
    public async Task GetAttachments_ReturnsAttachments_WhenAttachmentsExists()
    {
        //Act
        var retrivedAttachments = await _sut.GetAttachments(SHEET_ID);

        //Assert
        retrivedAttachments.Should()
            .NotBeNull();

        var attachments = retrivedAttachments.As<Attachment[]>();

        attachments.Should()
            .BeEquivalentTo(_attachments);
    }

    [Fact]
    public async Task GetAttachments_FromRow_ReturnsAttachments_WhenAttachmentsExists()
    {
        //Act
        var retrivedAttachments = await _sut.GetAttachments(SHEET_ID, ROW_ID);

        //Assert
        retrivedAttachments.Should()
            .NotBeNull();

        var attachments = retrivedAttachments.As<Attachment[]>();

        attachments.Should()
            .BeEquivalentTo(_attachments);
    }


    #endregion

    #region Delete

    [Fact]
    public async Task DeleteAttachment_ReturnTrue_WhenSuccessful()
    {
        //Act
        var attachmentDeleted = await _sut.DeleteAttachment(SHEET_ID, ATTACHMENT_ID);

        //Assert
        attachmentDeleted.Should()
            .BeTrue();
    }

    #endregion

    #region Setup

    protected override void RegisterResponses(MockHttpMessageHandler mockHttp)
    {
        MockGetAttachmentResponse(mockHttp, _attachment);
        MockGetAttachmentsResponse(mockHttp, _attachments);
        MockGetAttachmentsFromRowResponse(mockHttp, _attachments);
        MockDeleteAttachmentResponse(mockHttp);

        #region Local Functions

        static void MockGetAttachmentResponse(MockHttpMessageHandler mockHttp, Attachment attachment)
        {
            mockHttp.When(HttpMethod.Get, ApiRoutes.Attachments.GetAttachment(SHEET_ID, ATTACHMENT_ID))
                .Respond(HttpStatusCode.OK, JsonContent.Create(attachment));
        }

        static void MockGetAttachmentsResponse(MockHttpMessageHandler mockHttp, Attachment[] attachments)
        {
            var result = new IndexResult<Attachment>()
            {
                PageNumber = 1,
                PageSize = 2,
                TotalPages = 1,
                TotalCount = 2,
                Data = attachments
            };

            mockHttp.When(HttpMethod.Get, ApiRoutes.Attachments.GetAttachments(SHEET_ID))
                .Respond(HttpStatusCode.OK, JsonContent.Create(result));
        }

        static void MockGetAttachmentsFromRowResponse(MockHttpMessageHandler mockHttp, Attachment[] attachments)
        {
            var result = new IndexResult<Attachment>()
            {
                PageNumber = 1,
                PageSize = 2,
                TotalPages = 1,
                TotalCount = 2,
                Data = attachments
            };

            mockHttp.When(HttpMethod.Get, ApiRoutes.Attachments.GetAttachments(SHEET_ID, ROW_ID))
                .Respond(HttpStatusCode.OK, JsonContent.Create(result));
        }

        static void MockDeleteAttachmentResponse(MockHttpMessageHandler mockHttp)
        {
            mockHttp.When(HttpMethod.Delete, ApiRoutes.Attachments.DeleteAttachment(SHEET_ID, ATTACHMENT_ID))
                .Respond(HttpStatusCode.OK, CreateSucessfulEmptyresult());
        }

        #endregion
    }

    private static Attachment CreateTestAttachment()
    {
        return new Attachment
        {
            Id = 1,
            Name = "Single TestAttachment"
        };
    }

    private static Attachment[] CreateTestAttachments()
    {
        return new Attachment[]
        {
            new Attachment
            {
                Id = 2,
                Name = "TestAttachment 1"
            },
            new Attachment
            {
                Id = 3,
                Name = "TestAttachment 2"
            }
        };
    }

    #endregion
}