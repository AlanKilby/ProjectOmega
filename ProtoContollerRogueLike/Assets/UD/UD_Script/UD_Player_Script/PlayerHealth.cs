using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public float totalPlayerHealthSet;
    public float totalPlayerHealthUpgraded;
    public float currentPlayerHealth;
    public float healthPercent;
    [HideInInspector] public float healthRegenWithQuickRevive;

    public int quickReviveIconSlot;

    public bool hasQuickRevive;

    Animator anim;
    Inventory In;
    PlayerMouvement PM;
    Rigidbody2D rb;
    //Ajout Gus
    private string currentAnimation;
    const string PLAYER_DEATH = "PlayerDeath";

    private Material matWhite;
    private Material matDefault;
    SpriteRenderer sr;
    //

    void Start()
    {
        totalPlayerHealthUpgraded = totalPlayerHealthSet; // A Set en tout début de partie
        anim = GetComponent<Animator>();
        rb = GameObject.FindGameObjectWithTag("Player").GetComponent<Rigidbody2D>();
        In = GameObject.FindGameObjectWithTag("Player").GetComponent<Inventory>();
        PM = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMouvement>();
        currentPlayerHealth = totalPlayerHealthUpgraded;
        hasQuickRevive = false;

        //Ajout Gus
        sr = GetComponent<SpriteRenderer>();
        matWhite = Resources.Load("PlayerFlash", typeof(Material)) as Material;
        matDefault = sr.material;
        //
    }

    private void Update()
    {

        /*healthPercent = (currentPlayerHealth / totalPlayerHealth);
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
        }*/

    }

    //Ajout Gus
    void ResetMaterial()
    {
        sr.material = matDefault;
    }
    //

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
        rb.velocity = new Vector2(0.0f, 0.0f);
        GetComponent<PlayerMouvement>().enabled = false;
        GetComponent<Shooting>().enabled = false;

        //Merci Alan !
        foreach (Transform child in transform)
        {
            Destroy(child.gameObject);
        }
        //

        GetComponent<SwordAttack>().enabled = false;
        ChangeAnimationState(PLAYER_DEATH);
        //anim.SetBool("isDead", true);
        Destroy(gameObject, 1.85f);
        //
    }                    


    public void TakeDamage(int damage)
    {
        if (PM.isDashing && PM.hasModulePhaseShift)
        {

        }
        else
        {
            currentPlayerHealth -= damage;
          //Ajout Gus
            sr.material = matWhite;
        }
        healthPercent = (currentPlayerHealth / totalPlayerHealthUpgraded);
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
        //Ajout Gus
        if (currentPlayerHealth > 0)
        {
            Invoke("ResetMaterial", 0.1f);
        }
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
