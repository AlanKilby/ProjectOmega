using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_LoadingScreen : MonoBehaviour
{

    private void Start()
    {
        GameManagement.GameIsPaused = true;
        GameManagement.GameIsLoading = true;
    }

    public void LaunchGame()
    {
        GameManagement.GameIsPaused = false;
        GameManagement.GameIsLoading = false;
    }
}
