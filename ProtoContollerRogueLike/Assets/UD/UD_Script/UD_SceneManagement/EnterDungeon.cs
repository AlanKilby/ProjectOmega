using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnterDungeon : MonoBehaviour
{
    UI_FloorIndicator FI;
    FadeSceneManagerScript FSMS;

    private void Start()
    {
        FSMS = GameObject.Find("FadeManager").GetComponent<FadeSceneManagerScript>();
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Player") && Input.GetKeyDown(KeyCode.E))
        {
            FI = GameObject.Find("FloorIndicatorText").GetComponent<UI_FloorIndicator>();
            FI.UpdateIndicator();

            FindObjectOfType<AudioManager>().Play("Teleport Out");
            FindObjectOfType<AudioManager>().StopPlaying("Hub Music");
            FindObjectOfType<AudioManager>().StopPlaying("Hub Ambient");
            FindObjectOfType<AudioManager>().Play("Fight Music");

            //SceneManager.LoadScene("ProceduralGeneration");
            FadeSceneManagerScript.whatTransition = SceneTransition.Donjon;
            FSMS.FadeOut();
        }
    }
}
