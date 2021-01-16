using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChangingCamera : MonoBehaviour
{
    PlayerMouvement PM;
    Canvas PlayerUI;
    Camera cam;
    FadeSceneManagerScript FSMS;

    private void Start()
    {
        PM = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMouvement>();
        PlayerUI = GameObject.FindGameObjectWithTag("PlayerUI").GetComponent<Canvas>();
        FSMS = GameObject.Find("FadeManager").GetComponent<FadeSceneManagerScript>();
        cam = GetComponent<Camera>();
        PM.cam = cam;
        PlayerUI.worldCamera = cam;
        PlayerUI.sortingLayerName = "UppestLayer";
        FSMS.FadeIn();
    }

}
