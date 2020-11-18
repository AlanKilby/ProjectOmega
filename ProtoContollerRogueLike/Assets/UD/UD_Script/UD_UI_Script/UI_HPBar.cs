using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_HPBar : MonoBehaviour
{
    Image HealthBar;

    PlayerHealth PH;

    void Start()
    {
        PH = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerHealth>();
        HealthBar = GetComponent<Image>();
    }

    void Update()
    {
        HealthBar.fillAmount = PH.healthPercent;
    }
}
