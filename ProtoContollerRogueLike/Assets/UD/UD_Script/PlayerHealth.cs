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
    Rigidbody2D rb;
    //Ajout Gus
    private string currentAnimation;

    const string PLAYER_DEATH = "PlayerDeath";
    //

    void Start()
    {
        anim = GetComponent<Animator>();
        rb = GameObject.FindGameObjectWithTag("Player").GetComponent<Rigidbody2D>();
        currentPlayerHealth = totalPlayerHealth;
    }
    private void Update()
    {
        if (currentPlayerHealth <= 0 && !hasQuickRevive)
        {
            //Destroy(gameObject);
            //Ajout Gus
            Die();
            return;
            //
        }
        if (currentPlayerHealth <= 0 && hasQuickRevive)
        {
            currentPlayerHealth = healthRegenWithQuickRevive;
            hasQuickRevive = false;
        }
        healthPercent = (currentPlayerHealth / totalPlayerHealth);
    }
    //Ajout Gus
    public void Die()
    {
        rb.velocity = new Vector2 (0.0f, 0.0f);
        GetComponent<PlayerMouvement>().enabled = false;
        GetComponent<Shooting>().enabled = false;
        GetComponent<SwordAttack>().enabled = false;
        ChangeAnimationState(PLAYER_DEATH);
        //anim.SetBool("isDead", true);
        Destroy(gameObject, 1.85f);
    }
    // 2 problèmes persistent : - continue de tirer pendant l'anim de mort, stop script alan
    //                          - mettre rigidbody xy 0 0

    //


    public void TakeDamage(int damage)
    {
        currentPlayerHealth -= damage;
    }
   
    //Ajout Gus
    void ChangeAnimationState(string newAnimation)
    {
        if (currentAnimation == newAnimation) return;

        anim.Play(newAnimation);

        currentAnimation = newAnimation;
    }
    //
}
