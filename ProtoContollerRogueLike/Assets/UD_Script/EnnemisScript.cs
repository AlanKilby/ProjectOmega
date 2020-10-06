using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnnemisScript : MonoBehaviour
{
    public LayerMask whatIsPlayer;

    [SerializeField] EnnemiDrop ED;
    [SerializeField] private Animator anim;

    public int health;
    public int damage;
    public int dropRate;

    [SerializeField] private Transform attackHitBoxPos;
    [SerializeField] private LayerMask whatIsKillable;
    [SerializeField] private float attackRadius;

    public float zombieHitFireRate;
    private float zombieHitFireRateTimer;

    public bool takeDamage;
    public bool combatEnable;

    private void Start()
    {
        takeDamage = false;
        ED = GetComponent<EnnemiDrop>();
        anim = GetComponent<Animator>();
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
    }

    public void TakeDamage(int damage)
    {
        health -= damage;
        takeDamage = true;
    }

    void UpdateAnim()
    {
        anim.SetBool("takeDamage", takeDamage);
    }

    public void StopAnimTakeDamage()
    {
        takeDamage = false;
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

    public void Death()
    {
        ED.ChooseDropType();
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
