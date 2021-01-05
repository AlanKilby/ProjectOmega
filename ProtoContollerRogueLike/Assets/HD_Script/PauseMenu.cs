﻿using System.Collections;
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
        pause = false;
        player = GameObject.FindGameObjectWithTag("Player");
        UI = GameObject.Find("UI");
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && !GameManagement.GameIsLoading)
        {
            if (pause)
            {
                Resume();
            }
            else
            {
                if (!GameManagement.GameIsPaused)
                {
                    Pause();
                }
            }
            /*if (GameManagement.GameIsPaused)
            {
                if (pause)
                {
                    Resume();
                }                
            }                
            else
            {                
                Pause();
            }*/
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
        Destroy(GameObject.Find("DifficultyPanel"));
        Destroy(GameObject.Find("GameManager"));
        //Destroy l'audio manager
        Time.timeScale = 1f;
        SceneManager.LoadScene("Menu");
    }

    public void QuitGame() 
    {
        Debug.Log("Quitting game...");
        Application.Quit();
    }

}


       