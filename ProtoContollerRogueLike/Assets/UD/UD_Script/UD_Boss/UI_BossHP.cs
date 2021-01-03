using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_BossHP : MonoBehaviour
{
    Image hpBar;
    UD_BossBase boss;

    float hpBossMax;
    float hpBossCurrent;

    void Start()
    {
        hpBar = GetComponent<Image>();
        boss = GameObject.FindGameObjectWithTag("Boss").GetComponent<UD_BossBase>();
        hpBossMax = boss.health;
    }

    void Update()
    {
        hpBossCurrent = boss.health;
        hpBar.fillAmount = hpBossCurrent / hpBossMax;
    }
}
