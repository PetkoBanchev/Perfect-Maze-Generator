using UnityEngine;

public class CameraManager : MonoBehaviour
{
    [SerializeField] private Camera topDownCamera;
    void Start()
    {
        MazeManager.Instance.OnEmptyMazeSet += MoveCameraToCentreOfMaze;
    }

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
