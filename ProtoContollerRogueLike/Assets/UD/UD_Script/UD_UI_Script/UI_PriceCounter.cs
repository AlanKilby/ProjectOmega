using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_PriceCounter : MonoBehaviour
{
    UpgradePlatform UP;

    Text priceShowedText;

    public bool isForHPTotal;
    public bool isForDashCooldown;
    public bool isForSwordDamage;
    public bool isForAmmoDrop;

    void Start()
    {
        UP = GameObject.FindGameObjectWithTag("Player").GetComponent<UpgradePlatform>();
        priceShowedText = GetComponent<Text>();
    }


    void Update()
    {
        if (priceShowedText != null)
        {
            if (isForHPTotal)
            {
                if(UP.healthTotalUpgradeCountCurrent >= UP.healthTotalUpgradeCountLimit)
                {
                    priceShowedText.text = ("Full !");
                }
                else
                {
                    priceShowedText.text = UP.healthTotalUpgradeCost.ToString();
                }
            }
            if (isForDashCooldown)
            {
                if(UP.dashCooldownUpgradeCountCurrent >= UP.dashCooldownUpgradeCountLimit)
                {
                    priceShowedText.text = ("Full !");
                }
                else
                {
                    priceShowedText.text = UP.dashCooldownUpgradeCost.ToString();
                }
            }
            if (isForSwordDamage)
            {
                if(UP.swordDamageUpgradeCountCurrent >= UP.swordDamageUpgradeCountLimit)
                {
                    priceShowedText.text = ("Full !");
                }
                else
                {
                    priceShowedText.text = UP.swordDamageUpgradeCost.ToString();
                }
            }
            if (isForAmmoDrop)
            {
                if(UP.ammoMultiplicatorCountCurrent >= UP.ammoMultiplicatorCountLimit)
                {
                    priceShowedText.text = ("Full !");
                }
                else
                {
                    priceShowedText.text = UP.ammoMultiplicatorCost.ToString();
                }
            }
        }
    }
}
