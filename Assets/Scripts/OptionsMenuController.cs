using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using System.Linq;

public class OptionsMenuController : MonoBehaviour
{
    void Start()
    {
        // Screen.fullScreen = false;
    }

    public void SetFullscreen(bool isFullscreen)
    {
        // Screen.fullScreen = isFullscreen;
        if (isFullscreen == false)
        {
            Screen.SetResolution(1280, 720, FullScreenMode.Windowed);
        }
        else
        {
            Screen.SetResolution(Screen.resolutions.Last().width, Screen.resolutions.Last().height, FullScreenMode.FullScreenWindow);
        }
    }
}
