using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MazeGenerator : MonoBehaviour
{
    #region Private variables
    private ICell currentCell;
    private Stack<ICell> stack;
    #endregion

    #region Private methods
    private void Start()
    {
        MazeManager.Instance.OnEmptyMazeSet += GenerateMaze;
        stack = new Stack<ICell>();
    }

    private void GenerateMaze()
    {
         StartCoroutine(DepthFirstSearchAlgorithm());
    }

    /// <summary>
    /// Generates a maze using the depth-first search algorithm.
    /// For more information check https://en.wikipedia.org/wiki/Maze_generation_algorithm
    /// </summary>
    /// <returns></returns>
    private IEnumerator DepthFirstSearchAlgorithm()
    {
        var wallRemover = MazeManager.Instance.GetWallRemover(); // Caching the wall remover
        bool isAnimated = MazeManager.Instance.IsGenerationAnimated;
            currentCell = GetRandomCell();
        currentCell.IsVisited = true;
        stack.Push(currentCell);
        while (stack.Count > 0)
        {
            currentCell = stack.Pop();
            if (isAnimated)
                currentCell.SetColor(Color.blue);
            var nextCell = currentCell.GetRandomUnvisitedNeighbour();
            if (nextCell != null)
            {
                stack.Push(currentCell);
                wallRemover.RemoveWalls(currentCell, nextCell);
                nextCell.IsVisited = true;
                stack.Push(nextCell);
                if(isAnimated)
                    yield return new WaitForSeconds(10/(MazeManager.Instance.Width * MazeManager.Instance.Height));
            }
            if (isAnimated)
                currentCell.SetColor(Color.green);
        }
        yield return null;
    }
    /// <summary>
    /// Returns a random cell with two even coordinates. This is to ensure that the algorithm does not break when using hexagonal cells.
    /// The reason the hexagonal grid uses a doubled coordinate system. Each cell has only even (2,2) or odd (3,3) coordinates.
    /// This makes the function not entirely random. This should be resolved in the future.
    /// </summary>
    /// <returns></returns>
    private ICell GetRandomCell()
    {
        int x = 2 * Random.Range(0, MazeManager.Instance.Width / 2);
        int y = 2 * Random.Range(0, MazeManager.Instance.Height / 2);

        var cell = MazeManager.Instance.GetCell(x, y);
        return cell;
    }

    #endregion
}
