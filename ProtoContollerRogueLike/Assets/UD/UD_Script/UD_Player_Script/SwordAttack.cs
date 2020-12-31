using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordAttack : MonoBehaviour
{
    public LayerMask whatIsEnnemy;
    [SerializeField] GameObject counterbladeSlashArea;

    PlayerHealth PH;
    PostProcessEffect PPERed;
    //Ajout Gus
    PlayerMouvement PM;
    //
    [SerializeField]
    private bool combatEnabled;
    [SerializeField]
    private Transform attackHitBoxPos;
    [SerializeField]
    private LayerMask whatIsKillable;
    [SerializeField]
    private float attackRadius;
    public float vampireHealthStollen;
    public float counterBladeActiveTimeSet;

    [HideInInspector] public int damageUpgraded;
    public int damageTotal;

    public bool gotInput;
    public bool isAttacking;
    public bool isVampire;
    public bool hasCounterBlade;

    [SerializeField] private Animator anim;

    private void Start()
    {
        PH = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerHealth>();
        counterbladeSlashArea = GameObject.FindGameObjectWithTag("CounterBladeSlashArea");
        PPERed = GameObject.Find("VampireEffect").GetComponent<PostProcessEffect>();
        anim = GetComponent<Animator>();
        anim.SetBool("canAttack", combatEnabled);
        combatEnabled = true;
        isVampire = false;
        //Ajout Gus
        PM = GetComponent<PlayerMouvement>();
        //
        counterbladeSlashArea.SetActive(false);
        damageUpgraded = damageTotal;
    }

    private void Update()
    {
        CheckAttacks();
        //Ajout Gus
        CheckMovement();
        //

        if (Input.GetButtonDown("Fire2"))
        {
            GetAttackInput();
            //ChangeAnimationState(PLAYER_SWORD);

        }
        /*if (isAttacking && hasCounterBlade)
        {
            counterbladeSlashArea.SetActive(true);
        }
        else
        {
            counterbladeSlashArea.SetActive(false);
        }*/
    }

    public void GetAttackInput()
    {
        if (combatEnabled)
        {
            gotInput = true;
        }
    }

    private void CheckAttacks()
    {
        if (gotInput)
        {
            if (!isAttacking)
            {
                gotInput = false;
                isAttacking = true;
                anim.SetBool("isAttacking", isAttacking);
                FindObjectOfType<AudioManager>().Play("Sword");
            }
        }
    }

    //Ajout Gus
    private void CheckMovement() 
    {
        anim.SetBool("isMoving", PM.playerIsMoving);
    }
    //


    public void CheckAttackHitBox()
    {
        Collider2D hitInfo = Physics2D.OverlapCircle(attackHitBoxPos.position, attackRadius, whatIsEnnemy);
        if (hitInfo != null)
        {

            Rigidbody2D enemy = hitInfo.GetComponent<Rigidbody2D>();
            if (hitInfo.CompareTag("Ennemi"))
            {
                Debug.Log("DAMAGE");
                hitInfo.GetComponent<EnnemisScript>().TakeDamage(damageUpgraded);
                if (isVampire)
                {
                    PPERed.LaunchEffectToMax();
                    PPERed.canDisappear = true;
                    PPERed.canAppear = false;
                    if (PH.currentPlayerHealth < PH.totalPlayerHealthUpgraded - vampireHealthStollen)
                    {
                        PH.currentPlayerHealth = PH.currentPlayerHealth + vampireHealthStollen;
                    }
                    else
                    {
                        PH.currentPlayerHealth = PH.totalPlayerHealthUpgraded;
                    }
                }
            }
            if (hitInfo.CompareTag("Boss"))
            {
                Debug.Log("DAMAGE");
                hitInfo.GetComponent<UD_BossBase>().TakeDamage(damageUpgraded);
                /*if (isVampire)
                {
                    if (PH.currentPlayerHealth < PH.totalPlayerHealthUpgraded - vampireHealthStollen)
                    {
                        PH.currentPlayerHealth = PH.currentPlayerHealth + vampireHealthStollen;
                    }
                    else
                    {
                        PH.currentPlayerHealth = PH.totalPlayerHealthUpgraded;
                    }
                }*/ //POUR NE PAS QUE LE JOUEUR SE HEAL SUR LE BOSS J'AI SUPPR CETTE PARTIE - ULRIC
            }
            if (hitInfo.CompareTag("Tentacle"))
            {
                Debug.Log("DAMAGE");
                hitInfo.GetComponent<UD_TentacleBoss>().TakeDamage(damageUpgraded);
                if (isVampire)
                {
                    if (PH.currentPlayerHealth < PH.totalPlayerHealthUpgraded - vampireHealthStollen)
                    {
                        PH.currentPlayerHealth = PH.currentPlayerHealth + vampireHealthStollen;
                    }
                    else
                    {
                        PH.currentPlayerHealth = PH.totalPlayerHealthUpgraded;
                    }
                }
            }
            if (hitInfo.CompareTag("BossEgg"))
            {
                Debug.Log("DAMAGE");
                hitInfo.GetComponent<UD_EggBoss>().Spawn();
                /*if (isVampire)
                {
                    if (PH.currentPlayerHealth < PH.totalPlayerHealthUpgraded - vampireHealthStollen)
                    {
                        PH.currentPlayerHealth = PH.currentPlayerHealth + vampireHealthStollen;
                    }
                    else
                    {
                        PH.currentPlayerHealth = PH.totalPlayerHealthUpgraded;
                    }
                }*/ //POUR NE PAS QUE LE JOUEUR SE HEAL SUR LES OEUFS J'AI SUPPR CETTE PARTIE - ULRIC
            }


            /*if (hitInfo.CompareTag("Environement"))
            {
                Destroy(gameObject);
            }*/
        }
        if (hasCounterBlade)
        {
            StartCoroutine(CounterBladeCheck());
        }
    }

    private void FinishAttack()
    {
        isAttacking = false;
        anim.SetBool("isAttacking", isAttacking);

    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(attackHitBoxPos.position, attackRadius);
    }

    private IEnumerator CounterBladeCheck()
    {
        counterbladeSlashArea.SetActive(true);
        yield return new WaitForSeconds(counterBladeActiveTimeSet);
        counterbladeSlashArea.SetActive(false);
    }
}
