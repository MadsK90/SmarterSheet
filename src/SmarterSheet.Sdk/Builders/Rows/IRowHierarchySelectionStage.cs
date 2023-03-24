namespace SmarterSheet.Sdk.Builders.Rows;

public interface IRowHierarchySelectionStage
{
    public IRowPositionStage WithParent(ulong parentId);
    public IRowPositionStage WithSibling(ulong siblingId);
}