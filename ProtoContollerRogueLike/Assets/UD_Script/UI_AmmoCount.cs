using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_AmmoCount : MonoBehaviour
{
    [SerializeField] Shooting Sh;

    Text ammoCount; 

    void Start()
    {
        ammoCount = GetComponent<Text>();
    }

    void Update()
    {
        if(Sh != null)
        {
            if (Sh.gatlingEquiped)
            {
                ammoCount.text = Sh.gatlingCurrentAmmo.ToString();
            }
            
            if (Sh.shootgunEquiped)
            {
                ammoCount.text = Sh.shootgunCurrentAmmo.ToString();
            }

            if (Sh.pistolEquiped)
            {
                ammoCount.text = "Infinite Ammo !";
            }
        }
    }
}
