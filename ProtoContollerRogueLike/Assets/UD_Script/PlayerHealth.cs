using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public int totalPlayerHealth;
    public static int currentPlayerHealth;

    void Start()
    {
        currentPlayerHealth = totalPlayerHealth;
    }
    private void Update()
    {
        if (currentPlayerHealth <= 0)
        {
            Destroy(gameObject);
        }
    }

    public void TakeDamage(int damage)
    {
        currentPlayerHealth -= damage;
    }
}
