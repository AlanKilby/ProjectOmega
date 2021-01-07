using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UD_AcideSprayScript : MonoBehaviour
{
    public CameraSystem thisRoom;

    public float acideSprayLifeTimeSet;
    private float acideSprayLifeTimer;

    void Start()
    {
        thisRoom = gameObject.GetComponentInParent<CameraSystem>();
        acideSprayLifeTimer = acideSprayLifeTimeSet;
    }

    void Update()
    {
        if (thisRoom.playerIsInTheRoom.playerIsInTheRoom)
        {
            acideSprayLifeTimer -= Time.deltaTime;
            if (acideSprayLifeTimer <= 0)
            {
                Destroy(gameObject);
            }
        }
    }
}
