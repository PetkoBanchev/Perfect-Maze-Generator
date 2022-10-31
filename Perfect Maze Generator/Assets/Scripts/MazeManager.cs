using System;
using Unity.VisualScripting;
using UnityEngine;

public class MazeManager : MonoBehaviour
{
    #region Private varialbes
    private static MazeManager instance;

    [SerializeField, Range(10, 250)] int width = 10;
    [SerializeField, Range(10, 250)] int height = 10;
    [SerializeField, Range(1f, 5f)] float cellWidth = 1;
    [SerializeField] private bool isGenerationAnimated;

    private ICell[,] maze;
    private IGridGenerator gridGenerator;
    #endregion

    #region Public variables
    public event Action OnEmptyMazeSet;
    public GameObject mazeHolder;
    #endregion

    #region Private methods
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
    private void DestroyMaze()
    {
        Destroy(mazeHolder);
        mazeHolder = new GameObject("Maze Holder");
    }
    #endregion

    #region Public properties
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

    public float CellWidth
    {
        get { return cellWidth; }
        set { cellWidth = value; }
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
    #endregion

    #region Public methods
    public void CreateMaze()
    {
        DestroyMaze();
        gridGenerator.GenerateEmptyGrid(width, height);
    }
    #endregion
}
