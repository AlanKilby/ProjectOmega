using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void PlayGame()
    {
        GameManagement.GameIsPaused = false;
        SceneManager.LoadScene("UD_GameIntro");
        FindObjectOfType<AudioManager>().Play("Press Play");
        //FindObjectOfType<AudioManager>().Play("Fight Music");
        FindObjectOfType<AudioManager>().Play("Ambient Cave");
    }
    
    public void QuitGame()
    {
        Debug.Log("QUIT!");
        Application.Quit();
    }
   
}
