using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartMenuController : MonoBehaviour
{
    public void Start()
    {
        Screen.SetResolution(Screen.resolutions.Last().width, Screen.resolutions.Last().height, FullScreenMode.FullScreenWindow);
    }

    public void StartGame()
    {
        SceneManager.LoadScene("Level1");
    }

    public void QuitGame ()
    {
        Application.Quit();
    }
}
