namespace SmarterSheet.SDK.Tests.Unit;

public sealed class FolderTests : SheetClientTests
{
    #region Fields

    private readonly SheetClient _sut;
    private readonly Folder _folder;
    private readonly Folder[] _folders;

    #endregion

    #region Constants

    private const ulong FOLDER_ID = 1;
    private const ulong CREATE_FOLDER_ID = 2;
    private const ulong WORKSPACE_ID = 3;
    private const ulong CREATE_FOLDER_IN_WORKSPACE_ID = 4;
    private const ulong CREATE_SUBFOLDER_ID = 5;
    private const ulong COPY_DESTINATION_FOLDER_ID = 6;
    private const ulong MOVE_DESTINATION_FOLDER_ID = 7;

    private const string COPY_FOLDER_NAME = "Copied test folder";
    #endregion

    public FolderTests(ITestOutputHelper output) : base(output)
    {
        _folder = CreateTestFolder();
        _folders = CreateTestFolders();
        _sut = CreateSheetClient();
    }

    #region Get

    [Fact]
    public async Task GetFolder_ReturnFolder_WhenFolderExists()
    {
        //Arrange 

        //Act
        var retrivedFolder = await _sut.GetFolder(FOLDER_ID);

        //Assert
        retrivedFolder.Should()
            .NotBeNull();

        retrivedFolder.Should()
            .BeEquivalentTo(_folder);
    }

    [Fact]
    public async Task GetFolders_ReturnFolders_WhenFoldersExists()
    {
        //Act
        var retrivedFolders = await _sut.GetFolders();

        //Assert
        retrivedFolders.Should()
            .NotBeNull()
            .And.HaveCount(2);

        var folders = retrivedFolders.As<Folder[]>();

        folders.Should()
            .BeEquivalentTo(_folders);
    }

    [Fact]
    public async Task GetSubFolders_ReturnFolders_WhenSubFoldersExists()
    {

        //Act
        var retrivedSubFolders = await _sut.GetSubFolders(FOLDER_ID);

        //Assert
        retrivedSubFolders.Should()
            .NotBeNull();

        var folders = retrivedSubFolders.As<Folder[]>();

        folders.Should()
            .BeEquivalentTo(_folders);
    }

    #endregion

    #region Add

    [Fact]
    public async Task CreateFolder_ReturnId_WhenSuccessful()
    {
        //Act
        var createdFolderId = await _sut.CreateFolder("");

        //Assert
        createdFolderId.Should()
            .Be(createdFolderId);
    }

    [Fact]
    public async Task CreateFolderInWorkspace_ReturnId_WhenSuccessful()
    {
        //Act
        var createdFolderId = await _sut.CreateFolderInWorkspace(WORKSPACE_ID, "");

        //Assert
        createdFolderId.Should()
            .Be(CREATE_FOLDER_IN_WORKSPACE_ID);
    }

    [Fact]
    public async Task CreateSubFolder_ReturnId_WhenSuccesful()
    {
        //Act
        var createdSubFolder = await _sut.CreateSubFolder(FOLDER_ID, "");

        //assert
        createdSubFolder.Should()
            .Be(CREATE_SUBFOLDER_ID);
    }

    #endregion

    #region Copy

    [Fact]
    public async Task CopyFolder_ReturnTrue_WhenSuccessful()
    {
        //Act
        var copiedFolder = await _sut.CopyFolder(FOLDER_ID, COPY_DESTINATION_FOLDER_ID, COPY_FOLDER_NAME);

        //Assert
        copiedFolder.Should()
            .BeTrue();
    }

    #endregion

    #region Delete

    [Fact]
    public async Task DeleteFolder_ReturnTrue_WhenSuccessful()
    {
        //Act
        var folderDeleted = await _sut.DeleteFolder(FOLDER_ID);

        //Assert
        folderDeleted.Should()
            .BeTrue();
    }

    #endregion

    #region Move

    [Fact]
    public async Task MoveFolder_ReturnTrue_WhenSuccessful()
    {
        //Act
        var folderMoved = await _sut.MoveFolder(FOLDER_ID, MOVE_DESTINATION_FOLDER_ID);

        //Assert
        folderMoved.Should()
            .BeTrue();
    }

    #endregion

    #region Rename

    [Fact]
    public async Task RenameFolder_ReturnTrue_WhenSuccessful()
    {
        //Act
        var folderRenamed = await _sut.RenameFolder(FOLDER_ID, "");

        //Assert
        folderRenamed.Should()
            .BeTrue();
    }

    #endregion

    #region Setup

