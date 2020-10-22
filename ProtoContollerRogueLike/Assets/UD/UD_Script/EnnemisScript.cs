using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnnemisScript : MonoBehaviour
{
    public LayerMask whatIsPlayer;

    [SerializeField] LootDrop lootDrop;
    [SerializeField] private Animator anim;

    public int health;
    public int damage;
    public int dropRate;

    [SerializeField] private Transform attackHitBoxPos;
    [SerializeField] private Transform playerPos;
    [SerializeField] private LayerMask whatIsKillable;
    [SerializeField] private float attackRadius;

    public float zombieHitFireRate;
    private float zombieHitFireRateTimer;

    public bool takeDamage;
    public bool combatEnable;
    [SerializeField] private bool isAttacking;

    private void Start()
    {
        isAttacking = false;
        takeDamage = false;
        lootDrop = GetComponent<LootDrop>();
        anim = GetComponent<Animator>();
        playerPos = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
    }

    private void Update()
    {
        UpdateAnim();
        if(health <= 0)
        {
            /*int randomNumber = Random.Range(1, dropRate);
            print(randomNumber);
            if (ED != null && randomNumber == 1)
            {
                LootDrop();
                Death();
            }
            else Death();*/


            //LootDrop();


            Death();
        }
        if (combatEnable)
        {
            CheckIfDamage();
            zombieHitFireRateTimer = zombieHitFireRate;
            combatEnable = false;
        }
        CheckCombatEnable();
        //PlayerDirection(); //de l'ancien script d'alan, ne convient plus au pathfinding a*
    }

    public void TakeDamage(int damage)
    {
        health -= damage;
        takeDamage = true;
    }

    void UpdateAnim()
    {
        anim.SetBool("takeDamage", takeDamage);
        anim.SetBool("isAttacking", isAttacking);
    }

    public void StopAnimTakeDamage()
    {
        takeDamage = false;
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

    public void LootDrop()
    {
        int rand = Random.Range(1, 100);
        if (rand >= 1 && rand <= dropRate)
        {
            print("loot !");
        }
    }

    void PlayerDirection()
    {
        Vector3 diff = playerPos.position - gameObject.transform.position;
        diff.Normalize();

        float rot_z = Mathf.Atan2(diff.y, diff.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0f, 0f, rot_z - 90);
    }

    public void Death()
    {
        lootDrop.DropLoot();
        Destroy(gameObject);
    }

    /*private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            other.gameObject.GetComponent<PlayerHealth>().TakeDamage(damage);
        }
    }*/
}
