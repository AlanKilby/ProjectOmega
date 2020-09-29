using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Module : MonoBehaviour
{
    public float fireRateMultiplier;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("pré test");
        if (collision.gameObject.CompareTag("Player") == true)
        {
            Debug.Log("post test");
            collision.GetComponent<Shooting>().UpgradeRateOfFire(fireRateMultiplier);
            Destroy(gameObject);
        }
    }
}
