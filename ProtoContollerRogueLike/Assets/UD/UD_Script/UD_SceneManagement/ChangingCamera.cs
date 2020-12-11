using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChangingCamera : MonoBehaviour
{
    PlayerMouvement PM;
    Canvas PlayerUI;
    Camera cam;

    private void Start()
    {
        PM = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMouvement>();
        PlayerUI = GameObject.FindGameObjectWithTag("PlayerUI").GetComponent<Canvas>();
        cam = GetComponent<Camera>();
        PM.cam = cam;
        PlayerUI.worldCamera = cam;
    }

}
