using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_UpgradeButton : MonoBehaviour
{
    Button Bu;
    UpgradePlatform UP;
    CurrencySysteme CS;

    [Header("Choose what Upgrade is the Button")]
    [SerializeField] bool isHealthMaxUpgrade;
    [SerializeField] bool isDashCooldownUpgrade;
    [SerializeField] bool isSwordDamageUpgrade;
    [SerializeField] bool isAmmoDropUpgrade;

    void Start()
    {
        Bu = GetComponent<Button>();
        CS = GameObject.FindGameObjectWithTag("Player").GetComponent<CurrencySysteme>();
        UP = GameObject.FindGameObjectWithTag("Player").GetComponent<UpgradePlatform>();
    }

    public void ButtonClicked()
    {
        if (isHealthMaxUpgrade && CS.currentMoneyAmount>=UP.healthTotalUpgradeCost)
        {
            UP.UpgradeTotalHealth();
        }
        if (isDashCooldownUpgrade && CS.currentMoneyAmount >= UP.dashCooldownUpgradeCost)
        {
            UP.UpgradeDashCoolDown();
        }
        if (isSwordDamageUpgrade && CS.currentMoneyAmount >= UP.swordDamageUpgradeCost)
        {
            UP.UpgradeSwordDamage();
        }
        if (isAmmoDropUpgrade && CS.currentMoneyAmount >= UP.ammoMultiplicatorCost)
        {
            UP.UpgradeAmmoMultiplicator();
        }
    }
}
