using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MazeGenerator : MonoBehaviour
{
    private ICell currentCell;

    private Stack<ICell> stack;

    private IWallRemover wallRemover;


    private void Awake()
    {
        wallRemover = GetComponent<IWallRemover>();
    }
    // Start is called before the first frame update
    void Start()
    {
        MazeManager.Instance.OnEmptyMazeSet += GenerateMaze;
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
            currentCell.SetColor(Color.blue);
            var nextCell = currentCell.GetRandomUnvisitedNeighbour();
            if (nextCell != null)
            {
                stack.Push(currentCell);
                wallRemover.RemoveWalls(currentCell, nextCell);
                nextCell.IsVisited = true;
                stack.Push(nextCell);
                if(MazeManager.Instance.IsGenerationAnimated)
                    yield return new WaitForSeconds(10/(MazeManager.Instance.Width * MazeManager.Instance.Height));
            }
            currentCell.SetColor(Color.green);
        }
        yield return new WaitForSeconds(0);
    }
}