    protected override void RegisterResponses(MockHttpMessageHandler mockHttp)
    {
        MockGetFolderResponse(mockHttp, _folder);
        MockGetFoldersResponse(mockHttp, _folders);
        MockGetSubFoldersResponse(mockHttp, _folders);
        MockCreateFolderResponse(mockHttp);
        MockCreateFolderInWorkspaceResponse(mockHttp);
        MockCreateSubFolderResponse(mockHttp);
        MockCopyFolderResponse(mockHttp);
        MockDeleteFolderResponse(mockHttp);
        MockMoveFolderResponse(mockHttp);
        MockRenameFolderResponse(mockHttp);

        #region Local Functions

        static void MockGetFolderResponse(MockHttpMessageHandler mockHttp, Folder folder)
        {
            mockHttp.When(HttpMethod.Get, ApiRoutes.Folders.GetFolder(folder.Id))
                .Respond(HttpStatusCode.OK, JsonContent.Create(folder));
        }

        static void MockGetFoldersResponse(MockHttpMessageHandler mockHttp, Folder[] folders)
        {
            var result = new IndexResult<Folder>()
            {
                PageNumber = 1,
                PageSize = 2,
                TotalPages = 1,
                TotalCount = 2,
                Data = folders
            };

            mockHttp.When(HttpMethod.Get, ApiRoutes.Folders.GetFolders())
                .Respond(HttpStatusCode.OK, JsonContent.Create(result));
        }

        static void MockGetSubFoldersResponse(MockHttpMessageHandler mockHttp, Folder[] folders)
        {
            var result = new IndexResult<Folder>()
            {
                PageNumber = 1,
                PageSize = 2,
                TotalPages = 1,
                TotalCount = 2,
                Data = folders
            };

            mockHttp.When(HttpMethod.Get, ApiRoutes.Folders.GetSubFolders(FOLDER_ID))
                .Respond(HttpStatusCode.OK, JsonContent.Create(result));
        }

        static void MockCreateFolderResponse(MockHttpMessageHandler mockHttp)
        {
            var createdFolder = new Folder
            {
                Id = CREATE_FOLDER_ID
            };

            mockHttp.When(HttpMethod.Post, ApiRoutes.Folders.CreateFolder())
                .Respond(HttpStatusCode.OK, CreateSuccessfulResult(createdFolder));
        }

        static void MockCreateFolderInWorkspaceResponse(MockHttpMessageHandler mockHttp)
        {
            var createdFolder = new Folder
            {
                Id = CREATE_FOLDER_IN_WORKSPACE_ID
            };

            mockHttp.When(HttpMethod.Post, ApiRoutes.Folders.CreateFolderInWorkspace(WORKSPACE_ID))
                .Respond(HttpStatusCode.OK, CreateSuccessfulResult(createdFolder));
        }

        static void MockCreateSubFolderResponse(MockHttpMessageHandler mockHttp)
        {
            var createdFolder = new Folder
            {
                Id = CREATE_SUBFOLDER_ID
            };

            mockHttp.When(HttpMethod.Post, ApiRoutes.Folders.CreateSubFolder(FOLDER_ID))
                .Respond(HttpStatusCode.OK, CreateSuccessfulResult(createdFolder));
        }

        static void MockCopyFolderResponse(MockHttpMessageHandler mockHttp)
        {
            mockHttp.When(HttpMethod.Post, ApiRoutes.Folders.CopyFolder(FOLDER_ID))
                .Respond(HttpStatusCode.OK, CreateSucessfulEmptyresult());
        }

        static void MockDeleteFolderResponse(MockHttpMessageHandler mockHttp)
        {
            mockHttp.When(HttpMethod.Delete, ApiRoutes.Folders.DeleteFolder(FOLDER_ID))
                .Respond(HttpStatusCode.OK, CreateSucessfulEmptyresult());
        }

        static void MockMoveFolderResponse(MockHttpMessageHandler mockHttp)
        {
            mockHttp.When(HttpMethod.Post, ApiRoutes.Folders.MoveFolder(FOLDER_ID))
                .Respond(HttpStatusCode.OK, CreateSucessfulEmptyresult());
        }

        static void MockRenameFolderResponse(MockHttpMessageHandler mockHttp)
        {
            mockHttp.When(HttpMethod.Put, ApiRoutes.Folders.DeleteFolder(FOLDER_ID))
                .Respond(HttpStatusCode.OK, CreateSucessfulEmptyresult());
        }

        #endregion
    }

    private static Folder CreateTestFolder()
    {
        return new Folder
        {
            Id = 1,
            Name = "Single TestFolder",
        };
    }

    private static Folder[] CreateTestFolders()
    {
        return new Folder[]
        {
            new Folder
            {
                Id = 2,
                Name = "TestFolder 1"
            },
            new Folder
            {
                Id = 3,
                Name = "TestFolder 2"
            }
        };
    }

    #endregion
}