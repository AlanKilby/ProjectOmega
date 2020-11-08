using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public float totalPlayerHealth;
    public float currentPlayerHealth;
    public float healthPercent;
    [HideInInspector] public float healthRegenWithQuickRevive;

    public bool hasQuickRevive;

    Animator anim;

    void Start()
    {
        anim = GetComponent<Animator>();
        currentPlayerHealth = totalPlayerHealth;
    }
    private void Update()
    {
        if (currentPlayerHealth <= 0 && !hasQuickRevive)
        {
            Destroy(gameObject);
        }
        if (currentPlayerHealth <= 0 && hasQuickRevive)
        {
            currentPlayerHealth = healthRegenWithQuickRevive;
            hasQuickRevive = false;
        }
        healthPercent = (currentPlayerHealth / totalPlayerHealth);
    }

    public void TakeDamage(int damage)
    {
        currentPlayerHealth -= damage;
    }
}
