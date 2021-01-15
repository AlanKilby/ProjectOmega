using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_PointInfoCheat : MonoBehaviour
{
    public static bool moduleCheatEnable;

    private void Start()
    {
        //moduleCheatEnable = false;
    }

    public void EnableModuleCheat()
    {
        moduleCheatEnable = true;
    }
    
    public void DiseableModuleCheat()
    {
        moduleCheatEnable = false;
    }
}
