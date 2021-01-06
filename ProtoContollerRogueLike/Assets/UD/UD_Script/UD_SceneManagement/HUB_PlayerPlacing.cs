using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HUB_PlayerPlacing : MonoBehaviour
{
    GameObject player;

    public Vector3 startPosition;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        player.transform.position = startPosition;
        FindObjectOfType<AudioManager>().Play("Hub Music");
    }

}
