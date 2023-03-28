namespace SmarterSheet.Sdk;

public sealed class SheetClient
{
    #region Fields
    private readonly HttpClient _httpClient;
    #endregion

    public SheetClient(HttpClient httpClient, string apiKey)
    {
        _httpClient = httpClient;
        _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", apiKey);

    }

    #region Rows

    public async Task<Row?> GetRow(ulong sheetId, ulong rowId, IEnumerable<RowInclusion>? inclusions = null)
    {
        using var response = await _httpClient.GetAsync(ApiRoutes.Rows.GetRow(sheetId, rowId, inclusions));

        var rowResponse = await response.HandleResponseAsync<Row>();
        if (rowResponse == default)
            return null;

        return rowResponse;
    }

    public async Task<Row?> AddRow(ulong sheetId, Row row, IEnumerable<RowInclusion>? inclusions = null)
    {
        using var response = await _httpClient.PostAsJsonAsync(ApiRoutes.Rows.AddRow(sheetId, inclusions), RowBuilder.DefaultAddRow(row));

        var rowsResponse = await response.HandleResultResponseAsync<Row>();
        if (rowsResponse == default)
            return null;

        return rowsResponse;
    }

    public async Task<Row?> AddRow(ulong sheetId, AddRow addRow, IEnumerable<RowInclusion>? inclusions = null)
    {
        using var response = await _httpClient.PostAsJsonAsync(ApiRoutes.Rows.AddRow(sheetId, inclusions), addRow);

        var rowsResponse = await response.HandleResultsResponseAsync<Row>();
        if (rowsResponse == default || !rowsResponse.Any())
            return null;

        return rowsResponse.FirstOrDefault();
    }

    public async Task<IEnumerable<Row>> AddRows(ulong sheetId, IEnumerable<AddRow> rows, IEnumerable<RowInclusion>? inclusions = null)
    {
        using var response = await _httpClient.PostAsJsonAsync(ApiRoutes.Rows.AddRows(sheetId, inclusions), rows);

        var rowsResponse = await response.HandleResultsResponseAsync<Row>();
        if (rowsResponse == default)
            return Array.Empty<Row>();

        return rowsResponse;
    }

    public async Task<IEnumerable<Row>> AddRows(ulong sheetId, IEnumerable<Row> rows, IEnumerable<RowInclusion>? inclusions = null)
    {
        using var response = await _httpClient.PostAsJsonAsync(ApiRoutes.Rows.AddRows(sheetId, inclusions), RowBuilder.DefaultAddRows(rows));

        var rowsResponse = await response.HandleResultsResponseAsync<Row>();
        if (rowsResponse == default)
            return Array.Empty<Row>();

        return rowsResponse;
    }

    public async Task<bool> CopyRowFromSheet(ulong fromSheetId, Row row, ulong toSheetId)
    {
        using var response = await _httpClient.PostAsJsonAsync(ApiRoutes.Rows.CopyRowFromSheet(fromSheetId), new CopyRowsToSheetRequest 
        { 
            Sheet = new CopyToSheetObject 
            { 
                SheetId = toSheetId 
            }, 
            RowIds = new ulong[] { row.Id }
        });


        return response.HandleResponse();
    }

    public async Task<bool> CopyRowsFromSheet(ulong fromSheetId, IEnumerable<Row> rows, ulong toSheetId)
    {
        using var response = await _httpClient.PostAsJsonAsync(ApiRoutes.Rows.CopyRowsFromSheet(fromSheetId), new CopyRowsToSheetRequest 
        { 
            Sheet = new CopyToSheetObject 
            { 
                SheetId = toSheetId 
            }, 
            RowIds = rows.Select(x => x.Id) 
        });

        return response.HandleResponse();
    }

    public async Task<bool> DeleteRow(ulong sheetId, ulong rowId)
    {
        using var response = await _httpClient.DeleteAsync(ApiRoutes.Rows.DeleteRow(sheetId, rowId));

        return response.HandleResponse();
    }

    public async Task<bool> DeleteRows(ulong sheetId, IEnumerable<ulong> rowIds)
    {
        using var response = await _httpClient.DeleteAsync(ApiRoutes.Rows.DeleteRows(sheetId, rowIds));

        return response.HandleResponse();

    }

    public async Task<bool> UpdateRow(ulong sheetId, Row row)
    {
        using var response = await _httpClient.PutAsJsonAsync(ApiRoutes.Rows.UpdateRow(sheetId), RowBuilder.DefaultUpdateRow(row));

        var updateResponse = await response.HandleResultsResponseAsync<Row>();
        if (updateResponse == default)
            return false;

        return true;
    }

