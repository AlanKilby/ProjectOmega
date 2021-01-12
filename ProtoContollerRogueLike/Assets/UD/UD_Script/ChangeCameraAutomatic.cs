using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChangeCameraAutomatic : MonoBehaviour
{
    Canvas cam;

    void Start()
    {
        cam = GetComponent<Canvas>();
        cam.worldCamera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
    }
}
