using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UD_AcideSprayScript : MonoBehaviour
{
    public CameraSystem thisRoom;
    Animator anim;

    public float acideSprayLifeTimeSet;
    private float acideSprayLifeTimer;

    bool disappear;

    void Start()
    {
        thisRoom = gameObject.GetComponentInParent<CameraSystem>();
        anim = GetComponent<Animator>();
        acideSprayLifeTimer = acideSprayLifeTimeSet;
        disappear = false;
    }

    void Update()
    {
        if (thisRoom.playerIsInTheRoom.playerIsInTheRoom)
        {
            acideSprayLifeTimer -= Time.deltaTime;
            if (acideSprayLifeTimer <= 0)
            {
                disappear = true;
            }
        }

        anim.SetBool("disappear", disappear);
    }
}
