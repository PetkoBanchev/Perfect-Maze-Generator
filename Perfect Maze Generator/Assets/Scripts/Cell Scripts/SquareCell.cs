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
    private List<ICell> neighbours = new List<ICell>();
    private bool areNeighboursCached = false;
    #endregion

    #region Private methods
    /// <summary>
    /// Caches all of the unvisited neighbours adjacent to the cell
    /// </summary>
    private void CacheUnvisitedNeighbours()
    {
        //Top Neighbour (x, y + 1)
        if (y + 1 < MazeManager.Instance.Height)
        {
            var neigbhour = MazeManager.Instance.GetCell(x, y + 1);
            if (!neigbhour.IsVisited)
                neighbours.Add(neigbhour);
        }
        //Right Neighbour (x +1, y)
        if (x + 1 < MazeManager.Instance.Width)
        {
            var neigbhour = MazeManager.Instance.GetCell(x + 1, y);
            if (!neigbhour.IsVisited)
                neighbours.Add(neigbhour);
        }
        //Bottom Neighbour (x, y - 1)
        if (y - 1 >= 0)
        {
            var neigbhour = MazeManager.Instance.GetCell(x, y - 1);
            if (!neigbhour.IsVisited)
                neighbours.Add(neigbhour);
        }
        //Left Neighbour (x - 1, y)
        if (x - 1 >= 0)
        {
            var neigbhour = MazeManager.Instance.GetCell(x - 1, y);
            if (!neigbhour.IsVisited)
                neighbours.Add(neigbhour);
        }
        areNeighboursCached = true;
    }
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
        set 
        { 
            isVisited = value; 
            if(MazeManager.Instance.IsGenerationAnimated)
                SetColor(Color.green); 
        }
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
    /// <summary>
    /// Caches the neighbours once. Iterates through all of the cached neighbours.
    /// Removes any visited neighbours.
    /// Returns a random unvisited neighbour from the remaining ones.
    /// FUTURE IPROVEMENT: refactor and use an abstract base class to reduce code duplication
    /// </summary>
    /// <returns></returns>
    public ICell GetRandomUnvisitedNeighbour()
    {
        if (!areNeighboursCached)
            CacheUnvisitedNeighbours();

        if (neighbours.Count > 0)
        {
            // Remove all visited neigbours and return null if there are no unvisited neighbours remaining
            for (int i = 0; i < neighbours.Count; i++)
            {
                if (neighbours[i].IsVisited)
                {
                    neighbours.RemoveAt(i);
                    i--; // accounts for the removed element and the subsequent index shift of the remaining elements
                }
                if (neighbours.Count <= 0)
                    return null;
            }

            // Return a random neigbhour. It is safe to not check if the randomNeighbour has been visited, since all visited neighbours are removed above.
            int randomIndex = Random.Range(0, neighbours.Count);
            return neighbours[randomIndex];

        }
        return null;
    }
    #endregion

}
