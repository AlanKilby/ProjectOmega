using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float speed;
    public Transform playerPos;
    public RoomTriggerCollider thisRoom;
    private void Start()
    {
        thisRoom = gameObject.GetComponentInParent<RoomTriggerCollider>();
        playerPos = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
    }
    private void Update()
    {
        if(thisRoom.playerIsInTheRoom == true)
        {
            transform.position = Vector2.MoveTowards(transform.position, playerPos.transform.position, speed * Time.deltaTime);


        }

    }


}
