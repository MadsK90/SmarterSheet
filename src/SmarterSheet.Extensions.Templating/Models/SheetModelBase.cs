namespace SmarterSheet.Extensions.Templating.Models;

public abstract class SheetModelBase
{
    internal ulong RowId { get; set; }
    public int RowNumber { get; set; }
    public Discussion[]? Discussions { get; set; }
    public Attachment[]? Attachments { get; set; }
    public bool IsParent { get; set; }

    public ulong ModelSource { get; set; }
}