using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyList : MonoBehaviour
{
    public GameObject[] enemies;
    public float[] enemyDropRate;



    private void Start()
    {
        for (int i = 1; i < enemyDropRate.Length; i++)
        {
            enemyDropRate[i] = enemyDropRate[i] + enemyDropRate[i - 1];
        }
    }

}
