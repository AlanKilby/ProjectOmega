using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_CurrencyCounter : MonoBehaviour
{
    CurrencySysteme CS;

    Text currencyShowed;

    void Start()
    {
        CS = GameObject.FindGameObjectWithTag("Player").GetComponent<CurrencySysteme>();
        currencyShowed = GetComponent<Text>();
    }

    void Update()
    {
        if (currencyShowed != null)
        {
            currencyShowed.text = CS.currentMoneyAmount.ToString();
        }
    }
}
