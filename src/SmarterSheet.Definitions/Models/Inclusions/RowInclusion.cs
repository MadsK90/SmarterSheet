namespace SmarterSheet.Definitions.Models.Inclusions;

[EnumExtensions]
public enum RowInclusion
{
    [Display(Name = "attachments")]
    Attachments,

    [Display(Name = "columnType")]
    ColumnType,

    [Display(Name = "discussions")]
    Discussions,

    [Display(Name = "filters")]
    Filters,

    [Display(Name = "format")]
    Format,

    [Display(Name = "objectValue")]
    ObjectValue,

    [Display(Name = "rowPermalink")]
    RowPermalink,

    [Display(Name = "writerInfo")]
    WriterInfo,

    [Display(Name = "columns")]
    Columns
}