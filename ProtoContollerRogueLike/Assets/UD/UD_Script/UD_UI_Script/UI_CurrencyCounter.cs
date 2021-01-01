using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_CurrencyCounter : MonoBehaviour
{

    Text currencyShowed;

    void Start()
    {
        currencyShowed = GetComponent<Text>();
    }

    void Update()
    {
        if (currencyShowed != null)
        {
            currencyShowed.text = CurrencySysteme.currentMoneyAmount.ToString();
        }
    }
}
