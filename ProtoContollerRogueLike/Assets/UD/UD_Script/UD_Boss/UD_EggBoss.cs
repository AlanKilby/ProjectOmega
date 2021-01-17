using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UD_EggBoss : MonoBehaviour
{
    public CameraSystem thisRoom;

    [SerializeField] GameObject[] spawnerOfThisEggList;
    UD_EggBossSpawner EBS;
    Rigidbody2D rb;
    [SerializeField] float slowDownSpeed;
    float ownRBvelocityX;
    float ownRBvelocityY;
    [SerializeField] int damageOnHitWithPlayer;

    [HideInInspector] public bool isMoving;

    Animator anim;
    private bool explode;
    private bool canSpawn;

    private void Start()
    {
        thisRoom = gameObject.GetComponentInParent<CameraSystem>();
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        isMoving = true;
        canSpawn = true;
        explode = false;
    }

    private void Update()
    {
        if (isMoving)
        {
            //rb.velocity -= new Vector2(slowDownSpeed, slowDownSpeed) * Time.deltaTime;
            ownRBvelocityX = Mathf.Abs(rb.velocity.x);
            ownRBvelocityY = Mathf.Abs(rb.velocity.y);
            if (ownRBvelocityX <= 0.0f && ownRBvelocityY <=0.0f)
            {
                rb.velocity = new Vector3(0, 0, 0);
                isMoving = false;
            }
        }
        anim.SetBool("explode", explode);
    }

    public void Spawn()
    {
        for(int i = 0; i < spawnerOfThisEggList.Length; i++)
        {
            EBS = spawnerOfThisEggList[i].GetComponent<UD_EggBossSpawner>();
            EBS.SpawnEnemies();
        }
        canSpawn = false;
        explode = true;
    }

    public void DestroyHimself()
    {
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (canSpawn)
        {
            if (collision.CompareTag("Player") && thisRoom.playerIsInTheRoom.playerCamera)
            {
                if (isMoving)
                {
                    collision.GetComponent<PlayerHealth>().TakeDamage(damageOnHitWithPlayer);
                }
                else
                {
                    Spawn();
                }
            }
            if (collision.CompareTag("Environement") || collision.CompareTag("BossEgg") && thisRoom.playerIsInTheRoom.playerCamera)
            {
                rb.velocity = new Vector3(0, 0, 0);
                isMoving = false;
            }
        }
    }
}
