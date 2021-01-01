using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradePlatform : MonoBehaviour
{
    PlayerHealth PH;
    SwordAttack SA;
    PlayerMouvement PM;
    CurrencySysteme CS;

    [Header("Upgrade Health Total")]
    public float healthTotalUpgradeByTier;
    public float healthTotalUpgradeCountLimit;
    public float healthTotalUpgradeCountCurrent;
    [HideInInspector] public int healthTotalUpgradeCost;
    public int healthTotalUpgradeCostBase;
    public float healthTotalUpgradeCostMultiplicatorByTier;

    [Header("Upgrade Dash Cooldown")]
    public float dashCooldownUpgradeByTier;
    public float dashCooldownUpgradeCountLimit;
    public float dashCooldownUpgradeCountCurrent;
    [HideInInspector] public int dashCooldownUpgradeCost;
    public int dashCooldownUpgradeCostBase;
    public float dashCooldownUpgradeCostMultiplicatorByTier;

    [Header("Upgrade Sword Damage")]
    public int swordDamageUpgradeByTier;
    public float swordDamageUpgradeCountLimit;
    public float swordDamageUpgradeCountCurrent;
    [HideInInspector] public int swordDamageUpgradeCost;
    public int swordDamageUpgradeCostBase;
    public float swordDamageUpgradeCostMultiplicatorByTier;

    [Header("Upgrade Ammo Drop")]
    public float ammoMultiplicatorByTier;
    public float ammoMultiplicatorCountLimit;
    public float ammoMultiplicatorCountCurrent;
    [HideInInspector] public int ammoMultiplicatorCost;
    public int ammoMultiplicatorCostBase;
    public float ammoMultiplicatorCostMultiplicatorByTier;
    [HideInInspector] public float ammoMultiplicatorCurrent;

    void Start()
    {
        PH = GetComponent<PlayerHealth>();
        SA = GetComponent<SwordAttack>();
        PM = GetComponent<PlayerMouvement>();
        CS = GetComponent<CurrencySysteme>();
        healthTotalUpgradeCountCurrent = 0.0f;
        dashCooldownUpgradeCountCurrent = 0.0f;
        swordDamageUpgradeCountCurrent = 0.0f;
        ammoMultiplicatorCountCurrent = 0.0f;
        healthTotalUpgradeCost = healthTotalUpgradeCostBase;
        dashCooldownUpgradeCost = dashCooldownUpgradeCostBase;
        swordDamageUpgradeCost = swordDamageUpgradeCostBase;
        ammoMultiplicatorCost = ammoMultiplicatorCostBase;
        ammoMultiplicatorCurrent = 1.0f;
    }

    
    public void UpgradeTotalHealth()
    {
        if(healthTotalUpgradeCountCurrent < healthTotalUpgradeCountLimit && CS!=null)
        {
            CurrencySysteme.currentMoneyAmount = CurrencySysteme.currentMoneyAmount - healthTotalUpgradeCost;
            PH.totalPlayerHealthUpgraded = PH.totalPlayerHealthUpgraded + healthTotalUpgradeByTier;
            PH.currentPlayerHealth = PH.totalPlayerHealthUpgraded;
            healthTotalUpgradeCountCurrent ++;
            float healthTotalUpgradeCostCalculate = healthTotalUpgradeCost * healthTotalUpgradeCostMultiplicatorByTier;
            healthTotalUpgradeCost = (int)healthTotalUpgradeCostCalculate;
        }
    }
    public void UpgradeDashCoolDown()
    {
        if (dashCooldownUpgradeCountCurrent < dashCooldownUpgradeCountLimit && CS != null)
        {
            CurrencySysteme.currentMoneyAmount -= dashCooldownUpgradeCost;
            PM.dashReloadTimeUpgraded -= dashCooldownUpgradeByTier;
            dashCooldownUpgradeCountCurrent++;
            float dashCooldownUpgradeCostCalculate = dashCooldownUpgradeCost * dashCooldownUpgradeCostMultiplicatorByTier;
            dashCooldownUpgradeCost = (int)dashCooldownUpgradeCostCalculate;
        }
    }
    public void UpgradeSwordDamage()
    {
        if (swordDamageUpgradeCountCurrent < swordDamageUpgradeCountLimit && CS != null)
        {
            CurrencySysteme.currentMoneyAmount -= swordDamageUpgradeCost;
            SA.damageUpgraded += swordDamageUpgradeByTier;
            swordDamageUpgradeCountCurrent++;
            float swordDamageUpgradeCostCalculate = swordDamageUpgradeCost * swordDamageUpgradeCostMultiplicatorByTier;
            swordDamageUpgradeCost = (int)swordDamageUpgradeCostCalculate;
        }
    }
    
    public void UpgradeAmmoMultiplicator()
    {
        if (ammoMultiplicatorCountCurrent < ammoMultiplicatorCountLimit && CS != null)
        {
            CurrencySysteme.currentMoneyAmount -= ammoMultiplicatorCost;
            ammoMultiplicatorCurrent = ammoMultiplicatorCurrent * ammoMultiplicatorByTier;
            ammoMultiplicatorCountCurrent++;
            float ammoMultiplicatorCostCalculate = ammoMultiplicatorCost * ammoMultiplicatorCostMultiplicatorByTier;
            ammoMultiplicatorCost = (int)ammoMultiplicatorCostCalculate;
        }
    }
}
