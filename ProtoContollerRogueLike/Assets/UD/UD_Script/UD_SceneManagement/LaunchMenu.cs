﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LaunchMenu : MonoBehaviour
{
    public float launchMenuTime;

    private void Start()
    {
        Invoke("StartMenu", launchMenuTime);
        AudioManager.volumeSlider = 1f;
    }

    private void StartMenu()
    {
        SceneManager.LoadScene("Menu");
    }
}