    public async Task<bool> UpdateRow(ulong sheetId, UpdateRow row)
    {
        using var response = await _httpClient.PutAsJsonAsync(ApiRoutes.Rows.UpdateRows(sheetId), row);

        var updateResponse = await response.HandleResultsResponseAsync<Row>();
        if (updateResponse == default)
            return false;

        return true;
    }

    public async Task<bool> UpdateRows(ulong sheetId, IEnumerable<Row> rows)
    {
        using var response = await _httpClient.PutAsJsonAsync($"/sheets/{sheetId}/rows", RowBuilder.DefaultUpdateRows(rows));

        var updateResponse = await response.HandleResultsResponseAsync<Row>();
        if (updateResponse == default)
            return false;

        return true;
    }

    public async Task<bool> UpdateRows(ulong sheetId, IEnumerable<UpdateRow> rows)
    {
        using var response = await _httpClient.PutAsJsonAsync($"/sheets/{sheetId}/rows", rows);

        var updateResponse = await response.HandleResultsResponseAsync<Row>();
        if (updateResponse == default)
            return false;

        return true;
    }

    #endregion

    #region Sheets

    public async Task<Sheet?> GetSheet(ulong sheetId, IEnumerable<SheetInclusion>? inclusions = null)
    {
        using var response = await _httpClient.GetAsync(ApiRoutes.Sheets.GetSheet(sheetId, inclusions));

        var sheetResponse = await response.HandleResponseAsync<Sheet>();
        if (sheetResponse == default)
            return null;

        return sheetResponse;
    }

    public async Task<Sheet?> GetSheet(ulong sheetId, ulong filterId, IEnumerable<SheetInclusion>? inclusions = null)
    {
        using var response = await _httpClient.GetAsync(ApiRoutes.Sheets.GetSheet(sheetId, filterId, inclusions));

        var sheetResponse = await response.HandleResultResponseAsync<Sheet>();
        if (sheetResponse == default)
            return null;

        return sheetResponse;
    }

    public async Task<IEnumerable<Sheet>> GetSheets(IEnumerable<SheetInclusion>? inclusions = null)
    {
        using var response = await _httpClient.GetAsync(ApiRoutes.Sheets.GetSheets(inclusions));

        var sheetsResponse = await response.HandleIndexResultResponseAsync<Sheet>();
        if (sheetsResponse == default)
            return Array.Empty<Sheet>();

        return sheetsResponse;
    }

    public async Task<IEnumerable<Sheet>> GetSheetsInFolder(ulong folderId)
    {
        var folder = await GetFolder(folderId);
        if (folder == null || folder.Sheets == null)
            return Array.Empty<Sheet>();

        return folder.Sheets;
    }

    public async Task<Sheet?> CreateSheet(Sheet sheet)
    {
        using var response = await _httpClient.PostAsJsonAsync(ApiRoutes.Sheets.CreateSheet(),
            sheet.ConvertToCreateSheet());
                
        var sheetResponse = await response.HandleResultResponseAsync<Sheet>();
        if (sheetResponse == default)
            return null;

        return sheetResponse;
    }

    public async Task<Sheet?> CreateSheet(string sheetName, ulong templateId, IEnumerable<TemplateInclusion>? inclusions = null)
    {
        using var response = await _httpClient.PostAsJsonAsync(ApiRoutes.Sheets.CreateSheetFromTemplate(inclusions),
            new CreateSheetFromTemplateRequest
            {
                Name = sheetName,
                FromId = templateId,
            });

        var sheetResponse = await response.HandleResultResponseAsync<Sheet>();
        if(sheetResponse == default) 
            return null;

        return sheetResponse;
    }

    public async Task<Sheet?> CreateSheet(string sheetName, ulong templateId, ulong folderId, IEnumerable<SheetInclusion>? inclusions = null)
    {
        var response = await _httpClient.PostAsJsonAsync(ApiRoutes.Sheets.CreateSheetFromTemplateInFolder(folderId, inclusions),
            new CreateSheetFromTemplateRequest
            {
                Name = sheetName,
                FromId = templateId,
            });

        var sheetResponse = await response.HandleResultResponseAsync<Sheet>();
        if(sheetResponse == default) 
            return null;

        return sheetResponse;
    }

