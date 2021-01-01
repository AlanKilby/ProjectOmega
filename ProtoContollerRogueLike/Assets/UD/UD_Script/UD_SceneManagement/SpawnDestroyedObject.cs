using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnDestroyedObject : MonoBehaviour
{
    [SerializeField] GameObject[] objectToSpawn;

    [SerializeField] Transform spawnForPlayer;

    void Start()
    {
        for(int i = 0; i < objectToSpawn.Length; i++)
        {
            GameObject spawnedObject = Instantiate(objectToSpawn[i]);
            if (spawnedObject.CompareTag("Player"))
            {
                spawnedObject.transform.position = spawnForPlayer.position;
            }
        }
    }

    void Update()
    {
        
    }
}
