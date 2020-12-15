using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnnemiSummonerSpawner : MonoBehaviour
{
    public LayerMask whatIsEnvironement;
    [SerializeField] EnnemiSummoner ES;

    public Transform currentRoom;
    public CameraSystem isPlayerInRoom;
    public float spawnChance;
    [SerializeField] private float spawnZoneRadius;

    [SerializeField] private bool spawnZoneAllowed;

    //[SerializeField] GameObject minionsPlayground;
    private void Start()
    {
        spawnZoneAllowed = true;
        currentRoom = gameObject.transform.parent;
        isPlayerInRoom = currentRoom.GetComponentInParent<CameraSystem>();
    }

    public void SpawnEnemy()
    {
        //Instantiate(ES.minionsPrefab, new Vector3(gameObject.transform.position.x, gameObject.transform.position.y, 0), Quaternion.identity, currentRoom.transform);
        if (spawnZoneAllowed)
        {
            GameObject minion = Instantiate(ES.minionsPrefab, new Vector3(gameObject.transform.position.x, gameObject.transform.position.y, 0), Quaternion.identity, currentRoom.transform);
            minion.transform.SetParent(isPlayerInRoom.transform);
        }
        /* POUR AJOUTER UNE CHANCE DE FAIRE SPAWNER
        int i;

        i = Random.Range(1, 101);

        if (i <= spawnChance && i >= 1 && spawnZoneAllowed)
        {
            Debug.Log("SpawnedMinions");
            Instantiate(ES.minionsPrefab, new Vector3(gameObject.transform.position.x, gameObject.transform.position.y, 0), Quaternion.identity, currentRoom.transform);
        }*/
    }

    /*public void CheckSpawnZone()
    {
        Collider2D hitInfo = Physics2D.OverlapCircle(gameObject.transform.position, spawnZoneRadius, whatIsEnvironement);
        if (hitInfo != null)
        {
            if (!hitInfo.CompareTag("Environement"))
            {
                SpawnEnemy();
            }
        }
    }*/

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Environement")
        {
            spawnZoneAllowed = false;
        }
    }
    
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.tag == "Environement")
        {
            spawnZoneAllowed = true;
        }
    }
}
