using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerModuleStation : MonoBehaviour
{
    public bool moduleExplosiveCharge;
    public bool soulScream;

    private void Start()
    {
        moduleExplosiveCharge = false;
        soulScream = false;
    }
}
