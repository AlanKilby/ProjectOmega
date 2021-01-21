using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOutroEnd : MonoBehaviour
{
    private void Start()
    {
        CursorManager.SetMenuCursor();
        FindObjectOfType<AudioManager>().Play("Credits Music");
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            SceneManager.LoadScene("UD_GameCredits");
        }
    }
}
