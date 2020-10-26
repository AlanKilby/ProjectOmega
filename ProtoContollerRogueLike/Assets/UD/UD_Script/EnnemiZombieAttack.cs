using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnnemiZombieAttack : MonoBehaviour
{
    public LayerMask whatIsPlayer;

    [SerializeField] private Animator anim;

    public int damage;

    [SerializeField] private Transform attackHitBoxPos;
    [SerializeField] private Transform playerPos;
    [SerializeField] private LayerMask whatIsKillable;
    [SerializeField] private float attackRadius;

    public float zombieHitFireRate;
    private float zombieHitFireRateTimer;

    public bool combatEnable;
    [SerializeField] private bool isAttacking;

    private void Start()
    {
        isAttacking = false;
        anim = GetComponent<Animator>();
        playerPos = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
    }

    private void Update()
    {
        UpdateAnim();
        if (combatEnable)
        {
            CheckIfDamage();
            zombieHitFireRateTimer = zombieHitFireRate;
            combatEnable = false;
        }
        CheckCombatEnable();
    }

    void UpdateAnim()
    {
        anim.SetBool("isAttacking", isAttacking);
    }

    public void StopAnimAttack()
    {
        isAttacking = false;
    }

    private void CheckCombatEnable()
    {
        if (!combatEnable)
        {
            zombieHitFireRateTimer -= Time.deltaTime;
            if (zombieHitFireRateTimer < 0.0f)
            {
                combatEnable = true;
            }
        }
    }

    private void CheckIfDamage()
    {
        Collider2D hitInfo = Physics2D.OverlapCircle(attackHitBoxPos.position, attackRadius, whatIsPlayer);
        if (hitInfo != null)
        {
            Rigidbody2D player = hitInfo.GetComponent<Rigidbody2D>();
            if (hitInfo.CompareTag("Player"))
            {
                hitInfo.GetComponent<PlayerHealth>().TakeDamage(damage);
                isAttacking = true;
            }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(attackHitBoxPos.position, attackRadius);
    }
}
