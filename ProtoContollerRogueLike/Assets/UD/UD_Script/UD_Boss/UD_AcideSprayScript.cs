using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UD_AcideSprayScript : MonoBehaviour
{
    public float acideSprayLifeTimeSet;
    private float acideSprayLifeTimer;

    void Start()
    {
        acideSprayLifeTimer = acideSprayLifeTimeSet;
    }

    void Update()
    {
        acideSprayLifeTimer -= Time.deltaTime;
        if(acideSprayLifeTimer <= 0)
        {
            Destroy(gameObject);
        }
    }
}
