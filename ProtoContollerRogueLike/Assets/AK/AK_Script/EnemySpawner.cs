using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public Transform currentRoom;
    public EnemyList enemyList;
    public RoomTriggerCollider isPlayerInRoom;
    public float spawnChance;
    public float waitTime;

    private void Start()
    {
        currentRoom = gameObject.transform.parent;
        isPlayerInRoom = currentRoom.GetComponentInParent<RoomTriggerCollider>();
        enemyList = GameObject.FindGameObjectWithTag("Enemy List").GetComponent<EnemyList>();
        Invoke("SpawnEnemy", waitTime);
    }

    private void SpawnEnemy()
    {
        int i;

        i = Random.Range(1, 101);

        if (i <= spawnChance && i >= 1)
        {
            int j = Random.Range(0, enemyList.enemies.Length);
            //Debug.Log("Spawned");
            Instantiate(enemyList.enemies[j], new Vector3(gameObject.transform.position.x, gameObject.transform.position.y, 0), Quaternion.identity, currentRoom.transform);
        }
    }
}
