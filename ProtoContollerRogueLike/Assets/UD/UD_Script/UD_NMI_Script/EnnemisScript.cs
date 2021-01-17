using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class EnnemisScript : MonoBehaviour
{
    public LayerMask whatIsPlayer;

    AIPath AIP;
    CurrencySysteme CS;
    DifficultyPanel DP;
    public CameraSystem thisRoom;

    [SerializeField] LootDrop lootDrop;
    [SerializeField] private Animator anim;

    [Header("Type of Enemy ?")]
    [SerializeField] private bool isZombie;
    [SerializeField] private bool isSpider;
    [SerializeField] private bool isSummoner;


    [Header("Stats")]
    public int health;
    public int dropRate;
    public float speed;
    public int moneyDrop;

    public bool isStunned;
    private float stunTimer;
    public float stunTimerSet;
    public float stunSlowPercentageEffect;

    //Ajout Gus
    private Material matWhite;
    private Material matDefault;
    SpriteRenderer sr;
    //

    public bool takeDamage;

    private void Start()
    {
        takeDamage = false;
        CS = GameObject.FindGameObjectWithTag("Player").GetComponent<CurrencySysteme>();
        thisRoom = gameObject.GetComponentInParent<CameraSystem>();
        lootDrop = GetComponent<LootDrop>();
        anim = GetComponent<Animator>();
        AIP = GetComponent<AIPath>();
        AIP.maxSpeed = speed;
        isStunned = false;

        DP = GameObject.Find("DifficultyPanel").GetComponent<DifficultyPanel>();

        //Augmentation des HP en fonction de la difficulté et des stages
        if (isZombie)
        {
            health = (int)Mathf.Round((health + DP.currentStageHPBonusForZombie) * DP.currentModHPMultiplier);
        }
        if (isSpider)
        {
            health = (int)Mathf.Round((health + DP.currentStageHPBonusForSpider) * DP.currentModHPMultiplier);
        }
        if (isSummoner)
        {
            health = (int)Mathf.Round((health + DP.currentStageHPBonusForSummoner) * DP.currentModHPMultiplier);
        }
        
        //Ajout Gus
        sr = GetComponent<SpriteRenderer>();
        matWhite = Resources.Load("EnemyFlash", typeof(Material)) as Material;
        matDefault = sr.material;
        //
    }

    private void Update()
    {
        /*UpdateAnim();
        if (health <= 0)
        {
            Death();
        }
        //PlayerDirection(); //de l'ancien script d'alan, ne convient plus au pathfinding a*
    }*/
        if (isStunned)
        {
            AIP.maxSpeed = speed * (1 - stunSlowPercentageEffect / 100);
        }
        else
        {
            AIP.maxSpeed = speed;
        }
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
            if(stunTimer <= 0)
            {
                isStunned = false;
            }
        }
    }
        
    public void TakeDamage(int damage)
    {
        if (thisRoom.playerIsInTheRoom.playerCamera)
        {
            health -= damage;
            takeDamage = true;

            //Ajout Gus
            sr.material = matWhite;
            if (health <= 0)
            {
                Death();
                if (isZombie)
                    FindObjectOfType<AudioManager>().Play("Zombie Death");
                if (isSpider)
                    FindObjectOfType<AudioManager>().Play("Spider Death");
                if (isSummoner)
                    FindObjectOfType<AudioManager>().Play("Summoner Death");
            }
            else
            {
                Invoke("ResetMaterial", 0.1f);
            }
            //
        }
        else
        {
            return;
        }
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
        lootDrop.DropLoot();
        Destroy(gameObject);
    }

    /*void PlayerDirection()
    {
        Vector3 diff = playerPos.position - gameObject.transform.position;
        diff.Normalize();

        float rot_z = Mathf.Atan2(diff.y, diff.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0f, 0f, rot_z - 90);
    }*/

    /*private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            other.gameObject.GetComponent<PlayerHealth>().TakeDamage(damage);
        }
    }*/
}
