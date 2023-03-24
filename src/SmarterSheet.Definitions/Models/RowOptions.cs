namespace SmarterSheet.Definitions.Models;

public sealed class RowOptions
{
    public RowLocation Location { get; init; }
    public bool Expanded { get; init; }
    public bool Locked { get; init; }
    public ulong ParentId { get; init; }
    public ulong SiblingId { get; init; }

    public RowOptions(RowLocation location, bool expanded = false, bool locked = false, ulong parentId = 0, ulong siblingId = 0)
    {
        Location = location;
        Expanded = expanded;
        Locked = locked;
        ParentId = parentId;
        SiblingId = siblingId;
    }
}