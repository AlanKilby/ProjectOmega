﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConsummableHealthPack : MonoBehaviour
{
    PlayerHealth PH;
    public float healthRegenerate;

    private void Start()
    {
        PH = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerHealth>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player") == true)
        {
            if (PH.currentPlayerHealth < PH.totalPlayerHealth - healthRegenerate)
            {
                PH.currentPlayerHealth = PH.currentPlayerHealth + healthRegenerate;
                Destroy(gameObject);
            }
            else
            {
                PH.currentPlayerHealth = PH.totalPlayerHealth;
                Destroy(gameObject);
            }
        }
    }
}