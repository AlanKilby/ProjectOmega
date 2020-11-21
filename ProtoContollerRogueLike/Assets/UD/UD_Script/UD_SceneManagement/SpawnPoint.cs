using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPoint : MonoBehaviour
{
    [SerializeField] GameObject spawnPlayer;
    GameObject player;

    private void Start()
    {
        spawnPlayer = GameObject.FindGameObjectWithTag("SpawnPlayer");
        player = GameObject.FindGameObjectWithTag("Player");
        player.transform.position = spawnPlayer.transform.position;
    }

}
