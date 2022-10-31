using System;
using System.Collections;
using UnityEngine;

public class SquareGridGenerator : MonoBehaviour, IGridGenerator
{
    [SerializeField] GameObject cellPrefab;

    private ICell[,] grid;

    public event Action<ICell[,]> OnEmptyGridGenerated;
    public void GenerateEmptyGrid(int width, int height)
    {
        StartCoroutine(InstantiateGrid(width, height));
    }

    private IEnumerator InstantiateGrid(int width, int height)
    {
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
}