    public async Task<Sheet?> CreateSheetInFolder(Sheet sheet, ulong folderId)
    {
        using var response = await _httpClient.PostAsJsonAsync(ApiRoutes.Sheets.CreateSheetInFolder(folderId),
            sheet.ConvertToCreateSheet());

        var sheetResponse = await response.HandleResultResponseAsync<Sheet>();
        if (sheetResponse == default)
            return null;

        return sheetResponse;
    }

    public async Task<Sheet?> CreateSheetInWorkspace(Sheet sheet, ulong workspaceId)
    {
        using var response = await _httpClient.PostAsJsonAsync(ApiRoutes.Sheets.CreateSheetInWorkspace(workspaceId),
            sheet.ConvertToCreateSheet());

        var sheetResponse = await response.HandleResultResponseAsync<Sheet>();
        if (sheetResponse == default)
            return null;

        return sheetResponse;
    }

    public async Task<Sheet?> CopySheet(ulong sheedId, ulong folderId, string name, IEnumerable<SheetInclusion>? inclusions = null)
    {
        using var response = await _httpClient.PostAsJsonAsync(ApiRoutes.Sheets.CopySheet(sheedId, inclusions),
            new CopySheetRequest
            {
                DestinationId = folderId,
                DestinationType = "folder",
                NewName = name,
            });

        var sheetResponse = await response.HandleResultResponseAsync<Sheet>();
        if (sheetResponse == default)
            return null;

        return sheetResponse;
    }

    public async Task<bool> DeleteSheet(ulong sheetId)
    {
        using var response = await _httpClient.DeleteAsync(ApiRoutes.Sheets.DeleteSheet(sheetId));

        return await response.HandleEmptyResultResponseAsync();
    }

    public async Task<bool> MoveSheet(ulong sheetId, ulong folderId)
    {
        using var response = await _httpClient.PostAsJsonAsync(ApiRoutes.Sheets.MoveSheet(sheetId),
            new MoveSheetRequest
            {
                DestinationType = "folder",
                DestinationId = folderId
            });

        var sheetResponse = await response.HandleResultResponseAsync<Sheet>();
        if (sheetResponse == default)
            return false;

        return true;
    }

    public async Task<bool> RenameSheet(ulong sheetId, string name)
    {
        using var response = await _httpClient.PutAsJsonAsync(ApiRoutes.Sheets.RenameSheet(sheetId),
            new RenameSheetRequest
            {
                Name = name
            });

        var sheetResponse = await response.HandleResultResponseAsync<Sheet>();
        if (sheetResponse == default)
            return false;

        return true;
    }

    #endregion

    #region Folders

    public async Task<Folder?> GetFolder(ulong folderId, IEnumerable<FolderInclusion>? inclusions = null)
    {
        using var response = await _httpClient.GetAsync(ApiRoutes.Folders.GetFolder(folderId, inclusions));

        var folderResponse = await response.HandleResponseAsync<Folder>();
        if (folderResponse == default)
            return null;

        return folderResponse;
    }
    
    public async Task<IEnumerable<Folder>> GetFolders()
    {
        using var response = await _httpClient.GetAsync(ApiRoutes.Folders.GetFolders());

        var folderResponse = await response.HandleIndexResultResponseAsync<Folder>();
        if (folderResponse == default)
            return Array.Empty<Folder>();

        return folderResponse;
    }

    public async Task<IEnumerable<Folder>> GetSubFolders(ulong folderId)
    {
        using var response = await _httpClient.GetAsync(ApiRoutes.Folders.GetSubFolders(folderId));

        var folderResponse = await response.HandleIndexResultResponseAsync<Folder>();
        if (folderResponse == default)
            return Array.Empty<Folder>();

        return folderResponse;
    }

    public async Task<ulong> CreateFolder(string name)
    {
        using var response = await _httpClient.PostAsJsonAsync(ApiRoutes.Folders.CreateFolder(),
            new CreateFolderRequest 
            { 
                FolderName = name 
            });

        var folderResponse = await response.HandleResultResponseAsync<Folder>();
        if (folderResponse == default)
            return default;

        return folderResponse.Id;
    }

    public async Task<ulong> CreateFolderInWorkspace(ulong workspaceId, string name)
    {
        using var response = await _httpClient.PostAsJsonAsync(ApiRoutes.Folders.CreateFolderInWorkspace(workspaceId),
            new CreateFolderRequest
            {
                FolderName = name
            });

        var folderResponse = await response.HandleResultResponseAsync<Folder>();
        if (folderResponse == default)
            return default;

        return folderResponse.Id;
    }

