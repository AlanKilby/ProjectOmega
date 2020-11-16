using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomDesign : MonoBehaviour
{
    public GameObject[] roomDesign;

    int roomPicker;

    private void Start()
    {
        roomPicker = Random.Range(0, roomDesign.Length);
        Instantiate(roomDesign[roomPicker], new Vector2(transform.position.x,transform.position.y), Quaternion.identity, transform);
    }

}
