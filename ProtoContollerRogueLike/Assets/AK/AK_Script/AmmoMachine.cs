using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoMachine : MonoBehaviour
{
    public int ShotgunAmmoPrice;
    public int GatlingAmmoPrice;

    public int ShotgunAmmoQuantity;
    public int GatlingAmmoQuantity;

    public void shotgunAmmo()
    {
        if (CurrencySysteme.currentMoneyAmount >= ShotgunAmmoPrice)
        {
            FindObjectOfType<AudioManager>().Play("Buying");
            CurrencySysteme.currentMoneyAmount -= ShotgunAmmoPrice;
            GameObject.FindGameObjectWithTag("Player").GetComponent<Inventory>().ammoCounter[1] += ShotgunAmmoQuantity;
        }
        
    }

    public void gatlingAmmo()
    {
        if (CurrencySysteme.currentMoneyAmount >= GatlingAmmoPrice)
        {
            FindObjectOfType<AudioManager>().Play("Buying");
            CurrencySysteme.currentMoneyAmount -= GatlingAmmoPrice;
            GameObject.FindGameObjectWithTag("Player").GetComponent<Inventory>().ammoCounter[0] += GatlingAmmoQuantity;
        }
        
    }
}
