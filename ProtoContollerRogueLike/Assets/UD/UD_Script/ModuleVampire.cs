using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModuleVampire : MonoBehaviour
{
    SwordAttack SA;

    private void Start()
    {
        SA = GameObject.FindGameObjectWithTag("Player").GetComponent<SwordAttack>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player") == true)
        {
            SA.isVampire = true;
            Destroy(gameObject);
        }
    }
}
