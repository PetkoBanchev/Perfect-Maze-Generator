
public interface ICell
{
    public int X { get; set; }
    public int Y { get; set; }
    public bool IsVisited { get; set; }
    public void RemoveWalls(Wall wall);
    public ICell GetRandomUnvisitedNeighbour();
}
