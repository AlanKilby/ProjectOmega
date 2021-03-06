﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponList : MonoBehaviour
{
    public GameObject[] weapons;
    public float[] gunDropRate;

    float randomLoot;

    private void Start()
    {
        Debug.Log(gunDropRate.Length);
        for(int i = 1; i < gunDropRate.Length; i++)
        {
            gunDropRate[i] = gunDropRate[i] + gunDropRate[i - 1];
        }
    }

    public void DropGun(Transform gunParent, Transform thisObject)
    {
        randomLoot = Random.Range(0, gunDropRate[gunDropRate.Length-1]);
        Debug.Log(randomLoot);
        
        if(randomLoot < gunDropRate[0])
        {
            GameObject drop = Instantiate(weapons[0], thisObject.transform.position, Quaternion.Euler(0, 0, Random.Range(-180, 180)));
            drop.transform.SetParent(gunParent);
        }
        else
        {
            for (int i = 1; i < gunDropRate.Length; i++)
            {
                if(i != gunDropRate.Length-1 && randomLoot < gunDropRate[i] && randomLoot > gunDropRate[i - 1])
                {
                    GameObject drop = Instantiate(weapons[i], thisObject.transform.position, Quaternion.Euler(0, 0, Random.Range(-180, 180)));
                    drop.transform.SetParent(gunParent);
                    return;

                }
                else if(i == gunDropRate.Length-1 && randomLoot < gunDropRate[i])
                {
                    GameObject drop = Instantiate(weapons[i], thisObject.transform.position, Quaternion.Euler(0, 0, Random.Range(-180, 180)));
                    drop.transform.SetParent(gunParent);
                    return;
                }
            }
        }
    }

}
