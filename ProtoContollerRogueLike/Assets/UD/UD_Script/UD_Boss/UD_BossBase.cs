using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UD_BossBase : MonoBehaviour
{
    //SCRIPT INSPIRE DE L'ENNEMI SCRIPT ET ADAPTE AU BOSS
    public LayerMask whatIsPlayer;

    Transform playerPos;

    CurrencySysteme CS;

    [SerializeField] LootDrop lootDrop;
    [SerializeField] private Animator anim;

    [Header("Stats")]
    public int health;
    public int dropRate;
    public float speed;
    public int moneyDrop;

    public bool isStunned;
    private float stunTimer;
    public float stunTimerSet;
    public float stunSlowPercentageEffect;

    [HideInInspector] public bool isAlive;

    //Ajout Gus
    private Material matWhite;
    private Material matDefault;
    SpriteRenderer sr;
    //

    public bool takeDamage;

    private void Start()
    {
        isAlive = true;
        takeDamage = false;
        CS = GameObject.FindGameObjectWithTag("Player").GetComponent<CurrencySysteme>();
        lootDrop = GetComponent<LootDrop>();
        anim = GetComponent<Animator>();
        isStunned = false;

        playerPos = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();

        //Ajout Gus
        sr = GetComponent<SpriteRenderer>();
        matWhite = Resources.Load("EnemyFlash", typeof(Material)) as Material;
        matDefault = sr.material;
        //
    }

    private void Update()
    {
        if (isStunned)
        {
            //EFFETS SI IL EST STUN
        }
        else
        {
            //EFFETS SI IL EST N'EST PAS STUN
        }
        PlayerDirection();
        StunnedTimer();
    }

    //Ajout Gus
    void ResetMaterial()
    {
        sr.material = matDefault;
    }
    //

    void StunnedTimer()
    {
        if (isStunned)
        {
            stunTimer -= Time.deltaTime;
            if (stunTimer <= 0)
            {
                isStunned = false;
            }
        }
    }

    public void TakeDamage(int damage)
    {
        health -= damage;
        takeDamage = true;

        //Ajout Gus
        sr.material = matWhite;
        if (health <= 0)
        {
            isAlive = false;
            Death();
        }
        else
        {
            Invoke("ResetMaterial", 0.1f);
        }
        //
    }

    public void Stun()
    {
        isStunned = true;
        stunTimer = stunTimerSet;
    }

    /*void UpdateAnim()
    {
        anim.SetBool("takeDamage", takeDamage);
    }*/

    public void StopAnimTakeDamage()
    {
        takeDamage = false;
    }



    public void Death()
    {
        CS.GainMoney(moneyDrop);
        //CS.currentMoneyAmount += moneyDrop;
        //lootDrop.DropLoot(); //A VOIR CE QU'IL LOOT A LA MORT
        Destroy(gameObject);
    }

    void PlayerDirection()
    {
        Vector3 diff = playerPos.position - gameObject.transform.position;
        diff.Normalize();

        float rot_z = Mathf.Atan2(diff.y, diff.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0f, 0f, rot_z - 90);
    }
}
