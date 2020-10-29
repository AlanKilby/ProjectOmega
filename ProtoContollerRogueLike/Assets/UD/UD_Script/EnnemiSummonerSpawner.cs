using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnnemiSummonerSpawner : MonoBehaviour
{
    [SerializeField] EnnemiSummoner ES;

    public Transform currentRoom;
    public RoomTriggerCollider isPlayerInRoom;
    public float spawnChance;

    private bool spawnZoneAllowed;

    private void Start()
    {
        currentRoom = gameObject.transform.parent;
        isPlayerInRoom = currentRoom.GetComponentInParent<RoomTriggerCollider>();
    }

    public void SpawnEnemy()
    {
        Debug.Log("SpawnedMinions");
        Instantiate(ES.minionsPrefab, new Vector3(gameObject.transform.position.x, gameObject.transform.position.y, 0), Quaternion.identity, currentRoom.transform);
        //GameObject minion = Instantiate(ES.minionsPrefab, new Vector3(gameObject.transform.position.x, gameObject.transform.position.y, 0), Quaternion.identity, currentRoom.transform);
        //minion.transform.SetParent(currentRoom);
        /*int i;

        i = Random.Range(1, 101);

        if (i <= spawnChance && i >= 1 && spawnZoneAllowed)
        {
            Debug.Log("SpawnedMinions");
            Instantiate(ES.minionsPrefab, new Vector3(gameObject.transform.position.x, gameObject.transform.position.y, 0), Quaternion.identity, currentRoom.transform);
        }*/
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Environement"))
        {
            spawnZoneAllowed = true;
        }
    }
    
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Environement"))
        {
            spawnZoneAllowed = false;
        }
    }
}
