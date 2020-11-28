using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_UnlimitedAmmo : MonoBehaviour
{
    [SerializeField] Image graph;

    ConsumablePlatform CP;

    void Start()
    {
        CP = GameObject.FindGameObjectWithTag("Player").GetComponent<ConsumablePlatform>();
        graph.gameObject.SetActive(false);
    }

    void Update()
    {
        if(CP.hasUnlimitedAmmo && CP!=null)
        {
            graph.gameObject.SetActive(true);
            float fillPercent = CP.unlimitedAmmoTimer / CP.unlimitedAmmoTimeSet;
            graph.fillAmount = fillPercent;
        }
        else
        {
            graph.gameObject.SetActive(false);
        }
    }
}
