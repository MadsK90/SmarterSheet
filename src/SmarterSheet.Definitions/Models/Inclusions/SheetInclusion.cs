namespace SmarterSheet.Definitions.Models.Inclusions;

[EnumExtensions]
public enum SheetInclusion
{
    [Display(Name = "attachments")]
    Attachments,

    [Display(Name = "columnType")]
    ColumnType,

    [Display(Name = "crossSheetReferences ")]
    CrossSheetReference,

    [Display(Name = "discussions")]
    Discussions,

    [Display(Name = "filters")]
    Filters,

    [Display(Name = "filterDefinitions")]
    FilterDefinitions,

    [Display(Name = "format")]
    Format,

    [Display(Name = "ganttConfig")]
    GanttConfig,

    [Display(Name = "objectValue")]
    ObjectValue,

    [Display(Name = "ownerInfo")]
    OwnerInfo,

    [Display(Name = "rowPermaLink")]
    RowPermalink,

    [Display(Name = "source")]
    Source,

    [Display(Name = "writerInfo")]
    WriterInfo
}