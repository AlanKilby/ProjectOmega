using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnnemiSummonerDontMoveArea : MonoBehaviour
{
    [SerializeField] EnnemiSummoner ES;

    private void Update()
    {

        if (!ES.playerInSight)
        {
            ES.playerInDontMoveArea = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            ES.playerInDontMoveArea = true;
        }
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            ES.playerInDontMoveArea = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            ES.playerInDontMoveArea = false;
        }
    }
}
