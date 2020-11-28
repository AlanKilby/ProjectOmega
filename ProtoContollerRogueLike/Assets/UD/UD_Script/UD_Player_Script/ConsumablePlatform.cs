using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConsumablePlatform : MonoBehaviour
{
    // Unlimited Ammo Consumable
    [HideInInspector] public bool hasUnlimitedAmmo;
    public float unlimitedAmmoTimeSet;
    [HideInInspector] public float unlimitedAmmoTimer;


    void Start()
    {
        hasUnlimitedAmmo = false;
    }

    void Update()
    {
        UnlimitedAmmoTimer();
    }

    private void UnlimitedAmmoTimer()
    {
        if (hasUnlimitedAmmo)
        {
            unlimitedAmmoTimer -= Time.deltaTime;
            if (unlimitedAmmoTimer <= 0.0f)
            {
                hasUnlimitedAmmo = false;
            }
        }
    }

    public void StartUnlimitedAmmo()
    {
        unlimitedAmmoTimer = unlimitedAmmoTimeSet;
        hasUnlimitedAmmo = true;
    }
}
