using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_DoublePoint : MonoBehaviour
{
    [SerializeField] Image graph;

    [SerializeField] ConsumablePlatform CP;

    void Start()
    {
        //CP = GameObject.FindGameObjectWithTag("Player").GetComponent<ConsumablePlatform>();
        graph.gameObject.SetActive(false);
    }

    void Update()
    {
        if (CP.hasDoublePoint && CP != null)
        {
            graph.gameObject.SetActive(true);
            float fillPercent = CP.doublePointTimer / CP.doublePointTimeSet;
            graph.fillAmount = fillPercent;
        }
        else
        {
            graph.gameObject.SetActive(false);
        }
    }
}
