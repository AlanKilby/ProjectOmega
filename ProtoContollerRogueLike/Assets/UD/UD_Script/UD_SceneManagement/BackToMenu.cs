using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BackToMenu : MonoBehaviour
{
    private void Start()
    {
        CursorManager.SetMenuCursor();
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            SceneManager.LoadScene("Menu");
            FindObjectOfType<AudioManager>().StopPlaying("Fight Music");
            FindObjectOfType<AudioManager>().StopPlaying("Hub Music");
            FindObjectOfType<AudioManager>().StopPlaying("Boss Music");
        }
    }
}
