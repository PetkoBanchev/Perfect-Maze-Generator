
public interface IWallRemover
{
    public CellType CellType { get; }
    public void RemoveWalls(ICell current, ICell next);
}
