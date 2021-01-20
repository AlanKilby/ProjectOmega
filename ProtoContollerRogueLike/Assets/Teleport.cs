using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleport : MonoBehaviour
{
    private bool canTeleport;
    private GameObject player;

    public Vector3 clickPosition;

    void Start()
    {
        canTeleport = false;
        player = GameObject.FindGameObjectWithTag("Player");
    }

    void Update()
    {
        if (Input.GetKeyDown("f1"))
        {
            canTeleport = true;
        }

        if (Input.GetMouseButtonDown(0) && canTeleport == true)
        {
            Debug.Log(Input.mousePosition);

            clickPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition + new Vector3(0, 0, 2));
            player.transform.position = new Vector3(clickPosition.x, clickPosition.y, 0);
            canTeleport = false;
        }
    }
}
