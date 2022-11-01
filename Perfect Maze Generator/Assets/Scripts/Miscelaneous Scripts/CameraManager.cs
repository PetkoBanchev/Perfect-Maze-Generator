using UnityEngine;

public class CameraManager : MonoBehaviour
{
    [SerializeField] private Camera topDownCamera;
    private void Start()
    {
        MazeManager.Instance.OnEmptyMazeSet += MoveCameraToCentreOfMaze;
    }

    /// <summary>
    /// Crude way to centre a topdown orthographic camera on the maze.
    /// A more elegant solution must be implemented in the future.
    /// </summary>
    private void MoveCameraToCentreOfMaze()
    {
        var halfWidth = (MazeManager.Instance.Width * MazeManager.Instance.CellWidth / 2) - 1;
        var halfHeight = (MazeManager.Instance.Height * MazeManager.Instance.CellWidth / 2) -1;
        topDownCamera.transform.position = new Vector3(halfWidth, 5, halfHeight);

        if (halfHeight >= halfWidth)
            topDownCamera.orthographicSize = (MazeManager.Instance.Height * MazeManager.Instance.CellWidth) / 2 + 1;
        else
            topDownCamera.orthographicSize = (MazeManager.Instance.Width * MazeManager.Instance.CellWidth) / 2 + 1;

    }
}
