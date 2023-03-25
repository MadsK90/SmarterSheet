using SmarterSheet.Sdk.Requests.Sheet;

namespace SmarterSheet.Sdk;

public sealed class SheetClient
{
    #region Fields
    private readonly HttpClient _httpClient;
    #endregion

    public SheetClient(HttpClient httpClient)
    {
        _httpClient = httpClient;
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

        var rowResponse = await response.HandleResponseAsync<Row>();
        if (rowResponse == default)
            return false;

        return true;
    }

    public async Task<bool> DeleteRows(ulong sheetId, IEnumerable<ulong> rowIds)
    {
        using var response = await _httpClient.DeleteAsync(ApiRoutes.Rows.DeleteRows(sheetId, rowIds));

        var rowResponse = await response.HandleResponseAsync<Row>();
        if (rowResponse == default)
            return false;

        return true;   
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

        var sheetResponse = await response.HandleResponseAsync<Sheet>();
        if (sheetResponse == default)
            return null;

        return sheetResponse;
    }

    public async Task<IEnumerable<Sheet>> GetSheets(IEnumerable<SheetInclusion>? inclusions = null)
    {
        using var response = await _httpClient.GetAsync(ApiRoutes.Sheets.GetSheets(inclusions));

        var sheetsResponse = await response.HandleIndexResultResponse<Sheet>();
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

    public async Task<bool> DeleteSheet(ulong sheetId)
    {
        using var response = await _httpClient.DeleteAsync(ApiRoutes.Sheets.DeleteSheet(sheetId));

        return await response.HandleEmptyResultResponseAsync();
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

    #endregion
}