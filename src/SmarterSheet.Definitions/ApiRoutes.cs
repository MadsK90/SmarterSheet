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

        public static string DeleteSheet(ulong sheetId)
        {
            return $"{Version}/sheets/{sheetId}";
        }
    }
}