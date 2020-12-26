using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EscapeWave : MonoBehaviour
{

    public bool waveStarts = false;

    public float timerBetweenWaves;
    private float timerHolder;

    public EnemyList enemyList;
    public Transform currentRoom;

    private void Start()
    {
        timerHolder = timerBetweenWaves;
        currentRoom = gameObject.transform.parent;
        enemyList = GameObject.FindGameObjectWithTag("Enemy List").GetComponent<EnemyList>();
    }
    void Update()
    {
        if (waveStarts)
        {
            timerBetweenWaves -= Time.deltaTime;
        }

        if(timerBetweenWaves <= 0)
        {
            SummonMonster();
            timerBetweenWaves = timerHolder;
        }
    }


    public void SummonMonster()
    {
        int j = Random.Range(0, enemyList.enemies.Length);
        Instantiate(enemyList.enemies[j], new Vector3(gameObject.transform.position.x, gameObject.transform.position.y, 0), Quaternion.identity, currentRoom.transform);
    }
}
