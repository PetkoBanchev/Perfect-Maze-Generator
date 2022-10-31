using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MazeSettingsSetter : MonoBehaviour
{
    [SerializeField] private TMP_InputField widthInput;
    [SerializeField] private TMP_InputField heightInput;
    [SerializeField] private Toggle isAnimationGeneratedInput;

    public void SetWidth()
    {
        int width = Int32.Parse(widthInput.text);
        if (width < 10)
        {
            MazeManager.Instance.Width = 10;
            widthInput.text = "10";
        }
        else if (width > 250)
        {
            MazeManager.Instance.Width = 250;
            widthInput.text = "250";
        }
        else
        {
            MazeManager.Instance.Width = width;
        }
    }

    public void SetHeight()
    {
        int height = Int32.Parse(heightInput.text);
        if (height < 10)
        {
            MazeManager.Instance.Height = 10;
            heightInput.text = "10";    
        }
        else if (height > 250)
        {
            MazeManager.Instance.Height = 250;
            heightInput.text = "250";
        }
        else
        {
            MazeManager.Instance.Height = height;
        }
    }

    public void SetIsGenerationAnimated()
    {
        bool isAnimationGenerated = isAnimationGeneratedInput.isOn;
        if (isAnimationGenerated)
            MazeManager.Instance.IsGenerationAnimated = true;
        else
            MazeManager.Instance.IsGenerationAnimated = false;
    }
}
