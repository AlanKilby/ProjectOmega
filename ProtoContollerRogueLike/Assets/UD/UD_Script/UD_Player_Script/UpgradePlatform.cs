using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradePlatform : MonoBehaviour
{
    PlayerHealth PH;
    SwordAttack SA;
    PlayerMouvement PM;

    [Header("Upgrade Health Total")]
    public float healthTotalUpgradeByTier;
    public float healthTotalUpgradeCountLimit;
    public float healthTotalUpgradeCountCurrent;
    public float healthTotalUpgradeCost;
    public float healthTotalUpgradeCostMultiplicatorByTier;

    [Header("Upgrade Dash Cooldown")]
    public float dashCooldownUpgradeByTier;
    public float dashCooldownUpgradeCountLimit;
    public float dashCooldownUpgradeCountCurrent;
    public float dashCooldownUpgradeCost;
    public float dashCooldownUpgradeCostMultiplicatorByTier;

    [Header("Upgrade Sword Damage")]
    public int swordDamageUpgradeByTier;
    public float swordDamageUpgradeCountLimit;
    public float swordDamageUpgradeCountCurrent;
    public float swordDamageUpgradeCost;
    public float swordDamageUpgradeCostMultiplicatorByTier;

    [Header("Upgrade Ammo Drop")]
    public float ammoMultiplicatorByTier;
    public float ammoMultiplicatorCountLimit;
    public float ammoMultiplicatorCountCurrent;
    public float ammoMultiplicatorCost;
    public float ammoMultiplicatorCostMultiplicatorByTier;
    [HideInInspector] public float ammoMultiplicatorCurrent;

    void Start()
    {
        PH = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerHealth>();
        SA = GameObject.FindGameObjectWithTag("Player").GetComponent<SwordAttack>();
        PM = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMouvement>();
        healthTotalUpgradeCountCurrent = 0.0f;
        dashCooldownUpgradeCountCurrent = 0.0f;
        swordDamageUpgradeCountCurrent = 0.0f;
        ammoMultiplicatorCountCurrent = 0.0f;
    }

    
    public void UpgradeTotalHealth()
    {
        if(healthTotalUpgradeCountCurrent < healthTotalUpgradeCountLimit)
        {
            PH.totalPlayerHealthUpgraded += healthTotalUpgradeByTier;
            healthTotalUpgradeCountCurrent ++;
        }
    }
    public void UpgradeDashCoolDown()
    {
        if (dashCooldownUpgradeCountCurrent < dashCooldownUpgradeCountLimit)
        {
            PM.dashReloadTimeUpgraded += dashCooldownUpgradeByTier;
            dashCooldownUpgradeCountCurrent++;
        }
    }
    public void UpgradeSwordDamage()
    {
        if (swordDamageUpgradeCountCurrent < swordDamageUpgradeCountLimit)
        {
            SA.damageUpgraded += swordDamageUpgradeByTier;
            swordDamageUpgradeCountCurrent++;
        }
    }
    
    public void UpgradeAmmoMultiplicator()
    {
        if (ammoMultiplicatorCountCurrent < ammoMultiplicatorCountLimit)
        {
            ammoMultiplicatorCurrent = ammoMultiplicatorCurrent * ammoMultiplicatorByTier;
            ammoMultiplicatorCountCurrent++;
        }
    }
}
