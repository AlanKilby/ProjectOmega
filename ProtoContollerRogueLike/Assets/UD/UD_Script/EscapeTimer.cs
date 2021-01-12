using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EscapeTimer : MonoBehaviour
{
    EscapePod EP;
    [SerializeField] GameObject EscapeUITimer;
    [SerializeField] Text EscapeUITimerText;

    void Start()
    {
        EP = GetComponent<EscapePod>();
    }

    void Update()
    {
        if (EP.escapeStart)
        {
            EscapeUITimer.SetActive(true);
            EscapeUITimerText.text = Mathf.Round(EP.escapeTimer).ToString();
        }
        else
        {
            EscapeUITimer.SetActive(false);
        }
    }
}