    public async Task<ulong> CreateSubFolder(ulong folderId, string name)
    {
        using var response = await _httpClient.PostAsJsonAsync(ApiRoutes.Folders.CreateSubFolder(folderId),
            new CreateFolderRequest
            {
                FolderName = name
            });

        var folderResponse = await response.HandleResultResponseAsync<Folder>();
        if (folderResponse == default)
            return default;

        return folderResponse.Id;
    }

    public async Task<bool> CopyFolder(ulong folderId, ulong destinationFolderId, string name, IEnumerable<FolderInclusion> inclusions = null)
    {
        using var response = await _httpClient.PostAsJsonAsync(ApiRoutes.Folders.CopyFolder(folderId),
            new CopyFolderRequest
            {
                DestinationId = destinationFolderId,
                DestinationType = "folder",
                NewName = name,
            });

        return await response.HandleEmptyResultResponseAsync();
    }

    public async Task<bool> DeleteFolder(ulong folderId)
    {
        using var response = await _httpClient.DeleteAsync(ApiRoutes.Folders.DeleteFolder(folderId));

        return await response.HandleEmptyResultResponseAsync();
    }

    public async Task<bool> MoveFolder(ulong folderId, ulong destinationFolderId)
    {
        using var response = await _httpClient.PostAsJsonAsync(ApiRoutes.Folders.MoveFolder(folderId),
            new MoveFolderRequest
            {
                DestinationId = destinationFolderId,
                DestinationType = "folder"
            });

        var folderResponse = await response.HandleResponseAsync<Folder>();
        if (folderResponse == default)
            return false;

        return true;
    }

    public async Task<bool> RenameFolder(ulong folderId, string name)
    {
        using var response = await _httpClient.PutAsJsonAsync(ApiRoutes.Folders.RenameFolder(folderId),
               new RenameFolderRequest 
               { 
                   Name = name 
               });

        var folderResponse = await response.HandleResponseAsync<Folder>();
        if (folderResponse == default)
            return false;

        return true;
    }

    #endregion

    #region Workspaces

    public async Task<Workspace?> GetWorkspace(ulong workspaceId, bool loadAll = false)
    {
        using var response = await _httpClient.GetAsync(ApiRoutes.Workspaces.GetWorkspace(workspaceId, loadAll));

        var workspaceResponse = await response.HandleResponseAsync<Workspace>();
        if (workspaceResponse == default)
            return null;

        return workspaceResponse;
    }

    public async Task<IEnumerable<Workspace>> GetWorkspaces()
    {
        using var response = await _httpClient.GetAsync(ApiRoutes.Workspaces.GetWorkspaces());

        var workspaceResponse = await response.HandleIndexResultResponseAsync<Workspace>();
        if (workspaceResponse == default)
            return Array.Empty<Workspace>();

        return workspaceResponse;
    }

    public async Task<ulong> CreateWorkspace(string name)
    {
        using var response = await _httpClient.PostAsJsonAsync(ApiRoutes.Workspaces.CreateWorkspace(),
            new CreateWorkspaceRequest
            {
                WorkspaceName = name
            });

        var workspaceResponse = await response.HandleResultResponseAsync<Workspace>();
        if (workspaceResponse == default)
            return default;

        return workspaceResponse.Id;
    }

    public async Task<bool> CopyWorkspace(ulong workspaceId, string name, IEnumerable<WorkspaceInclusion>? inclusions = null)
    {
        using var response = await _httpClient.PostAsJsonAsync(ApiRoutes.Workspaces.CopyWorkspace(workspaceId, inclusions),
            new CopyWorkspaceRequest
            {
                Name = name,
            });

        return await response.HandleEmptyResultResponseAsync();
    }

    #endregion

    #region Attachments

    public async Task<Attachment?> GetAttachment(ulong sheetId, ulong attachmentId)
    {
        using var response = await _httpClient.GetAsync(ApiRoutes.Attachments.GetAttachment(sheetId, attachmentId));

        var attachmentResponse = await response.HandleResponseAsync<Attachment>();
        if (attachmentResponse == default)
            return null;

        return attachmentResponse;
    }
        
    public async Task<IEnumerable<Attachment>> GetAttachments(ulong sheetId)
    {
        using var response = await _httpClient.GetAsync(ApiRoutes.Attachments.GetAttachments(sheetId));

        var attachmentResponse = await response.HandleIndexResultResponseAsync<Attachment>();
        if (attachmentResponse == default)
            return Array.Empty<Attachment>();

        return attachmentResponse;
    }

