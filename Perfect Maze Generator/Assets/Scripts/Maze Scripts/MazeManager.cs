using System;
using System.Collections.Generic;
using UnityEngine;

public class MazeManager : MonoBehaviour
{
    #region Private varialbes
    private static MazeManager instance;

    [SerializeField, Range(10, 250)] int width = 10;
    [SerializeField, Range(10, 250)] int height = 10;
    [SerializeField, Range(1f, 5f)] float cellWidth = 1;
    [SerializeField] private bool isGenerationAnimated;
    private CellType cellType = CellType.Square;

    private ICell[,] maze;
    private Dictionary<CellType, IGridGenerator> gridGenerators;
    private Dictionary<CellType, IWallRemover> wallRemovers;
    #endregion

    #region Public variables
    public event Action OnEmptyMazeSet;
    public GameObject mazeHolder;
    #endregion

    #region Private methods
    private void Awake()
    {
        instance = this;
        GetAllGenerators();
        GetAllWallRemovers();
        gridGenerators[cellType].OnEmptyGridGenerated += SetMaze; //default implementation
        mazeHolder = new GameObject("Maze Holder");
    }
    /// <summary>
    /// Caches all of the grid generators via relfection
    /// </summary>
    private void GetAllGenerators()
    {
        gridGenerators = new Dictionary<CellType, IGridGenerator>();
        var allGridGenerators = GetComponents<IGridGenerator>();
        foreach (var generator in allGridGenerators)
            gridGenerators.Add(generator.CellType, generator);
    }
    /// <summary>
    /// Caches all of the wall removers via relfection
    /// </summary>
    private void GetAllWallRemovers()
    {
        wallRemovers = new Dictionary<CellType, IWallRemover>();
        var allWallRemovers = GetComponents<IWallRemover>();
        foreach (var wallRemover in allWallRemovers)
            wallRemovers.Add(wallRemover.CellType, wallRemover); // Unable to create a function using generics due to the CellType dependency here.
    }
 /// <summary>
 /// Recieves and caches the empty grid from the gridGenerator.
 /// Invokes an event that triggers the algorithmic generation of the maze
 /// </summary>
 /// <param name="_maze"></param>
    private void SetMaze(ICell[,] _maze)
    {
        maze = _maze;
        OnEmptyMazeSet?.Invoke();
    }
    /// <summary>
    /// Destroying the old maze by destroying the holder object.
    /// And creating a new holder object.
    /// </summary>
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

    public CellType CellType
    {
        get { return cellType; }
        set 
        {
            gridGenerators[cellType].OnEmptyGridGenerated -= SetMaze; //Unsubscribing the old gridGenerator
            cellType = value;
            gridGenerators[cellType].OnEmptyGridGenerated += SetMaze; //Subscribing the new gridGenerator
        }
    }
    
    public IWallRemover GetWallRemover()
    {
        return wallRemovers[cellType];
    }
    public ICell GetCell(int x, int y)
    {
        return maze[x, y];
    }
    #endregion

    #region Public methods
    /// <summary>
    /// It is attatched in the editor to the Generate Maze button
    /// </summary>
    public void CreateMaze()
    {
        DestroyMaze();
        gridGenerators[cellType].GenerateEmptyGrid();
    }
    #endregion
}
