using System;
using System.Collections;
using UnityEngine;

public class SquareGridGenerator : MonoBehaviour, IGridGenerator
{
    [SerializeField] GameObject cellPrefab;
    [SerializeField] private int cellWidth;

    private ICell[,] grid;

    public event Action<ICell[,]> OnEmptyGridGenerated;
    public void GenerateEmptyGrid(int width, int height)
    {
        StartCoroutine(InstantiateGrid(width, height));
    }

    private IEnumerator InstantiateGrid(int width, int height)
    {
        grid = new ICell[width, height];
        for (int y = 0; y < height; y++)
        {
            for (int x = 0; x < width; x++)
            {
                var cell = Instantiate(cellPrefab, new Vector3(x * cellWidth, 0, y * cellWidth), Quaternion.identity);
                cell.transform.localScale = new Vector3(cellWidth, 0.1f, cellWidth);
                cell.transform.SetParent(MazeManager.Instance.mazeHolder.transform);
                grid[x, y] = cell.GetComponent<ICell>();
                grid[x, y].X = x;
                grid[x, y].Y = y;
            }
        }
        yield return new WaitForSeconds(0);
        OnEmptyGridGenerated?.Invoke(grid);
    }

}

