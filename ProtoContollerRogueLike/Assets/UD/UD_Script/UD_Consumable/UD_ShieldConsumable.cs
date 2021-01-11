using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UD_ShieldConsumable : MonoBehaviour
{
    PlayerHealth PH;

    private void Start()
    {
        PH = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerHealth>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player") == true)
        {
            FindObjectOfType<AudioManager>().Play("Shield"); 
            PH.StartShield();
            Destroy(gameObject);
        }
    }
}
