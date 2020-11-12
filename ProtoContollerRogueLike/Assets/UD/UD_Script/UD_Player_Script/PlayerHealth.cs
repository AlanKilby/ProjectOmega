using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public float totalPlayerHealth;
    public float currentPlayerHealth;
    public float healthPercent;
    [HideInInspector] public float healthRegenWithQuickRevive;

    public int quickReviveIconSlot;

    public bool hasQuickRevive;

    Animator anim;
    Inventory In;
    Rigidbody2D rb;
    //Ajout Gus
    private string currentAnimation;

    const string PLAYER_DEATH = "PlayerDeath";
    //

    void Start()
    {
        anim = GetComponent<Animator>();
        rb = GameObject.FindGameObjectWithTag("Player").GetComponent<Rigidbody2D>();
        In = GameObject.FindGameObjectWithTag("Player").GetComponent<Inventory>();
        currentPlayerHealth = totalPlayerHealth;
    }
    private void Update()
    {
        healthPercent = (currentPlayerHealth / totalPlayerHealth);
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
            foreach (Transform child in In.moduleSlots[quickReviveIconSlot].transform)
            {
                Destroy(child.gameObject);
            }
            In.isFullModule[quickReviveIconSlot] = false;
            RemoveFromList(quickReviveIconSlot);
        }
    }

    void RemoveFromList(int index)
    {
        if (index >= 0 && index < In.moduleSlots.Length)
        {

            Destroy(In.moduleGameObject[index]);
            In.moduleGameObject[quickReviveIconSlot] = null;

        }
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
