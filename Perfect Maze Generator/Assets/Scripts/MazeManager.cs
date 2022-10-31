using System;
using Unity.VisualScripting;
using UnityEngine;

public class MazeManager : MonoBehaviour
{   
    private static MazeManager instance;
    [SerializeField, Range(10, 250)] int width = 10;
    [SerializeField, Range(10, 250)] int height = 10;
    [SerializeField] private bool isGenerationAnimated;

    private ICell[,] maze;
    private IGridGenerator gridGenerator;

    public event Action OnEmptyMazeSet;
    public GameObject mazeHolder;
    private void Awake()
    {
        instance = this;
        gridGenerator = GetComponent<IGridGenerator>();
        gridGenerator.OnEmptyGridGenerated += SetMaze;
        mazeHolder = new GameObject("Maze Holder");
    }
    private void SetMaze(ICell[,] _maze)
    {
        maze = _maze;
        OnEmptyMazeSet?.Invoke();
    }

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

    public bool IsGenerationAnimated
    {
        get { return isGenerationAnimated; }
        set { isGenerationAnimated = value; }
    }
    
    public ICell GetCell(int x, int y)
    {
        return maze[x, y];
    }

    private void DestroyMaze()
    {
        Destroy(mazeHolder);
        mazeHolder = new GameObject("Maze Holder");
    }

    public void CreateMaze()
    {
        DestroyMaze();
        gridGenerator.GenerateEmptyGrid(width, height);
    }
}
