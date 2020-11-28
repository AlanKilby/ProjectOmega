using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConsumablePlatform : MonoBehaviour
{
    [Header("Unlimited Ammo")]
    public float unlimitedAmmoTimeSet;
    [HideInInspector] public float unlimitedAmmoTimer;
    [HideInInspector] public bool hasUnlimitedAmmo;

    [Header("DoublePoint")]
    public float doublePointTimeSet;
    [HideInInspector] public float doublePointTimer;
    public int doublePointMultiplicatorSet;
    [HideInInspector] public int doublePointCurrentMultiplicator;
    [HideInInspector] public bool hasDoublePoint;



    void Start()
    {
        hasUnlimitedAmmo = false;
        hasDoublePoint = false;
        doublePointCurrentMultiplicator = 1;
    }

    void Update()
    {
        UnlimitedAmmoTimer();
        DoublePointTimer();
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

    private void DoublePointTimer()
    {
        if (hasDoublePoint)
        {
            doublePointTimer -= Time.deltaTime;
            if (doublePointTimer <= 0.0f)
            {
                doublePointCurrentMultiplicator = 1;
                hasDoublePoint = false;
            }
        }
    }

    public void StartUnlimitedAmmo()
    {
        unlimitedAmmoTimer = unlimitedAmmoTimeSet;
        hasUnlimitedAmmo = true;
    }
    
    public void StartDoublePoint()
    {
        doublePointCurrentMultiplicator = doublePointMultiplicatorSet;
        doublePointTimer = doublePointTimeSet;
        hasDoublePoint = true;
    }
}
