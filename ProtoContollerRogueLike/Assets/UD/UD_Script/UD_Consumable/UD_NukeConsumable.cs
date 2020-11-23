using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UD_NukeConsumable : MonoBehaviour
{
    public RoomTriggerCollider thisRoom;

    public int nukeDamage;

    private void Start()
    {
        thisRoom = gameObject.GetComponentInParent<RoomTriggerCollider>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player") == true)
        {
            foreach(Transform child1 in thisRoom.transform)
            {
                if (child1.CompareTag("Ennemi"))
                {
                    foreach (Transform child2 in thisRoom.transform)
                    {
                        if (child2.CompareTag("Ennemi"))
                        {
                            foreach (Transform child3 in thisRoom.transform)
                            {
                                if (child3.CompareTag("Ennemi"))
                                {
                                    child3.GetComponent<EnnemisScript>().TakeDamage(nukeDamage);
                                }
                            }
                            child2.GetComponent<EnnemisScript>().TakeDamage(nukeDamage);
                        }
                    }
                    child1.GetComponent<EnnemisScript>().TakeDamage(nukeDamage);
                }
            }
            Destroy(gameObject);
        }
    }
}
