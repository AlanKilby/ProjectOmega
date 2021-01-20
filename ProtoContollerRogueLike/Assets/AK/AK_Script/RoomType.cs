using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomType : MonoBehaviour
{
    public int type;
    private RoomDesign roomDesign;

    public void RoomDestruction()
    {
        Debug.Log("Destruction executed " + type);
        Destroy(gameObject);
        //roomDesign = gameObject.GetComponentInParent<RoomDesign>();
        //roomDesign.RoomDestruction();
    }
}
