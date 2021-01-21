using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HUB_PlayerPlacing : MonoBehaviour
{
    GameObject player;
    PlayerHealth PH;
    UI_FloorIndicator FI;

    public Vector3 startPosition;

    void Start()
    {
        FindObjectOfType<AudioManager>().StopPlaying("Boss Music"); 
        FindObjectOfType<AudioManager>().Play("Hub Music");
        player = GameObject.FindGameObjectWithTag("Player");
        PH = player.GetComponent<PlayerHealth>();
        PH.currentPlayerHealth = PH.totalPlayerHealthUpgraded;
        player.transform.position = startPosition;
        FI = GameObject.Find("FloorIndicatorText").GetComponent<UI_FloorIndicator>();
        FI.IndicatorHUB();
        
    }

}
