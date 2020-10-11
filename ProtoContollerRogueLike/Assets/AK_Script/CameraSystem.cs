using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraSystem : MonoBehaviour
{
    public RoomTriggerCollider isPlayerInRoom;
    public Transform mainCameraPos;

    private void Start()
    {
        isPlayerInRoom = GetComponentInParent<RoomTriggerCollider>();
        mainCameraPos = GameObject.FindGameObjectWithTag("MainCamera").transform;
    }

    private void Update()
    {
        if (isPlayerInRoom.playerIsInTheRoom)
        {
            mainCameraPos.position = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y,mainCameraPos.position.z);
        }
    }
}
