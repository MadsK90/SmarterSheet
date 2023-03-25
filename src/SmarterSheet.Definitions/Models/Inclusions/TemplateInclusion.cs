namespace SmarterSheet.Definitions.Models.Inclusions;

[EnumExtensions]
public enum TemplateInclusion
{
    [Display(Name = "attachments")]
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
    Rules
}