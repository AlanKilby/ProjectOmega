using Pathfinding;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnnemiRoomTrigger : MonoBehaviour
{
    public CameraSystem thisRoom;

    void Start()
    {
        gameObject.GetComponent<AIPath>().enabled = false;
        thisRoom = GetComponentInParent<CameraSystem>();
    }

    void Update()
    {
        if (thisRoom.playerIsInTheRoom.playerCamera)
        {
            gameObject.GetComponent<AIPath>().enabled = true;
        }
        else gameObject.GetComponent<AIPath>().enabled = false;
    }
}
