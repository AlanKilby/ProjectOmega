using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_FloorIndicator : MonoBehaviour
{
    Text floorIndicator;

    void Start()
    {
        floorIndicator = GetComponent<Text>();
        UpdateIndicator();
    }

    public void UpdateIndicator()
    {
        if(DifficultyPanel.currentStage == 0)
        {
            floorIndicator.text = "FLOOR : " + DifficultyPanel.currentStage.ToString();
        }
        else
        {
            floorIndicator.text = "FLOOR : - " + DifficultyPanel.currentStage.ToString();
        }
    }

    public void IndicatorHUB()
    {
        floorIndicator.text = "HUB";
    }
}
