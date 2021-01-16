using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UD_TentacleDealDamage : MonoBehaviour
{
    public int tentacleDamageToPlayer;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            collision.GetComponent<PlayerHealth>().TakeDamage(tentacleDamageToPlayer);
        }
    }
}
