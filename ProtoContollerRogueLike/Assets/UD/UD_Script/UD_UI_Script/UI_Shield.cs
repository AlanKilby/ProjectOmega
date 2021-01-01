using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_Shield : MonoBehaviour
{
    [SerializeField] Image graph;

    [SerializeField] PlayerHealth PH;

    void Start()
    {
        //PH = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerHealth>();
        graph.gameObject.SetActive(false);
    }

    void Update()
    {
        if (PH.hasShield && PH != null)
        {
            graph.gameObject.SetActive(true);
            float fillPercent = (float) PH.shieldHealthPointCurrent / PH.shieldHealthPointSet;
            graph.fillAmount = fillPercent;
        }
        else
        {
            graph.gameObject.SetActive(false);
        }
    }
}
