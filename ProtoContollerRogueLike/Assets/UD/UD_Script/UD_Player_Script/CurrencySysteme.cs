using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CurrencySysteme : MonoBehaviour
{
    ConsumablePlatform CP;

    public static int currentMoneyAmount;

    [SerializeField] int moneyGainWithCheat;

    private void Start()
    {
        CP = GameObject.FindGameObjectWithTag("Player").GetComponent<ConsumablePlatform>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.M))
        {
            GainMoney(moneyGainWithCheat);
        }
    }

    public void GainMoney(int moneyGained)
    {
        currentMoneyAmount += moneyGained * CP.doublePointCurrentMultiplicator;
    }
}
