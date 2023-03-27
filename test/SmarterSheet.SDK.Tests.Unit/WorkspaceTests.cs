namespace SmarterSheet.SDK.Tests.Unit;

public sealed class WorkspaceTests : SheetClientTests
{
    #region Fields

    private readonly SheetClient _sut;
    private readonly Workspace _workspace;
    private readonly Workspace[] _workspaces;

    #endregion

    #region Constants

    private const ulong WORKSPACE_ID = 1;
    private const ulong CREATE_WORKSPACE_ID = 2;

    #endregion

    public WorkspaceTests(ITestOutputHelper output) : base(output)
    {
        _workspace = CreateTestWorkspace();
        _workspaces = CreateTestWorkspaces();
        _sut = CreateSheetClient();
    }

    #region Get

    [Fact]
    public async Task GetWorkspace_ReturnWorkspace_WhenSuccessful()
    {
        //Act
        var retrivedWorkspace = await _sut.GetWorkspace(WORKSPACE_ID);

        //Assert
        retrivedWorkspace.Should()
            .NotBeNull();

        retrivedWorkspace.Should()
            .BeEquivalentTo(_workspace);
    }

    [Fact]
    public async Task GetWorkspaces_ReturnWorkspaces_WhenSuccessful()
    {
        //Act
        var retrivedWorkspaces = await _sut.GetWorkspaces();

        //Assert
        retrivedWorkspaces.Should()
            .NotBeNull();

        var workspaces = retrivedWorkspaces.As<Workspace[]>();

        workspaces.Should()
            .BeEquivalentTo(_workspaces);
    }

    #endregion

    #region Add

    [Fact]
    public async Task CreateWorkspace_ReturnId_WhenSuccessful()
    {
        //Act
        var addedWorkspace = await _sut.CreateWorkspace("");

        //Assert
        addedWorkspace.Should()
            .Be(CREATE_WORKSPACE_ID);
    }

    #endregion

    #region Copy

    [Fact]
    public async Task CopyWorkspace_ReturnTrue_WhenSuccessful()
    {
        //Act
        var workspaceCopied = await _sut.CopyWorkspace(WORKSPACE_ID, "");

        //Assert
        workspaceCopied.Should()
            .BeTrue();
    }

    #endregion

    #region Setup

    protected override void RegisterResponses(MockHttpMessageHandler mockHttp)
    {
        MockGetWorkspaceResponse(mockHttp, _workspace);
        MockGetWorkspacesResponse(mockHttp, _workspaces);
        MockCreateWorkspaceResponse(mockHttp);
        MockCopyWorkspaceResponse(mockHttp);
        
        #region Local Functions

        static void MockGetWorkspaceResponse(MockHttpMessageHandler mockHttp, Workspace workspace)
        {
            mockHttp.When(HttpMethod.Get, ApiRoutes.Workspaces.GetWorkspace(workspace.Id))
                .Respond(HttpStatusCode.OK, JsonContent.Create(workspace));
        }

        static void MockGetWorkspacesResponse(MockHttpMessageHandler mockHttp, Workspace[] workspaces)
        {
            var result = new IndexResult<Workspace>()
            {
                PageNumber = 1,
                PageSize = 2,
                TotalPages = 1,
                TotalCount = 2,
                Data = workspaces
            };

            mockHttp.When(HttpMethod.Get, ApiRoutes.Workspaces.GetWorkspaces())
                .Respond(HttpStatusCode.OK, JsonContent.Create(result));
        }

        static void MockCreateWorkspaceResponse(MockHttpMessageHandler mockHttp)
        {
            var createdWorkspace = new Workspace
            {
                Id = CREATE_WORKSPACE_ID
            };

            mockHttp.When(HttpMethod.Post, ApiRoutes.Workspaces.CreateWorkspace())
                .Respond(HttpStatusCode.OK, CreateSuccessfulResult(createdWorkspace));
        }

        static void MockCopyWorkspaceResponse(MockHttpMessageHandler mockHttp)
        {
            mockHttp.When(HttpMethod.Post, ApiRoutes.Workspaces.CopyWorkspace(WORKSPACE_ID))
                .Respond(HttpStatusCode.OK, CreateSucessfulEmptyresult());
        }

        #endregion
    }

    private static Workspace CreateTestWorkspace()
    {
        return new Workspace
        {
            Id = 1,
            Name = "Single Workspace"
        };
    }

    private static Workspace[] CreateTestWorkspaces()
    {
        return new Workspace[]
        {
            new Workspace
            {
                Id = 2,
                Name = "Test Workspace 1"
            },
            new Workspace
            {
                Id = 3,
                Name = "Test Workspace 2"
            }
        };
    }

    #endregion
}