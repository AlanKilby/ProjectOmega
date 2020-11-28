using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CurrencySysteme : MonoBehaviour
{
    ConsumablePlatform CP;

    public int currentMoneyAmount;

    private void Start()
    {
        CP = GameObject.FindGameObjectWithTag("Player").GetComponent<ConsumablePlatform>();
    }

    public void GainMoney(int moneyGained)
    {
        currentMoneyAmount += moneyGained * CP.doublePointCurrentMultiplicator;
    }
}
