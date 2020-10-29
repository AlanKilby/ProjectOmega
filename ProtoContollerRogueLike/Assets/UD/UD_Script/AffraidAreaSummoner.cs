using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AffraidAreaSummoner : MonoBehaviour
{
    [SerializeField] EnnemiSummoner ES;

    private void Update()
    {

        if (!ES.playerInSight)
        {
            ES.playerInAffraidArea = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            ES.playerInAffraidArea = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            ES.playerInAffraidArea = false;
        }
    }
}
