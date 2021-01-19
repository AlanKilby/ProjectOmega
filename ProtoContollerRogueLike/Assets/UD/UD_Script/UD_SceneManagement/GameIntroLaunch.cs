using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameIntroLaunch : MonoBehaviour
{
    private void Start()
    {
        CursorManager.SetMenuCursor();
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            CursorManager.SetFightCursor();
            SceneManager.LoadScene("UD_Tuto");
        }
    }
}
