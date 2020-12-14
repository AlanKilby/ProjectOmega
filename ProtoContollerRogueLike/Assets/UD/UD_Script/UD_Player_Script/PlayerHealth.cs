using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour
{
    GameObject UI;
    GameObject sol;
    GameObject Room;


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

    [Header("Shield")]
    public int shieldHealthPointSet;
    [HideInInspector] public int shieldHealthPointCurrent;
    [HideInInspector] public bool hasShield;

    void Start()
    {
        totalPlayerHealthUpgraded = totalPlayerHealthSet; // A Set en tout début de partie
        anim = GetComponent<Animator>();
        rb = GameObject.FindGameObjectWithTag("Player").GetComponent<Rigidbody2D>();
        In = GameObject.FindGameObjectWithTag("Player").GetComponent<Inventory>();
        PM = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMouvement>();
        UI = GameObject.Find("UI");
        sol = GameObject.Find("Sol");
        Room = GameObject.Find("RoomObjects");
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
        healthPercent = (currentPlayerHealth / totalPlayerHealthUpgraded);
        if (shieldHealthPointCurrent <= 0)
        {
            hasShield = false;
        }

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
        Destroy(UI);
        Destroy(sol);
        Destroy(Room);
        //anim.SetBool("isDead", true);
        Invoke("GameOver", 1.69f);
        Destroy(gameObject, 1.70f);
        

        //
    }


    public void TakeDamage(int damage)
    {
        if (PM.isDashing && PM.hasModulePhaseShift)
        {

        }
        else if (currentPlayerHealth > 0) //Ajout Gus
        {
            if (hasShield)
            {
                shieldHealthPointCurrent -= damage;
                //Ajout Gus
                sr.material = matWhite;
                Invoke("ResetMaterial", 0.1f);
                FindObjectOfType<AudioManager>().Play("Damage Hit");
                //
            }
            else
            {
                currentPlayerHealth -= damage;
                FindObjectOfType<AudioManager>().Play("Damage Hit");
                //Ajout Gus
                sr.material = matWhite;
                Invoke("ResetMaterial", 0.1f);
                //
            }
        }

        if (currentPlayerHealth <= 0 && !hasQuickRevive)
        {
            //Destroy(gameObject);
            //Ajout Gus
            FindObjectOfType<AudioManager>().StopPlaying("Damage Hit");
            FindObjectOfType<AudioManager>().StopPlaying("Fight Music");
            FindObjectOfType<AudioManager>().Play("Death");
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

    public void StartShield()
    {
        shieldHealthPointCurrent = shieldHealthPointSet;
        hasShield = true;
    }

    private void GameOver()
    {
        SceneManager.LoadScene("GameOver");
    }

    //Ajout Gus
    void ChangeAnimationState(string newAnimation)
    {
        if (currentAnimation == newAnimation) return;

        anim.Play(newAnimation);

        currentAnimation = newAnimation;
    }
}