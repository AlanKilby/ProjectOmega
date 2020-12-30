using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Module : MonoBehaviour
{
    public GameObject moduleIcon;
    public float spawnProbability;
    public int moduleSlot;

    public string textShowedOnPopUp;

    UI_ModulePopUp popUpObject;
    Image popUpImage;
    Text popUpText; 

    private void Start()
    {
        popUpObject = GameObject.Find("ModulePopUp").GetComponent<UI_ModulePopUp>();
        popUpImage = GameObject.Find("ModulePopUpImage").GetComponent<Image>();
        popUpText = GameObject.Find("ModulePopUpText").GetComponent<Text>();
    }

    public void LaunchPopUp()
    {
        popUpImage.sprite = moduleIcon.GetComponent<Image>().sprite;
        popUpText.text = textShowedOnPopUp;
        popUpObject.ShowPopUp();
    }

}
