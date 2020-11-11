using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AffraidArea : MonoBehaviour
{
    [SerializeField] Spider Sp;

    private void Update()
    {

        if (!Sp.playerInSight)
        {
            Sp.playerInAffraidArea = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Sp.playerInAffraidArea = true;
        }
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Sp.playerInAffraidArea = true;
        }
    }
    
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Sp.playerInAffraidArea = false;
        }
    }
}
