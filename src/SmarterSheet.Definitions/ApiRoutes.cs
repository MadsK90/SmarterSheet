namespace SmarterSheet.Definitions;

public static class ApiRoutes
{
    public const string Version = "/2.0";
    public const string Base = "https://api.smartsheet.com";

    public static class Rows
    {
        public static string GetRow(ulong sheetId, ulong rowId, IEnumerable<RowInclusion>? inclusions = null)
        {
            var path = $"{Version}/sheets/{sheetId}/rows/{rowId}";

            if (inclusions == null)
                return path;

            return $"{path}{UrlHelper.CreateUrlParameter(inclusions)}";
        }

        public static string AddRow(ulong sheetId, IEnumerable<RowInclusion>? inclusions = null)
        {
            var path = $"{Version}/sheets/{sheetId}/rows";

            if (inclusions == null)
                return path;

            return $"{path}{UrlHelper.CreateUrlParameter(inclusions)}";
        }

        public static string AddRows(ulong sheetId, IEnumerable<RowInclusion>? inclusions = null)
        {
            var path = $"{Version}/sheets/{sheetId}/rows";

            if (inclusions == null)
                return path;

            return $"{path}{UrlHelper.CreateUrlParameter(inclusions)}";
        }

        public static string CopyRowFromSheet(ulong fromSheetId)
        {
            return $"{Version}/sheets/{fromSheetId}/rows/copy";
        }

        public static string CopyRowsFromSheet(ulong fromSheetId)
        {
            return $"{Version}/sheets/{fromSheetId}/rows/copy";
        }

        public static string DeleteRow(ulong sheetId, ulong rowId)
        {
            return $"{Version}/sheets/{sheetId}/rows?ids={rowId}";
        }

        public static string DeleteRows(ulong sheetId, IEnumerable<ulong> rowIds)
        {
            return $"{Version}/sheets/{sheetId}/rows?ids={rowIds.CreateCommaSeperatedList()}";
        }

        public static string UpdateRow(ulong sheetId)
        {
            return $"{Version}/sheets/{sheetId}/rows";
        }

        public static string UpdateRows(ulong sheetId)
        {
            return $"{Version}/sheets/{sheetId}/rows";
        }
    }

    public static class Sheets
    {


        public static string CreateSheet()
        {
            return $"{Version}/sheets";
        }

        public static string CreateSheetInFolder(ulong folderId)
        {
            return $"{Version}/folders/{folderId}/sheets";
        }

        public static string CreateSheetInWorkspace(ulong workspaceId)
        {
            return $"{Version}/workspaces/{workspaceId}/sheets";
        }

        public static string DeleteSheet(ulong sheetId)
        {
            return $"{Version}/sheets/{sheetId}";
        }

        public static string GetSheet(ulong sheetId, IEnumerable<SheetInclusion>? inclusions = null)
        {
            var path = $"{Version}/sheets/{sheetId}";

            if(inclusions == null) 
                return path;

            return $"{path}{UrlHelper.CreateUrlParameter(inclusions)}";
        }

        public static string GetSheet(ulong sheetId, ulong filterId, IEnumerable<SheetInclusion>? inclusions = null)
        {
            var path = $"{Version}/sheets/{sheetId}";

            if (inclusions == null)
                return $"{path}exclude=filteredOutRows&filter={filterId}";

            return $"{path}{UrlHelper.CreateUrlParameter(inclusions)}&exclude=filteredOutRows&filter={filterId}";
        }
    
        public static string GetSheets(IEnumerable<SheetInclusion>? inclusions = null)
        {
            var path = $"{Version}/sheets";

            if( inclusions == null)
                return path;

            return $"{path}{UrlHelper.CreateUrlParameter(inclusions)}";
        }

        public static string CopySheet(ulong sheedId, IEnumerable<SheetInclusion>? inclusions)
        {
            var path = $"{Version}/sheets/{sheedId}/copy";

            if (inclusions == null)
                return path;

            return $"{path}{UrlHelper.CreateUrlParameter(inclusions)}";
        }

        public static string MoveSheet(ulong sheetId)
        {
            return $"{Version}/sheets/{sheetId}/move";
        }

        public static string RenameSheet(ulong sheetId)
        {
            return $"{Version}/sheets/{sheetId}";
        }

        public static string CreateSheetFromTemplate(IEnumerable<TemplateInclusion>? inclusions)
        {
            var path = $"{Version}/sheets";

            if(inclusions == null)
                return path;

            return $"{path}{UrlHelper.CreateUrlParameter(inclusions)}";
        }

        public static string CreateSheetFromTemplateInFolder(ulong folderId, IEnumerable<SheetInclusion>? inclusions)
        {
            var path = $"{Version}/folders/{folderId}/sheets";

            if (inclusions == null)
                return path;

            return $"{path}{UrlHelper.CreateUrlParameter(inclusions)}";
        }
    }

    public static class Folders
    {
        public static string GetFolder(ulong folderId, IEnumerable<FolderInclusion>? inclusions = null)
        {
            var path = $"{Version}/folders/{folderId}";

            if (inclusions == null)
                return path;

            return $"{path}{UrlHelper.CreateUrlParameter(inclusions)}";

        }
    }
}