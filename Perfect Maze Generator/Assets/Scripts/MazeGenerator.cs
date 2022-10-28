using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MazeGenerator : MonoBehaviour
{
    private ICell currentCell;

    private Stack<ICell> stack;

    private IGridGenerator gridGenerator;
    private IWallRemover wallRemover;


    private void Awake()
    {
        gridGenerator = GetComponent<IGridGenerator>();
        wallRemover = GetComponent<IWallRemover>();
    }
    // Start is called before the first frame update
    void Start()
    {
        MazeManager.Instance.OnEmptyMazeSet += GenerateMaze;
        gridGenerator.GenerateEmptyGrid(MazeManager.Instance.Width, MazeManager.Instance.Height);
        stack = new Stack<ICell>();
    }

    private void GenerateMaze()
    {
         StartCoroutine(DepthFirstSearchAlgorithm());
    }

    private IEnumerator DepthFirstSearchAlgorithm()
    {
        currentCell = MazeManager.Instance.GetCell(Random.Range(0, MazeManager.Instance.Width), Random.Range(0, MazeManager.Instance.Height));
        currentCell.IsVisited = true;
        stack.Push(currentCell);
        while (stack.Count > 0)
        {
            currentCell = stack.Pop();
            var nextCell = currentCell.GetRandomUnvisitedNeighbour();
            if (nextCell != null)
            {
                stack.Push(currentCell);
                wallRemover.RemoveWalls(currentCell, nextCell);
                nextCell.IsVisited = true;
                stack.Push(nextCell);
                yield return new WaitForSeconds(0.1f);
            }
        }
        yield return new WaitForSeconds(0);
    }
}
