using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UD_Ravin : MonoBehaviour
{
    PlayerMouvement PM;

    Collider2D ownCollider2D;

    private void Start()
    {
        PM = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMouvement>();
        ownCollider2D = GetComponent<Collider2D>();
    }

    private void EnableRavinCollision(Collider2D ravinCollider)
    {
        Physics2D.IgnoreCollision(ravinCollider, ownCollider2D, true);
    }

    private void DiseableRavinCollision(Collider2D ravinCollider)
    {
        Physics2D.IgnoreCollision(ravinCollider, ownCollider2D, false);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (PM.isDashing && collision.gameObject.CompareTag("Ravin"))
        {
            EnableRavinCollision(collision.collider);
            print("enableCollision");
        }
    }
    private void OnCollisionStay2D(Collision2D collision)
    {
        if (PM.isDashing && collision.gameObject.CompareTag("Ravin"))
        {
            EnableRavinCollision(collision.collider);
            print("enableCollision");
        }
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (PM.isDashing && collision.gameObject.CompareTag("Ravin"))
        {
            DiseableRavinCollision(collision.collider);
            print("diseableCollision");
        }
    }
}
