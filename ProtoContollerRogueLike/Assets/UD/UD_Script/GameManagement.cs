using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManagement : MonoBehaviour
{
    public static bool GameIsPaused = false;
    public static bool GameIsLoading = false;

    void Start()
    {
        
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            GameIsPaused = !GameIsPaused;
        }
    }
}
