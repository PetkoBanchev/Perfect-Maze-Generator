using System.Collections.Generic;
using UnityEngine;

public class SquareCell : MonoBehaviour, ICell
{
    #region Private Variables
    [SerializeField] private int x;
    [SerializeField] private int y;
    [SerializeField] private bool isVisited = false;
    [SerializeField] private GameObject[] wallObjects;
    [SerializeField] private MeshRenderer meshRenderer;
    #endregion

    #region Public properties
    public int X
    {
        get { return x; }
        set { x = value; }
    }
    public int Y
    {
        get { return y; }
        set { y = value; }
    }

    public bool IsVisited
    {
        get { return isVisited;}
        set { isVisited = value; SetColor(Color.green); }
    }
    #endregion

    #region Public methods
    public void SetColor(Color color)
    {
        meshRenderer.material.color = color;
    }

    public void RemoveWalls(Wall wall)
    {
        switch (wall)
        {
            case Wall.TOP:
                wallObjects[0].SetActive(false);
                break;
            case Wall.RIGHT:
                wallObjects[1].SetActive(false);
                break;
            case Wall.BOTTOM:
                wallObjects[2].SetActive(false);
                break;
            case Wall.LEFT:
                wallObjects[3].SetActive(false);
                break;
        }
    }

    public ICell GetRandomUnvisitedNeighbour()
    {
        List<ICell> neighbours = new List<ICell>();

        //Top Neighbour (x, y + 1)
        if (y + 1 < MazeManager.Instance.Height)
        {
            var neigbhour = MazeManager.Instance.GetCell(x, y + 1);
            if (!neigbhour.IsVisited)
                neighbours.Add(MazeManager.Instance.GetCell(x, y + 1));
        }
        //Right Neighbour (x +1, y)
        if (x + 1 < MazeManager.Instance.Width)
        {
            var neigbhour = MazeManager.Instance.GetCell(x + 1, y);
            if (!neigbhour.IsVisited)
                neighbours.Add(MazeManager.Instance.GetCell(x + 1, y));
        }
        //Bottom Neighbour (x, y - 1)
        if (y - 1 >= 0)
        {
            var neigbhour = MazeManager.Instance.GetCell(x, y - 1);
            if (!neigbhour.IsVisited)
                neighbours.Add(MazeManager.Instance.GetCell(x, y - 1));
        }
        //Left Neighbour (x - 1, y)
        if (x - 1 >= 0)
        {
            var neigbhour = MazeManager.Instance.GetCell(x - 1, y);
            if (!neigbhour.IsVisited)
                neighbours.Add(MazeManager.Instance.GetCell(x - 1, y));
        }

        if (neighbours.Count > 0)
        {
            int i = UnityEngine.Random.Range(0, neighbours.Count);
            return neighbours[i];
        }
        return null;
    }
    #endregion

}
