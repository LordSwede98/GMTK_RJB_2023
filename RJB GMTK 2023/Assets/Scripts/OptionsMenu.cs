using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OptionsMenu : MonoBehaviour
{
    public void SetResolution480()
    {
        Screen.SetResolution(640, 480, Screen.fullScreenMode);
    }

    public void SetResolution720()
    {
        Screen.SetResolution(1280, 720, Screen.fullScreenMode);
    }

    public void SetResolution1080()
    {
        Screen.SetResolution(1920, 1080, Screen.fullScreenMode);
    }

    public void SetWindowed()
    {
        Screen.fullScreenMode = FullScreenMode.Windowed;
    }

    public void SetFullscreen()
    {
        Screen.fullScreenMode = FullScreenMode.ExclusiveFullScreen;
    }
}
