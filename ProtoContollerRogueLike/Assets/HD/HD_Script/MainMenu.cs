using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    private void Start()
    {
        CursorManager.SetMenuCursor();
        FindObjectOfType<AudioManager>().StopPlaying("Credits Music");
        FindObjectOfType<AudioManager>().StopPlaying("Ambient Cave");
        FindObjectOfType<AudioManager>().Play("Splashscreen Music");
    }

    public void PlayGame()
    {
        CurrencySysteme.currentMoneyAmount = 0;
        GameManagement.GameIsPaused = false;
        SceneManager.LoadScene("UD_GameIntro");
        FindObjectOfType<AudioManager>().StopPlaying("Splashscreen Music"); 
        FindObjectOfType<AudioManager>().Play("Play");
        FindObjectOfType<AudioManager>().Play("Boss Music");
        FindObjectOfType<AudioManager>().Play("Ambient Cave");
    }
    
    public void QuitGame()
    {
        Debug.Log("QUIT!");
        Application.Quit();
    }
   
}
