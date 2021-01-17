using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public Transform currentRoom;
    public EnemyList enemyList;
    public CameraSystem isPlayerInRoom;
    public float spawnChance;
    public float waitTime;

    private void Start()
    {
        currentRoom = gameObject.transform.parent;
        isPlayerInRoom = currentRoom.GetComponentInParent<CameraSystem>();
        enemyList = GameObject.FindGameObjectWithTag("Enemy List").GetComponent<EnemyList>();
        Invoke("SpawnEnemy", waitTime);
    }

    private void SpawnEnemy()
    {
        float i;

        i = Random.Range(1, 100);


        

        if (i <= spawnChance && i >= 1)
        {
            float j = Random.Range(0, enemyList.enemyDropRate[enemyList.enemyDropRate.Length-1]);

            if (j <= enemyList.enemyDropRate[0])
            {
                Instantiate(enemyList.enemies[0], new Vector3(gameObject.transform.position.x, gameObject.transform.position.y, 0), Quaternion.identity, isPlayerInRoom.transform);
            }
            else
            {
                for (int k = 1; k < enemyList.enemyDropRate.Length; k++)
                {
                    if (k != enemyList.enemyDropRate.Length - 1 && j >= enemyList.enemyDropRate[k] && j <= enemyList.enemyDropRate[k + 1])
                    {
                        Instantiate(enemyList.enemies[k], new Vector3(gameObject.transform.position.x, gameObject.transform.position.y, 0), Quaternion.identity, isPlayerInRoom.transform);
                    }
                    else if (k == enemyList.enemyDropRate.Length - 1 && j >= enemyList.enemyDropRate[k])
                    {
                        Instantiate(enemyList.enemies[k], new Vector3(gameObject.transform.position.x, gameObject.transform.position.y, 0), Quaternion.identity, isPlayerInRoom.transform);
                    }
                }
            }

            
            //Debug.Log("Spawned");
            //Instantiate(enemyList.enemies[j], new Vector3(gameObject.transform.position.x, gameObject.transform.position.y, 0), Quaternion.identity, isPlayerInRoom.transform);
        }
    }
}
