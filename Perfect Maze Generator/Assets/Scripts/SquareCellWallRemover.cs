using UnityEngine;
public class SquareCellWallRemover : MonoBehaviour, IWallRemover
{
    public void RemoveWalls(ICell current, ICell next)
    {
        int xOffset = current.X - next.X;
        int yOffset = current.Y - next.Y;

        //Top Neighbour
        if (xOffset == 0 && yOffset == -1)
        {
            current.RemoveWalls(Wall.TOP);
            next.RemoveWalls(Wall.BOTTOM);
        }
        //Right Neighbour
        if (xOffset == -1 && yOffset == 0)
        {
            current.RemoveWalls(Wall.RIGHT);
            next.RemoveWalls(Wall.LEFT);
        }
        //Bottom Neighbour
        if (xOffset == 0 && yOffset == 1)
        {
            current.RemoveWalls(Wall.BOTTOM);
            next.RemoveWalls(Wall.TOP);
        }
        //Left Neighbour
        if (xOffset == 1 && yOffset == 0)
        {
            current.RemoveWalls(Wall.LEFT);
            next.RemoveWalls(Wall.RIGHT);
        }
    }
}
