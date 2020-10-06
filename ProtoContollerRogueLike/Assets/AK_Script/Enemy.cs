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



            // Cette partie du script fut trouvee sur le forum Unity https://answers.unity.com/questions/585035/lookat-2d-equivalent-.html?_ga=2.230719519.1043224240.1601999147-1783980511.1597703941
            Vector3 diff = playerPos.position - gameObject.transform.position;
            diff.Normalize();

            float rot_z = Mathf.Atan2(diff.y, diff.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0f, 0f, rot_z - 90);

        }

    }


}
