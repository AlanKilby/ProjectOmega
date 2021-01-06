using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EscapePod : MonoBehaviour
{
    public float escapeTimer;

    private bool escapeStart = false;

    public GameObject[] escapeSpawners;

    private void OnTriggerStay2D(Collider2D other)
    {
        Debug.Log("can escape");
        if (other.CompareTag("Player") && Input.GetKeyDown(KeyCode.E))
        {
            escapeStart = true;
            FindObjectOfType<AudioManager>().StopPlaying("Fight Music"); 
            FindObjectOfType<AudioManager>().Play("Teleport In");

            for (int i = 0; i < escapeSpawners.Length; i++)
            {
                escapeSpawners[i].GetComponent<EscapeWave>().waveStarts = true;
            }
        }
    }

    private void Update()
    {
        if (escapeStart)
        {
            escapeTimer -= Time.deltaTime;
        }

        if(escapeTimer <= 0)
        {
            SceneManager.LoadScene("UD_HUBtest");
            FindObjectOfType<AudioManager>().Play("Hub Music");
            FindObjectOfType<AudioManager>().Play("Hub Ambient");
        }
    }

}
