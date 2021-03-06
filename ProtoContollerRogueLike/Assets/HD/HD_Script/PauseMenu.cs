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
    FadeSceneManagerScript FSMS;

    private void Start()
    {
        pause = false;
        player = GameObject.FindGameObjectWithTag("Player");
        UI = GameObject.Find("UI");
        FSMS = GameObject.Find("FadeManager").GetComponent<FadeSceneManagerScript>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && !GameManagement.GameIsLoading)
        {
            if (pause)
            {
                Resume();
                FindObjectOfType<AudioManager>().Play("Ok");
            }
            else
            {
                if (!GameManagement.GameIsPaused)
                {
                    CursorManager.SetMenuCursor();
                    Pause();
                    FindObjectOfType<AudioManager>().Play("Pause");
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
        CursorManager.SetFightCursor();
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
        FindObjectOfType<AudioManager>().StopPlaying("Hub Music");
        FindObjectOfType<AudioManager>().StopPlaying("Ambient Cave");
        FindObjectOfType<AudioManager>().StopPlaying("Boss Music");
        FindObjectOfType<AudioManager>().StopPlaying("Fight Music");
        Destroy(player);
        //Destroy(UI); Maintenant Détruit depuis le FadeSceneManager
        Destroy(GameObject.Find("DifficultyPanel"));
        Destroy(GameObject.Find("GameManager"));
        //Destroy l'audio manager
        Time.timeScale = 1f;
        //SceneManager.LoadScene("Menu");
        FadeSceneManagerScript.whatTransition = SceneTransition.MainMenu;
        FSMS.FadeOut();
    }

    public void QuitGame() 
    {
        Debug.Log("Quitting game...");
        Application.Quit();
    }

}


       