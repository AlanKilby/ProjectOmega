using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordAttack : MonoBehaviour
{
    public LayerMask whatIsEnnemy;

    PlayerHealth PH;
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

    [HideInInspector] public int damageUpgraded;
    public int damageTotal;

    public bool gotInput;
    public bool isAttacking;
    public bool isVampire;

    [SerializeField] private Animator anim;

    private void Start()
    {
        PH = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerHealth>();
        anim = GetComponent<Animator>();
        anim.SetBool("canAttack", combatEnabled);
        combatEnabled = true;
        isVampire = false;
        //Ajout Gus
        PM = GetComponent<PlayerMouvement>();
        //
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

            /*if (hitInfo.CompareTag("Environement"))
            {
                Destroy(gameObject);
            }*/
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
}
