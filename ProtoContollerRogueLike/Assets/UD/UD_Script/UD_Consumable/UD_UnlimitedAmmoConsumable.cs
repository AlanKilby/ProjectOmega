using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UD_UnlimitedAmmoConsumable : MonoBehaviour
{
    ConsumablePlatform CP;

    private void Start()
    {
        CP = GameObject.FindGameObjectWithTag("Player").GetComponent<ConsumablePlatform>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player") == true)
        {
            CP.StartUnlimitedAmmo();
            Destroy(gameObject);
        }
    }
}
