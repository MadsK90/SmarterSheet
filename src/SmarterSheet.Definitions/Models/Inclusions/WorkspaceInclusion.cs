namespace SmarterSheet.Definitions.Models.Inclusions;

[EnumExtensions]
public enum WorkspaceInclusion
{
    [Display(Name ="attachments")]
    Attachments,

    [Display(Name = "cellLinks")]
    CellLinks,

    [Display(Name = "data")]
    Data,

    [Display(Name = "discussions")]
    Discussions,

    [Display(Name = "filters")]
    Filters,

    [Display(Name = "forms")]
    Forms,

    [Display(Name = "ruleRecipients")]
    RuleRecipients,

    [Display(Name = "rules")]
    Rules,

    [Display(Name = "shares")]
    Shares
}