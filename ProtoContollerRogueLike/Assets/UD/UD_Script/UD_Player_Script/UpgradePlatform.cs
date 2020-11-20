using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradePlatform : MonoBehaviour
{
    PlayerHealth PH;
    SwordAttack SA;
    PlayerMouvement PM;

    public float healthTotalUpgradeByTier;
    public float healthTotalUpgradeCountLimit;
    public float healthTotalUpgradeCountCurrent;
    public float healthTotalUpgradeCost;
    public float healthTotalUpgradeCostMultiplicatorByTier;
    
    public float dashCooldownUpgradeByTier;
    public float dashCooldownUpgradeCountLimit;
    public float dashCooldownUpgradeCountCurrent;
    public float dashCooldownUpgradeCost;
    public float dashCooldownUpgradeCostMultiplicatorByTier;

    public int swordDamageUpgradeByTier;
    public float swordDamageUpgradeCountLimit;
    public float swordDamageUpgradeCountCurrent;
    public float swordDamageUpgradeCost;
    public float swordDamageUpgradeCostMultiplicatorByTier;

    void Start()
    {
        PH = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerHealth>();
        SA = GameObject.FindGameObjectWithTag("Player").GetComponent<SwordAttack>();
        PM = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMouvement>();
        healthTotalUpgradeCountCurrent = 0.0f;
        dashCooldownUpgradeCountCurrent = 0.0f;
        swordDamageUpgradeCountCurrent = 0.0f;
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
}
