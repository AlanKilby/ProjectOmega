using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UD_TentacleDealDamage : MonoBehaviour
{
    public int tentacleDamageToPlayer;

    bool canDealDamage;

    public float damageTimeSet;
    float damageTimer;

    private void Start()
    {
        canDealDamage = true;
    }

    private void Update()
    {
        if (!canDealDamage)
        {
            damageTimer -= Time.deltaTime;
            if(damageTimer <= 0.0f)
            {
                canDealDamage = true;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && canDealDamage)
        {
            collision.GetComponent<PlayerHealth>().TakeDamage(tentacleDamageToPlayer);
            damageTimer = damageTimeSet;
            canDealDamage = false;
        }
    }
}
