namespace SmarterSheet.Sdk.Builders.Rows;

public interface IRowPositionStage
{
    public IRowBuildStage ToBottom();
    public IRowBuildStage ToTop();
}