    public async Task<IEnumerable<Attachment>> GetAttachments(ulong sheetId, ulong rowId)
    {
        using var response = await _httpClient.GetAsync(ApiRoutes.Attachments.GetAttachments(sheetId, rowId));

        var attachmentResponse = await response.HandleIndexResultResponseAsync<Attachment>();
        if (attachmentResponse == default)
            return Array.Empty<Attachment>();

        return attachmentResponse;
    }

    public async Task<bool> DeleteAttachment(ulong sheetId, ulong attachmentId)
    {
        using var response = await _httpClient.DeleteAsync(ApiRoutes.Attachments.DeleteAttachment(sheetId, attachmentId));

        return await response.HandleEmptyResultResponseAsync();
    }

    #endregion

    #region Discussions

    public async Task<Discussion?> GetDiscussion(ulong sheetId, ulong discussionId)
    {
        using var response = await _httpClient.GetAsync(ApiRoutes.Discussions.GetDiscussion(sheetId, discussionId));

        var discussionResponse = await response.HandleResultResponseAsync<Discussion>();
        if (discussionResponse == default)
            return null;

        return discussionResponse;
    }

    public async Task<IEnumerable<Discussion>> GetDiscussions(ulong sheetId, IEnumerable<DiscussionInclusion>? inclusions = null)
    {
        using var response = await _httpClient.GetAsync(ApiRoutes.Discussions.GetDiscussions(sheetId, inclusions));

        var discussionResponse = await response.HandleIndexResultResponseAsync<Discussion>();
        if (discussionResponse == default)
            return Array.Empty<Discussion>();

        return discussionResponse;
    }

    public async Task<IEnumerable<Discussion>> GetDiscussions(ulong sheetId, ulong rowId, IEnumerable<DiscussionInclusion>? inclusions = null)
    {
        using var response = await _httpClient.GetAsync(ApiRoutes.Discussions.GetDiscussions(sheetId, rowId, inclusions));

        var discussionResponse = await response.HandleIndexResultResponseAsync<Discussion>();
        if (discussionResponse == default)
            return Array.Empty<Discussion>();

        return discussionResponse;
    }

    public async Task<Discussion?> CreateDiscussion(ulong sheetId, string comment)
    {
        using var response = await _httpClient.PostAsJsonAsync(ApiRoutes.Discussions.CreateDiscussion(sheetId),
            new CreateDiscussionRequest
            {
                Comment = new Comment
                {
                    Text = comment
                }
            });

        var discussionResponse = await response.HandleResultResponseAsync<Discussion>();
        if (discussionResponse == default)
            return null;

        return discussionResponse;
    }

    public async Task<Discussion?> CreateDiscussion(ulong sheetId, ulong rowId, string comment)
    {
        using var response = await _httpClient.PostAsJsonAsync(ApiRoutes.Discussions.CreateDiscussion(sheetId, rowId),
            new CreateDiscussionRequest
            {
                Comment = new Comment 
                { 
                    Text = comment 
                }
            });

        var discussionResponse = await response.HandleResultResponseAsync<Discussion>();
        if (discussionResponse == default)
            return null;

        return discussionResponse;
    }

    public async Task<bool> DeleteDiscussion(ulong sheetId, ulong discussionId)
    {
        using var response = await _httpClient.DeleteAsync(ApiRoutes.Discussions.DeleteDiscussion(sheetId, discussionId));

        return await response.HandleEmptyResultResponseAsync();
    }

    #endregion

    #region Templates

    public async Task<IEnumerable<Template>> GetTemplates()
    {
        using var response = await _httpClient.GetAsync(ApiRoutes.Templates.GetTemplates());

        var templateResponse = await response.HandleIndexResultResponseAsync<Template>();
        if(templateResponse == default)
            return Array.Empty<Template>();

        return templateResponse;
    }

    #endregion

    /// <summary>
    /// Gets all columns of a sheet without retriving all the data in the sheet.
    /// Usefull for templating
    /// </summary>
    /// <param name="sheetId"></param>
    /// <returns></returns>
    public async Task<IEnumerable<Column>> GetSheetColumns(ulong sheetId)
    {
        using var response = await _httpClient.GetAsync(ApiRoutes.Misc.GetSheetColumns(sheetId));

        var sheetResponse = await response.HandleResponseAsync<Sheet>();
        if (sheetResponse == default || sheetResponse.Columns == null)
            return Array.Empty<Column>();

        return sheetResponse.Columns;
    }
}