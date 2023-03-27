namespace SmarterSheet.Definitions;

public static class ApiRoutes
{
    public const string VERSION = "/2.0";
    public const string BASE = "https://api.smartsheet.com";

    public static class Rows
    {
        public static string GetRow(ulong sheetId, ulong rowId, IEnumerable<RowInclusion>? inclusions = null)
        {
            var path = $"{VERSION}/sheets/{sheetId}/rows/{rowId}";

            if (inclusions == null)
                return path;

            return $"{path}{UrlHelper.CreateUrlParameter(inclusions)}";
        }

        public static string AddRow(ulong sheetId, IEnumerable<RowInclusion>? inclusions = null)
        {
            var path = $"{VERSION}/sheets/{sheetId}/rows";

            if (inclusions == null)
                return path;

            return $"{path}{UrlHelper.CreateUrlParameter(inclusions)}";
        }

        public static string AddRows(ulong sheetId, IEnumerable<RowInclusion>? inclusions = null)
        {
            var path = $"{VERSION}/sheets/{sheetId}/rows";

            if (inclusions == null)
                return path;

            return $"{path}{UrlHelper.CreateUrlParameter(inclusions)}";
        }

        public static string CopyRowFromSheet(ulong fromSheetId)
        {
            return $"{VERSION}/sheets/{fromSheetId}/rows/copy";
        }

        public static string CopyRowsFromSheet(ulong fromSheetId)
        {
            return $"{VERSION}/sheets/{fromSheetId}/rows/copy";
        }

        public static string DeleteRow(ulong sheetId, ulong rowId)
        {
            return $"{VERSION}/sheets/{sheetId}/rows?ids={rowId}";
        }

        public static string DeleteRows(ulong sheetId, IEnumerable<ulong> rowIds)
        {
            return $"{VERSION}/sheets/{sheetId}/rows?ids={rowIds.CreateCommaSeperatedList()}";
        }

        public static string UpdateRow(ulong sheetId)
        {
            return $"{VERSION}/sheets/{sheetId}/rows";
        }

        public static string UpdateRows(ulong sheetId)
        {
            return $"{VERSION}/sheets/{sheetId}/rows";
        }
    }

    public static class Sheets
    {
        public static string CreateSheet()
        {
            return $"{VERSION}/sheets";
        }

        public static string CreateSheetInFolder(ulong folderId)
        {
            return $"{VERSION}/folders/{folderId}/sheets";
        }

        public static string CreateSheetFromTemplate(IEnumerable<TemplateInclusion>? inclusions = null)
        {
            var path = $"{VERSION}/sheets";

            if (inclusions == null)
                return path;

            return $"{path}{UrlHelper.CreateUrlParameter(inclusions)}";
        }

        public static string CreateSheetFromTemplateInFolder(ulong folderId, IEnumerable<SheetInclusion>? inclusions = null)
        {
            var path = $"{VERSION}/folders/{folderId}/sheets";

            if (inclusions == null)
                return path;

            return $"{path}{UrlHelper.CreateUrlParameter(inclusions)}";
        }

        public static string CreateSheetInWorkspace(ulong workspaceId)
        {
            return $"{VERSION}/workspaces/{workspaceId}/sheets";
        }

        public static string DeleteSheet(ulong sheetId)
        {
            return $"{VERSION}/sheets/{sheetId}";
        }

        public static string GetSheet(ulong sheetId, IEnumerable<SheetInclusion>? inclusions = null)
        {
            var path = $"{VERSION}/sheets/{sheetId}";

            if(inclusions == null) 
                return path;

            return $"{path}{UrlHelper.CreateUrlParameter(inclusions)}";
        }

        public static string GetSheet(ulong sheetId, ulong filterId, IEnumerable<SheetInclusion>? inclusions = null)
        {
            var path = $"{VERSION}/sheets/{sheetId}";

            if (inclusions == null)
                return $"{path}exclude=filteredOutRows&filter={filterId}";

            return $"{path}{UrlHelper.CreateUrlParameter(inclusions)}&exclude=filteredOutRows&filter={filterId}";
        }
    
        public static string GetSheets(IEnumerable<SheetInclusion>? inclusions = null)
        {
            var path = $"{VERSION}/sheets";

            if( inclusions == null)
                return path;

            return $"{path}{UrlHelper.CreateUrlParameter(inclusions)}";
        }

        public static string CopySheet(ulong sheedId, IEnumerable<SheetInclusion>? inclusions = null)
        {
            var path = $"{VERSION}/sheets/{sheedId}/copy";

            if (inclusions == null)
                return path;

            return $"{path}{UrlHelper.CreateUrlParameter(inclusions)}";
        }

        public static string MoveSheet(ulong sheetId)
        {
            return $"{VERSION}/sheets/{sheetId}/move";
        }

