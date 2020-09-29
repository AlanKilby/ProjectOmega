using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnnemisScript : MonoBehaviour
{
    [SerializeField] private Animator anim;

    public int health;

    public bool takeDamage;

    private void Start()
    {
        takeDamage = false;
        anim = GetComponent<Animator>();
    }

    private void Update()
    {
        UpdateAnim();
        if(health <= 0)
        {
            Destroy(gameObject);
        }
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
}
