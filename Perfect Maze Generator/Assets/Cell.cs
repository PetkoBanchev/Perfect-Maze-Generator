using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cell : MonoBehaviour
{
    public int x;
    public int y;

    public bool IsVisited = false;

    public bool[] wallsVisibility = { true, true, true, true};

    [SerializeField] GameObject[] wallObjects;

    public void RemoveWalls()
    {
        for(int i = 0; i < wallObjects.Length; i++)
        {
            if (!wallsVisibility[i])
            {
                wallObjects[i].SetActive(false);
            }
        }
    }
}
