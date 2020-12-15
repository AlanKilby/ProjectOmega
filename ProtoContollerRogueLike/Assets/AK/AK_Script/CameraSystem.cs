using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraSystem : MonoBehaviour
{
    public RoomTriggerCollider isPlayerInRoom;
    public Transform mainCameraPos;

    private void Start()
    {
        isPlayerInRoom = GetComponentInChildren<RoomTriggerCollider>();
        mainCameraPos = GameObject.FindGameObjectWithTag("MainCamera").transform;
    }

    private void Update()
    {
        if (isPlayerInRoom.playerIsInTheRoom)
        {
            mainCameraPos.position = new Vector3(gameObject.transform.position.x-2, gameObject.transform.position.y,mainCameraPos.position.z);
        }
    }
}
