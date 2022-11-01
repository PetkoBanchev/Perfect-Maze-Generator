using System;
using System.Collections;
using UnityEngine;

public class SquareGridGenerator : MonoBehaviour, IGridGenerator
{
    #region Private variables
    [SerializeField] GameObject cellPrefab;
    private CellType cellType = CellType.Square;
    private ICell[,] grid;
    #endregion

    #region Public variables
    public event Action<ICell[,]> OnEmptyGridGenerated;
    #endregion

    #region Public properties
    public CellType CellType
    {
        get { return cellType; }
    }
    #endregion

    #region Public methods
    public void GenerateEmptyGrid()
    {
        StartCoroutine(InstantiateGrid());
    }
    #endregion

    #region Private methods
    /// <summary>
    /// Creates a simple square grid using a nested loop.
    /// In the end an event is fired that passes along the grid.
    /// </summary>
    /// <returns></returns>
    private IEnumerator InstantiateGrid()
    {
        var width = MazeManager.Instance.Width;
        var height = MazeManager.Instance.Height;   
        var cellWidth = MazeManager.Instance.CellWidth;
        grid = new ICell[width, height];
        for (int y = 0; y < height; y++)
        {
            for (int x = 0; x < width; x++)
            {
                //Instantiating the cell in the scene
                var cell = Instantiate(cellPrefab, new Vector3(x * cellWidth, 0, y * cellWidth), Quaternion.identity);
                cell.transform.localScale = new Vector3(cellWidth, 0.1f, cellWidth);
                cell.transform.SetParent(MazeManager.Instance.mazeHolder.transform);

                //Caching the cell into a 2D array
                grid[x, y] = cell.GetComponent<ICell>();
                grid[x, y].X = x;
                grid[x, y].Y = y;
            }
        }
        yield return null;
        OnEmptyGridGenerated?.Invoke(grid);
    }
    #endregion
}

