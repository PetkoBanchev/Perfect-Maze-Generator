using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MazeManager : MonoBehaviour
{
    [SerializeField] int width = 10;
    [SerializeField] int heigth = 10;
    [SerializeField] GameObject cellPrefab;
    private Cell[,] maze;

    private Cell currentCell;

    private Stack<Cell> stack;
    // Start is called before the first frame update
    void Start()
    {
        maze = new Cell[width, heigth];
        stack = new Stack<Cell>();
        for(int y = 0; y<heigth; y++)
        {
            for(int x = 0; x<width; x++)
            {
                var cell = Instantiate(cellPrefab, new Vector3(x, 0, y), Quaternion.identity);
                maze[x,y] = cell.GetComponent<Cell>();
                maze[x, y].x = x;
                maze[x, y].y = y;
            }
        }
        GenerateMaze();
;    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void GenerateMaze()
    {
        currentCell = maze[0, 0];
        currentCell.IsVisited = true;
        stack.Push(currentCell);
        while(stack.Count > 0)
        {
            currentCell = stack.Pop();
            var next = GetRandomUnvisitedNeighbour(currentCell.x, currentCell.y);
            if(next != null)
            {
                stack.Push(currentCell);
                RemoveWalls(currentCell, next);
                next.IsVisited = true;
                stack.Push(next);
            }
        }
    }

    private void RemoveWalls(Cell current, Cell next)
    {
        int xOffset = current.x - next.x;
        int yOffset = current.y - next.y;

        //Top Neighbour
        if(xOffset == 0 && yOffset == -1)
        {
            current.wallsVisibility[0] = false;
            next.wallsVisibility[2] = false;
        }
        //Right Neighbour
        if (xOffset == -1 && yOffset == 0)
        {
            current.wallsVisibility[1] = false;
            next.wallsVisibility[3] = false;
        }
        //Bottom Neighbour
        if (xOffset == 0 && yOffset == 1)
        {
            current.wallsVisibility[2] = false;
            next.wallsVisibility[0] = false;
        }
        //Left Neighbour
        if (xOffset == 1 && yOffset == 0)
        {
            current.wallsVisibility[3] = false;
            next.wallsVisibility[1] = false;
        }

        current.RemoveWalls();
        next.RemoveWalls();
    }

    private Cell GetRandomUnvisitedNeighbour(int x, int y)
    {
        List<Cell> neighbours = new List<Cell>();

        //Top Neighbour (x, y + 1)
        if(y + 1 < heigth && !maze[x, y + 1].IsVisited)
        {
            neighbours.Add(maze[x, y + 1]);
        }
        //Right Neighbour (x +1, y)
        if (x + 1 < width && !maze[x + 1, y].IsVisited)
        {
            neighbours.Add(maze[x + 1, y]);
        }
        //Bottom Neighbour (x, y - 1)
        if (y - 1 > 0 && !maze[x, y - 1].IsVisited)
        {
            neighbours.Add(maze[x, y - 1]);
        }
        //Left Neighbour (x - 1, y)
        if (x - 1 > 0 && !maze[x - 1, y].IsVisited)
        {
            neighbours.Add(maze[x - 1, y]);
        }

        if (neighbours.Count > 0)
        {
            int i = UnityEngine.Random.Range(0, neighbours.Count);
            return neighbours[i];
        }
        return null;
    }
}
