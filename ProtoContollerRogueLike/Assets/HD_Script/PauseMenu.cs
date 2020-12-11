using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class PauseMenu : MonoBehaviour
{
    public bool pause;
    public GameObject pauseMenuUI;

    GameObject player;
    GameObject UI;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        UI = GameObject.Find("UI");
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && !GameManagement.GameIsLoading)
        {

            if (GameManagement.GameIsPaused)
            {
                
                Resume();
            }
                
            else
            {
                
                Pause();
            }
        }
    }

    public void Resume()
    {
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        GameManagement.GameIsPaused = false;
        pause = false;
        
    }
    void Pause()
    {
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        GameManagement.GameIsPaused = true;
        pause = true;
    } 

    public void LoadMenu() 
    {
        Destroy(player);
        Destroy(UI);
        Time.timeScale = 1f;
        SceneManager.LoadScene("Menu");
    }

    public void QuitGame() 
    {
        Debug.Log("Quitting game...");
        Application.Quit();
    }

}


       