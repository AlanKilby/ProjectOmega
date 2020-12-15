using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraSystem : MonoBehaviour
{
    public RoomTriggerCollider playerIsInTheRoom;
    public Transform mainCameraPos;

    private void Start()
    {
        playerIsInTheRoom = GetComponentInChildren<RoomTriggerCollider>();
        mainCameraPos = GameObject.FindGameObjectWithTag("MainCamera").transform;
    }

    private void Update()
    {
        if (playerIsInTheRoom.playerIsInTheRoom)
        {
            mainCameraPos.position = new Vector3(gameObject.transform.position.x-2, gameObject.transform.position.y,mainCameraPos.position.z);
        }
    }
}
