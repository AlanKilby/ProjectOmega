﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnterDungeon : MonoBehaviour
{
    private void OnTriggerStay2D(Collider2D other)
    {
        Debug.Log("can escape");
        if (other.CompareTag("Player") && Input.GetKeyDown(KeyCode.E))
        {
            FindObjectOfType<AudioManager>().Play("Teleport Out");
            FindObjectOfType<AudioManager>().StopPlaying("Hub Music");
            FindObjectOfType<AudioManager>().StopPlaying("Hub Ambient");
            FindObjectOfType<AudioManager>().Play("Fight Music");
            
            SceneManager.LoadScene("ProceduralGeneration");
        }
    }
}
