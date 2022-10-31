using UnityEngine;

public class MazeUIToggler : MonoBehaviour
{
    [SerializeField] private GameObject MazeUIPanel;
    private bool isVisible = true;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
            ToggleVisibility();
    }

    public void ToggleVisibility()
    {
        if (isVisible)
        {
            MazeUIPanel.SetActive(false);
            isVisible = false;
        }
        else
        {
            MazeUIPanel.SetActive(true);
            isVisible = true;
        }
    }
}
