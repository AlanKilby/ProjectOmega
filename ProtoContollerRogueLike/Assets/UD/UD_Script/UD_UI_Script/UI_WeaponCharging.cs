using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_WeaponCharging : MonoBehaviour
{
    [SerializeField] Weapon weapon = null;

    [SerializeField] Slider ChargingWeapon;
    [SerializeField] GameObject fill;

    void Start()
    {

    }

    void Update()
    {
        ChargingWeapon.value = weapon.loadingGunChargePercentage;
        if (!weapon.gunInHand)
        {
            fill.SetActive(false);
        }
        else
        {
            fill.SetActive(true);
        }
    }
}
