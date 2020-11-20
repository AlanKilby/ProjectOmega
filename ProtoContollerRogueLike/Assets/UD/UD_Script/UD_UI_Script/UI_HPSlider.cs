using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_HPSlider : MonoBehaviour
{
    [SerializeField] PlayerHealth PH;

    [SerializeField] Slider HPSlider;

    RectTransform rt;

    [SerializeField] private float expressSliderValue;

    void Start()
    {
        rt = GetComponent<RectTransform>();
        
    }


    void Update()
    {
        rt.localScale = new Vector3(PH.healthPercent, 1, 1);
        expressSliderValue = PH.healthPercent;
        //HPSlider.value = CalculateSliderValue();
    }

    float CalculateSliderValue()
    {
        return (PH.currentPlayerHealth / PH.totalPlayerHealthUpgraded);
    }
}
