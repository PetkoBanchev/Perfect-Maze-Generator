using System;
using UnityEngine;

public class MazeManager : MonoBehaviour
{
    [SerializeField] int width = 10;
    [SerializeField] int height = 10;
    private ICell[,] maze;
    private IGridGenerator gridGenerator;
    private void Awake()
    {
        instance = this;
        gridGenerator = GetComponent<IGridGenerator>();
        gridGenerator.OnEmptyGridGenerated += SetMaze;
    }
    private void SetMaze(ICell[,] _maze)
    {
        maze = _maze;
        OnEmptyMazeSet?.Invoke();
    }
    public event Action OnEmptyMazeSet;

    private static MazeManager instance;
    public static MazeManager Instance
    {
        get
        {
            return instance;
        }
    }
    public int Width
    {
        get { return width; }
        set { width = value; }
    }
    public int Height
    {
        get { return height; }
        set { height = value; }
    }
    public ICell GetCell(int x, int y)
    {
        return maze[x, y];
    }


}
