using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnnemisScript : MonoBehaviour
{
    public LayerMask whatIsPlayer;

    [SerializeField] LootDrop lootDrop;
    [SerializeField] private Animator anim;

    public int health;
    public int dropRate;

    public bool takeDamage;

    private void Start()
    {
        takeDamage = false;
        lootDrop = GetComponent<LootDrop>();
        anim = GetComponent<Animator>();
    }

    private void Update()
    {
        UpdateAnim();
        if(health <= 0)
        {
            Death();
        }
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
    }

    public void StopAnimTakeDamage()
    {
        takeDamage = false;
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
