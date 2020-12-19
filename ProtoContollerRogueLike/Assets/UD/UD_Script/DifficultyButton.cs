using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class DifficultyButton : MonoBehaviour
{
    [SerializeField] private bool easyButton;
    [SerializeField] private bool normalButton;
    [SerializeField] private bool hardButton;

    [SerializeField] private GameObject easyButtonObject;
    [SerializeField] private GameObject normalButtonObject;
    [SerializeField] private GameObject hardButtonObject;

    private void Update()
    {
        if(DifficultyPanel.currentDifficulty == Difficulty.Easy && easyButton)
        {
            gameObject.GetComponent<Toggle>().isOn = true;
        }        
        else if(DifficultyPanel.currentDifficulty == Difficulty.Normal && normalButton)
        {
            gameObject.GetComponent<Toggle>().isOn = true;
        }
        else if (DifficultyPanel.currentDifficulty == Difficulty.Hard && hardButton)
        {
            gameObject.GetComponent<Toggle>().isOn = true;
        }
        /*else
        {
            gameObject.GetComponent<Toggle>().isOn = false;
        }*/
    }

    public void SetDifficulty()
    {
        if (easyButton)
        {
            DifficultyPanel.currentDifficulty = Difficulty.Easy;
        }
        if (normalButton)
        {
            DifficultyPanel.currentDifficulty = Difficulty.Normal;
        }
        if (hardButton)
        {
            DifficultyPanel.currentDifficulty = Difficulty.Hard;
        }
    }
}
