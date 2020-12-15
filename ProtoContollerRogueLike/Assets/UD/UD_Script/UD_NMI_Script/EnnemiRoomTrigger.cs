using Pathfinding;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnnemiRoomTrigger : MonoBehaviour
{
    public RoomTriggerCollider thisRoom;

    void Start()
    {
        gameObject.GetComponent<AIPath>().enabled = false;
        thisRoom = GameObject.FindGameObjectWithTag("RoomTrigger").GetComponentInParent<RoomTriggerCollider>();
    }

    void Update()
    {
        if (thisRoom.playerIsInTheRoom)
        {
            gameObject.GetComponent<AIPath>().enabled = true;
        }
        else gameObject.GetComponent<AIPath>().enabled = false;
    }
}
