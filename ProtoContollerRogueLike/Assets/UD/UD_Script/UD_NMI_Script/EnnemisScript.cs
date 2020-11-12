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

    //Ajout Gus
    private Material matWhite;
    private Material matDefault;
    SpriteRenderer sr;
    //

    public bool takeDamage;

    private void Start()
    {
        takeDamage = false;
        lootDrop = GetComponent<LootDrop>();
        anim = GetComponent<Animator>();
        
        //Ajout Gus
        sr = GetComponent<SpriteRenderer>();
        matWhite = Resources.Load("WhiteFlash", typeof(Material)) as Material;
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
   
    }

    //Ajout Gus
    void ResetMaterial() 
    { 
        sr.material = matDefault; 
    }
    //
        
    public void TakeDamage(int damage)
    {
        health -= damage;
        takeDamage = true;
        
        //Ajout Gus
        sr.material = matWhite;
        if (health <= 0)
        {
            Death();
        }
        else
        {
            Invoke("ResetMaterial", 0.1f);
        }
        //
    }

    /*void UpdateAnim()
    {
        anim.SetBool("takeDamage", takeDamage);
    }*/

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
