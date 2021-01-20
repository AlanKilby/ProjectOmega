using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject[] enemies;
    public float[] enemyDropRate;

    public Transform currentRoom;
    public EnemyList enemyList;
    public CameraSystem isPlayerInRoom;
    public float spawnChance;
    public float waitTime;

    private void Start()
    {
        for (int i = 1; i < enemyDropRate.Length; i++)
        {
            enemyDropRate[i] = enemyDropRate[i] + enemyDropRate[i - 1];
            //Debug.Log(enemyDropRate[i]);
        }
        
        currentRoom = gameObject.transform.parent;
        isPlayerInRoom = currentRoom.GetComponentInParent<CameraSystem>();
        enemyList = GameObject.FindGameObjectWithTag("Enemy List").GetComponent<EnemyList>();
        Invoke("SpawnEnemy", waitTime);
    }

    private void SpawnEnemy()
    {
        float i = Random.Range(0,100);
        //Debug.Log(i);

        if (i <= spawnChance && i >= 1)
        {
            float j = Random.Range(0, 100);

            if (j <= enemyDropRate[0])
            {
                Instantiate(enemies[0], new Vector3(gameObject.transform.position.x, gameObject.transform.position.y, 0), Quaternion.identity, isPlayerInRoom.transform);
            }
            else
            {
                for (int k = 1; k < enemyDropRate.Length; k++)
                {
                    if ( j > enemyDropRate[k-1] && j < enemyDropRate[k])
                    {
                        Instantiate(enemies[k], new Vector3(gameObject.transform.position.x, gameObject.transform.position.y, 0), Quaternion.identity, isPlayerInRoom.transform);
                        return;
                    }
                    //else if (k == (enemyDropRate.Length - 1) && j <= enemyDropRate[k])
                    //{
                    //    Instantiate(enemies[k], new Vector3(gameObject.transform.position.x, gameObject.transform.position.y, 0), Quaternion.identity, isPlayerInRoom.transform);
                    //    return;
                    //}
                }
            }

            
            //Debug.Log("Spawned");
            //Instantiate(enemyList.enemies[j], new Vector3(gameObject.transform.position.x, gameObject.transform.position.y, 0), Quaternion.identity, isPlayerInRoom.transform);
        }
    }
}