        public static string RenameSheet(ulong sheetId)
        {
            return $"{VERSION}/sheets/{sheetId}";
        }
    }

    public static class Folders
    {
        public static string GetFolder(ulong folderId, IEnumerable<FolderInclusion>? inclusions = null)
        {
            var path = $"{VERSION}/folders/{folderId}";

            if (inclusions == null)
                return path;

            return $"{path}{UrlHelper.CreateUrlParameter(inclusions)}";

        }

        public static string CreateFolder()
        {
            return $"{VERSION}/home/folders";
        }

        public static string CreateFolderInWorkspace(ulong workspaceId)
        {
            return $"{VERSION}/workspaces/{workspaceId}/folders";
        }

        public static string CreateSubFolder(ulong folderId)
        {
            return $"{VERSION}/folders/{folderId}/folders";
        }

        public static string DeleteFolder(ulong folderId)
        {
            return $"{VERSION}/folders/{folderId}";
        }

        public static string? GetSubFolders(ulong folderId)
        {
            return $"{VERSION}/folders/{folderId}/folders";
        }

        public static string? GetFolders()
        {
            return $"{VERSION}/home/folders";
        }

        public static string MoveFolder(ulong folderId)
        {
            return $"{VERSION}/folders/{folderId}/move";
        }

        public static string RenameFolder(ulong folderId)
        {
            return $"{VERSION}/folders/{folderId}";
        }

        public static string CopyFolder(ulong folderId)
        {
            return $"{VERSION}/folders/{folderId}/copy";
        }
    }

    public static class Workspaces
    {
        public static string CopyWorkspace(ulong workspaceId, IEnumerable<WorkspaceInclusion>? inclusions = null)
        {
            var path = $"{VERSION}/workspaces/{workspaceId}/copy";

            if (inclusions == null)
                return path;

            return $"{path}{UrlHelper.CreateUrlParameter(inclusions)}";
        }

        public static string CreateWorkspace()
        {
            return $"{VERSION}/workspaces";
        }

        public static string GetWorkspace(ulong workspaceId, bool loadAll = false)
        {
            return $"{VERSION}/workspaces/{workspaceId}?loadAll={loadAll}";
        }

        public static string? GetWorkspaces()
        {
            return $"{VERSION}/workspaces";
        }
    }

    public static class Attachments
    {
        public static string? DeleteAttachment(ulong sheetId, ulong attachmentId)
        {
            return $"{VERSION}/sheets/{sheetId}/attachments/{attachmentId}";
        }

        public static string GetAttachment(ulong sheetId, ulong attachmentId)
        {
            return $"{VERSION}/sheets/{sheetId}/attachments/{attachmentId}";
        }

        public static string? GetAttachments(ulong sheetId)
        {
            return $"{VERSION}/sheets/{sheetId}/attachments";
        }

        public static string? GetAttachments(ulong sheetId, ulong rowId)
        {
            return $"{VERSION}/sheets/{sheetId}/rows/{rowId}/attachments";
        }
    }

    public static class Discussions
    {
        public static string CreateDiscussion(ulong sheetId)
        {
            return $"{VERSION}/sheets/{sheetId}/discussions";
        }

        public static string CreateDiscussion(ulong sheetId, ulong rowId)
        {
            return $"{VERSION}/sheets/{sheetId}/rows/{rowId}discussions";
        }

        public static string DeleteDiscussion(ulong sheetId, ulong discussionId)
        {
            return $"{VERSION}/sheets/{sheetId}/discussions/{discussionId}";
        }

        public static string GetDiscussion(ulong sheetId, ulong discussionId)
        {
            return $"{VERSION}/sheets/{sheetId}/discussions/{discussionId}";
        }

        public static string GetDiscussions(ulong sheetId, IEnumerable<DiscussionInclusion>? inclusions = null)
        {
            var path = $"/sheets/{sheetId}/discussions";

            if (inclusions == null)
                return path;

            return $"{path}{UrlHelper.CreateUrlParameter(inclusions)}";
        }

        public static string GetDiscussions(ulong sheetId, ulong rowId, IEnumerable<DiscussionInclusion>? inclusions = null)
        {
            var path = $"/sheets/{sheetId}/discussions/rows/{rowId}";

            if (inclusions == null)
                return path;

            return $"{path}{UrlHelper.CreateUrlParameter(inclusions)}";
        }
    }

    public static class Templates
    {
        public static string GetTemplates()
        {
            return $"{VERSION}/templates";
        }
    }

    public static class Misc
    {
        public static string GetSheetColumns(ulong sheetId)
        {
            return $"{VERSION}/sheets/{sheetId}?rowNumbers=0";
        }

    }
}