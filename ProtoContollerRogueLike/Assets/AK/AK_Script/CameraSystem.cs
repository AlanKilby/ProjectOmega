using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraSystem : MonoBehaviour
{
    public PlayerCamera playerIsInTheRoom;
    public Transform mainCameraPos;

    private void Start()
    {
        playerIsInTheRoom = GetComponentInChildren<PlayerCamera>();
        mainCameraPos = GameObject.FindGameObjectWithTag("CameraHolder").transform;
    }

    private void Update()
    {
        if (playerIsInTheRoom.playerCamera)
        {
            mainCameraPos.position = new Vector3(gameObject.transform.position.x-2, gameObject.transform.position.y,mainCameraPos.position.z);
        }
    }
}
