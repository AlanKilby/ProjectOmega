using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UD_BossBase : MonoBehaviour
{
    //SCRIPT INSPIRE DE L'ENNEMI SCRIPT ET ADAPTE AU BOSS
    public LayerMask whatIsPlayer;

    Transform playerPos;

    CurrencySysteme CS;
    DifficultyPanel DP;

    [SerializeField] LootDrop lootDrop;
    [SerializeField] private Animator anim;
    public CameraSystem thisRoom;

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

    [Header("Back to HUB when Died")]
    [SerializeField] GameObject HUBTeleporter;
    [SerializeField] Transform spawnPosForHUBTeleporter;

    //Ajout Gus
    private Material matWhite;
    private Material matDefault;
    SpriteRenderer sr;
    //

    [HideInInspector] public bool takeDamage;

    private void Start()
    {
        isAlive = true;
        takeDamage = false;
        DP = GameObject.Find("DifficultyPanel").GetComponent<DifficultyPanel>();
        CS = GameObject.FindGameObjectWithTag("Player").GetComponent<CurrencySysteme>();
        thisRoom = gameObject.GetComponentInParent<CameraSystem>();
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
        if (isAlive)
        {
            PlayerDirection();
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
            if (stunTimer <= 0)
            {
                isStunned = false;
            }
        }
    }

    public void TakeDamage(int damage)
    {
        if (thisRoom.playerIsInTheRoom.playerIsInTheRoom)
        {
            health -= damage;
            takeDamage = true;

            //Ajout Gus
            sr.material = matWhite;
            if (health <= 0)
            {
                isAlive = false;
                //Death();
                Invoke("ResetMaterial", 0.1f);
            }
            else
            {
                Invoke("ResetMaterial", 0.1f);
            }
            //
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
        DifficultyPanel.currentStage++;
        DP.StageUpDifficultyIncreased(DifficultyPanel.currentStage);
        Instantiate(HUBTeleporter, spawnPosForHUBTeleporter);
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
