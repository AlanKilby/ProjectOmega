using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordAttack : MonoBehaviour
{
    public LayerMask whatIsEnnemy;

    PlayerHealth PH;
    [SerializeField]
    private bool combatEnabled;
    [SerializeField]
    private Transform attackHitBoxPos;
    [SerializeField]
    private LayerMask whatIsKillable;
    [SerializeField]
    private float attackRadius;
    public float vampireHealthStollen;

    //Ajout Gus
    private string currentAnimation;

    const string PLAYER_SWORD = "PlayerSword";
    //

    public int damage;

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
    }

    private void Update()
    {
        CheckAttacks();
        if (Input.GetButtonDown("Fire2"))
        {
            GetAttackInput();
            ChangeAnimationState(PLAYER_SWORD);

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

    public void CheckAttackHitBox()
    {
        Collider2D hitInfo = Physics2D.OverlapCircle(attackHitBoxPos.position, attackRadius, whatIsEnnemy);
        if (hitInfo != null)
        {

            Rigidbody2D enemy = hitInfo.GetComponent<Rigidbody2D>();
            if (hitInfo.CompareTag("Ennemi"))
            {
                Debug.Log("DAMAGE");
                hitInfo.GetComponent<EnnemisScript>().TakeDamage(damage);
                if (isVampire)
                {
                    if (PH.currentPlayerHealth < PH.totalPlayerHealth - vampireHealthStollen)
                    {
                        PH.currentPlayerHealth = PH.currentPlayerHealth + vampireHealthStollen;
                    }
                    else
                    {
                        PH.currentPlayerHealth = PH.totalPlayerHealth;
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

    
    
    //Ajout Gus
    void ChangeAnimationState(string newAnimation)
    {
        if (currentAnimation == newAnimation) return;

        anim.Play(newAnimation);

        //currentAnimation = newAnimation;
    }
    //
}
