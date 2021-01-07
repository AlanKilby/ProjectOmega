using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnnemiZombieAttack : MonoBehaviour
{
    public LayerMask whatIsPlayer;

    [SerializeField] private Animator anim;
    DifficultyPanel DP;

    public int damage;

    //Ajout Gus
    private string currentAnimation;
    const string ZOMBIE_WALK = "ZombieWalk";
    const string ZOMBIE_ATTACK = "ZombieAttack";
    //

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
        DP = GameObject.Find("DifficultyPanel").GetComponent<DifficultyPanel>();
        //Augmentation des dégâts en fonction de la difficulté et des stages
        damage = (int)Mathf.Round((damage + DP.currentStageDamageBonusForZombie) * DP.currentModDamageMultiplier); 
    }

    private void Update()
    {
        UpdateAnim();
        if (combatEnable)
        {
            CheckIfDamage();
        }
        CheckCombatEnable();
    }

    void UpdateAnim()
    {
        //anim.SetBool("isAttacking", isAttacking);
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
                //Ajout Gus
                ChangeAnimationState(ZOMBIE_WALK);
                //
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
                //Ajout Gus
                ChangeAnimationState(ZOMBIE_ATTACK);
                //
                zombieHitFireRateTimer = zombieHitFireRate;
                combatEnable = false;
            }

        }
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

        currentAnimation = newAnimation;
    }
    //
}
