using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UD_EggBossSpawner : MonoBehaviour
{
    public LayerMask whatIsEnvironement;

    [SerializeField] GameObject minionsPrefab;
    public Transform currentRoom;
    public CameraSystem isPlayerInRoom;

    bool spawnZoneAllowed;

    void Start()
    {
        spawnZoneAllowed = true;
        currentRoom = gameObject.transform.parent;
        isPlayerInRoom = currentRoom.GetComponentInParent<CameraSystem>();
    }

    void Update()
    {

    }

    public void SpawnEnemies()
    {
        if (spawnZoneAllowed)
        {
            GameObject minion = Instantiate(minionsPrefab, new Vector3(gameObject.transform.position.x, gameObject.transform.position.y, 0), Quaternion.identity, currentRoom.transform);
            minion.transform.SetParent(isPlayerInRoom.transform);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Environement" || other.gameObject.tag == "BossEgg")
        {
            spawnZoneAllowed = false;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.tag == "Environement" || other.gameObject.tag == "BossEgg")
        {
            spawnZoneAllowed = true;
        }
    }
}
