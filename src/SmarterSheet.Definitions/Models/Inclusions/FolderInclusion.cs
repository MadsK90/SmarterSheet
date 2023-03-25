namespace SmarterSheet.Definitions.Models.Inclusions;

[EnumExtensions]
public enum FolderInclusion
{
    [Display(Name = "source")]
    Source,

    [Display(Name = "distributionLink")]
    DistributionLink,

    [Display(Name = "ownerInfo")]
    OwnerInfo,

    [Display(Name = "sheetVersion")]
    SheetVersion,

    [Display(Name = "permalinks")]
    Permalinks
}